using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetSkillUseCharacterID : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public int skillMpTypeNum;

	public int skillTypeNum;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		int @int = base.transform.GetComponentInParent<ParameterContainer>().GetInt("characterID");
		scenarioBattleTurnManager.useSkillPartyMemberID = @int;
		scenarioBattleSkillManager.selectSkillMpType = skillMpTypeNum;
		scenarioBattleSkillManager.selectSkillTypeNum = skillTypeNum;
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
