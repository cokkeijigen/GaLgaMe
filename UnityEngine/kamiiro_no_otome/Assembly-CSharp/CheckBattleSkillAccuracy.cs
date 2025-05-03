using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckBattleSkillAccuracy : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private BattleSkillData battleSkillData;

	public StateLink trueLink;

	public StateLink falseLink;

	public StateLink positiveSkillLink;

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
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		if (battleSkillData.skillType == BattleSkillData.SkillType.attack || battleSkillData.skillType == BattleSkillData.SkillType.magicAttack || battleSkillData.skillType == BattleSkillData.SkillType.mixAttack)
		{
			CalcAccuracy();
		}
		else if (battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
		{
			CalcAccuracy();
		}
		else
		{
			Transition(positiveSkillLink);
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

	private void CalcAccuracy()
	{
		float num = 0f;
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		if (battleSkillData.skillTarget == BattleSkillData.SkillTarget.solo)
		{
			scenarioBattleTurnManager.isAllTargetSkill = false;
			num = Random.Range(0, 100);
			if ((float)battleSkillData.skillAccuracy >= num)
			{
				if (scenarioBattleTurnManager.isUseSkillPlayer)
				{
					SkillAttackHitData item = new SkillAttackHitData
					{
						isAttackHit = true,
						memberID = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum],
						memberNum = scenarioBattleTurnManager.playerTargetNum
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
				}
				else
				{
					int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
					SkillAttackHitData item2 = new SkillAttackHitData
					{
						isAttackHit = true,
						memberID = memberID,
						memberNum = scenarioBattleTurnManager.enemyTargetNum
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item2);
				}
				Transition(trueLink);
			}
			else
			{
				if (scenarioBattleTurnManager.isUseSkillPlayer)
				{
					SkillAttackHitData item3 = new SkillAttackHitData
					{
						isAttackHit = false,
						memberID = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum],
						memberNum = scenarioBattleTurnManager.playerTargetNum
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item3);
				}
				else
				{
					int memberID2 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID;
					SkillAttackHitData item4 = new SkillAttackHitData
					{
						isAttackHit = false,
						memberID = memberID2,
						memberNum = scenarioBattleTurnManager.enemyTargetNum
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item4);
				}
				Invoke("InvokeMethod", 0f);
			}
		}
		else
		{
			scenarioBattleTurnManager.isAllTargetSkill = true;
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int i;
				for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
				{
					num = Random.Range(0, 100);
					if ((float)battleSkillData.skillAccuracy >= num)
					{
						if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead)
						{
							SkillAttackHitData item5 = new SkillAttackHitData
							{
								isAttackHit = true,
								memberID = PlayerStatusDataManager.enemyMember[i],
								memberNum = i
							};
							scenarioBattleTurnManager.skillAttackHitDataList.Add(item5);
						}
					}
					else
					{
						SkillAttackHitData item6 = new SkillAttackHitData
						{
							isAttackHit = false,
							memberID = PlayerStatusDataManager.enemyMember[i],
							memberNum = i
						};
						scenarioBattleTurnManager.skillAttackHitDataList.Add(item6);
					}
				}
			}
			else
			{
				int j;
				for (j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
				{
					int id = PlayerStatusDataManager.playerPartyMember[j];
					int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
					num = Random.Range(0, 100);
					if ((float)battleSkillData.skillAccuracy >= num)
					{
						if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
						{
							SkillAttackHitData item7 = new SkillAttackHitData
							{
								isAttackHit = true,
								memberID = id,
								memberNum = memberNum
							};
							scenarioBattleTurnManager.skillAttackHitDataList.Add(item7);
						}
					}
					else
					{
						SkillAttackHitData item8 = new SkillAttackHitData
						{
							isAttackHit = false,
							memberID = id,
							memberNum = memberNum
						};
						scenarioBattleTurnManager.skillAttackHitDataList.Add(item8);
					}
				}
			}
			if (scenarioBattleTurnManager.skillAttackHitDataList != null || scenarioBattleTurnManager.skillAttackHitDataList.Count > 0)
			{
				Transition(trueLink);
			}
			else
			{
				Invoke("InvokeMethod", 0f);
			}
		}
		Debug.Log("命中率：" + battleSkillData.skillAccuracy + " / スキル命中ランダム：" + num);
	}

	private void InvokeMethod()
	{
		Transition(falseLink);
	}
}
