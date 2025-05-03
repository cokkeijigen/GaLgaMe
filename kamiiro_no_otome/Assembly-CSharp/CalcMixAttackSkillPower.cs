using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcMixAttackSkillPower : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private BattleSkillData battleSkillData;

	private int characterId;

	private int characterNum;

	private List<float> damageValueList = new List<float>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		int num = 0;
		int num2 = 0;
		float num3 = 1f;
		float num4 = 1f;
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<bool> list3 = new List<bool>();
		List<bool> list4 = new List<bool>();
		BattleEnemyData battleEnemyData = null;
		damageValueList.Clear();
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			characterId = 0;
			if (!scenarioBattleSkillManager.isUseChargeSkill)
			{
				characterId = scenarioBattleTurnManager.useSkillPartyMemberID;
				characterNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == characterId).memberNum;
			}
			else
			{
				characterId = 0;
				characterNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == characterId).memberNum;
			}
			switch (battleSkillData.skillType)
			{
			case BattleSkillData.SkillType.mixAttack:
			{
				float num7 = (float)PlayerStatusDataManager.characterAttack[characterId] * ((float)battleSkillData.skillPower / 100f);
				int battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[characterNum], "attack");
				float buffPower2 = GetBuffPower(battleBuffCondition2);
				num = Mathf.RoundToInt(num7 * buffPower2);
				float num8 = (float)PlayerStatusDataManager.characterMagicAttack[characterId] * ((float)battleSkillData.skillPower / 100f);
				PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[characterNum], "magicAttack");
				GetBuffPower(battleBuffCondition2);
				num2 = Mathf.RoundToInt(num8 * buffPower2);
				num += num2;
				Debug.Log("物魔攻撃力：" + num + "／物魔スキル威力" + battleSkillData.skillPower);
				break;
			}
			case BattleSkillData.SkillType.chargeAttack:
			{
				float num5 = (float)PlayerStatusDataManager.playerChargeAttack * ((float)battleSkillData.skillPower / 100f);
				int battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[characterNum], "attack");
				float buffPower = GetBuffPower(battleBuffCondition);
				num = Mathf.RoundToInt(num5 * buffPower);
				float num6 = (float)PlayerStatusDataManager.playerChargeMagicAttack * ((float)battleSkillData.skillPower / 100f);
				PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[characterNum], "magicAttack");
				GetBuffPower(battleBuffCondition);
				num2 = Mathf.RoundToInt(num6 * buffPower);
				num += num2;
				Debug.Log("チャージ攻撃力：" + num + "／チャージスキル威力" + battleSkillData.skillPower);
				break;
			}
			}
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				int i;
				for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
				{
					int id = PlayerStatusDataManager.enemyMember[i];
					bool isDead = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead;
					if (!isDead)
					{
						list.Add(PlayerStatusDataManager.enemyDefense[i]);
						int battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[i], "defense");
						float buffPower3 = GetBuffPower(battleBuffCondition3);
						list[i] = Mathf.RoundToInt((float)list[i] * buffPower3);
						list2.Add(PlayerStatusDataManager.enemyMagicDefense[i]);
						battleBuffCondition3 += PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[i], "magicDefense");
						buffPower3 = GetBuffPower(battleBuffCondition3);
						list2[i] = Mathf.RoundToInt((float)list2[i] * buffPower3);
					}
					else
					{
						list.Add(int.MaxValue);
						list2.Add(int.MaxValue);
					}
					if (isDead)
					{
						continue;
					}
					int index = GameDataManager.instance.battleEnemyDataBase.enemyDataList.FindIndex((BattleEnemyData data) => data.enemyID == id);
					battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList[index];
					if (battleEnemyData.weakElementList.Contains(battleSkillData.skillElement) && battleEnemyData.weakElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						list3.Add(item: true);
						scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberNum == i).isWeakHit = true;
					}
					else
					{
						list3.Add(item: false);
					}
					if (battleEnemyData.resistElementList.Contains(battleSkillData.skillElement) && battleEnemyData.resistElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						list4.Add(item: true);
						scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberNum == i).isResistHit = true;
					}
					else
					{
						list4.Add(item: false);
					}
				}
			}
			else
			{
				int id2 = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
				int index2 = GameDataManager.instance.battleEnemyDataBase.enemyDataList.FindIndex((BattleEnemyData data) => data.enemyID == id2);
				battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList[index2];
				list.Add(battleEnemyData.enemyDefense);
				int battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum], "defense");
				float buffPower4 = GetBuffPower(battleBuffCondition4);
				list[0] = Mathf.RoundToInt((float)list[0] * buffPower4);
				list2.Add(battleEnemyData.enemyMagicDefense);
				battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum], "magicDefense");
				buffPower4 = GetBuffPower(battleBuffCondition4);
				list2[0] = Mathf.RoundToInt((float)list2[0] * buffPower4);
				if (battleEnemyData.weakElementList.Contains(battleSkillData.skillElement) && battleEnemyData.weakElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
				{
					Debug.Log("味方単体物魔スキル／弱点有効／ID：" + id2);
					list3.Add(item: true);
					scenarioBattleTurnManager.skillAttackHitDataList[0].isWeakHit = true;
				}
				else
				{
					Debug.Log("味方単体物魔スキル／弱点ではない／ID：" + id2);
					list3.Add(item: false);
				}
				if (battleEnemyData.resistElementList.Contains(battleSkillData.skillElement) && battleEnemyData.resistElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
				{
					Debug.Log("味方単体物魔スキル／耐性有効／ID：" + id2);
					scenarioBattleTurnManager.skillAttackHitDataList[0].isResistHit = true;
					list4.Add(item: true);
				}
				else
				{
					Debug.Log("味方単体物魔スキル／耐性なし／ID：" + id2);
					list4.Add(item: false);
				}
			}
		}
		else
		{
			int memberNum = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum;
			if (battleSkillData.skillType == BattleSkillData.SkillType.mixAttack)
			{
				float num9 = (float)PlayerStatusDataManager.enemyAttack[memberNum] * ((float)battleSkillData.skillPower / 100f);
				int battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "attack");
				float buffPower5 = GetBuffPower(battleBuffCondition5);
				num = Mathf.RoundToInt(num9 * buffPower5 * 0.8f);
				_ = PlayerStatusDataManager.enemyMagicAttack[memberNum];
				_ = (float)battleSkillData.skillPower / 100f;
				battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "magicAttack");
				buffPower5 = GetBuffPower(battleBuffCondition5);
				num2 = Mathf.RoundToInt(num9 * buffPower5 * 0.8f);
				if (scenarioBattleTurnManager.isAllTargetSkill)
				{
					int j;
					for (j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
					{
						if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
						{
							int targetID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).memberID;
							int index3 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == targetID);
							list.Add(PlayerStatusDataManager.characterDefense[targetID]);
							battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index3], "defense");
							buffPower5 = GetBuffPower(battleBuffCondition5);
							list[j] = Mathf.RoundToInt((float)list[j] * buffPower5);
							Debug.Log("エネミー物魔スキル／キャラID：" + targetID + "／物防：" + list[j] + "／物防バフ：" + buffPower5);
							list2.Add(PlayerStatusDataManager.characterMagicDefense[targetID]);
							battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index3], "magicDefense");
							buffPower5 = GetBuffPower(battleBuffCondition5);
							list2[j] = Mathf.RoundToInt((float)list2[j] * buffPower5);
							Debug.Log("エネミー物魔スキル／キャラID：" + targetID + "／魔防：" + list2[j] + "／魔防バフ：" + buffPower5);
						}
						else
						{
							list.Add(int.MaxValue);
							list2.Add(int.MaxValue);
						}
					}
				}
				else
				{
					int targetID2 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
					int index4 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == targetID2);
					list.Add(PlayerStatusDataManager.characterDefense[targetID2]);
					battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index4], "defense");
					buffPower5 = GetBuffPower(battleBuffCondition5);
					list[0] = Mathf.RoundToInt((float)list[0] * buffPower5);
					Debug.Log("エネミー物魔スキル／キャラID：" + targetID2 + "／物防：" + list[0] + "／物防バフ：" + buffPower5);
					list2.Add(PlayerStatusDataManager.characterMagicDefense[targetID2]);
					battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index4], "magicDefense");
					buffPower5 = GetBuffPower(battleBuffCondition5);
					list2[0] = Mathf.RoundToInt((float)list2[0] * buffPower5);
					Debug.Log("エネミー物魔スキル／キャラID：" + targetID2 + "／魔防：" + list2[0] + "／魔防バフ：" + buffPower5);
				}
			}
		}
		for (int num10 = list.Count - 1; num10 >= 0; num10--)
		{
			if (list[num10] == int.MaxValue)
			{
				Debug.Log("死亡しているキャラの防御力をリストから除外／順番は逆順：" + num10);
				list.RemoveAt(num10);
			}
		}
		for (int num11 = list2.Count - 1; num11 >= 0; num11--)
		{
			if (list2[num11] == int.MaxValue)
			{
				Debug.Log("死亡しているキャラの魔法防御力をリストから除外／順番は逆順：" + num11);
				list2.RemoveAt(num11);
			}
		}
		bool flag = false;
		for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataList.Count; k++)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				num3 = ((!list3[k]) ? 1f : ((PlayerEquipDataManager.accessoryWeakPointDamageUp[characterId] <= 0) ? 1.5f : 2f));
				num4 = ((!list4[k]) ? 1f : 0.5f);
				int num12 = PlayerEquipDataManager.accessorySkillCritical[characterId];
				if (num12 > 0)
				{
					int num13 = Random.Range(0, 100);
					if (num12 >= num13)
					{
						flag = true;
					}
				}
			}
			float num14 = Random.Range(0.9f, 1.1f);
			int num15 = Mathf.RoundToInt((float)num * num3 * num4 * num14);
			int num16 = list[k];
			int num17 = Mathf.Clamp(num15 - num16, 0, 9999);
			if (flag)
			{
				num17 *= 2;
			}
			int num18 = Mathf.RoundToInt((float)num2 * num3 * num4 * num14);
			int num19 = list2[k];
			int num20 = Mathf.Clamp(num18 - num19, 0, 9999);
			if (flag)
			{
				num20 *= 2;
			}
			scenarioBattleTurnManager.skillAttackHitDataList[k].attackDamage = num17 + num20;
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				Debug.Log($"物理ダメージ：{num17}／魔法ダメージ：{num20}／ダメージ倍率：{num3}" + $"／攻撃力：{num15}／防御力：{num16}／魔法攻撃力：{num18}／魔法防御力：{num19}" + $"／弱点：{list3[k]}／耐性：{list4[k]} ");
			}
			else
			{
				Debug.Log($"物理ダメージ：{num17}／魔法ダメージ：{num20}／ダメージ倍率：{num3}" + $"／攻撃力：{num15}／防御力：{num16}／魔法攻撃力：{num18}／魔法防御力：{num19}");
			}
		}
		if (!scenarioBattleTurnManager.isUseSkillPlayer)
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				for (int l = 0; l < scenarioBattleTurnManager.skillAttackHitDataList.Count; l++)
				{
					int memberNum2 = scenarioBattleTurnManager.skillAttackHitDataList[l].memberNum;
					scenarioBattleTurnManager.enemySkillAttackDamageList[memberNum2] += scenarioBattleTurnManager.skillAttackHitDataList[l].attackDamage;
				}
				Debug.Log("敵の攻撃時のダメージ量を代入／全体スキル");
			}
			else
			{
				int attackDamage = scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).attackDamage;
				scenarioBattleTurnManager.enemySkillAttackDamageList[scenarioBattleTurnManager.enemyTargetNum] += attackDamage;
				Debug.Log("敵の攻撃時のダメージ量を代入／単体スキル");
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

	private float GetBuffPower(int num)
	{
		return num / 100 + 1;
	}
}
