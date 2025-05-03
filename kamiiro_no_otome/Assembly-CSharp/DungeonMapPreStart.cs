using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonMapPreStart : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		PoolManager.Pools["BattleEffect"].DespawnAll();
		PoolManager.Pools["SkillEffect"].DespawnAll();
		PoolManager.Pools["DungeonObject"].DespawnAll();
		GameObject playerExpParent = dungeonMapStatusManager.playerExpParent;
		GameObject[] array2;
		if (playerExpParent.transform.childCount > 0)
		{
			GameObject[] array = new GameObject[playerExpParent.transform.childCount];
			for (int i = 0; i < playerExpParent.transform.childCount; i++)
			{
				array[i] = playerExpParent.transform.GetChild(i).gameObject;
			}
			array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].transform.SetParent(dungeonMapManager.poolManagerGO);
			}
		}
		if (dungeonMapManager.miniCardGroup.transform.childCount > 0)
		{
			GameObject[] array3 = new GameObject[dungeonMapManager.miniCardGroup.transform.childCount];
			for (int k = 0; k < array3.Length; k++)
			{
				array3[k] = dungeonMapManager.miniCardGroup.transform.GetChild(k).gameObject;
			}
			array2 = array3;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].transform.SetParent(dungeonMapManager.poolManagerGO);
			}
		}
		PlayerDataManager.mapPlaceStatusNum = 3;
		if (PlayerDataManager.dungeonMoveSpeed < 1)
		{
			PlayerDataManager.dungeonMoveSpeed = 1;
		}
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
		dungeonMapManager.isDungeonRouteAction = false;
		dungeonMapManager.isBossRouteSelect = false;
		PlayerNonSaveDataManager.isDungeonBossBattle = false;
		PlayerNonSaveDataManager.isDungeonNoRetryBossBattle = false;
		dungeonMapManager.routeGroupArray[0].SetActive(value: true);
		dungeonMapManager.routeGroupArray[1].SetActive(value: false);
		dungeonMapStatusManager.isTpSkipEnable = false;
		dungeonMapStatusManager.tpSkipButtonCheckImageGo.SetActive(value: false);
		dungeonMapStatusManager.skipInfoWindowGo.SetActive(value: false);
		dungeonMapManager.dungeonMapCanvas.SetActive(value: true);
		dungeonMapManager.dungeonBattleCanvas.SetActive(value: false);
		dungeonMapManager.routeSelectGroupWindow.SetActive(value: true);
		dungeonMapManager.routeButtonGroup.SetActive(value: true);
		dungeonMapManager.miniCardGroup.SetActive(value: true);
		dungeonMapManager.routeAnimationGroupWindow.SetActive(value: false);
		dungeonMapManager.sexButton.SetActive(value: false);
		dungeonMapManager.bossButton.SetActive(value: false);
		dungeonMapManager.subGroup.SetActive(value: true);
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			float y = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList[0].dungeonMoveV2.y;
			dungeonMapManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(80f, y);
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
			float y2 = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList[0].dungeonMoveV2.y;
			dungeonMapManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(240f, y2);
		}
		dungeonMapManager.dungeonMapCanvas.GetComponent<CanvasGroup>().interactable = false;
		dungeonMapManager.cardInfoFrame.SetActive(value: false);
		PlayerNonSaveDataManager.currentSceneName = "scenario";
		dungeonMapManager.dungeonNameTerm.Term = "area" + PlayerDataManager.currentPlaceName;
		dungeonMapManager.dungeonCurrentFloorText.text = PlayerNonSaveDataManager.dungeonSetStartFloorNum.ToString();
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
			dungeonMapManager.dungeonBgImageArray[0].sprite = dungeonData.dungeonBgList[0];
			dungeonMapManager.dungeonBgImageArray[1].sprite = dungeonData.dungeonAnimationBgList[0];
			Debug.Log("ダンジョン背景：朝昼");
		}
		else
		{
			dungeonMapManager.dungeonBgImageArray[0].sprite = dungeonData.dungeonNightBgList[0];
			dungeonMapManager.dungeonBgImageArray[1].sprite = dungeonData.dungeonAnimationNightBgList[0];
			Debug.Log("ダンジョン背景：夕夜");
		}
		dungeonMapManager.dungeonBgImageArray[0].SetNativeSize();
		dungeonMapManager.dungeonBgImageArray[0].rectTransform.localScale = new Vector3(dungeonData.dungeonBgScale, dungeonData.dungeonBgScale, dungeonData.dungeonBgScale);
		dungeonMapManager.dungeonBgImageArray[0].rectTransform.anchoredPosition = new Vector2(dungeonData.dungeonBgPositonX, dungeonData.dungeonBgPositonY);
		dungeonMapManager.getDropMoney = 0;
		dungeonMapManager.getDropItemDictionary.Clear();
		dungeonMapManager.dungeonCurrentFloorNum = PlayerNonSaveDataManager.dungeonSetStartFloorNum;
		dungeonMapManager.currentBorderNum = 0;
		for (int l = 0; l < dungeonData.dungeonBorderFloor.Count && dungeonMapManager.dungeonCurrentFloorNum >= dungeonData.dungeonBorderFloor[l]; l++)
		{
			if (dungeonData.dungeonBorderFloor[l] != 1)
			{
				dungeonMapManager.currentBorderNum++;
			}
		}
		dungeonMapManager.thisFloorActionNum = 0;
		dungeonMapManager.miniCardList.Clear();
		dungeonMapManager.selectCardList.Clear();
		dungeonMapManager.isMimicBattle = false;
		dungeonMapManager.isSexLibidoEventEnable = false;
		dungeonMapManager.isSexFloorEventEnable = false;
		for (int m = 0; m < dungeonMapManager.selectCardBonusArray.Length; m++)
		{
			dungeonMapManager.selectCardBonusArray[m] = 0;
		}
		dungeonMapStatusManager.isAgilityFrameSetUp.Clear();
		dungeonMapStatusManager.isExpFrameSetUp.Clear();
		dungeonMapManager.ResetDungeonRouteFrame();
		dungeonMapManager.ChooseCardInittialize();
		PlayerDataManager.playerLibido = 0;
		for (int n = 0; n < PlayerStatusDataManager.characterSp.Length; n++)
		{
			PlayerStatusDataManager.characterSp[n] = 0;
		}
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
			int num2 = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName];
			num = ((!(dungeonData.dungeonName == "Shrine1")) ? (dungeonData.beforeFreeMaxFloor + num2 * 10) : (10 + num2 * 10));
			dungeonMapManager.dungeonMaxFloorText.text = num.ToString();
			dungeonMapManager.dungeonMaxFloorNum = num;
		}
		else
		{
			int maxFloor2 = dungeonData.maxFloor;
			dungeonMapManager.dungeonMaxFloorText.text = maxFloor2.ToString();
			dungeonMapManager.dungeonMaxFloorNum = maxFloor2;
		}
	}
}
