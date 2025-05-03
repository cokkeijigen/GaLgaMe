using Arbor;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillAfterEcstasyText : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public float waitTime;

	public float waitTime2;

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
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(0, 2);
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[0].GetComponent<Localize>().Term = "sexHeroineEcstasyAfter_" + selectSexBattleHeroineId + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[1].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[2].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_AfterHealRaw.SetActive(value: false);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_AfterEcstasy.SetActive(value: true);
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
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[3].GetComponent<Localize>().Term = "sexBattleHealDamage1";
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[4].text = sexBattleTurnManager.sexBattleHealValue.ToString();
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[5].GetComponent<Localize>().Term = "sexBattleHealDamage2";
		Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[1], sexBattleEffectManager.sexBattleEffectSpawnPoint[0]);
		transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleHealValue.ToString();
		sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_AfterHealRaw.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[4].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_AfterEcstasy[5].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
