using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetHeaderStatusClockGroupEnable : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public bool isEnable;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		if (isEnable)
		{
			headerStatusManager.clockCanvasGroup.interactable = true;
			headerStatusManager.clockCanvasGroup.blocksRaycasts = true;
			headerStatusManager.clockCanvasGroup.alpha = 1f;
		}
		else
		{
			headerStatusManager.clockCanvasGroup.interactable = false;
			headerStatusManager.clockCanvasGroup.blocksRaycasts = false;
			headerStatusManager.clockCanvasGroup.alpha = 0.5f;
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
