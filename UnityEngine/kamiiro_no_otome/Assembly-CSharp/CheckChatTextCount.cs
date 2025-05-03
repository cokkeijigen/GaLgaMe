using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckChatTextCount : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public StateLink stateLink;

	public StateLink skipLink;

	public StateLink onlyOneEnd;

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
			if (chatWindowControl.isPrefabVisibleSkip)
			{
				Transition(skipLink);
			}
			else
			{
				Transition(stateLink);
			}
		}
		else if (chatWindowControl.enableListCount == 1 || chatWindowControl.isPrefabVisibleSkip)
		{
			Transition(onlyOneEnd);
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
