using UnityEngine;

namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/SetLanguage Button")]
	public class SetLanguage : MonoBehaviour
	{
		public string _Language;

		private void OnClick()
		{
			ApplyLanguage();
		}

		public void ApplyLanguage()
		{
			if (LocalizationManager.HasLanguage(_Language))
			{
				LocalizationManager.CurrentLanguage = _Language;
			}
		}
	}
}
