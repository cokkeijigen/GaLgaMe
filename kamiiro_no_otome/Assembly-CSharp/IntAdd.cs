using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class IntAdd : StateBehaviour
{
	public int addValue;

	public FlexibleInt inputInt;

	public OutputSlotInt outputInt;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		outputInt.SetValue(inputInt.value + addValue);
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
