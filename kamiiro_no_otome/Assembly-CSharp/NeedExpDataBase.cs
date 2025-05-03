using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/NeedExpData")]
public class NeedExpDataBase : SerializedScriptableObject
{
	public List<int> needCharacterLvExpList;

	public List<int> needSexLvExpList;

	public List<int> needKizunaLvExpList;

	public List<int> needCraftLvExpList;

	public List<int> needShopLvTransactionList;

	public List<int> needMergeFacilityLvExpList;

	public List<int> needFacilityLvCostList;

	public List<int> needFacilityToolLvCostList;

	public List<int> needFacilityFurnaceLvCostList;

	public List<int> needFacilityAddOnLvCostList;

	public List<string> needFacilityLvNeedRecipeList;

	public List<string> needFacilityToolLvNeedRecipeList;

	public List<string> needFacilityLvFurnaceNeedRecipeList;

	public List<string> needFacilityLvAddOnNeedRecipeList;

	public List<int> remainingDaysToCraftLvList;
}
