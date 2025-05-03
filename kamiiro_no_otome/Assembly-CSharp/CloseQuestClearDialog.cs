using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CloseQuestClearDialog : StateBehaviour
{
	private QuestManager questManager;

	private QuestApplyManager questApplyManager;

	public StateLink stateLink;

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
		questManager.ResetScrollViewContents("clear");
		if (PoolManager.Pools["questPool"].IsSpawned(questApplyManager.questClearSpawnGo))
		{
			PoolManager.Pools["questPool"].Despawn(questApplyManager.questClearSpawnGo, questApplyManager.questPoolParent);
		}
		if (PoolManager.Pools["questPool"].IsSpawned(questApplyManager.questLevelUpSpawnGo))
		{
			PoolManager.Pools["questPool"].Despawn(questApplyManager.questLevelUpSpawnGo, questApplyManager.questPoolParent);
		}
		questApplyManager.uIParticle.RefreshParticles();
		questApplyManager.uIParticle_levelUp.RefreshParticles();
		questApplyManager.dialogCanvasGo.SetActive(value: false);
		questManager.selectScrollContentIndex = 0;
		QuestData.QuestType questType = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID).questType;
		if ((uint)questType <= 1u || questType == QuestData.QuestType.contract)
		{
			PlayerQuestDataManager.RefreshEnableOrderedQuestList();
		}
		else
		{
			PlayerQuestDataManager.RefreshEnableStoryQuestList();
		}
		if (questApplyManager.isRewardRareItem)
		{
			questApplyManager.isRewardRareItem = false;
			questApplyManager.OpenQuestNoticeCanvas("rare", "");
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
