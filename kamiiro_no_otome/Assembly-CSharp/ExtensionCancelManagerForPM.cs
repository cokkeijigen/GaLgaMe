using Arbor;
using DG.Tweening;
using UnityEngine;

public class ExtensionCancelManagerForPM : MonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private InDoorTalkManager inDoorTalkManager;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public bool CheckHeroineUnFollow()
	{
		return PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock;
	}

	public void PreCloseExtensionUI()
	{
		headerStatusManager.clockGroupGo.SetActive(value: true);
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		DOTweenModuleUI.DOSizeDelta(endValue: new Vector2(490f, 80f), target: headerStatusManager.placePanelGo.GetComponent<RectTransform>(), duration: 0.1f);
		headerStatusManager.placePanelGo.SetActive(value: true);
		headerStatusManager.moneyPanelGo.SetActive(value: true);
		headerStatusManager.shopRankGroupGo.SetActive(value: true);
		inDoorTalkManager.exitButtonCanvasGroup.gameObject.SetActive(value: true);
		inDoorTalkManager.commandButtonGroupGo.SetActive(value: true);
		inDoorTalkManager.carriageBgGroup.SetActive(value: false);
		inDoorTalkManager.SetExitButtonFsmEnable(value: true);
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
	}
}
