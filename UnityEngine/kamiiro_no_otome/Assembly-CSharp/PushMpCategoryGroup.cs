using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushMpCategoryGroup : StateBehaviour
{
	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public int skillMpType;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleSkillManager.selectSkillMpType = skillMpType;
		scenarioBattleSkillManager.skillFSM.SendTrigger("OpenSkillWindow");
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
