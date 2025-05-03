using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenBattleResultCanvas : StateBehaviour
{
	private ResultDialogManager resultDialogManager;

	public int itemDropProbability;

	private SortedDictionary<int, int> getDropItemIdDictionary = new SortedDictionary<int, int>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		resultDialogManager = GetComponent<ResultDialogManager>();
	}

	public override void OnStateBegin()
	{
		resultDialogManager.isAutoCloseToggleInitialize = false;
		resultDialogManager.isResultAnimationEnd = false;
		ResultReset();
		resultDialogManager.resultCanvasGO.SetActive(value: true);
		int num = PlayerStatusDataManager.enemyGold.Sum();
		float num2 = Random.Range(0.9f, 1.1f);
		num = Mathf.FloorToInt((float)num * num2);
		if (PlayerEquipDataManager.accessoryDropMoneyUp > 0)
		{
			num = Mathf.FloorToInt((float)num * 1.5f);
		}
		if (PlayerNonSaveDataManager.battleResultDialogType == "scenarioBattle")
		{
			ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			if (scenarioBattleData.isSupportCharacterTakeDamage)
			{
				PlayerStatusDataManager.playerPartyMember = scenarioBattleData.battleCharacterID.ToArray();
			}
		}
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			Transform transform = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.characterImagePrefabGo, resultDialogManager.characterImageParentGo.transform);
			int num3 = PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[i]];
			transform.transform.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>().text = num3.ToString();
			transform.transform.Find("Mask/Face Image").GetComponent<Image>().sprite = GameDataManager.instance.playerResultFrameSprite[PlayerStatusDataManager.playerPartyMember[i]];
			resultDialogManager.characterImageSpawnGoList.Add(transform.gameObject);
			int num4 = PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[i]];
			Slider component = transform.transform.Find("Exp Slider").GetComponent<Slider>();
			component.maxValue = PlayerStatusDataManager.characterNextLvExp[PlayerStatusDataManager.playerPartyMember[i]];
			component.minValue = PlayerStatusDataManager.characterCurrentLvExp[PlayerStatusDataManager.playerPartyMember[i]];
			component.value = num4;
			transform.gameObject.GetComponent<ParameterContainer>().SetInt("characterID", PlayerStatusDataManager.playerPartyMember[i]);
			transform.gameObject.GetComponent<ParameterContainer>().SetInt("levelUpCount", 0);
			transform.GetComponent<PlayMakerFSM>().enabled = true;
		}
		int num5 = PlayerStatusDataManager.enemyExp.Sum();
		float num6 = 0f;
		for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
		{
			num6 += (float)PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[j]];
		}
		num6 /= (float)PlayerStatusDataManager.playerPartyMember.Length;
		float num7 = PlayerStatusDataManager.enemyLv.Sum();
		num7 /= (float)PlayerStatusDataManager.enemyMember.Length;
		Debug.Log("味方LV：" + num6 + "／敵LV：" + num7);
		if (num7 > num6)
		{
			float num8 = Mathf.Clamp(num7 / num6, 0f, 2f);
			float f = (float)num5 * num8;
			num5 = Mathf.FloorToInt(f);
			Debug.Log("LV差のEXPボーナス：" + Mathf.FloorToInt(f));
		}
		if (PlayerEquipDataManager.accessoryExpUp > 0)
		{
			num5 = Mathf.FloorToInt((float)num5 * 1.5f);
		}
		Sprite sprite = null;
		string battleResultDialogType = PlayerNonSaveDataManager.battleResultDialogType;
		if (!(battleResultDialogType == "scenarioBattle"))
		{
			if (battleResultDialogType == "dungeonBattle")
			{
				DungeonMapManager component2 = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
				DungeonGetItemManager component3 = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
				int num9 = 0;
				if (!component2.isBossRouteSelect && !PlayerNonSaveDataManager.isDungeonScnearioBattle)
				{
					DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
					resultDialogManager.SpawnDungeonBattleWinEffect();
					num += component2.consecutiveResultData[0];
					num5 += component2.consecutiveResultData[1];
					resultDialogManager.expText.text = num5.ToString();
					for (int k = 0; k < PlayerStatusDataManager.enemyMember.Length; k++)
					{
						component2.consecutiveResultEnemyMember.Add(PlayerStatusDataManager.enemyMember[k]);
					}
					getDropItemIdDictionary.Clear();
					int accessoryItemDiscover = PlayerEquipDataManager.accessoryItemDiscover;
					for (int l = 0; l < component2.consecutiveResultEnemyMember.Count; l++)
					{
						int num10 = Random.Range(0, 100);
						Debug.Log("獲得判定：" + num10 + "/アイテム取得確率：" + (itemDropProbability + accessoryItemDiscover));
						if (itemDropProbability + accessoryItemDiscover >= num10)
						{
							int id = component2.consecutiveResultEnemyMember[l];
							Dictionary<int, float> dropItemDictionary = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id).dropItemDictionary;
							int dungeonBattleDropItem = component2.GetDungeonBattleDropItem(dropItemDictionary);
							component2.SetDropItemDictionary(dungeonBattleDropItem, bonusChance: false);
							AddDropItemIdDictionary(dungeonBattleDropItem);
						}
						int num11 = Random.Range(0, 100);
						Debug.Log("粉末獲得判定：" + num11 + "/アイテム取得確率：" + (itemDropProbability / 3 + accessoryItemDiscover));
						if (itemDropProbability / 3 + accessoryItemDiscover >= num11)
						{
							int num12 = 850;
							component2.SetDropItemDictionary(num12, bonusChance: false);
							AddDropItemIdDictionary(num12);
						}
						int num13 = Random.Range(0, 100);
						Debug.Log("小さな魔力片獲得判定：" + num13 + "/アイテム取得確率：" + (itemDropProbability / 3 + accessoryItemDiscover));
						if (itemDropProbability / 3 + accessoryItemDiscover >= num13)
						{
							int num14 = 840;
							component2.SetDropItemDictionary(num14, bonusChance: false);
							AddDropItemIdDictionary(num14);
						}
						int num15 = Random.Range(0, 100);
						Debug.Log("不思議な金属の剥片獲得判定：" + num15 + "/アイテム取得確率：" + (itemDropProbability / 3 + accessoryItemDiscover));
						if (itemDropProbability / 6 + accessoryItemDiscover >= num15)
						{
							int num16 = 590;
							component2.SetDropItemDictionary(num16, bonusChance: false);
							AddDropItemIdDictionary(num16);
						}
						int num17 = Random.Range(0, 100);
						Debug.Log("共通収集獲得判定：" + num17 + "/アイテム取得確率：" + (itemDropProbability / 2 + accessoryItemDiscover));
						if (itemDropProbability / 2 + accessoryItemDiscover >= num17)
						{
							int commonItem = component3.GetCommonItem("collect");
							component2.SetDropItemDictionary(commonItem, bonusChance: false);
							AddDropItemIdDictionary(commonItem);
						}
						int num18 = Random.Range(0, 100);
						Debug.Log("亡骸獲得判定：" + num18 + "/アイテム取得確率：" + ((float)itemDropProbability / 1.2f + (float)accessoryItemDiscover));
						if ((float)itemDropProbability / 1.2f + (float)accessoryItemDiscover >= (float)num18)
						{
							int num19 = Random.Range(0, dungeonMapData.getCashableItemID[component2.currentBorderNum].Length);
							int num20 = dungeonMapData.getCashableItemID[component2.currentBorderNum][num19];
							component2.SetDropItemDictionary(num20, bonusChance: false);
							AddDropItemIdDictionary(num20);
						}
						int num21 = Random.Range(0, 100);
						Debug.Log("亡骸獲得判定：" + num21 + "/アイテム取得確率：" + (itemDropProbability / 3 + accessoryItemDiscover));
						if (itemDropProbability / 3 + accessoryItemDiscover >= num21)
						{
							int num22 = Random.Range(0, dungeonMapData.getTreasureItemID[component2.currentBorderNum].Length);
							int num23 = dungeonMapData.getTreasureItemID[component2.currentBorderNum][num22];
							component2.SetDropItemDictionary(num23, bonusChance: false);
							AddDropItemIdDictionary(num23);
						}
						int num24 = Random.Range(0, 100);
						if (PlayerDungeonBorderGetItemManager.separatorInMostLargeFloorList.Contains(component2.currentBorderNum) && num24 < 30)
						{
							int inMostLargeFloorItemId = PlayerDungeonBorderGetItemManager.GetInMostLargeFloorItemId(component2.currentBorderNum, "battle");
							Debug.Log("区切りの最後の境界でアイテムを獲得／戦闘ID：" + inMostLargeFloorItemId + "／現在の境界：" + component2.currentBorderNum);
							component2.SetDropItemDictionary(inMostLargeFloorItemId, bonusChance: false);
						}
					}
					foreach (KeyValuePair<int, int> item in getDropItemIdDictionary)
					{
						Transform transform2 = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.itemImagePrefabGo, resultDialogManager.itemImageParentGo.transform);
						resultDialogManager.itemImageSpawnGoList.Add(transform2.gameObject);
						transform2.GetComponent<ParameterContainer>().SetInt("itemID", item.Key);
						transform2.transform.Find("Count Group/Count Text").GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
						sprite = PlayerInventoryDataAccess.GetItemSprite(item.Key);
						transform2.transform.Find("Mask/Image").GetComponent<Image>().sprite = sprite;
						transform2.GetComponent<ArborFSM>().SendTrigger("ResultIteminitialize");
					}
					if (PlayerNonSaveDataManager.preGetDropMagicMaterial.Count > 0)
					{
						component2.GetDropMagicMaterialDictionary();
						foreach (KeyValuePair<int, int> item2 in component2.getDropMagicMaterialDictionary)
						{
							component2.SetDropMagicMateterialDictionary(item2.Key, item2.Value);
							int key = item2.Key;
							Transform transform3 = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.itemImagePrefabGo, resultDialogManager.itemImageParentGo.transform);
							resultDialogManager.itemImageSpawnGoList.Add(transform3.gameObject);
							transform3.GetComponent<ParameterContainer>().SetInt("itemID", key);
							transform3.transform.Find("Count Group/Count Text").GetComponent<TextMeshProUGUI>().text = item2.Value.ToString();
							sprite = PlayerInventoryDataAccess.GetItemSprite(key);
							transform3.transform.Find("Mask/Image").GetComponent<Image>().sprite = sprite;
							transform3.GetComponent<ArborFSM>().SendTrigger("ResultIteminitialize");
						}
						Debug.Log("魔力片を戦利品に追加");
					}
					if (component2.selectCardBonusArray[component2.thisFloorActionNum] > 1)
					{
						num9 = GetExpBonus(num5);
						resultDialogManager.bonusExpGroupGo.SetActive(value: true);
					}
					else
					{
						resultDialogManager.bonusExpGroupGo.SetActive(value: false);
					}
					for (int m = 0; m < component2.consecutiveResultEnemyMember.Count; m++)
					{
						PlayerQuestDataManager.RefreshOrderedQuestEnemyCount(component2.consecutiveResultEnemyMember[m]);
					}
				}
				else if (component2.isBossRouteSelect)
				{
					resultDialogManager.SpawnDungeonBattleWinEffect();
					resultDialogManager.expText.text = num5.ToString();
					DungeonMapData dungeonData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
					int targetID = 0;
					int num25 = 0;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					if (dungeonData.deepDungeonQuestFlag != int.MaxValue)
					{
						flag2 = true;
					}
					if (flag2)
					{
						QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == dungeonData.deepDungeonQuestFlag);
						bool isOrdered = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == dungeonData.deepDungeonQuestFlag).isOrdered;
						bool isClear = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == dungeonData.deepDungeonQuestFlag).isClear;
						bool flag4 = false;
						for (int n = 0; n < questData.questStartFlagNameList.Count; n++)
						{
							if (!PlayerFlagDataManager.scenarioFlagDictionary[questData.questStartFlagNameList[n]])
							{
								flag4 = true;
								break;
							}
						}
						if (isOrdered)
						{
							num25 = 1;
							targetID = dungeonData.bossEnemyID[1];
							Debug.Log("深く潜れるクエストのボス／" + targetID);
						}
						else if (!flag4 && !isClear && dungeonData.isExtraQuestBossExist)
						{
							flag3 = true;
							targetID = dungeonData.extraQuestBossID;
							Debug.Log("特殊クエストのボス／" + targetID);
						}
						else if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag))
						{
							if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonData.deepDungeonFlag])
							{
								_ = component2.dungeonMaxFloorNum / 10;
								num25 = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName];
								targetID = dungeonData.bossEnemyID[num25];
								if (num25 == dungeonData.bossCount)
								{
									flag = true;
								}
								Debug.Log("倒したボスNum：" + num25 + "／ボスID：" + targetID + "／最奥ボス：" + flag);
							}
						}
						else
						{
							num25 = 0;
							targetID = dungeonData.bossEnemyID[0];
							Debug.Log("通常階層のボス／" + targetID);
						}
					}
					else if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag))
					{
						if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonData.deepDungeonFlag])
						{
							num25 = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName];
							targetID = dungeonData.bossEnemyID[num25];
							if (num25 == dungeonData.bossCount)
							{
								flag = true;
							}
							Debug.Log("倒したボスNum：" + num25 + "／ボスID：" + targetID + "／最奥ボス：" + flag);
						}
					}
					else
					{
						targetID = dungeonData.bossEnemyID[0];
					}
					List<int> list = new List<int>();
					if (flag3)
					{
						List<int> getExtraQuestBossItemID = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).getExtraQuestBossItemID;
						list.Add(getExtraQuestBossItemID[0]);
						list.Add(getExtraQuestBossItemID[1]);
					}
					else
					{
						List<int[]> getBossItemID = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName).getBossItemID;
						list.Add(getBossItemID[num25][0]);
						int num26 = Random.Range(1, 4);
						list.Add(getBossItemID[num25][num26]);
					}
					for (int num27 = 0; num27 < list.Count; num27++)
					{
						Debug.Log("ボス戦獲得アイテム／index：" + num27 + "／ID：" + list[num27]);
						Transform transform4 = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.itemImagePrefabGo, resultDialogManager.itemImageParentGo.transform);
						resultDialogManager.itemImageSpawnGoList.Add(transform4.gameObject);
						transform4.GetComponent<ParameterContainer>().SetInt("itemID", list[num27]);
						transform4.transform.Find("Count Group/Count Text").GetComponent<TextMeshProUGUI>().text = "1";
						sprite = PlayerInventoryDataAccess.GetItemSprite(list[num27]);
						transform4.transform.Find("Mask/Image").GetComponent<Image>().sprite = sprite;
						transform4.GetComponent<ArborFSM>().SendTrigger("ResultIteminitialize");
						component2.SetDropItemDictionary(list[num27], bonusChance: false);
					}
					if (flag && dungeonData.getDeepLastBossItemID != 999 && PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData data) => data.itemID == dungeonData.getDeepLastBossItemID) == null)
					{
						PlayerNonSaveDataManager.isDungeonGetRareItem = true;
						PlayerNonSaveDataManager.dungeonGetRareItemId = dungeonData.getDeepLastBossItemID;
						if (dungeonData.getDeepLastBossItemID < 950)
						{
							int itemSortID = component2.GetItemSortID(dungeonData.getDeepLastBossItemID);
							PlayerInventoryDataAccess.PlayerHaveEventItemAdd(dungeonData.getDeepLastBossItemID, itemSortID);
							ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == dungeonData.getDeepLastBossItemID);
							if (!string.IsNullOrEmpty(itemEventItemData.rewardRecipeName))
							{
								PlayerFlagDataManager.recipeFlagDictionary[itemEventItemData.rewardRecipeName] = true;
								PlayerNonSaveDataManager.isDungeonGetRareItemWithRecipe = true;
								Debug.Log("ダンジョン最奥でレシピ獲得");
								if (itemEventItemData.rewardRecipeName == "recipe3030")
								{
									PlayerFlagDataManager.enableNewCraftFlagDictionary[1120] = true;
								}
								if (itemEventItemData.rewardRecipeName == "recipe3040")
								{
									PlayerFlagDataManager.enableNewCraftFlagDictionary[2070] = true;
								}
							}
						}
						else
						{
							PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == dungeonData.getDeepLastBossItemID));
						}
						Debug.Log("最奥ボスアイテムを獲得／ID：" + dungeonData.getDeepLastBossItemID);
					}
					PlayerQuestDataManager.RefreshOrderedQuestEnemyCount(targetID);
					Debug.Log("ボスID：" + targetID);
				}
				else
				{
					resultDialogManager.expText.text = num5.ToString();
					List<int> getItemID = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName).getItemID;
					for (int num28 = 0; num28 < getItemID.Count; num28++)
					{
						Transform transform5 = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.itemImagePrefabGo, resultDialogManager.itemImageParentGo.transform);
						resultDialogManager.itemImageSpawnGoList.Add(transform5.gameObject);
						transform5.GetComponent<ParameterContainer>().SetInt("itemID", getItemID[num28]);
						transform5.transform.Find("Count Group/Count Text").GetComponent<TextMeshProUGUI>().text = "1";
						sprite = PlayerInventoryDataAccess.GetItemSprite(getItemID[num28]);
						transform5.transform.Find("Mask/Image").GetComponent<Image>().sprite = sprite;
						transform5.GetComponent<ArborFSM>().SendTrigger("ResultIteminitialize");
						component2.SetDropItemDictionary(getItemID[num28], bonusChance: false);
					}
				}
				component2.getDropMoney += num;
				PlayerDataManager.AddHaveMoney(num);
				resultDialogManager.bonusExpText.text = num9.ToString();
				for (int num29 = 0; num29 < PlayerStatusDataManager.playerPartyMember.Length; num29++)
				{
					PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[num29]] += num5;
					PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[num29]] += num9;
					PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[num29]] = Mathf.Clamp(PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[num29]], 0, 999999);
				}
			}
		}
		else
		{
			resultDialogManager.SpawnScenarioBattleWinEffect();
			Debug.Log("コマンド戦闘リザルト表示");
			PlayerDataManager.AddHaveMoney(num);
			List<int> itemList = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData item) => item.scenarioName == PlayerNonSaveDataManager.resultScenarioName).getItemID;
			int num30;
			for (num30 = 0; num30 < itemList.Count; num30++)
			{
				Transform transform6 = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.itemImagePrefabGo, resultDialogManager.itemImageParentGo.transform);
				resultDialogManager.itemImageSpawnGoList.Add(transform6.gameObject);
				transform6.GetComponent<ParameterContainer>().SetInt("itemID", itemList[num30]);
				transform6.transform.Find("Count Group/Count Text").GetComponent<TextMeshProUGUI>().text = "1";
				sprite = PlayerInventoryDataAccess.GetItemSprite(itemList[num30]);
				transform6.transform.Find("Mask/Image").GetComponent<Image>().sprite = sprite;
				transform6.GetComponent<ArborFSM>().SendTrigger("ResultIteminitialize");
				int itemSortID2 = PlayerInventoryDataAccess.GetItemSortID(itemList[num30]);
				if (itemSortID2 < 3000)
				{
					PlayerInventoryDataAccess.PlayerHaveItemAdd(itemList[num30], itemSortID2, 1);
					continue;
				}
				PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == itemList[num30]));
			}
			_ = PlayerNonSaveDataManager.resultScenarioName == "Shrine1_50BossBattle";
			resultDialogManager.bonusExpGroupGo.SetActive(value: false);
			resultDialogManager.expText.text = num5.ToString();
			for (int num31 = 0; num31 < PlayerStatusDataManager.playerPartyMember.Length; num31++)
			{
				PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[num31]] += num5;
			}
			for (int num32 = 0; num32 < PlayerStatusDataManager.enemyMember.Length; num32++)
			{
				PlayerQuestDataManager.RefreshOrderedQuestEnemyCount(PlayerStatusDataManager.enemyMember[num32]);
			}
		}
		resultDialogManager.SetDropItemGroupUiElements();
		resultDialogManager.goldText.text = num.ToString();
		PlayerInventoryDataAccess.HaveItemListSortAll();
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		resultDialogManager.isAutoCloseToggleInitialize = true;
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void ResultReset()
	{
		resultDialogManager.DespawnResultGetItem();
		resultDialogManager.characterImageSpawnGoList.Clear();
		resultDialogManager.itemImageSpawnGoList.Clear();
		resultDialogManager.autoCloseToggle.isOn = PlayerDataManager.isResultAutoClose;
		resultDialogManager.autoCloseGroupGo.SetActive(value: false);
	}

	private int GetExpBonus(int originExp)
	{
		DungeonMapManager component = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		float num = component.selectCardBonusArray[component.thisFloorActionNum];
		num /= 10f;
		int result = (int)((float)originExp * num);
		Debug.Log("ボーナスEXP：" + result + "／ボーナス倍率：" + num);
		return result;
	}

	private void AddDropItemIdDictionary(int itemId)
	{
		DungeonMapManager component = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		if (getDropItemIdDictionary.ContainsKey(itemId))
		{
			getDropItemIdDictionary[itemId]++;
			getDropItemIdDictionary[itemId] += component.getMaterialBonusNum;
			Debug.Log("ドロップアイテム同じKeyがある／Key：" + itemId + "／数量：" + getDropItemIdDictionary[itemId] + "／素材ボーナス：" + component.getMaterialBonusNum);
		}
		else
		{
			getDropItemIdDictionary.Add(itemId, 1);
			getDropItemIdDictionary[itemId] += component.getMaterialBonusNum;
			Debug.Log("ドロップアイテム同じKeyはない／Key：" + itemId + "／素材ボーナス：" + component.getMaterialBonusNum);
		}
	}
}
