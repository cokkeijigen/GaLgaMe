using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendGarellyPageButtonDown : StateBehaviour
{
	private GarellyManager garellyManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		garellyManager = GameObject.Find("Garelly Manager").GetComponent<GarellyManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		int @int = parameterContainer.GetInt("pageNum");
		garellyManager.PushPageButton(@int);
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
