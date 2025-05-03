using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class PushEnemyImageButtonGo : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		int num = parameterContainer.GetInt("enemyPartyNum");
		if (scenarioBattleSkillManager.isUseSkill)
		{
			scenarioBattleTurnManager.playerTargetNum = num;
			scenarioBattleSkillManager.skillFSM.SendTrigger("SelectEnemyFrameButton");
		}
		else if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == num).isDead)
		{
			utageBattleSceneManager.SetPlayerFocusTarget(num);
			MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
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
