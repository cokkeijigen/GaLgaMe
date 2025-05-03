using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SexDamageZoomStart : StateBehaviour
{
	public Camera sexTouchCamera;

	public float animationTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		sexTouchCamera.DOOrthoSize(5.1f, animationTime).SetLoops(2, LoopType.Yoyo);
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
