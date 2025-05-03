using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillSelfFertilizeDamageText : StateBehaviour
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
		SexSkillData selectSexSkillData = sexBattleManager.selectSexSkillData;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(0, 2);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_SelfFertilize.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[0].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 2;
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[1].GetComponent<Localize>().Term = "sexAttack_Piston" + selectSexSkillData.skillID + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[1].gameObject.SetActive(value: true);
		MasterAudio.PlaySound("Voice_HardPiston_" + selectSexBattleHeroineId, 1f, null, 0f, null, null);
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
		_ = sexBattleManager.selectSexSkillData;
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[2].GetComponent<Localize>().Term = "sexBattleTarget_00";
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[3].text = sexBattleTurnManager.sexBattleSelfDamageValue.ToString();
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[4].GetComponent<Localize>().Term = "sexBattleAddDamage";
		Debug.Log("自分へのダメージ：" + sexBattleTurnManager.sexBattleSelfDamageValue);
		Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[0], sexBattleEffectManager.sexBattleEffectSpawnPoint[0]);
		transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleSelfDamageValue.ToString();
		sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[2].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_SelfFertilize[4].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
