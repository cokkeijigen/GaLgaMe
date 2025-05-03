using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ShopItemCategoryListRefresh : StateBehaviour
{
	private ShopManager shopManager;

	public StateLink stateLink;

	public StateLink noStatusLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
	}

	public override void OnStateBegin()
	{
		shopManager.ShopScrollItemDesapwnAll();
		int num = int.MinValue;
		int clickedItemUniqueID = int.MinValue;
		switch (shopManager.selectTabCategoryNum)
		{
		case 0:
		{
			int n;
			for (n = 0; n < PlayerInventoryDataManager.haveItemList.Count; n++)
			{
				ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData item) => item.itemID == PlayerInventoryDataManager.haveItemList[n].itemID);
				if (itemData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemList[n].itemID;
					}
					Transform transform6 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform6, n);
					string term6 = itemData.category.ToString() + itemData.itemID;
					transform6.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term6;
					transform6.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemList[n].haveCountNum.ToString();
					string text2 = itemData.category.ToString();
					if (text2 != "")
					{
						SetItemIconSprite(transform6, text2);
					}
					ShopItemListClickAction component6 = transform6.GetComponent<ShopItemListClickAction>();
					component6.itemID = PlayerInventoryDataManager.haveItemList[n].itemID;
					component6.instanceID = 0;
				}
			}
			break;
		}
		case 1:
		{
			int j;
			for (j = 0; j < PlayerInventoryDataManager.haveItemMaterialList.Count; j++)
			{
				ItemMaterialData itemMaterialData = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData item) => item.itemID == PlayerInventoryDataManager.haveItemMaterialList[j].itemID);
				if (itemMaterialData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemMaterialList[j].itemID;
					}
					Transform transform2 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform2, j);
					string term2 = itemMaterialData.category.ToString() + itemMaterialData.itemID;
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term2;
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemMaterialList[j].haveCountNum.ToString();
					string category = itemMaterialData.category.ToString();
					SetItemIconSprite(transform2, category);
					ShopItemListClickAction component2 = transform2.GetComponent<ShopItemListClickAction>();
					component2.itemID = PlayerInventoryDataManager.haveItemMaterialList[j].itemID;
					component2.instanceID = 0;
				}
			}
			break;
		}
		case 2:
		{
			int l;
			for (l = 0; l < PlayerInventoryDataManager.haveItemCanMakeMaterialList.Count; l++)
			{
				ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData item) => item.itemID == PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].itemID);
				if (itemCanMakeMaterialData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].itemID;
					}
					Transform transform4 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform4, l);
					string term4 = itemCanMakeMaterialData.category.ToString() + itemCanMakeMaterialData.itemID;
					transform4.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term4;
					transform4.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].haveCountNum.ToString();
					string category3 = itemCanMakeMaterialData.category.ToString();
					SetItemIconSprite(transform4, category3);
					ShopItemListClickAction component4 = transform4.GetComponent<ShopItemListClickAction>();
					component4.itemID = PlayerInventoryDataManager.haveItemCanMakeMaterialList[l].itemID;
					component4.instanceID = 0;
				}
			}
			break;
		}
		case 3:
		{
			int num2;
			for (num2 = 0; num2 < PlayerInventoryDataManager.haveCashableItemList.Count; num2++)
			{
				ItemCashableItemData itemCashableItemData = GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData item) => item.itemID == PlayerInventoryDataManager.haveCashableItemList[num2].itemID);
				if (itemCashableItemData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveCashableItemList[num2].itemID;
					}
					Transform transform7 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform7, num2);
					string term7 = itemCashableItemData.category.ToString() + itemCashableItemData.itemID;
					transform7.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term7;
					transform7.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveCashableItemList[num2].haveCountNum.ToString();
					string text3 = itemCashableItemData.category.ToString();
					if (text3 != "")
					{
						SetItemIconSprite(transform7, text3);
					}
					ShopItemListClickAction component7 = transform7.GetComponent<ShopItemListClickAction>();
					component7.itemID = PlayerInventoryDataManager.haveCashableItemList[num2].itemID;
					component7.instanceID = 0;
				}
			}
			break;
		}
		case 4:
		{
			int m;
			for (m = 0; m < PlayerInventoryDataManager.haveItemMagicMaterialList.Count; m++)
			{
				ItemMagicMaterialData itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData item) => item.itemID == PlayerInventoryDataManager.haveItemMagicMaterialList[m].itemID);
				if (itemMagicMaterialData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveItemMagicMaterialList[m].itemID;
					}
					Transform transform5 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform5, m);
					string term5 = itemMagicMaterialData.category.ToString() + itemMagicMaterialData.itemID;
					transform5.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term5;
					transform5.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemMagicMaterialList[m].haveCountNum.ToString();
					string category4 = itemMagicMaterialData.category.ToString();
					SetItemIconSprite(transform5, category4);
					ShopItemListClickAction component5 = transform5.GetComponent<ShopItemListClickAction>();
					component5.itemID = PlayerInventoryDataManager.haveItemMagicMaterialList[m].itemID;
					component5.instanceID = 0;
				}
			}
			break;
		}
		case 5:
		{
			int k;
			for (k = 0; k < PlayerInventoryDataManager.haveAccessoryList.Count; k++)
			{
				ItemAccessoryData itemAccessoryData = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == PlayerInventoryDataManager.haveAccessoryList[k].itemID);
				if (itemAccessoryData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveAccessoryList[k].itemID;
					}
					Transform transform3 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform3, k);
					string term3 = itemAccessoryData.category.ToString() + itemAccessoryData.itemID;
					transform3.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term3;
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "1";
					string category2 = itemAccessoryData.category.ToString();
					SetItemIconSprite(transform3, category2);
					ShopItemListClickAction component3 = transform3.GetComponent<ShopItemListClickAction>();
					component3.itemID = PlayerInventoryDataManager.haveAccessoryList[k].itemID;
					component3.instanceID = 0;
				}
			}
			break;
		}
		case 6:
		{
			int i;
			for (i = 0; i < PlayerInventoryDataManager.haveEventItemList.Count; i++)
			{
				ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData item) => item.itemID == PlayerInventoryDataManager.haveEventItemList[i].itemID);
				if (itemEventItemData != null)
				{
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveEventItemList[i].itemID;
					}
					Transform transform = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform, i);
					string term = itemEventItemData.category.ToString() + itemEventItemData.itemID;
					transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
					transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "1";
					string text = itemEventItemData.category.ToString();
					if (text != "")
					{
						SetItemIconSprite(transform, text);
					}
					ShopItemListClickAction component = transform.GetComponent<ShopItemListClickAction>();
					component.itemID = PlayerInventoryDataManager.haveEventItemList[i].itemID;
					component.instanceID = 0;
				}
			}
			break;
		}
		}
		if (shopManager.isTraded && shopManager.isStillHaveItem)
		{
			Debug.Log("取引後＆まだ所持数が１以上ある");
			Transition(stateLink);
		}
		else if ((shopManager.isTraded && num != int.MinValue) || shopManager.isTalismanTraded)
		{
			shopManager.clickedItemID = num;
			shopManager.clickedItemUniqueID = clickedItemUniqueID;
			ResetItemListSlider();
			Debug.Log("売却後＆firstItemID変数が更新されてる");
			Transition(stateLink);
		}
		else if (num != int.MinValue)
		{
			shopManager.clickedItemID = num;
			shopManager.clickedItemUniqueID = clickedItemUniqueID;
			ResetItemListSlider();
			Debug.Log("firstItemID変数が更新されてる");
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
		transform.SetParent(shopManager.itemSelectScrollContentGo);
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
		shopManager.clickedItemIndex = 0;
		shopManager.itemSelectScrollRect.transform.Find("Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}
}
