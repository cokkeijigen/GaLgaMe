using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetHeaderStatusMenuEnable : StateBehaviour
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
			headerStatusManager.menuCanvasGroup.interactable = true;
			headerStatusManager.menuCanvasGroup.blocksRaycasts = true;
			headerStatusManager.menuCanvasGroup.alpha = 1f;
			headerStatusManager.mapHelpMarkCanvasGroup.interactable = true;
			if (PlayerDataManager.isLocalMapActionLimit)
			{
				headerStatusManager.restShortcutCanvasGroup.interactable = false;
				headerStatusManager.restShortcutCanvasGroup.alpha = 0.5f;
				headerStatusManager.exitButton.interactable = false;
				headerStatusManager.exitButton.alpha = 0.5f;
			}
			else if (PlayerDataManager.mapPlaceStatusNum == 0)
			{
				headerStatusManager.restShortcutCanvasGroup.interactable = true;
				headerStatusManager.restShortcutCanvasGroup.alpha = 1f;
				headerStatusManager.exitButton.interactable = false;
				headerStatusManager.exitButton.alpha = 0.5f;
			}
			else if (PlayerDataManager.mapPlaceStatusNum == 2)
			{
				headerStatusManager.restShortcutCanvasGroup.interactable = false;
				headerStatusManager.restShortcutCanvasGroup.alpha = 0.5f;
				headerStatusManager.exitButton.interactable = false;
				headerStatusManager.exitButton.alpha = 0.5f;
			}
			else
			{
				headerStatusManager.restShortcutCanvasGroup.interactable = true;
				headerStatusManager.restShortcutCanvasGroup.alpha = 1f;
				headerStatusManager.exitButton.interactable = true;
				headerStatusManager.exitButton.blocksRaycasts = true;
				headerStatusManager.exitButton.alpha = 1f;
			}
		}
		else
		{
			headerStatusManager.menuCanvasGroup.interactable = false;
			headerStatusManager.menuCanvasGroup.alpha = 0.5f;
			headerStatusManager.exitButton.interactable = false;
			headerStatusManager.exitButton.alpha = 0.5f;
			headerStatusManager.mapHelpMarkCanvasGroup.interactable = false;
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
