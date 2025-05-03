using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckChatTextCountIsLast : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public StateLink stateLink;

	public StateLink chatEnd;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
		if (chatWindowControl.chatMessageList.Count > 0)
		{
			Transition(stateLink);
		}
		else
		{
			Transition(chatEnd);
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
