using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PutBackUiScreenLevel : StateBehaviour
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
		PlayerNonSaveDataManager.openUiScreenLevel = PlayerNonSaveDataManager.beforeUiScreenLevel;
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
