using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcHeroineSexSkill : StateBehaviour
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
		int num = 0;
		SexSkillData heroineSexSkillData = null;
		sexBattleTurnManager.sexBattleDamageValue = 0;
		sexBattleTurnManager.sexBattleSelfDamageValue = 0;
		sexBattleTurnManager.sexBattleHealValue = 0;
		sexBattleTurnManager.sexBattleAddTranceValue = 0;
		sexBattleTurnManager.sexBattleAddSelfTranceValue = 0;
		sexBattleManager.isHeroineAbsorb = false;
		if (PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[1], "absorb") > 0)
		{
			sexBattleManager.isHeroineAbsorb = true;
			heroineSexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == 1500);
		}
		else
		{
			switch (sexBattleManager.selectSexSkillData.skillType)
			{
			case SexSkillData.SkillType.sexAttack:
			case SexSkillData.SkillType.berserk:
				num = Random.Range(0, 10);
				if (num < 6)
				{
					Debug.Log("ヒロインのカウンター／攻撃系のピストン対応スキル");
					num = Random.Range(0, sexBattleManager.heroineAttackSkillList.Count);
					heroineSexSkillData = sexBattleManager.heroineAttackSkillList[num];
				}
				else
				{
					Debug.Log("ヒロインのカウンター／全てのピストン対応スキル");
					num = Random.Range(0, sexBattleManager.heroinePistonSkillList.Count);
					heroineSexSkillData = sexBattleManager.heroinePistonSkillList[num];
				}
				break;
			case SexSkillData.SkillType.caress:
				Debug.Log("ヒロインのカウンター／愛撫対応スキル");
				num = Random.Range(0, sexBattleManager.heroineCaressSkillList.Count);
				heroineSexSkillData = sexBattleManager.heroineCaressSkillList[num];
				break;
			case SexSkillData.SkillType.heal:
				Debug.Log("ヒロインのカウンター／回復対応スキル");
				num = Random.Range(0, sexBattleManager.heroineHealSkillList.Count);
				heroineSexSkillData = sexBattleManager.heroineHealSkillList[num];
				break;
			}
		}
		sexBattleManager.heroineSexSkillData = heroineSexSkillData;
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
