using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class StartFadeOutTransition : StateBehaviour
{
	private CanvasGroup canvasGroup;

	public float time;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public override void OnStateBegin()
	{
		canvasGroup.DOFade(0f, time).OnComplete(delegate
		{
			canvasGroup.blocksRaycasts = false;
			Transition(stateLink);
		});
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
