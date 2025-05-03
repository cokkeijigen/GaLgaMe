using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSlipDamageText : StateBehaviour
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
		SetSexBattleSlipMessage_HeroineVoice();
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[3].gameObject.SetActive(value: true);
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

	private void SetSexBattleSlipMessage_HeroineVoice()
	{
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(0, 2);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[3].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + "_Piston" + num;
		MasterAudio.PlaySound("Voice_Piston_" + selectSexBattleHeroineId, 1f, null, 0f, null, null);
	}

	private void InvokeMethod()
	{
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[4].GetComponent<Localize>().Term = "sexBattleTarget_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + 0;
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[5].text = sexBattleTurnManager.sexBattleDamageValue.ToString();
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[6].GetComponent<Localize>().Term = "sexBattleAddDamage";
		int num = 0;
		if (sexBattleTurnManager.isCriticalSuccess)
		{
			num = 2;
		}
		Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[num], sexBattleEffectManager.sexBattleEffectSpawnPoint[1]);
		transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleDamageValue.ToString();
		sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_DamageRaw[2].SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[4].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[5].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Slip[6].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
