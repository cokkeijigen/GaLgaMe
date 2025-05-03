using System;
using System.Collections;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusItemCategoryListRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private ItemDataBase itemDataBase;

	private ItemMaterialDataBase itemMaterialDataBase;

	private ItemCanMakeMaterialDataBase itemCanMakeMaterialDataBase;

	private ItemCampItemDataBase itemCampItemDataBase;

	private ItemMagicMaterialDataBase itemMagicMaterialDataBase;

	private ItemEventItemDataBase itemEventItemDataBase;

	private ItemWeaponDataBase itemWeaponDataBase;

	private ItemArmorDataBase itemArmorDataBase;

	private ItemAccessoryDataBase itemAccessoryDataBase;

	public StateLink stateLink;

	public StateLink noStatusLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GetComponent<StatusManager>();
		itemDataBase = GameDataManager.instance.itemDataBase;
		itemMaterialDataBase = GameDataManager.instance.itemMaterialDataBase;
		itemCanMakeMaterialDataBase = GameDataManager.instance.itemCanMakeMaterialDataBase;
		itemCampItemDataBase = GameDataManager.instance.itemCampItemDataBase;
		itemMagicMaterialDataBase = GameDataManager.instance.itemMagicMaterialDataBase;
		itemEventItemDataBase = GameDataManager.instance.itemEventItemDataBase;
		itemWeaponDataBase = GameDataManager.instance.itemWeaponDataBase;
		itemArmorDataBase = GameDataManager.instance.itemArmorDataBase;
		itemAccessoryDataBase = GameDataManager.instance.itemAccessoryDataBase;
	}

	public override void OnStateBegin()
	{
		statusManager.ResetScrollViewContents(statusManager.itemContentGO.transform, isCustom: false);
		statusManager.factorEquipButtonGo.SetActive(value: false);
		GameObject[] itemScrollSummaryGoArray = statusManager.itemScrollSummaryGoArray;
		for (int i = 0; i < itemScrollSummaryGoArray.Length; i++)
		{
			itemScrollSummaryGoArray[i].SetActive(value: false);
		}
		int num = int.MinValue;
		int selectItemUniqueId = int.MinValue;
		switch (statusManager.selectItemCategoryNum)
		{
		case 0:
		{
			int k;
			for (k = 0; k < PlayerInventoryDataManager.haveItemList.Count; k++)
			{
				ItemData itemData = itemDataBase.itemDataList.Find((ItemData item) => item.itemID == PlayerInventoryDataManager.haveItemList[k].itemID);
				if (itemData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemList[k].itemID;
					}
					Transform transform2 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[0]);
					RefreshItemList(transform2, k);
					string term2 = itemData.category.ToString() + itemData.itemID;
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term2;
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemList[k].haveCountNum.ToString();
					string text2 = itemData.category.ToString();
					if (text2 != "")
					{
						SetItemIconSprite(transform2, text2);
					}
					StatusItemListClickAction component2 = transform2.GetComponent<StatusItemListClickAction>();
					component2.itemID = PlayerInventoryDataManager.haveItemList[k].itemID;
					component2.instanceID = 0;
				}
			}
			statusManager.itemScrollSummaryGoArray[0].SetActive(value: true);
			statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_useItem";
			SetScrollRectHeight(isEquipVisible: false);
			break;
		}
		case 1:
		{
			int num2;
			for (num2 = 0; num2 < PlayerInventoryDataManager.haveItemMaterialList.Count; num2++)
			{
				ItemMaterialData itemMaterialData = itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData item) => item.itemID == PlayerInventoryDataManager.haveItemMaterialList[num2].itemID);
				if (itemMaterialData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemMaterialList[num2].itemID;
					}
					Transform transform6 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[0]);
					RefreshItemList(transform6, num2);
					string term6 = itemMaterialData.category.ToString() + itemMaterialData.itemID;
					transform6.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term6;
					transform6.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemMaterialList[num2].haveCountNum.ToString();
					string category3 = itemMaterialData.category.ToString();
					SetItemIconSprite(transform6, category3);
					StatusItemListClickAction component6 = transform6.GetComponent<StatusItemListClickAction>();
					component6.itemID = PlayerInventoryDataManager.haveItemMaterialList[num2].itemID;
					component6.instanceID = 0;
				}
			}
			statusManager.itemScrollSummaryGoArray[0].SetActive(value: true);
			statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_material";
			SetScrollRectHeight(isEquipVisible: false);
			break;
		}
		case 2:
		{
			int l;
			for (l = 0; l < PlayerInventoryDataManager.haveItemCanMakeMaterialList.Count; l++)
			{
				ItemCanMakeMaterialData itemCanMakeMaterialData = itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData item) => item.itemID == PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].itemID);
				if (itemCanMakeMaterialData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].itemID;
					}
					Transform transform3 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[0]);
					RefreshItemList(transform3, l);
					string term3 = itemCanMakeMaterialData.category.ToString() + itemCanMakeMaterialData.itemID;
					transform3.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term3;
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].haveCountNum.ToString();
					string category = itemCanMakeMaterialData.category.ToString();
					SetItemIconSprite(transform3, category);
					StatusItemListClickAction component3 = transform3.GetComponent<StatusItemListClickAction>();
					component3.itemID = PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].itemID;
					component3.instanceID = 0;
				}
			}
			statusManager.itemScrollSummaryGoArray[0].SetActive(value: true);
			statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_canMakeMaterial";
			SetScrollRectHeight(isEquipVisible: false);
			break;
		}
		case 3:
		{
			int n;
			for (n = 0; n < PlayerInventoryDataManager.haveItemCampItemList.Count; n++)
			{
				ItemCampItemData itemCampItemData = itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData item) => item.itemID == PlayerInventoryDataManager.haveItemCampItemList[n].itemID);
				if (itemCampItemData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemCampItemList[n].itemID;
					}
					Transform transform5 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[0]);
					RefreshItemList(transform5, n);
					string term5 = "campItem" + itemCampItemData.itemID;
					transform5.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term5;
					transform5.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemCampItemList[n].haveCountNum.ToString();
					string category2 = itemCampItemData.category.ToString();
					SetItemIconSprite(transform5, category2);
					StatusItemListClickAction component5 = transform5.GetComponent<StatusItemListClickAction>();
					component5.itemID = PlayerInventoryDataManager.haveItemCampItemList[n].itemID;
					component5.instanceID = 0;
				}
			}
			statusManager.itemScrollSummaryGoArray[0].SetActive(value: true);
			statusManager.headerCategoryTextLoc.Term = "itemTypeSummary_adventureKit";
			SetScrollRectHeight(isEquipVisible: false);
			break;
		}
		case 4:
		{
			int num3;
			for (num3 = 0; num3 < PlayerInventoryDataManager.haveItemMagicMaterialList.Count; num3++)
			{
				ItemMagicMaterialData itemMagicMaterialData = itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData item) => item.itemID == PlayerInventoryDataManager.haveItemMagicMaterialList[num3].itemID);
				if (itemMagicMaterialData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemMagicMaterialList[num3].itemID;
					}
					Transform transform7 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[0]);
					RefreshItemList(transform7, num3);
					string term7 = itemMagicMaterialData.category.ToString() + itemMagicMaterialData.itemID;
					transform7.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term7;
					transform7.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemMagicMaterialList[num3].haveCountNum.ToString();
					string category4 = itemMagicMaterialData.category.ToString();
					SetItemIconSprite(transform7, category4);
					StatusItemListClickAction component7 = transform7.GetComponent<StatusItemListClickAction>();
					component7.itemID = PlayerInventoryDataManager.haveItemMagicMaterialList[num3].itemID;
					component7.instanceID = 0;
				}
			}
			statusManager.itemScrollSummaryGoArray[0].SetActive(value: true);
			statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_magicMaterial";
			SetScrollRectHeight(isEquipVisible: false);
			break;
		}
		case 5:
		{
			int m;
			for (m = 0; m < PlayerInventoryDataManager.haveEventItemList.Count; m++)
			{
				ItemEventItemData itemEventItemData = itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData item) => item.itemID == PlayerInventoryDataManager.haveEventItemList[m].itemID);
				if (itemEventItemData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveEventItemList[m].itemID;
					}
					Transform transform4 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[0]);
					RefreshItemList(transform4, m);
					string term4 = itemEventItemData.category.ToString() + itemEventItemData.itemID;
					transform4.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term4;
					transform4.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "1";
					string text3 = itemEventItemData.category.ToString();
					if (text3 != "")
					{
						SetItemIconSprite(transform4, text3);
					}
					StatusItemListClickAction component4 = transform4.GetComponent<StatusItemListClickAction>();
					component4.itemID = PlayerInventoryDataManager.haveEventItemList[m].itemID;
					component4.instanceID = 0;
				}
			}
			statusManager.itemScrollSummaryGoArray[0].SetActive(value: true);
			statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_eventItem";
			SetScrollRectHeight(isEquipVisible: false);
			break;
		}
		case 6:
		{
			int j;
			for (j = 0; j < PlayerInventoryDataManager.haveCashableItemList.Count; j++)
			{
				ItemCashableItemData itemCashableItemData = GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData item) => item.itemID == PlayerInventoryDataManager.haveCashableItemList[j].itemID);
				if (itemCashableItemData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveCashableItemList[j].itemID;
					}
					Transform transform = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[0]);
					RefreshItemList(transform, j);
					string term = itemCashableItemData.category.ToString() + itemCashableItemData.itemID;
					transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
					transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveCashableItemList[j].haveCountNum.ToString();
					string text = itemCashableItemData.category.ToString();
					if (text != "")
					{
						SetItemIconSprite(transform, text);
					}
					StatusItemListClickAction component = transform.GetComponent<StatusItemListClickAction>();
					component.itemID = PlayerInventoryDataManager.haveCashableItemList[j].itemID;
					component.instanceID = 0;
				}
			}
			statusManager.itemScrollSummaryGoArray[0].SetActive(value: true);
			statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_cashableItem";
			SetScrollRectHeight(isEquipVisible: false);
			break;
		}
		}
		if (num != int.MinValue)
		{
			if (!statusManager.isEquipedItemSummaryShow)
			{
				statusManager.selectItemId = num;
				statusManager.selectItemUniqueId = selectItemUniqueId;
				ResetItemListSlider();
			}
			else
			{
				StartCoroutine(SetItemListSliderToSelected(statusManager.selectItemId));
			}
			Transition(stateLink);
		}
		else
		{
			Transition(noStatusLink);
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(statusManager.itemContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Transform go, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = sprite;
	}

	private void ResetItemListSlider()
	{
		statusManager.itemViewArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}

	private IEnumerator SetItemListSliderToSelected(int itemID)
	{
		for (int i = 0; i < 1; i++)
		{
			yield return null;
		}
		ResetItemListSlider();
		float height = statusManager.itemContentGO.transform.parent.GetComponent<RectTransform>().rect.height;
		int childCount = statusManager.itemContentGO.transform.childCount;
		if (childCount > 0)
		{
			int num = childCount;
			int num2 = 0;
			for (int j = 0; j < childCount; j++)
			{
				if (statusManager.itemContentGO.transform.GetChild(j).gameObject.activeSelf)
				{
					num2++;
					if (statusManager.itemContentGO.transform.GetChild(j).GetComponent<StatusItemListClickAction>().itemID == itemID)
					{
						num = j;
					}
				}
			}
			if (num < childCount)
			{
				float height2 = statusManager.itemContentGO.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
				float spacing = statusManager.itemContentGO.GetComponent<VerticalLayoutGroup>().spacing;
				float num3 = (float)num2 * (height2 + spacing);
				int num4 = Convert.ToInt32(Math.Floor((double)height / (double)(height2 + spacing)));
				if (num >= num4)
				{
					float num5 = ((float)(num + 1) * (height2 + spacing) - height) / (num3 - height);
					if (num5 < 0f)
					{
						num5 = 0f;
					}
					if (num5 > 1f)
					{
						num5 = 1f;
					}
					statusManager.itemViewArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f - num5;
					Debug.Log(num5.ToString());
					yield return null;
				}
			}
		}
		yield return null;
	}

	private void SetScrollRectHeight(bool isEquipVisible)
	{
		if (isEquipVisible)
		{
			statusManager.itemEquipButtonGO.SetActive(value: true);
			statusManager.itemSelectWIndowScrollView.offsetMin = new Vector2(23f, 104f);
		}
		else
		{
			statusManager.itemEquipButtonGO.SetActive(value: false);
			statusManager.itemSelectWIndowScrollView.offsetMin = new Vector2(23f, 28f);
		}
	}
}
