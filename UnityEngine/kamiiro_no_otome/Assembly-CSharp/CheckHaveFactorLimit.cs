using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckHaveFactorLimit : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private CraftAddOnManager craftAddOnManager;

	public StateLink listFullLink;

	public StateLink listNotFullLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponentInParent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GetComponent<CraftCheckManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		List<HaveFactorData> dict = null;
		List<HaveFactorData> dict2 = null;
		int num = 0;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
			{
				ItemWeaponData itemWeaponData = null;
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict2);
					itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
					if (itemWeaponData != null)
					{
						num = itemWeaponData.factorHaveLimit;
					}
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict2);
					itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
					if (itemWeaponData != null)
					{
						num = itemWeaponData.factorHaveLimit;
					}
				}
				break;
			}
			case "merge":
			{
				ItemPartyWeaponData itemPartyWeaponData = null;
				if (craftCanvasManager.isPowerUpCraft)
				{
					itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID);
					if (!(itemPartyWeaponData != null))
					{
						break;
					}
					num = itemPartyWeaponData.factorHaveLimit;
					for (int j = 0; j < itemPartyWeaponData.needMaterialList.Count; j++)
					{
						int itemID2 = itemPartyWeaponData.needMaterialList[j].itemID;
						if (itemID2 >= 1300 && itemID2 < 2000)
						{
							PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(itemID2, 0, ref dict);
							PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(itemID2, 0, ref dict2);
							break;
						}
					}
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, 0, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftManager.clickedItemID, 0, ref dict2);
					itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID);
					if (itemPartyWeaponData != null)
					{
						num = itemPartyWeaponData.factorHaveLimit;
					}
				}
				break;
			}
			}
			break;
		case 1:
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
			{
				ItemArmorData itemArmorData = null;
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict2);
					itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
					if (itemArmorData != null)
					{
						num = itemArmorData.factorHaveLimit;
					}
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict2);
					itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
					if (itemArmorData != null)
					{
						num = itemArmorData.factorHaveLimit;
					}
				}
				break;
			}
			case "merge":
			{
				ItemPartyArmorData itemPartyArmorData = null;
				if (craftCanvasManager.isPowerUpCraft)
				{
					itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID);
					if (!(itemPartyArmorData != null))
					{
						break;
					}
					num = itemPartyArmorData.factorHaveLimit;
					for (int i = 0; i < itemPartyArmorData.needMaterialList.Count; i++)
					{
						int itemID = itemPartyArmorData.needMaterialList[i].itemID;
						if (itemID >= 2300 && itemID < 3000)
						{
							PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(itemID, 0, ref dict);
							PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(itemID, 0, ref dict2);
							break;
						}
					}
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, 0, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(craftManager.clickedItemID, 0, ref dict2);
					itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID);
					if (itemPartyArmorData != null)
					{
						num = itemPartyArmorData.factorHaveLimit;
					}
				}
				break;
			}
			}
			break;
		}
		if (dict != null && num > 0)
		{
			if (num < dict.Count)
			{
				ParameterContainer component = craftAddOnManager.overlayCanvasSelectWindow.GetComponent<ParameterContainer>();
				craftAddOnManager.overlayCanvasSelectWindow.SetActive(value: false);
				component.GetGameObject("summaryWindowGo").SetActive(value: false);
				craftCheckManager.factorDisposalWindow.SetActive(value: true);
				craftCheckManager.blackImageGo.transform.SetSiblingIndex(6);
				craftCheckManager.tempSetFactorList = dict2;
				craftCheckManager.tempHaveFactorList = dict;
				Transition(listFullLink);
			}
			else
			{
				Transition(listNotFullLink);
			}
		}
		else
		{
			Transition(listNotFullLink);
		}
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
