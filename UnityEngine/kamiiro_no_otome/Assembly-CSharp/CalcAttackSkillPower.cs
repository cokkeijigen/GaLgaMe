using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CalcAttackSkillPower : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private BattleSkillData battleSkillData;

	private int characterId;

	private int characterNum;

	private List<float> damageValueList = new List<float>();

	public StateLink attackLink;

	public StateLink chargeAttackLink;

	public StateLink deBuffLink;

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
		float num2 = 1f;
		float num3 = 1f;
		List<int> list = new List<int>();
		new List<int>();
		List<bool> list2 = new List<bool>();
		List<bool> list3 = new List<bool>();
		BattleEnemyData battleEnemyData = null;
		damageValueList.Clear();
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		if (battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
		{
			string effectType = battleSkillData.effectType;
			string text = effectType.Substring(0, 1).ToUpper();
			effectType = "SeSkill" + text + effectType.Substring(1);
			MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
			Transition(deBuffLink);
			return;
		}
		if (battleSkillData.skillType == BattleSkillData.SkillType.mixAttack || battleSkillData.skillType == BattleSkillData.SkillType.chargeAttack)
		{
			Transition(chargeAttackLink);
			return;
		}
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			characterId = 0;
			characterId = scenarioBattleTurnManager.useSkillPartyMemberID;
			characterNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == characterId).memberNum;
			switch (battleSkillData.skillType)
			{
			case BattleSkillData.SkillType.attack:
			{
				float num5 = (float)PlayerStatusDataManager.characterAttack[characterId] * ((float)battleSkillData.skillPower / 100f);
				int battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[characterNum], "attack");
				battleBuffCondition2 += PlayerEquipDataManager.accessorySkillPowerUp[characterId];
				float buffPower2 = GetBuffPower(battleBuffCondition2);
				num = Mathf.RoundToInt(num5 * buffPower2);
				Debug.Log("キャラ攻撃力：" + PlayerStatusDataManager.characterAttack[characterId] + "／スキル威力" + battleSkillData.skillPower);
				if (scenarioBattleTurnManager.isAllTargetSkill)
				{
					int j;
					for (j = 0; j < PlayerStatusDataManager.enemyMember.Length; j++)
					{
						int id3 = PlayerStatusDataManager.enemyMember[j];
						if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
						{
							list.Add(PlayerStatusDataManager.enemyDefense[j]);
							battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[j], "defense");
							buffPower2 = GetBuffPower(battleBuffCondition2);
							list[j] = Mathf.RoundToInt((float)list[j] * buffPower2);
						}
						else
						{
							list.Add(int.MaxValue);
						}
						int index2 = GameDataManager.instance.battleEnemyDataBase.enemyDataList.FindIndex((BattleEnemyData data) => data.enemyID == id3);
						battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList[index2];
						if (battleEnemyData.weakElementList.Contains(battleSkillData.skillElement) && battleEnemyData.weakElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
						{
							list2.Add(item: true);
							scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberNum == j).isWeakHit = true;
						}
						else
						{
							list2.Add(item: false);
						}
						if (battleEnemyData.resistElementList.Contains(battleSkillData.skillElement) && battleEnemyData.resistElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
						{
							list3.Add(item: true);
							scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberNum == j).isResistHit = true;
						}
						else
						{
							list3.Add(item: false);
						}
					}
				}
				else
				{
					int id4 = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
					battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id4);
					list.Add(battleEnemyData.enemyDefense);
					battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum], "defense");
					buffPower2 = GetBuffPower(battleBuffCondition2);
					list[0] = Mathf.RoundToInt((float)list[0] * buffPower2);
					if (battleEnemyData.weakElementList.Contains(battleSkillData.skillElement) && battleEnemyData.weakElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("味方単体物理スキル／弱点有効／ID：" + id4);
						list2.Add(item: true);
						scenarioBattleTurnManager.skillAttackHitDataList[0].isWeakHit = true;
					}
					else
					{
						Debug.Log("味方単体物理スキル／弱点ではない／ID：" + id4);
						list2.Add(item: false);
					}
					if (battleEnemyData.resistElementList.Contains(battleSkillData.skillElement) && battleEnemyData.resistElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("味方単体物理スキル／耐性有効／ID：" + id4);
						scenarioBattleTurnManager.skillAttackHitDataList[0].isResistHit = true;
						list3.Add(item: true);
					}
					else
					{
						Debug.Log("味方単体物理スキル／耐性なし／ID：" + id4);
						list3.Add(item: false);
					}
				}
				break;
			}
			case BattleSkillData.SkillType.magicAttack:
			{
				float num4 = (float)PlayerStatusDataManager.characterMagicAttack[characterId] * ((float)battleSkillData.skillPower / 100f);
				int battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[characterNum], "magicAttack");
				battleBuffCondition += PlayerEquipDataManager.accessorySkillPowerUp[characterId];
				float buffPower = GetBuffPower(battleBuffCondition);
				num = Mathf.RoundToInt(num4 * buffPower);
				if (scenarioBattleTurnManager.isAllTargetSkill)
				{
					int i;
					for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
					{
						int id = PlayerStatusDataManager.enemyMember[i];
						bool isDead = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead;
						if (!isDead)
						{
							list.Add(PlayerStatusDataManager.enemyMagicDefense[i]);
							battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[i], "magicDefense");
							buffPower = GetBuffPower(battleBuffCondition);
							list[i] = Mathf.RoundToInt((float)list[i] * buffPower);
						}
						else
						{
							list.Add(int.MaxValue);
						}
						if (isDead)
						{
							continue;
						}
						int index = GameDataManager.instance.battleEnemyDataBase.enemyDataList.FindIndex((BattleEnemyData data) => data.enemyID == id);
						battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList[index];
						if (battleEnemyData.weakElementList.Contains(battleSkillData.skillElement) && battleEnemyData.weakElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
						{
							list2.Add(item: true);
							scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberNum == i).isWeakHit = true;
						}
						else
						{
							list2.Add(item: false);
						}
						if (battleEnemyData.resistElementList.Contains(battleSkillData.skillElement) && battleEnemyData.resistElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
						{
							list3.Add(item: true);
							scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberNum == i).isResistHit = true;
						}
						else
						{
							list3.Add(item: false);
						}
					}
				}
				else
				{
					int id2 = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
					battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id2);
					list.Add(battleEnemyData.enemyMagicDefense);
					battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[scenarioBattleTurnManager.playerTargetNum], "magicDefense");
					buffPower = GetBuffPower(battleBuffCondition);
					list[0] = Mathf.RoundToInt((float)list[0] * buffPower);
					if (battleEnemyData.weakElementList.Contains(battleSkillData.skillElement) && battleEnemyData.weakElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("味方単体魔法スキル／弱点有効／ID：" + id2);
						list2.Add(item: true);
						scenarioBattleTurnManager.skillAttackHitDataList[0].isWeakHit = true;
					}
					else
					{
						Debug.Log("味方単体魔法スキル／弱点ではない／ID：" + id2);
						list2.Add(item: false);
					}
					if (battleEnemyData.resistElementList.Contains(battleSkillData.skillElement) && battleEnemyData.resistElementList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("味方単体魔法スキル／耐性有効／ID：" + id2);
						scenarioBattleTurnManager.skillAttackHitDataList[0].isResistHit = true;
						list3.Add(item: true);
					}
					else
					{
						Debug.Log("味方単体魔法スキル／耐性なし／ID：" + id2);
						list3.Add(item: false);
					}
				}
				break;
			}
			}
		}
		else
		{
			int memberNum = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum;
			switch (battleSkillData.skillType)
			{
			case BattleSkillData.SkillType.attack:
			{
				float num7 = (float)PlayerStatusDataManager.enemyAttack[memberNum] * ((float)battleSkillData.skillPower / 100f);
				int battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "attack");
				float buffPower4 = GetBuffPower(battleBuffCondition4);
				num = Mathf.RoundToInt(num7 * buffPower4);
				Debug.Log("エネミースキルの攻撃力：" + num7 + "／エネミーのバフ：" + buffPower4);
				if (scenarioBattleTurnManager.isAllTargetSkill)
				{
					int l;
					for (l = 0; l < PlayerStatusDataManager.playerPartyMember.Length; l++)
					{
						if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == l).isDead)
						{
							int targetID3 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == l).memberID;
							int index5 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == targetID3);
							CharacterStatusData characterStatusData3 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == targetID3);
							list.Add(PlayerStatusDataManager.characterDefense[targetID3]);
							battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index5], "defense");
							buffPower4 = GetBuffPower(battleBuffCondition4);
							list[l] = Mathf.RoundToInt((float)list[l] * buffPower4);
							Debug.Log("味方の防御力：" + list[l] + "／ID：" + targetID3);
							if (characterStatusData3.weakElementsList.Contains(battleSkillData.skillElement) && characterStatusData3.weakElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
							{
								list2.Add(item: true);
								scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberID == targetID3).isWeakHit = true;
								Debug.Log("味方の弱点属性あり：／ID：" + targetID3);
							}
							else
							{
								list2.Add(item: false);
							}
							if (characterStatusData3.resistElementsList.Contains(battleSkillData.skillElement) && characterStatusData3.resistElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
							{
								list3.Add(item: true);
								scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberID == targetID3).isResistHit = true;
								Debug.Log("味方の耐性属性あり：／ID：" + targetID3);
							}
							else
							{
								list3.Add(item: false);
							}
						}
						else
						{
							list.Add(int.MaxValue);
						}
					}
				}
				else
				{
					int targetID4 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
					int index6 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum);
					CharacterStatusData characterStatusData4 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == targetID4);
					list.Add(PlayerStatusDataManager.characterDefense[targetID4]);
					battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index6], "defense");
					buffPower4 = GetBuffPower(battleBuffCondition4);
					list[0] = Mathf.RoundToInt((float)list[0] * buffPower4);
					Debug.Log("味方の防御力：" + list[0] + "／ID：" + targetID4);
					if (characterStatusData4.weakElementsList.Contains(battleSkillData.skillElement) && characterStatusData4.weakElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("敵単体物理スキル／弱点有効");
						list2.Add(item: true);
						scenarioBattleTurnManager.skillAttackHitDataList[0].isWeakHit = true;
					}
					else
					{
						Debug.Log("敵単体物理スキル／弱点ではない");
						list2.Add(item: false);
					}
					if (characterStatusData4.resistElementsList.Contains(battleSkillData.skillElement) && characterStatusData4.resistElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("敵単体物理スキル／耐性有効");
						scenarioBattleTurnManager.skillAttackHitDataList[0].isResistHit = true;
						list3.Add(item: true);
					}
					else
					{
						Debug.Log("敵単体物理スキル／耐性なし");
						list3.Add(item: false);
					}
				}
				break;
			}
			case BattleSkillData.SkillType.magicAttack:
			{
				float num6 = (float)PlayerStatusDataManager.enemyMagicAttack[memberNum] * ((float)battleSkillData.skillPower / 100f);
				int battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "magicAttack");
				float buffPower3 = GetBuffPower(battleBuffCondition3);
				num = Mathf.RoundToInt(num6 * buffPower3);
				if (scenarioBattleTurnManager.isAllTargetSkill)
				{
					int k;
					for (k = 0; k < PlayerStatusDataManager.playerPartyMember.Length; k++)
					{
						if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == k).isDead)
						{
							int targetID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == k).memberID;
							int index3 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == targetID);
							CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == targetID);
							list.Add(PlayerStatusDataManager.characterMagicDefense[targetID]);
							battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index3], "magicDefense");
							buffPower3 = GetBuffPower(battleBuffCondition3);
							list[k] = Mathf.RoundToInt((float)list[k] * buffPower3);
							Debug.Log("味方の防御力：" + list[k] + "／ID：" + targetID);
							if (characterStatusData.weakElementsList.Contains(battleSkillData.skillElement) && characterStatusData.weakElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
							{
								list2.Add(item: true);
								scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberID == targetID).isWeakHit = true;
								Debug.Log("味方の弱点属性あり：／ID：" + targetID);
							}
							else
							{
								list2.Add(item: false);
							}
							if (characterStatusData.resistElementsList.Contains(battleSkillData.skillElement) && characterStatusData.resistElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
							{
								list3.Add(item: true);
								scenarioBattleTurnManager.skillAttackHitDataList.Find((SkillAttackHitData data) => data.memberID == targetID).isResistHit = true;
								Debug.Log("味方の耐性属性あり：／ID：" + targetID);
							}
							else
							{
								list3.Add(item: false);
							}
						}
						else
						{
							list.Add(int.MaxValue);
						}
					}
				}
				else
				{
					int targetID2 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
					int index4 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum);
					CharacterStatusData characterStatusData2 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == targetID2);
					list.Add(PlayerStatusDataManager.characterMagicDefense[targetID2]);
					battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[index4], "magicDefense");
					buffPower3 = GetBuffPower(battleBuffCondition3);
					list[0] = Mathf.RoundToInt((float)list[0] * buffPower3);
					Debug.Log("味方の防御力：" + list[0] + "／ID：" + targetID2);
					if (characterStatusData2.weakElementsList.Contains(battleSkillData.skillElement) && characterStatusData2.weakElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("敵単体魔法スキル／弱点有効");
						list2.Add(item: true);
						scenarioBattleTurnManager.skillAttackHitDataList[0].isWeakHit = true;
					}
					else
					{
						Debug.Log("敵単体魔法スキル／弱点ではない");
						list2.Add(item: false);
					}
					if (characterStatusData2.resistElementsList.Contains(battleSkillData.skillElement) && characterStatusData2.resistElementsList.Count > 0 && !string.IsNullOrEmpty(battleSkillData.skillElement))
					{
						Debug.Log("敵単体魔法スキル／耐性有効");
						scenarioBattleTurnManager.skillAttackHitDataList[0].isResistHit = true;
						list3.Add(item: true);
					}
					else
					{
						Debug.Log("敵単体魔法スキル／耐性なし");
						list3.Add(item: false);
					}
				}
				break;
			}
			}
		}
		for (int num8 = list.Count - 1; num8 >= 0; num8--)
		{
			if (list[num8] == int.MaxValue)
			{
				Debug.Log("死亡しているキャラの防御力をリストから除外／順番は逆順：" + num8);
				list.RemoveAt(num8);
			}
		}
		bool flag = false;
		for (int m = 0; m < scenarioBattleTurnManager.skillAttackHitDataList.Count; m++)
		{
			Debug.Log("スキルダメージ計算順：" + m + "／防御力：" + list[m]);
			if (!scenarioBattleTurnManager.isUseSkillPlayer)
			{
				num2 = ((!list2[m]) ? 1f : 1.5f);
				num3 = ((!list3[m]) ? 1f : 0.5f);
			}
			else
			{
				num2 = ((!list2[m]) ? 1f : ((PlayerEquipDataManager.accessoryWeakPointDamageUp[characterId] <= 0) ? 1.5f : 2f));
				num3 = ((!list3[m]) ? 1f : 0.5f);
				int num9 = PlayerEquipDataManager.accessorySkillCritical[characterId];
				if (num9 > 0)
				{
					int num10 = Random.Range(0, 100);
					if (num9 >= num10)
					{
						flag = true;
					}
				}
			}
			float num11 = Random.Range(0.9f, 1.1f);
			int num12 = Mathf.RoundToInt((float)num * num2 * num3 * num11);
			int num13 = list[m];
			int num14 = Mathf.Clamp(num12 - num13, 0, 99999);
			if (flag)
			{
				num14 *= 2;
			}
			scenarioBattleTurnManager.skillAttackHitDataList[m].attackDamage = num14;
			Debug.Log("攻撃順：" + m + "／ターゲットNum：" + scenarioBattleTurnManager.skillAttackHitDataList[m].memberNum + "／ターゲットID：" + scenarioBattleTurnManager.skillAttackHitDataList[m].memberID);
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				Debug.Log("スキルダメージ：" + num14 + "／ダメージ倍率：" + num2 + "／攻撃力：" + num12 + "／防御力：" + num13 + "／弱点：" + list2[m] + "／耐性：" + list3[m]);
			}
			else
			{
				Debug.Log("スキルダメージ：" + num14 + "／ダメージ倍率：" + num2 + "／攻撃力：" + num12 + "／防御力：" + num13);
			}
		}
		if (!scenarioBattleTurnManager.isUseSkillPlayer)
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				for (int n = 0; n < scenarioBattleTurnManager.skillAttackHitDataList.Count; n++)
				{
					int memberNum2 = scenarioBattleTurnManager.skillAttackHitDataList[n].memberNum;
					scenarioBattleTurnManager.enemySkillAttackDamageList[memberNum2] += scenarioBattleTurnManager.skillAttackHitDataList[n].attackDamage;
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
		Transition(attackLink);
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
