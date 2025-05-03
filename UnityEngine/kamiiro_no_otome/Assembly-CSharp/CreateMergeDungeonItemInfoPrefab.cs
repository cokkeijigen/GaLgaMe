using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CreateMergeDungeonItemInfoPrefab : StateBehaviour
{
	private DungeonItemInfoManager dungeonItemInfoManager;

	private ParameterContainer spawnGoParam;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonItemInfoManager = GameObject.Find("Dungeon Item Info Manager").GetComponent<DungeonItemInfoManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.isDungeonGetItemHighlight = false;
		dungeonItemInfoManager.dungeonInfoWindow.SetActive(value: true);
		dungeonItemInfoManager.DespawnScrollPrefabGo();
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		DungeonGetItemInfoData dungeonGetItemInfoData = GameDataManager.instance.dungeonItemInfoDataBase.dungeonGetItemInfoDataList.Find((DungeonGetItemInfoData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		int num = 0;
		PlayerDungeonBorderGetItemManager.InitializeAllIdList();
		if (dungeonMapData.dungeonName == "Dungeon4" && PlayerFlagDataManager.scenarioFlagDictionary[dungeonMapData.deepDungeonFlag])
		{
			PlayerNonSaveDataManager.isDungeonGetItemHighlight = true;
		}
		for (int i = 0; i < dungeonItemInfoManager.dungeonFloorListByTenLevels.Count; i++)
		{
			Transform transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorDataPrefabGo, dungeonItemInfoManager.dungeonInfoContentGo.transform);
			dungeonItemInfoManager.spawnedFloorPrefabGoList.Add(transform);
			spawnGoParam = transform.GetComponent<ParameterContainer>();
			if (dungeonMapData.isTowerDungeon)
			{
				spawnGoParam.GetVariableList<UguiTextVariable>("floorNumTextList")[0].text.gameObject.SetActive(value: false);
			}
			else
			{
				spawnGoParam.GetVariableList<UguiTextVariable>("floorNumTextList")[0].text.gameObject.SetActive(value: true);
			}
			spawnGoParam.GetVariableList<UguiTextVariable>("floorNumTextList")[1].text.text = dungeonItemInfoManager.dungeonFloorListByMinLevels[i].ToString();
			spawnGoParam.GetVariableList<UguiTextVariable>("floorNumTextList")[2].text.text = dungeonItemInfoManager.dungeonFloorListByMaxLevels[i].ToString();
			dungeonItemInfoManager.collectGetItemIdList.Clear();
			dungeonItemInfoManager.battleGetItemIdList.Clear();
			dungeonItemInfoManager.mergeGetItemIdDictionary.Clear();
			dungeonItemInfoManager.collectAllGetItemIdList.Clear();
			dungeonItemInfoManager.corpseAllGetItemIdList.Clear();
			for (int j = 0; j < dungeonItemInfoManager.dungeonFloorListByTenLevels[i]; j++)
			{
				for (int k = 0; k < 9; k++)
				{
					int num2 = dungeonGetItemInfoData.normalEnemyDropItemInfoTable[num][k];
					if (num2 != 0 && !dungeonItemInfoManager.battleGetItemIdList.Contains(num2))
					{
						dungeonItemInfoManager.battleGetItemIdList.Add(num2);
					}
				}
				for (int l = 0; l < 9; l++)
				{
					int num3 = dungeonGetItemInfoData.hardEnemyDropItemInfoTable[num][l];
					if (num3 != 0 && !dungeonItemInfoManager.battleGetItemIdList.Contains(num3))
					{
						dungeonItemInfoManager.battleGetItemIdList.Add(num3);
					}
				}
				for (int m = 0; m < 10; m++)
				{
					int num4 = dungeonGetItemInfoData.collectCommonMaterialItemInfoTable[num][m];
					if (num4 != 0)
					{
						if (!dungeonItemInfoManager.collectGetItemIdList.Contains(num4))
						{
							dungeonItemInfoManager.collectGetItemIdList.Add(num4);
						}
						if (!dungeonItemInfoManager.collectAllGetItemIdList.Contains(num4))
						{
							dungeonItemInfoManager.collectAllGetItemIdList.Add(num4);
						}
					}
				}
				for (int n = 0; n < 10; n++)
				{
					int num5 = dungeonGetItemInfoData.collectCommonItemInfoTable[num][n];
					if (num5 != 0)
					{
						if (!dungeonItemInfoManager.collectGetItemIdList.Contains(num5))
						{
							dungeonItemInfoManager.collectGetItemIdList.Add(num5);
						}
						if (!dungeonItemInfoManager.collectAllGetItemIdList.Contains(num5))
						{
							dungeonItemInfoManager.collectAllGetItemIdList.Add(num5);
						}
					}
				}
				for (int num6 = 0; num6 < 10; num6++)
				{
					int num7 = dungeonGetItemInfoData.corpseCommonItemInfoTable[num][num6];
					if (num7 != 0)
					{
						if (!dungeonItemInfoManager.collectGetItemIdList.Contains(num7))
						{
							dungeonItemInfoManager.collectGetItemIdList.Add(num7);
						}
						if (!dungeonItemInfoManager.corpseAllGetItemIdList.Contains(num7))
						{
							dungeonItemInfoManager.corpseAllGetItemIdList.Add(num7);
						}
					}
				}
				for (int num8 = 0; num8 < 10; num8++)
				{
					int item = dungeonGetItemInfoData.treasureCommonItemInfoTable[num][num8];
					if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item))
					{
						dungeonItemInfoManager.collectGetItemIdList.Add(item);
					}
				}
				for (int num9 = 0; num9 < 10; num9++)
				{
					int item2 = dungeonGetItemInfoData.treasureCommonItemInfoTable[num][num9];
					if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item2))
					{
						dungeonItemInfoManager.collectGetItemIdList.Add(item2);
					}
				}
				for (int num10 = 0; num10 < 3; num10++)
				{
					int item3 = dungeonGetItemInfoData.getTreasureItemInfoTable[num][num10];
					if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item3))
					{
						dungeonItemInfoManager.collectGetItemIdList.Add(item3);
					}
				}
				for (int num11 = 0; num11 < 3; num11++)
				{
					int item4 = dungeonGetItemInfoData.getCollectItemInfoTable[num][num11];
					if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item4))
					{
						dungeonItemInfoManager.collectGetItemIdList.Add(item4);
					}
					if (!dungeonItemInfoManager.collectAllGetItemIdList.Contains(item4))
					{
						dungeonItemInfoManager.collectAllGetItemIdList.Add(item4);
					}
				}
				for (int num12 = 0; num12 < 3; num12++)
				{
					int item5 = dungeonGetItemInfoData.getCashableItemInfoTable[num][num12];
					if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item5))
					{
						dungeonItemInfoManager.collectGetItemIdList.Add(item5);
					}
				}
				num++;
			}
			PlayerDungeonBorderGetItemManager.AddCollectGetItemIdList(dungeonItemInfoManager.collectAllGetItemIdList);
			PlayerDungeonBorderGetItemManager.AddCorpseGetItemIdList(dungeonItemInfoManager.corpseAllGetItemIdList);
			PlayerDungeonBorderGetItemManager.AddBattleGetItemIdList(dungeonItemInfoManager.battleGetItemIdList);
			for (int num13 = 0; num13 < dungeonItemInfoManager.collectGetItemIdList.Count; num13++)
			{
				if (dungeonItemInfoManager.battleGetItemIdList.Contains(dungeonItemInfoManager.collectGetItemIdList[num13]))
				{
					dungeonItemInfoManager.battleGetItemIdList.Remove(dungeonItemInfoManager.collectGetItemIdList[num13]);
				}
			}
			foreach (int collectGetItemId in dungeonItemInfoManager.collectGetItemIdList)
			{
				dungeonItemInfoManager.mergeGetItemIdDictionary.Add(collectGetItemId, value: false);
			}
			foreach (int battleGetItemId in dungeonItemInfoManager.battleGetItemIdList)
			{
				dungeonItemInfoManager.mergeGetItemIdDictionary.Add(battleGetItemId, value: true);
			}
			foreach (KeyValuePair<int, bool> item6 in dungeonItemInfoManager.mergeGetItemIdDictionary)
			{
				SpawnItemNameText(item6.Key, item6.Value);
			}
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

	private void SpawnItemNameText(int itemId, bool isBattle)
	{
		Transform transform = null;
		List<int> list = new List<int>();
		if (itemId >= 100)
		{
			if (itemId < 600)
			{
				string itemCategory = PlayerInventoryDataAccess.GetItemCategory(itemId);
				switch (itemCategory)
				{
				case "fuelMaterial":
				case "crystal":
				case "metalMaterial":
				{
					GameObject gameObject4 = spawnGoParam.GetGameObjectList("itemCategoryParentGoList")[0];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					if (PlayerNonSaveDataManager.isDungeonGetItemHighlight)
					{
						if (itemId == 202 || itemId == 150)
						{
							SetTextColor(transform, isBattle, isHilight: true);
						}
						else
						{
							SetTextColor(transform, isBattle);
						}
					}
					else
					{
						SetTextColor(transform, isBattle);
					}
					break;
				}
				case "monsterMaterial":
				{
					GameObject gameObject5 = spawnGoParam.GetGameObjectList("itemCategoryParentGoList")[1];
					if (itemId < 350 && !isBattle)
					{
						string text = itemId.ToString();
						char c = text[1];
						if (text[2].ToString() == "0")
						{
							transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
							transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c;
							SetTextColor(transform, isBattle);
						}
					}
					else if (isBattle)
					{
						list = dungeonItemInfoManager.battleGetItemIdList;
						if (itemId < 350)
						{
							if (itemId < 310)
							{
								if (list.Contains(300) && list.Contains(301) && list.Contains(302) && list.Contains(303))
								{
									if (itemId == 300)
									{
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
										char c2 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c2;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
									transform.GetComponent<Localize>().Term = itemCategory + itemId;
									SetTextColor(transform, isBattle);
								}
							}
							else if (itemId < 320)
							{
								if (list.Contains(310) && list.Contains(311) && list.Contains(312) && list.Contains(313))
								{
									if (itemId == 310)
									{
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
										char c3 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c3;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
									transform.GetComponent<Localize>().Term = itemCategory + itemId;
									SetTextColor(transform, isBattle);
								}
							}
							else if (itemId < 330)
							{
								if (list.Contains(320) && list.Contains(321) && list.Contains(322) && list.Contains(323))
								{
									if (itemId == 320)
									{
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
										char c4 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c4;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
									transform.GetComponent<Localize>().Term = itemCategory + itemId;
									SetTextColor(transform, isBattle);
								}
							}
							else if (itemId < 340)
							{
								if (list.Contains(330) && list.Contains(331) && list.Contains(332) && list.Contains(333))
								{
									if (itemId == 330)
									{
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
										char c5 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c5;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
									transform.GetComponent<Localize>().Term = itemCategory + itemId;
									SetTextColor(transform, isBattle);
								}
							}
							else if (list.Contains(340) && list.Contains(341) && list.Contains(342) && list.Contains(343))
							{
								if (itemId == 340)
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
									char c6 = itemId.ToString()[1];
									transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c6;
									SetTextColor(transform, isBattle);
								}
							}
							else
							{
								transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
								transform.GetComponent<Localize>().Term = itemCategory + itemId;
								SetTextColor(transform, isBattle);
							}
						}
						else
						{
							transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
							transform.GetComponent<Localize>().Term = itemCategory + itemId;
							SetTextColor(transform, isBattle);
						}
					}
					else
					{
						transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
						transform.GetComponent<Localize>().Term = itemCategory + itemId;
						SetTextColor(transform, isBattle);
					}
					break;
				}
				case "natureMaterial":
				{
					GameObject gameObject3 = spawnGoParam.GetGameObjectList("itemCategoryParentGoList")[2];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject3.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					if (PlayerNonSaveDataManager.isDungeonGetItemHighlight)
					{
						if (itemId == 421)
						{
							SetTextColor(transform, isBattle, isHilight: true);
						}
						else
						{
							SetTextColor(transform, isBattle);
						}
					}
					else
					{
						SetTextColor(transform, isBattle);
					}
					break;
				}
				case "mysticMaterial":
				case "wonderMaterialParts":
				{
					GameObject gameObject2 = spawnGoParam.GetGameObjectList("itemCategoryParentGoList")[3];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject2.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					if (PlayerNonSaveDataManager.isDungeonGetItemHighlight)
					{
						if (itemId == 480)
						{
							SetTextColor(transform, isBattle, isHilight: true);
						}
						else
						{
							SetTextColor(transform, isBattle);
						}
					}
					else
					{
						SetTextColor(transform, isBattle);
					}
					break;
				}
				case "stoneMaterial":
				{
					GameObject gameObject = spawnGoParam.GetGameObjectList("itemCategoryParentGoList")[4];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					SetTextColor(transform, isBattle);
					break;
				}
				}
			}
			else if (itemId < 950 && itemId <= 840)
			{
				GameObject gameObject6 = spawnGoParam.GetGameObjectList("itemCategoryParentGoList")[5];
				transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject6.transform);
				string itemCategory2 = PlayerInventoryDataAccess.GetItemCategory(itemId);
				transform.GetComponent<Localize>().Term = itemCategory2 + itemId;
				SetTextColor(transform, isBattle);
			}
		}
		dungeonItemInfoManager.spawnedTextPrefabGoList.Add(transform);
	}

	private void SetTextColor(Transform textGo, bool isBattle, bool isHilight = false)
	{
		if (isBattle)
		{
			textGo.GetComponent<Text>().color = Color.yellow;
		}
		else if (isHilight)
		{
			textGo.GetComponent<Text>().color = new Color(0f, 0.8f, 1f);
		}
		else
		{
			textGo.GetComponent<Text>().color = Color.white;
		}
	}
}
