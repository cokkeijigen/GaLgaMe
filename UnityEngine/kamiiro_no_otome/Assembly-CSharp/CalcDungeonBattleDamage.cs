using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonBattleDamage : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private ParameterContainer parameterContainer;

	public OutputSlotInt outputDamage;

	public OutputSlotInt outputSp;

	public OutputSlotInt outputCharge;

	public OutputSlotInt outputID;

	private int attackValue;

	private int defenseValue;

	private int magicAttackValue;

	private int magicDefenseValue;

	public float playerMagnification;

	public float enemyMagnification;

	private int weakEnemyCount;

	private List<bool> weakElementEnemyList = new List<bool>();

	private int resistEnemyCount;

	private List<bool> resistElementEnemyList = new List<bool>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		int num9 = 0;
		bool flag = false;
		float num10 = 0f;
		float num11 = 0f;
		bool flag2 = false;
		float num12 = 0.5f;
		float num13 = 0.5f;
		dungeonBattleManager.isAttackWeakHit = false;
		dungeonBattleManager.isAttackResistHit = false;
		string text = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		int num14 = int.Parse(parameterContainer.GetStringList("AgilityQueueList")[0].Substring(1));
		if (!(text == "p"))
		{
			if (text == "e")
			{
				outputID.SetValue(num14);
				attackValue = PlayerStatusDataManager.enemyAttack[num14];
				defenseValue = PlayerStatusDataManager.playerAllDefense;
				magicAttackValue = PlayerStatusDataManager.enemyMagicAttack[num14];
				magicDefenseValue = PlayerStatusDataManager.playerAllMagicDefense;
				if (PlayerDataManager.DungeonHeroineFollowNum == 4)
				{
					attackValue /= 2;
					defenseValue = PlayerStatusDataManager.characterDefense[0] / 2;
				}
				for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					num9 += PlayerEquipDataManager.equipFactorParry[PlayerStatusDataManager.playerPartyMember[i]];
				}
				int num15 = Random.Range(1, 100);
				if (num9 >= num15)
				{
					flag = true;
					Debug.Log("受け流し成功");
				}
				num11 = 1f + (float)dungeonMapStatusManager.dungeonBuffDefense / 100f;
				Debug.Log("ダンジョンの防御バフ：" + num11);
				defenseValue = Mathf.FloorToInt((float)defenseValue * num11);
				if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
				{
					int battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "attack");
					float buffPower = GetBuffPower(battleBuffCondition);
					num5 = Mathf.RoundToInt((float)PlayerStatusDataManager.enemyAttack[0] * buffPower);
				}
				if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
				{
					int battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "defense");
					float buffPower2 = GetBuffPower(battleBuffCondition2);
					num6 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterDefense[0] * buffPower2);
				}
				if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
				{
					int battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "magicAttack");
					float buffPower3 = GetBuffPower(battleBuffCondition3);
					num7 = Mathf.RoundToInt((float)PlayerStatusDataManager.enemyMagicAttack[0] * buffPower3);
				}
				if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
				{
					int battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "magicDefense");
					float buffPower4 = GetBuffPower(battleBuffCondition4);
					num8 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterMagicDefense[0] * buffPower4);
				}
			}
		}
		else
		{
			int num16 = PlayerStatusDataManager.playerPartyMember[num14];
			outputID.SetValue(num16);
			attackValue = PlayerStatusDataManager.characterAttack[num16];
			defenseValue = PlayerStatusDataManager.enemyAllDefense;
			magicAttackValue = PlayerStatusDataManager.characterMagicAttack[num16];
			magicDefenseValue = PlayerStatusDataManager.enemyAllMagicDefense;
			num10 = 1f + (float)dungeonMapStatusManager.dungeonBuffAttack / 100f;
			Debug.Log("ダンジョンの攻撃バフ：" + num10);
			attackValue = Mathf.RoundToInt((float)attackValue * num10);
			if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
			{
				int battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "attack");
				float buffPower5 = GetBuffPower(battleBuffCondition5);
				num5 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterAttack[0] * buffPower5);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
			{
				int battleBuffCondition6 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "defense");
				float buffPower6 = GetBuffPower(battleBuffCondition6);
				num6 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterDefense[0] * buffPower6);
			}
			if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
			{
				int battleBuffCondition7 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "magicAttack");
				float buffPower7 = GetBuffPower(battleBuffCondition7);
				num7 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterMagicAttack[0] * buffPower7);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
			{
				int battleBuffCondition8 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "magicDefense");
				float buffPower8 = GetBuffPower(battleBuffCondition8);
				num8 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterMagicDefense[0] * buffPower8);
			}
			if (num16 == 4)
			{
				flag2 = true;
				CheckWeakElement();
				CheckResistElement();
			}
		}
		attackValue = Mathf.FloorToInt(Mathf.Clamp((float)attackValue + num5, 0f, 9999f));
		magicAttackValue = Mathf.FloorToInt(Mathf.Clamp((float)magicAttackValue + num7, 0f, 9999f));
		defenseValue = Mathf.FloorToInt(Mathf.Clamp((float)defenseValue + num6, 0f, 9999f));
		magicDefenseValue = Mathf.FloorToInt(Mathf.Clamp((float)magicDefenseValue + num8, 0f, 9999f));
		Debug.Log("攻撃力：" + attackValue + "／魔法攻撃力：" + magicAttackValue + "／防御力：" + defenseValue + "／魔法防御力：" + magicDefenseValue);
		if (flag2)
		{
			float num17 = num12 * (float)weakEnemyCount;
		}
		if (flag2)
		{
			float num18 = num13 * (float)(resistEnemyCount / PlayerStatusDataManager.enemyMember.Length);
		}
		if (dungeonBattleManager.isCriticalAttack)
		{
			float num19 = Random.Range(0.8f, 1.2f);
			num = (float)(attackValue * 2) * num19;
			num2 = (float)(magicAttackValue * 2) * num19;
		}
		else
		{
			num = attackValue - defenseValue;
			num2 = magicAttackValue - magicDefenseValue;
		}
		float num20 = Random.Range(0.8f, 1.2f);
		num = Mathf.Floor(num * num20);
		num = Mathf.Clamp(num, 0f, 9999f);
		num20 = Random.Range(0.9f, 1.1f);
		num2 = Mathf.Floor(num2 * num20 * num12 * num13);
		num2 = Mathf.Clamp(num2, 0f, 9999f);
		float num21 = num + num2;
		Debug.Log("物理ダメージ：" + num + "／魔法ダメージ：" + num2);
		if (flag)
		{
			num21 /= 2f;
		}
		if (parameterContainer.GetBoolList("factorEffectSuccessList")[3])
		{
			num21 = 99999f;
		}
		parameterContainer.SetInt("enemyAttackDamage", (int)num21);
		outputDamage.SetValue((int)num21);
		float num22 = 0f;
		float num23 = 0f;
		for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
		{
			num22 += (float)PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[j]];
		}
		int k;
		for (k = 0; k < PlayerStatusDataManager.enemyMember.Length; k++)
		{
			int enemyLV = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == PlayerStatusDataManager.enemyMember[k]).enemyLV;
			num23 += (float)enemyLV;
		}
		num22 /= (float)PlayerStatusDataManager.playerPartyMember.Length;
		num23 /= (float)PlayerStatusDataManager.enemyMember.Length;
		if (!(text == "p"))
		{
			if (text == "e")
			{
				float num24 = num21 / (float)PlayerStatusDataManager.playerAllMaxHp;
				num3 = num24 * playerMagnification / 1.5f;
				num4 = num24 * enemyMagnification;
				Debug.Log("エネミー側チャージ率：" + num4);
				if (num23 > num22)
				{
					float num25 = Mathf.Clamp(num23 / num22, 0f, 2f);
					num4 *= num25;
					Debug.Log("エネミー側チャージ率増加：" + num4);
				}
				switch (PlayerStatusDataManager.enemyMember.Length)
				{
				case 1:
					if (dungeonMapManager.isBossRouteSelect)
					{
						num4 *= 1.3f;
					}
					break;
				case 2:
					num4 *= 1.2f;
					break;
				case 3:
					num4 *= 1.3f;
					break;
				case 4:
					num4 *= 1.5f;
					break;
				}
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					num3 = Mathf.Floor(num3);
					outputSp.SetValue((int)num3);
				}
				num4 = Mathf.Floor(num4);
				outputCharge.SetValue((int)num4);
			}
		}
		else
		{
			float num26 = num21 / (float)PlayerStatusDataManager.enemyAllMaxHp;
			num3 = num26 * playerMagnification;
			num4 = num26 * enemyMagnification / 2.5f;
			Debug.Log("プレイヤー側チャージ率：" + num3);
			if (num22 > num23)
			{
				float num27 = Mathf.Clamp(num22 / num23, 0f, 1.5f);
				num3 *= num27;
				Debug.Log("プレイヤー側チャージ率増加：" + num3);
			}
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				num3 = Mathf.Floor(num3);
				outputSp.SetValue((int)num3);
			}
			num4 = Mathf.Floor(num4);
			outputCharge.SetValue((int)num4);
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

	private float GetBuffPower(int num)
	{
		return num / 100 + 1;
	}

	private void CheckWeakElement()
	{
		int i;
		for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			if (GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == PlayerStatusDataManager.enemyMember[i]).weakElementList.Contains("水"))
			{
				weakElementEnemyList.Add(item: true);
				dungeonBattleManager.isAttackWeakHit = true;
				Debug.Log("敵の中に弱点属性のものがいる");
			}
			else
			{
				weakElementEnemyList.Add(item: false);
			}
		}
		IEnumerable<bool> source = weakElementEnemyList.Where((bool data) => data);
		weakEnemyCount = source.Count();
		Debug.Log("レヴィ攻撃／弱点属性確認終了");
	}

	private void CheckResistElement()
	{
		int i;
		for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
		{
			if (GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == PlayerStatusDataManager.enemyMember[i]).resistElementList.Contains("水"))
			{
				resistElementEnemyList.Add(item: true);
				dungeonBattleManager.isAttackResistHit = true;
				Debug.Log("敵の中に耐性属性のものがいる");
			}
			else
			{
				resistElementEnemyList.Add(item: false);
			}
		}
		IEnumerable<bool> source = resistElementEnemyList.Where((bool data) => data);
		resistEnemyCount = source.Count();
		Debug.Log("レヴィ攻撃／耐性属性確認終了");
	}
}
