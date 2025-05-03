using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcStateVampirePower : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public StateLink noVampireLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		scenarioBattleTurnManager.skillAttackHitDataSubList.Clear();
		List<float> list = new List<float>();
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < scenarioBattleTurnManager.enemyNormalAttackDamageList.Count; i++)
		{
			if (scenarioBattleTurnManager.enemyNormalAttackDamageList[i] > 0)
			{
				flag = true;
				Debug.Log("吸血判定：通常ダメージあり");
				break;
			}
		}
		for (int j = 0; j < scenarioBattleTurnManager.enemySkillAttackDamageList.Count; j++)
		{
			if (scenarioBattleTurnManager.enemySkillAttackDamageList[j] > 0)
			{
				flag = true;
				Debug.Log("吸血判定：スキルダメージあり");
				break;
			}
		}
		if (flag)
		{
			for (int k = 0; k < PlayerStatusDataManager.playerPartyMember.Length; k++)
			{
				int num = PlayerEquipDataManager.equipFactorVampire[PlayerStatusDataManager.playerPartyMember[k]];
				int num2 = PlayerEquipDataManager.accessoryVampire[PlayerStatusDataManager.playerPartyMember[k]];
				if (num + num2 > 0)
				{
					int id = PlayerStatusDataManager.playerPartyMember[k];
					if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).isDead)
					{
						SkillAttackHitData item = new SkillAttackHitData
						{
							memberNum = k,
							attackDamage = scenarioBattleTurnManager.enemyNormalAttackDamageList[PlayerStatusDataManager.playerPartyMember[k]] + scenarioBattleTurnManager.enemySkillAttackDamageList[PlayerStatusDataManager.playerPartyMember[k]]
						};
						scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
						list.Add(PlayerEquipDataManager.equipFactorVampire[PlayerStatusDataManager.playerPartyMember[k]]);
					}
				}
			}
			for (int l = 0; l < scenarioBattleTurnManager.skillAttackHitDataList.Count; l++)
			{
				list[l] /= 100f;
				float num3 = (float)scenarioBattleTurnManager.skillAttackHitDataList[l].attackDamage * list[l];
				float num4 = Random.Range(0.9f, 1.1f);
				int num5 = Mathf.FloorToInt(num3 * num4);
				scenarioBattleTurnManager.skillAttackHitDataList[l].healValue = num5;
				Debug.Log($"ダメージ吸収量は：{num5}／ダメージ吸収力：{list[l]}");
			}
			for (int m = 0; m < scenarioBattleTurnManager.skillAttackHitDataList.Count; m++)
			{
				if (scenarioBattleTurnManager.skillAttackHitDataList[m].healValue > 0)
				{
					flag2 = true;
					break;
				}
			}
		}
		if (flag2)
		{
			Debug.Log("ダメージ吸収するキャラがいる");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("ダメージ吸収するキャラはいない");
			Transition(noVampireLink);
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
