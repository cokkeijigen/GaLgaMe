using System;
using System.Collections.Generic;
using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using Utage;

public class UtageAddSceneManager : MonoBehaviour
{
	public AdvEngine advEngine;

	public AdvUiManager advUiManager;

	public bool isTalkCancelAccept;

	public bool isTalkCancelClick;

	public string[] talkCancelJumpLabelArray = new string[2];

	private ArborFSM arborFSM;

	private ArborFSM scenarioBattleFSM;

	private ArborFSM dungeonMapFSM;

	private ArborFSM dungeonBattleFSM;

	public GameObject scenarioAlertDialog;

	public Localize scenarioAlertTextLoc;

	public GameObject scenarioSelectionDialog;

	public Localize[] scenarioSelectionTextLocArray;

	public GameObject backToBeforeDialog;

	public Localize backToBeforeTextLoc;

	private bool isStatusSetUp;

	private void Awake()
	{
		arborFSM = GetComponent<ArborFSM>();
	}

	public void GetArborComponent()
	{
		scenarioBattleFSM = GameObject.Find("Scenario Battle Manager").GetComponent<ArborFSM>();
		dungeonMapFSM = GameObject.Find("Dungeon Map Manager").GetComponent<ArborFSM>();
		dungeonBattleFSM = GameObject.Find("Dungeon Battle Manager").GetComponent<ArborFSM>();
	}

	public void RestartUtageText()
	{
		advUiManager.enabled = true;
	}

	public void ResetGameTime(AdvCommandSendMessageByName command)
	{
		PlayerDataManager.currentTimeZone = 0;
		PlayerDataManager.totalTimeZoneCount = 4;
		PlayerDataManager.currentMonthDay = 1;
		PlayerDataManager.currentTotalDay = 1;
	}

	public void AddScenarioMovePointTime(AdvCommandSendMessageByName command)
	{
		int addTimeZoneNum = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = false;
		PlayerNonSaveDataManager.isUsedShop = false;
		PlayerNonSaveDataManager.isAddTimeFromDungeon = false;
		PlayerNonSaveDataManager.isAddTimeFromScenario = true;
		PlayerNonSaveDataManager.addTimeZoneNum = addTimeZoneNum;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
	}

	public void SetCurrentTimeZone(AdvCommandSendMessageByName command)
	{
		int num = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		int num2 = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		bool flag = command.ParseCellOptional(AdvColumnName.Arg5, defaultVal: false);
		bool flag2 = command.ParseCellOptional(AdvColumnName.Arg6, defaultVal: true);
		int num3 = 0;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = false;
		PlayerNonSaveDataManager.isUsedShop = false;
		if (num == 0)
		{
			num3 = 4 - PlayerDataManager.currentTimeZone;
			num3 += num2 * 4;
			if (PlayerDataManager.currentTimeZone == 0 && !flag2)
			{
				num3 = 0;
			}
		}
		else
		{
			if (num >= PlayerDataManager.currentTimeZone)
			{
				num3 = ((num > PlayerDataManager.currentTimeZone) ? (num - PlayerDataManager.currentTimeZone) : (flag2 ? 4 : 0));
			}
			else
			{
				int num4 = 4 - PlayerDataManager.currentTimeZone;
				num3 = num + num4;
			}
			num3 += num2 * 4;
		}
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
		PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
		PlayerNonSaveDataManager.addTimeZoneNum = num3;
		Debug.Log("宴から時間を設定／現在時刻：" + PlayerDataManager.currentTimeZone + "／設定時刻：" + num + "／追加時間：" + num3 + "／追加日数：" + num2);
		PlayerNonSaveDataManager.isAddTimeFromDungeon = false;
		PlayerNonSaveDataManager.isAddTimeFromScenario = true;
		if (flag)
		{
			GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		}
	}

