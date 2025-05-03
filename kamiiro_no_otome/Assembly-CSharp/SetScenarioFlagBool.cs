using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioFlagBool : StateBehaviour
{
	public string flagName;

	public bool setValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerFlagDataManager.scenarioFlagDictionary[flagName] = setValue;
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
