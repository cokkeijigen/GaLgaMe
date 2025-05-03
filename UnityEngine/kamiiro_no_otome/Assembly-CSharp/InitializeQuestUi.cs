using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeQuestUi : StateBehaviour
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
		questManager.selectScrollContentIndex = 0;
		if (PlayerDataManager.isNoCheckNewQuest)
		{
			if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
			{
				questManager.newStoryQuestBalloonGo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-146f, 0f);
			}
			else
			{
				questManager.newStoryQuestBalloonGo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-195f, 0f);
			}
			questManager.newStoryQuestBalloonGo.SetActive(value: true);
		}
		else
		{
			questManager.newStoryQuestBalloonGo.SetActive(value: false);
		}
		if (PlayerDataManager.isNoCheckNewSubQuest)
		{
			questManager.newStorySubQuestBalloonGo.SetActive(value: true);
		}
		else
		{
			questManager.newStorySubQuestBalloonGo.SetActive(value: false);
		}
		if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
		{
			questManager.questTypeTabGoArray[0].SetActive(value: true);
			questManager.selectTabTypeNum = 0;
		}
		else
		{
			questManager.questTypeTabGoArray[0].SetActive(value: false);
			questManager.selectTabTypeNum = 2;
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
