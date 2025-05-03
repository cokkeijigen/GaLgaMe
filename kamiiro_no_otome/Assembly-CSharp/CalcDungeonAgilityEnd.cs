using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonAgilityEnd : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerBattleConditionAccess.ReCalcBuffContinutyTurn();
		PlayerBattleConditionAccess.ReCalcBadStateTurn(isScenairoBattle: false);
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
