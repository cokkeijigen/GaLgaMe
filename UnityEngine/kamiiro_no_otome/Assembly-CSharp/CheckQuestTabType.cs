using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckQuestTabType : StateBehaviour
{
	private QuestManager questManager;

	public StateLink requestLink;

	public StateLink orderedLink;

	public StateLink storyLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
	}

	public override void OnStateBegin()
	{
		switch (questManager.selectTabTypeNum)
		{
		case 0:
			Transition(requestLink);
			break;
		case 1:
			Transition(orderedLink);
			break;
		case 2:
			if (GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID).rewardList != null)
			{
				Transition(orderedLink);
			}
			else
			{
				Transition(storyLink);
			}
			break;
		case 3:
			if (GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID).rewardList != null)
			{
				Transition(orderedLink);
			}
			else
			{
				Transition(storyLink);
			}
			break;
		}
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
