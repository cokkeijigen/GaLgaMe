using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ApplyCreateNewMergeItem : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		List<NeedMaterialData> list = new List<NeedMaterialData>();
		int characterNum = 0;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemPartyWeaponData itemPartyWeaponData = null;
			itemPartyWeaponData = ((!craftCanvasManager.isPowerUpCraft) ? GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID) : GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.nextItemID));
			if (itemPartyWeaponData != null)
			{
				if (craftCanvasManager.isPowerUpCraft)
				{
					list = itemPartyWeaponData.needMaterialList;
					_ = itemPartyWeaponData.price;
					craftCheckManager.craftedItemID = craftManager.nextItemID;
				}
				else
				{
					list = itemPartyWeaponData.needMaterialEditList;
					_ = itemPartyWeaponData.editPrice;
					craftCheckManager.craftedItemID = craftManager.clickedItemID;
				}
			}
			characterNum = ((craftCheckManager.craftedItemID < 1400) ? 1 : ((craftCheckManager.craftedItemID >= 1500) ? ((craftCheckManager.craftedItemID >= 1600) ? 4 : 3) : 2));
			break;
		}
		case 1:
		{
			ItemPartyArmorData itemPartyArmorData = null;
			itemPartyArmorData = ((!craftCanvasManager.isPowerUpCraft) ? GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID) : GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.nextItemID));
			if (itemPartyArmorData != null)
			{
				if (craftCanvasManager.isPowerUpCraft)
				{
					list = itemPartyArmorData.needMaterialList;
					_ = itemPartyArmorData.price;
					craftCheckManager.craftedItemID = craftManager.nextItemID;
				}
				else
				{
					list = itemPartyArmorData.needMaterialEditList;
					_ = itemPartyArmorData.editPrice;
					craftCheckManager.craftedItemID = craftManager.clickedItemID;
				}
			}
			characterNum = ((craftCheckManager.craftedItemID < 2400) ? 1 : ((craftCheckManager.craftedItemID >= 2500) ? ((craftCheckManager.craftedItemID >= 2600) ? 4 : 3) : 2));
			break;
		}
		}
		if (list == null)
		{
			return;
		}
		int count = list.Count;
		count = ((count < 3) ? count : 3);
		for (int i = 0; i < list.Count; i++)
		{
			int itemID = list[i].itemID;
			if (itemID < 900)
			{
				PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(itemID, list[i].needNum);
			}
		}
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			if (craftCanvasManager.isPowerUpCraft)
			{
				PlayerInventoryDataEquipAccess.UpgradePlayerHavePartyWeapon(craftManager.clickedItemID, craftManager.nextItemID);
				PlayerEquipDataManager.SetEquipPlayerWeapon(craftManager.nextItemID, 0, characterNum);
			}
			else
			{
				PlayerInventoryDataEquipAccess.UpgradePlayerHavePartyWeapon(craftManager.clickedItemID, craftManager.clickedItemID);
				PlayerEquipDataManager.SetEquipPlayerWeapon(craftManager.clickedItemID, 0, characterNum);
			}
			break;
		case 1:
			if (craftCanvasManager.isPowerUpCraft)
			{
				PlayerInventoryDataEquipAccess.UpgradePlayerHavePartyArmor(craftManager.clickedItemID, craftManager.nextItemID);
				PlayerEquipDataManager.SetEquipPlayerArmor(craftManager.nextItemID, 0, characterNum);
			}
			else
			{
				PlayerInventoryDataEquipAccess.UpgradePlayerHavePartyArmor(craftManager.clickedItemID, craftManager.clickedItemID);
				PlayerEquipDataManager.SetEquipPlayerArmor(craftManager.clickedItemID, 0, characterNum);
			}
			break;
		}
		if (craftAddOnManager.selectedMagicMatrialID[0] != 0)
		{
			PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(craftAddOnManager.selectedMagicMatrialID[0], 1);
		}
		if (craftAddOnManager.selectedMagicMatrialID[1] != 0)
		{
			PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(craftAddOnManager.selectedMagicMatrialID[1], 1);
		}
		PlayerInventoryFactorManager.HaveItemFactorSort();
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
		PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
		switch (PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv)
		{
		case 1:
			PlayerNonSaveDataManager.addTimeZoneNum += 3;
			break;
		case 2:
			PlayerNonSaveDataManager.addTimeZoneNum += 2;
			break;
		case 3:
		case 4:
			PlayerNonSaveDataManager.addTimeZoneNum++;
			break;
		}
		GameObject.Find("Craft LV Manager").GetComponent<ArborFSM>().SendTrigger("AddCraftExp");
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
