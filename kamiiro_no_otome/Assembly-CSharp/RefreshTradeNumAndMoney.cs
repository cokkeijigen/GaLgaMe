using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshTradeNumAndMoney : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private ShopManager shopManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
	}

	public override void OnStateBegin()
	{
		if (shopManager.isItemNothing)
		{
			shopManager.clickedItemID = 9999;
			shopManager.applyButtonCanvasGroup.alpha = 0.5f;
			shopManager.applyButtonCanvasGroup.interactable = false;
			shopManager.tradeNumCanvasGroup.alpha = 0.5f;
			shopManager.tradeNumCanvasGroup.interactable = false;
		}
		else if (shopManager.isEquipItem)
		{
			shopManager.applyButtonCanvasGroup.alpha = 0.5f;
			shopManager.applyButtonCanvasGroup.interactable = false;
		}
		else
		{
			shopManager.applyButtonCanvasGroup.alpha = 1f;
			shopManager.applyButtonCanvasGroup.interactable = true;
		}
		shopManager.selectTradeNum = 1;
		shopManager.RefreshTradeData();
		string text = $"{PlayerDataManager.playerHaveMoney:#,0}";
		headerStatusManager.moneyText.text = text;
		shopManager.moneyText.text = text;
		shopManager.SetShopBalloonText();
		shopManager.isTraded = false;
		shopManager.isTalismanTraded = false;
		shopManager.isEquipItem = false;
		shopManager.isStillHaveItem = false;
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
}
