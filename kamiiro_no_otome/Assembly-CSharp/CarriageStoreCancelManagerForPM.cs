using Arbor;
using DG.Tweening;
using UnityEngine;

public class CarriageStoreCancelManagerForPM : MonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private InDoorTalkManager inDoorTalkManager;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public bool CheckHeroineUnFollow()
	{
		return PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock;
	}

	public bool CheckTutorialOpening()
	{
		return PlayerNonSaveDataManager.isCarriageStoreTutorial;
	}

	public void PreCloseCarriageStoreUI()
	{
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		DOTweenModuleUI.DOSizeDelta(endValue: new Vector2(490f, 80f), target: headerStatusManager.placePanelGo.GetComponent<RectTransform>(), duration: 0.1f);
		headerStatusManager.placePanelGo.SetActive(value: true);
		headerStatusManager.moneyPanelGo.SetActive(value: true);
		headerStatusManager.shopRankGroupGo.SetActive(value: true);
		headerStatusManager.clockCanvasGroup.gameObject.SetActive(value: true);
		headerStatusManager.partyGroupParent.SetActive(value: true);
		inDoorTalkManager.SetExitButtonFsmEnable(value: true);
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
		localMapAccessManager.localMapExitFSM.gameObject.SetActive(value: true);
	}
}
