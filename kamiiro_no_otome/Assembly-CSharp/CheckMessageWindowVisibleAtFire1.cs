using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

[AddComponentMenu("")]
public class CheckMessageWindowVisibleAtFire1 : StateBehaviour
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
		if (SceneManager.GetSceneByName("systemUI").isLoaded)
		{
			Debug.Log("フルモード時の環境設定表示中");
		}
		else if (!chatWindowControl.isChatCreating && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
		{
			if (chatWindowControl.backLogWindowGo.activeInHierarchy)
			{
				Debug.Log("フルモード時のバックログ表示中");
			}
			else if (advEngine.UiManager.Status == AdvUiManager.UiStatus.Default)
			{
				chatWindowControl.isPushUtageButton = false;
				Debug.Log("メッセージウィンドウ表示");
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
