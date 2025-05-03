using Arbor;
using UnityEngine;

public class ScenarioBattleVoiceManager : MonoBehaviour
{
	public ArborFSM arborFSM;

	public GameObject battleDialogCanvasGo;

	public void PushDialogOkButton()
	{
		PlayerFlagDataManager.tutorialFlagDictionary["lucyVoiceSelect"] = true;
		battleDialogCanvasGo.SetActive(value: false);
		arborFSM.SendTrigger("EndVoiceSelect");
	}

	public void PushLucyVoiceSelectToggle(bool isSexyType)
	{
		if (isSexyType)
		{
			PlayerOptionsDataManager.isLucyVoiceTypeSoft = false;
			PlayerOptionsDataManager.isLucyVoiceTypeSexy = true;
		}
		else
		{
			PlayerOptionsDataManager.isLucyVoiceTypeSoft = true;
			PlayerOptionsDataManager.isLucyVoiceTypeSexy = false;
		}
	}
}
