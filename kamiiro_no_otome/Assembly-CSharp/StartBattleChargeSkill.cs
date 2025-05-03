using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class StartBattleChargeSkill : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleTurnManager.playerSkillData = scenarioBattleTurnManager.useSkillData;
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		GameObject[] commandButtonGroup = utageBattleSceneManager.commandButtonGroup;
		for (int i = 0; i < commandButtonGroup.Length; i++)
		{
			commandButtonGroup[i].SetActive(value: false);
		}
		utageBattleSceneManager.chargeAttackButton.SetActive(value: false);
		utageBattleSceneManager.chargeAttackEffectGo.SetActive(value: false);
		utageBattleSceneManager.battleCanvas.GetComponent<CanvasGroup>().interactable = false;
		utageBattleSceneManager.SetEnemyTargetGroupVisble(isVisible: false);
		int j;
		for (j = 0; j < PlayerStatusDataManager.enemyMember.Length; j++)
		{
			if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
			{
				SkillAttackHitData item = new SkillAttackHitData
				{
					memberNum = j,
					memberID = PlayerStatusDataManager.enemyMember[j],
					isAttackHit = true
				};
				scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
			}
		}
		for (int k = 0; k < scenarioBattleSkillManager.chargeSkillCharacterId.Count; k++)
		{
			PlayerStatusDataManager.characterSp[scenarioBattleSkillManager.chargeSkillCharacterId[k]] = 0;
		}
		foreach (GameObject playerFrameGo in utageBattleSceneManager.playerFrameGoList)
		{
			playerFrameGo.GetComponent<ArborFSM>().SendTrigger("ConsumeCharacterSp");
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
