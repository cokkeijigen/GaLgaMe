using Arbor;
using DG.Tweening;
using UnityEngine;

public class ShopCancelManagerForPM : MonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private InDoorTalkManager inDoorTalkManager;

	private ShopManager shopManager;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
	}

	public void SendOpenShopOverlayCanvas(bool value)
	{
		shopManager.OpenShopOverlayCanvas(value);
	}

	public void PreCloseShopUI()
	{
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		DOTweenModuleUI.DOSizeDelta(endValue: new Vector2(490f, 80f), target: headerStatusManager.placePanelGo.GetComponent<RectTransform>(), duration: 0.1f);
		headerStatusManager.placePanelGo.SetActive(value: true);
		headerStatusManager.moneyPanelGo.SetActive(value: true);
		headerStatusManager.shopRankGroupGo.SetActive(value: true);
		GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().SetExitButtonFsmEnable(value: true);
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
		if (PlayerNonSaveDataManager.isUsedShopForScnearioCheck)
		{
			inDoorTalkManager.talkFSM.SendTrigger("RefreshInDoorAfterRest");
		}
	}
}
