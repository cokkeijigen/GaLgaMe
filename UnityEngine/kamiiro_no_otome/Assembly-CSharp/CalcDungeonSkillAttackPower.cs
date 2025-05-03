using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonSkillAttackPower : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private ParameterContainer parameterContainer;

	private BattleSkillData battleSkillData;

	public OutputSlotInt outputDamage;

	public OutputSlotInt outputSp;

	public OutputSlotInt outputCharge;

	public float playerMagnification;

	public float enemyMagnification;

	private int weakEnemyCount;

	private List<bool> weakElementEnemyList = new List<bool>();

	private int resistEnemyCount;

	private List<bool> resistElementEnemyList = new List<bool>();

	public StateLink attackLink;

	public StateLink deBuffLink;

	public StateLink badStatusLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		battleSkillData = dungeonBattleManager.battleSkillData;
		if (battleSkillData.skillType != BattleSkillData.SkillType.deBuff)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			int num5 = 0;
			float num6 = 0f;
			float num7 = 0.5f;
			float num8 = 0f;
			float num9 = 0.5f;
			float num10 = 0f;
			parameterContainer.GetInt("useSkillCharacterID");
			int @int = parameterContainer.GetInt("useSkillCharacterNum");
			dungeonBattleManager.isAttackWeakHit = false;
			dungeonBattleManager.isAttackResistHit = false;
			weakElementEnemyList.Clear();
			resistElementEnemyList.Clear();
			if (parameterContainer.GetBool("isPlayerSkill"))
			{
				switch (battleSkillData.skillType)
				{
				case BattleSkillData.SkillType.attack:
					num3 = PlayerStatusDataManager.playerAllAttack;
					num6 = 1f + (float)dungeonMapStatusManager.dungeonBuffAttack / 100f;
					num3 *= num6;
					num4 = PlayerStatusDataManager.enemyAllDefense;
					if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
					{
						num5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "defense");
					}
					break;
				case BattleSkillData.SkillType.magicAttack:
					num3 = PlayerStatusDataManager.playerAllMagicAttack;
					num6 = 1f + (float)dungeonMapStatusManager.dungeonBuffAttack / 100f;
					num3 *= num6;
					num4 = PlayerStatusDataManager.enemyAllMagicDefense;
					if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
					{
						num5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "magicDefense");
					}
					break;
				case BattleSkillData.SkillType.chargeAttack:
					num3 = PlayerStatusDataManager.playerChargeAttack + PlayerStatusDataManager.playerChargeMagicAttack;
					num6 = 1f + (float)dungeonMapStatusManager.dungeonBuffAttack / 100f;
					num3 *= num6;
					num4 = PlayerStatusDataManager.enemyAllDefense + PlayerStatusDataManager.enemyAllMagicDefense;
					if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
					{
						num5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "defense");
						num5 += PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "magicDefense");
					}
					break;
				}
				CheckWeakElement();
				CheckResistElement();
			}
			else
			{
				switch (battleSkillData.skillType)
				{
				case BattleSkillData.SkillType.attack:
					num3 = PlayerStatusDataManager.enemyAttack[@int];
					num4 = PlayerStatusDataManager.playerAllDefense;
					if (PlayerDataManager.DungeonHeroineFollowNum == 4)
					{
						num3 /= 2f;
						num4 = PlayerStatusDataManager.characterDefense[0] / 2;
					}
					num6 = 1f + (float)dungeonMapStatusManager.dungeonBuffDefense / 100f;
					num4 *= num6;
					if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
					{
						num5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "defense");
					}
					break;
				case BattleSkillData.SkillType.magicAttack:
					num3 = PlayerStatusDataManager.enemyMagicAttack[@int];
					num4 = PlayerStatusDataManager.playerAllMagicDefense;
					num6 = 1f + (float)dungeonMapStatusManager.dungeonBuffDefense / 100f;
					num4 *= num6;
					if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
					{
						num5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "magicDefense");
					}
					break;
				case BattleSkillData.SkillType.mixAttack:
					num3 = PlayerStatusDataManager.enemyAttack[@int];
					num4 = PlayerStatusDataManager.playerAllDefense;
					if (PlayerDataManager.DungeonHeroineFollowNum == 4)
					{
						num3 /= 2f;
						num4 = PlayerStatusDataManager.characterDefense[0] / 2;
					}
					num6 = 1f + (float)dungeonMapStatusManager.dungeonBuffDefense / 100f;
					num4 *= num6;
					if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
					{
						num5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "defense");
					}
					num3 += (float)PlayerStatusDataManager.enemyMagicAttack[@int];
					num4 += (float)PlayerStatusDataManager.playerAllMagicDefense;
					break;
				}
			}
			if (parameterContainer.GetBool("isPlayerSkill"))
			{
				float num11 = num7 * (float)weakEnemyCount;
				num8 = 1f + num11;
			}
			else
			{
				num8 = 1f;
			}
			if (parameterContainer.GetBool("isPlayerSkill"))
			{
				float num12 = num9 * (float)(resistEnemyCount / PlayerStatusDataManager.enemyMember.Length);
				num10 = 1f - num12;
			}
			else
			{
				num10 = 1f;
			}
			float num13 = Random.Range(0.9f, 1.1f);
			int num14 = Mathf.RoundToInt((float)Mathf.RoundToInt(num3 * num8 * num10 * num13) * ((float)battleSkillData.skillPower / 100f));
			int num15 = Mathf.RoundToInt(num4 + (float)num5);
			int value = Mathf.Clamp(num14 - num15, 0, 99999);
			value = Mathf.Clamp(value, 0, 99999);
			outputDamage.SetValue(value);
			Debug.Log("スキル威力：" + battleSkillData.skillPower + "／スキルダメージ：" + value + "／弱点倍率：" + num8 + "耐性倍率：" + num10);
			if (parameterContainer.GetBool("isPlayerSkill"))
			{
				float num16 = value / PlayerStatusDataManager.enemyAllMaxHp;
				num = num16 * playerMagnification;
				num2 = num16 * enemyMagnification / 2.5f;
				num2 = Mathf.Round(num2);
				num2 = Mathf.Clamp(num2, 0f, 99999f);
				outputCharge.SetValue((int)num2);
			}
			else
			{
				float num17 = value / PlayerStatusDataManager.playerAllMaxHp;
				num = num17 * playerMagnification / 2.5f;
				num2 = num17 * enemyMagnification;
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					num = Mathf.Round(num / 3f);
					outputSp.SetValue((int)num);
				}
				num2 = Mathf.Round(num2);
				num2 = Mathf.Clamp(num2, 0f, 99999f);
				outputCharge.SetValue((int)num2);
			}
			Transition(attackLink);
		}
		else if (battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
		{
			Transition(deBuffLink);
		}
		else
		{
			Transition(badStatusLink);
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

	private void CheckWeakElement()
	{
		int i;
		for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			if (GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == PlayerStatusDataManager.enemyMember[i]).weakElementList.Contains(battleSkillData.skillElement))
			{
				weakElementEnemyList.Add(item: true);
				dungeonBattleManager.isAttackWeakHit = true;
				Debug.Log("スキル／敵の中に弱点属性のものがいる");
			}
			else
			{
				weakElementEnemyList.Add(item: false);
			}
		}
		IEnumerable<bool> source = weakElementEnemyList.Where((bool data) => data);
		weakEnemyCount = source.Count();
		Debug.Log("スキル攻撃／弱点属性確認終了");
	}

	private void CheckResistElement()
	{
		int i;
		for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			if (GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == PlayerStatusDataManager.enemyMember[i]).resistElementList.Contains(battleSkillData.skillElement))
			{
				resistElementEnemyList.Add(item: true);
				dungeonBattleManager.isAttackResistHit = true;
				Debug.Log("スキル／敵の中に耐性属性のものがいる");
			}
			else
			{
				resistElementEnemyList.Add(item: false);
			}
		}
		IEnumerable<bool> source = resistElementEnemyList.Where((bool data) => data);
		resistEnemyCount = source.Count();
		Debug.Log("スキル攻撃／耐性属性確認終了");
	}
}
