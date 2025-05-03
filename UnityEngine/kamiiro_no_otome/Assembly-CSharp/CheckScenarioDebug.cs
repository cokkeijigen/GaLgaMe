using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class CheckScenarioDebug : StateBehaviour
{
	public AdvEngine advEngine;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		bool isScenarioDebug = GameObject.Find("GameScene Manager").GetComponent<UtageCommandManager>().isScenarioDebug;
		advEngine.Param.SetParameterBoolean("isScenarioDebug", isScenarioDebug);
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
