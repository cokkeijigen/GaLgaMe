using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushUtageButtonGroup : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public bool setValue;

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
		if (chatWindowControl == null)
		{
			chatWindowControl = GameObject.Find("Chat Manager").GetComponent<ChatWindowControl>();
		}
		chatWindowControl.isPushUtageButton = setValue;
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
