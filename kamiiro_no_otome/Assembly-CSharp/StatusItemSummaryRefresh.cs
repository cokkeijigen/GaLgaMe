using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusItemSummaryRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private ParameterContainer summaryParameter;

	private Localize NameTextLoc;

	private Image itemImageImage;

	private Localize itemSummaryText;

	private Localize[] Panel_TypeTextLoc = new Localize[8];

	private Text[] Panel_PowerText = new Text[8];

	public bool noStatusSwitch;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GetComponent<StatusManager>();
		summaryParameter = statusManager.itemSummmaryWindow.GetComponent<ParameterContainer>();
		NameTextLoc = summaryParameter.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize;
		itemImageImage = summaryParameter.GetVariable<UguiImage>("itemImage").image;
		itemSummaryText = summaryParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i] = statusManager.itemSummmaryWindow.GetComponent<ParameterContainer>().GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
			Panel_PowerText[i] = statusManager.itemSummmaryWindow.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
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
		summaryParameter.GetGameObject("statusPower7Text").SetActive(value: true);
		summaryParameter.GetGameObject("factorSlotTextGroup").SetActive(value: false);
		if (!noStatusSwitch)
		{
			int selectItemCategoryNum = statusManager.selectItemCategoryNum;
			int itemID = statusManager.selectItemId;
			switch (selectItemCategoryNum)
			{
			case 0:
			{
				ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData m) => m.itemID == itemID);
				if (!(itemData != null))
				{
					break;
				}
				NameTextLoc.Term = itemData.category.ToString() + itemID;
				itemImageImage.sprite = itemData.itemSprite;
				itemSummaryText.Term = itemData.category.ToString() + itemID + "_summary";
				Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemData.category;
				Panel_PowerText[0].transform.GetComponent<Localize>().enabled = true;
				Panel_PowerText[0].transform.GetComponent<Localize>().Term = "skillTarget_" + itemData.target;
				Panel_TypeTextLoc[1].Term = "skillPower";
				Panel_PowerText[1].text = itemData.itemPower.ToString();
				if (itemData.category == ItemData.Category.medicine)
				{
					if (itemData.itemPower == 0)
					{
						Panel_TypeTextLoc[1].Term = "itemTypeSummary_medicinePoison";
					}
					else
					{
						Panel_TypeTextLoc[1].Term = "itemTypeSummary_medicineAll";
					}
					Panel_PowerText[1].text = " ";
					Panel_TypeTextLoc[2].Term = "itemTypeSummary_battleOnly";
				}
				else if (itemData.category == ItemData.Category.mpPotion || itemData.category == ItemData.Category.revive)
				{
					Panel_TypeTextLoc[2].Term = "itemTypeSummary_scenarioBattleOnly";
				}
				else if (itemData.category == ItemData.Category.rareDropRateUp)
				{
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemData.category;
					Panel_PowerText[0].transform.GetComponent<Localize>().Term = "empty";
					Panel_TypeTextLoc[2].Term = "itemTypeSummary_dungeonMapOnly";
				}
				break;
			}
			case 1:
			{
				ItemMaterialData itemMaterialData = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData m) => m.itemID == itemID);
				if (itemMaterialData != null)
				{
					NameTextLoc.Term = itemMaterialData.category.ToString() + itemID;
					itemImageImage.sprite = itemMaterialData.itemSprite;
					itemSummaryText.Term = itemMaterialData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_material";
					Panel_PowerText[0].text = " ";
					Panel_TypeTextLoc[1].Term = "itemTypeSummary_" + itemMaterialData.category;
				}
				break;
			}
			case 2:
			{
				ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData m) => m.itemID == itemID);
				if (itemCanMakeMaterialData != null)
				{
					NameTextLoc.Term = itemCanMakeMaterialData.category.ToString() + itemID;
					itemImageImage.sprite = itemCanMakeMaterialData.itemSprite;
					itemSummaryText.Term = itemCanMakeMaterialData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_canMakeMaterial";
					Panel_PowerText[0].text = " ";
					Panel_TypeTextLoc[1].Term = "itemTypeSummary_" + itemCanMakeMaterialData.category;
				}
				break;
			}
			case 3:
			{
				ItemCampItemData itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData m) => m.itemID == itemID);
				if (itemCampItemData != null)
				{
					NameTextLoc.Term = "campItem" + itemID;
					itemImageImage.sprite = itemCampItemData.itemSprite;
					itemSummaryText.Term = "campItem" + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_adventureKit";
					Panel_PowerText[0].text = "";
					ItemCampItemData.Category category = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == itemID).category;
					Panel_TypeTextLoc[1].Term = "itemTypeSummary_" + category;
					Panel_PowerText[1].text = "";
					Panel_PowerText[2].text = itemCampItemData.power.ToString();
					switch (category)
					{
					case ItemCampItemData.Category.camp:
						Panel_TypeTextLoc[2].Term = "summaryDungeonRestBonus";
						Panel_PowerText[2].text = itemCampItemData.subPower.ToString();
						break;
					case ItemCampItemData.Category.lanthanum:
						Panel_TypeTextLoc[2].Term = "summaryDungeonBuffBonus";
						break;
					case ItemCampItemData.Category.charm:
						Panel_TypeTextLoc[2].Term = "summaryDungeonDeBuffBonus";
						break;
					case ItemCampItemData.Category.medicKit:
						Panel_TypeTextLoc[2].Term = "summaryDungeonFloorHeal";
						break;
					}
				}
				break;
			}
			case 4:
			{
				ItemMagicMaterialData itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData m) => m.itemID == itemID);
				if (!(itemMagicMaterialData != null))
				{
					break;
				}
				NameTextLoc.Term = itemMagicMaterialData.category.ToString() + itemID;
				itemImageImage.sprite = itemMagicMaterialData.itemSprite;
				itemSummaryText.Term = itemMagicMaterialData.category.ToString() + itemID + "_summary";
				Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemMagicMaterialData.category;
				if (itemMagicMaterialData.category == ItemMagicMaterialData.Category.magicMaterial)
				{
					Panel_PowerText[0].text = " ";
					Panel_TypeTextLoc[1].Term = "itemTypeSummary_needTalisman";
					break;
				}
				if (itemMagicMaterialData.category == ItemMagicMaterialData.Category.magicMaterialPowder)
				{
					Panel_PowerText[0].text = " ";
					Panel_TypeTextLoc[1].Term = "skillPower";
					Panel_PowerText[1].text = itemMagicMaterialData.addOnPower.ToString();
					break;
				}
				if (itemMagicMaterialData.category == ItemMagicMaterialData.Category.addOnMaterialParts)
				{
					Panel_PowerText[0].text = " ";
					Panel_TypeTextLoc[1].Term = "itemTypeSummary_needTalisman";
					break;
				}
				Panel_PowerText[0].transform.GetComponent<Localize>().enabled = true;
				Panel_PowerText[0].transform.GetComponent<Localize>().Term = itemMagicMaterialData.addOnType.ToString();
				switch (itemMagicMaterialData.addOnType)
				{
				case ItemMagicMaterialData.AddOnType.type:
					Panel_TypeTextLoc[1].Term = "categoryAddOnTypeItem";
					Panel_PowerText[1].transform.GetComponent<Localize>().enabled = true;
					Panel_PowerText[1].transform.GetComponent<Localize>().Term = "factor_" + itemMagicMaterialData.factorType;
					Panel_TypeTextLoc[2].Term = "summaryAddOnTypePower";
					Panel_PowerText[2].text = itemMagicMaterialData.addOnPower.ToString();
					break;
				case ItemMagicMaterialData.AddOnType.power:
					Panel_TypeTextLoc[1].Term = "categoryAddOnPowerItem";
					Panel_PowerText[1].transform.GetComponent<Localize>().enabled = true;
					Panel_PowerText[1].transform.GetComponent<Localize>().Term = "craftGetFactorPower" + itemMagicMaterialData.addOnPower;
					break;
				}
				break;
			}
			case 5:
			{
				ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData m) => m.itemID == itemID);
				if (itemEventItemData != null)
				{
					NameTextLoc.Term = itemEventItemData.category.ToString() + itemID;
					itemImageImage.sprite = itemEventItemData.itemSprite;
					itemSummaryText.Term = itemEventItemData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemEventItemData.category;
					Panel_PowerText[0].text = " ";
				}
				break;
			}
			case 6:
			{
				ItemCashableItemData itemCashableItemData = GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData m) => m.itemID == itemID);
				if (itemCashableItemData != null)
				{
					NameTextLoc.Term = itemCashableItemData.category.ToString() + itemID;
					itemImageImage.sprite = itemCashableItemData.itemSprite;
					itemSummaryText.Term = itemCashableItemData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemCashableItemData.category;
					Panel_PowerText[0].text = " ";
				}
				break;
			}
			}
		}
		else
		{
			statusManager.factorEquipButtonGo.SetActive(value: false);
			CanvasGroup component = statusManager.itemEquipButtonGO.GetComponent<CanvasGroup>();
			component.alpha = 0.5f;
			component.interactable = false;
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
}
