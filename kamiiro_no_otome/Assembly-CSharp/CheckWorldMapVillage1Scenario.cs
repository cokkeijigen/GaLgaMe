using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckWorldMapVillage1Scenario : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public StateLink disableLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		string key = "M_Main_001-4";
		if (PlayerFlagDataManager.scenarioFlagDictionary[key])
		{
			Transition(stateLink);
			return;
		}
		parameterContainer.GetGameObject("alertBalloon").gameObject.SetActive(value: false);
		parameterContainer.SetBool("isWorldMapToUtage", value: false);
		parameterContainer.SetBool("isWorldMapToInDoor", value: false);
		parameterContainer.SetBool("isWorldMapPointDisable", value: true);
		parameterContainer.SetString("disablePointTerm", "dialogWorldMapDisable");
		Transition(disableLink);
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
