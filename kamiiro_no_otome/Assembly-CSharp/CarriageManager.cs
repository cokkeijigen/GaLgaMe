using System;
using System.Collections.Generic;
using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarriageManager : SerializedMonoBehaviour
{
	public ArborFSM arborFSM;

	public CarriageTalkManager carriageTalkManager;

	private LocalMapAccessManager localMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	public Animator storeAnimator;

	public Text moneyText;

	public GameObject[] itemCategoryTabGoArray;

	public GameObject itemSelectScrollContentGo;

	public GameObject priceSettingGo;

	public Sprite[] selectTabSpriteArray;

	public Sprite[] selectScrollContentSpriteArray;

	public Sprite[] storeDisplayButtonSpriteArray;

	public Scrollbar itemSelectViewScrollBar;

	public Text defaultPriceText;

	public Text factorPriceText;

	public GameObject sellSettingFrameGo;

	public ParameterContainer summaryParameterContainer;

	public GameObject factorScrollContentGo;

	public TextMeshProUGUI[] infoFactorGroupText;

	public GameObject itemLockImageGo;

	public Image itemLockButtonImage;

	public Sprite[] itemLockButtonSpriteArray;

	public Localize itemLockButtonLocText;

	public GameObject priceSettingButtonGo;

	public Text setSellPriceText;

	public Text setTradeSymbolText;

	public Text setTradeProbabilityText;

	public Text setMagnificationText;

	public GameObject priceSettingAlertFrameGo;

	public CanvasGroup startButtonCanvasGroup;

	public GameObject startButtonAlertFrameGo;

	public GameObject dialogCanvas;

	public Localize hotSellingTypeLoc;

	public TextMeshProUGUI hotSellingRemainDayText;

	public TextMeshProUGUI hotSellingPriceBonusText;

	public TextMeshProUGUI hotSellingTradeBonusText;

	public GameObject poolParentGo;

	public GameObject selectItemPrefabGo;

	public GameObject factorItemPrefabGo;

	public int selectCategoryNum;

	public int selectScrollContentIndex;

	public bool isScrollContentInitialize;

	public int clickedItemID;

	public int clickedUniqueID = int.MaxValue;

	public int sellClickedItemID;

	public int sellClickedUniqueID;

	public int sellScrollContentIndex;

	public bool isSellChangeClick;

	public bool isSelectItemLock;

	public int sellPriceMinLimit;

	public int sellPriceMaxLimit;

	public int tempSetPriceMagnification;

	public bool isItemInStore;

	public Color enableColor = new Color(0.196f, 0.196f, 0.196f, 1f);

	public Color disableColor = new Color(0.6f, 0.6f, 0.6f, 1f);

	[NonSerialized]
	public int[] itemStatusParam = new int[8];

	private void Awake()
	{
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		selectCategoryNum = PlayerNonSaveDataManager.storeSelectCategoryNum;
		storeAnimator.SetInteger("itemCategory", 0);
		storeAnimator.SetBool("isDisplay", value: false);
		switch (PlayerDataManager.hotSellingCategoryNum)
		{
		case 0:
			hotSellingTypeLoc.Term = "itemTypeHeader_weapon";
			break;
		case 1:
			hotSellingTypeLoc.Term = "itemTypeHeader_armor";
			break;
		}
		hotSellingRemainDayText.text = PlayerDataManager.hotSellingRemainDayCount.ToString();
		hotSellingPriceBonusText.text = PlayerDataManager.hotSellingPriceBonus.ToString();
		hotSellingTradeBonusText.text = PlayerDataManager.hotSellingTradeBonus.ToString();
		if (PlayerDataManager.carriageStoreSellMagnification == 0)
		{
			PlayerDataManager.carriageStoreSellMagnification = 100;
		}
		List<ShopRankData> list = (from data in GameDataManager.instance.shopRankDataBase.shopRankDataList.Where((ShopRankData data) => data.rankType == ShopRankData.RankType.sales).ToList()
			orderby data.sortID descending
			select data).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].needSalesAmount <= PlayerDataManager.totalSalesAmount)
			{
				sellPriceMinLimit = list[i].sellPriceMinLimit;
				sellPriceMaxLimit = list[i].sellPriceMaxLimit;
				break;
			}
		}
		if (PlayerDataManager.totalSalesAmount > GameDataManager.instance.shopRankDataBase.shopRankDataList.Find((ShopRankData data) => data.sortID == 52).needSalesAmount)
		{
			priceSettingGo.GetComponent<CanvasGroup>().interactable = true;
			priceSettingButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		}
		else
		{
			priceSettingGo.GetComponent<CanvasGroup>().interactable = false;
			priceSettingButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
		}
	}

	private void Start()
	{
		localMapAccessManager.localMapExitFSM.gameObject.SetActive(value: false);
		string text = $"{PlayerDataManager.playerHaveMoney:#,0}";
		moneyText.text = text;
	}

	public void OpenDialogCanvas(bool value)
	{
		dialogCanvas.SetActive(value);
		if (value)
		{
			carriageTalkManager.TalkBalloonStoreStart();
		}
		else
		{
			carriageTalkManager.TalkBalloonItemSelect();
		}
	}

	public void StartStoreTending()
	{
		PlayerDataManager.isStoreTending = true;
		PlayerNonSaveDataManager.isOpencarriageStoreResult = true;
		GameObject.Find("Store Tending Manager").GetComponent<ArborFSM>().SendTrigger("StartStoreTendingAnimation");
	}

	public void PushSelectCategoryButton(int categoryNum)
	{
		if (selectCategoryNum != categoryNum)
		{
			selectCategoryNum = categoryNum;
			PlayerNonSaveDataManager.storeSelectCategoryNum = categoryNum;
			storeAnimator.SetInteger("itemCategory", categoryNum);
			clickedItemID = 0;
			arborFSM.SendTrigger("ChangeItemCategoryTab");
		}
	}

	public void CraftScrollItemDesapwnAll()
	{
		Transform[] array = new Transform[itemSelectScrollContentGo.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = itemSelectScrollContentGo.transform.GetChild(i);
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (PoolManager.Pools["Carriage Item Pool"].IsSpawned(array[j]))
			{
				array[j].GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = selectScrollContentSpriteArray[0];
				PoolManager.Pools["Carriage Item Pool"].Despawn(array[j], 0f, poolParentGo.transform);
			}
		}
		Debug.Log("スクロール項目を全部デスポーン");
	}

	public int GetScrollContentIndexFromItemId(int itemID, int uniqueID)
	{
		int result = 0;
		int childCount = itemSelectScrollContentGo.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (PlayerNonSaveDataManager.selectCraftCanvasName == "craft" || PlayerNonSaveDataManager.selectCraftCanvasName == "newCraft")
			{
				int itemID2 = itemSelectScrollContentGo.transform.GetChild(i).GetComponent<CraftItemListClickAction>().itemID;
				int instanceID = itemSelectScrollContentGo.transform.GetChild(i).GetComponent<CraftItemListClickAction>().instanceID;
				if (itemID2 == itemID && instanceID == uniqueID)
				{
					result = i;
					break;
				}
			}
			else if (itemSelectScrollContentGo.transform.GetChild(i).GetComponent<MergeItemListClickAction>().itemID == itemID)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	public void RefreshCarriageSummaryParameter()
	{
		IList<I2LocalizeComponent> variableList = summaryParameterContainer.GetVariableList<I2LocalizeComponent>("statusTypeLoc");
		IList<UguiTextVariable> variableList2 = summaryParameterContainer.GetVariableList<UguiTextVariable>("statusPowerText");
		switch (selectCategoryNum)
		{
		case 0:
			variableList[0].localize.Term = "statusAttack";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicAttack";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusAccuracy";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusCritical";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			variableList[4].localize.Term = "statusItemMp";
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "empty";
			variableList2[5].text.text = "";
			variableList[6].localize.Term = "statusFacotrSlot";
			variableList2[6].text.text = itemStatusParam[6].ToString();
			variableList[7].localize.Term = "statusFacotrLimitNum";
			variableList2[7].text.text = itemStatusParam[7].ToString();
			break;
		case 1:
			variableList[0].localize.Term = "statusDefense";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicDefense";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusEvasion";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusAbnormalResist";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			variableList[4].localize.Term = "statusRecoveryMpAdd";
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "empty";
			variableList2[5].text.text = "";
			variableList[6].localize.Term = "statusFacotrSlot";
			variableList2[6].text.text = itemStatusParam[6].ToString();
			variableList[7].localize.Term = "statusFacotrLimitNum";
			variableList2[7].text.text = itemStatusParam[7].ToString();
			break;
		}
	}

	public void RefreshCarriagePriceSetting(bool isInitialize)
	{
		int num = 0;
		switch (selectCategoryNum)
		{
		case 0:
		{
			GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == clickedItemID);
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == clickedItemID && data.itemUniqueID == clickedUniqueID);
			if (isInitialize)
			{
				setMagnificationText.text = haveWeaponData.sellPriceMagnification.ToString();
				tempSetPriceMagnification = PlayerDataManager.carriageStoreSellMagnification;
				num = haveWeaponData.sellPriceMagnification;
			}
			else
			{
				setMagnificationText.text = tempSetPriceMagnification.ToString();
				num = tempSetPriceMagnification;
			}
			int equipItemSellPrice2 = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(clickedItemID, clickedUniqueID);
			float num6 = (float)num / 100f;
			int num7 = Mathf.RoundToInt((float)equipItemSellPrice2 * num6);
			setSellPriceText.text = num7.ToString();
			int num8 = Mathf.Abs(num - 100);
			Debug.Log("倍率：" + num + "／倍率-100の値：" + num8);
			if (num8 != 0)
			{
				int num9 = num8 / 10 * 4;
				if (num > 100)
				{
					setTradeSymbolText.text = "-";
				}
				else
				{
					setTradeSymbolText.text = "+";
				}
				setTradeProbabilityText.text = num9.ToString();
			}
			else
			{
				setTradeSymbolText.text = "±";
				setTradeProbabilityText.text = "0";
			}
			break;
		}
		case 1:
		{
			GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == clickedItemID);
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == clickedItemID && data.itemUniqueID == clickedUniqueID);
			if (isInitialize)
			{
				setMagnificationText.text = haveArmorData.sellPriceMagnification.ToString();
				tempSetPriceMagnification = PlayerDataManager.carriageStoreSellMagnification;
				num = haveArmorData.sellPriceMagnification;
			}
			else
			{
				setMagnificationText.text = tempSetPriceMagnification.ToString();
				num = tempSetPriceMagnification;
			}
			int equipItemSellPrice = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(clickedItemID, clickedUniqueID);
			float num2 = (float)num / 100f;
			int num3 = Mathf.RoundToInt((float)equipItemSellPrice * num2);
			setSellPriceText.text = num3.ToString();
			int num4 = Mathf.Abs(num - 100);
			if (num4 != 0)
			{
				int num5 = num4 / 10 * 4;
				if (num > 100)
				{
					setTradeSymbolText.text = "-";
				}
				else
				{
					setTradeSymbolText.text = "+";
				}
				setTradeProbabilityText.text = num5.ToString();
			}
			else
			{
				setTradeSymbolText.text = "±";
				setTradeProbabilityText.text = "0";
			}
			break;
		}
		}
	}

	public void AddTempMagnificationNum(int num)
	{
		tempSetPriceMagnification += num;
		tempSetPriceMagnification = Mathf.Clamp(tempSetPriceMagnification, sellPriceMinLimit, sellPriceMaxLimit);
		RefreshCarriagePriceSetting(isInitialize: false);
		arborFSM.SendTrigger("RefreshSellPrice");
	}

	public void SetTempMagnificationLimitNum(bool isLowerLimit)
	{
		if (!isLowerLimit)
		{
			tempSetPriceMagnification = sellPriceMaxLimit;
		}
		else
		{
			tempSetPriceMagnification = sellPriceMinLimit;
		}
		RefreshCarriagePriceSetting(isInitialize: false);
		arborFSM.SendTrigger("RefreshSellPrice");
	}

	public void SetDiplayItemForSale()
	{
		bool flag = false;
		CarriageItemListClickAction component = itemSelectScrollContentGo.transform.GetChild(sellScrollContentIndex).GetComponent<CarriageItemListClickAction>();
		switch (selectCategoryNum)
		{
		case 0:
		{
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == sellClickedItemID && data.itemUniqueID == sellClickedUniqueID);
			haveWeaponData.isItemStoreDisplay = !haveWeaponData.isItemStoreDisplay;
			flag = (component.isStoreDisplay = haveWeaponData.isItemStoreDisplay);
			break;
		}
		case 1:
		{
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == sellClickedItemID && data.itemUniqueID == sellClickedUniqueID);
			haveArmorData.isItemStoreDisplay = !haveArmorData.isItemStoreDisplay;
			flag = (component.isStoreDisplay = haveArmorData.isItemStoreDisplay);
			break;
		}
		}
		isSellChangeClick = true;
		clickedItemID = sellClickedItemID;
		clickedUniqueID = sellClickedUniqueID;
		selectScrollContentIndex = sellScrollContentIndex;
		arborFSM.SendTrigger("SendCarriageItemListIndex");
		storeAnimator.SetInteger("itemCategory", selectCategoryNum);
		storeAnimator.SetBool("isDisplay", flag);
		if (flag)
		{
			storeAnimator.SetTrigger("itemSet");
		}
		else
		{
			storeAnimator.SetTrigger("itemReset");
		}
		if (flag)
		{
			MasterAudio.PlaySound("SeItemInStock", 1f, null, 0f, null, null);
		}
		else
		{
			MasterAudio.PlaySound("SeItemOutStock", 1f, null, 0f, null, null);
		}
		ParameterContainer component2 = itemSelectScrollContentGo.transform.GetChild(sellScrollContentIndex).GetComponent<ParameterContainer>();
		SetScrollPrefabDisplayButtonTerm(component2, flag, isListRefresh: false);
	}

	public void SetScrollPrefabDisplayButtonTerm(ParameterContainer param, bool isStoreDisplay, bool isListRefresh)
	{
		param.GetGameObject("buttonGo").GetComponent<Button>().interactable = true;
		if (isStoreDisplay)
		{
			param.GetGameObject("textGroupGo").GetComponent<CanvasGroup>().alpha = 0.5f;
			param.GetVariable<UguiImage>("itemImage").image.sprite = selectScrollContentSpriteArray[2];
			param.GetVariable<I2LocalizeComponent>("buttonLoc").localize.Term = "buttonSellCancel";
			param.GetGameObject("buttonGo").GetComponent<Image>().sprite = storeDisplayButtonSpriteArray[1];
		}
		else
		{
			param.GetGameObject("textGroupGo").GetComponent<CanvasGroup>().alpha = 1f;
			param.GetVariable<UguiImage>("itemImage").image.sprite = selectScrollContentSpriteArray[0];
			param.GetVariable<I2LocalizeComponent>("buttonLoc").localize.Term = "buttonCarriageStoreSell";
			param.GetGameObject("buttonGo").GetComponent<Image>().sprite = storeDisplayButtonSpriteArray[0];
		}
		if (!isListRefresh)
		{
			itemSelectScrollContentGo.transform.GetChild(sellScrollContentIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = selectScrollContentSpriteArray[1];
		}
		startButtonCanvasGroup.gameObject.GetComponent<ArborFSM>().SendTrigger("CalcItemIsInStore");
	}

	public void CheckPriceSettingAlertFrameOpen()
	{
		if (PlayerDataManager.totalSalesAmount <= GameDataManager.instance.shopRankDataBase.shopRankDataList.Find((ShopRankData data) => data.sortID == 52).needSalesAmount)
		{
			priceSettingAlertFrameGo.SetActive(value: true);
		}
	}
}
