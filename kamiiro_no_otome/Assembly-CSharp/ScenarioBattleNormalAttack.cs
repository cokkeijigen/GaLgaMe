using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ScenarioBattleNormalAttack : StateBehaviour
{
	public enum Type
	{
		player,
		enemy,
		support
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public OutputSlotInt outputDamage;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		float num = 0f;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		int num10 = 0;
		int num11 = 0;
		int num12 = 0;
		float num13 = 1f;
		float num14 = 1f;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		int memberId = 0;
		scenarioBattleTurnManager.isNormalWeaklHit = false;
		scenarioBattleTurnManager.isNormalResistHit = false;
		switch (type)
		{
		case Type.player:
		{
			num2 = PlayerStatusDataManager.characterAttack[PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID];
			num3 = PlayerStatusDataManager.enemyDefense[scenarioBattleTurnManager.playerTargetNum];
			num4 = PlayerStatusDataManager.characterMagicAttack[PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID];
			num5 = PlayerStatusDataManager.enemyMagicDefense[scenarioBattleTurnManager.playerTargetNum];
			num6 = PlayerStatusDataManager.characterCritical[PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID];
			int memberNum = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberNum;
			memberId = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID;
			if (memberId == 4)
			{
				flag5 = true;
			}
			if (PlayerBattleConditionManager.playerBuffCondition[memberNum].Count > 0)
			{
				int battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[memberNum], "attack");
				float buffPower5 = GetBuffPower(battleBuffCondition5);
				num7 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterAttack[memberNum] * buffPower5);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum].Count > 0)
			{
				int battleBuffCondition6 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum], "defense");
				float buffPower6 = GetBuffPower(battleBuffCondition6);
				num8 = Mathf.RoundToInt((float)PlayerStatusDataManager.enemyDefense[scenarioBattleTurnManager.playerTargetNum] * buffPower6);
			}
			if (PlayerBattleConditionManager.playerBuffCondition[memberNum].Count > 0)
			{
				int battleBuffCondition7 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[memberNum], "magicAttack");
				float buffPower7 = GetBuffPower(battleBuffCondition7);
				num9 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterMagicAttack[memberNum] * buffPower7);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum].Count > 0)
			{
				int battleBuffCondition8 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum], "magicDefense");
				float buffPower8 = GetBuffPower(battleBuffCondition8);
				num10 = Mathf.RoundToInt((float)PlayerStatusDataManager.enemyMagicDefense[scenarioBattleTurnManager.playerTargetNum] * buffPower8);
			}
			if (PlayerBattleConditionManager.playerBuffCondition[memberNum].Count > 0)
			{
				num11 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[memberNum], "critical");
			}
			int id2 = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
			BattleEnemyData battleEnemyData2 = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id2);
			CharacterStatusData characterStatusData2 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == memberId);
			Debug.Log("味方の攻撃／対象の敵のID：" + id2);
			if (id2 == 950 || id2 == 951 || id2 == 1012)
			{
				flag4 = true;
			}
			if (!string.IsNullOrEmpty(characterStatusData2.normalAttackElement))
			{
				if (battleEnemyData2.weakElementList.Contains(characterStatusData2.normalAttackElement))
				{
					flag = true;
					scenarioBattleTurnManager.isNormalWeaklHit = true;
				}
				if (battleEnemyData2.resistElementList.Contains(characterStatusData2.normalAttackElement))
				{
					flag2 = true;
					scenarioBattleTurnManager.isNormalResistHit = true;
				}
			}
			break;
		}
		case Type.enemy:
		{
			int targetID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
			int index = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum);
			num2 = PlayerStatusDataManager.enemyAttack[PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum];
			num3 = PlayerStatusDataManager.characterDefense[targetID];
			num4 = PlayerStatusDataManager.enemyMagicAttack[PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum];
			num5 = PlayerStatusDataManager.characterMagicDefense[targetID];
			num6 = PlayerStatusDataManager.enemyCritical[PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum];
			int memberNum = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum;
			Debug.Log("敵の攻撃／対象の味方のID：" + targetID);
			if (targetID == 4)
			{
				flag4 = true;
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[memberNum].Count > 0)
			{
				int battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "attack");
				float buffPower = GetBuffPower(battleBuffCondition);
				num7 = Mathf.RoundToInt((float)PlayerStatusDataManager.enemyAttack[memberNum] * buffPower);
			}
			if (PlayerBattleConditionManager.playerBuffCondition[index].Count > 0)
			{
				int battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index], "defense");
				float buffPower2 = GetBuffPower(battleBuffCondition2);
				num8 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterDefense[targetID] * buffPower2);
				Debug.Log("味方の物防バフ：" + buffPower2 + "／キャラID：" + targetID + "／キャラIndex：" + index);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[memberNum].Count > 0)
			{
				int battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "magicAttack");
				float buffPower3 = GetBuffPower(battleBuffCondition3);
				num9 = Mathf.RoundToInt((float)PlayerStatusDataManager.enemyMagicAttack[memberNum] * buffPower3);
			}
			if (PlayerBattleConditionManager.playerBuffCondition[index].Count > 0)
			{
				int battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index], "magicDefense");
				float buffPower4 = GetBuffPower(battleBuffCondition4);
				num10 = Mathf.RoundToInt((float)PlayerStatusDataManager.characterMagicDefense[targetID] * buffPower4);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[memberNum].Count > 0)
			{
				num11 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "critical");
			}
			num12 = PlayerEquipDataManager.equipFactorCriticalResist[targetID];
			int num15 = 0;
			if (PlayerBattleConditionManager.playerBuffCondition[scenarioBattleTurnManager.enemyTargetNum].Count > 0)
			{
				num15 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[scenarioBattleTurnManager.enemyTargetNum], "parry");
			}
			int num16 = PlayerEquipDataManager.equipFactorParry[targetID] + num15;
			int num17 = Random.Range(1, 100);
			Debug.Log("受け流しバフ：" + num15 + "／受け流しファクター：" + num16 + "／ランダム：" + num17);
			if (num16 >= num17)
			{
				flag3 = true;
				Debug.Log("受け流し成功");
			}
			int id = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.playerTargetNum].memberID;
			int index2 = GameDataManager.instance.battleEnemyDataBase.enemyDataList.FindIndex((BattleEnemyData data) => data.enemyID == id);
			BattleEnemyData battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList[index2];
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == targetID);
			if (!string.IsNullOrEmpty(battleEnemyData.normalAttackElement))
			{
				if (characterStatusData.weakElementsList.Contains(battleEnemyData.normalAttackElement))
				{
					flag = true;
					scenarioBattleTurnManager.isNormalWeaklHit = true;
				}
				if (characterStatusData.resistElementsList.Contains(battleEnemyData.normalAttackElement))
				{
					flag2 = true;
					scenarioBattleTurnManager.isNormalResistHit = true;
				}
			}
			break;
		}
		case Type.support:
		{
			int supportAttackMemberId = utageBattleSceneManager.supportAttackMemberId;
			num2 = PlayerStatusDataManager.characterAttack[supportAttackMemberId];
			num3 = PlayerStatusDataManager.enemyDefense[scenarioBattleTurnManager.playerTargetNum];
			num4 = PlayerStatusDataManager.characterMagicAttack[supportAttackMemberId];
			num5 = PlayerStatusDataManager.enemyMagicDefense[scenarioBattleTurnManager.playerTargetNum];
			num6 = PlayerStatusDataManager.characterCritical[supportAttackMemberId];
			break;
		}
		}
		Debug.Log("攻撃力：" + num2 + "／バフ物攻：" + num7 + "／魔法攻撃力：" + num4 + "／バフ魔攻：" + num9);
		Debug.Log("防御力：" + num3 + "／バフ物防：" + num8 + "／魔法防御力：" + num5 + "／バフ魔防：" + num10);
		num2 += num7;
		num3 += num8;
		num4 += num9;
		num5 += num10;
		num6 = Mathf.Clamp(num6 + num11 - num12, 0, 100);
		Debug.Log("対象はレヴィor再戦リヴィアサンである：" + flag4);
		int num18 = Random.Range(1, 100);
		if (num6 >= num18 && num3 < 10000 && !flag4)
		{
			float num19 = Random.Range(0.9f, 1.1f);
			num = Mathf.Round((float)(num2 * 2) * num19);
			num = Mathf.Clamp(num, 0f, 9999f);
			scenarioBattleTurnManager.isCriticalHit = true;
			Debug.Log("クリティカルダメージ：" + num + "／クリティカル率：" + num6);
		}
		else
		{
			float num20 = Random.Range(0.9f, 1.1f);
			int num21 = Mathf.Clamp(num2 - num3, 0, 9999);
			int num22 = Mathf.Clamp(num4 - num5, 0, 9999);
			int num23 = num21;
			if (flag5)
			{
				num23 = num21 + num22;
				Debug.Log("攻撃側はレヴィである");
			}
			num = Mathf.Round((float)num23 * num20);
			num = Mathf.Clamp(num, 0f, 9999f);
			scenarioBattleTurnManager.isCriticalHit = false;
			Debug.Log("通常ダメージ：" + num + "／物理：" + num21 + "／魔法：" + num22 + "／クリティカル率：" + num6);
		}
		num13 = ((!flag) ? 1f : ((PlayerEquipDataManager.accessoryWeakPointDamageUp[4] <= 0) ? 1.5f : 2f));
		num14 = ((!flag2) ? 1f : 0.5f);
		num = Mathf.Clamp(Mathf.RoundToInt(num * num13 * num14), 0, 9999);
		Debug.Log("弱点：" + flag + "／耐性：" + flag2);
		scenarioBattleTurnManager.isParrySuccess = false;
		if (flag3)
		{
			num /= 2f;
			scenarioBattleTurnManager.isParrySuccess = true;
		}
		if (scenarioBattleTurnManager.factorEffectSuccessList[3])
		{
			num = 99999f;
		}
		else
		{
			num = Mathf.RoundToInt(num);
			num = Mathf.Clamp(num, 0f, 99999f);
		}
		outputDamage.SetValue((int)num);
		if (PlayerNonSaveDataManager.isEnemyHpZeroAttack && type == Type.player)
		{
			int value = PlayerStatusDataManager.enemyHp[scenarioBattleTurnManager.playerTargetNum];
			outputDamage.SetValue(value);
			PlayerNonSaveDataManager.isEnemyHpZeroAttack = false;
			Debug.Log("デバッグ中の時のダメージ処理／ダメージ量：" + value);
		}
		switch (type)
		{
		case Type.enemy:
			scenarioBattleTurnManager.enemyNormalAttackDamageList[scenarioBattleTurnManager.enemyTargetNum] += (int)num;
			Debug.Log("ダメージ量を代入：" + (int)num);
			break;
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
		float result = 0f;
		if (num != 0)
		{
			result = (float)num / 100f;
		}
		return result;
	}
}
