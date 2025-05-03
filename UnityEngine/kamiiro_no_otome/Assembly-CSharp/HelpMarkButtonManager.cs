using UnityEngine;

public class HelpMarkButtonManager : MonoBehaviour
{
	public void SetHelpMarkButtonIndex()
	{
		PlayerNonSaveDataManager.isSendHelpMarkButton = true;
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
			PlayerNonSaveDataManager.sendHelpMarkButtonIndex = 5;
			break;
		case 1:
			PlayerNonSaveDataManager.sendHelpMarkButtonIndex = 5;
			GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>().localMapExitFSM.gameObject.SetActive(value: false);
			break;
		case 2:
			if (PlayerDataManager.currentPlaceName == "Carriage")
			{
				PlayerNonSaveDataManager.sendHelpMarkButtonIndex = 0;
			}
			else
			{
				PlayerNonSaveDataManager.sendHelpMarkButtonIndex = 5;
			}
			GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().exitButtonCanvasGroup.gameObject.SetActive(value: false);
			break;
		}
	}
}
