using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetParamString : StateBehaviour
{
	public string setParamName;

	public string setStringValue;

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
		container.SetString(setParamName, setStringValue);
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
