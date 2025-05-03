using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckHeroineFollowAtItemShop : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	private PlayMakerFSM alertDialogFSM;

	public StateLink followLink;

	public StateLink noFollowLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		alertDialogFSM = totalMapAccessManager.mapAlertDialogGo.GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredSolo";
			OpenMapAlertDialog();
		}
		else if (PlayerDataManager.currentTimeZone > 1)
		{
			totalMapAccessManager.alertTextLoc.Term = "dialogItemShopDisableClose";
			OpenMapAlertDialog();
		}
		else
		{
			Transition(noFollowLink);
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

	private void OpenMapAlertDialog()
	{
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[1].blocksRaycasts = false;
		headerStatusManager.menuCanvasGroup.alpha = 0.5f;
		headerStatusManager.exitButton.interactable = false;
		headerStatusManager.exitButton.blocksRaycasts = false;
		headerStatusManager.exitButton.alpha = 0.5f;
		localMapAccessManager.SetLocalMapExitEnable(isEnable: false);
		totalMapAccessManager.mapAlertDialogGo.SetActive(value: true);
		alertDialogFSM.SendEvent("CloseMapAlertDialog");
		Transition(followLink);
	}
}
