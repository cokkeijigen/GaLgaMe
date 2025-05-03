using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class CheckMessageWindowVisible : StateBehaviour
{
	private AdvEngine advEngine;

	private ChatWindowControl chatWindowControl;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		advEngine = GameObject.Find("AdvEngine").GetComponent<AdvEngine>();
		chatWindowControl = GameObject.Find("Chat Manager").GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
		if (!chatWindowControl.isFullMode)
		{
			return;
		}
		if (!chatWindowControl.isChatCreating && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
		{
			if (chatWindowControl.backLogWindowGo.activeInHierarchy)
			{
				chatWindowControl.isBackLogVisible = false;
				chatWindowControl.isPushUtageButton = false;
				Debug.Log("フルモード時のバックログ非表示");
			}
			else if (advEngine.UiManager.Status == AdvUiManager.UiStatus.Default)
			{
				chatWindowControl.isPushUtageButton = false;
				Debug.Log("メッセージウィンドウ表示");
			}
			else
			{
				chatWindowControl.isPushUtageButton = true;
				Debug.Log("メッセージウィンドウ非表示");
			}
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
