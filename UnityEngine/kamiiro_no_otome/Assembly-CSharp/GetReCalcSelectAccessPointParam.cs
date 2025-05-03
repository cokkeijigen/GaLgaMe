using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GetReCalcSelectAccessPointParam : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public InputSlotAny inputSlotAny;

	private bool isInitialized;

	public StateLink stateLink;

	public StateLink disableLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		isInitialized = false;
		inputSlotAny.TryGetValue<ParameterContainer>(out parameterContainer);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (parameterContainer.GetBool("isInitialize") && !isInitialized)
		{
			isInitialized = true;
			string @string = parameterContainer.GetString("disablePointTerm");
			if (parameterContainer.GetBool("isWorldMapPointDisable"))
			{
				PlayerNonSaveDataManager.selectDisableMapPointTerm = @string;
				Transition(disableLink);
			}
			else
			{
				Transition(stateLink);
			}
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
