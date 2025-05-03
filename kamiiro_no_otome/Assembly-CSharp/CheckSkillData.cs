using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSkillData : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleSkillManager.blackImageGoArray[0].SetActive(value: true);
		scenarioBattleTurnManager.isUseSkillPlayer = true;
		scenarioBattleSkillManager.isUseSkill = true;
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

	private void SetTargetPlayer()
	{
		scenarioBattleTurnManager.setFrameTypeName = "select";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		Debug.Log("味方を対象にする");
	}

	private void SetTargetEnemy()
	{
		scenarioBattleTurnManager.setFrameTypeName = "select";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		Debug.Log("敵を対象にする");
	}
}
