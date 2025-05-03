using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftCanvasManager : SerializedMonoBehaviour
{
	private struct ItemListtoPowerSort
	{
		public int instanceID;

		public int factorPowerTotal;

		public int factorMagicPowerTotal;

		public int factorValueToatal;

		public int haveFactorCount;
	}

	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	public GameObject[] craftNeedItemGoArray;

	public GameObject[] craftWindowGoArray;

	public Image needCostFrameImage;

	public Sprite[] needCostFrameSpriteArray;

	public Localize[] infoWindowSummaryLocArray;

	public Image[] infoEditIconImage;

	public Localize[] infoEditTextLoc;

	public Text[] infoEditNumText;

	public CanvasGroup infoApplyButtonCanvasGroup;

	public ParameterContainer selectItemInfoParam;

	public GameObject remainingDaysFrameGo;

	public TextMeshProUGUI[] remainingDaysNumTextArray;

	public GameObject remainingDaysAlertGo;

	public GameObject infoFactorScrollContentGO;

	public TextMeshProUGUI[] infoFactorGroupText;

	public GameObject craftStartButtonGo;

	public GameObject mergeInheritButtonGo;

	public Localize[] craftStartButtonTextLocArray;

	public Sprite[] craftStartButtonSpriteArray;

	public Localize infoPowerUpLockTextLoc;

	public GameObject[] craftInheritGoArray;

	public Localize craftInheritButtonTextLoc;

	public Image[] craftInheritImageArray;

	public Sprite[] craftInheritLockSpriteArray;

	public Localize[] itemTextLocGroup;

	public Button[] applyButtonGroup;

	public Localize applyButtonLoc;

	public Localize[] GetFactorTypeLocGroup;

	public bool isUniqueItem;

	public bool isWorkShopLvEnough;

	public bool isFurnaceLvEnough;

	public bool isRemainingDaysZero;

	public bool isInheritButtonLock;

	public bool isPowerUpCraft;

	public bool isAutoEvolutionCraft;

	public bool isCompleteEnhanceCount;

	public bool isNextItemNothing;

	public bool isCraftComplete;

	public ItemWeaponData itemWeaponData;

	public ItemArmorData itemArmorData;

	public ItemCanMakeMaterialData itemCanMakeMaterialData;

	public ItemEventItemData itemEventItemData;

	public ItemCampItemData itemCampItemData;

	private void Awake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public void ShowStatusParameters()
	{
		int[] itemStatusParam = craftManager.itemStatusParam;
		ParameterContainer parameterContainer = craftCanvasManager.selectItemInfoParam;
		IList<I2LocalizeComponent> variableList = parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLoc");
		IList<UguiTextVariable> variableList2 = parameterContainer.GetVariableList<UguiTextVariable>("statusPowerText");
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			variableList[0].localize.Term = "statusAttack";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicAttack";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusAccuracy";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusCritical";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
			case "newCraft":
				variableList[4].localize.Term = "statusItemMp";
				break;
			case "merge":
				variableList[4].localize.Term = "statusComboProbability";
				break;
			}
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "empty";
			variableList2[5].text.text = "";
			variableList[6].localize.Term = "statusFactorSlot";
			variableList2[6].text.text = itemStatusParam[6].ToString();
			variableList[7].localize.Term = "statusFactorLimitNum";
			variableList2[7].text.text = itemStatusParam[7].ToString();
			break;
		case 1:
			variableList[0].localize.Term = "statusDefense";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicDefense";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusEvasion";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusAbnormalResist";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
			case "newCraft":
				variableList[4].localize.Term = "statusRecoveryMpAdd";
				break;
			case "merge":
				variableList[4].localize.Term = "statusRecoveryMpAdd";
				break;
			}
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "empty";
			variableList2[5].text.text = "";
			variableList[6].localize.Term = "statusFactorSlot";
			variableList2[6].text.text = itemStatusParam[6].ToString();
			variableList[7].localize.Term = "statusFactorLimitNum";
			variableList2[7].text.text = itemStatusParam[7].ToString();
			break;
		case 2:
		{
			variableList[0].localize.Term = "itemTypeSummary_canMakeMaterial";
			variableList2[0].text.text = "";
			ItemCanMakeMaterialData.Category category2 = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID).category;
			variableList[1].localize.Term = "itemTypeSummary_" + category2;
			variableList2[1].text.text = "";
			for (int j = 2; j < 8; j++)
			{
				variableList[j].localize.Term = "noStatus";
				variableList2[j].text.text = "";
			}
			break;
		}
		case 3:
		{
			variableList[0].localize.Term = "itemTypeSummary_eventItem";
			variableList2[0].text.text = "";
			for (int k = 1; k < 8; k++)
			{
				variableList[k].localize.Term = "noStatus";
				variableList2[k].text.text = "";
			}
			break;
		}
		case 4:
		{
			variableList[0].localize.Term = "itemTypeSummary_adventureKit";
			variableList2[0].text.text = "";
			ItemCampItemData.Category category = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID).category;
			variableList[1].localize.Term = "itemTypeSummary_" + category;
			variableList2[1].text.text = "";
			variableList2[2].text.text = itemCampItemData.power.ToString();
			switch (category)
			{
			case ItemCampItemData.Category.camp:
				variableList[2].localize.Term = "summaryDungeonRestBonus";
				variableList2[2].text.text = itemCampItemData.subPower.ToString();
				break;
			case ItemCampItemData.Category.lanthanum:
				variableList[2].localize.Term = "summaryDungeonBuffBonus";
				break;
			case ItemCampItemData.Category.charm:
				variableList[2].localize.Term = "summaryDungeonDeBuffBonus";
				break;
			case ItemCampItemData.Category.medicKit:
				variableList[2].localize.Term = "summaryDungeonFloorHeal";
				break;
			}
			for (int i = 3; i < 8; i++)
			{
				variableList[i].localize.Term = "noStatus";
				variableList2[i].text.text = "";
			}
			break;
		}
		}
	}

	public void ResetHaveFactorList(int MaxVal, bool ChangeMaxVal)
	{
		for (int num = infoFactorScrollContentGO.transform.childCount - 1; num >= 0; num--)
		{
			GameObject gameObject = infoFactorScrollContentGO.transform.GetChild(num).gameObject;
			if (gameObject.tag == "CustomScrollItem")
			{
				gameObject.transform.SetParent(craftManager.poolParentGO.transform);
				if (PoolManager.Pools["Craft Item Pool"].IsSpawned(gameObject.transform))
				{
					PoolManager.Pools["Craft Item Pool"].Despawn(gameObject.transform);
				}
			}
		}
		infoFactorGroupText[0].text = "0";
		if (ChangeMaxVal)
		{
			infoFactorGroupText[1].text = MaxVal.ToString();
		}
	}

	public void AdjustApplyButtonActive()
	{
		bool[] requiredItemENOUGH = craftManager.requiredItemENOUGH;
		bool flag = requiredItemENOUGH[0] && requiredItemENOUGH[1] && requiredItemENOUGH[2] && requiredItemENOUGH[3];
		CanvasGroup component = applyButtonGroup[0].GetComponent<CanvasGroup>();
		if (isPowerUpCraft)
		{
			if (flag && !isUniqueItem && isWorkShopLvEnough && isFurnaceLvEnough)
			{
				component.alpha = 1f;
				component.interactable = true;
			}
			else
			{
				component.alpha = 0.5f;
				component.interactable = false;
			}
		}
		else if (flag && !isUniqueItem && isWorkShopLvEnough && isFurnaceLvEnough)
		{
			component.alpha = 1f;
			component.interactable = true;
		}
		else
		{
			component.alpha = 0.5f;
			component.interactable = false;
		}
		if (isWorkShopLvEnough && isFurnaceLvEnough)
		{
			if (isPowerUpCraft)
			{
				if (component.interactable)
				{
					applyButtonLoc.Term = "buttonInheritCraftStart";
				}
				else
				{
					applyButtonLoc.Term = "buttonInheritCraftMaterialShotage";
				}
			}
			else if (craftCanvasManager.isCompleteEnhanceCount)
			{
				if (component.interactable)
				{
					applyButtonLoc.Term = "buttonEvolutionCraftStart";
				}
				else
				{
					applyButtonLoc.Term = "buttonEvolutionCraftMaterialShotage";
				}
			}
			else if (craftCanvasManager.isNextItemNothing)
			{
				if (component.interactable)
				{
					applyButtonLoc.Term = "buttonCraftNextItemNothing";
				}
				else
				{
					applyButtonLoc.Term = "buttonCraftStartMaterialShotage";
				}
			}
			else if (component.interactable)
			{
				applyButtonLoc.Term = "buttonCraftStart";
			}
			else
			{
				applyButtonLoc.Term = "buttonCraftStartMaterialShotage";
			}
		}
		else if (isPowerUpCraft)
		{
			applyButtonLoc.Term = "buttonInheritCraftImpossible";
		}
		else
		{
			applyButtonLoc.Term = "buttonCraftImpossible";
		}
		if (!isRemainingDaysZero)
		{
			return;
		}
		if (craftManager.CheckNextItemUnlockRecipe(craftManager.clickedItemID))
		{
			if (component.interactable)
			{
				applyButtonLoc.Term = "buttonInheritCraftStart";
			}
			else
			{
				applyButtonLoc.Term = "buttonInheritCraftMaterialShotage";
			}
		}
		else
		{
			applyButtonLoc.Term = "buttonRemainingDyasToCraftImpossible";
			component.alpha = 0.5f;
			component.interactable = false;
		}
	}

	public void AdjustApplyButtonActive_NEW()
	{
		bool[] requiredItemENOUGH = craftManager.requiredItemENOUGH;
		bool num = requiredItemENOUGH[0] && requiredItemENOUGH[1] && requiredItemENOUGH[2] && requiredItemENOUGH[3];
		CanvasGroup component = applyButtonGroup[0].GetComponent<CanvasGroup>();
		if (num && !isUniqueItem && isWorkShopLvEnough && isFurnaceLvEnough)
		{
			component.alpha = 1f;
			component.interactable = true;
		}
		else
		{
			component.alpha = 0.5f;
			component.interactable = false;
		}
		if (isUniqueItem)
		{
			switch (craftManager.selectCategoryNum)
			{
			case 3:
				applyButtonLoc.Term = "buttonNewCraftCreated";
				break;
			case 4:
				applyButtonLoc.Term = "buttonNewCraftHaved";
				break;
			}
		}
		else if (isWorkShopLvEnough && isFurnaceLvEnough)
		{
			if (component.interactable)
			{
				applyButtonLoc.Term = "buttonNewCraftStart";
			}
			else
			{
				applyButtonLoc.Term = "buttonNewCraftStart";
			}
		}
		else
		{
			applyButtonLoc.Term = "buttonNewCraftImpossible";
		}
	}

	public void AdjustApplyButtonActive_MERGE()
	{
		bool[] requiredItemENOUGH = craftManager.requiredItemENOUGH;
		bool num = requiredItemENOUGH[0] && requiredItemENOUGH[1] && requiredItemENOUGH[2] && requiredItemENOUGH[3];
		CanvasGroup component = applyButtonGroup[0].GetComponent<CanvasGroup>();
		if (num && isWorkShopLvEnough)
		{
			component.alpha = 1f;
			component.interactable = true;
		}
		else
		{
			component.alpha = 0.5f;
			component.interactable = false;
		}
		if (isWorkShopLvEnough)
		{
			if (isPowerUpCraft)
			{
				if (component.interactable)
				{
					applyButtonLoc.Term = "buttonMergeStart";
				}
				else
				{
					applyButtonLoc.Term = "buttonMergeMaterialShotage";
				}
			}
			else if (component.interactable)
			{
				applyButtonLoc.Term = "buttonEditStart";
			}
			else
			{
				applyButtonLoc.Term = "buttonEditMaterialShortage";
			}
		}
		else if (isPowerUpCraft)
		{
			applyButtonLoc.Term = "buttonMergeImpossible";
		}
		else
		{
			applyButtonLoc.Term = "buttonEditImpossible";
		}
	}

	public void SetRecipeContent(int index, int quantity = 1)
	{
		GameObject gameObject = null;
		ParameterContainer parameterContainer = null;
		string selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
		if (!(selectCraftCanvasName == "craft"))
		{
			if (selectCraftCanvasName == "newCraft")
			{
				gameObject = newCraftCanvasManager.newCraftNeedItemGoArray[index];
				parameterContainer = newCraftCanvasManager.newCraftNeedItemGoArray[index].GetComponent<ParameterContainer>();
			}
		}
		else
		{
			gameObject = craftNeedItemGoArray[index];
			parameterContainer = craftNeedItemGoArray[index].GetComponent<ParameterContainer>();
		}
		int num = 0;
		int num2 = 0;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
			if (!(selectCraftCanvasName == "craft"))
			{
				if (selectCraftCanvasName == "newCraft")
				{
					num = itemWeaponData.needMaterialNewerList[index].itemID;
					num2 = itemWeaponData.needMaterialNewerList[index].needNum;
				}
			}
			else if (isPowerUpCraft)
			{
				num = itemWeaponData.needMaterialList[index].itemID;
				num2 = itemWeaponData.needMaterialList[index].needNum;
			}
			else if (isRemainingDaysZero)
			{
				num = itemWeaponData.needMaterialList[index].itemID;
				num2 = itemWeaponData.needMaterialList[index].needNum;
			}
			else if (isCompleteEnhanceCount)
			{
				num = itemWeaponData.needMaterialEditList[index].itemID;
				num2 = itemWeaponData.needMaterialEditList[index].needNum;
			}
			else
			{
				num = itemWeaponData.needMaterialEditList[index].itemID;
				num2 = itemWeaponData.needMaterialEditList[index].needNum;
			}
			break;
		case 1:
			selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
			if (!(selectCraftCanvasName == "craft"))
			{
				if (selectCraftCanvasName == "newCraft")
				{
					num = itemArmorData.needMaterialNewerList[index].itemID;
					num2 = itemArmorData.needMaterialNewerList[index].needNum;
				}
			}
			else if (isPowerUpCraft)
			{
				num = itemArmorData.needMaterialList[index].itemID;
				num2 = itemArmorData.needMaterialList[index].needNum;
			}
			else if (isRemainingDaysZero)
			{
				num = itemArmorData.needMaterialList[index].itemID;
				num2 = itemArmorData.needMaterialList[index].needNum;
			}
			else if (isCompleteEnhanceCount)
			{
				num = itemArmorData.needMaterialEditList[index].itemID;
				num2 = itemArmorData.needMaterialEditList[index].needNum;
			}
			else
			{
				num = itemArmorData.needMaterialEditList[index].itemID;
				num2 = itemArmorData.needMaterialEditList[index].needNum;
			}
			break;
		case 2:
			num = itemCanMakeMaterialData.needMaterialList[index].itemID;
			num2 = itemCanMakeMaterialData.needMaterialList[index].needNum;
			break;
		case 3:
			num = itemEventItemData.needMaterialList[index].itemID;
			num2 = itemEventItemData.needMaterialList[index].needNum;
			break;
		case 4:
			num = itemCampItemData.needMaterialList[index].itemID;
			num2 = itemCampItemData.needMaterialList[index].needNum;
			break;
		}
		num2 *= quantity;
		string itemName = "";
		Sprite itemIconSprite = null;
		Sprite itemSprite = null;
		string CategoryName = "";
		if (craftManager.GetCommonDataFromItemID(num, ref itemName, ref itemIconSprite, ref CategoryName, ref itemSprite))
		{
			gameObject.transform.Find("Group").gameObject.SetActive(value: true);
			switch (CategoryName)
			{
			case "camp":
			case "lanthanum":
			case "charm":
			case "medicKit":
				parameterContainer.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "campItem" + num;
				break;
			default:
				parameterContainer.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = CategoryName + num;
				break;
			}
			parameterContainer.GetVariable<UguiImage>("iconImage").image.sprite = itemIconSprite;
			parameterContainer.GetVariable<UguiTextVariable>("needNumText").text.text = num2.ToString();
			int playerHaveItemNum = PlayerInventoryDataAccess.GetPlayerHaveItemNum(num);
			parameterContainer.GetVariable<UguiTextVariable>("haveNumText").text.text = playerHaveItemNum.ToString();
			parameterContainer.SetInt("itemId", num);
			GameObject gameObject2 = parameterContainer.GetGameObject("chooseButtonGo");
			if (gameObject2 != null)
			{
				gameObject2.gameObject.SetActive(value: false);
			}
			if (num2 <= playerHaveItemNum)
			{
				craftManager.requiredItemENOUGH[index] = true;
				parameterContainer.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.enableColor;
			}
			else
			{
				craftManager.requiredItemENOUGH[index] = false;
				parameterContainer.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.disableColor;
			}
			parameterContainer.GetVariable<UguiTextVariable>("nameText").text.fontStyle = FontStyle.Normal;
		}
	}

	public void ResetGetFactorInfo()
	{
		GetFactorTypeLocGroup[0].Term = "craftInfoRandom";
		GetFactorTypeLocGroup[1].Term = "addOnPower1";
		int num = 1;
		num = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv;
		GetFactorTypeLocGroup[2].Term = "craftGetFactorPower" + num;
	}

	public void ResetGetFactorInfo_ADDON()
	{
		if (craftAddOnManager.selectedMagicMatrialID[0] != 0)
		{
			int id = craftAddOnManager.selectedMagicMatrialID[0];
			string text = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == id).factorType.ToString();
			GetFactorTypeLocGroup[0].Term = "factor_" + text;
		}
		else
		{
			GetFactorTypeLocGroup[0].Term = "craftInfoRandom";
		}
		int num = 1;
		num = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv;
		if (craftAddOnManager.selectedMagicMatrialID[1] != 0)
		{
			int id2 = craftAddOnManager.selectedMagicMatrialID[1];
			int addOnPower = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == id2).addOnPower;
			int num2 = Mathf.Clamp(num - 2 + addOnPower, 1, 4);
			int num3 = Mathf.Clamp(num + addOnPower, 1, 4);
			GetFactorTypeLocGroup[1].Term = "craftGetFactorPower" + num2;
			GetFactorTypeLocGroup[2].Term = "craftGetFactorPower" + num3;
		}
		else
		{
			GetFactorTypeLocGroup[1].Term = "craftGetFactorPower1";
			GetFactorTypeLocGroup[2].Term = "craftGetFactorPower" + num;
		}
	}

	public bool GetInheritButtonLock()
	{
		return isInheritButtonLock;
	}
}
