using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckLearnedSkill : StateBehaviour
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
		int skillID = statusManager.selectSkillId;
		LearnedSkillData learnedSkillData = PlayerInventoryDataManager.playerLearnedSkillList.Find((LearnedSkillData data) => data.skillID == skillID);
		CanvasGroup component = statusSkillViewManager.skillLearnButtonGO.GetComponent<CanvasGroup>();
		if (learnedSkillData != null)
		{
			statusSkillViewManager.skillLearnButtonLoc.Term = "buttonLearned";
			statusSkillViewManager.learnCostGroup.SetActive(value: false);
			component.alpha = 0.5f;
			component.interactable = false;
		}
		else
		{
			statusSkillViewManager.skillLearnButtonLoc.Term = "buttonLearn";
			int learnCost = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID).learnCost;
			statusSkillViewManager.learnCostText.text = learnCost.ToString();
			statusSkillViewManager.learnCostGroup.SetActive(value: true);
			if (PlayerDataManager.playerHaveKizunaPoint >= learnCost)
			{
				component.alpha = 1f;
				component.interactable = true;
			}
			else
			{
				component.alpha = 0.5f;
				component.interactable = false;
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
