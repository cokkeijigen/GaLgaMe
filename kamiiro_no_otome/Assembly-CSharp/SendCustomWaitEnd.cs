using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendCustomWaitEnd : StateBehaviour
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
		chatWindowControl.backLogList.Clear();
		chatWindowControl.isPause = false;
		chatWindowControl.isEndText = false;
		chatWindowControl.isRecreate = false;
		chatWindowControl.chatGoList.Clear();
		chatWindowControl.chatMessageList.Clear();
		chatWindowControl.chatNameList.Clear();
		chatWindowControl.advEngine.ResumeScenario();
		Debug.Log("ポーズシナリオ終了");
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
