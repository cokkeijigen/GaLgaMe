using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class StatusSkillLearnApply : StateBehaviour
{
	private StatusManager statusManager;

	private StatusSkillViewManager statusSkillViewManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusSkillViewManager = GameObject.Find("Skill View Manager").GetComponent<StatusSkillViewManager>();
	}

	public override void OnStateBegin()
	{
		int learnCost = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == statusManager.selectSkillId).learnCost;
		PlayerDataManager.playerHaveKizunaPoint -= learnCost;
		PlayerInventoryDataEquipAccess.AddLearnedSkill(statusManager.selectSkillId);
		PlayerQuestDataManager.RefreshStoryQuestFlagData("skillLearn", 1);
		statusSkillViewManager.learnSkillSummaryLoc.Term = "alertSkillLearnApply";
		statusSkillViewManager.dialogButtonArray[0].SetActive(value: false);
		statusSkillViewManager.dialogButtonArray[1].SetActive(value: true);
		MasterAudio.PlaySound("SeSkillLearned", 1f, null, 0f, null, null);
		Transform transform = PoolManager.Pools["Status Custom Pool"].Spawn(statusSkillViewManager.learnedEffectPrefabGo, statusSkillViewManager.uIParticle.transform);
		statusSkillViewManager.learnedEffectSpawnGo = transform;
		transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.localScale = new Vector3(1f, 1f, 1f);
		statusSkillViewManager.uIParticle.RefreshParticles();
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
