using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public ArborFSM shopFSM;

	public ArborFSM commonFSM;

	public Localize shopTypeTextLoc;

	public Text moneyText;

	public CanvasGroup shopCanvasGroup;

	public GameObject itemSelectTabGo;

	public RectTransform itemSelectScrollRect;

	public Transform itemSelectScrollContentGo;

	public GameObject[] itemSelectScrollPrefabGo;

	public Transform prefabParentGo;

	public Image[] itemSelectCategoryTabImage;

	public Sprite[] itemSelectCategorySpriteArray;

	public Sprite[] selectScrollContentSpriteArray;

	public Localize[] itemSelectHeaderTextLoc;

	public Localize itemSelectScrollSummaryLoc;

	public Localize itemSelectSummaryPowerTextLoc;

	public GameObject[] summaryCustomButtonGroup;

	public ParameterContainer summaryParam;

	public CanvasGroup tradeNumCanvasGroup;

	public Text tradeNumText;

	public Text tradeMoneyNumText;

	public Localize[] summaryTradeTextLoc;

	public Sprite[] noItemImageSprite;

	public CanvasGroup applyButtonCanvasGroup;

	public GameObject shopOverlayCanvas;

	public Localize tradeConfirmTextLoc;

	public GameObject shopNoticeCanvas;

	public Localize shopNoticeTextLoc1;

	public Localize shopNoticeTextLoc2;

	public Image characterBalloonImage;

	public Sprite[] characterBalloonSprite;

	public Localize characterBalloonTextLoc;

	public int selectTabCategoryNum;

	public int clickedItemID;

	public int clickedItemUniqueID;

	public int clickedItemIndex;

	public int selectTradeNum;

	public int selectTradeMoney;

	public bool isTraded;

	public bool isTalismanTraded;

	public bool isItemNothing;

	public bool isEquipItem;

	public bool isStillHaveItem;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	private void Start()
	{
		string text = $"{PlayerDataManager.playerHaveMoney:#,0}";
		moneyText.text = text;
		if (PlayerNonSaveDataManager.isShopBuy)
		{
			shopTypeTextLoc.Term = "headerShopBuy";
		}
		else
		{
			shopTypeTextLoc.Term = "headerShopSell";
		}
	}

	public void RefreshTradeData()
	{
		if (clickedItemID != 9999)
		{
			if (clickedItemID < 1000)
			{
				int itemPriceFromItemID = PlayerInventoryDataAccess.GetItemPriceFromItemID(clickedItemID);
				selectTradeMoney = itemPriceFromItemID * selectTradeNum;
				Debug.Log("単価：" + itemPriceFromItemID + "／個数：" + selectTradeNum);
			}
			else
			{
				int itemPriceFromItemID2 = PlayerInventoryDataAccess.GetItemPriceFromItemID(clickedItemID);
				selectTradeMoney = itemPriceFromItemID2 * selectTradeNum;
				Debug.Log("単価：" + itemPriceFromItemID2 + "／個数：" + selectTradeNum);
			}
		}
		else
		{
			selectTradeMoney = 0;
		}
		tradeNumText.text = selectTradeNum.ToString();
		string text = $"{selectTradeMoney:#,0}";
		tradeMoneyNumText.text = text;
		if (PlayerNonSaveDataManager.isShopBuy)
		{
			if (selectTradeMoney <= PlayerDataManager.playerHaveMoney && clickedItemID != 9999)
			{
				applyButtonCanvasGroup.alpha = 1f;
				applyButtonCanvasGroup.interactable = true;
			}
			else
			{
				applyButtonCanvasGroup.alpha = 0.5f;
				applyButtonCanvasGroup.interactable = false;
			}
		}
	}

	public void PushMultiPlusNumButton(int num)
	{
		selectTradeNum += num;
		int max = 999;
		int playerHaveItemNum = PlayerInventoryDataAccess.GetPlayerHaveItemNum(clickedItemID);
		if (num == 999)
		{
			int a = 999 - playerHaveItemNum;
			int itemPriceFromItemID = PlayerInventoryDataAccess.GetItemPriceFromItemID(clickedItemID);
			int b = PlayerDataManager.playerHaveMoney / itemPriceFromItemID;
			max = Mathf.Min(a, b);
		}
		if (PlayerNonSaveDataManager.isShopBuy)
		{
			selectTradeNum = Mathf.Clamp(selectTradeNum, 1, max);
		}
		else
		{
			selectTradeNum = Mathf.Clamp(selectTradeNum, 1, playerHaveItemNum);
		}
		RefreshTradeData();
	}

	public void PushMultiMinusNumButton(int num)
	{
		selectTradeNum -= num;
		int max = 999;
		int playerHaveItemNum = PlayerInventoryDataAccess.GetPlayerHaveItemNum(clickedItemID);
		if (PlayerNonSaveDataManager.isShopBuy)
		{
			selectTradeNum = Mathf.Clamp(selectTradeNum, 1, max);
		}
		else
		{
			selectTradeNum = Mathf.Clamp(selectTradeNum, 1, playerHaveItemNum);
		}
		RefreshTradeData();
	}

	public void RefreshCategoryTab(int index)
	{
		selectTradeNum = 1;
		clickedItemIndex = 0;
		selectTabCategoryNum = index;
		itemSelectScrollSummaryLoc.Term = "summaryHaveCount";
		itemSelectSummaryPowerTextLoc.gameObject.SetActive(value: false);
		switch (index)
		{
		case 0:
			itemSelectHeaderTextLoc[1].Term = "itemTypeHeader_useItem";
			tradeNumCanvasGroup.alpha = 1f;
			tradeNumCanvasGroup.interactable = true;
			break;
		case 1:
			itemSelectHeaderTextLoc[1].Term = "itemTypeHeader_material";
			tradeNumCanvasGroup.alpha = 1f;
			tradeNumCanvasGroup.interactable = true;
			break;
		case 2:
			itemSelectHeaderTextLoc[1].Term = "itemTypeHeader_canMakeMaterial";
			tradeNumCanvasGroup.alpha = 1f;
			tradeNumCanvasGroup.interactable = true;
			break;
		case 3:
			itemSelectHeaderTextLoc[1].Term = "itemTypeHeader_cashableItem";
			tradeNumCanvasGroup.alpha = 1f;
			tradeNumCanvasGroup.interactable = true;
			break;
		case 4:
			itemSelectHeaderTextLoc[1].Term = "itemTypeHeader_magicMaterial";
			tradeNumCanvasGroup.alpha = 1f;
			tradeNumCanvasGroup.interactable = true;
			break;
		case 5:
			itemSelectHeaderTextLoc[1].Term = "itemTypeHeader_accessory";
			tradeNumCanvasGroup.alpha = 0.5f;
			tradeNumCanvasGroup.interactable = false;
			break;
		case 6:
			itemSelectHeaderTextLoc[1].Term = "itemTypeHeader_eventItem";
			tradeNumCanvasGroup.alpha = 0.5f;
			tradeNumCanvasGroup.interactable = false;
			break;
		}
		shopFSM.SendTrigger("RefreshScrollView");
	}

	public void PushShopApplyButton()
	{
		if (PlayerNonSaveDataManager.isShopBuy)
		{
			if (clickedItemID < 900)
			{
				int itemSortID = PlayerInventoryDataAccess.GetItemSortID(clickedItemID);
				PlayerInventoryDataAccess.PlayerHaveItemAdd(clickedItemID, itemSortID, selectTradeNum);
			}
			else if (clickedItemID < 1000)
			{
				int itemSortID2 = PlayerInventoryDataAccess.GetItemSortID(clickedItemID);
				PlayerInventoryDataAccess.PlayerHaveEventItemAdd(clickedItemID, itemSortID2);
				PlayerQuestDataManager.RefreshStoryQuestFlagData("itemShop");
			}
			else
			{
				PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == clickedItemID));
			}
			PlayerDataManager.AddHaveMoney(selectTradeMoney * -1);
			MasterAudio.PlaySound("SeShopBuy", 1f, null, 0f, null, null);
		}
		else
		{
			if (clickedItemID < 1000)
			{
				if (PlayerInventoryDataAccess.GetPlayerHaveItemNum(clickedItemID) - selectTradeNum > 0)
				{
					isStillHaveItem = true;
				}
				else
				{
					isStillHaveItem = false;
				}
				int itemSortID3 = PlayerInventoryDataAccess.GetItemSortID(clickedItemID);
				int addNum = selectTradeNum * -1;
				PlayerInventoryDataAccess.PlayerHaveItemAdd(clickedItemID, itemSortID3, addNum);
			}
			else if (clickedItemID < 2000)
			{
				PlayerInventoryDataEquipAccess.PlayerHaveWeaponRemove(clickedItemID, clickedItemUniqueID);
				isTalismanTraded = true;
			}
			else
			{
				PlayerInventoryDataEquipAccess.PlayerHaveArmorRemove(clickedItemID, clickedItemUniqueID);
				isTalismanTraded = true;
			}
			PlayerDataManager.AddHaveMoney(selectTradeMoney);
			MasterAudio.PlaySound("SeShopSell", 1f, null, 0f, null, null);
		}
		PlayerInventoryDataAccess.HaveItemListSortAll();
		PlayerNonSaveDataManager.isUsedShop = true;
		PlayerNonSaveDataManager.isUsedShopForScnearioCheck = true;
		isTraded = true;
		if (clickedItemID > 900 && clickedItemID < 950)
		{
			int num = int.Parse(GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == clickedItemID).rewardRecipeName.Substring(6));
			Debug.Log("解放するレシピ番号：" + num);
			if (num >= 9000)
			{
				shopNoticeTextLoc1.Term = "noticeNewRecipe1";
				shopNoticeTextLoc2.Term = "noticeNewExtension";
			}
			else
			{
				shopNoticeTextLoc1.Term = "noticeNewRecipe1";
				shopNoticeTextLoc2.Term = "noticeNewRecipe2";
			}
			shopOverlayCanvas.SetActive(value: false);
			MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
			shopNoticeCanvas.SetActive(value: true);
		}
		else
		{
			shopOverlayCanvas.SetActive(value: false);
			SetShopBalloonText();
			shopFSM.SendTrigger("RefreshScrollView");
		}
	}

	public void OpenShopOverlayCanvas(bool isEnable)
	{
		if (isEnable)
		{
			shopOverlayCanvas.SetActive(value: true);
			SetShopConfirmBalloonText();
			if (PlayerNonSaveDataManager.isShopBuy)
			{
				tradeConfirmTextLoc.Term = "buttonBuyConfirm";
			}
			else
			{
				tradeConfirmTextLoc.Term = "buttonSellConfirm";
			}
		}
		else
		{
			shopOverlayCanvas.SetActive(value: false);
			SetShopBalloonText();
		}
	}

	public void ShopScrollItemDesapwnAll()
	{
		int childCount = itemSelectScrollContentGo.childCount;
		if (childCount > 0)
		{
			Transform[] array = new Transform[childCount];
			for (int i = 0; i < childCount; i++)
			{
				array[i] = itemSelectScrollContentGo.GetChild(i);
			}
			Transform[] array2 = array;
			foreach (Transform instance in array2)
			{
				PoolManager.Pools["Shop Pool Item"].Despawn(instance, 0f, prefabParentGo);
			}
			Debug.Log("項目を全部デスポーン");
		}
	}

	public void PushShopExitButton()
	{
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		SceneManager.UnloadSceneAsync("shopUI");
	}

	public void SetShopBalloonText()
	{
		switch (PlayerDataManager.currentPlaceName)
		{
		case "ItemShop":
			if (PlayerNonSaveDataManager.isShopBuy)
			{
				characterBalloonImage.sprite = characterBalloonSprite[1];
				characterBalloonTextLoc.Term = "shopBuyBalloonItemSelect_Charlo";
			}
			break;
		case "HunterGuild":
		case "Bar":
		case "CommonShop":
			characterBalloonImage.sprite = characterBalloonSprite[0];
			if (isEquipItem)
			{
				characterBalloonTextLoc.Term = "shopSellBalloonItemSelectIsEquip";
			}
			else if (PlayerNonSaveDataManager.isShopBuy)
			{
				characterBalloonTextLoc.Term = "shopBuyBalloonItemSelect";
			}
			else
			{
				characterBalloonTextLoc.Term = "shopSellBalloonItemSelect";
			}
			break;
		}
	}

	public void SetShopConfirmBalloonText()
	{
		switch (PlayerDataManager.currentPlaceName)
		{
		case "ItemShop":
			if (PlayerNonSaveDataManager.isShopBuy)
			{
				switch (selectTabCategoryNum)
				{
				case 0:
					characterBalloonImage.sprite = characterBalloonSprite[1];
					characterBalloonTextLoc.Term = "shopBuyBalloonItemConfirm_Charlo";
					break;
				case 5:
					characterBalloonImage.sprite = characterBalloonSprite[1];
					characterBalloonTextLoc.Term = "shopBuyBalloonItemConfirm_accessory_Charlo";
					break;
				case 6:
					characterBalloonImage.sprite = characterBalloonSprite[2];
					characterBalloonTextLoc.Term = "shopBuyBalloonItemConfirm_talisman_Charlo";
					break;
				}
			}
			break;
		case "HunterGuild":
		case "Bar":
		case "CommonShop":
			characterBalloonImage.sprite = characterBalloonSprite[0];
			if (PlayerNonSaveDataManager.isShopBuy)
			{
				characterBalloonTextLoc.Term = "shopBuyBalloonItemConfirm";
			}
			else
			{
				characterBalloonTextLoc.Term = "shopSellBalloonItemConfirm";
			}
			break;
		}
	}
}
