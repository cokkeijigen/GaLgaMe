using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckRouteSelectButton : StateBehaviour
{
	public CanvasGroup canvasGroup;

	private DungeonMapManager dungeonMapManager;

	public CanvasGroup applyButton;

	public CanvasGroup resetButton;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (dungeonMapManager.isBossRouteSelect)
		{
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.alpha = 0.7f;
			applyButton.interactable = true;
			applyButton.alpha = 1f;
		}
		else if (dungeonMapManager.selectCardList.Count >= 3)
		{
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.alpha = 0.7f;
			applyButton.interactable = true;
			applyButton.alpha = 1f;
		}
		else
		{
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			canvasGroup.alpha = 1f;
			applyButton.interactable = false;
			applyButton.alpha = 0.5f;
		}
		if (dungeonMapManager.selectCardList.Count >= 1 || dungeonMapManager.isBossRouteSelect)
		{
			resetButton.interactable = true;
			resetButton.alpha = 1f;
		}
		else
		{
			resetButton.interactable = false;
			resetButton.alpha = 0.5f;
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
