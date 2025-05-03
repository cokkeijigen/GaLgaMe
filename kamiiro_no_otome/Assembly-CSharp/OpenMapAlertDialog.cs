using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenMapAlertDialog : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private PlayMakerFSM alertDialogFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		alertDialogFSM = totalMapAccessManager.mapAlertDialogGo.GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			totalMapAccessManager.alertTextLoc.Term = PlayerNonSaveDataManager.selectDisableMapPointTerm;
			totalMapAccessManager.mapAlertDialogGo.SetActive(value: true);
		}
		else
		{
			totalMapAccessManager.mapAlertDialogGo.SetActive(value: true);
		}
		alertDialogFSM.SendEvent("CloseMapAlertDialog");
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
