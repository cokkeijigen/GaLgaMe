using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CraftInfoAndStatusRefresh : StateBehaviour
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
		craftCanvasManager.remainingDaysFrameGo.SetActive(value: true);
		craftCanvasManager.remainingDaysAlertGo.SetActive(value: false);
		craftCanvasManager.isRemainingDaysZero = false;
		craftCanvasManager.isPowerUpCraft = false;
		craftCanvasManager.isAutoEvolutionCraft = false;
		craftCanvasManager.isNextItemNothing = false;
		craftCanvasManager.craftStartButtonGo.GetComponent<CanvasGroup>().interactable = true;
		craftCanvasManager.craftStartButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		craftCanvasManager.craftStartButtonGo.GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[0];
		craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftDetail";
		craftCanvasManager.craftStartButtonTextLocArray[1].Term = "buttonInheritCraftDetail";
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
			selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemWeaponData.needWorkShopLevel.ToString();
			selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			string text2 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text2;
			selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemWeaponData.itemSprite;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text2 + "_summary";
			craftManager.itemStatusParam[0] = itemWeaponData.attackPower;
			craftManager.itemStatusParam[1] = itemWeaponData.magicAttackPower;
			craftManager.itemStatusParam[2] = itemWeaponData.accuracy;
			craftManager.itemStatusParam[3] = itemWeaponData.critical;
			craftManager.itemStatusParam[4] = itemWeaponData.weaponMp;
			craftManager.itemStatusParam[6] = itemWeaponData.factorSlot;
			craftManager.itemStatusParam[7] = itemWeaponData.factorHaveLimit;
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData2 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemWeaponData.needWorkShopLevel <= craftWorkShopData2.workShopLv;
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
			craftCanvasManager.remainingDaysNumTextArray[1].text = haveWeaponData.remainingDaysToCraft.ToString();
			if (haveWeaponData.remainingDaysToCraft == 0)
			{
				craftCanvasManager.remainingDaysAlertGo.SetActive(value: true);
				craftCanvasManager.isRemainingDaysZero = true;
			}
			int nextID2 = craftManager.GetNextUpgradeItemID(craftManager.clickedItemID);
			Debug.Log("次の進化or継承アイテムID：" + nextID2);
			if (nextID2 != -1)
			{
				craftManager.nextItemID = nextID2;
				ItemWeaponData itemWeaponData2 = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == nextID2);
				if (PlayerFlagDataManager.recipeFlagDictionary[itemWeaponData2.recipeFlagName])
				{
					craftCanvasManager.isInheritButtonLock = false;
					if (haveWeaponData.remainingDaysToCraft > 0)
					{
						if (itemWeaponData.factorSlot <= haveWeaponData.itemEnhanceCount)
						{
							craftCanvasManager.isCompleteEnhanceCount = true;
							craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonEvolutionCraftDetail";
						}
						else
						{
							craftCanvasManager.isCompleteEnhanceCount = false;
							craftCanvasManager.isInheritButtonLock = true;
							craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftDetail";
						}
					}
					else
					{
						craftCanvasManager.isPowerUpCraft = true;
						craftCanvasManager.isCompleteEnhanceCount = false;
						craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonInheritCraftDetail";
						craftCanvasManager.craftStartButtonGo.GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[1];
					}
				}
				else
				{
					craftCanvasManager.isInheritButtonLock = true;
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonEvolutionCraftLocked";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.isCompleteEnhanceCount = false;
				craftCanvasManager.isNextItemNothing = true;
				if (haveWeaponData.remainingDaysToCraft > 0)
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftDetail";
				}
				else
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftImpossible";
				}
			}
			break;
		}
		case 1:
		{
			ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
			selectItemInfoParam.GetGameObject("needFurnaceLvGo").SetActive(value: false);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.text = itemArmorData.needWorkShopLevel.ToString();
			selectItemInfoParam.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			string text = "armor" + itemArmorData.itemID;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
			selectItemInfoParam.GetVariable<UguiImage>("itemImage").image.sprite = itemArmorData.itemSprite;
			selectItemInfoParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text + "_summary";
			craftManager.itemStatusParam[0] = itemArmorData.defensePower;
			craftManager.itemStatusParam[1] = itemArmorData.magicDefensePower;
			craftManager.itemStatusParam[2] = itemArmorData.evasion;
			craftManager.itemStatusParam[3] = itemArmorData.abnormalResist;
			craftManager.itemStatusParam[4] = itemArmorData.recoveryMp;
			craftManager.itemStatusParam[6] = itemArmorData.factorSlot;
			craftManager.itemStatusParam[7] = itemArmorData.factorHaveLimit;
			craftCanvasManager.ShowStatusParameters();
			CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemArmorData.needWorkShopLevel <= craftWorkShopData.workShopLv;
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
			craftCanvasManager.remainingDaysNumTextArray[1].text = haveArmorData.remainingDaysToCraft.ToString();
			if (haveArmorData.remainingDaysToCraft == 0)
			{
				craftCanvasManager.remainingDaysAlertGo.SetActive(value: true);
				craftCanvasManager.isRemainingDaysZero = true;
			}
			craftCanvasManager.craftInheritImageArray[0].sprite = itemArmorData.itemInheritSprite;
			int nextID = craftManager.GetNextUpgradeItemID(craftManager.clickedItemID);
			if (nextID != -1)
			{
				craftManager.nextItemID = nextID;
				ItemArmorData itemArmorData2 = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == nextID);
				if (PlayerFlagDataManager.recipeFlagDictionary[itemArmorData2.recipeFlagName])
				{
					craftCanvasManager.isInheritButtonLock = false;
					craftCanvasManager.craftInheritImageArray[1].sprite = itemArmorData2.itemInheritSprite;
					if (haveArmorData.remainingDaysToCraft > 0)
					{
						if (itemArmorData.factorSlot <= haveArmorData.itemEnhanceCount)
						{
							craftCanvasManager.isCompleteEnhanceCount = true;
							craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonEvolutionCraftDetail";
						}
						else
						{
							craftCanvasManager.isCompleteEnhanceCount = false;
							craftCanvasManager.isInheritButtonLock = true;
							craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftDetail";
						}
					}
					else
					{
						craftCanvasManager.isPowerUpCraft = true;
						craftCanvasManager.isCompleteEnhanceCount = false;
						craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonInheritCraftDetail";
						craftCanvasManager.craftStartButtonGo.GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[1];
					}
				}
				else
				{
					craftCanvasManager.isInheritButtonLock = true;
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonEvolutionCraftLocked";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.isCompleteEnhanceCount = false;
				craftCanvasManager.isNextItemNothing = true;
				if (haveArmorData.remainingDaysToCraft > 0)
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftDetail";
				}
				else
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftImpossible";
				}
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
