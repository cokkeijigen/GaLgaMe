using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckPushUtageButton : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!chatWindowControl.isPushUtageButton)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
