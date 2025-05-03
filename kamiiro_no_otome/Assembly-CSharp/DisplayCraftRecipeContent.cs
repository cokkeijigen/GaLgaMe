using System;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DisplayCraftRecipeContent : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	private CraftTalkManager craftTalkManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	public StateLink stateLink;

	public StateLink stateLink_MagicEdit;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		newCraftCanvasManager.craftQuantity = 1;
		newCraftCanvasManager.multiNumText.text = "1";
		craftManager.canvasGroupArray[0].gameObject.SetActive(value: false);
		craftManager.canvasGroupArray[1].gameObject.SetActive(value: true);
		craftCanvasManager.craftNeedItemGoArray[3].SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[0].SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -402f);
		craftCanvasManager.craftWindowGoArray[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -597f);
		craftTalkManager.TalkBalloonItemSelectAfter();
		bool flag = false;
		string selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
		if (!(selectCraftCanvasName == "craft"))
		{
			if (selectCraftCanvasName == "newCraft")
			{
				flag = true;
			}
		}
		else
		{
			flag = false;
		}
		int num = 0;
		int num2 = 0;
		int quantity = 1;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemWeaponData itemWeaponData = null;
			if (!craftCanvasManager.isPowerUpCraft)
			{
				itemWeaponData = (craftCanvasManager.isRemainingDaysZero ? GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.nextItemID) : ((!craftCanvasManager.isCompleteEnhanceCount) ? GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID) : GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID)));
			}
			else
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.nextItemID);
				craftManager.craftCommandTypeLoc.Term = "buttonInheritCraftMini";
			}
			craftCanvasManager.itemWeaponData = itemWeaponData;
			selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
			if (!(selectCraftCanvasName == "craft"))
			{
				if (selectCraftCanvasName == "newCraft")
				{
					num = itemWeaponData.needMaterialNewerList.Count;
				}
			}
			else if (craftCanvasManager.isPowerUpCraft)
			{
				num = itemWeaponData.needMaterialList.Count;
			}
			else if (!craftCanvasManager.isRemainingDaysZero)
			{
				num = ((!craftCanvasManager.isCompleteEnhanceCount) ? itemWeaponData.needMaterialEditList.Count : itemWeaponData.needMaterialEditList.Count);
			}
			else
			{
				num = itemWeaponData.needMaterialNewerList.Count;
				float num3 = (float)itemWeaponData.factorSlot / 2f;
				num3 = (float)Math.Round(num3, MidpointRounding.AwayFromZero);
				quantity = (int)num3;
				newCraftCanvasManager.craftQuantity = (int)num3;
			}
			num2 = 0;
			for (int m = 0; m < num; m++)
			{
				craftCanvasManager.SetRecipeContent(m, quantity);
				num2++;
			}
			for (int n = num2; n < 4; n++)
			{
				craftCanvasManager.craftNeedItemGoArray[n].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[n] = true;
			}
			CraftWorkShopData craftWorkShopData3 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemWeaponData.needWorkShopLevel <= craftWorkShopData3.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			if (PlayerNonSaveDataManager.selectCraftCanvasName == "craft")
			{
				craftCanvasManager.AdjustApplyButtonActive();
			}
			else
			{
				craftCanvasManager.AdjustApplyButtonActive_NEW();
			}
			craftCanvasManager.ResetHaveFactorList(itemWeaponData.factorHaveLimit, ChangeMaxVal: true);
			SetInfoWindowType(0);
			break;
		}
		case 1:
		{
			ItemArmorData itemArmorData = null;
			itemArmorData = (craftCanvasManager.isPowerUpCraft ? GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.nextItemID) : (craftCanvasManager.isRemainingDaysZero ? GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.nextItemID) : ((!craftCanvasManager.isCompleteEnhanceCount) ? GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID) : GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID))));
			craftCanvasManager.itemArmorData = itemArmorData;
			selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
			if (!(selectCraftCanvasName == "craft"))
			{
				if (selectCraftCanvasName == "newCraft")
				{
					num = itemArmorData.needMaterialNewerList.Count;
				}
			}
			else if (craftCanvasManager.isPowerUpCraft)
			{
				num = itemArmorData.needMaterialList.Count;
			}
			else if (!craftCanvasManager.isRemainingDaysZero)
			{
				num = ((!craftCanvasManager.isCompleteEnhanceCount) ? itemArmorData.needMaterialEditList.Count : itemArmorData.needMaterialEditList.Count);
			}
			else
			{
				num = itemArmorData.needMaterialNewerList.Count;
				float num6 = (float)itemArmorData.factorSlot / 2f;
				num6 = (float)Math.Round(num6, MidpointRounding.AwayFromZero);
				quantity = (int)num6;
				newCraftCanvasManager.craftQuantity = (int)num6;
			}
			num2 = 0;
			for (int num7 = 0; num7 < num; num7++)
			{
				craftCanvasManager.SetRecipeContent(num7, quantity);
				num2++;
			}
			for (int num8 = num2; num8 < 4; num8++)
			{
				craftCanvasManager.craftNeedItemGoArray[num8].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[num8] = true;
			}
			CraftWorkShopData craftWorkShopData5 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemArmorData.needWorkShopLevel <= craftWorkShopData5.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			if (PlayerNonSaveDataManager.selectCraftCanvasName == "craft")
			{
				craftCanvasManager.AdjustApplyButtonActive();
			}
			else
			{
				craftCanvasManager.AdjustApplyButtonActive_NEW();
			}
			craftCanvasManager.ResetHaveFactorList(itemArmorData.factorHaveLimit, ChangeMaxVal: true);
			SetInfoWindowType(0);
			break;
		}
		case 2:
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.itemCanMakeMaterialData = itemCanMakeMaterialData;
			num = itemCanMakeMaterialData.needMaterialList.Count;
			num2 = 0;
			for (int num4 = 0; num4 < num; num4++)
			{
				craftCanvasManager.SetRecipeContent(num4);
				num2++;
			}
			for (int num5 = num2; num5 < 4; num5++)
			{
				craftCanvasManager.craftNeedItemGoArray[num5].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[num5] = true;
			}
			ParameterContainer component3 = craftManager.canvasGroupArray[1].GetComponent<ParameterContainer>();
			component3.GetGameObject("needFurnaceLvGo").SetActive(value: true);
			component3.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemCanMakeMaterialData.needWorkShopLevel.ToString();
			component3.GetVariable<UguiTextVariable>("needFurnaceText").text.text = itemCanMakeMaterialData.needFurnaceLevel.ToString();
			component3.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCanMakeMaterialData.category.ToString()];
			string text3 = itemCanMakeMaterialData.category.ToString() + itemCanMakeMaterialData.itemID;
			component3.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text3;
			component3.GetVariable<UguiImage>("itemImage").image.sprite = itemCanMakeMaterialData.itemSprite;
			component3.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text3 + "_summary";
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData4 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemCanMakeMaterialData.needWorkShopLevel <= craftWorkShopData4.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = itemCanMakeMaterialData.needFurnaceLevel <= craftWorkShopData4.furnaceLv;
			SetSummaryAlertGoVisible();
			craftCanvasManager.AdjustApplyButtonActive_NEW();
			SetInfoWindowType(1);
			break;
		}
		case 3:
		{
			ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.itemEventItemData = itemEventItemData;
			num = itemEventItemData.needMaterialList.Count;
			num2 = 0;
			for (int k = 0; k < num; k++)
			{
				craftCanvasManager.SetRecipeContent(k);
				num2++;
			}
			for (int l = num2; l < 4; l++)
			{
				craftCanvasManager.craftNeedItemGoArray[l].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[l] = true;
			}
			ParameterContainer component2 = craftManager.canvasGroupArray[1].GetComponent<ParameterContainer>();
			component2.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			component2.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemEventItemData.needWorkShopLevel.ToString();
			component2.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemEventItemData.category.ToString()];
			string text2 = "eventItem" + itemEventItemData.itemID;
			component2.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text2;
			component2.GetVariable<UguiImage>("itemImage").image.sprite = itemEventItemData.itemSprite;
			component2.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text2 + "_summary";
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData2 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemEventItemData.needWorkShopLevel <= craftWorkShopData2.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			string key2 = itemEventItemData.category.ToString() + itemEventItemData.itemID;
			bool isUniqueItem2 = PlayerFlagDataManager.keyItemFlagDictionary[key2];
			craftCanvasManager.isUniqueItem = isUniqueItem2;
			craftCanvasManager.AdjustApplyButtonActive_NEW();
			SetInfoWindowType(1);
			break;
		}
		case 4:
		{
			ItemCampItemData itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.itemCampItemData = itemCampItemData;
			num = itemCampItemData.needMaterialList.Count;
			num2 = 0;
			for (int i = 0; i < num; i++)
			{
				craftCanvasManager.SetRecipeContent(i);
				num2++;
			}
			for (int j = num2; j < 4; j++)
			{
				craftCanvasManager.craftNeedItemGoArray[j].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[j] = true;
			}
			ParameterContainer component = craftManager.canvasGroupArray[1].GetComponent<ParameterContainer>();
			component.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			component.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemCampItemData.needWorkShopLevel.ToString();
			component.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCampItemData.category.ToString()];
			string text = "campItem" + itemCampItemData.itemID;
			component.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
			component.GetVariable<UguiImage>("itemImage").image.sprite = itemCampItemData.itemSprite;
			component.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text + "_summary";
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemCampItemData.needWorkShopLevel <= craftWorkShopData.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			string key = "campItem" + itemCampItemData.itemID;
			bool isUniqueItem = PlayerFlagDataManager.keyItemFlagDictionary[key];
			craftCanvasManager.isUniqueItem = isUniqueItem;
			craftCanvasManager.AdjustApplyButtonActive_NEW();
			SetInfoWindowType(1);
			break;
		}
		}
		craftAddOnManager.MagicMaterialListRefresh();
		if ((craftManager.selectCategoryNum == 0 || craftManager.selectCategoryNum == 1) && !flag)
		{
			Transition(stateLink_MagicEdit);
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

	private void SetInfoWindowType(int typeNum)
	{
		int enableAddOnLv = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv;
		switch (typeNum)
		{
		case 0:
			if (!craftCanvasManager.isPowerUpCraft && enableAddOnLv > 0)
			{
				craftManager.craftAddOnGoArray[0].SetActive(value: true);
				craftManager.craftAddOnGoArray[1].SetActive(value: false);
			}
			else
			{
				craftManager.craftAddOnGoArray[0].SetActive(value: false);
				craftManager.craftAddOnGoArray[1].SetActive(value: true);
				if (craftCanvasManager.isPowerUpCraft)
				{
					craftManager.addOnLockTextLoc.Term = "craftAddOnLock_Inherit";
				}
				else
				{
					craftManager.addOnLockTextLoc.Term = "craftAddOnLock_Craft";
				}
			}
			craftCanvasManager.craftWindowGoArray[1].SetActive(value: true);
			craftCanvasManager.craftWindowGoArray[2].SetActive(value: true);
			craftCanvasManager.ResetGetFactorInfo();
			craftCanvasManager.craftWindowGoArray[3].SetActive(value: false);
			craftManager.addOnHeaderTextLoc.Term = "itemTypeSummary_addOnMaterial";
			break;
		case 1:
			craftCanvasManager.craftWindowGoArray[1].SetActive(value: false);
			craftCanvasManager.craftWindowGoArray[2].SetActive(value: false);
			switch (craftManager.selectCategoryNum)
			{
			case 2:
				craftCanvasManager.craftWindowGoArray[3].SetActive(value: true);
				break;
			case 3:
			case 4:
				craftCanvasManager.craftWindowGoArray[3].SetActive(value: false);
				break;
			}
			break;
		}
	}

	private void SetSummaryAlertGoVisible()
	{
		ParameterContainer component = craftManager.canvasGroupArray[1].GetComponent<ParameterContainer>();
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "merge":
			if (craftCanvasManager.isWorkShopLvEnough)
			{
				component.GetGameObjectList("alertGoList")[0].SetActive(value: false);
				component.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 1f, 1f);
				component.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 1f, 1f);
				component.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 1f, 1f);
			}
			else
			{
				component.GetGameObjectList("alertGoList")[0].SetActive(value: true);
				component.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 0.55f, 0.3f);
				component.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 0.55f, 0.3f);
				component.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 0.55f, 0.3f);
			}
			break;
		case "newCraft":
			if (craftCanvasManager.isWorkShopLvEnough)
			{
				component.GetGameObjectList("alertGoList")[0].SetActive(value: false);
				component.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 1f, 1f);
				component.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 1f, 1f);
				component.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 1f, 1f);
			}
			else
			{
				component.GetGameObjectList("alertGoList")[0].SetActive(value: true);
				component.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 0.55f, 0.3f);
				component.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 0.55f, 0.3f);
				component.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 0.55f, 0.3f);
			}
			if (craftManager.selectCategoryNum == 2)
			{
				if (craftCanvasManager.isFurnaceLvEnough)
				{
					component.GetGameObjectList("alertGoList")[1].SetActive(value: false);
					component.GetVariableList<UguiImage>("needLvIconList")[1].image.color = new Color(1f, 1f, 1f);
					component.GetVariable<UguiTextVariable>("needFurnaceText").text.color = new Color(1f, 1f, 1f);
					component.GetVariableList<UguiTextVariable>("needSummaryTextList")[1].text.color = new Color(1f, 1f, 1f);
				}
				else
				{
					component.GetGameObjectList("alertGoList")[1].SetActive(value: true);
					component.GetVariableList<UguiImage>("needLvIconList")[1].image.color = new Color(1f, 0.55f, 0.3f);
					component.GetVariable<UguiTextVariable>("needFurnaceText").text.color = new Color(1f, 0.55f, 0.3f);
					component.GetVariableList<UguiTextVariable>("needSummaryTextList")[1].text.color = new Color(1f, 0.55f, 0.3f);
				}
			}
			break;
		}
	}
}
