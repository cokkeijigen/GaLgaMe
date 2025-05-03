using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerShopRankDataManager : SerializedBehaviour
{
	public static string[] GetShopRankNameArray()
	{
		string[] array = new string[2];
		bool flag = false;
		List<ShopRankData> source = GameDataManager.instance.shopRankDataBase.shopRankDataList.Where((ShopRankData data) => data.rankType == ShopRankData.RankType.scenario).ToList();
		List<ShopRankData> scenarioSortData = source.OrderByDescending((ShopRankData data) => data.sortID).ToList();
		int i;
		for (i = 0; i < scenarioSortData.Count; i++)
		{
			for (int j = 0; j < scenarioSortData[i].needScenarioFlagList.Count; j++)
			{
				if (!PlayerFlagDataManager.scenarioFlagDictionary[scenarioSortData[i].needScenarioFlagList[j]])
				{
					Debug.Log(scenarioSortData[i].needScenarioFlagList[j] + "は未クリア");
					flag = true;
				}
			}
			if (flag)
			{
				flag = false;
				continue;
			}
			if (i == 0)
			{
				HaveEventItemData haveEventItemData = PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData data) => data.itemID == scenarioSortData[i].needEventItem);
				if (PlayerDataManager.totalSalesAmount >= scenarioSortData[i].needSalesAmount && haveEventItemData != null)
				{
					array[0] = scenarioSortData[i].rankNameTerm;
					if (scenarioSortData[i].rankLevel > PlayerDataManager.currentShopRankFirstNum)
					{
						PlayerNonSaveDataManager.isShopRankChange = true;
					}
					PlayerNonSaveDataManager.shopRankTempLvArray[0] = scenarioSortData[i].rankLevel;
					break;
				}
				continue;
			}
			array[0] = scenarioSortData[i].rankNameTerm;
			if (scenarioSortData[i].rankLevel > PlayerDataManager.currentShopRankFirstNum)
			{
				PlayerNonSaveDataManager.isShopRankChange = true;
			}
			PlayerNonSaveDataManager.shopRankTempLvArray[0] = scenarioSortData[i].rankLevel;
			break;
		}
		List<ShopRankData> list = (from data in GameDataManager.instance.shopRankDataBase.shopRankDataList.Where((ShopRankData data) => data.rankType == ShopRankData.RankType.sales).ToList()
			orderby data.sortID descending
			select data).ToList();
		for (int k = 0; k < list.Count; k++)
		{
			if (list[k].needSalesAmount <= PlayerDataManager.totalSalesAmount)
			{
				array[1] = list[k].rankNameTerm;
				break;
			}
		}
		return array;
	}
}
