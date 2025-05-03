using Arbor;
using I2.Loc;
using UnityEngine;
using Utage;

public class DialogManager : MonoBehaviour
{
	public GameObject dialogWindowGo;

	public Localize dialogTextLoc;

	public void OpenDialog()
	{
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "saveLoad":
			if (PlayerNonSaveDataManager.selectSystemTabName == "save")
			{
				dialogTextLoc.Term = "alertSaveDialog";
			}
			else if (PlayerNonSaveDataManager.selectSystemTabName == "load")
			{
				dialogTextLoc.Term = "alertLoadDialog";
			}
			break;
		case "quit":
			dialogTextLoc.Term = "alertQuitDialog";
			break;
		case "survey":
			dialogTextLoc.Term = "alertSurveyStart";
			break;
		case "surveyEnd":
			if (GameObject.Find("Sex Battle Group") == null)
			{
				dialogTextLoc.Term = "alertSurveyEnd";
			}
			else
			{
				dialogTextLoc.Term = "alertSexBattleEnd";
			}
			break;
		}
	}

	public void PushDialogOkButton()
	{
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "saveLoad":
		{
			ArborFSM component = GameObject.Find("SaveLoad Manager").GetComponent<ArborFSM>();
			if (PlayerNonSaveDataManager.selectSystemTabName == "save")
			{
				component.SendTrigger("ExecuteSave");
			}
			else if (PlayerNonSaveDataManager.selectSystemTabName == "load")
			{
				component.SendTrigger("ExecuteLoad");
			}
			break;
		}
		case "quit":
			Application.Quit();
			break;
		case "survey":
			switch (PlayerDataManager.mapPlaceStatusNum)
			{
			case 3:
			{
				DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
				int currentBorderNum = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().currentBorderNum;
				if (PlayerDataManager.currentTimeZone < 2)
				{
					PlayerNonSaveDataManager.sexBattleBgSprite = dungeonMapData.dungeonBgList[currentBorderNum];
				}
				else
				{
					PlayerNonSaveDataManager.sexBattleBgSprite = dungeonMapData.dungeonNightBgList[currentBorderNum];
				}
				GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Param.SetParameter("dungeonHeroineNum", PlayerDataManager.DungeonHeroineFollowNum);
				PlayerNonSaveDataManager.selectSexBattleHeroineId = PlayerDataManager.DungeonHeroineFollowNum;
				PlayerNonSaveDataManager.startSexSceneTypeName = "sexBattle";
				PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentDungeonName + "_Survey";
				GameObject.Find("Dungeon Map Manager").GetComponent<ArborFSM>().SendTrigger("StartDungeonSurvey");
				break;
			}
			case 1:
			case 2:
				PlayerNonSaveDataManager.sexBattleBgSprite = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>().localMapUnlockDataBase.localMapUnlockDataList.Find((LocalMapUnlockData data) => data.currentPlaceName == PlayerDataManager.currentPlaceName).inDoorSurveyBgSpriteArray[PlayerDataManager.currentTimeZone];
				PlayerNonSaveDataManager.startSexSceneTypeName = "wareChange";
				if (PlayerFlagDataManager.heroineFirstSexTouchFlagList[PlayerDataManager.DungeonHeroineFollowNum])
				{
					GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>().StartInDoorSurveyFromDialog();
				}
				else
				{
					GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>().surveyCommandButtonGo.GetComponent<ArborFSM>().SendTrigger("StartSurvey");
				}
				break;
			}
			break;
		case "surveyEnd":
			GameObject.Find("Sex Touch Manager").GetComponent<ArborFSM>().SendTrigger("BackToBeforeSurvay");
			break;
		}
	}

	public void SendInDoorExitButtonEnable(bool value)
	{
		InDoorTalkManager component = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		component.SetExitButtonFsmEnable(value);
		component.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
	}

	public void PutBackUiScreenLevel()
	{
		PlayerNonSaveDataManager.openUiScreenLevel = PlayerNonSaveDataManager.beforeUiScreenLevel;
	}
}
