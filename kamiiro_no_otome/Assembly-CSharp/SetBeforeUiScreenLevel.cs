using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetBeforeUiScreenLevel : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.beforeUiScreenLevel = PlayerNonSaveDataManager.openUiScreenLevel;
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
