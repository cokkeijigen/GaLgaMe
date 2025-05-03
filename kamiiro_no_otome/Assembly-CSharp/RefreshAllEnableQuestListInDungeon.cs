using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshAllEnableQuestListInDungeon : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

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
		PlayerQuestDataManager.RefreshEnableRequestQuestList();
		PlayerQuestDataManager.RefreshEnableOrderedQuestList();
		PlayerQuestDataManager.RefreshEnableStoryQuestList();
		PlayerQuestDataManager.RefreshSupplyQuestHaveItemCount();
		PlayerQuestDataManager.RefreshStoryQuestFlagData("sexScenario");
		PlayerQuestDataManager.CheckUnreportedQuest();
		if (PlayerDataManager.isNoCheckNewQuest)
		{
			dungeonMapManager.questNewAlertIconGo.SetActive(value: true);
			Debug.Log("新規クエストアラート表示");
		}
		else
		{
			dungeonMapManager.questNewAlertIconGo.SetActive(value: false);
			Debug.Log("新規クエストアラート非表示");
			if (PlayerNonSaveDataManager.isUnreportedQuest)
			{
				dungeonMapManager.questNoticeAlertIconGo.SetActive(value: true);
				Debug.Log("未報告アラート表示");
			}
			else
			{
				dungeonMapManager.questNoticeAlertIconGo.SetActive(value: false);
				Debug.Log("未報告アラート非表示");
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
