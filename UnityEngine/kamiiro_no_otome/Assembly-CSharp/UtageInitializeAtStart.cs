using Arbor;
using PathologicalGames;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class UtageInitializeAtStart : StateBehaviour
{
	public AdvEngine advEngine;

	private ChatWindowControl chatWindowControl;

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
		advEngine.BacklogManager.Backlogs.Clear();
		chatWindowControl.isPushUtageButton = false;
		chatWindowControl.isFullMode = false;
		chatWindowControl.isPause = false;
		chatWindowControl.isEndText = false;
		chatWindowControl.isBackLogVisible = false;
		chatWindowControl.isRecreate = false;
		chatWindowControl.isChatCreating = false;
		chatWindowControl.chatGoList.Clear();
		chatWindowControl.restoreBackLogList.Clear();
		CanvasGroup[] componentsInChildren = chatWindowControl.messageWindowManager.GetComponentsInChildren<CanvasGroup>(includeInactive: true);
		foreach (CanvasGroup canvasGroup in componentsInChildren)
		{
			if (canvasGroup.gameObject.CompareTag("ChatText"))
			{
				PoolManager.Pools["ChatPool"].Despawn(canvasGroup.transform, 0f, chatWindowControl.poolParent.transform);
			}
		}
		Debug.Log("宴開始時／チャットPrefabを全て削除");
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
