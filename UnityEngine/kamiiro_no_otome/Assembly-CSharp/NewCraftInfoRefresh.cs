using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class NewCraftInfoRefresh : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		newCraftCanvasManager.infoSummaryIconGoArray[0].SetActive(value: true);
		newCraftCanvasManager.infoSummaryIconGoArray[1].SetActive(value: true);
		newCraftCanvasManager.infoSummaryIconGoArray[3].SetActive(value: true);
		newCraftCanvasManager.infoSummaryTextLocArray[3].Term = "summaryNeedWorkShopLv";
		newCraftCanvasManager.newCraftApplyButton.alpha = 1f;
		newCraftCanvasManager.newCraftApplyButton.interactable = true;
		craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonNewCraftDetail";
		newCraftCanvasManager.isNewCraftImpossible = false;
		craftCanvasManager.remainingDaysFrameGo.SetActive(value: false);
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
			newCraftCanvasManager.infoSummaryIconGoArray[0].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			newCraftCanvasManager.infoSummaryTextLocArray[0].Term = itemWeaponData.category.ToString() + craftManager.clickedItemID;
			if (PlayerFlagDataManager.enableNewCraftFlagDictionary[itemWeaponData.itemID])
			{
				newCraftCanvasManager.infoSummaryIconGoArray[1].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["attack"];
				newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "statusAttack";
				newCraftCanvasManager.infoSummaryNumTextArray[0].text = itemWeaponData.attackPower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: true);
				newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: true);
				newCraftCanvasManager.infoSummaryIconGoArray[2].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["magicAttack"];
				newCraftCanvasManager.infoSummaryTextLocArray[2].Term = "statusMagicAttack";
				newCraftCanvasManager.infoSummaryNumTextArray[1].text = itemWeaponData.magicAttackPower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[1].gameObject.SetActive(value: true);
			}
			else
			{
				newCraftCanvasManager.infoSummaryIconGoArray[1].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["attack"];
				newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "statusAttack";
				newCraftCanvasManager.infoSummaryNumTextArray[0].text = itemWeaponData.attackPower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: true);
				newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: true);
				newCraftCanvasManager.infoSummaryIconGoArray[2].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["magicAttack"];
				newCraftCanvasManager.infoSummaryTextLocArray[2].Term = "statusMagicAttack";
				newCraftCanvasManager.infoSummaryNumTextArray[1].text = itemWeaponData.magicAttackPower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[1].gameObject.SetActive(value: true);
				SetSelectItemInfoImpossible(0);
			}
			newCraftCanvasManager.infoSummaryNumTextArray[2].text = itemWeaponData.needWorkShopLevel.ToString();
			break;
		}
		case 1:
		{
			ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
			newCraftCanvasManager.infoSummaryIconGoArray[0].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			newCraftCanvasManager.infoSummaryTextLocArray[0].Term = "armor" + craftManager.clickedItemID;
			if (PlayerFlagDataManager.enableNewCraftFlagDictionary[itemArmorData.itemID])
			{
				newCraftCanvasManager.infoSummaryIconGoArray[1].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["defense"];
				newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "statusDefense";
				newCraftCanvasManager.infoSummaryNumTextArray[0].text = itemArmorData.defensePower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: true);
				newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: true);
				newCraftCanvasManager.infoSummaryIconGoArray[2].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["magicDefense"];
				newCraftCanvasManager.infoSummaryTextLocArray[2].Term = "statusMagicDefense";
				newCraftCanvasManager.infoSummaryNumTextArray[1].text = itemArmorData.magicDefensePower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[1].gameObject.SetActive(value: true);
			}
			else
			{
				newCraftCanvasManager.infoSummaryIconGoArray[1].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["defense"];
				newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "statusDefense";
				newCraftCanvasManager.infoSummaryNumTextArray[0].text = itemArmorData.defensePower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: true);
				newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: true);
				newCraftCanvasManager.infoSummaryIconGoArray[2].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["magicDefense"];
				newCraftCanvasManager.infoSummaryTextLocArray[2].Term = "statusMagicDefense";
				newCraftCanvasManager.infoSummaryNumTextArray[1].text = itemArmorData.magicDefensePower.ToString();
				newCraftCanvasManager.infoSummaryNumTextArray[1].gameObject.SetActive(value: true);
				SetSelectItemInfoImpossible(1);
			}
			newCraftCanvasManager.infoSummaryNumTextArray[2].text = itemArmorData.needWorkShopLevel.ToString();
			break;
		}
		case 2:
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID);
			newCraftCanvasManager.infoSummaryIconGoArray[0].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCanMakeMaterialData.category.ToString()];
			newCraftCanvasManager.infoSummaryTextLocArray[0].Term = itemCanMakeMaterialData.category.ToString() + craftManager.clickedItemID;
			newCraftCanvasManager.infoSummaryIconGoArray[1].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCanMakeMaterialData.category.ToString()];
			newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "itemTypeSummary_canMakeMaterial";
			newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: false);
			newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: true);
			newCraftCanvasManager.infoSummaryIconGoArray[2].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["furnace"];
			newCraftCanvasManager.infoSummaryTextLocArray[2].Term = "summaryNeedFernaceLv";
			newCraftCanvasManager.infoSummaryNumTextArray[1].text = itemCanMakeMaterialData.needFurnaceLevel.ToString();
			newCraftCanvasManager.infoSummaryNumTextArray[1].gameObject.SetActive(value: true);
			newCraftCanvasManager.infoSummaryNumTextArray[2].text = itemCanMakeMaterialData.needWorkShopLevel.ToString();
			break;
		}
		case 3:
		{
			ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == craftManager.clickedItemID);
			if (itemEventItemData != null)
			{
				string text2 = "eventItem" + itemEventItemData.itemID;
				if (PlayerFlagDataManager.keyItemFlagDictionary[text2])
				{
					newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
					newCraftCanvasManager.newCraftApplyButton.interactable = false;
					craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonNewCraftCreated";
					Debug.Log(text2 + "はすでに作成済み");
				}
				newCraftCanvasManager.infoSummaryIconGoArray[0].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemEventItemData.category.ToString()];
				newCraftCanvasManager.infoSummaryTextLocArray[0].Term = itemEventItemData.category.ToString() + craftManager.clickedItemID;
				newCraftCanvasManager.infoSummaryIconGoArray[1].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemEventItemData.category.ToString()];
				newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "itemTypeSummary_" + itemEventItemData.category;
				newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: false);
				newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: false);
				newCraftCanvasManager.infoSummaryNumTextArray[2].text = itemEventItemData.needWorkShopLevel.ToString();
			}
			else
			{
				SetSelectItemInfoEmpty();
			}
			break;
		}
		case 4:
		{
			ItemCampItemData itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID);
			if (itemCampItemData != null)
			{
				string text = "campItem" + itemCampItemData.itemID;
				if (PlayerFlagDataManager.keyItemFlagDictionary[text])
				{
					newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
					newCraftCanvasManager.newCraftApplyButton.interactable = false;
					craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonNewCraftHaved";
					Debug.Log(text + "はすでに作成済み");
				}
				newCraftCanvasManager.infoSummaryIconGoArray[0].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemCampItemData.category.ToString()];
				newCraftCanvasManager.infoSummaryTextLocArray[0].Term = "campItem" + craftManager.clickedItemID;
				newCraftCanvasManager.infoSummaryIconGoArray[1].GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["camp"];
				newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "itemTypeSummary_adventureKit";
				newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: false);
				newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: false);
				newCraftCanvasManager.infoSummaryNumTextArray[2].text = itemCampItemData.needWorkShopLevel.ToString();
			}
			else
			{
				SetSelectItemInfoEmpty();
			}
			break;
		}
		}
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		case 1:
			craftCanvasManager.infoWindowSummaryLocArray[0].Term = "headerOverView";
			break;
		case 2:
		case 3:
		case 4:
			craftCanvasManager.infoWindowSummaryLocArray[0].Term = "headerOverView";
			break;
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

	private void SetSelectItemInfoImpossible(int categoryNum)
	{
		newCraftCanvasManager.isNewCraftImpossible = true;
		newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
		newCraftCanvasManager.newCraftApplyButton.interactable = false;
		craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonNewCraftDetailImpossible";
	}

	private void SetSelectItemInfoEmpty()
	{
		newCraftCanvasManager.infoSummaryIconGoArray[0].SetActive(value: false);
		newCraftCanvasManager.infoSummaryTextLocArray[0].Term = "noSelectItemSummary_short";
		newCraftCanvasManager.infoSummaryIconGoArray[1].SetActive(value: false);
		newCraftCanvasManager.infoSummaryTextLocArray[1].Term = "empty";
		newCraftCanvasManager.infoSummaryNumTextArray[0].gameObject.SetActive(value: false);
		newCraftCanvasManager.infoSummaryFrameGoArray[2].SetActive(value: false);
		newCraftCanvasManager.infoSummaryIconGoArray[3].SetActive(value: false);
		newCraftCanvasManager.infoSummaryTextLocArray[3].Term = "empty";
		newCraftCanvasManager.infoSummaryNumTextArray[2].text = "";
		newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
		newCraftCanvasManager.newCraftApplyButton.interactable = false;
	}
}
