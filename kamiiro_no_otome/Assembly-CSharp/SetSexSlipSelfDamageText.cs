using Arbor;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSlipSelfDamageText : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public float waitTime;

	public float despawnTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)sexBattleManager.battleSpeed;
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[7].GetComponent<Localize>().Term = "sexBattleTarget_00";
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[8].text = sexBattleTurnManager.sexBattleSelfDamageValue.ToString();
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[9].GetComponent<Localize>().Term = "sexBattleAddDamage";
		Debug.Log("自分へのスリップダメージ：" + sexBattleTurnManager.sexBattleSelfDamageValue);
		Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[0], sexBattleEffectManager.sexBattleEffectSpawnPoint[0]);
		transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleSelfDamageValue.ToString();
		sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_DamageRaw[3].SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[7].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[8].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[9].gameObject.SetActive(value: true);
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
