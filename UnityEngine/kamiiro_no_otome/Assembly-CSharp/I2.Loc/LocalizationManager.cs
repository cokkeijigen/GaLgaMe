using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace I2.Loc
{
	public static class LocalizationManager
	{
		public delegate object _GetParam(string param);

		public delegate void OnLocalizeCallback();

		private static string mCurrentLanguage;

		private static string mLanguageCode;

		private static CultureInfo mCurrentCulture;

		private static bool mChangeCultureInfo = false;

		public static bool IsRight2Left = false;

		public static bool HasJoinedWords = false;

		public static List<ILocalizationParamsManager> ParamManagers = new List<ILocalizationParamsManager>();

		private static string[] LanguagesRTL = new string[21]
		{
			"ar-DZ", "ar", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA",
			"ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-AE", "ar-YE", "fa", "he", "ur",
			"ji"
		};

		public static List<LanguageSourceData> Sources = new List<LanguageSourceData>();

		public static string[] GlobalSources = new string[1] { "I2Languages" };

		public static Func<LanguageSourceData, bool> Callback_AllowSyncFromGoogle = null;

		private static string mCurrentDeviceLanguage;

		public static List<ILocalizeTargetDescriptor> mLocalizeTargets = new List<ILocalizeTargetDescriptor>();

		private static bool mLocalizeIsScheduled = false;

		private static bool mLocalizeIsScheduledWithForcedValue = false;

		public static bool HighlightLocalizedTargets = false;

		public static string CurrentLanguage
		{
			get
			{
				InitializeIfNeeded();
				return mCurrentLanguage;
			}
			set
			{
				InitializeIfNeeded();
				string supportedLanguage = GetSupportedLanguage(value);
				if (!string.IsNullOrEmpty(supportedLanguage) && mCurrentLanguage != supportedLanguage)
				{
					SetLanguageAndCode(supportedLanguage, GetLanguageCode(supportedLanguage));
				}
			}
		}

		public static string CurrentLanguageCode
		{
			get
			{
				InitializeIfNeeded();
				return mLanguageCode;
			}
			set
			{
				InitializeIfNeeded();
				if (mLanguageCode != value)
				{
					string languageFromCode = GetLanguageFromCode(value);
					if (!string.IsNullOrEmpty(languageFromCode))
					{
						SetLanguageAndCode(languageFromCode, value);
					}
				}
			}
		}

		public static string CurrentRegion
		{
			get
			{
				string currentLanguage = CurrentLanguage;
				int num = currentLanguage.IndexOfAny("/\\".ToCharArray());
				if (num > 0)
				{
					return currentLanguage.Substring(num + 1);
				}
				num = currentLanguage.IndexOfAny("[(".ToCharArray());
				int num2 = currentLanguage.LastIndexOfAny("])".ToCharArray());
				if (num > 0 && num != num2)
				{
					return currentLanguage.Substring(num + 1, num2 - num - 1);
				}
				return string.Empty;
			}
			set
			{
				string text = CurrentLanguage;
				int num = text.IndexOfAny("/\\".ToCharArray());
				if (num > 0)
				{
					CurrentLanguage = text.Substring(num + 1) + value;
					return;
				}
				num = text.IndexOfAny("[(".ToCharArray());
				int num2 = text.LastIndexOfAny("])".ToCharArray());
				if (num > 0 && num != num2)
				{
					text = text.Substring(num);
				}
				CurrentLanguage = text + "(" + value + ")";
			}
		}

		public static string CurrentRegionCode
		{
			get
			{
				string currentLanguageCode = CurrentLanguageCode;
				int num = currentLanguageCode.IndexOfAny(" -_/\\".ToCharArray());
				if (num >= 0)
				{
					return currentLanguageCode.Substring(num + 1);
				}
				return string.Empty;
			}
			set
			{
				string text = CurrentLanguageCode;
				int num = text.IndexOfAny(" -_/\\".ToCharArray());
				if (num > 0)
				{
					text = text.Substring(0, num);
				}
				CurrentLanguageCode = text + "-" + value;
			}
		}

		public static CultureInfo CurrentCulture => mCurrentCulture;

		public static event OnLocalizeCallback OnLocalizeEvent;

		public static void InitializeIfNeeded()
		{
			if (string.IsNullOrEmpty(mCurrentLanguage) || Sources.Count == 0)
			{
				AutoLoadGlobalParamManagers();
				UpdateSources();
				SelectStartupLanguage();
			}
		}

		public static string GetVersion()
		{
			return "2.8.13 f1";
		}

		public static int GetRequiredWebServiceVersion()
		{
			return 5;
		}

		public static string GetWebServiceURL(LanguageSourceData source = null)
		{
			if (source != null && !string.IsNullOrEmpty(source.Google_WebServiceURL))
			{
				return source.Google_WebServiceURL;
			}
			InitializeIfNeeded();
			for (int i = 0; i < Sources.Count; i++)
			{
				if (Sources[i] != null && !string.IsNullOrEmpty(Sources[i].Google_WebServiceURL))
				{
					return Sources[i].Google_WebServiceURL;
				}
			}
			return string.Empty;
		}

		public static void SetLanguageAndCode(string LanguageName, string LanguageCode, bool RememberLanguage = true, bool Force = false)
		{
			if (mCurrentLanguage != LanguageName || mLanguageCode != LanguageCode || Force)
			{
				if (RememberLanguage)
				{
					PersistentStorage.SetSetting_String("I2 Language", LanguageName);
				}
				mCurrentLanguage = LanguageName;
				mLanguageCode = LanguageCode;
				mCurrentCulture = CreateCultureForCode(LanguageCode);
				if (mChangeCultureInfo)
				{
					SetCurrentCultureInfo();
				}
				IsRight2Left = IsRTL(mLanguageCode);
				HasJoinedWords = GoogleLanguages.LanguageCode_HasJoinedWord(mLanguageCode);
				LocalizeAll(Force);
			}
		}

		private static CultureInfo CreateCultureForCode(string code)
		{
			try
			{
				return CultureInfo.CreateSpecificCulture(code);
			}
			catch (Exception)
			{
				return CultureInfo.InvariantCulture;
			}
		}

		public static void EnableChangingCultureInfo(bool bEnable)
		{
			if (!mChangeCultureInfo && bEnable)
			{
				SetCurrentCultureInfo();
			}
			mChangeCultureInfo = bEnable;
		}

		private static void SetCurrentCultureInfo()
		{
			Thread.CurrentThread.CurrentCulture = mCurrentCulture;
		}

		private static void SelectStartupLanguage()
		{
			if (Sources.Count == 0)
			{
				return;
			}
			string setting_String = PersistentStorage.GetSetting_String("I2 Language", string.Empty);
			string currentDeviceLanguage = GetCurrentDeviceLanguage();
			if (!string.IsNullOrEmpty(setting_String) && HasLanguage(setting_String, AllowDiscartingRegion: true, Initialize: false))
			{
				SetLanguageAndCode(setting_String, GetLanguageCode(setting_String));
				return;
			}
			if (!Sources[0].IgnoreDeviceLanguage)
			{
				string supportedLanguage = GetSupportedLanguage(currentDeviceLanguage, ignoreDisabled: true);
				if (!string.IsNullOrEmpty(supportedLanguage))
				{
					SetLanguageAndCode(supportedLanguage, GetLanguageCode(supportedLanguage), RememberLanguage: false);
					return;
				}
			}
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				if (Sources[i].mLanguages.Count <= 0)
				{
					continue;
				}
				for (int j = 0; j < Sources[i].mLanguages.Count; j++)
				{
					if (Sources[i].mLanguages[j].IsEnabled())
					{
						SetLanguageAndCode(Sources[i].mLanguages[j].Name, Sources[i].mLanguages[j].Code, RememberLanguage: false);
						return;
					}
				}
			}
		}

		public static bool HasLanguage(string Language, bool AllowDiscartingRegion = true, bool Initialize = true, bool SkipDisabled = true)
		{
			if (Initialize)
			{
				InitializeIfNeeded();
			}
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				if (Sources[i].GetLanguageIndex(Language, AllowDiscartingRegion: false, SkipDisabled) >= 0)
				{
					return true;
				}
			}
			if (AllowDiscartingRegion)
			{
				int j = 0;
				for (int count2 = Sources.Count; j < count2; j++)
				{
					if (Sources[j].GetLanguageIndex(Language, AllowDiscartingRegion: true, SkipDisabled) >= 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static string GetSupportedLanguage(string Language, bool ignoreDisabled = false)
		{
			string languageCode = GoogleLanguages.GetLanguageCode(Language);
			if (!string.IsNullOrEmpty(languageCode))
			{
				int i = 0;
				for (int count = Sources.Count; i < count; i++)
				{
					int languageIndexFromCode = Sources[i].GetLanguageIndexFromCode(languageCode, exactMatch: true, ignoreDisabled);
					if (languageIndexFromCode >= 0)
					{
						return Sources[i].mLanguages[languageIndexFromCode].Name;
					}
				}
				int j = 0;
				for (int count2 = Sources.Count; j < count2; j++)
				{
					int languageIndexFromCode2 = Sources[j].GetLanguageIndexFromCode(languageCode, exactMatch: false, ignoreDisabled);
					if (languageIndexFromCode2 >= 0)
					{
						return Sources[j].mLanguages[languageIndexFromCode2].Name;
					}
				}
			}
			int k = 0;
			for (int count3 = Sources.Count; k < count3; k++)
			{
				int languageIndex = Sources[k].GetLanguageIndex(Language, AllowDiscartingRegion: false, ignoreDisabled);
				if (languageIndex >= 0)
				{
					return Sources[k].mLanguages[languageIndex].Name;
				}
			}
			int l = 0;
			for (int count4 = Sources.Count; l < count4; l++)
			{
				int languageIndex2 = Sources[l].GetLanguageIndex(Language, AllowDiscartingRegion: true, ignoreDisabled);
				if (languageIndex2 >= 0)
				{
					return Sources[l].mLanguages[languageIndex2].Name;
				}
			}
			return string.Empty;
		}

		public static string GetLanguageCode(string Language)
		{
			if (Sources.Count == 0)
			{
				UpdateSources();
			}
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				int languageIndex = Sources[i].GetLanguageIndex(Language);
				if (languageIndex >= 0)
				{
					return Sources[i].mLanguages[languageIndex].Code;
				}
			}
			return string.Empty;
		}

		public static string GetLanguageFromCode(string Code, bool exactMatch = true)
		{
			if (Sources.Count == 0)
			{
				UpdateSources();
			}
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				int languageIndexFromCode = Sources[i].GetLanguageIndexFromCode(Code, exactMatch);
				if (languageIndexFromCode >= 0)
				{
					return Sources[i].mLanguages[languageIndexFromCode].Name;
				}
			}
			return string.Empty;
		}

		public static List<string> GetAllLanguages(bool SkipDisabled = true)
		{
			if (Sources.Count == 0)
			{
				UpdateSources();
			}
			List<string> Languages = new List<string>();
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				Languages.AddRange(from x in Sources[i].GetLanguages(SkipDisabled)
					where !Languages.Contains(x)
					select x);
			}
			return Languages;
		}

		public static List<string> GetAllLanguagesCode(bool allowRegions = true, bool SkipDisabled = true)
		{
			List<string> Languages = new List<string>();
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				Languages.AddRange(from x in Sources[i].GetLanguagesCode(allowRegions, SkipDisabled)
					where !Languages.Contains(x)
					select x);
			}
			return Languages;
		}

		public static bool IsLanguageEnabled(string Language)
		{
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				if (!Sources[i].IsLanguageEnabled(Language))
				{
					return false;
				}
			}
			return true;
		}

		private static void LoadCurrentLanguage()
		{
			for (int i = 0; i < Sources.Count; i++)
			{
				int languageIndex = Sources[i].GetLanguageIndex(mCurrentLanguage, AllowDiscartingRegion: true, SkipDisabled: false);
				Sources[i].LoadLanguage(languageIndex, UnloadOtherLanguages: true, useFallback: true, onlyCurrentSpecialization: true, forceLoad: false);
			}
		}

		public static void PreviewLanguage(string NewLanguage)
		{
			mCurrentLanguage = NewLanguage;
			mLanguageCode = GetLanguageCode(mCurrentLanguage);
			IsRight2Left = IsRTL(mLanguageCode);
			HasJoinedWords = GoogleLanguages.LanguageCode_HasJoinedWord(mLanguageCode);
		}

		public static void AutoLoadGlobalParamManagers()
		{
			LocalizationParamsManager[] array = UnityEngine.Object.FindObjectsOfType<LocalizationParamsManager>();
			foreach (LocalizationParamsManager localizationParamsManager in array)
			{
				if (localizationParamsManager._IsGlobalManager && !ParamManagers.Contains(localizationParamsManager))
				{
					Debug.Log(localizationParamsManager);
					ParamManagers.Add(localizationParamsManager);
				}
			}
		}

		public static void ApplyLocalizationParams(ref string translation, bool allowLocalizedParameters = true)
		{
			ApplyLocalizationParams(ref translation, (string p) => GetLocalizationParam(p, null), allowLocalizedParameters);
		}

		public static void ApplyLocalizationParams(ref string translation, GameObject root, bool allowLocalizedParameters = true)
		{
			ApplyLocalizationParams(ref translation, (string p) => GetLocalizationParam(p, root), allowLocalizedParameters);
		}

		public static void ApplyLocalizationParams(ref string translation, Dictionary<string, object> parameters, bool allowLocalizedParameters = true)
		{
			ApplyLocalizationParams(ref translation, delegate(string p)
			{
				object value = null;
				return parameters.TryGetValue(p, out value) ? value : null;
			}, allowLocalizedParameters);
		}

		public static void ApplyLocalizationParams(ref string translation, _GetParam getParam, bool allowLocalizedParameters = true)
		{
			if (translation == null)
			{
				return;
			}
			string text = null;
			int num = 0;
			int length = translation.Length;
			int num2 = 0;
			while (num2 >= 0 && num2 < translation.Length)
			{
				int num3 = translation.IndexOf("{[", num2);
				if (num3 < 0)
				{
					break;
				}
				int num4 = translation.IndexOf("]}", num3);
				if (num4 < 0)
				{
					break;
				}
				int num5 = translation.IndexOf("{[", num3 + 1);
				if (num5 > 0 && num5 < num4)
				{
					num2 = num5;
					continue;
				}
				int num6 = ((translation[num3 + 2] == '#') ? 3 : 2);
				string param = translation.Substring(num3 + num6, num4 - num3 - num6);
				string text2 = (string)getParam(param);
				if (text2 != null)
				{
					if (allowLocalizedParameters)
					{
						LanguageSourceData source;
						TermData termData = GetTermData(text2, out source);
						if (termData != null)
						{
							int languageIndex = source.GetLanguageIndex(CurrentLanguage);
							if (languageIndex >= 0)
							{
								text2 = termData.GetTranslation(languageIndex);
							}
						}
					}
					string oldValue = translation.Substring(num3, num4 - num3 + 2);
					translation = translation.Replace(oldValue, text2);
					int result = 0;
					if (int.TryParse(text2, out result))
					{
						text = GoogleLanguages.GetPluralType(CurrentLanguageCode, result).ToString();
					}
					num2 = num3 + text2.Length;
				}
				else
				{
					num2 = num4 + 2;
				}
			}
			if (text != null)
			{
				string text3 = "[i2p_" + text + "]";
				num = translation.IndexOf(text3, StringComparison.OrdinalIgnoreCase);
				num = ((num >= 0) ? (num + text3.Length) : 0);
				length = translation.IndexOf("[i2p_", num + 1, StringComparison.OrdinalIgnoreCase);
				if (length < 0)
				{
					length = translation.Length;
				}
				translation = translation.Substring(num, length - num);
			}
		}

		internal static string GetLocalizationParam(string ParamName, GameObject root)
		{
			string text = null;
			if ((bool)root)
			{
				MonoBehaviour[] components = root.GetComponents<MonoBehaviour>();
				int i = 0;
				for (int num = components.Length; i < num; i++)
				{
					if (components[i] is ILocalizationParamsManager localizationParamsManager && components[i].enabled)
					{
						text = localizationParamsManager.GetParameterValue(ParamName);
						if (text != null)
						{
							return text;
						}
					}
				}
			}
			int j = 0;
			for (int count = ParamManagers.Count; j < count; j++)
			{
				text = ParamManagers[j].GetParameterValue(ParamName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		private static string GetPluralType(MatchCollection matches, string langCode, _GetParam getParam)
		{
			int i = 0;
			for (int count = matches.Count; i < count; i++)
			{
				Match match = matches[i];
				string value = match.Groups[match.Groups.Count - 1].Value;
				string text = (string)getParam(value);
				if (text != null)
				{
					int result = 0;
					if (int.TryParse(text, out result))
					{
						return GoogleLanguages.GetPluralType(langCode, result).ToString();
					}
				}
			}
			return null;
		}

		public static string ApplyRTLfix(string line)
		{
			return ApplyRTLfix(line, 0, ignoreNumbers: true);
		}

		public static string ApplyRTLfix(string line, int maxCharacters, bool ignoreNumbers)
		{
			if (string.IsNullOrEmpty(line))
			{
				return line;
			}
			char c = line[0];
			if (c == '!' || c == '.' || c == '?')
			{
				line = line.Substring(1) + c;
			}
			int tagStart = -1;
			int num = 0;
			int num2 = 40000;
			num = 0;
			List<string> list = new List<string>();
			while (I2Utils.FindNextTag(line, num, out tagStart, out num))
			{
				string text = "@@" + (char)(num2 + list.Count) + "@@";
				list.Add(line.Substring(tagStart, num - tagStart + 1));
				line = line.Substring(0, tagStart) + text + line.Substring(num + 1);
				num = tagStart + 5;
			}
			line = line.Replace("\r\n", "\n");
			line = I2Utils.SplitLine(line, maxCharacters);
			line = RTLFixer.Fix(line, showTashkeel: true, !ignoreNumbers);
			for (int i = 0; i < list.Count; i++)
			{
				int length = line.Length;
				for (int j = 0; j < length; j++)
				{
					if (line[j] == '@' && line[j + 1] == '@' && line[j + 2] >= num2 && line[j + 3] == '@' && line[j + 4] == '@')
					{
						int num3 = line[j + 2] - num2;
						num3 = ((num3 % 2 != 0) ? (num3 - 1) : (num3 + 1));
						if (num3 >= list.Count)
						{
							num3 = list.Count - 1;
						}
						line = line.Substring(0, j) + list[num3] + line.Substring(j + 5);
						break;
					}
				}
			}
			return line;
		}

		public static string FixRTL_IfNeeded(string text, int maxCharacters = 0, bool ignoreNumber = false)
		{
			if (IsRight2Left)
			{
				return ApplyRTLfix(text, maxCharacters, ignoreNumber);
			}
			return text;
		}

		public static bool IsRTL(string Code)
		{
			return Array.IndexOf(LanguagesRTL, Code) >= 0;
		}

		public static bool UpdateSources()
		{
			UnregisterDeletededSources();
			RegisterSourceInResources();
			RegisterSceneSources();
			return Sources.Count > 0;
		}

		private static void UnregisterDeletededSources()
		{
			for (int num = Sources.Count - 1; num >= 0; num--)
			{
				if (Sources[num] == null)
				{
					RemoveSource(Sources[num]);
				}
			}
		}

		private static void RegisterSceneSources()
		{
			LanguageSource[] array = (LanguageSource[])Resources.FindObjectsOfTypeAll(typeof(LanguageSource));
			foreach (LanguageSource languageSource in array)
			{
				if (!Sources.Contains(languageSource.mSource))
				{
					if (languageSource.mSource.owner == null)
					{
						languageSource.mSource.owner = languageSource;
					}
					AddSource(languageSource.mSource);
				}
			}
		}

		private static void RegisterSourceInResources()
		{
			string[] globalSources = GlobalSources;
			foreach (string name in globalSources)
			{
				LanguageSourceAsset asset = ResourceManager.pInstance.GetAsset<LanguageSourceAsset>(name);
				if ((bool)asset && !Sources.Contains(asset.mSource))
				{
					if (!asset.mSource.mIsGlobalSource)
					{
						asset.mSource.mIsGlobalSource = true;
					}
					asset.mSource.owner = asset;
					AddSource(asset.mSource);
				}
			}
		}

		private static bool AllowSyncFromGoogle(LanguageSourceData Source)
		{
			if (Callback_AllowSyncFromGoogle == null)
			{
				return true;
			}
			return Callback_AllowSyncFromGoogle(Source);
		}

		internal static void AddSource(LanguageSourceData Source)
		{
			if (Sources.Contains(Source))
			{
				return;
			}
			Sources.Add(Source);
			if (Source.HasGoogleSpreadsheet() && Source.GoogleUpdateFrequency != LanguageSourceData.eGoogleUpdateFrequency.Never && AllowSyncFromGoogle(Source))
			{
				Source.Import_Google_FromCache();
				bool justCheck = false;
				if (Source.GoogleUpdateDelay > 0f)
				{
					CoroutineManager.Start(Delayed_Import_Google(Source, Source.GoogleUpdateDelay, justCheck));
				}
				else
				{
					Source.Import_Google(ForceUpdate: false, justCheck);
				}
			}
			for (int i = 0; i < Source.mLanguages.Count(); i++)
			{
				Source.mLanguages[i].SetLoaded(loaded: true);
			}
			if (Source.mDictionary.Count == 0)
			{
				Source.UpdateDictionary(force: true);
			}
		}

		private static IEnumerator Delayed_Import_Google(LanguageSourceData source, float delay, bool justCheck)
		{
			yield return new WaitForSeconds(delay);
			source?.Import_Google(ForceUpdate: false, justCheck);
		}

		internal static void RemoveSource(LanguageSourceData Source)
		{
			Sources.Remove(Source);
		}

		public static bool IsGlobalSource(string SourceName)
		{
			return Array.IndexOf(GlobalSources, SourceName) >= 0;
		}

		public static LanguageSourceData GetSourceContaining(string term, bool fallbackToFirst = true)
		{
			if (!string.IsNullOrEmpty(term))
			{
				int i = 0;
				for (int count = Sources.Count; i < count; i++)
				{
					if (Sources[i].GetTermData(term) != null)
					{
						return Sources[i];
					}
				}
			}
			if (!fallbackToFirst || Sources.Count <= 0)
			{
				return null;
			}
			return Sources[0];
		}

		public static UnityEngine.Object FindAsset(string value)
		{
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				UnityEngine.Object @object = Sources[i].FindAsset(value);
				if ((bool)@object)
				{
					return @object;
				}
			}
			return null;
		}

		public static void ApplyDownloadedDataFromGoogle()
		{
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				Sources[i].ApplyDownloadedDataFromGoogle();
			}
		}

		public static string GetCurrentDeviceLanguage(bool force = false)
		{
			if (force || string.IsNullOrEmpty(mCurrentDeviceLanguage))
			{
				DetectDeviceLanguage();
			}
			return mCurrentDeviceLanguage;
		}

		private static void DetectDeviceLanguage()
		{
			mCurrentDeviceLanguage = Application.systemLanguage.ToString();
			if (mCurrentDeviceLanguage == "ChineseSimplified")
			{
				mCurrentDeviceLanguage = "Chinese (Simplified)";
			}
			if (mCurrentDeviceLanguage == "ChineseTraditional")
			{
				mCurrentDeviceLanguage = "Chinese (Traditional)";
			}
		}

		public static void RegisterTarget(ILocalizeTargetDescriptor desc)
		{
			if (mLocalizeTargets.FindIndex((ILocalizeTargetDescriptor x) => x.Name == desc.Name) != -1)
			{
				return;
			}
			for (int i = 0; i < mLocalizeTargets.Count; i++)
			{
				if (mLocalizeTargets[i].Priority > desc.Priority)
				{
					mLocalizeTargets.Insert(i, desc);
					return;
				}
			}
			mLocalizeTargets.Add(desc);
		}

		public static string GetTranslation(string Term, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null, bool allowLocalizedParameters = true)
		{
			string Translation = null;
			TryGetTranslation(Term, out Translation, FixForRTL, maxLineLengthForRTL, ignoreRTLnumbers, applyParameters, localParametersRoot, overrideLanguage, allowLocalizedParameters);
			return Translation;
		}

		public static string GetTermTranslation(string Term, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null, bool allowLocalizedParameters = true)
		{
			return GetTranslation(Term, FixForRTL, maxLineLengthForRTL, ignoreRTLnumbers, applyParameters, localParametersRoot, overrideLanguage, allowLocalizedParameters);
		}

		public static bool TryGetTranslation(string Term, out string Translation, bool FixForRTL = true, int maxLineLengthForRTL = 0, bool ignoreRTLnumbers = true, bool applyParameters = false, GameObject localParametersRoot = null, string overrideLanguage = null, bool allowLocalizedParameters = true)
		{
			Translation = null;
			if (string.IsNullOrEmpty(Term))
			{
				return false;
			}
			InitializeIfNeeded();
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				if (Sources[i].TryGetTranslation(Term, out Translation, overrideLanguage))
				{
					if (applyParameters)
					{
						ApplyLocalizationParams(ref Translation, localParametersRoot, allowLocalizedParameters);
					}
					if (IsRight2Left && FixForRTL)
					{
						Translation = ApplyRTLfix(Translation, maxLineLengthForRTL, ignoreRTLnumbers);
					}
					return true;
				}
			}
			return false;
		}

		public static T GetTranslatedObject<T>(string AssetName, Localize optionalLocComp = null) where T : UnityEngine.Object
		{
			if (optionalLocComp != null)
			{
				return optionalLocComp.FindTranslatedObject<T>(AssetName);
			}
			T val = FindAsset(AssetName) as T;
			if ((bool)val)
			{
				return val;
			}
			return ResourceManager.pInstance.GetAsset<T>(AssetName);
		}

		public static T GetTranslatedObjectByTermName<T>(string Term, Localize optionalLocComp = null) where T : UnityEngine.Object
		{
			return GetTranslatedObject<T>(GetTranslation(Term, FixForRTL: false));
		}

		public static string GetAppName(string languageCode)
		{
			if (!string.IsNullOrEmpty(languageCode))
			{
				for (int i = 0; i < Sources.Count; i++)
				{
					if (string.IsNullOrEmpty(Sources[i].mTerm_AppName))
					{
						continue;
					}
					int languageIndexFromCode = Sources[i].GetLanguageIndexFromCode(languageCode, exactMatch: false);
					if (languageIndexFromCode < 0)
					{
						continue;
					}
					TermData termData = Sources[i].GetTermData(Sources[i].mTerm_AppName);
					if (termData != null)
					{
						string translation = termData.GetTranslation(languageIndexFromCode);
						if (!string.IsNullOrEmpty(translation))
						{
							return translation;
						}
					}
				}
			}
			return Application.productName;
		}

		public static void LocalizeAll(bool Force = false)
		{
			LoadCurrentLanguage();
			if (!Application.isPlaying)
			{
				DoLocalizeAll(Force);
				return;
			}
			mLocalizeIsScheduledWithForcedValue |= Force;
			if (!mLocalizeIsScheduled)
			{
				CoroutineManager.Start(Coroutine_LocalizeAll());
			}
		}

		private static IEnumerator Coroutine_LocalizeAll()
		{
			mLocalizeIsScheduled = true;
			yield return null;
			mLocalizeIsScheduled = false;
			bool force = mLocalizeIsScheduledWithForcedValue;
			mLocalizeIsScheduledWithForcedValue = false;
			DoLocalizeAll(force);
		}

		private static void DoLocalizeAll(bool Force = false)
		{
			Localize[] array = (Localize[])Resources.FindObjectsOfTypeAll(typeof(Localize));
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				array[i].OnLocalize(Force);
			}
			if (LocalizationManager.OnLocalizeEvent != null)
			{
				LocalizationManager.OnLocalizeEvent();
			}
		}

		public static List<string> GetCategories()
		{
			List<string> list = new List<string>();
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				Sources[i].GetCategories(OnlyMainCategory: false, list);
			}
			return list;
		}

		public static List<string> GetTermsList(string Category = null)
		{
			if (Sources.Count == 0)
			{
				UpdateSources();
			}
			if (Sources.Count == 1)
			{
				return Sources[0].GetTermsList(Category);
			}
			HashSet<string> hashSet = new HashSet<string>();
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				hashSet.UnionWith(Sources[i].GetTermsList(Category));
			}
			return new List<string>(hashSet);
		}

		public static TermData GetTermData(string term)
		{
			InitializeIfNeeded();
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				TermData termData = Sources[i].GetTermData(term);
				if (termData != null)
				{
					return termData;
				}
			}
			return null;
		}

		public static TermData GetTermData(string term, out LanguageSourceData source)
		{
			InitializeIfNeeded();
			int i = 0;
			for (int count = Sources.Count; i < count; i++)
			{
				TermData termData = Sources[i].GetTermData(term);
				if (termData != null)
				{
					source = Sources[i];
					return termData;
				}
			}
			source = null;
			return null;
		}
	}
}
