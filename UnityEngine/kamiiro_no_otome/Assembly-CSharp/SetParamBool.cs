using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetParamBool : StateBehaviour
{
	public string setParamName;

	public bool setBoolValue;

	private ParameterContainer container;

	public StateLink nextState;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		container = GameObject.Find("Arbor Variable").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		container.SetBool(setParamName, setBoolValue);
		Transition(nextState);
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
