using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class MergeInfoRefresh : StateBehaviour
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
		craftCanvasManager.remainingDaysFrameGo.SetActive(value: false);
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.infoEditIconImage[0].sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemPartyWeaponData.category.ToString()];
			craftCanvasManager.infoEditTextLoc[0].Term = itemPartyWeaponData.category.ToString() + craftManager.clickedItemID;
			craftCanvasManager.infoEditIconImage[1].sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["attack"];
			craftCanvasManager.infoEditTextLoc[1].Term = "statusAttack";
			craftCanvasManager.infoEditNumText[1].text = itemPartyWeaponData.attackPower.ToString();
			craftCanvasManager.infoEditIconImage[2].sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["magicAttack"];
			craftCanvasManager.infoEditTextLoc[2].Term = "statusMagicAttack";
			craftCanvasManager.infoEditNumText[2].text = itemPartyWeaponData.magicAttackPower.ToString();
			craftCanvasManager.infoEditNumText[0].text = itemPartyWeaponData.needWorkShopLevel.ToString();
			craftCanvasManager.craftInheritImageArray[0].sprite = itemPartyWeaponData.itemInheritSprite;
			int nextID2 = craftManager.GetNextUpgradePartyItemID(craftManager.clickedItemID);
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
					craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockRecipe";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[1];
				craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockCraftMax";
			}
			break;
		}
		case 1:
		{
			ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID);
			craftCanvasManager.infoEditIconImage[0].sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			craftCanvasManager.infoEditTextLoc[0].Term = "armor" + craftManager.clickedItemID;
			craftCanvasManager.infoEditIconImage[1].sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["defense"];
			craftCanvasManager.infoEditTextLoc[1].Term = "statusDefense";
			craftCanvasManager.infoEditNumText[1].text = itemPartyArmorData.defensePower.ToString();
			craftCanvasManager.infoEditIconImage[2].sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["magicDefense"];
			craftCanvasManager.infoEditTextLoc[2].Term = "statusMagicDefense";
			craftCanvasManager.infoEditNumText[2].text = itemPartyArmorData.magicDefensePower.ToString();
			craftCanvasManager.infoEditNumText[0].text = itemPartyArmorData.needWorkShopLevel.ToString();
			craftCanvasManager.craftInheritImageArray[0].sprite = itemPartyArmorData.itemInheritSprite;
			int nextID = craftManager.GetNextUpgradePartyItemID(craftManager.clickedItemID);
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
					craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockRecipe";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[1];
				craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockCraftMax";
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
