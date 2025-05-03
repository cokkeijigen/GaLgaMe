using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckShopScenario : StateBehaviour
{
	public PlayMakerFSM cancelPlayMakerFSM;

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
		shopManager.shopNoticeCanvas.SetActive(value: false);
		ExceptionEventCheckData exceptionEventCheckData = PlayerExceptionScenarioDataManager.CheckItemShopCurrentEvent();
		if (string.IsNullOrEmpty(exceptionEventCheckData.currentScenarioName))
		{
			shopManager.OpenShopOverlayCanvas(isEnable: false);
			Debug.Log("買い物での発生シナリオなし");
			cancelPlayMakerFSM.SendEvent("ResetShopCancelButton");
			Transition(stateLink);
			return;
		}
		shopManager.shopCanvasGroup.blocksRaycasts = false;
		GameObject.Find("InDoor Canvas").SetActive(value: false);
		GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().SetExitButtonFsmEnable(value: true);
		GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
		PlayerNonSaveDataManager.currentSceneName = "shop";
		PlayerNonSaveDataManager.loadSceneName = "scenario";
		PlayerNonSaveDataManager.selectScenarioName = exceptionEventCheckData.currentScenarioName;
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
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
