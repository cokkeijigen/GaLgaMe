using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckThisPlaceOtherHeroine : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	private PlayMakerFSM alertDialogFSM;

	public StateLink otherHeroineLink;

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
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			ParameterContainer componentInParent = GetComponentInParent<ParameterContainer>();
			componentInParent.TryGetBool("isHeroineExist", out var value);
			if (value)
			{
				if (GetComponentInParent<ParameterContainer>().GetBool("isHeroineExist"))
				{
					string scenarioName = componentInParent.GetString("scenarioName");
					if (!string.IsNullOrEmpty(scenarioName))
					{
						string text = base.transform.parent.name;
						if (scenarioName != text)
						{
							bool otherHeroineExceptionIsExists = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == scenarioName).otherHeroineExceptionIsExists;
							Debug.Log("シナリオ名：" + scenarioName + "／例外bool：" + otherHeroineExceptionIsExists);
							if (otherHeroineExceptionIsExists)
							{
								Transition(stateLink);
								return;
							}
							totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredSolo";
							OpenMapAlertDialog();
						}
						else
						{
							totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredSolo";
							OpenMapAlertDialog();
						}
					}
					else
					{
						totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredSolo";
						OpenMapAlertDialog();
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
		totalMapAccessManager.mapCanvasGroupArray[0].interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[0].blocksRaycasts = false;
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[1].blocksRaycasts = false;
		headerStatusManager.menuCanvasGroup.alpha = 0.5f;
		headerStatusManager.menuCanvasGroup.interactable = false;
		headerStatusManager.menuCanvasGroup.blocksRaycasts = false;
		headerStatusManager.exitButton.interactable = false;
		headerStatusManager.exitButton.blocksRaycasts = false;
		headerStatusManager.exitButton.alpha = 0.5f;
		localMapAccessManager.SetLocalMapExitEnable(isEnable: false);
		totalMapAccessManager.mapAlertDialogGo.SetActive(value: true);
		alertDialogFSM.SendEvent("CloseMapAlertDialog");
		Transition(otherHeroineLink);
	}
}
