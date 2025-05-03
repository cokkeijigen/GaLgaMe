using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class NewCraftInfoAndStatusRefresh : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer selectItemInfoParam = craftCanvasManager.selectItemInfoParam;
		selectItemInfoParam.GetGameObject("needWorkShopLvGo").SetActive(value: true);
		selectItemInfoParam.GetGameObject("needInheritWorkShopLvGo").SetActive(value: false);
		selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.gameObject.SetActive(value: true);
		newCraftCanvasManager.newCraftApplyButton.alpha = 1f;
		newCraftCanvasManager.newCraftApplyButton.interactable = true;
		craftCanvasManager.craftStartButtonGo.GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[0];
		newCraftCanvasManager.isNewCraftImpossible = false;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.itemWeaponData = itemWeaponData;
			selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemWeaponData.needWorkShopLevel.ToString();
			selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			string text6 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text6;
			selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemWeaponData.itemSprite;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text6 + "_summary";
			craftManager.itemStatusParam[0] = itemWeaponData.attackPower;
			craftManager.itemStatusParam[1] = itemWeaponData.magicAttackPower;
			craftManager.itemStatusParam[2] = itemWeaponData.accuracy;
			craftManager.itemStatusParam[3] = itemWeaponData.critical;
			craftManager.itemStatusParam[4] = itemWeaponData.weaponMp;
			craftManager.itemStatusParam[6] = itemWeaponData.factorSlot;
			craftManager.itemStatusParam[7] = itemWeaponData.factorHaveLimit;
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData4 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemWeaponData.needWorkShopLevel <= craftWorkShopData4.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			break;
		}
		case 1:
		{
			ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.itemArmorData = itemArmorData;
			selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemArmorData.needWorkShopLevel.ToString();
			selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			string text5 = "armor" + itemArmorData.itemID;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text5;
			selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemArmorData.itemSprite;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text5 + "_summary";
			craftManager.itemStatusParam[0] = itemArmorData.defensePower;
			craftManager.itemStatusParam[1] = itemArmorData.magicDefensePower;
			craftManager.itemStatusParam[2] = itemArmorData.evasion;
			craftManager.itemStatusParam[3] = itemArmorData.abnormalResist;
			craftManager.itemStatusParam[4] = itemArmorData.recoveryMp;
			craftManager.itemStatusParam[6] = itemArmorData.factorSlot;
			craftManager.itemStatusParam[7] = itemArmorData.factorHaveLimit;
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData3 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemArmorData.needWorkShopLevel <= craftWorkShopData3.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			break;
		}
		case 2:
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.itemCanMakeMaterialData = itemCanMakeMaterialData;
			selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: true);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemCanMakeMaterialData.needWorkShopLevel.ToString();
			selectItemInfoParam.GetVariable<UguiTextVariable>("needFurnaceText").text.text = itemCanMakeMaterialData.needFurnaceLevel.ToString();
			selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCanMakeMaterialData.category.ToString()];
			string text7 = itemCanMakeMaterialData.category.ToString() + itemCanMakeMaterialData.itemID;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text7;
			selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemCanMakeMaterialData.itemSprite;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text7 + "_summary";
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData5 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemCanMakeMaterialData.needWorkShopLevel <= craftWorkShopData5.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = itemCanMakeMaterialData.needFurnaceLevel <= craftWorkShopData5.furnaceLv;
			SetSummaryAlertGoVisible();
			break;
		}
		case 3:
		{
			ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == craftManager.clickedItemID);
			if (itemEventItemData != null)
			{
				craftCanvasManager.itemEventItemData = itemEventItemData;
				string text3 = "eventItem" + itemEventItemData.itemID;
				if (PlayerFlagDataManager.keyItemFlagDictionary[text3])
				{
					newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
					newCraftCanvasManager.newCraftApplyButton.interactable = false;
					craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonNewCraftCreated";
					Debug.Log(text3 + "はすでに作成済み");
				}
				selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
				selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemEventItemData.needWorkShopLevel.ToString();
				selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemEventItemData.category.ToString()];
				string text4 = "eventItem" + itemEventItemData.itemID;
				selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text4;
				selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemEventItemData.itemSprite;
				selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text4 + "_summary";
				craftCanvasManager.ShowStatusParameters();
				CraftWorkShopData craftWorkShopData2 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
				craftCanvasManager.isWorkShopLvEnough = itemEventItemData.needWorkShopLevel <= craftWorkShopData2.workShopLv;
				craftCanvasManager.isFurnaceLvEnough = true;
				SetSummaryAlertGoVisible();
			}
			else
			{
				SetSelectItemInfoEmpty();
			}
			break;
		}
		case 4:
		{
			ItemCampItemData itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID);
			if (itemCampItemData != null)
			{
				craftCanvasManager.itemCampItemData = itemCampItemData;
				string text = "campItem" + itemCampItemData.itemID;
				if (PlayerFlagDataManager.keyItemFlagDictionary[text])
				{
					newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
					newCraftCanvasManager.newCraftApplyButton.interactable = false;
					craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonNewCraftHaved";
					Debug.Log(text + "はすでに作成済み");
				}
				selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
				selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemCampItemData.needWorkShopLevel.ToString();
				selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCampItemData.category.ToString()];
				string text2 = "campItem" + itemCampItemData.itemID;
				selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text2;
				selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemCampItemData.itemSprite;
				selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text2 + "_summary";
				craftCanvasManager.ShowStatusParameters();
				CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
				craftCanvasManager.isWorkShopLvEnough = itemCampItemData.needWorkShopLevel <= craftWorkShopData.workShopLv;
				craftCanvasManager.isFurnaceLvEnough = true;
				SetSummaryAlertGoVisible();
			}
			else
			{
				SetSelectItemInfoEmpty();
			}
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

	private void SetSelectItemInfoEmpty()
	{
		ParameterContainer selectItemInfoParam = craftCanvasManager.selectItemInfoParam;
		selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
		selectItemInfoParam.GetGameObject("needWorkShopLvGo").SetActive(value: false);
		selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = "--";
		selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.gameObject.SetActive(value: false);
		selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = "noStatus";
		selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = newCraftCanvasManager.noSelectItemImageSprite;
		selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = "noSelectItemSummary";
		ParameterContainer selectItemInfoParam2 = craftCanvasManager.selectItemInfoParam;
		IList<I2LocalizeComponent> variableList = selectItemInfoParam2.GetVariableList<I2LocalizeComponent>("statusTypeLoc");
		IList<UguiTextVariable> variableList2 = selectItemInfoParam2.GetVariableList<UguiTextVariable>("statusPowerText");
		for (int i = 0; i < 8; i++)
		{
			variableList[i].localize.Term = "noStatus";
			variableList2[i].text.text = "";
		}
		newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
		newCraftCanvasManager.newCraftApplyButton.interactable = false;
	}

	private void SetSummaryAlertGoVisible()
	{
		ParameterContainer selectItemInfoParam = craftCanvasManager.selectItemInfoParam;
		if (craftCanvasManager.isWorkShopLvEnough)
		{
			selectItemInfoParam.GetGameObjectList("alertGoList")[0].SetActive(value: false);
			selectItemInfoParam.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 1f, 1f);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 1f, 1f);
			selectItemInfoParam.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 1f, 1f);
		}
		else
		{
			selectItemInfoParam.GetGameObjectList("alertGoList")[0].SetActive(value: true);
			selectItemInfoParam.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 0.55f, 0.3f);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 0.55f, 0.3f);
			selectItemInfoParam.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 0.55f, 0.3f);
		}
		if (craftManager.selectCategoryNum == 2)
		{
			if (craftCanvasManager.isFurnaceLvEnough)
			{
				selectItemInfoParam.GetGameObjectList("alertGoList")[1].SetActive(value: false);
				selectItemInfoParam.GetVariableList<UguiImage>("needLvIconList")[1].image.color = new Color(1f, 1f, 1f);
				selectItemInfoParam.GetVariable<UguiTextVariable>("needFurnaceText").text.color = new Color(1f, 1f, 1f);
				selectItemInfoParam.GetVariableList<UguiTextVariable>("needSummaryTextList")[1].text.color = new Color(1f, 1f, 1f);
			}
			else
			{
				selectItemInfoParam.GetGameObjectList("alertGoList")[1].SetActive(value: true);
				selectItemInfoParam.GetVariableList<UguiImage>("needLvIconList")[1].image.color = new Color(1f, 0.55f, 0.3f);
				selectItemInfoParam.GetVariable<UguiTextVariable>("needFurnaceText").text.color = new Color(1f, 0.55f, 0.3f);
				selectItemInfoParam.GetVariableList<UguiTextVariable>("needSummaryTextList")[1].text.color = new Color(1f, 0.55f, 0.3f);
			}
		}
	}
}
