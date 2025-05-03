using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenExtensionCanvasBranch : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private CraftExtensionManager craftExtensionManager;

	private InDoorTalkManager inDoorTalkManager;

	public StateLink recoveryLink;

	public StateLink extensionLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		headerStatusManager.placeTextLoc.Term = "place" + PlayerDataManager.currentPlaceName;
		inDoorTalkManager.exitButtonCanvasGroup.gameObject.SetActive(value: false);
		inDoorTalkManager.commandButtonGroupGo.SetActive(value: false);
		inDoorTalkManager.carriageBgGroup.SetActive(value: true);
		craftExtensionManager.craftLvFSM.SendTrigger("RefreshCraftLvWindow");
		string selectExtensionCanvasName = PlayerNonSaveDataManager.selectExtensionCanvasName;
		if (!(selectExtensionCanvasName == "mpRecovery"))
		{
			if (selectExtensionCanvasName == "extension")
			{
				string text = $"{PlayerDataManager.playerHaveMoney:#,0}";
				craftExtensionManager.moneyFrameText.text = text;
				craftExtensionManager.moneyFrameGo.SetActive(value: true);
				craftExtensionManager.craftTypeTextLoc.Term = "buttonExtension";
				craftExtensionManager.uiVisibleButtonGo.SetActive(value: true);
				craftExtensionManager.uiVisibleButtonLoc.Term = "buttonExtensionUiHidden";
				craftExtensionManager.uiVisibleButtonImage.sprite = craftExtensionManager.uiVisibleButtonSpriteArray[0];
				craftExtensionManager.isExtensionUiHidden = false;
				craftExtensionManager.mpRecoveryCanvas.SetActive(value: false);
				craftExtensionManager.extensionCanvas.SetActive(value: true);
				Transition(extensionLink);
			}
		}
		else
		{
			craftExtensionManager.moneyFrameGo.SetActive(value: false);
			craftExtensionManager.craftTypeTextLoc.Term = "buttonMpRecovery";
			craftExtensionManager.uiVisibleButtonGo.SetActive(value: false);
			craftExtensionManager.mpRecoveryCanvas.SetActive(value: true);
			craftExtensionManager.extensionCanvas.SetActive(value: false);
			Transition(recoveryLink);
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
}
