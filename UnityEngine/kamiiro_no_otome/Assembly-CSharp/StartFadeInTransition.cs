using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class StartFadeInTransition : StateBehaviour
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
		canvasGroup.blocksRaycasts = true;
		canvasGroup.DOFade(1f, time).OnComplete(delegate
		{
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
