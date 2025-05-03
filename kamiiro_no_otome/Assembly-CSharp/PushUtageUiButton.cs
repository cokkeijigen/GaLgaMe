using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class PushUtageUiButton : StateBehaviour
{
	public enum Type
	{
		backLog,
		close
	}

	private ChatWindowControl chatWindowControl;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GameObject.Find("Chat Manager").GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
		AdvUguiMessageWindow component = GameObject.Find("ChatWindow").GetComponent<AdvUguiMessageWindow>();
		switch (type)
		{
		case Type.backLog:
			chatWindowControl.isBackLogVisible = true;
			component.OnTapBackLog();
			break;
		case Type.close:
			component.OnTapCloseWindow();
			break;
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
