using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcSexSlipDamage : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	public StateLink noCrazyLink;

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
		float num = 0f;
		int num2 = 0;
		int num3 = 100;
		int num4 = 0;
		if (PlayerSexStatusDataManager.playerSexSubPower[0].Find((PlayerSexStatusDataManager.MemberSexSubPower data) => data.type == "crazy") != null)
		{
			float num5 = 100f;
			float num6 = PlayerSexStatusDataManager.playerSexAttack[0];
			num = PlayerSexStatusDataManager.playerSexSensitivity[PlayerNonSaveDataManager.selectSexBattleHeroineId];
			num2 = PlayerSexStatusDataManager.playerSexCritical[0];
			if (PlayerSexStatusDataManager.playerSexSubPower[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "enhancement") > 0)
			{
				num3 += 25;
			}
			if (PlayerSexStatusDataManager.playerSexSubPower[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "sniping") > 0)
			{
				num4 += 5;
			}
			num5 += (float)PlayerSexStatusDataManager.playerSexPassiveBuffVaginaSensetivity[PlayerNonSaveDataManager.selectSexBattleHeroineId];
			float num7 = num6 * 0.5f;
			sexBattleTurnManager.isCriticalSuccess = false;
			num2 += num4;
			if (Random.Range(0, 100) <= num2)
			{
				sexBattleTurnManager.isCriticalSuccess = true;
				num7 *= 1.5f;
			}
			num7 *= (float)num3 / 100f;
			num5 /= 100f;
			num = 1f + num / 100f;
			Debug.Log("バフ込みダメージ：" + num7 + "／感度：" + num + "／バフ感度：" + num5);
			float num8 = Random.Range(0.9f, 1.1f);
			num7 *= num8;
			float num9 = 100f;
			if (PlayerSexStatusDataManager.playerSexBuffCondition[1].Count > 0)
			{
				int sexBattleBuffCondition = PlayerSexBattleConditionAccess.GetSexBattleBuffCondition(PlayerSexStatusDataManager.playerSexBuffCondition[0], "attack");
				num9 += (float)sexBattleBuffCondition;
			}
			num9 = ((!sexBattleManager.isAnalSex) ? (num9 + (float)PlayerSexStatusDataManager.playerSexPassiveBuffVaginaAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId]) : (num9 + (float)PlayerSexStatusDataManager.playerSexPassiveBuffAnalAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId]));
			num9 /= 100f;
			float num10 = (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			float num11 = (float)Mathf.RoundToInt(num7 * num10 * num9) * 0.5f;
			num11 *= (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			sexBattleTurnManager.sexBattleSelfDamageValue = Mathf.RoundToInt(num11);
			sexBattleTurnManager.sexBattleDamageValue = Mathf.RoundToInt(num7 * num * num5);
			Debug.Log("与ダメージ：" + sexBattleTurnManager.sexBattleDamageValue);
			SexSkillData sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == 0);
			int num12 = Random.Range(sexSkillData.heroineAffectPower / 2, sexSkillData.heroineAffectPower);
			if (sexSkillData.heroineAffect == SexSkillData.TranceAffect.tranceUp)
			{
				float f = (float)num12 / 2f;
				sexBattleTurnManager.sexBattleAddTranceValue = Mathf.RoundToInt(f);
			}
			else if (PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[1], "trance") == 0)
			{
				_ = (float)num12 / 2f;
				sexBattleTurnManager.sexBattleAddTranceValue = Mathf.RoundToInt(num12 * -1);
			}
			else
			{
				sexBattleTurnManager.sexBattleAddTranceValue = 0;
			}
			int num13 = Random.Range(sexSkillData.playerAffectPower / 2, sexSkillData.playerAffectPower);
			if (sexSkillData.playerAffect == SexSkillData.TranceAffect.tranceUp)
			{
				_ = (float)num12 / 2f;
				sexBattleTurnManager.sexBattleAddSelfTranceValue = Mathf.RoundToInt(num13);
			}
			else
			{
				_ = (float)num12 / 2f;
				sexBattleTurnManager.sexBattleAddSelfTranceValue = Mathf.RoundToInt(num13 * -1);
			}
			Debug.Log("夢中突きである");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("夢中突きではない");
			Transition(noCrazyLink);
		}
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
