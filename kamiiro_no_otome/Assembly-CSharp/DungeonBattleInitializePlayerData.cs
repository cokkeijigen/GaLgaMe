using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DungeonBattleInitializePlayerData : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	private ArborFSM statusFSM;

	public int itemDropProbability;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		statusFSM = GameObject.Find("Dungeon Map Status Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager.isCharacterAgilitySetUp.Clear();
		dungeonBattleManager.isEnemyAgilitySetUp.Clear();
		dungeonBattleManager.playerAgilityGoList.Clear();
		dungeonBattleManager.enemyAgilityGoList.Clear();
		dungeonBattleManager.playerHpText.text = PlayerStatusDataManager.playerAllHp.ToString();
		dungeonBattleManager.playerMaxHpText.text = PlayerStatusDataManager.playerAllMaxHp.ToString();
		dungeonBattleManager.playerHpSlider.maxValue = PlayerStatusDataManager.playerAllMaxHp;
		dungeonBattleManager.playerHpSlider.value = PlayerStatusDataManager.playerAllHp;
		PlayerStatusDataManager.CalkPlayerChargeStatus();
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			dungeonBattleManager.playerSpSlider.value = PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum];
			dungeonBattleManager.playerSpText.text = PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum].ToString();
			int heroineId = PlayerDataManager.DungeonHeroineFollowNum;
			List<DungeonLibidoData> dungeonLibidoDataList = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList;
			Vector2 dungeonBattleFollowEdenV = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == heroineId).dungeonBattleFollowEdenV2;
			dungeonBattleManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(dungeonBattleFollowEdenV.x, dungeonBattleFollowEdenV.y);
			Vector2 dungeonBattleFollowEdenSizeV = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == heroineId).dungeonBattleFollowEdenSizeV2;
			dungeonBattleManager.chracterImageGoArray[0].GetComponent<RectTransform>().sizeDelta = new Vector2(dungeonBattleFollowEdenSizeV.x, dungeonBattleFollowEdenSizeV.y);
			dungeonBattleManager.chracterImageGoArray[1].SetActive(value: true);
			dungeonBattleManager.chracterImageGoArray[1].GetComponent<Image>().sprite = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == heroineId).battleSprite;
			Vector2 dungeonBattleV = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == heroineId).dungeonBattleV2;
			dungeonBattleManager.chracterImageGoArray[1].GetComponent<RectTransform>().anchoredPosition = dungeonBattleV;
			Vector2 dungeonBattleSizeV = dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == heroineId).dungeonBattleSizeV2;
			dungeonBattleManager.chracterImageGoArray[1].GetComponent<RectTransform>().sizeDelta = new Vector2(dungeonBattleSizeV.x, dungeonBattleSizeV.y);
			if (dungeonLibidoDataList.Find((DungeonLibidoData data) => data.characterID == heroineId).dungeonBattleIsLeadPosition)
			{
				dungeonBattleManager.chracterImageGoArray[1].transform.SetSiblingIndex(2);
			}
			else
			{
				dungeonBattleManager.chracterImageGoArray[1].transform.SetSiblingIndex(1);
			}
			if (PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] >= 100)
			{
				dungeonBattleManager.chargetAttackButton.SetActive(value: true);
				Debug.Log("チャージボタン表示／ヒロイン同行中");
			}
			else
			{
				dungeonBattleManager.chargetAttackButton.SetActive(value: false);
				Debug.Log("チャージボタン非表示／ヒロイン同行中");
			}
		}
		else
		{
			dungeonBattleManager.playerSpSlider.value = 0f;
			dungeonBattleManager.playerSpText.text = "0";
			dungeonBattleManager.chracterImageGoArray[1].SetActive(value: false);
			dungeonBattleManager.chracterImageGoArray[1].transform.SetSiblingIndex(1);
			List<DungeonLibidoData> dungeonLibidoDataList2 = dungeonMapManager.dungeonLibidoDataBase.dungeonLibidoDataList;
			float y = dungeonLibidoDataList2[0].dungeonBattleV2.y;
			dungeonBattleManager.chracterImageGoArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(100f, y);
			Vector2 dungeonBattleSizeV2 = dungeonLibidoDataList2[0].dungeonBattleSizeV2;
			dungeonBattleManager.chracterImageGoArray[0].GetComponent<RectTransform>().sizeDelta = new Vector2(dungeonBattleSizeV2.x, dungeonBattleSizeV2.y);
			dungeonBattleManager.chargetAttackButton.SetActive(value: false);
			Debug.Log("チャージボタン非表示／ソロ");
		}
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			dungeonBattleManager.isCharacterAgilitySetUp.Add(item: false);
			Transform transform = PoolManager.Pools["DungeonObject"].Spawn(dungeonBattleManager.playerAgilityGo, dungeonBattleManager.playerAgilityParent.transform);
			int num = PlayerStatusDataManager.playerPartyMember[i];
			transform.GetComponent<ParameterContainer>().SetInt("characterID", PlayerStatusDataManager.playerPartyMember[i]);
			transform.GetComponent<ParameterContainer>().SetInt("partyMemberNum", i);
			transform.GetComponent<ParameterContainer>().SetInt("characterAgility", PlayerStatusDataManager.characterAgility[num]);
			transform.GetComponent<ParameterContainer>().GetVariable<TmpText>("levelText").textMeshProUGUI.text = PlayerStatusDataManager.characterLv[num].ToString();
			transform.GetComponent<ParameterContainer>().SetBool("isInitialize", value: false);
			transform.transform.localScale = new Vector3(1f, 1f, 1f);
			dungeonBattleManager.playerAgilityGoList.Add(transform.gameObject);
		}
		statusFSM.SendTrigger("RefreshDungeonBuff");
		if (dungeonMapManager.isMimicBattle)
		{
			int num2 = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).battleMimicID[dungeonMapManager.currentBorderNum];
			PlayerStatusDataManager.enemyMember = new int[1] { num2 };
		}
		else if (!dungeonMapManager.isBossRouteSelect && !PlayerNonSaveDataManager.isDungeonScnearioBattle)
		{
			DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
			int enemyCountNum = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].enemyCountNum;
			string subTypeString = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].subTypeString;
			int[] array = null;
			if (!(subTypeString == "battle"))
			{
				if (subTypeString == "hardBattle")
				{
					array = dungeonMapData.hardBattleEnemyID[dungeonMapManager.currentBorderNum];
				}
			}
			else
			{
				array = dungeonMapData.battleEnemyID[dungeonMapManager.currentBorderNum];
			}
			List<int> list = new List<int>();
			for (int j = 0; j < enemyCountNum; j++)
			{
				int num3 = Random.Range(0, 3);
				list.Add(array[num3]);
				Debug.Log("参戦エネミーID：" + array[num3] + "／ランダム：" + num3);
			}
			PlayerStatusDataManager.enemyMember = list.ToArray();
		}
		else if (dungeonMapManager.isBossRouteSelect)
		{
			BackUpBeforeData();
			DungeonMapData dungeonData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
			bool flag = false;
			if (dungeonData.deepDungeonQuestFlag != int.MaxValue)
			{
				flag = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == dungeonData.deepDungeonQuestFlag).isOrdered;
				Debug.Log("深く潜れるクエストの受注：" + flag);
			}
			int index = 0;
			if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag) || flag)
			{
				if (flag)
				{
					index = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName];
				}
				else if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonData.deepDungeonFlag])
				{
					index = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName];
				}
			}
			int[] enemyMember = new int[1] { dungeonData.bossEnemyID[index] };
			if (dungeonData.isExtraQuestBossExist && dungeonData.extraQuestBossFloor == dungeonMapManager.dungeonCurrentFloorNum)
			{
				QuestClearData questData = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == dungeonData.extraQuestID);
				QuestData questData2 = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questData.sortID);
				bool flag2 = false;
				for (int k = 0; k < questData2.questStartFlagNameList.Count; k++)
				{
					if (!PlayerFlagDataManager.scenarioFlagDictionary[questData2.questStartFlagNameList[k]])
					{
						flag2 = true;
						break;
					}
				}
				Debug.Log("特殊クエストID／" + questData.sortID + "／表示フラグの未達成：" + flag2 + "／特殊クエストのクリア：" + questData.isClear);
				if (!flag2 && !questData.isClear)
				{
					enemyMember = new int[1] { dungeonData.extraQuestBossID };
					Debug.Log("特殊クエストのボスを表示");
				}
			}
			PlayerStatusDataManager.enemyMember = enemyMember;
		}
		else
		{
			BackUpBeforeData();
			PlayerStatusDataManager.enemyMember = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName).battleEnemyID.ToArray();
		}
		PlayerStatusDataManager.SetUpEnemyStatus(CallBackMethod);
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

	private void BackUpBeforeData()
	{
		PlayerNonSaveDataManager.beforePlayerAllHp = PlayerStatusDataManager.playerAllHp;
		for (int i = 0; i < PlayerStatusDataManager.partyMemberCount; i++)
		{
			PlayerNonSaveDataManager.beforePlayerSp[i] = PlayerStatusDataManager.characterSp[i];
		}
		PlayerNonSaveDataManager.beforeHaveItemSortIdList.Clear();
		PlayerNonSaveDataManager.beforeHaveItemIdList.Clear();
		PlayerNonSaveDataManager.beforeHaveItemHaveNumList.Clear();
		for (int j = 0; j < PlayerInventoryDataManager.haveItemList.Count; j++)
		{
			PlayerNonSaveDataManager.beforeHaveItemSortIdList.Add(PlayerInventoryDataManager.haveItemList[j].itemSortID);
			PlayerNonSaveDataManager.beforeHaveItemIdList.Add(PlayerInventoryDataManager.haveItemList[j].itemID);
			PlayerNonSaveDataManager.beforeHaveItemHaveNumList.Add(PlayerInventoryDataManager.haveItemList[j].haveCountNum);
		}
	}

	private void CallBackMethod()
	{
		Debug.Log("コールバックで呼ばれた");
		for (int i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			dungeonBattleManager.isEnemyAgilitySetUp.Add(item: false);
			Transform transform = PoolManager.Pools["BattleObject"].Spawn(dungeonBattleManager.enemyAgilityGo, dungeonBattleManager.enemyAgilityParent.transform);
			int id = PlayerStatusDataManager.enemyMember[i];
			transform.GetComponent<ParameterContainer>().SetInt("enemyID", PlayerStatusDataManager.enemyMember[i]);
			transform.GetComponent<ParameterContainer>().SetInt("enemyPartyNum", i);
			transform.GetComponent<ParameterContainer>().SetInt("enemyAgility", PlayerStatusDataManager.enemyAgility[i]);
			Sprite sprite = null;
			sprite = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id).enemyImageMiniSprite;
			transform.GetComponent<ParameterContainer>().GetVariable<UguiImage>("enemyImage").image.sprite = sprite;
			transform.GetComponent<ParameterContainer>().SetBool("isInitialize", value: false);
			dungeonBattleManager.enemyAgilityGoList.Add(transform.gameObject);
		}
		dungeonBattleManager.enemyHpText.text = PlayerStatusDataManager.enemyAllHp.ToString();
		dungeonBattleManager.enemyMaxHpText.text = PlayerStatusDataManager.enemyAllMaxHp.ToString();
		dungeonBattleManager.enemyHpSlider.maxValue = PlayerStatusDataManager.enemyAllMaxHp;
		dungeonBattleManager.enemyHpSlider.value = PlayerStatusDataManager.enemyAllMaxHp;
		int enemyCharge = Random.Range(0, 20);
		dungeonBattleManager.enemyCharge = enemyCharge;
		dungeonBattleManager.enemyChargeSlider.value = 0f;
		dungeonBattleManager.enemyChargeText.text = "0";
		if (dungeonMapManager.battleConsecutiveRoundNum > 1)
		{
			PlayerBattleConditionAccess.DungeonBattleConditionInititialize(isEnemyOnly: true);
			dungeonBattleManager.SetAllStatusIconInVisible(1);
		}
		else
		{
			PlayerBattleConditionAccess.DungeonBattleConditionInititialize(isEnemyOnly: false);
			dungeonBattleManager.SetAllStatusIconInVisible(0);
			dungeonBattleManager.SetAllStatusIconInVisible(1);
		}
		if (dungeonMapManager.battleConsecutiveRoundNum == 0)
		{
			PlayerNonSaveDataManager.preGetDropMagicMaterial.Clear();
			dungeonBattleManager.itemWaitSlider.value = dungeonBattleManager.itemWaitSlider.maxValue;
			dungeonBattleManager.itemWaitSlider.GetComponent<ArborFSM>().SendTrigger("StartItemWait");
		}
		int j;
		for (j = 0; j < PlayerStatusDataManager.enemyMember.Length; j++)
		{
			int dropMagicMaterial = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == PlayerStatusDataManager.enemyMember[j]).dropMagicMaterial;
			ParameterContainer component = dungeonBattleManager.enemyAgilityGoList[j].GetComponent<ParameterContainer>();
			if (dropMagicMaterial != 0)
			{
				int num = Random.Range(0, 100);
				if (PlayerNonSaveDataManager.isDungeonScnearioBattle)
				{
					num = 0;
					Debug.Log("ダンジョンのシナリオ戦闘");
				}
				else if (dungeonMapManager.isBossRouteSelect)
				{
					num = 0;
					Debug.Log("ボスなので、魔力片ドロップ確定");
				}
				else if (dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].subTypeString == "hardBattle")
				{
					num -= 25;
					Debug.Log("強敵なので、魔力片ドロップ確率アップ");
				}
				else if (dropMagicMaterial >= 682 || dungeonMapManager.battleConsecutiveRoundNum > 1)
				{
					num -= 15;
					Debug.Log("連戦orレッド以上なので、魔力片ドロップ確率ややアップ");
				}
				Debug.Log("獲得確率：" + itemDropProbability + "／装飾品バフ：" + PlayerEquipDataManager.accessoryItemDiscover + "／魔力片獲得判定：" + num);
				bool flag = false;
				if (dungeonMapManager.isMimicBattle)
				{
					flag = ((90 >= num) ? true : false);
				}
				if (itemDropProbability + PlayerEquipDataManager.accessoryItemDiscover >= num || flag)
				{
					int key = int.Parse(dungeonMapManager.thisFloorActionNum.ToString() + j);
					PlayerNonSaveDataManager.preGetDropMagicMaterial.Add(key, dropMagicMaterial);
					Debug.Log("魔力片のプリドロップを追加");
					component.GetGameObject("materialIconGo").SetActive(value: true);
				}
				else
				{
					component.GetGameObject("materialIconGo").SetActive(value: false);
				}
			}
			else
			{
				component.GetGameObject("materialIconGo").SetActive(value: false);
			}
		}
		Transition(stateLink);
	}
}
