using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CreateDungeonItemInfoPrefab : StateBehaviour
{
	private DungeonItemInfoManager dungeonItemInfoManager;

	private ParameterContainer parameterContainer;

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
		dungeonItemInfoManager.dungeonInfoWindow.SetActive(value: true);
		dungeonItemInfoManager.DespawnScrollPrefabGo();
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		DungeonGetItemInfoData dungeonGetItemInfoData = GameDataManager.instance.dungeonItemInfoDataBase.dungeonGetItemInfoDataList.Find((DungeonGetItemInfoData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		for (int i = 0; i < dungeonItemInfoManager.dungeonVisibleBorderFloorCount; i++)
		{
			Transform transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorDataPrefabGo, dungeonItemInfoManager.dungeonInfoContentGo.transform);
			dungeonItemInfoManager.spawnedFloorPrefabGoList.Add(transform);
			parameterContainer = transform.GetComponent<ParameterContainer>();
			if (dungeonMapData.isTowerDungeon)
			{
				parameterContainer.GetVariableList<UguiTextVariable>("floorNumTextList")[0].text.gameObject.SetActive(value: false);
			}
			else
			{
				parameterContainer.GetVariableList<UguiTextVariable>("floorNumTextList")[0].text.gameObject.SetActive(value: true);
			}
			parameterContainer.GetVariableList<UguiTextVariable>("floorNumTextList")[1].text.text = dungeonMapData.dungeonBorderFloor[i].ToString();
			if (i == dungeonItemInfoManager.dungeonVisibleBorderFloorCount - 1)
			{
				parameterContainer.GetVariableList<UguiTextVariable>("floorNumTextList")[2].text.text = dungeonItemInfoManager.dungeonMaxFloorNum.ToString();
			}
			else
			{
				int num = dungeonMapData.dungeonBorderFloor[i + 1];
				parameterContainer.GetVariableList<UguiTextVariable>("floorNumTextList")[2].text.text = (num - 1).ToString();
			}
			dungeonItemInfoManager.collectGetItemIdList.Clear();
			dungeonItemInfoManager.battleGetItemIdList.Clear();
			dungeonItemInfoManager.mergeGetItemIdDictionary.Clear();
			for (int j = 0; j < 9; j++)
			{
				int num2 = dungeonGetItemInfoData.normalEnemyDropItemInfoTable[i][j];
				if (num2 != 0 && !dungeonItemInfoManager.battleGetItemIdList.Contains(num2))
				{
					dungeonItemInfoManager.battleGetItemIdList.Add(num2);
				}
			}
			for (int k = 0; k < 9; k++)
			{
				int num3 = dungeonGetItemInfoData.hardEnemyDropItemInfoTable[i][k];
				if (num3 != 0 && !dungeonItemInfoManager.battleGetItemIdList.Contains(num3))
				{
					dungeonItemInfoManager.battleGetItemIdList.Add(num3);
				}
			}
			for (int l = 0; l < 10; l++)
			{
				int num4 = dungeonGetItemInfoData.collectCommonMaterialItemInfoTable[i][l];
				if (num4 != 0 && !dungeonItemInfoManager.collectGetItemIdList.Contains(num4))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(num4);
				}
			}
			for (int m = 0; m < 10; m++)
			{
				int num5 = dungeonGetItemInfoData.collectCommonItemInfoTable[i][m];
				if (num5 != 0 && !dungeonItemInfoManager.collectGetItemIdList.Contains(num5))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(num5);
				}
			}
			for (int n = 0; n < 10; n++)
			{
				int num6 = dungeonGetItemInfoData.corpseCommonItemInfoTable[i][n];
				if (num6 != 0 && !dungeonItemInfoManager.collectGetItemIdList.Contains(num6))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(num6);
				}
			}
			for (int num7 = 0; num7 < 10; num7++)
			{
				int item = dungeonGetItemInfoData.treasureCommonItemInfoTable[i][num7];
				if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(item);
				}
			}
			for (int num8 = 0; num8 < 10; num8++)
			{
				int item2 = dungeonGetItemInfoData.treasureCommonItemInfoTable[i][num8];
				if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item2))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(item2);
				}
			}
			for (int num9 = 0; num9 < 3; num9++)
			{
				int item3 = dungeonGetItemInfoData.getTreasureItemInfoTable[i][num9];
				if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item3))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(item3);
				}
			}
			for (int num10 = 0; num10 < 3; num10++)
			{
				int item4 = dungeonGetItemInfoData.getCollectItemInfoTable[i][num10];
				if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item4))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(item4);
				}
			}
			for (int num11 = 0; num11 < 3; num11++)
			{
				int item5 = dungeonGetItemInfoData.getCashableItemInfoTable[i][num11];
				if (!dungeonItemInfoManager.collectGetItemIdList.Contains(item5))
				{
					dungeonItemInfoManager.collectGetItemIdList.Add(item5);
				}
			}
			for (int num12 = 0; num12 < dungeonItemInfoManager.collectGetItemIdList.Count; num12++)
			{
				if (dungeonItemInfoManager.battleGetItemIdList.Contains(dungeonItemInfoManager.collectGetItemIdList[num12]))
				{
					dungeonItemInfoManager.battleGetItemIdList.Remove(dungeonItemInfoManager.collectGetItemIdList[num12]);
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
					GameObject gameObject5 = parameterContainer.GetGameObjectList("itemCategoryParentGoList")[0];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject5.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					SetTextColor(transform, isBattle);
					break;
				}
				case "monsterMaterial":
				{
					GameObject gameObject4 = parameterContainer.GetGameObjectList("itemCategoryParentGoList")[1];
					if (itemId < 350 && !isBattle)
					{
						string text = itemId.ToString();
						char c = text[1];
						if (text[2].ToString() == "0")
						{
							transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
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
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
										char c2 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c2;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
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
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
										char c3 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c3;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
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
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
										char c4 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c4;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
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
										transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
										char c5 = itemId.ToString()[1];
										transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c5;
										SetTextColor(transform, isBattle);
									}
								}
								else
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
									transform.GetComponent<Localize>().Term = itemCategory + itemId;
									SetTextColor(transform, isBattle);
								}
							}
							else if (list.Contains(340) && list.Contains(341) && list.Contains(342) && list.Contains(343))
							{
								if (itemId == 340)
								{
									transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
									char c6 = itemId.ToString()[1];
									transform.GetComponent<Localize>().Term = "enemyMaterialSeries" + c6;
									SetTextColor(transform, isBattle);
								}
							}
							else
							{
								transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
								transform.GetComponent<Localize>().Term = itemCategory + itemId;
								SetTextColor(transform, isBattle);
							}
						}
						else
						{
							transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
							transform.GetComponent<Localize>().Term = itemCategory + itemId;
							SetTextColor(transform, isBattle);
						}
					}
					else
					{
						transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject4.transform);
						transform.GetComponent<Localize>().Term = itemCategory + itemId;
						SetTextColor(transform, isBattle);
					}
					break;
				}
				case "natureMaterial":
				{
					GameObject gameObject3 = parameterContainer.GetGameObjectList("itemCategoryParentGoList")[2];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject3.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					SetTextColor(transform, isBattle);
					break;
				}
				case "mysticMaterial":
				case "wonderMaterialParts":
				{
					GameObject gameObject2 = parameterContainer.GetGameObjectList("itemCategoryParentGoList")[3];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject2.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					SetTextColor(transform, isBattle);
					break;
				}
				case "stoneMaterial":
				{
					GameObject gameObject = parameterContainer.GetGameObjectList("itemCategoryParentGoList")[4];
					transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject.transform);
					transform.GetComponent<Localize>().Term = itemCategory + itemId;
					SetTextColor(transform, isBattle);
					break;
				}
				}
			}
			else if (itemId < 950 && itemId <= 840)
			{
				GameObject gameObject6 = parameterContainer.GetGameObjectList("itemCategoryParentGoList")[5];
				transform = PoolManager.Pools["totalMapPool"].Spawn(dungeonItemInfoManager.dungeonFloorTextPrefabGo, gameObject6.transform);
				string itemCategory2 = PlayerInventoryDataAccess.GetItemCategory(itemId);
				transform.GetComponent<Localize>().Term = itemCategory2 + itemId;
				SetTextColor(transform, isBattle);
			}
		}
		dungeonItemInfoManager.spawnedTextPrefabGoList.Add(transform);
	}

	private void SetTextColor(Transform textGo, bool isBattle)
	{
		if (isBattle)
		{
			textGo.GetComponent<Text>().color = Color.yellow;
		}
		else
		{
			textGo.GetComponent<Text>().color = Color.white;
		}
	}
}
