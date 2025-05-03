using Arbor;
using I2.Loc;
using UnityEngine;
using Utage;

public class DungeonDialogManager : MonoBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public GameObject dungeonDialogCanvasGo;

	public GameObject dungeonDialogWindowGo;

	public GameObject dungeonDialogTextGroup;

	public GameObject dungeonMenstrualDialogTextGroup;

	public GameObject dungeonNoRetryDialogTextGroup;

	public GameObject dungeonShrine1DeepBossDialogTextGroup;

	public Localize[] dungeonDialogTextLocArray;

	public Localize dungeonMenstrualNameDialogLocText;

	public Localize dungeonShrine1DeepBossDialogTextLoc;

	public GameObject normalButtonGroup;

	public GameObject menstrualDayButtonGroup;

	public GameObject noRetryButtonGroup;

	private void Awake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public void PushDungeonDialogOkButton()
	{
		CloseDungeonDialog();
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "dungeonSurvey":
		{
			DungeonMapData dungeonMapData2 = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
			int currentBorderNum2 = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().currentBorderNum;
			if (PlayerDataManager.currentTimeZone < 2)
			{
				PlayerNonSaveDataManager.sexBattleBgSprite = dungeonMapData2.dungeonBgList[currentBorderNum2];
			}
			else
			{
				PlayerNonSaveDataManager.sexBattleBgSprite = dungeonMapData2.dungeonNightBgList[currentBorderNum2];
			}
			GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Param.SetParameter("dungeonHeroineNum", PlayerDataManager.DungeonHeroineFollowNum);
			PlayerNonSaveDataManager.selectSexBattleHeroineId = PlayerDataManager.DungeonHeroineFollowNum;
			PlayerNonSaveDataManager.startSexSceneTypeName = "sexTouch";
			PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentDungeonName + "_Survey";
			GameObject.Find("Dungeon Map Manager").GetComponent<ArborFSM>().SendTrigger("StartDungeonSurvey");
			break;
		}
		case "dungeonSexBattle":
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
		case "dungeonSexEvent":
			PlayerNonSaveDataManager.selectScenarioName = PlayerNonSaveDataManager.dungeonEventScenarioName;
			dungeonMapManager.mapFSM.SendTrigger("StartDungeonSexEvent");
			break;
		}
	}

	public void CloseDungeonDialog()
	{
		dungeonDialogCanvasGo.SetActive(value: false);
		dungeonDialogWindowGo.SetActive(value: false);
	}

	public bool CheckDungeonSexEvent()
	{
		return dungeonMapManager.isSexLibidoEventEnable;
	}

	public bool CheckHeroineMenstrualDay()
	{
		bool result = false;
		PlayerSexStatusDataManager.CheckSexHeroineMenstrualDay();
		if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
		{
			result = true;
		}
		else
		{
			PlayerNonSaveDataManager.isMenstrualDaySexUseCondom = false;
		}
		return result;
	}

	public bool CheckHeroineUnLockFertilizationFlag()
	{
		CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerDataManager.DungeonHeroineFollowNum);
		return PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterFertilizationFlag];
	}

	public void OpenDungeonMenstrualDayDialog()
	{
		dungeonDialogTextGroup.SetActive(value: false);
		dungeonMenstrualDialogTextGroup.SetActive(value: true);
		dungeonNoRetryDialogTextGroup.SetActive(value: false);
		dungeonShrine1DeepBossDialogTextGroup.SetActive(value: false);
		dungeonMenstrualNameDialogLocText.Term = "character" + PlayerDataManager.DungeonHeroineFollowNum;
		normalButtonGroup.SetActive(value: false);
		menstrualDayButtonGroup.SetActive(value: true);
		noRetryButtonGroup.SetActive(value: false);
	}

	public void PushDungeonMenstrualDialogOkButton(bool value)
	{
		PlayerNonSaveDataManager.isMenstrualDaySexUseCondom = value;
		PushDungeonDialogOkButton();
	}

	public bool GetBattleDialogCanvasIsActive()
	{
		bool result = false;
		if (GameObject.Find("Battle Dialog Canvas") != null)
		{
			result = true;
		}
		Debug.Log("撤退ダイアログの表示：" + result);
		return result;
	}
}
