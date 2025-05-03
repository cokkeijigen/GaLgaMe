using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckCommandQueueWait : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!parameterContainer.GetBool("isItemQueued") && !parameterContainer.GetBool("isRetreatQueued"))
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
