using Arbor;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillSelfDamageText : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public float waitTime;

	public float waitTime2;

	public float despawnTime;

	public StateLink caressLink;

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
		if (selectSexSkillData.skillType == SexSkillData.SkillType.sexAttack || selectSexSkillData.skillType == SexSkillData.SkillType.fertilize)
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[0].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 2;
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[1].GetComponent<Localize>().Term = "sexAttack_Piston" + selectSexSkillData.skillID + num;
		}
		else
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[1].GetComponent<Localize>().Term = "sexAttack_Heal";
		}
		sexBattleMessageTextManager.sexBattleMessageGroup_Self[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Self[1].gameObject.SetActive(value: true);
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
		SexSkillData selectSexSkillData = sexBattleManager.selectSexSkillData;
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Self.SetActive(value: true);
		if (selectSexSkillData.actionType == SexSkillData.ActionType.piston)
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[2].GetComponent<Localize>().Term = "sexBattleTarget_00";
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[3].text = sexBattleTurnManager.sexBattleSelfDamageValue.ToString();
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[4].GetComponent<Localize>().Term = "sexBattleAddDamage";
			Debug.Log("自分へのダメージ：" + sexBattleTurnManager.sexBattleSelfDamageValue);
			Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[0], sexBattleEffectManager.sexBattleEffectSpawnPoint[0]);
			transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleSelfDamageValue.ToString();
			sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
		}
		else
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[2].GetComponent<Localize>().Term = "sexBattleHealDamage1";
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[3].text = sexBattleTurnManager.sexBattleHealValue.ToString();
			sexBattleMessageTextManager.sexBattleMessageGroup_Self[4].GetComponent<Localize>().Term = "sexBattleHealDamage2";
			Transform transform2 = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[1], sexBattleEffectManager.sexBattleEffectSpawnPoint[0]);
			transform2.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleHealValue.ToString();
			sexBattleEffectManager.SetEffectDeSpawnReserve(transform2, isSkillPool: false, despawnTime);
		}
		sexBattleMessageTextManager.sexBattleMessageGroup_Self[2].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Self[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Self[4].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		SexSkillData selectSexSkillData = sexBattleManager.selectSexSkillData;
		if (selectSexSkillData.skillType == SexSkillData.SkillType.sexAttack || selectSexSkillData.skillType == SexSkillData.SkillType.fertilize)
		{
			Transition(stateLink);
		}
		else
		{
			Transition(caressLink);
		}
	}
}
