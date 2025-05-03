using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

namespace I2.Loc
{
	public static class GoogleTranslation
	{
		public delegate void fnOnTranslated(string Translation, string Error);

		public delegate void fnOnTranslationReady(Dictionary<string, TranslationQuery> dict, string error);

		private static List<UnityWebRequest> mCurrentTranslations = new List<UnityWebRequest>();

		private static List<TranslationJob> mTranslationJobs = new List<TranslationJob>();

		public static bool CanTranslate()
		{
			if (LocalizationManager.Sources.Count > 0)
			{
				return !string.IsNullOrEmpty(LocalizationManager.GetWebServiceURL());
			}
			return false;
		}

		public static void Translate(string text, string LanguageCodeFrom, string LanguageCodeTo, fnOnTranslated OnTranslationReady)
		{
			LocalizationManager.InitializeIfNeeded();
			if (!CanTranslate())
			{
				OnTranslationReady(null, "WebService is not set correctly or needs to be reinstalled");
				return;
			}
			if (LanguageCodeTo == LanguageCodeFrom)
			{
				OnTranslationReady(text, null);
				return;
			}
			Dictionary<string, TranslationQuery> queries = new Dictionary<string, TranslationQuery>();
			if (string.IsNullOrEmpty(LanguageCodeTo))
			{
				OnTranslationReady(string.Empty, null);
				return;
			}
			CreateQueries(text, LanguageCodeFrom, LanguageCodeTo, queries);
			Translate(queries, delegate(Dictionary<string, TranslationQuery> results, string error)
			{
				if (!string.IsNullOrEmpty(error) || results.Count == 0)
				{
					OnTranslationReady(null, error);
				}
				else
				{
					string translation = RebuildTranslation(text, queries, LanguageCodeTo);
					OnTranslationReady(translation, null);
				}
			});
		}

		public static string ForceTranslate(string text, string LanguageCodeFrom, string LanguageCodeTo)
		{
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dictionary);
			TranslationJob_Main translationJob_Main = new TranslationJob_Main(dictionary, null);
			while (true)
			{
				switch (translationJob_Main.GetState())
				{
				case TranslationJob.eJobState.Running:
					break;
				case TranslationJob.eJobState.Failed:
					return null;
				default:
					return GetQueryResult(text, "", dictionary);
				}
			}
		}

		public static void Translate(Dictionary<string, TranslationQuery> requests, fnOnTranslationReady OnTranslationReady, bool usePOST = true)
		{
			AddTranslationJob(new TranslationJob_Main(requests, OnTranslationReady));
		}

		public static bool ForceTranslate(Dictionary<string, TranslationQuery> requests, bool usePOST = true)
		{
			TranslationJob_Main translationJob_Main = new TranslationJob_Main(requests, null);
			while (true)
			{
				switch (translationJob_Main.GetState())
				{
				case TranslationJob.eJobState.Running:
					break;
				case TranslationJob.eJobState.Failed:
					return false;
				default:
					return true;
				}
			}
		}

