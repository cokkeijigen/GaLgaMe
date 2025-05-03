using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenCraftedItemWindow : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private ItemWeaponData itemWeaponData;

	private ItemArmorData itemArmorData;

	private ItemCanMakeMaterialData itemCanMakeMaterialData;

	private ItemEventItemData itemEventItemData;

	private ItemCampItemData itemCampItemData;

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
		string selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
		if (!(selectCraftCanvasName == "craft"))
		{
			if (selectCraftCanvasName == "newCraft")
			{
				craftCheckManager.infoWindowHeaderTextLoc.Term = "headerCraftSummary";
			}
		}
		else if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
		{
			craftCheckManager.infoWindowHeaderTextLoc.Term = "headerCraftSummary";
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
			if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.nextItemID);
			}
			else
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
			}
			craftCheckManager.craftedSummaryWindow.SetActive(value: true);
			ParameterContainer component5 = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
			craftCheckManager.infoFactorScrollFrameGo.SetActive(value: true);
			component5.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			string text5 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
			component5.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text5;
			component5.GetVariable<UguiImage>("itemImage").image.sprite = itemWeaponData.itemSprite;
			component5.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text5 + "_summary";
			craftManager.itemStatusParam[0] = itemWeaponData.attackPower;
			craftManager.itemStatusParam[1] = itemWeaponData.magicAttackPower;
			craftManager.itemStatusParam[2] = itemWeaponData.accuracy;
			craftManager.itemStatusParam[3] = itemWeaponData.critical;
			craftManager.itemStatusParam[4] = itemWeaponData.weaponMp;
			craftManager.itemStatusParam[6] = itemWeaponData.factorSlot;
			craftManager.itemStatusParam[7] = itemWeaponData.factorHaveLimit;
			craftCheckManager.ShowStatusParameters();
			break;
		}
		case 1:
		{
			itemArmorData = null;
			if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.nextItemID);
			}
			else
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
			}
			craftCheckManager.craftedSummaryWindow.SetActive(value: true);
			ParameterContainer component4 = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
			craftCheckManager.infoFactorScrollFrameGo.SetActive(value: true);
			component4.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			string text4 = "armor" + itemArmorData.itemID;
			component4.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text4;
			component4.GetVariable<UguiImage>("itemImage").image.sprite = itemArmorData.itemSprite;
			component4.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text4 + "_summary";
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
		case 2:
		{
			itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID);
			craftCheckManager.craftedSummaryWindow.SetActive(value: true);
			ParameterContainer component3 = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
			craftCheckManager.infoFactorScrollFrameGo.SetActive(value: false);
			component3.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCanMakeMaterialData.category.ToString()];
			string text3 = itemCanMakeMaterialData.category.ToString() + itemCanMakeMaterialData.itemID;
			component3.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text3;
			component3.GetVariable<UguiImage>("itemImage").image.sprite = itemCanMakeMaterialData.itemSprite;
			component3.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text3 + "_summary";
			craftCheckManager.ShowStatusParameters();
			break;
		}
		case 3:
		{
			itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == craftManager.clickedItemID);
			craftCheckManager.craftedSummaryWindow.SetActive(value: true);
			ParameterContainer component2 = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
			craftCheckManager.infoFactorScrollFrameGo.SetActive(value: false);
			component2.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemEventItemData.category.ToString()];
			string text2 = "eventItem" + itemEventItemData.itemID;
			component2.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text2;
			component2.GetVariable<UguiImage>("itemImage").image.sprite = itemEventItemData.itemSprite;
			component2.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text2 + "_summary";
			craftCheckManager.ShowStatusParameters();
			break;
		}
		case 4:
		{
			itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID);
			craftCheckManager.craftedSummaryWindow.SetActive(value: true);
			ParameterContainer component = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
			craftCheckManager.infoFactorScrollFrameGo.SetActive(value: false);
			component.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCampItemData.category.ToString()];
			string text = "campItem" + itemCampItemData.itemID;
			component.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
			component.GetVariable<UguiImage>("itemImage").image.sprite = itemCampItemData.itemSprite;
			component.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text + "_summary";
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
