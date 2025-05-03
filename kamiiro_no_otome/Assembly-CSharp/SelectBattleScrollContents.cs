using Arbor;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SelectBattleScrollContents : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ParameterContainer parameterContainer;

	private Localize[] Panel_TypeTextLoc = new Localize[8];

	private Text[] Panel_PowerText = new Text[8];

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		parameterContainer = scenarioBattleSkillManager.itemWindow.GetComponent<ParameterContainer>();
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i] = parameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLocGroup")[i].localize;
			Panel_PowerText[i] = parameterContainer.GetGameObjectList("statusPowerGoGroup")[i].GetComponent<Text>();
		}
	}

	public override void OnStateBegin()
	{
		Localize localize = parameterContainer.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize;
		Image image = parameterContainer.GetVariable<UguiImage>("iconImage").image;
		Image image2 = parameterContainer.GetVariable<UguiImage>("itemImage").image;
		Localize localize2 = parameterContainer.GetVariable<I2LocalizeComponent>("itemSummaryTextLoc").localize;
		localize.Term = "noStatus";
		image.gameObject.SetActive(value: false);
		image2.sprite = scenarioBattleSkillManager.noItemImageSprite;
		localize2.Term = "noSelectItemSummary";
		for (int i = 0; i < 8; i++)
		{
			Panel_TypeTextLoc[i].Term = "noStatus";
			Panel_PowerText[i].text = " ";
		}
		Panel_PowerText[1].transform.GetComponent<Localize>().enabled = false;
		scenarioBattleSkillManager.itemApplyButton.interactable = true;
		scenarioBattleSkillManager.itemApplyButton.transform.Find("Apply Text").GetComponent<TextMeshProUGUI>().alpha = 1f;
		if (PlayerInventoryDataManager.haveItemList.Count > 0)
		{
			SetScrollContentSprite();
			int itemID = scenarioBattleTurnManager.battleUseItemID;
			ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemID);
			localize.Term = itemData.category.ToString() + itemID;
			image.gameObject.SetActive(value: true);
			image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemData.category.ToString()];
			image2.sprite = itemData.itemSprite;
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
			}
			else
			{
				Panel_TypeTextLoc[1].Term = "skillPower";
				Panel_PowerText[1].text = itemData.itemPower.ToString();
			}
			if (itemData.category == ItemData.Category.mpPotion || itemData.category == ItemData.Category.allMpPotion || itemData.category == ItemData.Category.revive)
			{
				Panel_TypeTextLoc[2].Term = "itemTypeSummary_scenarioBattleOnly";
			}
			if (itemData.target == ItemData.Target.all)
			{
				scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "commandClickSummaryApplyItemAll";
			}
			else
			{
				scenarioBattleSkillManager.commandClickSummaryTextLocArray[1].Term = "commandClickSummaryApplyItemSolo";
			}
			if (itemData.category == ItemData.Category.rareDropRateUp)
			{
				scenarioBattleSkillManager.itemApplyButton.interactable = false;
				scenarioBattleSkillManager.itemApplyButton.transform.Find("Apply Text").GetComponent<TextMeshProUGUI>().alpha = 0.5f;
				Panel_TypeTextLoc[0].Term = "itemTypeSummary_rareDropRateUp";
				Panel_PowerText[0].transform.GetComponent<Localize>().Term = "empty";
				Panel_TypeTextLoc[2].Term = "itemTypeSummary_dungeonMapOnly";
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

	private void SetScrollContentSprite()
	{
		foreach (Transform item in scenarioBattleSkillManager.itemContentGo.transform)
		{
			item.GetComponent<Image>().sprite = scenarioBattleSkillManager.scrollContentSpriteArray[0];
		}
		int scrollContentClickNum = scenarioBattleSkillManager.scrollContentClickNum;
		scenarioBattleSkillManager.itemContentGo.transform.GetChild(scrollContentClickNum).GetComponent<Image>().sprite = scenarioBattleSkillManager.scrollContentSpriteArray[1];
	}
}
