using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendBattleSkillEnd : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public StateLink positiveSkillLink;

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
		Debug.Log("スキル終了");
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			scenarioBattleTurnManager.isUseSkillPlayer = false;
			scenarioBattleSkillManager.isUseSkill = false;
			scenarioBattleSkillManager.isUseChargeSkill = false;
			BattleSkillData playerSkillData = scenarioBattleTurnManager.playerSkillData;
			if (playerSkillData.skillType == BattleSkillData.SkillType.attack || playerSkillData.skillType == BattleSkillData.SkillType.magicAttack || playerSkillData.skillType == BattleSkillData.SkillType.mixAttack || playerSkillData.skillType == BattleSkillData.SkillType.chargeAttack || playerSkillData.skillType == BattleSkillData.SkillType.deBuff)
			{
				Transition(stateLink);
			}
			else
			{
				Transition(positiveSkillLink);
			}
		}
		else
		{
			BattleSkillData enemySkillData = scenarioBattleTurnManager.enemySkillData;
			if (enemySkillData.skillType == BattleSkillData.SkillType.attack || enemySkillData.skillType == BattleSkillData.SkillType.magicAttack || enemySkillData.skillType == BattleSkillData.SkillType.mixAttack || enemySkillData.skillType == BattleSkillData.SkillType.deBuff)
			{
				GameObject.Find("Battle Enemy Turn").GetComponent<ArborFSM>().SendTrigger("EnemySkillEnd");
			}
			else
			{
				GameObject.Find("Battle Enemy Turn").GetComponent<ArborFSM>().SendTrigger("EnemyPositiveSkillEnd");
			}
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
