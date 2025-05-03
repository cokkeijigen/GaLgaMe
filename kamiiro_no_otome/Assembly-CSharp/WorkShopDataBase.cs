using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/WorkShop Data Base")]
public class WorkShopDataBase : SerializedScriptableObject
{
	public class WorkShopData
	{
		public int[][] factorAutoGetProbability;

		public int[] facilityLvUpCostMoney;

		public int[] oneTimeCraftLimitNum;

		public List<NeedMoneyAndItem> craftToolLvUpCost;

		public List<NeedMoneyAndItem> furnanceLvUpCost;

		public NeedMoneyAndItem addOnFacilityCost;
	}

	public class NeedMoneyAndItem
	{
		public int[][] needCost;
	}

	public List<WorkShopData> workShopDataList;
}
