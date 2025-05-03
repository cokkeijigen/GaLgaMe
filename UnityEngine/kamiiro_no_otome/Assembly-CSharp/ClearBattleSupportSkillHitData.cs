using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ClearBattleSupportSkillHitData : StateBehaviour
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
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		scenarioBattleTurnManager.skillAttackHitDataSubList.Clear();
		Debug.Log("スキルの命中リストをクリア");
		scenarioBattleTurnManager.battleUseSkillID = 0;
		scenarioBattleTurnManager.isAllTargetSkill = false;
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
