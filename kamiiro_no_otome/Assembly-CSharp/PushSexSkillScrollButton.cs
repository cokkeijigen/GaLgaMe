using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushSexSkillScrollButton : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private ArborFSM skillArborFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		skillArborFSM = GameObject.Find("SexBattle Skill Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = GetComponent<ParameterContainer>();
		int skillID = component.GetInt("skillID");
		sexBattleManager.selectSkillID = skillID;
		SexSkillData selectSexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillID);
		sexBattleManager.selectSexSkillData = selectSexSkillData;
		int siblingIndex = base.transform.GetSiblingIndex();
		sexBattleManager.selectSkillScrollIndex = siblingIndex;
		skillArborFSM.SendTrigger("RefreshSkillWindow");
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
