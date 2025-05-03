using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace I2.Loc
{
	public class TranslationJob_WEB : TranslationJob_WWW
	{
		private Dictionary<string, TranslationQuery> _requests;

		private GoogleTranslation.fnOnTranslationReady _OnTranslationReady;

		public string mErrorMessage;

		private string mCurrentBatch_ToLanguageCode;

		private string mCurrentBatch_FromLanguageCode;

		private List<string> mCurrentBatch_Text;

		private List<KeyValuePair<string, string>> mQueries;

		public TranslationJob_WEB(Dictionary<string, TranslationQuery> requests, GoogleTranslation.fnOnTranslationReady OnTranslationReady)
		{
			_requests = requests;
			_OnTranslationReady = OnTranslationReady;
			FindAllQueries();
			ExecuteNextBatch();
		}

		private void FindAllQueries()
		{
			mQueries = new List<KeyValuePair<string, string>>();
			foreach (KeyValuePair<string, TranslationQuery> request in _requests)
			{
				string[] targetLanguagesCode = request.Value.TargetLanguagesCode;
				foreach (string text in targetLanguagesCode)
				{
					mQueries.Add(new KeyValuePair<string, string>(request.Value.OrigText, request.Value.LanguageCode + ":" + text));
				}
			}
			mQueries.Sort((KeyValuePair<string, string> a, KeyValuePair<string, string> b) => a.Value.CompareTo(b.Value));
		}

		private void ExecuteNextBatch()
		{
			if (mQueries.Count == 0)
			{
				mJobState = eJobState.Succeeded;
				return;
			}
			mCurrentBatch_Text = new List<string>();
			string text = null;
			int num = 200;
			StringBuilder stringBuilder = new StringBuilder();
			int i;
			for (i = 0; i < mQueries.Count; i++)
			{
				string key = mQueries[i].Key;
				string value = mQueries[i].Value;
				if (text == null || value == text)
				{
					if (i != 0)
					{
						stringBuilder.Append("|||");
					}
					stringBuilder.Append(key);
					mCurrentBatch_Text.Add(key);
					text = value;
				}
				if (stringBuilder.Length > num)
				{
					break;
				}
			}
			mQueries.RemoveRange(0, i);
			string[] array = text.Split(':');
			mCurrentBatch_FromLanguageCode = array[0];
			mCurrentBatch_ToLanguageCode = array[1];
			string text2 = $"http://www.google.com/translate_t?hl=en&vi=c&ie=UTF8&oe=UTF8&submit=Translate&langpair={mCurrentBatch_FromLanguageCode}|{mCurrentBatch_ToLanguageCode}&text={Uri.EscapeUriString(stringBuilder.ToString())}";
			Debug.Log(text2);
			www = UnityWebRequest.Get(text2);
			I2Utils.SendWebRequest(www);
		}

		public override eJobState GetState()
		{
			if (www != null && www.isDone)
			{
				ProcessResult(www.downloadHandler.data, www.error);
				www.Dispose();
				www = null;
			}
			if (www == null)
			{
				ExecuteNextBatch();
			}
			return mJobState;
		}

		public void ProcessResult(byte[] bytes, string errorMsg)
		{
			if (string.IsNullOrEmpty(errorMsg))
			{
				string @string = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
				Debug.Log(ParseTranslationResult(@string, "aab"));
				if (string.IsNullOrEmpty(errorMsg))
				{
					if (_OnTranslationReady != null)
					{
						_OnTranslationReady(_requests, null);
					}
					return;
				}
			}
			mJobState = eJobState.Failed;
			mErrorMessage = errorMsg;
		}

		private string ParseTranslationResult(string html, string OriginalText)
		{
			try
			{
				int num = html.IndexOf("TRANSLATED_TEXT='") + "TRANSLATED_TEXT='".Length;
				int num2 = html.IndexOf("';var", num);
				string input = html.Substring(num, num2 - num);
				input = Regex.Replace(input, "\\\\x([a-fA-F0-9]{2})", (Match match) => char.ConvertFromUtf32(int.Parse(match.Groups[1].Value, NumberStyles.HexNumber)));
				input = Regex.Replace(input, "&#(\\d+);", (Match match) => char.ConvertFromUtf32(int.Parse(match.Groups[1].Value)));
				input = input.Replace("<br>", "\n");
				if (OriginalText.ToUpper() == OriginalText)
				{
					input = input.ToUpper();
				}
				else if (GoogleTranslation.UppercaseFirst(OriginalText) == OriginalText)
				{
					input = GoogleTranslation.UppercaseFirst(input);
				}
				else if (GoogleTranslation.TitleCase(OriginalText) == OriginalText)
				{
					input = GoogleTranslation.TitleCase(input);
				}
				return input;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message);
				return string.Empty;
			}
		}
	}
}
