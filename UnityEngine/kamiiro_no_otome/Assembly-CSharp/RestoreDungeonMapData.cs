using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class RestoreDungeonMapData : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		dungeonMapManager.isDungeonRouteAction = false;
		dungeonMapManager.isBossRouteSelect = false;
		dungeonMapManager.getDropItemDictionary = new Dictionary<int, int>(PlayerNonSaveDataManager.backUpGetDropItemDictionary);
		dungeonMapManager.getDropMoney = PlayerNonSaveDataManager.backUpGetDropMoney;
		dungeonMapManager.dungeonCurrentFloorNum = PlayerNonSaveDataManager.backUpDungeonCurrentFloorNum;
		dungeonMapManager.currentBorderNum = PlayerNonSaveDataManager.backUpCurrentBorderNum;
		dungeonMapManager.isBossRouteSelect = PlayerNonSaveDataManager.backUpIsBossRoute;
		PlayerDataManager.playerLibido = PlayerNonSaveDataManager.backUpPlayerLibido;
		dungeonMapStatusManager.dungeonBuffAttack = PlayerNonSaveDataManager.backUpDungeonBuffAttack;
		dungeonMapStatusManager.dungeonBuffDefense = PlayerNonSaveDataManager.backUpDungeonBuffDefense;
		dungeonMapStatusManager.dungeonBuffRetreat = PlayerNonSaveDataManager.backUpDungeonBuffRetreat;
		dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor = PlayerNonSaveDataManager.backUpDungeonDeBuffAgiityRemainFloor;
		dungeonMapStatusManager.dungeonDeBuffAgility = PlayerNonSaveDataManager.backUpDungeonDeBuffAgility;
		PoolManager.Pools["BattleEffect"].DespawnAll();
		PoolManager.Pools["SkillEffect"].DespawnAll();
		PoolManager.Pools["DungeonObject"].DespawnAll();
		GameObject[] array2;
		if (dungeonMapManager.miniCardGroup.transform.childCount > 0)
		{
			GameObject[] array = new GameObject[dungeonMapManager.miniCardGroup.transform.childCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = dungeonMapManager.miniCardGroup.transform.GetChild(i).gameObject;
			}
			array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].transform.SetParent(dungeonMapManager.poolManagerGO);
			}
		}
		PlayerDataManager.mapPlaceStatusNum = 3;
		dungeonMapManager.speedTmpGO.text = PlayerDataManager.dungeonMoveSpeed.ToString();
		PlayerDataManager.isDungeonMapAuto = false;
		dungeonMapManager.autoButtonLoc.Term = "buttonDungeonMapAutoStart";
		dungeonMapManager.autoAlertGroup.SetActive(value: false);
		dungeonMapManager.routeButtonGroup.SetActive(value: true);
		array2 = dungeonMapManager.routeSelectFrameArray;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].GetComponent<ParameterContainer>().GetGameObject("autoGroupGo").SetActive(value: false);
		}
		dungeonMapManager.routeGroupArray[0].SetActive(value: true);
		dungeonMapManager.routeGroupArray[1].SetActive(value: false);
		dungeonMapManager.routeSelectGroupWindow.SetActive(value: true);
		dungeonMapManager.routeButtonGroup.SetActive(value: true);
		dungeonMapManager.miniCardGroup.SetActive(value: true);
		dungeonMapManager.routeAnimationGroupWindow.SetActive(value: false);
		dungeonMapManager.sexButton.SetActive(value: false);
		dungeonMapManager.bossButton.SetActive(value: false);
		dungeonMapManager.subGroup.SetActive(value: true);
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			string characterDungeonSexUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerDataManager.DungeonHeroineFollowNum).characterDungeonSexUnLockFlag;
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonSexUnLockFlag])
			{
				dungeonMapStatusManager.playerLibidoGroupGo.SetActive(value: true);
			}
			else
			{
				dungeonMapStatusManager.playerLibidoGroupGo.SetActive(value: false);
			}
		}
		else
		{
			dungeonMapStatusManager.playerLibidoGroupGo.SetActive(value: false);
		}
		dungeonMapManager.dungeonMapCanvas.GetComponent<CanvasGroup>().interactable = false;
		dungeonMapManager.cardInfoFrame.SetActive(value: false);
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		dungeonMapManager.dungeonNameTerm.Term = "area" + PlayerDataManager.currentPlaceName;
		dungeonMapManager.dungeonCurrentFloorText.text = dungeonMapManager.dungeonCurrentFloorNum.ToString();
		DungeonMapData dungeonData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData item) => item.dungeonName == PlayerDataManager.currentDungeonName);
		dungeonMapManager.basementTextGo.SetActive(!dungeonData.isTowerDungeon);
		bool flag = false;
		if (dungeonData.deepDungeonQuestFlag != int.MaxValue)
		{
			flag = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == dungeonData.deepDungeonQuestFlag).isOrdered;
			Debug.Log("深く潜れるクエストの受注：" + flag);
		}
		if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag) || flag)
		{
			if (flag)
			{
				CheckDungeonBossCount(dungeonData, flag);
			}
			else if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonData.deepDungeonFlag])
			{
				CheckDungeonBossCount(dungeonData, flag);
			}
			else
			{
				dungeonMapManager.dungeonMaxFloorText.text = dungeonData.beforeFreeMaxFloor.ToString();
				dungeonMapManager.dungeonMaxFloorNum = dungeonData.beforeFreeMaxFloor;
			}
		}
		else
		{
			dungeonMapManager.dungeonMaxFloorText.text = dungeonData.beforeFreeMaxFloor.ToString();
			dungeonMapManager.dungeonMaxFloorNum = dungeonData.beforeFreeMaxFloor;
		}
		if (PlayerDataManager.currentTimeZone < 2)
		{
			dungeonMapManager.dungeonBgImageArray[0].sprite = dungeonData.dungeonBgList[dungeonMapManager.currentBorderNum];
			dungeonMapManager.dungeonBgImageArray[1].sprite = dungeonData.dungeonAnimationBgList[dungeonMapManager.currentBorderNum];
		}
		else
		{
			dungeonMapManager.dungeonBgImageArray[0].sprite = dungeonData.dungeonNightBgList[dungeonMapManager.currentBorderNum];
			dungeonMapManager.dungeonBgImageArray[1].sprite = dungeonData.dungeonAnimationNightBgList[dungeonMapManager.currentBorderNum];
		}
		dungeonMapManager.thisFloorActionNum = 0;
		dungeonMapManager.miniCardList.Clear();
		dungeonMapManager.isMimicBattle = false;
		for (int k = 0; k < dungeonMapManager.selectCardBonusArray.Length; k++)
		{
			dungeonMapManager.selectCardBonusArray[k] = 0;
		}
		dungeonMapStatusManager.isAgilityFrameSetUp.Clear();
		dungeonMapStatusManager.isExpFrameSetUp.Clear();
		dungeonMapManager.ResetDungeonRouteFrame();
		dungeonMapManager.ChooseCardInittialize();
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CheckDungeonBossCount(DungeonMapData dungeonData, bool isQuestOrdered)
	{
		if (isQuestOrdered)
		{
			int maxFloor = dungeonData.maxFloor;
			dungeonMapManager.dungeonMaxFloorText.text = maxFloor.ToString();
			dungeonMapManager.dungeonMaxFloorNum = maxFloor;
		}
		else if (dungeonData.bossCount > 0)
		{
			int num = 0;
			int num2 = 0;
			if (dungeonData.dungeonName == "Shrine1")
			{
				num = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName] - 1;
				num2 = dungeonData.beforeFreeMaxFloor + num * 10;
			}
			else
			{
				num = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName];
				num2 = dungeonData.beforeFreeMaxFloor + num * 10;
			}
			dungeonMapManager.dungeonMaxFloorText.text = num2.ToString();
			dungeonMapManager.dungeonMaxFloorNum = num2;
		}
		else
		{
			int maxFloor2 = dungeonData.maxFloor;
			dungeonMapManager.dungeonMaxFloorText.text = maxFloor2.ToString();
			dungeonMapManager.dungeonMaxFloorNum = maxFloor2;
		}
	}
}
