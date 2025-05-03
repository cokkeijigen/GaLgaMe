using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushSkillScrollButton : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ArborFSM arborFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		arborFSM = GameObject.Find("Battle Skill Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = GetComponent<ParameterContainer>();
		int skillID = component.GetInt("skillID");
		scenarioBattleTurnManager.battleUseSkillID = skillID;
		BattleSkillData playerSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
		scenarioBattleTurnManager.playerSkillData = playerSkillData;
		int siblingIndex = base.transform.GetSiblingIndex();
		scenarioBattleSkillManager.scrollContentClickNum = siblingIndex;
		scenarioBattleSkillManager.isScrollContentClick = true;
		arborFSM.SendTrigger("RefreshSkillWindow");
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
