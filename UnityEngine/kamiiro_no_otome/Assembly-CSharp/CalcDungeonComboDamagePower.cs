using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonComboDamagePower : StateBehaviour
{
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

	private int criticalValue;

	public float playerMagnification;

	public float enemyMagnification;

	public StateLink noComboLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		new List<int>();
		bool flag = false;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		string text = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		int num10 = int.Parse(parameterContainer.GetStringList("AgilityQueueList")[0].Substring(1));
		switch (text)
		{
		case "p":
		{
			int num11 = PlayerStatusDataManager.playerPartyMember[num10];
			outputID.SetValue(num11);
			if (num11 != 0 || !PlayerDataManager.isDungeonHeroineFollow)
			{
				break;
			}
			int num12 = PlayerStatusDataManager.characterComboProbability[PlayerDataManager.DungeonHeroineFollowNum];
			int num13 = Random.Range(0, 100);
			Debug.Log($"連携攻撃発生確認：{num13}/{num12}");
			if (num13 > num12)
			{
				break;
			}
			attackValue = PlayerStatusDataManager.characterAttack[num11];
			defenseValue = PlayerStatusDataManager.enemyAllDefense;
			magicAttackValue = PlayerStatusDataManager.characterMagicAttack[num11];
			magicDefenseValue = PlayerStatusDataManager.enemyAllMagicDefense;
			criticalValue = PlayerStatusDataManager.characterCritical[num11];
			num9 = 1f + (float)dungeonMapStatusManager.dungeonBuffAttack / 100f;
			Debug.Log("ダンジョンの攻撃バフ：" + num9);
			attackValue = Mathf.RoundToInt((float)attackValue * num9);
			if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
			{
				int battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "attack");
				float buffPower = GetBuffPower(battleBuffCondition);
				num5 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterAttack[0] * buffPower);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
			{
				int battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "defense");
				float buffPower2 = GetBuffPower(battleBuffCondition2);
				num6 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterDefense[0] * buffPower2);
			}
			if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
			{
				int battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "magicAttack");
				float buffPower3 = GetBuffPower(battleBuffCondition3);
				num7 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterMagicAttack[0] * buffPower3);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
			{
				int battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "magicDefense");
				float buffPower4 = GetBuffPower(battleBuffCondition4);
				num8 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterMagicDefense[0] * buffPower4);
			}
			if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
			{
				PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "critical");
			}
			attackValue = Mathf.FloorToInt(Mathf.Clamp((float)attackValue + num5, 0f, 9999f));
			magicAttackValue = Mathf.FloorToInt(Mathf.Clamp((float)magicAttackValue + num7, 0f, 9999f));
			defenseValue = Mathf.FloorToInt(Mathf.Clamp((float)defenseValue + num6, 0f, 9999f));
			magicDefenseValue = Mathf.FloorToInt(Mathf.Clamp((float)magicDefenseValue + num8, 0f, 9999f));
			num = attackValue - defenseValue;
			num2 = magicAttackValue - magicDefenseValue;
			float num14 = Random.Range(0.8f, 1.2f);
			num = Mathf.Floor(num * num14);
			num = Mathf.Clamp(num, 0f, 9999f);
			Debug.Log("物理ダメージ：" + num);
			num14 = Random.Range(0.9f, 1.1f);
			num2 = Mathf.Floor(num2 * num14);
			num2 = Mathf.Clamp(num2, 0f, 9999f);
			Debug.Log("魔法ダメージ：" + num2);
			float num15 = (num + num2) / 2f;
			outputDamage.SetValue((int)num15);
			if (text == "p")
			{
				float num16 = num15 / (float)PlayerStatusDataManager.enemyAllMaxHp;
				num3 = num16 * playerMagnification;
				num4 = num16 * enemyMagnification / 2.5f;
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					num3 = Mathf.Floor(num3);
					outputSp.SetValue((int)num3);
				}
				num4 = Mathf.Floor(num4);
				outputCharge.SetValue((int)num4);
			}
			flag = true;
			break;
		}
		}
		if (flag)
		{
			Debug.Log("連携攻撃が発動");
			Invoke("InvokeMethod", 0.2f);
		}
		else
		{
			Debug.Log("連携攻撃はなし");
			Transition(noComboLink);
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

	private float GetBuffPower(int num)
	{
		return num / 100 + 1;
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
