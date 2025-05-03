using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CloseBattleBuffWindow : StateBehaviour
{
	private BattleBuffInfoManager battleBuffInfoManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		battleBuffInfoManager = GameObject.Find("Battle Buff Info Manager").GetComponent<BattleBuffInfoManager>();
	}

	public override void OnStateBegin()
	{
		battleBuffInfoManager.CloseBuffInfoWindow();
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
