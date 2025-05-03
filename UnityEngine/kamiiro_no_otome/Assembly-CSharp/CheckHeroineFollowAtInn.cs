using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckHeroineFollowAtInn : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	private PlayMakerFSM alertDialogFSM;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	public StateLink alertLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		parameterContainer = GetComponentInParent<ParameterContainer>();
		alertDialogFSM = totalMapAccessManager.mapAlertDialogGo.GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		string @string = parameterContainer.GetString("scenarioName");
		parameterContainer.GetInt("scenarioSortId");
		if (!string.IsNullOrEmpty(@string))
		{
			if (@string == "MH_Levy_001")
			{
				if (PlayerDataManager.isDungeonHeroineFollow && PlayerDataManager.DungeonHeroineFollowNum == 2)
				{
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredShia";
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
		Transition(alertLink);
	}
}
