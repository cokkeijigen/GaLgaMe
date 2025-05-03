using UnityEngine;

namespace I2.Loc
{
	public class Example_LocalizedString : MonoBehaviour
	{
		public LocalizedString _MyLocalizedString;

		public string _NormalString;

		[TermsPopup("")]
		public string _StringWithTermPopup;

		public void Start()
		{
			Debug.Log(_MyLocalizedString);
			Debug.Log(LocalizationManager.GetTranslation(_NormalString));
			Debug.Log(LocalizationManager.GetTranslation(_StringWithTermPopup));
			Debug.Log((string)(LocalizedString)"Term2");
			Debug.Log(_MyLocalizedString);
			Debug.Log((LocalizedString)"Term3");
			LocalizedString localizedString = "Term3";
			localizedString.mRTL_IgnoreArabicFix = true;
			Debug.Log(localizedString);
			LocalizedString localizedString2 = "Term3";
			localizedString2.mRTL_ConvertNumbers = true;
			localizedString2.mRTL_MaxLineLength = 20;
			Debug.Log(localizedString2);
			Debug.Log(localizedString2);
		}
	}
}
