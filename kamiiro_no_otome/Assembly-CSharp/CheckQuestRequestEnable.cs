using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckQuestRequestEnable : StateBehaviour
{
	private QuestManager questManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
	}

	public override void OnStateBegin()
	{
		questManager.isQuestClearApplyButton = false;
		if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
		{
			questManager.questApplyButtonTextLoc.Term = "buttonQuestRequestApply";
			questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = true;
			questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		}
		else
		{
			questManager.questApplyButtonTextLoc.Term = "buttonQuestRequestApply";
			questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
			questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
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
