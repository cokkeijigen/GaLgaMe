using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetCanvasGroupVisible : StateBehaviour
{
	public CanvasGroup canvasGroup;

	public bool setValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (setValue)
		{
			canvasGroup.interactable = true;
			canvasGroup.alpha = 1f;
		}
		else
		{
			canvasGroup.interactable = false;
			canvasGroup.alpha = 0.5f;
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
