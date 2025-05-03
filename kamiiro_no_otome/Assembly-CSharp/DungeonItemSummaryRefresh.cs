using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DungeonItemSummaryRefresh : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonItemManager dungeonItemManager;

	private DungeonGetItemManager dungeonGetItemManager;

	private Localize[] Panel_TypeTextLoc = new Localize[8];

	private Text[] Panel_PowerText = new Text[8];

	public bool noStatusSwitch;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonItemManager = GetComponent<DungeonItemManager>();
		dungeonGetItemManager = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i] = dungeonItemManager.useItemSummaryWindowGo.GetComponent<ParameterContainer>().GetVariableList<I2LocalizeComponent>("statusTypeLoc")[i].localize;
			Panel_PowerText[i] = dungeonItemManager.useItemSummaryWindowGo.GetComponent<ParameterContainer>().GetVariableList<UguiTextVariable>("statusPowerText")[i].text;
		}
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = dungeonItemManager.useItemSummaryWindowGo.GetComponent<ParameterContainer>();
		Localize localize = component.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize;
		Image image = component.GetVariable<UguiImage>("itemImage").image;
		Localize localize2 = component.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize;
		localize.Term = "noStatus";
		image.sprite = dungeonItemManager.noItemImageSprite;
		localize2.Term = "noSelectItemSummary";
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i].Term = "noStatus";
			Panel_PowerText[i].text = " ";
		}
		Panel_PowerText[1].transform.GetComponent<Localize>().enabled = false;
		if (!noStatusSwitch)
		{
			SetScrollContentSprite();
			int itemID = dungeonItemManager.selectItemID;
			ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData m) => m.itemID == itemID);
			if (itemData != null)
			{
				localize.Term = itemData.category.ToString() + itemID;
				image.sprite = itemData.itemSprite;
				localize2.Term = itemData.category.ToString() + itemID + "_summary";
				Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemData.category;
				Panel_PowerText[0].transform.GetComponent<Localize>().enabled = true;
				Panel_PowerText[0].transform.GetComponent<Localize>().Term = "skillTarget_" + itemData.target;
				if (itemData.category == ItemData.Category.medicine)
				{
					string text = "";
					text = ((itemData.itemPower != 0) ? "itemTypeSummary_medicineAll" : "itemTypeSummary_medicinePoison");
					Panel_TypeTextLoc[1].Term = "itemTypeSummary_medicinePower";
					Panel_PowerText[1].transform.GetComponent<Localize>().enabled = true;
					Panel_PowerText[1].transform.GetComponent<Localize>().Term = text;
					Panel_TypeTextLoc[2].Term = "itemTypeSummary_battleOnly";
					if (!dungeonMapManager.dungeonBattleCanvas.activeInHierarchy)
					{
						dungeonItemManager.useButtonCanvasGroup.interactable = false;
						dungeonItemManager.useButtonCanvasGroup.alpha = 0.5f;
						Debug.Log("選択中のアイテム／状態回復アイテム／戦闘中ではない");
					}
					else
					{
						dungeonItemManager.useButtonCanvasGroup.interactable = true;
						dungeonItemManager.useButtonCanvasGroup.alpha = 1f;
						Debug.Log("選択中のアイテム／状態回復アイテム／戦闘中");
					}
				}
				else if (itemData.category == ItemData.Category.rareDropRateUp)
				{
					Panel_TypeTextLoc[0].Term = "itemTypeSummary_" + itemData.category;
					Panel_PowerText[0].transform.GetComponent<Localize>().Term = "empty";
					Panel_TypeTextLoc[1].Term = "skillPower";
					Panel_PowerText[1].text = itemData.itemPower.ToString();
					Panel_TypeTextLoc[2].Term = "itemTypeSummary_battleOnly";
					if (!dungeonMapManager.dungeonBattleCanvas.activeInHierarchy)
					{
						dungeonItemManager.useButtonCanvasGroup.interactable = true;
						dungeonItemManager.useButtonCanvasGroup.alpha = 1f;
						Debug.Log("選択中のアイテム／レア上昇率アップ／戦闘中ではない");
					}
					else
					{
						dungeonItemManager.useButtonCanvasGroup.interactable = false;
						dungeonItemManager.useButtonCanvasGroup.alpha = 0.5f;
						Debug.Log("選択中のアイテム／レア上昇率アップ／戦闘中");
					}
				}
				else
				{
					Panel_TypeTextLoc[1].Term = "skillPower";
					Panel_PowerText[1].text = itemData.itemPower.ToString();
					dungeonItemManager.useButtonCanvasGroup.interactable = true;
					dungeonItemManager.useButtonCanvasGroup.alpha = 1f;
					Debug.Log("選択中のアイテム／その他のアイテム");
				}
				if (itemData.category == ItemData.Category.mpPotion || itemData.category == ItemData.Category.allMpPotion || itemData.category == ItemData.Category.revive)
				{
					dungeonItemManager.useButtonCanvasGroup.interactable = false;
					dungeonItemManager.useButtonCanvasGroup.alpha = 0.5f;
					Panel_TypeTextLoc[2].Term = "itemTypeSummary_scenarioBattleOnly";
					Debug.Log("選択中のアイテム／MP回復or蘇生アイテム");
				}
			}
			Transition(stateLink);
		}
		else
		{
			dungeonItemManager.useButtonCanvasGroup.interactable = false;
			dungeonItemManager.useButtonCanvasGroup.alpha = 0.5f;
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

	private void SetScrollContentSprite()
	{
		for (int i = 0; i < dungeonItemManager.useItemScrollContentGo.transform.childCount; i++)
		{
			dungeonItemManager.useItemScrollContentGo.transform.GetChild(i).GetComponent<Image>().sprite = dungeonGetItemManager.viewScrollSpriteArray[0];
		}
		dungeonItemManager.useItemScrollContentGo.transform.GetChild(dungeonItemManager.selectItemSiblingIndex).GetComponent<Image>().sprite = dungeonGetItemManager.viewScrollSpriteArray[1];
	}
}
