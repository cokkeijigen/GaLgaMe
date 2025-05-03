using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcAttackSexSkillPower : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	public Type type;

	private int pistonSelfAttackDamage;

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
		float num2 = 0f;
		int num3 = 0;
		int num4 = 100;
		float num5 = 100f;
		int num6 = 0;
		switch (type)
		{
		case Type.player:
			sexSkillData = sexBattleManager.selectSexSkillData;
			num = PlayerSexStatusDataManager.playerSexAttack[0];
			num2 = PlayerSexStatusDataManager.playerSexSensitivity[PlayerNonSaveDataManager.selectSexBattleHeroineId];
			num3 = PlayerSexStatusDataManager.playerSexCritical[0];
			if (PlayerSexStatusDataManager.playerSexSubPower[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "enhancement") > 0)
			{
				num4 += 25;
			}
			if (PlayerSexStatusDataManager.playerSexSubPower[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "sniping") > 0)
			{
				num6 += 5;
			}
			switch (sexSkillData.bodyCategory)
			{
			case SexSkillData.BodyCategory.both:
				num5 += (float)PlayerSexStatusDataManager.playerSexPassiveBuffVaginaSensetivity[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				break;
			case SexSkillData.BodyCategory.tits:
			case SexSkillData.BodyCategory.nipple:
				num5 += (float)PlayerSexStatusDataManager.playerSexPassiveBuffTitsSensetivity[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				break;
			case SexSkillData.BodyCategory.clitoris:
				num5 += (float)PlayerSexStatusDataManager.playerSexPassiveBuffClitorisSensetivity[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				break;
			}
			break;
		case Type.heroine:
			sexSkillData = sexBattleManager.heroineSexSkillData;
			num = PlayerSexStatusDataManager.playerSexAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId];
			num2 = PlayerSexStatusDataManager.playerSexSensitivity[0];
			num3 = PlayerSexStatusDataManager.playerSexCritical[PlayerNonSaveDataManager.selectSexBattleHeroineId];
			if (PlayerSexStatusDataManager.playerSexBuffCondition[1].Count > 0)
			{
				int sexBattleBuffCondition = PlayerSexBattleConditionAccess.GetSexBattleBuffCondition(PlayerSexStatusDataManager.playerSexBuffCondition[0], "attack");
				num4 += sexBattleBuffCondition;
			}
			if (PlayerSexStatusDataManager.playerSexBuffCondition[1].Count > 0)
			{
				int sexBattleBuffCondition2 = PlayerSexBattleConditionAccess.GetSexBattleBuffCondition(PlayerSexStatusDataManager.playerSexBuffCondition[0], "critical");
				num6 += sexBattleBuffCondition2;
			}
			if (PlayerSexStatusDataManager.playerSexSubPower[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "enhancement") > 0)
			{
				num5 += 25f;
			}
			if (PlayerSexStatusDataManager.playerSexSubPower[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "desire") > 0)
			{
				num5 += 50f;
			}
			switch (sexSkillData.bodyCategory)
			{
			case SexSkillData.BodyCategory.vagina:
			case SexSkillData.BodyCategory.anal:
			case SexSkillData.BodyCategory.both:
				num4 = ((!sexBattleManager.isAnalSex) ? (num4 + PlayerSexStatusDataManager.playerSexPassiveBuffVaginaAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId]) : (num4 + PlayerSexStatusDataManager.playerSexPassiveBuffAnalAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId]));
				break;
			case SexSkillData.BodyCategory.mouth:
			case SexSkillData.BodyCategory.hand:
				num4 += PlayerSexStatusDataManager.playerSexPassiveBuffVoiceAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				break;
			case SexSkillData.BodyCategory.tits:
				num4 += PlayerSexStatusDataManager.playerSexPassiveBuffTitsAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId];
				break;
			}
			break;
		}
		float num7 = (float)num * ((float)sexSkillData.skillPower / 100f);
		Debug.Log("基礎ダメージ：" + num7 + "／スキル効力：" + sexSkillData.skillPower + "／感度：" + num2 + "／攻撃バフ：" + num4);
		if (sexBattleManager.selectSexSkillData.skillID != 200)
		{
			sexBattleTurnManager.isCriticalSuccess = false;
			num3 += num6;
			if (Random.Range(0, 100) <= num3)
			{
				sexBattleTurnManager.isCriticalSuccess = true;
				num7 *= 1.5f;
			}
		}
		num7 *= (float)num4 / 100f;
		num5 /= 100f;
		num2 = ((type != 0) ? (num2 / 100f) : (1f + num2 / 100f));
		Debug.Log("バフ込みダメージ：" + num7 + "／感度：" + num2 + "／バフ感度：" + num5);
		float num8 = Random.Range(0.9f, 1.1f);
		num7 *= num8;
		if (type == Type.player)
		{
			float num9 = 100f;
			if (PlayerSexStatusDataManager.playerSexBuffCondition[1].Count > 0)
			{
				int sexBattleBuffCondition3 = PlayerSexBattleConditionAccess.GetSexBattleBuffCondition(PlayerSexStatusDataManager.playerSexBuffCondition[0], "attack");
				num9 += (float)sexBattleBuffCondition3;
			}
			num9 = ((!sexBattleManager.isAnalSex) ? (num9 + (float)PlayerSexStatusDataManager.playerSexPassiveBuffVaginaAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId]) : (num9 + (float)PlayerSexStatusDataManager.playerSexPassiveBuffAnalAttack[PlayerNonSaveDataManager.selectSexBattleHeroineId]));
			num9 /= 100f;
			float num10 = (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			pistonSelfAttackDamage = Mathf.RoundToInt(num7 * num10 * num9);
			if (sexBattleTurnManager.isFertilizeRepeatPiston)
			{
				pistonSelfAttackDamage = 999;
			}
		}
		if (sexSkillData.skillType == SexSkillData.SkillType.berserk)
		{
			if (sexBattleTurnManager.sexBattleBerserkClickCount == 0)
			{
				sexBattleTurnManager.sexBattleBerserkClickCount = 1;
			}
			num7 *= (float)sexBattleTurnManager.sexBattleBerserkClickCount;
			Debug.Log("バーサーククリック数：" + sexBattleTurnManager.sexBattleBerserkClickCount);
		}
		sexBattleTurnManager.sexBattleDamageValue = Mathf.RoundToInt(num7 * num2 * num5);
		Debug.Log("与ダメージ：" + sexBattleTurnManager.sexBattleDamageValue);
		int num11 = Random.Range(sexSkillData.heroineAffectPower / 2, sexSkillData.heroineAffectPower);
		if (sexSkillData.heroineAffect == SexSkillData.TranceAffect.tranceUp)
		{
			sexBattleTurnManager.sexBattleAddTranceValue = num11;
		}
		else if (PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[1], "trance") == 0)
		{
			sexBattleTurnManager.sexBattleAddTranceValue = Mathf.RoundToInt(num11 * -1);
		}
		else
		{
			sexBattleTurnManager.sexBattleAddTranceValue = 0;
		}
		int num12 = 0;
		if (sexSkillData.skillType != SexSkillData.SkillType.berserk)
		{
			num12 = Random.Range(sexSkillData.playerAffectPower / 2, sexSkillData.playerAffectPower);
		}
		if (sexSkillData.playerAffect == SexSkillData.TranceAffect.tranceUp)
		{
			if (PlayerSexStatusDataManager.playerSexBuffCondition[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "desire") > 0)
			{
				int num13 = Random.Range(0, 25);
				num12 += num13;
			}
			sexBattleTurnManager.sexBattleAddSelfTranceValue = num12;
		}
		else
		{
			sexBattleTurnManager.sexBattleAddSelfTranceValue = num12 * -1;
		}
		if (type == Type.player && sexSkillData.actionType == SexSkillData.ActionType.piston)
		{
			CalcPistonSelfDamage();
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

	private void CalcPistonSelfDamage()
	{
		switch (sexBattleManager.selectSkillID)
		{
		case 0:
		{
			float num5 = (float)pistonSelfAttackDamage * 0.7f;
			num5 *= (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			sexBattleTurnManager.sexBattleSelfDamageValue = Mathf.RoundToInt(num5);
			break;
		}
		case 1:
		{
			float num4 = (float)pistonSelfAttackDamage * 0.7f;
			num4 *= (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			sexBattleTurnManager.sexBattleSelfDamageValue = Mathf.RoundToInt(num4);
			break;
		}
		case 2:
		{
			float num3 = (float)pistonSelfAttackDamage * 0.7f;
			num3 *= (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			sexBattleTurnManager.sexBattleSelfDamageValue = Mathf.RoundToInt(num3);
			break;
		}
		case 3:
		{
			float num2 = (float)pistonSelfAttackDamage * 0.7f;
			num2 *= (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			sexBattleTurnManager.sexBattleSelfDamageValue = Mathf.RoundToInt(num2);
			break;
		}
		case 200:
		{
			float num = (float)pistonSelfAttackDamage * 0.5f;
			num *= (float)PlayerSexStatusDataManager.playerSexSensitivity[0] / 100f;
			sexBattleTurnManager.sexBattleSelfDamageValue = Mathf.RoundToInt(num);
			break;
		}
		}
		Debug.Log("ピストン時の自分へのダメージ：" + sexBattleTurnManager.sexBattleSelfDamageValue);
	}
}
