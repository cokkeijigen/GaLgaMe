using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetShopRankName : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private CarriageStoreNoticeManager carriageStoreNoticeManager;

	public StateLink shopRankCnangeLink;

	public StateLink stateLink;

	public bool isCheckShopRank;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		carriageStoreNoticeManager = GameObject.Find("Store Tending Manager").GetComponent<CarriageStoreNoticeManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.isShopRankChange = false;
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
					headerStatusManager.shopRankTextLoc1.Term = scenarioSortData[i].rankNameTerm;
					if (scenarioSortData[i].rankLevel > PlayerDataManager.currentShopRankFirstNum)
					{
						PlayerNonSaveDataManager.isShopRankChange = true;
					}
					PlayerNonSaveDataManager.shopRankTempLvArray[0] = scenarioSortData[i].rankLevel;
					break;
				}
				continue;
			}
			headerStatusManager.shopRankTextLoc1.Term = scenarioSortData[i].rankNameTerm;
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
				headerStatusManager.shopRankTextLoc2.Term = list[k].rankNameTerm;
				if (list[k].rankLevel > PlayerDataManager.currentShopRankSecondNum)
				{
					PlayerNonSaveDataManager.isShopRankChange = true;
				}
				PlayerNonSaveDataManager.shopRankTempLvArray[1] = list[k].rankLevel;
				break;
			}
		}
		Debug.Log("称号の設定完了");
		if (PlayerNonSaveDataManager.isShopRankChange && PlayerNonSaveDataManager.isCheckShopRankChange && isCheckShopRank)
		{
			PlayerNonSaveDataManager.isCheckShopRankChange = false;
			Transition(shopRankCnangeLink);
			return;
		}
		if (PlayerNonSaveDataManager.isCheckShopRankChange)
		{
			PlayerNonSaveDataManager.isCheckShopRankChange = false;
			carriageStoreNoticeManager.CheckCarriageEvent();
			return;
		}
		totalMapAccessManager.mapCanvasGroupArray[0].interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = true;
		worldMapAccessManager.SetWorldMapCameraDragEnable(value: true);
		localMapAccessManager.localMapExitFSM.enabled = true;
		PlayerNonSaveDataManager.isCheckShopRankChange = false;
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
}
