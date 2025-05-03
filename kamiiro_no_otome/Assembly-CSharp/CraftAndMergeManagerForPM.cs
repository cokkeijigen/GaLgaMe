using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

public class CraftAndMergeManagerForPM : MonoBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	private CraftCheckManager craftCheckManager;

	private void Awake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
	}

	public void SetItemSelectScrollSummary()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
			craftManager.craftCommandTypeLoc.Term = "buttonCraft";
			craftManager.itemSerectScrollSummaryGoArray[0].SetActive(value: true);
			craftManager.itemSerectScrollSummaryGoArray[1].GetComponent<Localize>().Term = "summaryEnhanceCount";
			break;
		case "newCraft":
			craftManager.craftCommandTypeLoc.Term = "buttonNewCraftMini";
			craftManager.itemSerectScrollSummaryGoArray[0].SetActive(value: false);
			craftManager.itemSerectScrollSummaryGoArray[1].GetComponent<Localize>().Term = "summaryHaveCount";
			break;
		case "merge":
			craftManager.craftCommandTypeLoc.Term = "buttonMerge";
			craftManager.itemSerectScrollSummaryGoArray[0].SetActive(value: true);
			craftManager.itemSerectScrollSummaryGoArray[1].GetComponent<Localize>().Term = "summaryCharacter";
			break;
		}
	}

	public void InitializeCraftCanvas()
	{
		craftManager.itemSelectHeaderLocalize.Term = "headerSelectEditItem";
		craftManager.itemCategoryTabGoArray[2].SetActive(value: false);
		craftManager.itemCategoryTabGoArray[3].SetActive(value: false);
		craftCanvasManager.infoWindowSummaryLocArray[0].Term = "headerOverView";
		craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonCraftDetail";
		if (PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv > 0)
		{
			craftManager.craftAddOnGoArray[0].SetActive(value: true);
			craftManager.craftAddOnGoArray[1].SetActive(value: false);
		}
		else
		{
			craftManager.craftAddOnGoArray[0].SetActive(value: false);
			craftManager.craftAddOnGoArray[1].SetActive(value: true);
		}
	}

	public void RefreshCraftCanvas()
	{
		GameObject[] itemCategoryTabGoArray = craftManager.itemCategoryTabGoArray;
		for (int i = 0; i < itemCategoryTabGoArray.Length; i++)
		{
			itemCategoryTabGoArray[i].GetComponent<Image>().sprite = craftManager.selectTabSpriteArray[0];
		}
		craftManager.itemCategoryTabGoArray[craftManager.selectCategoryNum].GetComponent<Image>().sprite = craftManager.selectTabSpriteArray[1];
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			craftManager.itemSerectScrollSummaryGoArray[0].GetComponent<Localize>().Term = "statusAttack";
			break;
		case 1:
			craftManager.itemSerectScrollSummaryGoArray[0].GetComponent<Localize>().Term = "statusDefense";
			break;
		}
		craftAddOnManager.selectedMagicMatrialID[0] = 0;
		craftAddOnManager.selectedMagicMatrialID[1] = 0;
	}

	public void RefreshCraftHaveItemList()
	{
		craftManager.CraftScrollItemDesapwnAll();
		ItemWeaponData itemWeaponData = null;
		ItemArmorData itemArmorData = null;
		int num = 0;
		craftCanvasManager.isUniqueItem = false;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			List<int> idList2 = PlayerInventoryDataManager.haveWeaponList.Select((HaveWeaponData data) => data.itemID).ToList();
			int i;
			for (i = 0; i < PlayerInventoryDataManager.haveWeaponList.Count; i++)
			{
				itemWeaponData = null;
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == idList2[i]);
				if (itemWeaponData != null)
				{
					Transform transform2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[0]);
					ParameterContainer component3 = transform2.transform.GetComponent<ParameterContainer>();
					RefreshItemList(transform2, num++);
					string text2 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
					Debug.Log(text2);
					component3.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text2;
					component3.GetVariable<UguiTextVariable>("powerText").text.text = itemWeaponData.attackPower.ToString();
					craftManager.SetIconAndTextColor(component3, isDefault: true);
					component3.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
					component3.GetVariable<UguiTextVariable>("numText").text.text = PlayerInventoryDataManager.haveWeaponList[i].weaponHaveFactor.Count.ToString();
					SetItemIconSprite(component3.GetVariable<UguiImage>("iconImage").image, "greatSword");
					CraftItemListClickAction component4 = transform2.GetComponent<CraftItemListClickAction>();
					component4.itemID = itemWeaponData.itemID;
					int instanceID2 = (component4.instanceID = PlayerInventoryDataManager.haveWeaponList[i].itemUniqueID);
					component4.modeType = CraftItemListClickAction.ModeType.craftRecipe;
					if (PlayerInventoryDataManager.haveWeaponList[i].equipCharacter == 0)
					{
						component3.GetGameObject("equipIconGo").SetActive(value: true);
					}
					else
					{
						component3.GetGameObject("equipIconGo").SetActive(value: false);
					}
					SetFirstItemData(i, itemWeaponData.itemID, instanceID2, transform2.gameObject);
					Debug.Log("武器データ設定完了");
				}
				else
				{
					Debug.Log("武器データなし");
				}
			}
			break;
		}
		case 1:
		{
			List<int> idList = PlayerInventoryDataManager.haveArmorList.Select((HaveArmorData data) => data.itemID).ToList();
			int n;
			for (n = 0; n < idList.Count; n++)
			{
				itemArmorData = null;
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == idList[n]);
				Debug.Log("防具データ取得完了");
				if (itemArmorData != null)
				{
					Transform transform = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[0]);
					ParameterContainer component = transform.transform.GetComponent<ParameterContainer>();
					RefreshItemList(transform, num++);
					string text = "armor" + itemArmorData.itemID;
					Debug.Log(text);
					component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text;
					component.GetVariable<UguiTextVariable>("powerText").text.text = itemArmorData.defensePower.ToString();
					craftManager.SetIconAndTextColor(component, isDefault: true);
					component.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
					component.GetVariable<UguiTextVariable>("numText").text.text = PlayerInventoryDataManager.haveArmorList[n].armorHaveFactor.Count.ToString();
					SetItemIconSprite(component.GetVariable<UguiImage>("iconImage").image, "armor");
					CraftItemListClickAction component2 = transform.GetComponent<CraftItemListClickAction>();
					component2.itemID = itemArmorData.itemID;
					int instanceID = (component2.instanceID = PlayerInventoryDataManager.haveArmorList[n].itemUniqueID);
					component2.modeType = CraftItemListClickAction.ModeType.craftRecipe;
					if (PlayerInventoryDataManager.haveArmorList[n].equipCharacter == 0)
					{
						component.GetGameObject("equipIconGo").SetActive(value: true);
					}
					else
					{
						component.GetGameObject("equipIconGo").SetActive(value: false);
					}
					SetFirstItemData(n, itemArmorData.itemID, instanceID, transform.gameObject);
					Debug.Log("防具データ設定完了");
				}
				else
				{
					Debug.Log("防具データなし");
				}
			}
			break;
		}
		}
		if (craftCanvasManager.isCraftComplete)
		{
			int scrollContentIndexFromItemId = craftManager.GetScrollContentIndexFromItemId(craftCheckManager.craftedItemID, craftCheckManager.craftedUniqueID);
			craftManager.itemSelectScrollContentGO.transform.GetChild(scrollContentIndexFromItemId).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = craftManager.selectScrollContentSpriteArray[1];
			Debug.Log("選択中の項目は：" + scrollContentIndexFromItemId + "番目");
			craftManager.clickedItemID = craftCheckManager.craftedItemID;
			craftManager.clickedUniqueID = craftCheckManager.craftedUniqueID;
		}
	}

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(craftManager.itemSelectScrollContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
		Debug.Log("GO名" + transform.gameObject.name);
	}

	private void SetItemIconSprite(Image imageComponent, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		imageComponent.sprite = sprite;
	}

	private void SetFirstItemData(int loopNum, int itemID, int instanceID, GameObject go)
	{
		if (loopNum == 0 && craftManager.clickedItemID == 0)
		{
			craftManager.clickedItemID = itemID;
			craftManager.clickedUniqueID = instanceID;
			go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[1];
			Debug.Log("ループ番号：" + loopNum + "／選択アイテムID：" + craftManager.clickedItemID + "／GO名" + go.name);
		}
	}
}
