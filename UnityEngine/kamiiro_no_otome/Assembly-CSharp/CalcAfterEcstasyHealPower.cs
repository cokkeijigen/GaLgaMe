using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CalcAfterEcstasyHealPower : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public float despawnTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		RectTransform rectTransform = sexBattleManager.effectPrefabParentDictionary["vagina"];
		MasterAudio.PlaySound("SexBattle_Heal", 1f, null, 0f, null, null);
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "sexHeal1").effectPrefabGo;
		Transform transform = PoolManager.Pools["sexSkillPool"].Spawn(effectPrefabGo, rectTransform.transform);
		sexBattleEffectManager.SetEffectDeSpawnReserve(transform.transform, isSkillPool: true, despawnTime);
		int num = 50;
		int num2 = 0;
		int num3 = 100;
		num2 = PlayerSexStatusDataManager.playerSexHealPower[0];
		if (PlayerSexStatusDataManager.playerSexBuffCondition[0].Count > 0)
		{
			int sexBattleBuffCondition = PlayerSexBattleConditionAccess.GetSexBattleBuffCondition(PlayerSexStatusDataManager.playerSexBuffCondition[0], "heal");
			num3 += sexBattleBuffCondition;
		}
		float num4 = (float)num2 * ((float)num / 100f);
		Debug.Log("エデン回復力：" + num2 + "／スキル回復力：" + num + "／回復バフ：" + num3);
		num4 *= (float)num3 / 100f;
		float num5 = Random.Range(0.9f, 1.1f);
		num4 *= num5;
		sexBattleTurnManager.sexBattleHealValue = Mathf.RoundToInt(num4);
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
