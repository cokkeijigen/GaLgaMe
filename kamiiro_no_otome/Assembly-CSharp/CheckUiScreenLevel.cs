using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckUiScreenLevel : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		if (parameterContainer.GetInt("uiScreenLevel") == PlayerNonSaveDataManager.openUiScreenLevel)
		{
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
		}
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
