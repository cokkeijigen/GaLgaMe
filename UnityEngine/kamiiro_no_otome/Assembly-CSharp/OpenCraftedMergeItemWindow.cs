using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenCraftedMergeItemWindow : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private ItemPartyWeaponData itemWeaponData;

	private ItemPartyArmorData itemArmorData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
	}

	public override void OnStateBegin()
	{
		craftCheckManager.animationFrame.SetActive(value: false);
		craftCheckManager.animationSkipFrame.SetActive(value: false);
		if (craftCanvasManager.isPowerUpCraft)
		{
			craftCheckManager.infoWindowHeaderTextLoc.Term = "headerMergeSummary";
		}
		else
		{
			craftCheckManager.infoWindowHeaderTextLoc.Term = "headerEditSummary";
		}
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			itemWeaponData = null;
			if (craftCanvasManager.isPowerUpCraft)
			{
				itemWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.nextItemID);
			}
			else
			{
				itemWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID);
			}
			craftCheckManager.craftedSummaryWindow.SetActive(value: true);
			ParameterContainer component2 = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
			craftCheckManager.infoFactorScrollFrameGo.SetActive(value: true);
			component2.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			string text2 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
			component2.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text2;
			component2.GetVariable<UguiImage>("itemImage").image.sprite = itemWeaponData.itemSprite;
			component2.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text2 + "_summary";
			craftManager.itemStatusParam[0] = itemWeaponData.attackPower;
			craftManager.itemStatusParam[1] = itemWeaponData.magicAttackPower;
			craftManager.itemStatusParam[2] = itemWeaponData.accuracy;
			craftManager.itemStatusParam[3] = itemWeaponData.critical;
			craftManager.itemStatusParam[4] = itemWeaponData.comboProbability;
			craftManager.itemStatusParam[6] = itemWeaponData.factorSlot;
			craftManager.itemStatusParam[7] = itemWeaponData.factorHaveLimit;
			craftCheckManager.ShowStatusParameters();
			break;
		}
		case 1:
		{
			itemArmorData = null;
			if (craftCanvasManager.isPowerUpCraft)
			{
				itemArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.nextItemID);
			}
			else
			{
				itemArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID);
			}
			craftCheckManager.craftedSummaryWindow.SetActive(value: true);
			ParameterContainer component = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
			craftCheckManager.infoFactorScrollFrameGo.SetActive(value: true);
			component.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			string text = "armor" + itemArmorData.itemID;
			component.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
			component.GetVariable<UguiImage>("itemImage").image.sprite = itemArmorData.itemSprite;
			component.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text + "_summary";
			craftManager.itemStatusParam[0] = itemArmorData.defensePower;
			craftManager.itemStatusParam[1] = itemArmorData.magicDefensePower;
			craftManager.itemStatusParam[2] = itemArmorData.evasion;
			craftManager.itemStatusParam[3] = itemArmorData.abnormalResist;
			craftManager.itemStatusParam[4] = itemArmorData.recoveryMp;
			craftManager.itemStatusParam[6] = itemArmorData.factorSlot;
			craftManager.itemStatusParam[7] = itemArmorData.factorHaveLimit;
			craftCheckManager.ShowStatusParameters();
			break;
		}
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
