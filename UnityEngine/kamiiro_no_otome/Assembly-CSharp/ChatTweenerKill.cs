using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class ChatTweenerKill : StateBehaviour
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
		chatWindowControl.isPrefabVisibleSkip = true;
		chatWindowControl.tweener.Kill(complete: true);
		Debug.Log("TweenerをKill");
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
