using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GetBattleSkillData : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		int skillID = scenarioBattleTurnManager.battleUseSkillID;
		BattleSkillData playerSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
		scenarioBattleTurnManager.playerSkillData = playerSkillData;
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
