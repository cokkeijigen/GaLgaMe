using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class EndUtageEngine : StateBehaviour
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
		advEngine.EndScenario();
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (advEngine.IsEndScenario)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
