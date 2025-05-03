using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ShopItemSummaryRefresh : StateBehaviour
{
	private ShopManager shopManager;

	private ParameterContainer parameterContainer;

	private Localize NameTextLoc;

	private Image itemIconImage;

	private Image itemImageImage;

	private Localize itemSummaryText;

	private Localize[] Panel_TypeTextLoc = new Localize[8];

	private Text[] Panel_PowerText = new Text[8];

	public bool isSummaryClear;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
		parameterContainer = shopManager.summaryParam;
		NameTextLoc = parameterContainer.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize;
		itemIconImage = parameterContainer.GetVariable<UguiImage>("itemIcon").image;
		itemImageImage = parameterContainer.GetVariable<UguiImage>("itemImage").image;
		itemSummaryText = parameterContainer.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i] = parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
			Panel_PowerText[i] = parameterContainer.GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
		}
	}

	public override void OnStateBegin()
	{
		NameTextLoc.Term = "empty";
		itemIconImage.gameObject.SetActive(value: false);
		itemImageImage.sprite = shopManager.noItemImageSprite[1];
		itemSummaryText.Term = "noSelectItemSummary";
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i].Term = "noStatus";
			Panel_PowerText[i].text = " ";
		}
		Panel_PowerText[0].transform.GetComponent<Localize>().enabled = false;
		if (!isSummaryClear)
		{
			int selectTabCategoryNum = shopManager.selectTabCategoryNum;
			int itemID = shopManager.clickedItemID;
			itemIconImage.gameObject.SetActive(value: true);
			GameObject[] summaryCustomButtonGroup = shopManager.summaryCustomButtonGroup;
			for (int j = 0; j < summaryCustomButtonGroup.Length; j++)
			{
				summaryCustomButtonGroup[j].SetActive(value: false);
			}
			switch (selectTabCategoryNum)
			{
			case 0:
			{
				ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData m) => m.itemID == itemID);
				if (!(itemData != null))
				{
					break;
				}
				NameTextLoc.Term = itemData.category.ToString() + itemID;
				itemIconImage.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemData.category.ToString()];
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
				break;
			}
			case 1:
			{
				ItemMaterialData itemMaterialData = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData m) => m.itemID == itemID);
				if (itemMaterialData != null)
				{
					NameTextLoc.Term = itemMaterialData.category.ToString() + itemID;
					itemIconImage.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemMaterialData.category.ToString()];
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
					itemIconImage.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCanMakeMaterialData.category.ToString()];
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
				ItemCashableItemData itemCashableItemData = GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData m) => m.itemID == itemID);
				if (itemCashableItemData != null)
				{
					NameTextLoc.Term = itemCashableItemData.category.ToString() + itemID;
					itemIconImage.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCashableItemData.category.ToString()];
					itemImageImage.sprite = itemCashableItemData.itemSprite;
					itemSummaryText.Term = itemCashableItemData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemCashableItemData.category;
					Panel_PowerText[0].text = " ";
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
				itemIconImage.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemMagicMaterialData.category.ToString()];
				itemImageImage.sprite = itemMagicMaterialData.itemSprite;
				itemSummaryText.Term = itemMagicMaterialData.category.ToString() + itemID + "_summary";
				Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemMagicMaterialData.category;
				if (itemMagicMaterialData.category == ItemMagicMaterialData.Category.magicMaterial || itemMagicMaterialData.category == ItemMagicMaterialData.Category.addOnMaterialParts)
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
				Panel_PowerText[0].transform.GetComponent<Localize>().enabled = true;
				Panel_PowerText[0].transform.GetComponent<Localize>().Term = itemMagicMaterialData.addOnType.ToString();
				switch (itemMagicMaterialData.addOnType)
				{
				case ItemMagicMaterialData.AddOnType.type:
					Panel_TypeTextLoc[1].Term = "categoryAddOnTypeItem";
					Panel_PowerText[1].transform.GetComponent<Localize>().enabled = true;
					Panel_PowerText[1].transform.GetComponent<Localize>().Term = "factor_" + itemMagicMaterialData.factorType;
					break;
				case ItemMagicMaterialData.AddOnType.power:
					Panel_TypeTextLoc[1].Term = "categoryAddOnPowerItem";
					Panel_PowerText[1].transform.GetComponent<Localize>().enabled = false;
					Panel_PowerText[1].text = "LV " + itemMagicMaterialData.addOnPower;
					break;
				}
				break;
			}
			case 5:
			{
				ItemAccessoryData itemAccessoryData = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData m) => m.itemID == itemID);
				if (itemAccessoryData != null)
				{
					NameTextLoc.Term = itemAccessoryData.category.ToString() + itemID;
					itemIconImage.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemAccessoryData.category.ToString()];
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
				}
				break;
			}
			case 6:
			{
				ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData m) => m.itemID == itemID);
				if (itemEventItemData != null)
				{
					NameTextLoc.Term = itemEventItemData.category.ToString() + itemID;
					itemIconImage.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemEventItemData.category.ToString()];
					itemImageImage.sprite = itemEventItemData.itemSprite;
					itemSummaryText.Term = itemEventItemData.category.ToString() + itemID + "_summary";
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemEventItemData.category;
					Panel_PowerText[0].text = " ";
				}
				break;
			}
			}
			shopManager.isItemNothing = false;
		}
		else
		{
			shopManager.isItemNothing = true;
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
