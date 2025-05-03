using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChatCreatePrefab : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public StateLink stateLink;

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
		chatWindowControl.messageTexts.transform.GetComponent<CanvasGroup>().alpha = 0f;
	}

	public override void OnStateBegin()
	{
		if (chatWindowControl.isFullMode)
		{
			chatWindowControl.isChatCreating = true;
			chatWindowControl.CreateChatPrefabs();
		}
		else
		{
			chatWindowControl.SetMessageWindowImage();
		}
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
