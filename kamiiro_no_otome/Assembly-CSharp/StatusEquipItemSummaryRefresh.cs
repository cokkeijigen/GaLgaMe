using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusEquipItemSummaryRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private ParameterContainer summaryParam;

	private Localize NameTextLoc;

	private Image itemImageImage;

	private Localize itemSummaryText;

	private Localize[] Panel_TypeTextLoc = new Localize[8];

	private Text[] Panel_PowerText = new Text[8];

	private Text[] Panel_SlotText = new Text[2];

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GetComponent<StatusManager>();
		summaryParam = statusManager.itemSummmaryWindow.GetComponent<ParameterContainer>();
		NameTextLoc = summaryParam.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize;
		itemImageImage = summaryParam.GetVariable<UguiImage>("itemImage").image;
		itemSummaryText = summaryParam.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i] = statusManager.itemSummmaryWindow.GetComponent<ParameterContainer>().GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
			Panel_PowerText[i] = statusManager.itemSummmaryWindow.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
		}
		for (int j = 0; j < 2; j++)
		{
			Panel_SlotText[j] = statusManager.itemSummmaryWindow.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("factorSlotText")[j].text;
		}
	}

	public override void OnStateBegin()
	{
		NameTextLoc.Term = "noStatus";
		itemImageImage.sprite = statusManager.noItemImageSprite;
		itemSummaryText.Term = "noSelectItemSummary";
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i].Term = "noStatus";
			Panel_PowerText[i].text = " ";
		}
		Panel_PowerText[0].transform.GetComponent<Localize>().enabled = false;
		Panel_PowerText[1].transform.GetComponent<Localize>().enabled = false;
		summaryParam.GetGameObject("statusPower7Text").SetActive(value: false);
		summaryParam.GetGameObject("factorSlotTextGroup").SetActive(value: true);
		int selectItemCategoryNum = statusManager.selectItemCategoryNum;
		int itemID = statusManager.selectItemId;
		int[] resultTotalPowerArray = new int[11];
		int[] resultTotalPowerLimitArray = new int[11];
		bool[] resultIsAddPercentArray = new bool[11];
		switch (selectItemCategoryNum)
		{
		case 7:
			if (statusManager.selectCharacterNum == 0)
			{
				ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData m) => m.itemID == itemID);
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == itemID && data.itemUniqueID == statusManager.selectItemUniqueId);
				if (itemWeaponData != null)
				{
					PlayerEquipDataManager.CalcWeaponEquipFactorTotalPower(haveWeaponData.weaponSetFactor, ref resultTotalPowerArray, ref resultTotalPowerLimitArray, ref resultIsAddPercentArray);
					NameTextLoc.Term = itemWeaponData.category.ToString() + itemID;
					itemImageImage.sprite = itemWeaponData.itemSprite;
					itemSummaryText.Term = itemWeaponData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "statusAttack";
					Panel_PowerText[0].text = itemWeaponData.attackPower.ToString();
					Panel_TypeTextLoc[1].Term = "statusMagicAttack";
					Panel_PowerText[1].text = itemWeaponData.magicAttackPower.ToString();
					Panel_TypeTextLoc[2].Term = "statusAccuracy";
					Panel_PowerText[2].text = itemWeaponData.accuracy.ToString();
					Panel_TypeTextLoc[3].Term = "statusCritical";
					Panel_PowerText[3].text = itemWeaponData.critical.ToString();
					Panel_TypeTextLoc[4].Term = "statusItemMp";
					Panel_PowerText[4].text = itemWeaponData.weaponMp.ToString();
					Panel_TypeTextLoc[5].Term = "statusRemainingDaysToCraft";
					Panel_PowerText[5].text = haveWeaponData.remainingDaysToCraft.ToString();
					Panel_TypeTextLoc[6].Term = "statusFactorSlot";
					Panel_SlotText[0].text = haveWeaponData.weaponSetFactor.Count.ToString();
					Panel_SlotText[1].text = itemWeaponData.factorSlot.ToString();
					Panel_TypeTextLoc[7].Term = "statusFactorLimitNum";
					Panel_PowerText[7].text = itemWeaponData.factorHaveLimit.ToString();
					CanvasGroup component = statusManager.itemEquipButtonGO.GetComponent<CanvasGroup>();
					if (haveWeaponData.equipCharacter == 0)
					{
						component.alpha = 0.5f;
						component.interactable = false;
						statusManager.itemEquipButtonLoc.Term = "buttonNowEquip";
					}
					else if (haveWeaponData.isItemStoreDisplay)
					{
						component.alpha = 0.5f;
						component.interactable = false;
						statusManager.itemEquipButtonLoc.Term = "summaryStoreDisplay";
					}
					else
					{
						component.alpha = 1f;
						component.interactable = true;
						statusManager.itemEquipButtonLoc.Term = "buttonEquip";
					}
				}
			}
			else
			{
				ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData m) => m.itemID == itemID);
				HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData data) => data.itemID == itemID);
				if (itemPartyWeaponData != null)
				{
					PlayerEquipDataManager.CalcWeaponEquipFactorTotalPower(havePartyWeaponData.weaponSetFactor, ref resultTotalPowerArray, ref resultTotalPowerLimitArray, ref resultIsAddPercentArray);
					NameTextLoc.Term = itemPartyWeaponData.category.ToString() + itemID;
					itemImageImage.sprite = itemPartyWeaponData.itemSprite;
					itemSummaryText.Term = itemPartyWeaponData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "statusAttack";
					Panel_PowerText[0].text = itemPartyWeaponData.attackPower.ToString();
					Panel_TypeTextLoc[1].Term = "statusMagicAttack";
					Panel_PowerText[1].text = itemPartyWeaponData.magicAttackPower.ToString();
					Panel_TypeTextLoc[2].Term = "statusAccuracy";
					Panel_PowerText[2].text = itemPartyWeaponData.accuracy.ToString();
					Panel_TypeTextLoc[3].Term = "statusCritical";
					Panel_PowerText[3].text = itemPartyWeaponData.critical.ToString();
					Panel_TypeTextLoc[4].Term = "statusComboProbability";
					Panel_PowerText[4].text = itemPartyWeaponData.comboProbability.ToString();
					Panel_TypeTextLoc[5].Term = "empty";
					Panel_PowerText[5].text = "";
					Panel_TypeTextLoc[6].Term = "statusFactorSlot";
					Panel_SlotText[0].text = havePartyWeaponData.weaponSetFactor.Count.ToString();
					Panel_SlotText[1].text = itemPartyWeaponData.factorSlot.ToString();
					Panel_TypeTextLoc[7].Term = "statusFactorLimitNum";
					Panel_PowerText[7].text = itemPartyWeaponData.factorHaveLimit.ToString();
				}
			}
			break;
		case 8:
			if (statusManager.selectCharacterNum == 0)
			{
				ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData m) => m.itemID == itemID);
				HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == itemID && data.itemUniqueID == statusManager.selectItemUniqueId);
				if (itemArmorData != null)
				{
					PlayerEquipDataManager.CalcWeaponEquipFactorTotalPower(haveArmorData.armorSetFactor, ref resultTotalPowerArray, ref resultTotalPowerLimitArray, ref resultIsAddPercentArray);
					NameTextLoc.Term = "armor" + itemID;
					itemImageImage.sprite = itemArmorData.itemSprite;
					itemSummaryText.Term = "armor" + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "statusDefense";
					Panel_PowerText[0].text = itemArmorData.defensePower.ToString();
					Panel_TypeTextLoc[1].Term = "statusMagicDefense";
					Panel_PowerText[1].text = itemArmorData.magicDefensePower.ToString();
					Panel_TypeTextLoc[2].Term = "statusEvasion";
					Panel_PowerText[2].text = itemArmorData.evasion.ToString();
					Panel_TypeTextLoc[3].Term = "statusAbnormalResist";
					Panel_PowerText[3].text = itemArmorData.abnormalResist.ToString();
					Panel_TypeTextLoc[4].Term = "statusRecoveryMpAdd";
					Panel_PowerText[4].text = itemArmorData.recoveryMp.ToString();
					Panel_TypeTextLoc[5].Term = "statusRemainingDaysToCraft";
					Panel_PowerText[5].text = haveArmorData.remainingDaysToCraft.ToString();
					Panel_TypeTextLoc[6].Term = "statusFactorSlot";
					Panel_SlotText[0].text = haveArmorData.armorSetFactor.Count.ToString();
					Panel_SlotText[1].text = itemArmorData.factorSlot.ToString();
					Panel_TypeTextLoc[7].Term = "statusFactorLimitNum";
					Panel_PowerText[7].text = itemArmorData.factorHaveLimit.ToString();
					CanvasGroup component2 = statusManager.itemEquipButtonGO.GetComponent<CanvasGroup>();
					if (haveArmorData.equipCharacter == 0)
					{
						component2.alpha = 0.5f;
						component2.interactable = false;
						statusManager.itemEquipButtonLoc.Term = "buttonNowEquip";
					}
					else if (haveArmorData.isItemStoreDisplay)
					{
						component2.alpha = 0.5f;
						component2.interactable = false;
						statusManager.itemEquipButtonLoc.Term = "summaryStoreDisplay";
					}
					else
					{
						component2.alpha = 1f;
						component2.interactable = true;
						statusManager.itemEquipButtonLoc.Term = "buttonEquip";
					}
				}
			}
			else
			{
				ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData m) => m.itemID == itemID);
				HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData data) => data.itemID == itemID);
				if (itemPartyArmorData != null)
				{
					NameTextLoc.Term = "armor" + itemID;
					itemImageImage.sprite = itemPartyArmorData.itemSprite;
					itemSummaryText.Term = "armor" + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "statusDefense";
					Panel_PowerText[0].text = itemPartyArmorData.defensePower.ToString();
					Panel_TypeTextLoc[1].Term = "statusMagicDefense";
					Panel_PowerText[1].text = itemPartyArmorData.magicDefensePower.ToString();
					Panel_TypeTextLoc[2].Term = "statusEvasion";
					Panel_PowerText[2].text = itemPartyArmorData.evasion.ToString();
					Panel_TypeTextLoc[3].Term = "statusAbnormalResist";
					Panel_PowerText[3].text = itemPartyArmorData.abnormalResist.ToString();
					Panel_TypeTextLoc[4].Term = "statusRecoveryMpAdd";
					Panel_PowerText[4].text = itemPartyArmorData.recoveryMp.ToString();
					Panel_TypeTextLoc[5].Term = "empty";
					Panel_PowerText[5].text = "";
					Panel_TypeTextLoc[6].Term = "statusFactorSlot";
					Panel_SlotText[0].text = havePartyArmorData.armorSetFactor.Count.ToString();
					Panel_SlotText[1].text = itemPartyArmorData.factorSlot.ToString();
					Panel_TypeTextLoc[7].Term = "statusFactorLimitNum";
					Panel_PowerText[7].text = itemPartyArmorData.factorHaveLimit.ToString();
				}
			}
			break;
		case 9:
		{
			ItemAccessoryData itemAccessoryData = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData m) => m.itemID == itemID);
			statusManager.itemEquipButtonGO.GetComponent<CanvasGroup>();
			if (itemAccessoryData != null)
			{
				NameTextLoc.Term = itemAccessoryData.category.ToString() + itemID;
				itemImageImage.sprite = itemAccessoryData.itemSprite;
				itemSummaryText.Term = itemAccessoryData.category.ToString() + itemID + "_summary";
				Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemAccessoryData.powerType;
				Panel_PowerText[0].text = " ";
				Panel_TypeTextLoc[1].Term = "skillPower";
				switch (itemAccessoryData.powerType)
				{
				case ItemAccessoryData.PowerType.poisonNoEffect:
				case ItemAccessoryData.PowerType.paralyzeNoEffect:
				case ItemAccessoryData.PowerType.abnormalStateNoEffect:
				case ItemAccessoryData.PowerType.debuffNoEffect:
					Panel_PowerText[1].text = itemAccessoryData.itemPower + " ％";
					break;
				case ItemAccessoryData.PowerType.abnormalResistUp:
					Panel_PowerText[1].text = "＋" + itemAccessoryData.itemPower;
					break;
				case ItemAccessoryData.PowerType.vampire:
				case ItemAccessoryData.PowerType.skillCritical:
				case ItemAccessoryData.PowerType.skillPowerUp:
				case ItemAccessoryData.PowerType.itemDiscoveryUp:
					Panel_PowerText[1].text = "＋" + itemAccessoryData.itemPower + " ％";
					break;
				case ItemAccessoryData.PowerType.agilityUp:
				case ItemAccessoryData.PowerType.weakAttackUp:
				case ItemAccessoryData.PowerType.getExpUp:
				case ItemAccessoryData.PowerType.getMoneyUp:
					Panel_PowerText[1].text = "＋" + (itemAccessoryData.itemPower - 100) + " ％";
					break;
				}
				if (itemAccessoryData.category == ItemAccessoryData.Category.earRing)
				{
					Panel_TypeTextLoc[2].Term = "itemTypeSummary_edenOnly";
				}
				summaryParam.GetGameObject("statusPower7Text").SetActive(value: true);
				summaryParam.GetGameObject("factorSlotTextGroup").SetActive(value: false);
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
}
