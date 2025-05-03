using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckBackLogVisible : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public StateLink visibleLink;

	public StateLink inVisibleLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
		if (chatWindowControl.backLogWindowGo.activeInHierarchy)
		{
			Transition(visibleLink);
		}
		else
		{
			Transition(inVisibleLink);
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
