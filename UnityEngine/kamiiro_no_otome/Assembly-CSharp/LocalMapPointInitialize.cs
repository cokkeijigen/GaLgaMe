using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class LocalMapPointInitialize : StateBehaviour
{
	private ParameterContainer parameterContainer;

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
		parameterContainer.GetVariable<I2LocalizeComponent>("placeNameLoc").localize.Term = "place" + base.transform.parent.gameObject.name;
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
