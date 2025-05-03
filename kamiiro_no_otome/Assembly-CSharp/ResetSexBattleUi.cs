using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetSexBattleUi : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleManager.skillButtonGo.SetActive(value: false);
		sexBattleManager.menuButtonGo.SetActive(value: false);
		sexBattleManager.cumShotToggleGo.SetActive(value: false);
		sexBattleManager.skillWindowGo.SetActive(value: false);
		sexBattleManager.blackImageGo.SetActive(value: false);
		sexBattleManager.skillButtonGroupGo.SetActive(value: false);
		sexBattleManager.skillInfoWindowGo.SetActive(value: false);
		sexBattleManager.commandInfoFrameGo.SetActive(value: false);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Ecstasy.SetActive(value: false);
		sexBattleTurnManager.isFertilizeRepeatPiston = false;
		sexBattleMessageTextManager.ResetBattleTextMessage();
		PlayerSexBattleConditionAccess.SetPlayerUseSexSkillRecharge(0, sexBattleManager.selectSkillID);
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
