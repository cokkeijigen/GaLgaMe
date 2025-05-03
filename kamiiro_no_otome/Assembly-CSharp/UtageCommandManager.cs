using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

public class UtageCommandManager : MonoBehaviour
{
	private PlayMakerFSM bgmManegerFSM;

	public bool isScenarioDebug;

	private void Awake()
	{
		bgmManegerFSM = GameObject.Find("Bgm Play Manager").GetComponent<PlayMakerFSM>();
	}

	public void SetScenarioClearFlag(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerFlagDataManager.scenarioFlagDictionary[text] = true;
		if (PlayerFlagDataManager.sceneGarellyFlagDictionary.ContainsKey(text))
		{
			PlayerFlagDataManager.sceneGarellyFlagDictionary[text] = true;
		}
		PlayerQuestDataManager.RefreshStoryQuestFlagData("scenario", 0, text);
		PlayerQuestDataManager.RefreshStoryQuestFlagData("sexScenarioClear", 0, text);
		PlayerQuestDataManager.RefreshStoryQuestFlagData("heroineContract", 0, text);
		PlayerQuestDataManager.RefreshStoryQuestFlagData("fertilize", 0, text);
		Debug.Log(text + "：クリア");
	}

	public void SetSexEventTotalCount(AdvCommandSendMessageByName command)
	{
		string scenarioName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		ScenarioFlagData scenarioFlagData = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == scenarioName);
		for (int i = 0; i < scenarioFlagData.sexCharacterIdList.Count; i++)
		{
			int num = scenarioFlagData.sexCharacterIdList[i];
			if (num != 9)
			{
				if (num == 10)
				{
					num = 0;
				}
				PlayerSexStatusDataManager.AddTotalSexCount("uniqueSexCount", num, scenarioFlagData.sexUniqueCountList[i]);
				PlayerSexStatusDataManager.AddTotalSexCount("sexCount", num, scenarioFlagData.sexInsertCountList[i]);
				PlayerSexStatusDataManager.AddTotalSexCount("piston", num, scenarioFlagData.sexPistonCountList[i]);
				PlayerSexStatusDataManager.AddTotalSexCount("mouth", num, scenarioFlagData.sexMouthCountList[i]);
				PlayerSexStatusDataManager.AddTotalSexCount("outShot", num, scenarioFlagData.sexOutShotCountList[i]);
				PlayerSexStatusDataManager.AddTotalSexCount("inShot", num, scenarioFlagData.sexInShotCountList[i]);
				PlayerSexStatusDataManager.AddTotalSexCount("ecstasy", num, scenarioFlagData.sexHeroineEcstasyCountList[i]);
			}
		}
		Debug.Log(scenarioName + "：えっちイベントでの累計回数加算");
	}

	public void SetScenarioClearDayCount(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerFlagDataManager.eventStartingDayDictionary[text] = PlayerDataManager.currentTotalDay;
		Debug.Log(text + "：クリア日は：" + PlayerDataManager.currentTotalDay + "日");
	}

	public void CheckScenarioClear(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		bool value = false;
		if (PlayerFlagDataManager.scenarioFlagDictionary.ContainsKey(text))
		{
			value = PlayerFlagDataManager.scenarioFlagDictionary[text];
		}
		GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Param.SetParameter("isScenarioClear", value);
		Debug.Log(text + "はクリア：" + value);
	}

	public void CheckFirstSexTouchClear(AdvCommandSendMessageByName command)
	{
		int index = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		bool value = PlayerFlagDataManager.heroineFirstSexTouchFlagList[index];
		GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Param.SetParameter("isHeroineFirstSexTouchClear", value);
		Debug.Log("ヒロインID：" + index + "／初回身体検査のクリア状況：" + value);
	}

	public void SetFirstSexTouchClearFlag(AdvCommandSendMessageByName command)
	{
		int index = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerFlagDataManager.heroineFirstSexTouchFlagList[index] = true;
		Debug.Log("ヒロインID：" + index + "／初回身体検査をクリア");
	}

	public void SetResetScenarioFlagName(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		string text2 = command.ParseCellOptional(AdvColumnName.Arg4, "");
		PlayerNonSaveDataManager.isRetreatScenarioFlagReset = true;
		PlayerNonSaveDataManager.retreatResetFlagNameArray[0] = text;
		PlayerNonSaveDataManager.retreatResetFlagNameArray[1] = text2;
		Debug.Log("フラグのリセット用変数：" + PlayerNonSaveDataManager.isRetreatScenarioFlagReset);
	}

	public void SetDungeonClearFlag(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		if (PlayerFlagDataManager.dungeonFlagDictionary.ContainsKey(text))
		{
			PlayerFlagDataManager.dungeonFlagDictionary[text] = true;
		}
		else
		{
			PlayerFlagDataManager.dungeonFlagDictionary.Add(text, value: true);
		}
		Debug.Log(text + "：クリア");
	}

	public void CheckDungeonClear(AdvCommandSendMessageByName command)
	{
		bool value = false;
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		if (PlayerFlagDataManager.dungeonFlagDictionary.ContainsKey(text))
		{
			value = PlayerFlagDataManager.dungeonFlagDictionary[text];
		}
		GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Param.SetParameter("isDungeonClear", value);
		Debug.Log(text + "はクリア：" + value);
	}

	public void SetDungeonHeroineFollow(AdvCommandSendMessageByName command)
	{
		int num = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		bool num2 = command.ParseCellOptional(AdvColumnName.Arg4, defaultVal: false);
		if (num == 0)
		{
			PlayerDataManager.isDungeonHeroineFollow = false;
		}
		else
		{
			PlayerDataManager.isDungeonHeroineFollow = true;
			PlayerDataManager.DungeonHeroineFollowNum = num;
		}
		Debug.Log("ダンジョンへの同行者ID：" + num);
		if (num2)
		{
			GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		}
	}

	public void CheckDungeonHeroineFollow(AdvCommandSendMessageByName command)
	{
		bool isDungeonHeroineFollow = PlayerDataManager.isDungeonHeroineFollow;
		GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Param.SetParameter("isDungeonHeroineFollow", isDungeonHeroineFollow);
		Debug.Log("ダンジョンにヒロインは同行中：" + isDungeonHeroineFollow);
	}

	public void SetIsDungeonSexEvent(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerNonSaveDataManager.isDungeonSexEvent = true;
		Debug.Log("ダンジョンのイチャイチャフラグ：" + text);
	}

	public void RecoveryCharacterHp(AdvCommandSendMessageByName command)
	{
		int num = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerStatusDataManager.characterHp[num] = PlayerStatusDataManager.characterMaxHp[num];
		PlayerStatusDataManager.characterMp[num] = PlayerStatusDataManager.characterMaxMp[num];
	}

	public void SetNewMapPoint(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		string text2 = command.ParseCellOptional(AdvColumnName.Arg4, "");
		PlayerDataManager.newMapPointName[0] = text;
		PlayerDataManager.newMapPointName[1] = text2;
		PlayerDataManager.isNewMapNotice = true;
	}

	public void SetNeedEffectNewMapPoint(AdvCommandSendMessageByName command)
	{
		PlayerDataManager.needEffectNewWorldMapPointName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerDataManager.isNeedEffectNewWorldMapPoint = true;
	}

	public void SetEnableMapMenuButton(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerNonSaveDataManager.isEnableMapMenuNotice = true;
	}

	public void SetNewRecipeFlag(AdvCommandSendMessageByName command)
	{
		string key = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerFlagDataManager.recipeFlagDictionary[key] = true;
		PlayerDataManager.isNewRecipeNotice = true;
	}

	public void SetNewRecipeNoticeOnly(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerDataManager.isNewCraftAndExtensionNotice = true;
	}

	public void SetNewItemShopNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.isNewItemShopNotice = true;
	}

	public void SetNewStoryQuestNotice(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		if (!(text == "story"))
		{
			if (text == "sub")
			{
				PlayerNonSaveDataManager.isNewStorySubQuestNotice = true;
				PlayerDataManager.isNoCheckNewSubQuest = true;
			}
		}
		else
		{
			PlayerNonSaveDataManager.isNewStoryQuestNotice = true;
			PlayerDataManager.isNoCheckNewQuest = true;
		}
	}

	public void SetNewStoryQuestNoticeCheckFlag(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		int questId = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questId);
		bool flag = false;
		for (int i = 0; i < questData.questStartFlagNameList.Count; i++)
		{
			if (!PlayerFlagDataManager.scenarioFlagDictionary[questData.questStartFlagNameList[i]])
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			if (!(text == "story"))
			{
				if (text == "sub")
				{
					PlayerNonSaveDataManager.isNewStorySubQuestNotice = true;
					PlayerDataManager.isNoCheckNewSubQuest = true;
				}
			}
			else
			{
				PlayerNonSaveDataManager.isNewStoryQuestNotice = true;
				PlayerDataManager.isNoCheckNewQuest = true;
			}
			Debug.Log("必要フラグはクリアしているので、クエスト通知を表示する");
		}
		else
		{
			Debug.Log("必要フラグは未クリアなので、クエスト通知は表示しない");
		}
	}

	public void SetChargeAttackTutorial(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.isChargeAttackTutorial = true;
	}

	public void SetFreeDungeonNotice(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerNonSaveDataManager.noticeDungeonTermString = "area" + text;
		PlayerNonSaveDataManager.isFreeDungeonNotice = true;
	}

	public void SetDeepDungeonNotice(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerNonSaveDataManager.noticeDungeonTermString = "area" + text;
		PlayerNonSaveDataManager.isDeepDungeonNotice = true;
	}

	public void SetHeroineFollowNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = "character" + command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isHeroineFollowNotice = true;
		if (!PlayerFlagDataManager.tutorialFlagDictionary["heroineFollow"])
		{
			PlayerNonSaveDataManager.isHeroineFollowTutorial = true;
		}
	}

	public void SetHeroineAllTimeFollowNotice(AdvCommandSendMessageByName command)
	{
		int index = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[index] = true;
		PlayerNonSaveDataManager.noticeTermString = "character" + index;
		PlayerNonSaveDataManager.isHeroineAllTimeFollowNotice = true;
	}

	public void SetHeroineSpecifyFollowNotice(AdvCommandSendMessageByName command)
	{
		bool isHeroineSpecifyFollow = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: true);
		string heroineSpecifyFollowPoint = command.ParseCellOptional(AdvColumnName.Arg4, "");
		int heroineSpecifyFollowId = command.ParseCellOptional(AdvColumnName.Arg5, 0);
		PlayerDataManager.isHeroineSpecifyFollow = isHeroineSpecifyFollow;
		PlayerDataManager.heroineSpecifyFollowPoint = heroineSpecifyFollowPoint;
		PlayerDataManager.heroineSpecifyFollowId = heroineSpecifyFollowId;
		PlayerNonSaveDataManager.isHeroineSpecifyFollowNotice = true;
	}

	public void SetDungeonSexNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = "character" + command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isDungeonSexNotice = true;
	}

	public void SetEnableSexNotice(AdvCommandSendMessageByName command)
	{
		int num = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		string text = command.ParseCellOptional(AdvColumnName.Arg4, "");
		PlayerNonSaveDataManager.noticeTermString = "character" + num;
		switch (text)
		{
		case "Touch":
			PlayerNonSaveDataManager.isSexTouchxNotice = true;
			break;
		case "Fellatio":
			PlayerNonSaveDataManager.isFellatioNotice = true;
			break;
		case "Battle":
			PlayerNonSaveDataManager.isSexBattleNotice = true;
			break;
		}
	}

	public void SetHeroinePowerUpNotice(AdvCommandSendMessageByName command)
	{
		int index = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.noticeTermString = "character" + index;
		PlayerNonSaveDataManager.isHeroinePowerUpNotice = true;
		PlayerFlagDataManager.partyPowerUpFlagList[index] = true;
	}

	public void SetHeroineMergeNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = "character" + command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isHeroineMergeNotice = true;
	}

	public void SetHeroineMoveNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.isHeroineMoveNotice = true;
	}

	public void SetHeroineSexStatusViewNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = "character" + command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isHeroineSexStatusViewNotice = true;
	}

	public void SetHeroineMenstruationStartNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = "character" + command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isHeroineMenstruationStartNotice = true;
	}

	public void SetHeroineMenstruationViewNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = "character" + command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isHeroineMenstruationViewNotice = true;
	}

	public void SetHeroineEnableFertilizeNotice(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.noticeTermString = "character" + command.ParseCellOptional(AdvColumnName.Arg3, 0);
		PlayerNonSaveDataManager.isHeroineEnableFertilizeNotice = true;
	}

	public void AddDefaultLearnedSkill(AdvCommandSendMessageByName command)
	{
		PlayerInventoryDataEquipAccess.AddDefaultHeroineLearnedSkill(command.ParseCellOptional(AdvColumnName.Arg3, 0));
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
	}

	public void AddEventLearnedSkill(AdvCommandSendMessageByName command)
	{
		PlayerInventoryDataEquipAccess.AddEventHeroineLearnedSkill(command.ParseCellOptional(AdvColumnName.Arg3, 0));
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
	}

	public void AddHaveItem(AdvCommandSendMessageByName command)
	{
		int addItemID = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		int addSortID = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		int addNum = command.ParseCellOptional(AdvColumnName.Arg5, 0);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(addItemID, addSortID, addNum);
	}

	public void AddHaveEventItem(AdvCommandSendMessageByName command)
	{
		int addItemID = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		int addSortID = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		PlayerInventoryDataAccess.PlayerHaveEventItemAdd(addItemID, addSortID);
	}

	public void SetRareDropRate(AdvCommandSendMessageByName command)
	{
		int rareDropRateRaisePowerNum = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		int num = command.ParseCellOptional(AdvColumnName.Arg4, 0);
		PlayerDataManager.rareDropRateRaisePowerNum = rareDropRateRaisePowerNum;
		PlayerDataManager.rareDropRateRaiseRaimingDaysNum += num;
	}

	public void ReturnToTitleScene(AdvCommandSendMessageByName command)
	{
		isScenarioDebug = false;
		SceneManager.LoadScene("title");
		Debug.Log("タイトル画面に戻る");
	}

	public void StopMasterAudio(AdvCommandSendMessageByName command)
	{
		command.ParseCellOptional(AdvColumnName.Arg6, 0.2f);
		PlayerNonSaveDataManager.masterAudioFadeTime = 0.2f;
		bgmManegerFSM.SendEvent("FadeMasterAudioPlaylist");
		Debug.Log("BGMをフェードして停止");
	}

	public void SetLocalMapActionLimit(AdvCommandSendMessageByName command)
	{
		PlayerDataManager.isLocalMapActionLimit = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
	}

	public void CalcSkipBattleResult(AdvCommandSendMessageByName command)
	{
		command.ParseCellOptional(AdvColumnName.Arg3, "");
		GameObject.Find("Result Dialog Manager").GetComponent<ArborFSM>().SendTrigger("CalcSkipBattleResult");
	}

	public void AddRemainingSemenTime(AdvCommandSendMessageByName command)
	{
		int num = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		if (command.ParseCellOptional(AdvColumnName.Arg4, defaultVal: false))
		{
			PlayerSexStatusDataManager.heroineRemainingSemenCount_anal[num] = PlayerSexStatusDataManager.remainingSemenDefaultValue;
		}
		else
		{
			PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[num] = PlayerSexStatusDataManager.remainingSemenDefaultValue;
		}
	}

	public void SetDungeonSudenFloorEvent(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.isDungeonSuddenFloorEvent = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
	}

	public void SetShrine1DeepClearFlag(AdvCommandSendMessageByName command)
	{
		string text = command.ParseCellOptional(AdvColumnName.Arg3, "");
		if (!PlayerFlagDataManager.dungeonDeepClearFlagDictionary[text])
		{
			PlayerNonSaveDataManager.isDungeonDeepClear = true;
			PlayerFlagDataManager.dungeonDeepClearFlagDictionary[text] = true;
			Debug.Log(text + "：最奥ボス撃破");
		}
	}
}
