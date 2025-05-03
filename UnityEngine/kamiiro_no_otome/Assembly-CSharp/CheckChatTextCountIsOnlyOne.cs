using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckChatTextCountIsOnlyOne : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public StateLink stateLink;

	public StateLink onlyOne;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
		if (chatWindowControl.enableListCount == 1)
		{
			Transition(onlyOne);
		}
		else
		{
			Transition(stateLink);
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