		public static List<string> ConvertTranslationRequest(Dictionary<string, TranslationQuery> requests, bool encodeGET)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, TranslationQuery> request in requests)
			{
				TranslationQuery value = request.Value;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("<I2Loc>");
				}
				stringBuilder.Append(GoogleLanguages.GetGoogleLanguageCode(value.LanguageCode));
				stringBuilder.Append(":");
				for (int i = 0; i < value.TargetLanguagesCode.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(GoogleLanguages.GetGoogleLanguageCode(value.TargetLanguagesCode[i]));
				}
				stringBuilder.Append("=");
				string text = ((TitleCase(value.Text) == value.Text) ? value.Text.ToLowerInvariant() : value.Text);
				if (!encodeGET)
				{
					stringBuilder.Append(text);
					continue;
				}
				stringBuilder.Append(Uri.EscapeDataString(text));
				if (stringBuilder.Length > 4000)
				{
					list.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
			}
			list.Add(stringBuilder.ToString());
			return list;
		}

		private static void AddTranslationJob(TranslationJob job)
		{
			mTranslationJobs.Add(job);
			if (mTranslationJobs.Count == 1)
			{
				CoroutineManager.Start(WaitForTranslations());
			}
		}

		private static IEnumerator WaitForTranslations()
		{
			while (mTranslationJobs.Count > 0)
			{
				TranslationJob[] array = mTranslationJobs.ToArray();
				foreach (TranslationJob translationJob in array)
				{
					if (translationJob.GetState() != 0)
					{
						mTranslationJobs.Remove(translationJob);
					}
				}
				yield return null;
			}
		}

		public static string ParseTranslationResult(string html, Dictionary<string, TranslationQuery> requests)
		{
			if (html.StartsWith("<!DOCTYPE html>") || html.StartsWith("<HTML>"))
			{
				if (html.Contains("The script completed but did not return anything"))
				{
					return "The current Google WebService is not supported.\nPlease, delete the WebService from the Google Drive and Install the latest version.";
				}
				if (html.Contains("Service invoked too many times in a short time"))
				{
					return "";
				}
				return "There was a problem contacting the WebService. Please try again later\n" + html;
			}
			string[] array = html.Split(new string[1] { "<I2Loc>" }, StringSplitOptions.None);
			string[] separator = new string[1] { "<i2>" };
			int num = 0;
			string[] array2 = requests.Keys.ToArray();
			foreach (string text in array2)
			{
				TranslationQuery value = FindQueryFromOrigText(text, requests);
				string text2 = array[num++];
				if (value.Tags != null)
				{
					for (int num2 = value.Tags.Length - 1; num2 >= 0; num2--)
					{
						text2 = text2.Replace(GetGoogleNoTranslateTag(num2), value.Tags[num2]);
					}
				}
				value.Results = text2.Split(separator, StringSplitOptions.None);
				if (TitleCase(text) == text)
				{
					for (int j = 0; j < value.Results.Length; j++)
					{
						value.Results[j] = TitleCase(value.Results[j]);
					}
				}
				requests[value.OrigText] = value;
			}
			return null;
		}

		public static bool IsTranslating()
		{
			if (mCurrentTranslations.Count <= 0)
			{
				return mTranslationJobs.Count > 0;
			}
			return true;
		}

		public static void CancelCurrentGoogleTranslations()
		{
			mCurrentTranslations.Clear();
			foreach (TranslationJob mTranslationJob in mTranslationJobs)
			{
				mTranslationJob.Dispose();
			}
			mTranslationJobs.Clear();
		}

		public static void CreateQueries(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!text.Contains("[i2s_"))
			{
				CreateQueries_Plurals(text, LanguageCodeFrom, LanguageCodeTo, dict);
				return;
			}
			foreach (KeyValuePair<string, string> specialization in SpecializationManager.GetSpecializations(text))
			{
				CreateQueries_Plurals(specialization.Value, LanguageCodeFrom, LanguageCodeTo, dict);
			}
		}

		private static void CreateQueries_Plurals(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			bool flag = text.Contains("{[#");
			bool flag2 = text.Contains("[i2p_");
			if (!HasParameters(text) || (!flag && !flag2))
			{
				AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dict);
				return;
			}
			bool forceTag = flag;
			for (ePluralType ePluralType2 = ePluralType.Zero; ePluralType2 <= ePluralType.Plural; ePluralType2++)
			{
				string pluralType = ePluralType2.ToString();
				if (GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, pluralType))
				{
					string text2 = GetPluralText(text, pluralType);
					int pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType2);
					string pluralParameter = GetPluralParameter(text2, forceTag);
					if (!string.IsNullOrEmpty(pluralParameter))
					{
						text2 = text2.Replace(pluralParameter, pluralTestNumber.ToString());
					}
					AddQuery(text2, LanguageCodeFrom, LanguageCodeTo, dict);
				}
			}
		}

		public static void AddQuery(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!dict.ContainsKey(text))
			{
				TranslationQuery translationQuery = default(TranslationQuery);
				translationQuery.OrigText = text;
				translationQuery.LanguageCode = LanguageCodeFrom;
				translationQuery.TargetLanguagesCode = new string[1] { LanguageCodeTo };
				TranslationQuery query = translationQuery;
				query.Text = text;
				ParseNonTranslatableElements(ref query);
				dict[text] = query;
			}
			else
			{
				TranslationQuery value = dict[text];
				if (Array.IndexOf(value.TargetLanguagesCode, LanguageCodeTo) < 0)
				{
					value.TargetLanguagesCode = value.TargetLanguagesCode.Concat(new string[1] { LanguageCodeTo }).Distinct().ToArray();
				}
				dict[text] = value;
			}
		}

		private static string GetTranslation(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!dict.ContainsKey(text))
			{
				return null;
			}
			TranslationQuery translationQuery = dict[text];
			int num = Array.IndexOf(translationQuery.TargetLanguagesCode, LanguageCodeTo);
			if (num < 0)
			{
				return "";
			}
			if (translationQuery.Results == null)
			{
				return "";
			}
			return translationQuery.Results[num];
		}

		private static TranslationQuery FindQueryFromOrigText(string origText, Dictionary<string, TranslationQuery> dict)
		{
			foreach (KeyValuePair<string, TranslationQuery> item in dict)
			{
				if (item.Value.OrigText == origText)
				{
					return item.Value;
				}
			}
			return default(TranslationQuery);
		}

		public static bool HasParameters(string text)
		{
			int num = text.IndexOf("{[");
			if (num < 0)
			{
				return false;
			}
			return text.IndexOf("]}", num) > 0;
		}

		public static string GetPluralParameter(string text, bool forceTag)
		{
			int num = text.IndexOf("{[#");
			if (num < 0)
			{
				if (forceTag)
				{
					return null;
				}
				num = text.IndexOf("{[");
			}
			if (num < 0)
			{
				return null;
			}
			int num2 = text.IndexOf("]}", num + 2);
			if (num2 < 0)
			{
				return null;
			}
			return text.Substring(num, num2 - num + 2);
		}

		public static string GetPluralText(string text, string pluralType)
		{
			pluralType = "[i2p_" + pluralType + "]";
			int num = text.IndexOf(pluralType);
			if (num >= 0)
			{
				num += pluralType.Length;
				int num2 = text.IndexOf("[i2p_", num);
				if (num2 < 0)
				{
					num2 = text.Length;
				}
				return text.Substring(num, num2 - num);
			}
			num = text.IndexOf("[i2p_");
			if (num < 0)
			{
				return text;
			}
			if (num > 0)
			{
				return text.Substring(0, num);
			}
			num = text.IndexOf("]");
			if (num < 0)
			{
				return text;
			}
			num++;
			int num3 = text.IndexOf("[i2p_", num);
			if (num3 < 0)
			{
				num3 = text.Length;
			}
			return text.Substring(num, num3 - num);
		}

		private static int FindClosingTag(string tag, MatchCollection matches, int startIndex)
		{
			int i = startIndex;
			for (int count = matches.Count; i < count; i++)
			{
				string captureMatch = I2Utils.GetCaptureMatch(matches[i]);
				if (captureMatch[0] == '/' && tag.StartsWith(captureMatch.Substring(1)))
				{
					return i;
				}
			}
			return -1;
		}

		private static string GetGoogleNoTranslateTag(int tagNumber)
		{
			if (tagNumber < 70)
			{
				return "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++".Substring(0, tagNumber + 1);
			}
			string text = "";
			for (int i = -1; i < tagNumber; i++)
			{
				text += "+";
			}
			return text;
		}

		private static void ParseNonTranslatableElements(ref TranslationQuery query)
		{
			MatchCollection matchCollection = Regex.Matches(query.Text, "\\{\\[(.*?)]}|\\[(.*?)]|\\<(.*?)>");
			if (matchCollection == null || matchCollection.Count == 0)
			{
				return;
			}
			string text = query.Text;
			List<string> list = new List<string>();
			int i = 0;
			for (int count = matchCollection.Count; i < count; i++)
			{
				string captureMatch = I2Utils.GetCaptureMatch(matchCollection[i]);
				int num = FindClosingTag(captureMatch, matchCollection, i);
				if (num < 0)
				{
					string text2 = matchCollection[i].ToString();
					if (text2.StartsWith("{[") && text2.EndsWith("]}"))
					{
						text = text.Replace(text2, GetGoogleNoTranslateTag(list.Count) + " ");
						list.Add(text2);
					}
				}
				else if (captureMatch == "i2nt")
				{
					string text3 = query.Text.Substring(matchCollection[i].Index, matchCollection[num].Index - matchCollection[i].Index + matchCollection[num].Length);
					text = text.Replace(text3, GetGoogleNoTranslateTag(list.Count) + " ");
					list.Add(text3);
				}
				else
				{
					string text4 = matchCollection[i].ToString();
					text = text.Replace(text4, GetGoogleNoTranslateTag(list.Count) + " ");
					list.Add(text4);
					string text5 = matchCollection[num].ToString();
					text = text.Replace(text5, GetGoogleNoTranslateTag(list.Count) + " ");
					list.Add(text5);
				}
			}
			query.Text = text;
			query.Tags = list.ToArray();
		}

		public static string GetQueryResult(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!dict.ContainsKey(text))
			{
				return null;
			}
			TranslationQuery translationQuery = dict[text];
			if (translationQuery.Results == null || translationQuery.Results.Length < 0)
			{
				return null;
			}
			if (string.IsNullOrEmpty(LanguageCodeTo))
			{
				return translationQuery.Results[0];
			}
			int num = Array.IndexOf(translationQuery.TargetLanguagesCode, LanguageCodeTo);
			if (num < 0)
			{
				return null;
			}
			return translationQuery.Results[num];
		}

		public static string RebuildTranslation(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			if (!text.Contains("[i2s_"))
			{
				return RebuildTranslation_Plural(text, dict, LanguageCodeTo);
			}
			Dictionary<string, string> specializations = SpecializationManager.GetSpecializations(text);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> item in specializations)
			{
				dictionary[item.Key] = RebuildTranslation_Plural(item.Value, dict, LanguageCodeTo);
			}
			return SpecializationManager.SetSpecializedText(dictionary);
		}

		private static string RebuildTranslation_Plural(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			bool flag = text.Contains("{[#");
			bool flag2 = text.Contains("[i2p_");
			if (!HasParameters(text) || (!flag && !flag2))
			{
				return GetTranslation(text, LanguageCodeTo, dict);
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text2 = null;
			bool forceTag = flag;
			for (ePluralType ePluralType2 = ePluralType.Plural; ePluralType2 >= ePluralType.Zero; ePluralType2--)
			{
				string text3 = ePluralType2.ToString();
				if (!GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, text3))
				{
					continue;
				}
				string text4 = GetPluralText(text, text3);
				int pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType2);
				string pluralParameter = GetPluralParameter(text4, forceTag);
				if (!string.IsNullOrEmpty(pluralParameter))
				{
					text4 = text4.Replace(pluralParameter, pluralTestNumber.ToString());
				}
				string text5 = GetTranslation(text4, LanguageCodeTo, dict);
				if (!string.IsNullOrEmpty(pluralParameter))
				{
					text5 = text5.Replace(pluralTestNumber.ToString(), pluralParameter);
				}
				if (ePluralType2 == ePluralType.Plural)
				{
					text2 = text5;
				}
				else
				{
					if (text5 == text2)
					{
						continue;
					}
					stringBuilder.AppendFormat("[i2p_{0}]", text3);
				}
				stringBuilder.Append(text5);
			}
			return stringBuilder.ToString();
		}

		public static string UppercaseFirst(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			char[] array = s.ToLower().ToCharArray();
			array[0] = char.ToUpper(array[0]);
			return new string(array);
		}

		public static string TitleCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
		}
	}
}
