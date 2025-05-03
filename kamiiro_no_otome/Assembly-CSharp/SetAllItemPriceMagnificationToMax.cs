using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetAllItemPriceMagnificationToMax : StateBehaviour
{
	private int sellPriceMinLimit;

	private int sellPriceMaxLimit;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		List<ShopRankData> list = (from data in GameDataManager.instance.shopRankDataBase.shopRankDataList.Where((ShopRankData data) => data.rankType == ShopRankData.RankType.sales).ToList()
			orderby data.sortID descending
			select data).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].needSalesAmount <= PlayerDataManager.totalSalesAmount)
			{
				sellPriceMinLimit = list[i].sellPriceMinLimit;
				sellPriceMaxLimit = list[i].sellPriceMaxLimit;
				Debug.Log("現在額：" + PlayerDataManager.totalSalesAmount + "／必要額：" + list[i].needSalesAmount + "／売価上限：" + sellPriceMaxLimit);
				break;
			}
		}
		for (int j = 0; j < PlayerInventoryDataManager.haveWeaponList.Count; j++)
		{
			PlayerInventoryDataManager.haveWeaponList[j].sellPriceMagnification = sellPriceMaxLimit;
		}
		for (int k = 0; k < PlayerInventoryDataManager.haveArmorList.Count; k++)
		{
			PlayerInventoryDataManager.haveArmorList[k].sellPriceMagnification = sellPriceMaxLimit;
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
}
