using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendPushSexBattleSkillButton : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private ArborFSM sexBattleTurnFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnFSM = GameObject.Find("SexBattle Turn Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = GetComponent<ParameterContainer>();
		int skillID = component.GetInt("skillID");
		sexBattleManager.selectSkillID = skillID;
		SexSkillData selectSexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillID);
		sexBattleManager.selectSexSkillData = selectSexSkillData;
		sexBattleTurnFSM.SendTrigger("PushSkillUseButton");
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
