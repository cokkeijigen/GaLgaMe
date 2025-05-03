using UnityEngine;

public class HelpCancelManagerForPM : MonoBehaviour
{
	public void PreCloseHelpUI()
	{
		GameObject gameObject = GameObject.Find("LocalMap Access Manager");
		if (gameObject != null)
		{
			switch (PlayerDataManager.mapPlaceStatusNum)
			{
			case 0:
				if (PlayerNonSaveDataManager.isSendHelpMarkButton)
				{
					CanvasGroup component2 = GameObject.Find("World Canvas").GetComponent<CanvasGroup>();
					component2.interactable = true;
					component2.blocksRaycasts = true;
					PlayerDataManager.worldMapInputBlock = false;
				}
				else
				{
					PlayerNonSaveDataManager.isMapMenuRightClickDisable = false;
				}
				break;
			case 1:
			{
				LocalMapAccessManager component = gameObject.GetComponent<LocalMapAccessManager>();
				if (PlayerNonSaveDataManager.isSendHelpMarkButton)
				{
					component.SetLocalMapExitEnable(isEnable: true);
				}
				else
				{
					PlayerNonSaveDataManager.isMapMenuRightClickDisable = false;
				}
				component.localMapExitFSM.gameObject.SetActive(value: true);
				break;
			}
			case 2:
				GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().exitButtonCanvasGroup.gameObject.SetActive(value: true);
				break;
			}
		}
		else if (GameObject.Find("TotalMap Access Manager") != null)
		{
			PlayerNonSaveDataManager.isBattleMenuRightClickDisable = false;
		}
		else
		{
			PlayerNonSaveDataManager.isBattleMenuRightClickDisable = false;
		}
		PlayerNonSaveDataManager.isInDoorExitLock = false;
		PlayerNonSaveDataManager.isSendHelpMarkButton = false;
	}
}
