using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushCharacterFrameButton : StateBehaviour
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
		int @int = GetComponent<ParameterContainer>().GetInt("partyMemberNum");
		scenarioBattleTurnManager.playerTargetNum = @int;
		Debug.Log("選択キャラのNumは：" + @int);
		if (scenarioBattleSkillManager.isUseSkill)
		{
			scenarioBattleSkillManager.skillFSM.SendTrigger("SelectCharacterFrameButton");
		}
		else
		{
			scenarioBattleSkillManager.itemFSM.SendTrigger("SelectCharacterFrameButton");
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