	public void AllHealPartyHpAndMp(AdvCommandSendMessageByName command)
	{
		PlayerStatusDataManager.characterHp[0] = PlayerStatusDataManager.characterMaxHp[0];
		PlayerStatusDataManager.characterHp[0] = PlayerStatusDataManager.characterMaxHp[0];
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			int dungeonHeroineFollowNum = PlayerDataManager.DungeonHeroineFollowNum;
			PlayerStatusDataManager.characterHp[dungeonHeroineFollowNum] = PlayerStatusDataManager.characterMaxHp[dungeonHeroineFollowNum];
			PlayerStatusDataManager.characterHp[dungeonHeroineFollowNum] = PlayerStatusDataManager.characterMaxHp[dungeonHeroineFollowNum];
		}
	}

	public void SetDungeonEnterTime(AdvCommandSendMessageByName command)
	{
		int num = (PlayerDataManager.dungeonEnterTimeZoneNum = command.ParseCellOptional(AdvColumnName.Arg3, 0));
		Debug.Log("ダンジョンに入った時間を設定：" + num);
	}

	public void SetAfterScenarioName(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		string battleBeforePointType = command.ParseCellOptional(AdvColumnName.Arg4, "");
		bool isRestoreDungeonBattleFailed = command.ParseCellOptional(AdvColumnName.Arg5, defaultVal: false);
		string text2 = text.Substring(0, 6);
		Debug.Log("ジャンプするシナリオ名の前半部分：" + text2);
		if (text2 == "H_Lucy" && PlayerOptionsDataManager.isLucyVoiceTypeSexy)
		{
			PlayerNonSaveDataManager.victoryScenarioName = text + "after_sexy";
			PlayerNonSaveDataManager.rematchScenarioName = text + "battle_sexy";
			PlayerNonSaveDataManager.resultScenarioName = text;
		}
		else
		{
			PlayerNonSaveDataManager.victoryScenarioName = text + "after";
			PlayerNonSaveDataManager.rematchScenarioName = text + "battle";
			PlayerNonSaveDataManager.resultScenarioName = text;
		}
		PlayerNonSaveDataManager.battleBeforePointType = battleBeforePointType;
		PlayerNonSaveDataManager.isRestoreDungeonBattleFailed = isRestoreDungeonBattleFailed;
	}

	public void SetDungeonBattleAfterScenarioName(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		string resultScenarioName = command.ParseCellOptional(AdvColumnName.Arg4, "");
		bool isRestoreDungeonBattleFailed = command.ParseCellOptional(AdvColumnName.Arg5, defaultVal: false);
		PlayerNonSaveDataManager.victoryScenarioName = text + "_after";
		PlayerNonSaveDataManager.rematchScenarioName = text;
		PlayerNonSaveDataManager.resultScenarioName = resultScenarioName;
		PlayerNonSaveDataManager.battleBeforePointType = "Dungeon";
		PlayerNonSaveDataManager.isRestoreDungeonBattleFailed = isRestoreDungeonBattleFailed;
	}

	public void ScenarioBattlePreStart(AdvCommandSendMessageByName command)
	{
		PlayerDataManager.currentAccessPointName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		if (!PlayerNonSaveDataManager.isUtagePlayBattleBgm)
		{
			PlayerDataManager.playBgmCategoryName = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName).battleBgmName;
			GameObject.Find("Bgm Play Manager").GetComponent<PlayMakerFSM>().SendEvent("ChangeMasterAudioPlaylist");
			Debug.Log("戦闘BGMを開始");
		}
		else
		{
			Debug.Log("宴からBGMを開始している");
		}
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		scenarioBattleFSM.SendTrigger("ScenarioBattlePreStart");
	}

	public void DungeonMapPreProcess(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerDataManager.mapPlaceStatusNum = 3;
		PlayerDataManager.currentAccessPointName = text;
		PlayerDataManager.currentPlaceName = text;
		PlayerDataManager.currentDungeonName = text;
	}

	public void DungeonMapPreStart(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		dungeonMapFSM.SendTrigger("DungeonMapPreStart");
	}

	public void SetDeepDungeonFlagNum(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		int value = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		PlayerFlagDataManager.deepDungeonFlagDictionary[text] = value;
		Debug.Log("深く潜れるダンジョンのクリア階数を設定する：" + text);
	}

	public void CheckDeepDungeonFlagNum(AdvCommandSendMessageByName command)
	{
		string key = command.ParseCellOptional(AdvColumnName.Arg3, "");
		int value = PlayerFlagDataManager.deepDungeonFlagDictionary[key];
		GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Param.SetParameter("deepFloorClearNum", value);
	}

	public void SetDungeonCurrentFloorNum(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.dungeonSetStartFloorNum = command.ParseCellOptional(AdvColumnName.Arg3, 1);
	}

	public void StartDungeonFadeOut(AdvCommandSendMessageByName command)
	{
		float duration = command.ParseCellOptional(AdvColumnName.Arg3, 0f);
		CanvasGroup canvasGroup = GameObject.Find("Transition Canvas").GetComponent<CanvasGroup>();
		Debug.Log("ダンジョンイベントの開始時、フェードアウトする");
		canvasGroup.DOFade(0f, duration).OnComplete(delegate
		{
			canvasGroup.blocksRaycasts = false;
		});
	}

	public void RestartDungeonMap(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		dungeonMapFSM.SendTrigger("RestartDungeonMap");
	}

	public void DungeonBattlePreStart(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.battleBeforePointType = "Dungeon";
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		dungeonBattleFSM.SendTrigger("DungeonBattlePreStart");
	}

	public void ScenarioToDungeonBattlePreStart(AdvCommandSendMessageByName command)
	{
		string currentDungeonName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		int num = command.ParseCellOptional(AdvColumnName.Arg4, 1);
		PlayerDataManager.currentDungeonName = currentDungeonName;
		GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().dungeonCurrentFloorNum = num;
		if (num == 1)
		{
			PlayerNonSaveDataManager.battleBeforePointType = "WorldMap";
		}
		else
		{
			PlayerNonSaveDataManager.battleBeforePointType = "Dungeon";
		}
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		dungeonBattleFSM.SendTrigger("ScenarioToDungeonBattlePreStart");
	}

	public void SetMoveToDungeonInfo(AdvCommandSendMessageByName command)
	{
		string currentDungeonName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		int dungeonCurrentFloorNum = command.ParseCellOptional(AdvColumnName.Arg4, 1);
		PlayerDataManager.currentDungeonName = currentDungeonName;
		GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().dungeonCurrentFloorNum = dungeonCurrentFloorNum;
		PlayerNonSaveDataManager.isMoveToDungeonBattle = true;
	}

	public void SetRetreatFromBattle(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.isRetreatFromBattle = true;
	}

	public void SetHeroineDungeonFollow(AdvCommandSendMessageByName command)
	{
		int dungeonHeroineFollowNum = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		bool num = command.ParseCellOptional(AdvColumnName.Arg4, defaultVal: false);
		isStatusSetUp = command.ParseCellOptional(AdvColumnName.Arg5, defaultVal: true);
		PlayerDataManager.DungeonHeroineFollowNum = dungeonHeroineFollowNum;
		PlayerDataManager.isDungeonHeroineFollow = num;
		if (num)
		{
			PlayerStatusDataManager.playerPartyMember = new int[2]
			{
				0,
				PlayerDataManager.DungeonHeroineFollowNum
			};
		}
		else
		{
			PlayerStatusDataManager.playerPartyMember = new int[1];
		}
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
	}

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isStatusSetUp, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		Debug.Log("Equipデータの更新完了");
	}

	public void SetCharacterLevel(AdvCommandSendMessageByName command)
	{
		int num = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		int num2 = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		if (num == 2 && PlayerFlagDataManager.scenarioFlagDictionary["MH_Shia_012"])
		{
			int num3 = PlayerStatusDataManager.characterLv[0] + 3;
			num2 = Math.Max(num2, num3);
			Debug.Log($"キャラがリィナで、シア編をクリア済み／キャラNum：{num}／LV：{num2}／MaxLV：{num3}");
		}
		PlayerStatusDataManager.characterExp[num] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[num2 - 1];
		for (int i = 0; i < PlayerStatusDataManager.characterExp.Length; i++)
		{
			for (int j = 0; j < GameDataManager.instance.needExpDataBase.needCharacterLvExpList.Count; j++)
			{
				if (PlayerStatusDataManager.characterExp[i] >= GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j])
				{
					PlayerStatusDataManager.characterLv[i] = j + 1;
					continue;
				}
				int num4 = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j - 1];
				int num5 = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j];
				if (PlayerStatusDataManager.characterCurrentLvExp[i] < num4)
				{
					PlayerStatusDataManager.characterCurrentLvExp[i] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j - 1];
				}
				if (PlayerStatusDataManager.characterNextLvExp[i] < num5)
				{
					PlayerStatusDataManager.characterNextLvExp[i] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j];
				}
				break;
			}
		}
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
		Debug.Log("キャラNum：" + num + "／LV：" + num2);
	}

	public void SetPartyCharacterWeapon(AdvCommandSendMessageByName command)
	{
		int characterNum = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		int itemID = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		int num = command.ParseCellOptional(AdvColumnName.Arg5, 0);
		PlayerInventoryDataEquipAccess.UpgradePlayerHavePartyWeapon(itemID, num);
		PlayerEquipDataManager.SetEquipPlayerWeapon(num, 0, characterNum);
		Debug.Log("ID：" + characterNum + "／現在の武器：" + itemID + "／新しい武器：" + num);
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
	}

	public void SetPartyCharacterArmor(AdvCommandSendMessageByName command)
	{
		int characterNum = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		int itemID = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		int num = command.ParseCellOptional(AdvColumnName.Arg5, 0);
		Debug.Log("ID：" + characterNum + "／現在の防具：" + itemID + "／新しい防具：" + num);
		PlayerInventoryDataEquipAccess.UpgradePlayerHavePartyArmor(itemID, num);
		PlayerEquipDataManager.SetEquipPlayerArmor(num, 0, characterNum);
	}

	public void ScenarioCraftStart(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.selectCraftCanvasName = "newCraft";
		arborFSM.SendTrigger("ScenarioCraftStart");
		PlayerNonSaveDataManager.isTutorialCrafted = false;
		PlayerNonSaveDataManager.isRequiedUtageResume = true;
	}

	public void MoveWorldMapFromScenario(AdvCommandSendMessageByName command)
	{
		PlayerDataManager.currentAccessPointName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerDataManager.mapPlaceStatusNum = 0;
		arborFSM.SendTrigger("MoveMapSceneStart");
	}

	public void MoveLocalMapFromScenario(AdvCommandSendMessageByName command)
	{
		string currentAccessPointName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		string currentPlaceName = command.ParseCellOptional(AdvColumnName.Arg4, "");
		PlayerDataManager.currentAccessPointName = currentAccessPointName;
		PlayerDataManager.currentPlaceName = currentPlaceName;
		PlayerDataManager.mapPlaceStatusNum = 1;
		PlayerNonSaveDataManager.isUtageToLocalMap = true;
		arborFSM.SendTrigger("MoveMapSceneStart");
	}

	public void MoveInDoorFromScenario(AdvCommandSendMessageByName command)
	{
		string currentAccessPointName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		string currentPlaceName = command.ParseCellOptional(AdvColumnName.Arg4, "");
		PlayerDataManager.currentAccessPointName = currentAccessPointName;
		PlayerDataManager.currentPlaceName = currentPlaceName;
		PlayerDataManager.mapPlaceStatusNum = 2;
		PlayerNonSaveDataManager.isUtageToLocalMap = true;
		Debug.Log("宴からインドアに移動");
		arborFSM.SendTrigger("MoveMapSceneStart");
	}

	public void MoveWorldMapToInDoorFromScenario(AdvCommandSendMessageByName command)
	{
		string currentAccessPointName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		string currentPlaceName = command.ParseCellOptional(AdvColumnName.Arg4, "");
		PlayerDataManager.currentAccessPointName = currentAccessPointName;
		PlayerDataManager.currentPlaceName = currentPlaceName;
		PlayerDataManager.mapPlaceStatusNum = 2;
		PlayerNonSaveDataManager.isUtageToWorldMapInDoor = true;
		PlayerNonSaveDataManager.isWorldMapToInDoor = true;
		arborFSM.SendTrigger("MoveMapSceneStart");
	}

	public void MoveCarriageStoreFromScenario(AdvCommandSendMessageByName command)
	{
		if (PlayerDataManager.mapPlaceStatusNum != 0)
		{
			if (string.IsNullOrEmpty(PlayerDataManager.currentPlaceName))
			{
				PlayerNonSaveDataManager.isUtageToLocalMap = false;
			}
			else
			{
				PlayerNonSaveDataManager.isUtageToLocalMap = true;
			}
		}
		arborFSM.SendTrigger("MoveMapSceneStart");
	}

	public void SendGarellyEnd(AdvCommandSendMessageByName command)
	{
		if (PlayerNonSaveDataManager.isGarellyOpenWithTitle || PlayerNonSaveDataManager.isTitleDebugToUtage)
		{
			PlayerNonSaveDataManager.loadSceneName = "title";
			Debug.Log("タイトルに戻る");
		}
		else
		{
			PlayerNonSaveDataManager.loadSceneName = "main";
			Debug.Log("メインに戻る");
		}
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
	}

	public void OpenScenarioToInDoor(AdvCommandSendMessageByName command)
	{
		PlayerDataManager.currentAccessPointName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerDataManager.currentPlaceName = command.ParseCellOptional(AdvColumnName.Arg4, "");
		PlayerNonSaveDataManager.isRequiedUtageResume = true;
		GameObject.Find("InDoor Talk Manager").GetComponent<ArborFSM>().SendTrigger("OpenInDoorCanvas");
	}

	public void StartDungeonSurvey(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.loadSceneName = "sex";
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		PlayerSexStatusDataManager.CheckSexHeroineMenstrualDay();
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
	}

	public void StartDungeonSexBattle(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.loadSceneName = "sex";
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		PlayerSexStatusDataManager.CheckSexHeroineMenstrualDay();
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
	}

	public void EndDungeonSurvey(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		dungeonMapFSM.SendTrigger("DungeonSurveyEnd");
	}

	public void GetFertilizationEndBackLabel(AdvCommandSendMessageByName command)
	{
		string text = "";
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 2:
			text = "*" + PlayerDataManager.currentAccessPointName + "_SurveyFertilizeBackEnd";
			advEngine.Param.SetParameter("fertilizationEndBackLabel", text);
			break;
		case 3:
			text = "*" + PlayerDataManager.currentDungeonName + "_SurveyFertilizeBackEnd";
			advEngine.Param.SetParameter("fertilizationEndBackLabel", text);
			break;
		}
	}

	public void GetDungeonHeroineName(AdvCommandSendMessageByName command)
	{
		string value = "";
		switch (PlayerDataManager.DungeonHeroineFollowNum)
		{
		case 2:
			value = "リィナ";
			break;
		case 3:
			value = "シア";
			break;
		case 4:
			value = "レヴィ";
			break;
		}
		advEngine.Param.SetParameter("dungeonHeroineName", value);
	}

	public void GetDungeonHeroineNum(AdvCommandSendMessageByName command)
	{
		advEngine.Param.SetParameter("dungeonHeroineNum", PlayerDataManager.DungeonHeroineFollowNum);
	}

	public void SetIsDungeonScnenarioBattle(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.isDungeonScnearioBattle = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
	}

	public void GetCurrentTimeZoneNum(AdvCommandSendMessageByName command)
	{
		int value = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		if (PlayerNonSaveDataManager.isUtageToJumpFromGarelly)
		{
			advEngine.Param.SetParameter("currentTimeZoneNum", value);
		}
		else
		{
			advEngine.Param.SetParameter("currentTimeZoneNum", PlayerDataManager.currentTimeZone);
		}
	}

	public void GetCurrentPlaceName(AdvCommandSendMessageByName command)
	{
		string value = command.ParseCellOptional(AdvColumnName.Arg3, "");
		if (PlayerNonSaveDataManager.isUtageToJumpFromGarelly)
		{
			advEngine.Param.SetParameter("currentPlaceName", value);
		}
		else
		{
			advEngine.Param.SetParameter("currentPlaceName", PlayerDataManager.currentPlaceName);
		}
	}

	public void RefreshStoryQuest(AdvCommandSendMessageByName command)
	{
		PlayerQuestDataManager.RefreshEnableStoryQuestList();
		Debug.Log("宴からクエストリストを更新する");
	}

	public void RefreshGameClearQuest(AdvCommandSendMessageByName command)
	{
		string scenarioName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerQuestDataManager.RefreshEnableStoryQuestList();
		PlayerQuestDataManager.RefreshStoryQuestFlagData("gameClear", 0, scenarioName);
	}

	public void AddLearnedSkillFromUtage(AdvCommandSendMessageByName command)
	{
		string scenarioName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		List<BattleSkillData> list = (from data in GameDataManager.instance.playerSkillDataBase.skillDataList.Where((BattleSkillData data) => data.useCharacterID == 999).ToList()
			where data.learnScenarioName == scenarioName
			select data).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			PlayerInventoryDataEquipAccess.AddLearnedSkill(list[i].skillID);
			Debug.Log("習得スキルの追加：" + list[i].skillID);
		}
	}

	public void CheckInterruptedSave(AdvCommandSendMessageByName command)
	{
		string openDialogName = (PlayerNonSaveDataManager.utageInterruptedDialogType = command.ParseCellOptional(AdvColumnName.Arg3, ""));
		PlayerNonSaveDataManager.isInterruptedSave = false;
		PlayerNonSaveDataManager.isInterruptedAfterSave = false;
		PlayerNonSaveDataManager.openDialogName = openDialogName;
		GameObject.Find("Interrupted Dialog Manager").GetComponent<InterruptedDialogManager>().OpenInterruptedDialog();
	}

	public void OpenBackToBeforeDialog(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		if (!(text == "Fertilize"))
		{
			if (text == "GameClear")
			{
				PlayerNonSaveDataManager.backToBeforeScenarioLabelName = "BackToBeforeGameClear";
				backToBeforeTextLoc.Term = "alertBackToBeforeGameClear";
			}
		}
		else
		{
			PlayerNonSaveDataManager.backToBeforeScenarioLabelName = "FertilizeBackToNoConception";
			backToBeforeTextLoc.Term = "alertBackToNoConception";
		}
		backToBeforeDialog.SetActive(value: true);
	}

	public void CloseBackToBeforeDialog()
	{
		backToBeforeDialog.SetActive(value: false);
		advEngine.JumpScenario(PlayerNonSaveDataManager.backToBeforeScenarioLabelName);
	}

	public void OpenConceptionSelectionDialog(AdvCommandSendMessageByName command)
	{
		scenarioSelectionDialog.SetActive(value: true);
		scenarioSelectionTextLocArray[0].Term = "alertSelectConception1";
		scenarioSelectionTextLocArray[1].Term = "alertSelectConception2";
		scenarioSelectionTextLocArray[1].gameObject.SetActive(value: true);
	}

	public void CloseConceptionSelectionDialog(bool isClickOkButton)
	{
		scenarioSelectionDialog.SetActive(value: false);
		string text = "";
		if (isClickOkButton)
		{
			advEngine.JumpScenario("StartFertilizeEnd");
			return;
		}
		text = ((!(PlayerDataManager.currentAccessPointName == "Kingdom1")) ? "City1_SurveyEnd" : "Kingdom1_SurveyEnd");
		advEngine.JumpScenario(text);
	}

	public void CloseScenarioAlertDialog()
	{
		scenarioAlertDialog.SetActive(value: false);
		advEngine.ResumeScenario();
	}

	public void OpenScenarioAlertDialog(AdvCommandSendMessageByName command)
	{
		string term = command.ParseCellOptional(AdvColumnName.Arg3, "");
		scenarioAlertTextLoc.Term = term;
		scenarioAlertDialog.SetActive(value: true);
		MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
	}

	public void PlayPlaylistBgm(AdvCommandSendMessageByName command)
	{
		string utagePlayBattleBgmName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		bool isUtagePlayBattleBgmNonStop = command.ParseCellOptional(AdvColumnName.Arg4, defaultVal: false);
		PlayerNonSaveDataManager.utagePlayBattleBgmName = utagePlayBattleBgmName;
		PlayerDataManager.playBgmCategoryName = "utagePlaylist";
		PlayerNonSaveDataManager.isUtagePlayBattleBgm = true;
		PlayerNonSaveDataManager.isUtagePlayBattleBgmNonStop = isUtagePlayBattleBgmNonStop;
		GameObject.Find("Bgm Play Manager").GetComponent<MasterAudioCustomManager>().ChangeMasterAudioPlaylist();
		Debug.Log("宴からPlaylistを再生する");
	}

	public void FadeStopPlaylistBgm(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.masterAudioFadeTime = command.ParseCellOptional(AdvColumnName.Arg3, 0.2f);
		GameObject.Find("Bgm Play Manager").GetComponent<MasterAudioCustomManager>().FadeMasterAudioPlaylist();
		Debug.Log("宴からPlaylistをフェード停止する");
	}

	public void ResetPlaylistBgm(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.isUtagePlayBattleBgm = false;
		Debug.Log("宴からPlaylistを再生するの変数を初期化");
	}

	public void StartUtageHmode(AdvCommandSendMessageByName command)
	{
		if (command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false))
		{
			PlayerNonSaveDataManager.isUtageHmode = true;
			advEngine.Config.BgmVolume = PlayerOptionsDataManager.optionsHBgmVolume;
		}
		else
		{
			PlayerNonSaveDataManager.isUtageHmode = false;
			advEngine.Config.BgmVolume = PlayerOptionsDataManager.optionsBgmVolume;
		}
	}
}
