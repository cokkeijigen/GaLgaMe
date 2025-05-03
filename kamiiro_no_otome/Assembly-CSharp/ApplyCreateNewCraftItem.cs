using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ApplyCreateNewCraftItem : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private CraftAddOnManager craftAddOnManager;

	private NewCraftCanvasManager newCraftCanvasManager;

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
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		List<NeedMaterialData> list = null;
		string text = "";
		bool flag = false;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemWeaponData itemWeaponData = null;
			if (craftCanvasManager.isPowerUpCraft)
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.nextItemID);
				PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.nextItemID] = true;
			}
			else if (craftCanvasManager.isRemainingDaysZero)
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.nextItemID);
				PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.nextItemID] = true;
			}
			else if (craftCanvasManager.isCompleteEnhanceCount)
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
				PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.nextItemID] = true;
			}
			else
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
			}
			if (!(itemWeaponData != null))
			{
				break;
			}
			string selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
			if (!(selectCraftCanvasName == "craft"))
			{
				if (selectCraftCanvasName == "newCraft")
				{
					list = itemWeaponData.needMaterialNewerList;
				}
			}
			else
			{
				list = ((!craftCanvasManager.isPowerUpCraft) ? ((!craftCanvasManager.isRemainingDaysZero) ? ((!craftCanvasManager.isCompleteEnhanceCount) ? itemWeaponData.needMaterialEditList : itemWeaponData.needMaterialEditList) : itemWeaponData.needMaterialNewerList) : itemWeaponData.needMaterialList);
			}
			break;
		}
		case 1:
		{
			ItemArmorData itemArmorData = null;
			if (craftCanvasManager.isPowerUpCraft)
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.nextItemID);
				PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.nextItemID] = true;
			}
			else if (craftCanvasManager.isRemainingDaysZero)
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.nextItemID);
				PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.nextItemID] = true;
			}
			else if (craftCanvasManager.isCompleteEnhanceCount)
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
				PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.nextItemID] = true;
			}
			else
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
			}
			if (!(itemArmorData != null))
			{
				break;
			}
			string selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
			if (!(selectCraftCanvasName == "craft"))
			{
				if (selectCraftCanvasName == "newCraft")
				{
					list = itemArmorData.needMaterialNewerList;
				}
			}
			else
			{
				list = ((!craftCanvasManager.isPowerUpCraft) ? ((!craftCanvasManager.isRemainingDaysZero) ? ((!craftCanvasManager.isCompleteEnhanceCount) ? itemArmorData.needMaterialEditList : itemArmorData.needMaterialEditList) : itemArmorData.needMaterialNewerList) : itemArmorData.needMaterialList);
			}
			break;
		}
		case 2:
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID);
			if (itemCanMakeMaterialData != null)
			{
				list = itemCanMakeMaterialData.needMaterialList;
			}
			break;
		}
		case 3:
		{
			ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == craftManager.clickedItemID);
			if (itemEventItemData != null)
			{
				list = itemEventItemData.needMaterialList;
				text = itemEventItemData.category.ToString();
			}
			break;
		}
		case 4:
		{
			ItemCampItemData itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID);
			if (itemCampItemData != null)
			{
				list = itemCampItemData.needMaterialList;
				text = itemCampItemData.category.ToString();
			}
			break;
		}
		}
		if (list == null)
		{
			return;
		}
		int count = list.Count;
		count = ((count < 4) ? count : 4);
		Debug.Log("必要素材の個数の倍数：" + newCraftCanvasManager.craftQuantity);
		for (int i = 0; i < list.Count; i++)
		{
			int itemID = list[i].itemID;
			if (itemID < 900)
			{
				int count2 = list[i].needNum * newCraftCanvasManager.craftQuantity;
				PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(itemID, count2);
			}
		}
		if (PlayerNonSaveDataManager.selectCraftCanvasName == "craft")
		{
			switch (craftManager.selectCategoryNum)
			{
			case 0:
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					int num3 = PlayerInventoryDataEquipAccess.UpgradePlayerHaveWeapon(craftManager.clickedItemID, craftManager.clickedUniqueID, craftManager.nextItemID);
					craftCheckManager.craftedItemID = craftManager.nextItemID;
					craftCheckManager.craftedUniqueID = num3;
					PlayerEquipDataManager.SetEquipPlayerWeapon(craftManager.nextItemID, num3, 0);
					PlayerInventoryDataEquipAccess.AddCreateWeaponEnhanceCount(craftManager.nextItemID, num3, isPowerUp: true);
				}
				else
				{
					int num4 = PlayerInventoryDataEquipAccess.UpgradePlayerHaveWeapon(craftManager.clickedItemID, craftManager.clickedUniqueID, craftManager.clickedItemID);
					craftCheckManager.craftedItemID = craftManager.clickedItemID;
					craftCheckManager.craftedUniqueID = num4;
					PlayerEquipDataManager.SetEquipPlayerWeapon(craftManager.clickedItemID, num4, 0);
					PlayerInventoryDataEquipAccess.AddCreateWeaponEnhanceCount(craftManager.clickedItemID, num4, isPowerUp: false);
				}
				break;
			case 1:
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					int num = PlayerInventoryDataEquipAccess.UpgradePlayerHaveArmor(craftManager.clickedItemID, craftManager.clickedUniqueID, craftManager.nextItemID);
					craftCheckManager.craftedItemID = craftManager.nextItemID;
					craftCheckManager.craftedUniqueID = num;
					PlayerEquipDataManager.SetEquipPlayerArmor(craftManager.nextItemID, num, 0);
					PlayerInventoryDataEquipAccess.AddCreateArmorEnhanceCount(craftManager.nextItemID, num, isPowerUp: true);
				}
				else
				{
					int num2 = PlayerInventoryDataEquipAccess.UpgradePlayerHaveArmor(craftManager.clickedItemID, craftManager.clickedUniqueID, craftManager.clickedItemID);
					craftCheckManager.craftedItemID = craftManager.clickedItemID;
					craftCheckManager.craftedUniqueID = num2;
					PlayerEquipDataManager.SetEquipPlayerArmor(craftManager.clickedItemID, num2, 0);
					PlayerInventoryDataEquipAccess.AddCreateArmorEnhanceCount(craftManager.clickedItemID, num2, isPowerUp: false);
				}
				break;
			}
		}
		else
		{
			switch (craftManager.selectCategoryNum)
			{
			case 0:
			{
				int newInstanceID2 = PlayerInventoryDataEquipAccess.PlayerHaveWeaponAdd_INSTANCE(craftManager.clickedItemID, isEquip: false);
				int newUniqueID2 = PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(craftManager.clickedItemID, newInstanceID2, craftCheckManager.newFactorData.factorID, craftCheckManager.newFactorData.factorLV, craftCheckManager.newFactorData.factorPower);
				craftCheckManager.craftedItemID = craftManager.clickedItemID;
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == newInstanceID2);
				HaveFactorData item2 = haveWeaponData.weaponHaveFactor.Find((HaveFactorData data) => data.factorID == craftCheckManager.newFactorData.factorID && data.uniqueID == newUniqueID2);
				haveWeaponData.weaponSetFactor.Add(item2);
				flag = true;
				break;
			}
			case 1:
			{
				int newInstanceID = PlayerInventoryDataEquipAccess.PlayerHaveArmorAdd_INSTANCE(craftManager.clickedItemID, isEquip: false);
				int newUniqueID = PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(craftManager.clickedItemID, newInstanceID, craftCheckManager.newFactorData.factorID, craftCheckManager.newFactorData.factorLV, craftCheckManager.newFactorData.factorPower);
				craftCheckManager.craftedItemID = craftManager.clickedItemID;
				HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == newInstanceID);
				HaveFactorData item = haveArmorData.armorHaveFactor.Find((HaveFactorData data) => data.factorID == craftCheckManager.newFactorData.factorID && data.uniqueID == newUniqueID);
				haveArmorData.armorSetFactor.Add(item);
				flag = true;
				break;
			}
			case 2:
			{
				int itemSortID3 = PlayerInventoryDataAccess.GetItemSortID(craftManager.clickedItemID);
				PlayerInventoryDataAccess.PlayerHaveItemAdd(craftManager.clickedItemID, itemSortID3, newCraftCanvasManager.craftQuantity);
				craftCheckManager.craftedItemID = craftManager.clickedItemID;
				break;
			}
			case 3:
			{
				int itemSortID2 = PlayerInventoryDataAccess.GetItemSortID(craftManager.clickedItemID);
				PlayerInventoryDataAccess.PlayerHaveEventItemAdd(craftManager.clickedItemID, itemSortID2);
				craftCheckManager.craftedItemID = craftManager.clickedItemID;
				string key2 = text + craftManager.clickedItemID;
				PlayerFlagDataManager.keyItemFlagDictionary[key2] = true;
				break;
			}
			case 4:
			{
				int itemSortID = PlayerInventoryDataAccess.GetItemSortID(craftManager.clickedItemID);
				PlayerInventoryDataAccess.PlayerHaveCampItemAdd(craftManager.clickedItemID, itemSortID);
				craftCheckManager.craftedItemID = craftManager.clickedItemID;
				string key = "campItem" + craftManager.clickedItemID;
				PlayerFlagDataManager.keyItemFlagDictionary[key] = true;
				break;
			}
			}
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
		PlayerQuestDataManager.RefreshCraftQuestHaveItemCount();
		PlayerQuestDataManager.RefreshStoryQuestFlagData("craft");
		if (flag)
		{
			PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
		}
		else
		{
			Transition(stateLink);
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

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: false, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		Debug.Log("Equipデータの更新完了");
		Transition(stateLink);
	}
}
