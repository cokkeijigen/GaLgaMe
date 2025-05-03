using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckEnableTextCount : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public StateLink stateLink;

	public StateLink skipLink;

	public StateLink recreateEnd;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
		chatWindowControl.isChatCreating = false;
		if (chatWindowControl.enableListCount == 1)
		{
			chatWindowControl.chatMaster.transform.GetChild(0).Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: true);
		}
		if (chatWindowControl.loopCount >= chatWindowControl.enableListCount)
		{
			Transition(recreateEnd);
		}
		if (chatWindowControl.isPrefabVisibleSkip)
		{
			Transition(skipLink);
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
