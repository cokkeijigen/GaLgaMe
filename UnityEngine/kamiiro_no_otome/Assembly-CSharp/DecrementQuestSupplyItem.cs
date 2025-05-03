using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DecrementQuestSupplyItem : StateBehaviour
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
		QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID);
		if (questData.questType == QuestData.QuestType.supply || questData.questType == QuestData.QuestType.craft)
		{
			int num = questData.requirementList[0];
			int count = questData.requirementList[1];
			if (num < 900)
			{
				PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(num, count);
			}
			else if (num >= 950)
			{
				PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(num, count);
			}
		}
		if (questData.questType == QuestData.QuestType.itemGet || questData.questType == QuestData.QuestType.craft)
		{
			if (questData.isDeleteTheRequirementItemAtClear)
			{
				PlayerInventoryDataAccess.ConsumePlayerHaveItems_SINGLE(questData.requirementList[0]);
				Debug.Log("クエストID：" + questData.sortID + "／クリア後に依頼品を削除する");
			}
			else
			{
				Debug.Log("クエストID：" + questData.sortID + "／クリア後に依頼品を削除なし");
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
