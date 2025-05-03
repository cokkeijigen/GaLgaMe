using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/Source")]
	[ExecuteInEditMode]
	public class LanguageSource : MonoBehaviour, ISerializationCallbackReceiver, ILanguageSource
	{
		public delegate void fnOnSourceUpdated(LanguageSourceData source, bool ReceivedNewData, string errorMsg);

		public LanguageSourceData mSource = new LanguageSourceData();

		public int version;

		public bool NeverDestroy;

		public bool UserAgreesToHaveItOnTheScene;

		public bool UserAgreesToHaveItInsideThePluginsFolder;

		public bool GoogleLiveSyncIsUptoDate = true;

		public List<Object> Assets = new List<Object>();

		public string Google_WebServiceURL;

		public string Google_SpreadsheetKey;

		public string Google_SpreadsheetName;

		public string Google_LastUpdatedVersion;

		public LanguageSourceData.eGoogleUpdateFrequency GoogleUpdateFrequency = LanguageSourceData.eGoogleUpdateFrequency.Weekly;

		public float GoogleUpdateDelay = 5f;

		public List<LanguageData> mLanguages = new List<LanguageData>();

		public bool IgnoreDeviceLanguage;

		public LanguageSourceData.eAllowUnloadLanguages _AllowUnloadingLanguages;

		public List<TermData> mTerms = new List<TermData>();

		public bool CaseInsensitiveTerms;

		public LanguageSourceData.MissingTranslationAction OnMissingTranslation = LanguageSourceData.MissingTranslationAction.Fallback;

		public string mTerm_AppName;

		public LanguageSourceData SourceData
		{
			get
			{
				return mSource;
			}
			set
			{
				mSource = value;
			}
		}

		public event fnOnSourceUpdated Event_OnSourceUpdateFromGoogle;

		private void Awake()
		{
			mSource.owner = this;
			mSource.Awake();
		}

		private void OnDestroy()
		{
			NeverDestroy = false;
			if (!NeverDestroy)
			{
				mSource.OnDestroy();
			}
		}

		public string GetSourceName()
		{
			string text = base.gameObject.name;
			Transform parent = base.transform.parent;
			while ((bool)parent)
			{
				text = parent.name + "_" + text;
				parent = parent.parent;
			}
			return text;
		}

		public void OnBeforeSerialize()
		{
			version = 1;
		}

		public void OnAfterDeserialize()
		{
			if (version != 0 && mSource != null)
			{
				return;
			}
			mSource = new LanguageSourceData();
			mSource.owner = this;
			mSource.UserAgreesToHaveItOnTheScene = UserAgreesToHaveItOnTheScene;
			mSource.UserAgreesToHaveItInsideThePluginsFolder = UserAgreesToHaveItInsideThePluginsFolder;
			mSource.IgnoreDeviceLanguage = IgnoreDeviceLanguage;
			mSource._AllowUnloadingLanguages = _AllowUnloadingLanguages;
			mSource.CaseInsensitiveTerms = CaseInsensitiveTerms;
			mSource.OnMissingTranslation = OnMissingTranslation;
			mSource.mTerm_AppName = mTerm_AppName;
			mSource.GoogleLiveSyncIsUptoDate = GoogleLiveSyncIsUptoDate;
			mSource.Google_WebServiceURL = Google_WebServiceURL;
			mSource.Google_SpreadsheetKey = Google_SpreadsheetKey;
			mSource.Google_SpreadsheetName = Google_SpreadsheetName;
			mSource.Google_LastUpdatedVersion = Google_LastUpdatedVersion;
			mSource.GoogleUpdateFrequency = GoogleUpdateFrequency;
			mSource.GoogleUpdateDelay = GoogleUpdateDelay;
			mSource.Event_OnSourceUpdateFromGoogle += this.Event_OnSourceUpdateFromGoogle;
			if (mLanguages != null && mLanguages.Count > 0)
			{
				mSource.mLanguages.Clear();
				mSource.mLanguages.AddRange(mLanguages);
				mLanguages.Clear();
			}
			if (Assets != null && Assets.Count > 0)
			{
				mSource.Assets.Clear();
				mSource.Assets.AddRange(Assets);
				Assets.Clear();
			}
			if (mTerms != null && mTerms.Count > 0)
			{
				mSource.mTerms.Clear();
				for (int i = 0; i < mTerms.Count; i++)
				{
					mSource.mTerms.Add(mTerms[i]);
				}
				mTerms.Clear();
			}
			version = 1;
			this.Event_OnSourceUpdateFromGoogle = null;
		}
	}
}
