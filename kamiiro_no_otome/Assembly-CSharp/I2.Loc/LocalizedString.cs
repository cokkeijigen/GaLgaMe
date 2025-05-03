using System;

namespace I2.Loc
{
	[Serializable]
	public struct LocalizedString
	{
		public string mTerm;

		public bool mRTL_IgnoreArabicFix;

		public int mRTL_MaxLineLength;

		public bool mRTL_ConvertNumbers;

		public bool m_DontLocalizeParameters;

		public static implicit operator string(LocalizedString s)
		{
			return s.ToString();
		}

		public static implicit operator LocalizedString(string term)
		{
			LocalizedString result = default(LocalizedString);
			result.mTerm = term;
			return result;
		}

		public LocalizedString(LocalizedString str)
		{
			mTerm = str.mTerm;
			mRTL_IgnoreArabicFix = str.mRTL_IgnoreArabicFix;
			mRTL_MaxLineLength = str.mRTL_MaxLineLength;
			mRTL_ConvertNumbers = str.mRTL_ConvertNumbers;
			m_DontLocalizeParameters = str.m_DontLocalizeParameters;
		}

		public override string ToString()
		{
			string translation = LocalizationManager.GetTranslation(mTerm, !mRTL_IgnoreArabicFix, mRTL_MaxLineLength, !mRTL_ConvertNumbers, applyParameters: true);
			LocalizationManager.ApplyLocalizationParams(ref translation, !m_DontLocalizeParameters);
			return translation;
		}
	}
}
