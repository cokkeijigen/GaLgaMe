using Arbor;
using I2.Loc;
using UnityEngine;

public class BattleDialogManager : MonoBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private DungeonBattleManager dungeonBattleManager;

	public GameObject dialogWindowGo;

	public Localize dialogTextLoc;

	private ArborFSM dialogManagerFSM;

	private void Awake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dialogManagerFSM = GameObject.Find("Dialog Manager").GetComponent<ArborFSM>();
	}

	public void OpenBattleDialog()
	{
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "retreat":
			Time.timeScale = 0f;
			dialogTextLoc.Term = "alertRetreatDialog";
			break;
		case "dungeonMapRetreat":
			Time.timeScale = 0f;
			dialogTextLoc.Term = "alertRetreatDialog";
			if (PlayerDataManager.isDungeonMapAuto)
			{
				PlayerDataManager.isDungeonMapAuto = false;
				DungeonMapManager component = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
				component.autoAlertGroup.SetActive(value: false);
				component.autoButtonLoc.Term = "buttonDungeonMapAutoStart";
			}
			break;
		case "dungeonBattleRetreat":
			dialogTextLoc.Term = "alertRetreatDialog";
			break;
		case "dungeonMapAuto":
			Time.timeScale = 0f;
			if (PlayerDataManager.isDungeonMapAuto)
			{
				dialogTextLoc.Term = "alertDungeonAutoBeforeStop";
			}
			else
			{
				dialogTextLoc.Term = "alertDungeonAutoBeforeStart";
			}
			break;
		}
	}

	public string PushBattleDialogOkButton()
	{
		string result = "";
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "retreat":
			GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>().isRetreat = true;
			GameObject.Find("Scenario Manager").GetComponent<ArborFSM>().SendTrigger("MoveMapSceneStart");
			result = "scenarioRetreat";
			break;
		case "dungeonMapRetreat":
		{
			DungeonMapManager component3 = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
			Time.timeScale = 1f;
			if (PlayerDataManager.isDungeonMapAuto)
			{
				PlayerDataManager.isDungeonMapAuto = false;
				component3.autoAlertGroup.SetActive(value: false);
				component3.autoButtonLoc.Term = "buttonDungeonMapAutoStart";
			}
			result = "dungeonMapRetreat";
			break;
		}
		case "dungeonBattleRetreat":
			result = "dungeonBattleRetreat";
			break;
		case "dungeonMapAuto":
		{
			DungeonMapManager component = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
			RectTransform component2 = dungeonMapManager.routeSelectGroupWindow.GetComponent<RectTransform>();
			Time.timeScale = 1f;
			if (PlayerDataManager.isDungeonMapAuto)
			{
				PlayerDataManager.isDungeonMapAuto = false;
				component.autoAlertGroup.SetActive(value: false);
				component.autoButtonLoc.Term = "buttonDungeonMapAutoStart";
				component2.sizeDelta = new Vector2(800f, 375f);
			}
			else
			{
				PlayerDataManager.isDungeonMapAuto = true;
				component.autoAlertGroup.SetActive(value: true);
				component.autoButtonLoc.Term = "buttonDungeonMapAutoStop";
				component2.sizeDelta = new Vector2(800f, 485f);
				GameObject.Find("Dungeon Map Manager").GetComponent<ArborFSM>().SendTrigger("DungeonAutoStart");
			}
			result = "dungeonMapAuto";
			break;
		}
		}
		return result;
	}

	public void SetDungeonBattleAgilityCoroutine(bool isStop)
	{
		if (isStop)
		{
			dungeonBattleManager.StopAglityCoroutine();
		}
		else
		{
			dungeonBattleManager.RestartAglityCoroutine();
		}
	}

	public void CalcDungeonMapRetreat()
	{
		dungeonBattleManager.messageWindowGO.SetActive(value: true);
		dungeonBattleManager.messageTextArray[0].SetActive(value: true);
		dungeonBattleManager.messageWindowLoc[1].Term = "dungeonMapRetreat";
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			dungeonBattleManager.messageWindowLoc[0].Term = "battleTextPlayerAllTarget";
		}
		else
		{
			dungeonBattleManager.messageWindowLoc[0].Term = "character0";
		}
		PlayerStatusDataManager.CalcAllHpToCharacterHp();
		PlayerNonSaveDataManager.isDungeonScnearioBattle = false;
		CallDungeonGetItem();
		PlayerDataManager.mapPlaceStatusNum = 0;
		GameObject.Find("Scenario Manager").GetComponent<ArborFSM>().SendTrigger("MoveMapSceneStart");
	}

	public float CalcDungeonBattleRetreat()
	{
		dungeonBattleManager.messageWindowGO.SetActive(value: true);
		dungeonBattleManager.messageTextArray[0].SetActive(value: true);
		dungeonBattleManager.messageWindowLoc[1].Term = "dungeonRetreatStart";
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			dungeonBattleManager.messageWindowLoc[0].Term = "battleTextPlayerAllTarget";
		}
		else
		{
			dungeonBattleManager.messageWindowLoc[0].Term = "character0";
		}
		return 0.6f / (float)PlayerDataManager.dungeonBattleSpeed;
	}

	public bool CheckDungeonBattleRetreat()
	{
		bool flag = false;
		int num = PlayerDataManager.retreatProbability + dungeonMapStatusManager.dungeonBuffRetreat;
		int num2 = Random.Range(0, 100);
		Debug.Log("脱出確率：" + num + "／ランダム値：" + num2);
		if (num2 < num)
		{
			flag = true;
			GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>().isRetreat = true;
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public float OpenBattleRetreatMessage(bool isRetreat)
	{
		if (isRetreat)
		{
			dungeonBattleManager.messageTextArray[0].SetActive(value: false);
			dungeonBattleManager.messageWindowLoc[1].Term = "dungeonRetreatSuccess";
		}
		else
		{
			dungeonBattleManager.messageTextArray[0].SetActive(value: false);
			dungeonBattleManager.messageWindowLoc[1].Term = "dungeonRetreatFailed";
		}
		return 0.8f / (float)PlayerDataManager.dungeonBattleSpeed;
	}

	public void CallDungeonGetItem()
	{
		dungeonMapManager.GetItemToHaveData();
	}

	public int GetBattleConsecutiveRoundNum()
	{
		return dungeonMapManager.battleConsecutiveRoundNum;
	}

	public bool CheckSelectIsDungeon()
	{
		return PlayerDataManager.isSelectDungeon;
	}

	public int GetDungeonCurrentFloorNum()
	{
		return dungeonMapManager.dungeonCurrentFloorNum;
	}

	public bool CheckIsDungeonScenarioBattle()
	{
		return PlayerNonSaveDataManager.isDungeonScnearioBattle;
	}

	public bool CheckIsDungeonBossBattle()
	{
		return PlayerNonSaveDataManager.isDungeonBossBattle;
	}

	public bool CheckIsMoveToDungeonBattle()
	{
		return PlayerNonSaveDataManager.isMoveToDungeonBattle;
	}

	public bool CheckIsRetreatFromBattle()
	{
		return PlayerNonSaveDataManager.isRetreatFromBattle;
	}

	public bool CheckIsPrologueScenarioBattle()
	{
		bool result = false;
		if (PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-1" || PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-2")
		{
			result = true;
		}
		return result;
	}

	public void GoToWorldMapFromDungeon()
	{
		PlayerDataManager.mapPlaceStatusNum = 0;
		PlayerStatusDataManager.CalcAllHpToCharacterHp();
		PlayerNonSaveDataManager.isDungeonScnearioBattle = false;
		PlayerNonSaveDataManager.isMoveToDungeonBattle = false;
		GameObject.Find("Scenario Manager").GetComponent<ArborFSM>().SendTrigger("MoveMapSceneStart");
	}

	public string GetBattleBeforePoint()
	{
		return PlayerNonSaveDataManager.battleBeforePointType;
	}
}
