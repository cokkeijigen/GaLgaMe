using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class MergeInfoAndStatusRefresh : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer selectItemInfoParam = craftCanvasManager.selectItemInfoParam;
		selectItemInfoParam.GetGameObject("needWorkShopLvGo").SetActive(value: true);
		selectItemInfoParam.GetGameObject("needInheritWorkShopLvGo").SetActive(value: false);
		selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.gameObject.SetActive(value: true);
		craftCanvasManager.remainingDaysFrameGo.SetActive(value: false);
		craftCanvasManager.mergeInheritButtonGo.GetComponent<CanvasGroup>().interactable = true;
		craftCanvasManager.mergeInheritButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		craftCanvasManager.craftStartButtonGo.GetComponent<CanvasGroup>().interactable = true;
		craftCanvasManager.craftStartButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		craftCanvasManager.craftStartButtonGo.GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[0];
		craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftDetail";
		craftCanvasManager.craftStartButtonTextLocArray[1].Term = "buttonInheritCraftDetail";
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID);
			PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
			selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemPartyWeaponData.needWorkShopLevel.ToString();
			selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemPartyWeaponData.category.ToString()];
			string text2 = itemPartyWeaponData.category.ToString() + itemPartyWeaponData.itemID;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text2;
			selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemPartyWeaponData.itemSprite;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text2 + "_summary";
			craftManager.itemStatusParam[0] = itemPartyWeaponData.attackPower;
			craftManager.itemStatusParam[1] = itemPartyWeaponData.magicAttackPower;
			craftManager.itemStatusParam[2] = itemPartyWeaponData.accuracy;
			craftManager.itemStatusParam[3] = itemPartyWeaponData.critical;
			craftManager.itemStatusParam[4] = itemPartyWeaponData.comboProbability;
			craftManager.itemStatusParam[6] = itemPartyWeaponData.factorSlot;
			craftManager.itemStatusParam[7] = itemPartyWeaponData.factorHaveLimit;
			craftCanvasManager.ResetHaveFactorList(itemPartyWeaponData.factorHaveLimit, ChangeMaxVal: true);
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData2 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemPartyWeaponData.needWorkShopLevel <= craftWorkShopData2.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			craftCanvasManager.AdjustApplyButtonActive_MERGE();
			int nextID2 = craftManager.GetNextUpgradePartyItemID(craftManager.clickedItemID);
			Debug.Log("次の仲間継承アイテムID：" + nextID2);
			if (nextID2 != -1)
			{
				craftManager.nextItemID = nextID2;
				ItemPartyWeaponData itemPartyWeaponData2 = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == nextID2);
				if (PlayerFlagDataManager.recipeFlagDictionary[itemPartyWeaponData2.recipeFlagName])
				{
					craftCanvasManager.isInheritButtonLock = false;
					craftCanvasManager.craftInheritImageArray[1].sprite = itemPartyWeaponData2.itemInheritSprite;
				}
				else
				{
					craftCanvasManager.isInheritButtonLock = true;
					craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[0];
					craftCanvasManager.infoPowerUpLockTextLoc.Term = "buttonInheritCraftDetailLocked";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[1];
				craftCanvasManager.mergeInheritButtonGo.GetComponent<CanvasGroup>().interactable = false;
				craftCanvasManager.mergeInheritButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
				craftCanvasManager.craftStartButtonTextLocArray[1].Term = "buttonInheritCraftMax";
				craftCanvasManager.infoPowerUpLockTextLoc.Term = "buttonInheritCraftMax";
			}
			break;
		}
		case 1:
		{
			ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID);
			PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
			selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemPartyArmorData.needWorkShopLevel.ToString();
			selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			string text = "armor" + itemPartyArmorData.itemID;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
			selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemPartyArmorData.itemSprite;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text + "_summary";
			craftManager.itemStatusParam[0] = itemPartyArmorData.defensePower;
			craftManager.itemStatusParam[1] = itemPartyArmorData.magicDefensePower;
			craftManager.itemStatusParam[2] = itemPartyArmorData.evasion;
			craftManager.itemStatusParam[3] = itemPartyArmorData.abnormalResist;
			craftManager.itemStatusParam[4] = itemPartyArmorData.recoveryMp;
			craftManager.itemStatusParam[6] = itemPartyArmorData.factorSlot;
			craftManager.itemStatusParam[7] = itemPartyArmorData.factorHaveLimit;
			craftCanvasManager.ResetHaveFactorList(itemPartyArmorData.factorHaveLimit, ChangeMaxVal: true);
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemPartyArmorData.needWorkShopLevel <= craftWorkShopData.workShopLv;
			craftCanvasManager.isFurnaceLvEnough = true;
			SetSummaryAlertGoVisible();
			craftCanvasManager.AdjustApplyButtonActive_MERGE();
			int nextID = craftManager.GetNextUpgradePartyItemID(craftManager.clickedItemID);
			Debug.Log("次の仲間継承アイテムID：" + nextID);
			if (nextID != -1)
			{
				craftManager.nextItemID = nextID;
				ItemPartyArmorData itemPartyArmorData2 = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == nextID);
				if (PlayerFlagDataManager.recipeFlagDictionary[itemPartyArmorData2.recipeFlagName])
				{
					craftCanvasManager.isInheritButtonLock = false;
					craftCanvasManager.craftInheritImageArray[1].sprite = itemPartyArmorData2.itemInheritSprite;
				}
				else
				{
					craftCanvasManager.isInheritButtonLock = true;
					craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[0];
					craftCanvasManager.infoPowerUpLockTextLoc.Term = "buttonInheritCraftDetailLocked";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[1];
				craftCanvasManager.mergeInheritButtonGo.GetComponent<CanvasGroup>().interactable = false;
				craftCanvasManager.mergeInheritButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
				craftCanvasManager.craftStartButtonTextLocArray[1].Term = "buttonInheritCraftMax";
				craftCanvasManager.infoPowerUpLockTextLoc.Term = "buttonInheritCraftMax";
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
	}
}
