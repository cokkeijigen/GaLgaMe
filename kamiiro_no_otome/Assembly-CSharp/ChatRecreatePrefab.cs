using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChatRecreatePrefab : StateBehaviour
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
		chatWindowControl.DestroyChatPrefabs();
		chatWindowControl.RecreateChatPrefabs(chatWindowControl.chatMessageList.Count);
		Canvas.ForceUpdateCanvases();
		Transition(stateLink);
	}
}
