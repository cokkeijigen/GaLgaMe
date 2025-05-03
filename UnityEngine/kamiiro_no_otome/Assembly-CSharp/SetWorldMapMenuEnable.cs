using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetWorldMapMenuEnable : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public bool isEnable;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (isEnable)
		{
			headerStatusManager.menuCanvasGroup.interactable = true;
			headerStatusManager.menuCanvasGroup.alpha = 1f;
		}
		else
		{
			headerStatusManager.menuCanvasGroup.interactable = false;
			headerStatusManager.menuCanvasGroup.alpha = 0.5f;
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
