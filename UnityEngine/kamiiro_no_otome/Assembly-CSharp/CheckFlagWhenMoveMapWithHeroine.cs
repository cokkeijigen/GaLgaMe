using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckFlagWhenMoveMapWithHeroine : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	private PlayMakerFSM alertDialogFSM;

	public string checkStartFlagName;

	public string checkEndFlagName;

	public string alertTextTerm;

	public int impossibleFollowHeroineId;

	public StateLink disableLink;

	public StateLink stateLink;

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
		if (PlayerDataManager.isDungeonHeroineFollow && PlayerDataManager.DungeonHeroineFollowNum == impossibleFollowHeroineId)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[checkEndFlagName])
			{
				Transition(stateLink);
			}
			else if (PlayerFlagDataManager.scenarioFlagDictionary[checkStartFlagName])
			{
				totalMapAccessManager.alertTextLoc.Term = alertTextTerm;
				OpenMapAlertDialog();
			}
			else
			{
				Transition(stateLink);
			}
		}
		else
		{
			Transition(stateLink);
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
		Transition(disableLink);
	}
}
