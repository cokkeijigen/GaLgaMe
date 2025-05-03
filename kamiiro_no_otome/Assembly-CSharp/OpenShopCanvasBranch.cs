using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenShopCanvasBranch : StateBehaviour
{
	private ShopManager shopManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
	}

	public override void OnStateBegin()
	{
		int index = 0;
		switch (PlayerDataManager.currentPlaceName)
		{
		case "ItemShop":
			SetItemCategoryTab(isItemShop: true);
			shopManager.selectTabCategoryNum = 0;
			index = 0;
			SetItemSelectHeader();
			break;
		case "HunterGuild":
		case "Bar":
		case "CommonShop":
			SetItemCategoryTab(isItemShop: false);
			shopManager.selectTabCategoryNum = 0;
			index = 0;
			SetItemSelectHeader();
			break;
		}
		shopManager.isTraded = false;
		shopManager.RefreshCategoryTab(index);
		shopManager.SetShopBalloonText();
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

	private void SetItemCategoryTab(bool isItemShop)
	{
		if (isItemShop)
		{
			shopManager.itemSelectCategoryTabImage[0].gameObject.SetActive(value: true);
			shopManager.itemSelectCategoryTabImage[1].gameObject.SetActive(value: false);
			shopManager.itemSelectCategoryTabImage[2].gameObject.SetActive(value: false);
			shopManager.itemSelectCategoryTabImage[4].gameObject.SetActive(value: false);
			shopManager.itemSelectCategoryTabImage[5].gameObject.SetActive(value: true);
			shopManager.itemSelectCategoryTabImage[6].gameObject.SetActive(value: true);
		}
		else
		{
			shopManager.itemSelectCategoryTabImage[0].gameObject.SetActive(value: true);
			shopManager.itemSelectCategoryTabImage[1].gameObject.SetActive(value: true);
			shopManager.itemSelectCategoryTabImage[2].gameObject.SetActive(value: true);
			shopManager.itemSelectCategoryTabImage[4].gameObject.SetActive(value: true);
			shopManager.itemSelectCategoryTabImage[5].gameObject.SetActive(value: false);
			shopManager.itemSelectCategoryTabImage[6].gameObject.SetActive(value: false);
		}
	}

	private void SetItemSelectHeader()
	{
		if (PlayerNonSaveDataManager.isShopBuy)
		{
			shopManager.itemSelectHeaderTextLoc[0].Term = "headerShopBuy";
			shopManager.summaryTradeTextLoc[0].Term = "shopNumberToBuy";
			shopManager.summaryTradeTextLoc[1].Term = "shopBuyMoney";
			shopManager.summaryTradeTextLoc[2].Term = "buttonBuyApply";
			shopManager.itemSelectCategoryTabImage[3].gameObject.SetActive(value: false);
		}
		else
		{
			shopManager.itemSelectHeaderTextLoc[0].Term = "headerShopSell";
			shopManager.summaryTradeTextLoc[0].Term = "shopNumberToSell";
			shopManager.summaryTradeTextLoc[1].Term = "shopSellMoney";
			shopManager.summaryTradeTextLoc[2].Term = "buttonSellApply";
			shopManager.itemSelectCategoryTabImage[3].gameObject.SetActive(value: true);
		}
	}
}
