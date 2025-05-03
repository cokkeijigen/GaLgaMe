using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CarriageHaveItemListRefresh : StateBehaviour
{
	private CarriageManager carriageManager;

	private CarriageTalkManager carriageTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
		carriageTalkManager = GameObject.Find("Carriage Talk Manager").GetComponent<CarriageTalkManager>();
	}

	public override void OnStateBegin()
	{
		carriageManager.CraftScrollItemDesapwnAll();
		ItemWeaponData itemWeaponData = null;
		ItemArmorData itemArmorData = null;
		int num = 0;
		int itemUniqueID = 0;
		int num2 = 9;
		switch (carriageManager.selectCategoryNum)
		{
		case 0:
		{
			int i;
			for (i = 0; i < PlayerInventoryDataManager.haveWeaponList.Count; i++)
			{
				itemWeaponData = null;
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == PlayerInventoryDataManager.haveWeaponList[i].itemID);
				itemUniqueID = PlayerInventoryDataManager.haveWeaponList[i].itemUniqueID;
				num2 = PlayerInventoryDataManager.haveWeaponList[i].equipCharacter;
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == PlayerInventoryDataManager.haveWeaponList[i].itemID && data.itemUniqueID == itemUniqueID);
				if (itemWeaponData != null)
				{
					Transform transform2 = PoolManager.Pools["Carriage Item Pool"].Spawn(carriageManager.selectItemPrefabGo);
					ParameterContainer component3 = transform2.transform.GetComponent<ParameterContainer>();
					RefreshItemList(transform2, num++);
					string term2 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
					component3.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term2;
					component3.GetVariable<UguiTextVariable>("powerText").text.text = itemWeaponData.attackPower.ToString();
					component3.GetVariable<UguiTextVariable>("factorNumText").text.text = PlayerInventoryDataManager.haveWeaponList[i].weaponHaveFactor.Count.ToString();
					int equipItemSellPrice2 = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(PlayerInventoryDataManager.haveWeaponList[i].itemID, itemUniqueID);
					float num4 = (float)PlayerInventoryDataManager.haveWeaponList[i].sellPriceMagnification / 100f;
					int sellPrice2 = Mathf.RoundToInt((float)equipItemSellPrice2 * num4);
					component3.GetVariable<UguiTextVariable>("priceText").text.text = sellPrice2.ToString();
					PlayerInventoryDataManager.haveWeaponList[i].sellPrice = sellPrice2;
					SetItemIconSprite(component3.GetVariable<UguiImage>("iconImage").image, "greatSword");
					if (num2 == 0)
					{
						component3.GetGameObject("textGroupGo").GetComponent<CanvasGroup>().alpha = 1f;
						component3.GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[0];
						component3.GetGameObject("buttonGo").GetComponent<Image>().sprite = carriageManager.storeDisplayButtonSpriteArray[2];
						component3.GetGameObject("buttonGo").GetComponent<Button>().interactable = false;
						component3.GetVariable<I2LocalizeComponent>("buttonLoc").localize.Term = "buttonNowEquip";
						component3.GetGameObject("equipIconImage").SetActive(value: true);
					}
					else if (haveWeaponData.isItemStoreDisplayLock)
					{
						component3.GetGameObject("textGroupGo").GetComponent<CanvasGroup>().alpha = 1f;
						component3.GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[0];
						component3.GetGameObject("buttonGo").GetComponent<Image>().sprite = carriageManager.storeDisplayButtonSpriteArray[2];
						component3.GetGameObject("buttonGo").GetComponent<Button>().interactable = false;
						component3.GetVariable<I2LocalizeComponent>("buttonLoc").localize.Term = "summaryItemLock";
						component3.GetGameObject("equipIconImage").SetActive(value: false);
					}
					else
					{
						component3.GetGameObject("equipIconImage").SetActive(value: false);
						carriageManager.SetScrollPrefabDisplayButtonTerm(component3, PlayerInventoryDataManager.haveWeaponList[i].isItemStoreDisplay, isListRefresh: true);
					}
					CarriageItemListClickAction component4 = transform2.GetComponent<CarriageItemListClickAction>();
					component4.itemID = itemWeaponData.itemID;
					int instanceID2 = (component4.instanceID = PlayerInventoryDataManager.haveWeaponList[i].itemUniqueID);
					bool isItemStoreDisplay2 = PlayerInventoryDataManager.haveWeaponList[i].isItemStoreDisplay;
					component4.isStoreDisplay = isItemStoreDisplay2;
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
			int n;
			for (n = 0; n < PlayerInventoryDataManager.haveArmorList.Count; n++)
			{
				itemArmorData = null;
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == PlayerInventoryDataManager.haveArmorList[n].itemID);
				itemUniqueID = PlayerInventoryDataManager.haveArmorList[n].itemUniqueID;
				num2 = PlayerInventoryDataManager.haveArmorList[n].equipCharacter;
				HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == PlayerInventoryDataManager.haveArmorList[n].itemID && data.itemUniqueID == itemUniqueID);
				if (itemArmorData != null)
				{
					Transform transform = PoolManager.Pools["Carriage Item Pool"].Spawn(carriageManager.selectItemPrefabGo);
					ParameterContainer component = transform.transform.GetComponent<ParameterContainer>();
					RefreshItemList(transform, num++);
					string term = "armor" + itemArmorData.itemID;
					component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
					component.GetVariable<UguiTextVariable>("powerText").text.text = itemArmorData.defensePower.ToString();
					component.GetVariable<UguiTextVariable>("factorNumText").text.text = PlayerInventoryDataManager.haveArmorList[n].armorHaveFactor.Count.ToString();
					int equipItemSellPrice = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(PlayerInventoryDataManager.haveArmorList[n].itemID, itemUniqueID);
					float num3 = (float)PlayerInventoryDataManager.haveArmorList[n].sellPriceMagnification / 100f;
					int sellPrice = Mathf.RoundToInt((float)equipItemSellPrice * num3);
					component.GetVariable<UguiTextVariable>("priceText").text.text = sellPrice.ToString();
					PlayerInventoryDataManager.haveArmorList[n].sellPrice = sellPrice;
					component.GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[0];
					SetItemIconSprite(component.GetVariable<UguiImage>("iconImage").image, "armor");
					if (num2 == 0)
					{
						component.GetGameObject("textGroupGo").GetComponent<CanvasGroup>().alpha = 1f;
						component.GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[0];
						component.GetGameObject("buttonGo").GetComponent<Image>().sprite = carriageManager.storeDisplayButtonSpriteArray[2];
						component.GetGameObject("buttonGo").GetComponent<Button>().interactable = false;
						component.GetVariable<I2LocalizeComponent>("buttonLoc").localize.Term = "buttonNowEquip";
						component.GetGameObject("equipIconImage").SetActive(value: true);
					}
					else if (haveArmorData.isItemStoreDisplayLock)
					{
						component.GetGameObject("textGroupGo").GetComponent<CanvasGroup>().alpha = 1f;
						component.GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[0];
						component.GetGameObject("buttonGo").GetComponent<Image>().sprite = carriageManager.storeDisplayButtonSpriteArray[2];
						component.GetGameObject("buttonGo").GetComponent<Button>().interactable = false;
						component.GetVariable<I2LocalizeComponent>("buttonLoc").localize.Term = "summaryItemLock";
						component.GetGameObject("equipIconImage").SetActive(value: false);
					}
					else
					{
						component.GetGameObject("equipIconImage").SetActive(value: false);
						carriageManager.SetScrollPrefabDisplayButtonTerm(component, PlayerInventoryDataManager.haveArmorList[n].isItemStoreDisplay, isListRefresh: true);
					}
					CarriageItemListClickAction component2 = transform.GetComponent<CarriageItemListClickAction>();
					component2.itemID = itemArmorData.itemID;
					int instanceID = (component2.instanceID = PlayerInventoryDataManager.haveArmorList[n].itemUniqueID);
					bool isItemStoreDisplay = PlayerInventoryDataManager.haveArmorList[n].isItemStoreDisplay;
					component2.isStoreDisplay = isItemStoreDisplay;
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
		carriageTalkManager.TalkBalloonItemSelect();
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(carriageManager.itemSelectScrollContentGo.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Image imageComponent, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		imageComponent.sprite = sprite;
	}

	private void SetFirstItemData(int loopNum, int itemID, int instanceID, GameObject go)
	{
		if (loopNum == 0 && carriageManager.clickedItemID == 0)
		{
			carriageManager.clickedItemID = itemID;
			carriageManager.clickedUniqueID = instanceID;
			carriageManager.isScrollContentInitialize = true;
			go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = carriageManager.selectScrollContentSpriteArray[1];
			Debug.Log("ループ番号：" + loopNum + "／選択アイテムID：" + carriageManager.clickedItemID + "／GO名" + go.name);
			carriageManager.itemSelectViewScrollBar.value = 1f;
		}
	}
}
