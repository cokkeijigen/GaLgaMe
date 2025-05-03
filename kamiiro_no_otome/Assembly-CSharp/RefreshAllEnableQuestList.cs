using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshAllEnableQuestList : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerQuestDataManager.RefreshEnableRequestQuestList();
		PlayerQuestDataManager.RefreshEnableOrderedQuestList();
		PlayerQuestDataManager.RefreshEnableStoryQuestList();
		PlayerQuestDataManager.RefreshSupplyQuestHaveItemCount();
		PlayerQuestDataManager.RefreshStoryQuestFlagData("sexScenario");
		PlayerQuestDataManager.RefreshStoryQuestFlagData("dungeonClear");
		PlayerQuestDataManager.CheckUnreportedQuest();
		GameObject gameObject = GameObject.Find("Header Status Manager");
		if (gameObject != null)
		{
			HeaderStatusManager component = gameObject.GetComponent<HeaderStatusManager>();
			if (PlayerDataManager.isNoCheckNewQuest || PlayerDataManager.isNoCheckNewSubQuest)
			{
				component.newQuestBalloonGo.SetActive(value: true);
				Debug.Log("新規クエストアラート表示");
			}
			else
			{
				component.newQuestBalloonGo.SetActive(value: false);
				Debug.Log("新規クエストアラート非表示");
				if (PlayerNonSaveDataManager.isUnreportedQuest)
				{
					component.alertQuestBalloonGo.SetActive(value: true);
					Debug.Log("未報告アラート表示");
				}
				else
				{
					component.alertQuestBalloonGo.SetActive(value: false);
					Debug.Log("未報告アラート非表示");
				}
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
