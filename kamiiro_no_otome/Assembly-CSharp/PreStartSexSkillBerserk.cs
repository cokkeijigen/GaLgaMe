using Arbor;
using UnityEngine;
using UnityEngine.Playables;

[AddComponentMenu("")]
public class PreStartSexSkillBerserk : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	public PlayableDirector playableDirector;

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
		if (sexBattleTurnManager.isVictoryPiston)
		{
			sexBattleManager.pistonInfoLocText.Term = "pistonInfo_Victory";
		}
		else
		{
			sexBattleManager.pistonInfoLocText.Term = "pistonInfo_Berserk";
		}
		sexBattleManager.pistonInfoWindow.SetActive(value: true);
		playableDirector.Play();
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
