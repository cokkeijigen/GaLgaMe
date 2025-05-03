using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DungeonGetItemSetSummary : StateBehaviour
{
	private DungeonGetItemManager dungeonGetItemManager;

	private Localize[] panel_TypeTextLoc = new Localize[8];

	private Text[] panel_PowerText = new Text[8];

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonGetItemManager = GetComponent<DungeonGetItemManager>();
		for (int i = 0; i < 8; i++)
		{
			panel_TypeTextLoc[i] = dungeonGetItemManager.summaryWindowParameter.GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
			panel_PowerText[i] = dungeonGetItemManager.summaryWindowParameter.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
		}
	}

	public override void OnStateBegin()
	{
		if (dungeonGetItemManager.whereKeyValuePairsList.Count == 0)
		{
			dungeonGetItemManager.summaryWindowParameter.GetGameObjectList("summaryGroupList")[0].SetActive(value: false);
			dungeonGetItemManager.summaryWindowParameter.GetGameObjectList("summaryGroupList")[1].SetActive(value: true);
		}
		else
		{
			dungeonGetItemManager.summaryWindowParameter.GetGameObjectList("summaryGroupList")[0].SetActive(value: true);
			dungeonGetItemManager.summaryWindowParameter.GetGameObjectList("summaryGroupList")[1].SetActive(value: false);
			SetScrollContentsSprite();
			for (int i = 0; i < 8; i++)
			{
				panel_TypeTextLoc[i].Term = "noStatus";
				panel_PowerText[i].text = " ";
			}
			panel_PowerText[0].transform.GetComponent<Localize>().enabled = false;
			panel_PowerText[1].transform.GetComponent<Localize>().enabled = false;
			SetSummaryWindow();
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

	private void SetScrollContentsSprite()
	{
		GameObject[] array = new GameObject[dungeonGetItemManager.viewWindowContentGo.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = dungeonGetItemManager.viewWindowContentGo.transform.GetChild(i).gameObject;
		}
		GameObject[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].GetComponent<ParameterContainer>().GetVariable<UguiImage>("scrollImage").image.sprite = dungeonGetItemManager.viewScrollSpriteArray[0];
		}
		array[dungeonGetItemManager.selectItemSiblingIndex].GetComponent<ParameterContainer>().GetVariable<UguiImage>("scrollImage").image.sprite = dungeonGetItemManager.viewScrollSpriteArray[1];
	}

	private void SetSummaryWindow()
	{
		int itemID = dungeonGetItemManager.selectItemID;
		switch (dungeonGetItemManager.selectTabNum)
		{
		case 0:
		{
			ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData m) => m.itemID == itemID);
			if (itemData != null)
			{
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = itemData.category.ToString() + itemID;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<UguiImage>("itemImage").image.sprite = itemData.itemSprite;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = itemData.category.ToString() + itemID + "_summary";
				panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemData.category;
				panel_PowerText[0].transform.GetComponent<Localize>().enabled = true;
				panel_PowerText[0].transform.GetComponent<Localize>().Term = "skillTarget_" + itemData.target;
				panel_TypeTextLoc[1].Term = "skillPower";
				panel_PowerText[1].text = itemData.itemPower.ToString();
			}
			break;
		}
		case 1:
		{
			ItemMaterialData itemMaterialData = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData m) => m.itemID == itemID);
			if (itemMaterialData != null)
			{
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = itemMaterialData.category.ToString() + itemID;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<UguiImage>("itemImage").image.sprite = itemMaterialData.itemSprite;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = itemMaterialData.category.ToString() + itemID + "_summary";
				panel_TypeTextLoc[0].Term = "itemTypeSummary_material";
				panel_PowerText[0].text = " ";
				panel_TypeTextLoc[1].Term = "itemTypeSummary_" + itemMaterialData.category;
			}
			break;
		}
		case 2:
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData m) => m.itemID == itemID);
			if (itemCanMakeMaterialData != null)
			{
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = itemCanMakeMaterialData.category.ToString() + itemID;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<UguiImage>("itemImage").image.sprite = itemCanMakeMaterialData.itemSprite;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = itemCanMakeMaterialData.category.ToString() + itemID + "_summary";
				panel_TypeTextLoc[0].Term = "itemTypeSummary_canMakeMaterial";
				panel_PowerText[0].text = " ";
				panel_TypeTextLoc[1].Term = "itemTypeSummary_" + itemCanMakeMaterialData.category;
			}
			break;
		}
		case 3:
		{
			ItemMagicMaterialData itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData m) => m.itemID == itemID);
			if (!(itemMagicMaterialData != null))
			{
				break;
			}
			dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = itemMagicMaterialData.category.ToString() + itemID;
			dungeonGetItemManager.summaryWindowParameter.GetVariable<UguiImage>("itemImage").image.sprite = itemMagicMaterialData.itemSprite;
			dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = itemMagicMaterialData.category.ToString() + itemID + "_summary";
			panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemMagicMaterialData.category;
			if (itemMagicMaterialData.category == ItemMagicMaterialData.Category.magicMaterial)
			{
				panel_PowerText[0].text = " ";
				panel_TypeTextLoc[1].Term = "itemTypeSummary_needTalisman";
				break;
			}
			if (itemMagicMaterialData.category == ItemMagicMaterialData.Category.magicMaterialPowder)
			{
				panel_PowerText[0].text = " ";
				panel_TypeTextLoc[1].Term = "skillPower";
				panel_PowerText[1].text = itemMagicMaterialData.addOnPower.ToString();
				break;
			}
			if (itemMagicMaterialData.category == ItemMagicMaterialData.Category.addOnMaterialParts)
			{
				panel_TypeTextLoc[0].Term = "itemTypeSummary_material";
				panel_TypeTextLoc[1].Term = "itemTypeSummary_needTalisman";
				break;
			}
			panel_PowerText[0].transform.GetComponent<Localize>().enabled = true;
			panel_PowerText[0].transform.GetComponent<Localize>().Term = itemMagicMaterialData.addOnType.ToString();
			switch (itemMagicMaterialData.addOnType)
			{
			case ItemMagicMaterialData.AddOnType.type:
				panel_TypeTextLoc[1].Term = "categoryAddOnTypeItem";
				panel_PowerText[1].transform.GetComponent<Localize>().enabled = true;
				panel_PowerText[1].transform.GetComponent<Localize>().Term = "factor_" + itemMagicMaterialData.factorType;
				break;
			case ItemMagicMaterialData.AddOnType.power:
				panel_TypeTextLoc[1].Term = "categoryAddOnPowerItem";
				panel_PowerText[1].transform.GetComponent<Localize>().enabled = false;
				panel_PowerText[1].text = "LV " + itemMagicMaterialData.addOnPower;
				break;
			}
			break;
		}
		case 4:
		{
			ItemCashableItemData itemCashableItemData = GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData m) => m.itemID == itemID);
			if (itemCashableItemData != null)
			{
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = itemCashableItemData.category.ToString() + itemID;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<UguiImage>("itemImage").image.sprite = itemCashableItemData.itemSprite;
				dungeonGetItemManager.summaryWindowParameter.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = itemCashableItemData.category.ToString() + itemID + "_summary";
				panel_TypeTextLoc[0].Term = "itemTypeSummary_cashableItem";
				panel_PowerText[0].text = " ";
			}
			break;
		}
		}
	}
}
