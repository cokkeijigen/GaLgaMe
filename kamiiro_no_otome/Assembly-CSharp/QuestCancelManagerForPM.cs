using Arbor;
using UnityEngine;

public class QuestCancelManagerForPM : MonoBehaviour
{
	public bool CheckOpenDungeonScene()
	{
		bool result = false;
		if (PlayerDataManager.mapPlaceStatusNum == 3)
		{
			result = true;
		}
		return result;
	}

	public void PreCloseQuestUI()
	{
		HeaderStatusManager component = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		TotalMapAccessManager component2 = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		WorldMapAccessManager component3 = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		InDoorTalkManager component4 = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
			component2.totalMapFSM.SendTrigger("RefreshWorldMap");
			component.questFSM.SendTrigger("RefreshEnableQuestList");
			component3.worldCanvasGroup.interactable = true;
			component3.worldCanvasGroup.blocksRaycasts = true;
			PlayerDataManager.worldMapInputBlock = false;
			PlayerNonSaveDataManager.isRefreshWorldMap = true;
			component.SetVisibleMapExitButton(value: true);
			break;
		case 1:
			PlayerNonSaveDataManager.isRefreshLocalMap = true;
			component2.totalMapFSM.SendTrigger("AfterHeroineUnFollow");
			component.questFSM.SendTrigger("RefreshEnableQuestList");
			component3.worldCanvasGroup.interactable = true;
			component3.worldCanvasGroup.blocksRaycasts = true;
			PlayerDataManager.worldMapInputBlock = false;
			component.SetVisibleMapExitButton(value: true);
			break;
		case 2:
			component.questFSM.SendTrigger("RefreshEnableQuestList");
			component4.SetExitButtonFsmEnable(value: true);
			component4.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
			component.SetVisibleMapExitButton(value: true);
			break;
		case 3:
			break;
		}
	}
}
