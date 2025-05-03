using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class EndSexSkillBerserk : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleManager.pistonInfoWindow.SetActive(value: false);
		PlayerSexStatusDataManager.playerSexTrance[0] = 0;
		sexBattleTurnManager.sexBattlePistonCount += sexBattleTurnManager.sexBattleBerserkClickCount;
		if (sexBattleTurnManager.isVictoryPiston)
		{
			GameObject.Find("SexBattle Turn Manager").GetComponent<ArborFSM>().SendTrigger("EndVictoryPiston");
		}
		else
		{
			Invoke("InvokeMethod", 0.2f);
		}
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

	private void InvokeMethod()
	{
		GameObject.Find("SexBattle Turn Manager").GetComponent<ArborFSM>().SendTrigger("EndBerserkPiston");
	}
}
