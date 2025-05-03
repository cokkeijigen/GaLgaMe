using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushItemScrollButton : StateBehaviour
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
		arborFSM = GameObject.Find("Battle Item Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = GetComponent<ParameterContainer>();
		int @int = component.GetInt("itemID");
		scenarioBattleTurnManager.battleUseItemID = @int;
		scenarioBattleTurnManager.battleUseItemCategory = component.GetString("category");
		int siblingIndex = base.transform.GetSiblingIndex();
		scenarioBattleSkillManager.scrollContentClickNum = siblingIndex;
		scenarioBattleSkillManager.isScrollContentClick = true;
		arborFSM.SendTrigger("RefreshItemWindow");
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
