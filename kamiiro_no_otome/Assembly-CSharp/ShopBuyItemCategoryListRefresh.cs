using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ShopBuyItemCategoryListRefresh : StateBehaviour
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
		switch (shopManager.selectTabCategoryNum)
		{
		case 0:
		{
			for (int l = 0; l < GameDataManager.instance.itemDataBase.itemDataList.Count; l++)
			{
				ItemData itemData4 = GameDataManager.instance.itemDataBase.itemDataList[l];
				string shopSaleFlag3 = itemData4.shopSaleFlag;
				if (!string.IsNullOrEmpty(itemData4.shopSaleFlag) && PlayerFlagDataManager.scenarioFlagDictionary[shopSaleFlag3])
				{
					if (num == int.MinValue)
					{
						num = GameDataManager.instance.itemDataBase.itemDataList[l].itemID;
					}
					Transform transform4 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform4, l);
					string term4 = itemData4.category.ToString() + itemData4.itemID;
					transform4.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term4;
					HaveItemData haveItemData2 = PlayerInventoryDataManager.haveItemList.Find((HaveItemData data) => data.itemID == itemData4.itemID);
					if (haveItemData2 != null)
					{
						transform4.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = haveItemData2.haveCountNum.ToString();
					}
					else
					{
						transform4.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "0";
					}
					string text2 = itemData4.category.ToString();
					if (text2 != "")
					{
						SetItemIconSprite(transform4, text2);
					}
					ShopItemListClickAction component4 = transform4.GetComponent<ShopItemListClickAction>();
					component4.itemID = itemData4.itemID;
					component4.instanceID = 0;
				}
			}
			break;
		}
		case 1:
		{
			for (int n = 0; n < GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Count; n++)
			{
				ItemMaterialData itemData6 = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList[n];
				string shopSaleFlag4 = itemData6.shopSaleFlag;
				if (!string.IsNullOrEmpty(shopSaleFlag4) && PlayerFlagDataManager.scenarioFlagDictionary[shopSaleFlag4])
				{
					if (num == int.MinValue)
					{
						num = GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList[n].itemID;
					}
					Transform transform6 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform6, n);
					string term6 = itemData6.category.ToString() + itemData6.itemID;
					transform6.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term6;
					HaveItemData haveItemData4 = PlayerInventoryDataManager.haveItemMaterialList.Find((HaveItemData data) => data.itemID == itemData6.itemID);
					if (haveItemData4 != null)
					{
						transform6.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = haveItemData4.haveCountNum.ToString();
					}
					else
					{
						transform6.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "0";
					}
					string category4 = itemData6.category.ToString();
					SetItemIconSprite(transform6, category4);
					ShopItemListClickAction component6 = transform6.GetComponent<ShopItemListClickAction>();
					component6.itemID = itemData6.itemID;
					component6.instanceID = 0;
				}
			}
			break;
		}
		case 2:
		{
			for (int j = 0; j < GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Count; j++)
			{
				ItemCanMakeMaterialData itemData2 = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList[j];
				string shopSaleFlag = itemData2.shopSaleFlag;
				if (!string.IsNullOrEmpty(shopSaleFlag) && PlayerFlagDataManager.scenarioFlagDictionary[shopSaleFlag])
				{
					if (num == int.MinValue)
					{
						num = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList[j].itemID;
					}
					Transform transform2 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform2, j);
					string term2 = itemData2.category.ToString() + itemData2.itemID;
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term2;
					HaveItemData haveItemData = PlayerInventoryDataManager.haveItemCanMakeMaterialList.Find((HaveItemData data) => data.itemID == itemData2.itemID);
					if (haveItemData != null)
					{
						transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = haveItemData.haveCountNum.ToString();
					}
					else
					{
						transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "0";
					}
					string category = itemData2.category.ToString();
					SetItemIconSprite(transform2, category);
					ShopItemListClickAction component2 = transform2.GetComponent<ShopItemListClickAction>();
					component2.itemID = itemData2.itemID;
					component2.instanceID = 0;
				}
			}
			break;
		}
		case 4:
		{
			for (int m = 0; m < GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Count; m++)
			{
				ItemMagicMaterialData itemData5 = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList[m];
				if (!string.IsNullOrEmpty(itemData5.shopSaleFlag))
				{
					if (num == int.MinValue)
					{
						num = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList[m].itemID;
					}
					Transform transform5 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform5, m);
					string term5 = itemData5.category.ToString() + itemData5.itemID;
					transform5.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term5;
					HaveItemData haveItemData3 = PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData data) => data.itemID == itemData5.itemID);
					if (haveItemData3 != null)
					{
						transform5.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = haveItemData3.haveCountNum.ToString();
					}
					else
					{
						transform5.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "0";
					}
					string category3 = itemData5.category.ToString();
					SetItemIconSprite(transform5, category3);
					ShopItemListClickAction component5 = transform5.GetComponent<ShopItemListClickAction>();
					component5.itemID = itemData5.itemID;
					component5.instanceID = 0;
				}
			}
			break;
		}
		case 5:
		{
			for (int k = 0; k < GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Count; k++)
			{
				ItemAccessoryData itemData3 = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList[k];
				string shopSaleFlag2 = itemData3.shopSaleFlag;
				if (string.IsNullOrEmpty(shopSaleFlag2) || !PlayerFlagDataManager.scenarioFlagDictionary[shopSaleFlag2])
				{
					continue;
				}
				if (PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData data) => data.itemID == itemData3.itemID) == null)
				{
					if (num == int.MinValue)
					{
						num = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList[k].itemID;
					}
					Transform transform3 = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform3, k);
					string term3 = itemData3.category.ToString() + itemData3.itemID;
					transform3.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term3;
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "0";
					string category2 = itemData3.category.ToString();
					SetItemIconSprite(transform3, category2);
					ShopItemListClickAction component3 = transform3.GetComponent<ShopItemListClickAction>();
					component3.itemID = itemData3.itemID;
					component3.instanceID = 0;
				}
				else
				{
					Debug.Log("装飾品は所有済み／ID：" + itemData3.itemID);
				}
			}
			break;
		}
		case 6:
		{
			for (int i = 0; i < GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Count; i++)
			{
				ItemEventItemData itemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[i];
				if (!string.IsNullOrEmpty(itemData.shopSaleFlag) && PlayerFlagDataManager.scenarioFlagDictionary[itemData.shopSaleFlag] && PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData data) => data.itemID == itemData.itemID) == null)
				{
					if (num == int.MinValue)
					{
						num = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[i].itemID;
					}
					Transform transform = PoolManager.Pools["Shop Pool Item"].Spawn(shopManager.itemSelectScrollPrefabGo[0]);
					RefreshItemList(transform, i);
					string term = itemData.category.ToString() + itemData.itemID;
					transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
					transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("haveNumText").text.text = "0";
					string text = itemData.category.ToString();
					if (text != "")
					{
						SetItemIconSprite(transform, text);
					}
					ShopItemListClickAction component = transform.GetComponent<ShopItemListClickAction>();
					component.itemID = itemData.itemID;
					component.instanceID = 0;
				}
			}
			break;
		}
		}
		if (shopManager.isTraded && shopManager.selectTabCategoryNum != 5 && shopManager.selectTabCategoryNum != 6)
		{
			Debug.Log("取引後＆装飾品＆貴重品ではない");
			Transition(stateLink);
		}
		else if (num != int.MinValue)
		{
			shopManager.clickedItemID = num;
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
