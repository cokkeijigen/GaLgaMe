using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillSlipDamageText : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleMessageTextManager.ResetBattleTextMessage();
		float time = waitTime / (float)sexBattleManager.battleSpeed;
		int num = Random.Range(0, 2);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[0].GetComponent<Localize>().Term = "sexCrazy_" + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[1].GetComponent<Localize>().Term = "sexAttack_Crazy";
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Top.SetActive(value: false);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Slip.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[1].gameObject.SetActive(value: true);
		Invoke("InvokeMethod", time);
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
		Transition(stateLink);
	}
}
