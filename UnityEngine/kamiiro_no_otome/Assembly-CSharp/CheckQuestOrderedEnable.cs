using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckQuestOrderedEnable : StateBehaviour
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
		questManager.isQuestClearApplyButton = true;
		if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
		{
			QuestClearData questClearData = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questManager.clickedQuestID);
			if (questManager.isQuestCleared)
			{
				questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedCleared";
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
			}
			else if (questClearData.needRequirementCount <= questClearData.currentRequirementCount)
			{
				questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApply";
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = true;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
			}
			else
			{
				questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApplyDisable";
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
			}
		}
		else if (questManager.selectTabTypeNum == 2 || questManager.selectTabTypeNum == 3)
		{
			QuestClearData questClearData2 = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questManager.clickedQuestID);
			if (questManager.isQuestCleared)
			{
				questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedCleared";
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
			}
			else if (GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID).questType == QuestData.QuestType.totalSalesAmount)
			{
				if (questClearData2.needRequirementCount <= PlayerDataManager.totalSalesAmount)
				{
					questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApply";
					questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = true;
					questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
				}
				else
				{
					questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApplyDisable";
					questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
					questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
				}
			}
			else if (questClearData2.needRequirementCount <= questClearData2.currentRequirementCount)
			{
				questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApply";
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = true;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
			}
			else
			{
				questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApplyDisable";
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
			}
		}
		else
		{
			QuestClearData questClearData3 = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questManager.clickedQuestID);
			if (questManager.isQuestCleared)
			{
				questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedCleared";
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
			}
			else
			{
				if (questClearData3.needRequirementCount <= questClearData3.currentRequirementCount)
				{
					questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApplyUnReported";
				}
				else
				{
					questManager.questApplyButtonTextLoc.Term = "buttonQuestOrderedApplyDisable";
				}
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().interactable = false;
				questManager.questApplyButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
			}
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
