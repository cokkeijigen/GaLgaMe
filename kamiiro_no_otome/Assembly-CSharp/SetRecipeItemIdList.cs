using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetRecipeItemIdList : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		SetRecipeList();
		Debug.Log("レシピの登録処理を完了");
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

	private void SetRecipeList()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Count; i++)
		{
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList[i];
			string recipeFlagName = itemWeaponData.recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemWeaponData.itemID);
		}
		for (int j = 0; j < GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Count; j++)
		{
			ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList[j];
			string recipeFlagName = itemPartyWeaponData.recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemPartyWeaponData.itemID);
		}
		for (int k = 0; k < GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Count; k++)
		{
			ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList[k];
			string recipeFlagName = GameDataManager.instance.itemArmorDataBase.itemArmorDataList[k].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemArmorData.itemID);
		}
		for (int l = 0; l < GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Count; l++)
		{
			ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList[l];
			string recipeFlagName = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList[l].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemPartyArmorData.itemID);
		}
		for (int m = 0; m < GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Count; m++)
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList[m];
			string recipeFlagName = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList[m].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemCanMakeMaterialData.itemID);
		}
		for (int n = 0; n < GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Count; n++)
		{
			ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[n];
			string recipeFlagName = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[n].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemEventItemData.itemID);
		}
	}

	private void SetCraftItemIdDictionary(string recipeName, int itemID)
	{
		if (PlayerCraftStatusManager.craftRecipeItemIdDictionary.ContainsKey(recipeName))
		{
			PlayerCraftStatusManager.craftRecipeItemIdDictionary[recipeName].Add(itemID);
		}
		else
		{
			List<int> list = new List<int>();
			list.Add(itemID);
			PlayerCraftStatusManager.craftRecipeItemIdDictionary.Add(recipeName, list);
		}
		if (!PlayerFlagDataManager.recipeFlagDictionary.ContainsKey(recipeName))
		{
			PlayerFlagDataManager.recipeFlagDictionary.Add(recipeName, value: false);
		}
	}
}
