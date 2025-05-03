using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopRankManagerForPM : MonoBehaviour
{
	public bool CheckShopRankChange()
	{
		bool result = false;
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
					if (scenarioSortData[i].rankLevel > PlayerDataManager.currentShopRankFirstNum)
					{
						result = true;
					}
					PlayerNonSaveDataManager.shopRankTempLvArray[0] = scenarioSortData[i].rankLevel;
					break;
				}
				continue;
			}
			if (scenarioSortData[i].rankLevel > PlayerDataManager.currentShopRankFirstNum)
			{
				result = true;
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
				if (list[k].rankLevel > PlayerDataManager.currentShopRankSecondNum)
				{
					result = true;
				}
				PlayerNonSaveDataManager.shopRankTempLvArray[1] = list[k].rankLevel;
				break;
			}
		}
		return result;
	}

	public void OpenShopRankChangeDialog()
	{
		HeaderStatusManager component = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		string rankNameTerm = GameDataManager.instance.shopRankDataBase.shopRankDataList.Find((ShopRankData data) => data.rankLevel == PlayerNonSaveDataManager.shopRankTempLvArray[0] && data.rankType == ShopRankData.RankType.scenario).rankNameTerm;
		string rankNameTerm2 = GameDataManager.instance.shopRankDataBase.shopRankDataList.Find((ShopRankData data) => data.rankLevel == PlayerNonSaveDataManager.shopRankTempLvArray[1] && data.rankType == ShopRankData.RankType.sales).rankNameTerm;
		component.shopRankDialogTextLoc1.Term = rankNameTerm;
		component.shopRankDialogTextLoc2.Term = rankNameTerm2;
		PlayerDataManager.currentShopRankFirstNum = PlayerNonSaveDataManager.shopRankTempLvArray[0];
		PlayerDataManager.currentShopRankSecondNum = PlayerNonSaveDataManager.shopRankTempLvArray[1];
		PlayerNonSaveDataManager.isShopRankChangeFromNotice = true;
		component.shopRankDialogCanvasGo.SetActive(value: true);
		component.SpawnShopRankChangeEffect();
	}
}
