using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendLockSaveLoadCloseButton : StateBehaviour
{
	public PlayMakerFSM playMakerFSM;

	public bool isRightClickLock;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		playMakerFSM.FsmVariables.GetFsmBool("isRightClickLock").Value = isRightClickLock;
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
