using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerDungeonBorderGetItemManager : SerializedMonoBehaviour
{
	public static List<List<int>> separatorCollectGetItemIdList = new List<List<int>>();

	public static List<List<int>> separatorCorpseGetItemIdList = new List<List<int>>();

	public static List<List<int>> separatorBattleGetItemIdList = new List<List<int>>();

	public static List<int> separatorInMostLargeFloorList = new List<int>();

	public static void InitializeAllIdList()
	{
		separatorCollectGetItemIdList.Clear();
		separatorCorpseGetItemIdList.Clear();
		separatorBattleGetItemIdList.Clear();
	}

	public static void AddCollectGetItemIdList(List<int> idList)
	{
		List<int> list = new List<int>(idList);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (list[num] < 100)
			{
				list.RemoveAt(num);
			}
			else if (list[num] > 590)
			{
				list.RemoveAt(num);
			}
		}
		list.Sort();
		separatorCollectGetItemIdList.Add(list);
	}

	public static void AddCorpseGetItemIdList(List<int> idList)
	{
		List<int> list = new List<int>(idList);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (list[num] < 100)
			{
				list.RemoveAt(num);
			}
			else if (list[num] > 590)
			{
				list.RemoveAt(num);
			}
		}
		list.Sort();
		separatorCorpseGetItemIdList.Add(list);
	}

	public static void AddBattleGetItemIdList(List<int> idList)
	{
		List<int> list = new List<int>(idList);
		list.Sort();
		separatorBattleGetItemIdList.Add(list);
	}

	public static void AddMostLargeFloorList(int minBorderNum)
	{
		separatorInMostLargeFloorList.Add(minBorderNum);
	}

	public static int GetInMostLargeFloorItemId(int borderNum, string type)
	{
		int result = 0;
		switch (type)
		{
		case "collect":
		{
			int num2 = 0;
			if (Random.Range(0, 100) < 60)
			{
				int index5 = separatorInMostLargeFloorList.FindIndex((int data) => data == borderNum);
				int index6 = Random.Range(0, separatorCollectGetItemIdList[index5].Count);
				num2 = separatorCollectGetItemIdList[index5][index6];
			}
			else
			{
				DungeonMapData dungeonMapData2 = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
				DungeonMapManager component3 = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
				DungeonGetItemManager component4 = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
				int id2 = dungeonMapData2.collectCommonItemTable[component3.currentBorderNum];
				List<int> itemDataIdList2 = component4.dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id2).itemDataIdList;
				num2 = component4.GetDontHaveItemFromCommonItemList(itemDataIdList2);
			}
			result = num2;
			break;
		}
		case "corpse":
		{
			int num = 0;
			if (Random.Range(0, 100) < 60)
			{
				int index3 = separatorInMostLargeFloorList.FindIndex((int data) => data == borderNum);
				int index4 = Random.Range(0, separatorCorpseGetItemIdList[index3].Count);
				num = separatorCorpseGetItemIdList[index3][index4];
			}
			else
			{
				DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
				DungeonMapManager component = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
				DungeonGetItemManager component2 = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
				int id = dungeonMapData.corpseCommonItemTable[component.currentBorderNum];
				List<int> itemDataIdList = component2.dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id).itemDataIdList;
				num = component2.GetDontHaveItemFromCommonItemList(itemDataIdList);
			}
			result = num;
			break;
		}
		case "battle":
		{
			int index = separatorInMostLargeFloorList.FindIndex((int data) => data == borderNum);
			int index2 = Random.Range(0, separatorBattleGetItemIdList[index].Count);
			result = separatorBattleGetItemIdList[index][index2];
			break;
		}
		}
		return result;
	}
}
