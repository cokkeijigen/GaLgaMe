using Arbor;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillCounterText : StateBehaviour
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
		switch (sexBattleManager.heroineSexSkillData.skillType)
		{
		case SexSkillData.SkillType.sexAttack:
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[5].text = sexBattleTurnManager.sexBattleDamageValue.ToString();
			if (sexBattleTurnManager.isCriticalSuccess)
			{
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[6].GetComponent<Localize>().Term = "sexBattleAddCriticalDamage";
			}
			else
			{
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[6].GetComponent<Localize>().Term = "sexBattleAddDamage";
			}
			sexBattleMessageTextManager.sexBattleMessageGroupGo_DamageRaw[1].SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[4].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[5].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[6].gameObject.SetActive(value: true);
			Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[0], sexBattleEffectManager.sexBattleEffectSpawnPoint[0]);
			transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleDamageValue.ToString();
			sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
			break;
		}
		}
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
