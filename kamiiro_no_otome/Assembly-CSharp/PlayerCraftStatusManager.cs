using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerCraftStatusManager : SerializedBehaviour
{
	public static int playerCraftLv;

	public static int playerCraftExp;

	public static readonly int storeTendingProbability = 50;

	public static Dictionary<string, CraftWorkShopData> craftFacilityDataDictionary = new Dictionary<string, CraftWorkShopData> { 
	{
		"Kingdom1",
		new CraftWorkShopData()
	} };

	public static Dictionary<string, List<int>> craftRecipeItemIdDictionary = new Dictionary<string, List<int>>();

	public static void AddCraftExp(int addExp)
	{
		playerCraftExp += addExp;
		playerCraftExp = Mathf.Clamp(playerCraftExp, 0, 99999);
	}

	public static int GetCraftLvFromExp()
	{
		int num = 0;
		num = GameDataManager.instance.needExpDataBase.needCraftLvExpList.Where((int data) => data <= playerCraftExp).ToList().Count;
		Debug.Log("クラフトEXP：" + playerCraftExp + "／クラフトLV：" + num);
		return num;
	}
}
