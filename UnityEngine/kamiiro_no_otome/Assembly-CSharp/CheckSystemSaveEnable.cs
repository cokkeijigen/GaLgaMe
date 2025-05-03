using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSystemSaveEnable : StateBehaviour
{
	public CanvasGroup saveTabButton;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.systemSaveEnable)
		{
			saveTabButton.interactable = true;
			saveTabButton.alpha = 1f;
		}
		else
		{
			saveTabButton.interactable = false;
			saveTabButton.alpha = 0.5f;
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
