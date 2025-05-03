using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Shop Rank Data")]
public class ShopRankData : SerializedScriptableObject
{
	public enum RankType
	{
		scenario,
		sales
	}

	public int sortID;

	public int rankLevel;

	public RankType rankType;

	public List<string> needScenarioFlagList = new List<string>();

	public int needSalesAmount;

	public int needEventItem;

	public string rankNameTerm;

	public int sellPriceMinLimit;

	public int sellPriceMaxLimit;
}
