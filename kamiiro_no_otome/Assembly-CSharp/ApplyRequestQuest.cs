using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class ApplyRequestQuest : StateBehaviour
{
	private QuestManager questManager;

	private QuestApplyManager questApplyManager;

	public StateLink stateLink;

	public StateLink noticeLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
		questApplyManager = GameObject.Find("Quest Apply Manager").GetComponent<QuestApplyManager>();
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		questManager.questCanvasGroup.interactable = false;
		MasterAudio.PlaySound("SeQuestApply", 1f, null, 0f, null, null);
		QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID);
		PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questManager.clickedQuestID).isOrdered = true;
		questManager.selectScrollContentIndex = 0;
		PlayerQuestDataManager.RefreshEnableRequestQuestList();
		PlayerQuestDataManager.RefreshEnableOrderedQuestList();
		if (questData.questType == QuestData.QuestType.supply)
		{
			PlayerQuestDataManager.RefreshSupplyQuestHaveItemCount();
		}
		if (!string.IsNullOrEmpty(questData.questUnlockRecipeName))
		{
			PlayerFlagDataManager.recipeFlagDictionary[questData.questUnlockRecipeName] = true;
			flag = true;
			questApplyManager.OpenQuestNoticeCanvas("recipe", "");
		}
		if (questData.sortID == 4010 || questData.sortID == 4011 || questData.sortID == 21)
		{
			flag = true;
			if (questData.sortID == 4010)
			{
				PlayerFlagDataManager.deepDungeonFlagDictionary["Dungeon1"] = 1;
				questApplyManager.OpenQuestNoticeCanvas("dungeon", "areaDungeon1");
			}
			else if (questData.sortID == 4011)
			{
				PlayerFlagDataManager.deepDungeonFlagDictionary["Dungeon2"] = 1;
				questApplyManager.OpenQuestNoticeCanvas("dungeon", "areaDungeon2");
			}
			else
			{
				PlayerFlagDataManager.deepDungeonFlagDictionary["Forest1"] = 1;
				questApplyManager.OpenQuestNoticeCanvas("dungeon", "areaForest1");
			}
		}
		if (flag)
		{
			Transition(noticeLink);
		}
		else
		{
			Transition(stateLink);
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
