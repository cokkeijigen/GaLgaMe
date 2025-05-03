using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcHealSexSkillPower : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

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
		SexSkillData sexSkillData = null;
		int num = 0;
		int num2 = 100;
		sexSkillData = sexBattleManager.selectSexSkillData;
		num = PlayerSexStatusDataManager.playerSexHealPower[0];
		if (PlayerSexStatusDataManager.playerSexBuffCondition[0].Count > 0)
		{
			int sexBattleBuffCondition = PlayerSexBattleConditionAccess.GetSexBattleBuffCondition(PlayerSexStatusDataManager.playerSexBuffCondition[0], "heal");
			num2 += sexBattleBuffCondition;
		}
		float num3 = (float)num * ((float)sexSkillData.healPower / 100f);
		Debug.Log("エデン回復力：" + num + "／スキル回復力：" + sexSkillData.healPower + "／回復バフ：" + num2);
		num3 *= (float)num2 / 100f;
		float num4 = Random.Range(0.9f, 1.1f);
		num3 *= num4;
		sexBattleTurnManager.sexBattleHealValue = Mathf.RoundToInt(num3);
		if (sexSkillData.skillType == SexSkillData.SkillType.heal)
		{
			int num5 = Random.Range(sexSkillData.heroineAffectPower / 2, sexSkillData.heroineAffectPower);
			if (sexSkillData.heroineAffect == SexSkillData.TranceAffect.tranceUp)
			{
				sexBattleTurnManager.sexBattleAddTranceValue = num5;
			}
			else if (PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[1], "trance") == 0)
			{
				sexBattleTurnManager.sexBattleAddTranceValue = Mathf.RoundToInt(num5 * -1);
			}
			else
			{
				sexBattleTurnManager.sexBattleAddTranceValue = 0;
			}
			int num6 = Random.Range(sexSkillData.playerAffectPower / 2, sexSkillData.playerAffectPower);
			if (sexSkillData.playerAffect == SexSkillData.TranceAffect.tranceUp)
			{
				sexBattleTurnManager.sexBattleAddSelfTranceValue = num6;
			}
			else
			{
				sexBattleTurnManager.sexBattleAddSelfTranceValue = num6 * -1;
			}
		}
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
