using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendBattleSupportSkillEnd : StateBehaviour
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
		Debug.Log("サポートメンバーのスキル終了");
		scenarioBattleTurnManager.isUseSkillPlayer = false;
		scenarioBattleTurnManager.isSupportSkill = false;
		scenarioBattleTurnManager.battleUseSkillID = 0;
		scenarioBattleSkillManager.isUseSkill = false;
		scenarioBattleSkillManager.isUseChargeSkill = false;
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
