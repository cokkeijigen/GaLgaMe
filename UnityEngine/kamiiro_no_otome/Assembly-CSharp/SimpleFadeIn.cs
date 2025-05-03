using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SimpleFadeIn : StateBehaviour
{
	private CanvasGroup canvasGroup;

	public bool isCanvas;

	public float time;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		if (isCanvas)
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}
		else
		{
			canvasGroup = GameObject.Find("Transition Canvas").GetComponent<CanvasGroup>();
		}
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
