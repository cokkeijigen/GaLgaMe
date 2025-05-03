using UnityEngine;

namespace I2.Loc
{
	public class CallbackNotification : MonoBehaviour
	{
		public void OnModifyLocalization()
		{
			if (!string.IsNullOrEmpty(Localize.MainTranslation))
			{
				string translation = LocalizationManager.GetTranslation("Color/Red");
				Localize.MainTranslation = Localize.MainTranslation.Replace("{PLAYER_COLOR}", translation);
			}
		}
	}
}
