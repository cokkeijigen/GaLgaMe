using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetNewFactor : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private bool isNewFactorEquiped;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponentInParent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GetComponent<CraftCheckManager>();
	}

	public override void OnStateBegin()
	{
		int newFactorID = craftCheckManager.newFactorID;
		int newPowerLevel = craftCheckManager.newPowerLevel;
		int newFactorPower = craftCheckManager.newFactorPower;
		isNewFactorEquiped = false;
		int newUniqueID = -1;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
			{
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(craftManager.clickedItemID, craftManager.clickedUniqueID, newFactorID, newPowerLevel, newFactorPower);
				}
				else
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(craftManager.clickedItemID, craftManager.clickedUniqueID, newFactorID, newPowerLevel, newFactorPower);
				}
				ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
				if (haveWeaponData.weaponSetFactor.Count < itemWeaponData.factorSlot)
				{
					HaveFactorData item4 = haveWeaponData.weaponHaveFactor.Find((HaveFactorData data) => data.factorID == newFactorID && data.uniqueID == newUniqueID);
					haveWeaponData.weaponSetFactor.Add(item4);
					isNewFactorEquiped = true;
				}
				break;
			}
			case "merge":
			{
				if (craftCanvasManager.isPowerUpCraft)
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(craftManager.clickedItemID, newFactorID, newPowerLevel, newFactorPower);
				}
				else
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(craftManager.clickedItemID, newFactorID, newPowerLevel, newFactorPower);
				}
				ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID);
				HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData data) => data.itemID == craftManager.clickedItemID);
				if (havePartyWeaponData.weaponSetFactor.Count < itemPartyWeaponData.factorSlot)
				{
					HaveFactorData item3 = havePartyWeaponData.weaponHaveFactor.Find((HaveFactorData data) => data.factorID == newFactorID && data.uniqueID == newUniqueID);
					havePartyWeaponData.weaponSetFactor.Add(item3);
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
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(craftManager.clickedItemID, craftManager.clickedUniqueID, newFactorID, newPowerLevel, newFactorPower);
				}
				else
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(craftManager.clickedItemID, craftManager.clickedUniqueID, newFactorID, newPowerLevel, newFactorPower);
				}
				ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
				HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
				if (haveArmorData.armorSetFactor.Count < itemArmorData.factorSlot)
				{
					HaveFactorData item2 = haveArmorData.armorHaveFactor.Find((HaveFactorData data) => data.factorID == newFactorID && data.uniqueID == newUniqueID);
					haveArmorData.armorSetFactor.Add(item2);
					isNewFactorEquiped = true;
				}
				break;
			}
			case "merge":
			{
				if (craftCanvasManager.isPowerUpCraft)
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(craftManager.clickedItemID, newFactorID, newPowerLevel, newFactorPower);
				}
				else
				{
					newUniqueID = PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(craftManager.clickedItemID, newFactorID, newPowerLevel, newFactorPower);
				}
				ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID);
				HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData data) => data.itemID == craftManager.clickedItemID);
				if (havePartyArmorData.armorSetFactor.Count < itemPartyArmorData.factorSlot)
				{
					HaveFactorData item = havePartyArmorData.armorHaveFactor.Find((HaveFactorData data) => data.factorID == newFactorID && data.uniqueID == newUniqueID);
					havePartyArmorData.armorSetFactor.Add(item);
				}
				break;
			}
			}
			break;
		}
		craftCheckManager.newFactorData.uniqueID = newUniqueID;
		craftCheckManager.newFactorData.factorID = newFactorID;
		craftCheckManager.newFactorData.factorLV = newPowerLevel;
		craftCheckManager.newFactorData.factorPower = newFactorPower;
		if (isNewFactorEquiped)
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
