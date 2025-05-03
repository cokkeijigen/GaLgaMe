using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDebuffSkillPower : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private BattleSkillData battleSkillData;

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
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		string typeName = battleSkillData.buffType.ToString();
		int num = 0;
		int num2 = 0;
		List<int> list = new List<int>();
		List<List<PlayerBattleConditionManager.MemberBuffCondition>> list2 = new List<List<PlayerBattleConditionManager.MemberBuffCondition>>();
		List<PlayerBattleConditionManager.MemberIsDead> list3 = new List<PlayerBattleConditionManager.MemberIsDead>();
		List<int> hitMemberList = new List<int>();
		if (scenarioBattleTurnManager.skillAttackHitDataSubList.Count > 0)
		{
			num = ((battleSkillData.skillType != BattleSkillData.SkillType.deBuff) ? battleSkillData.subSkillPower : battleSkillData.skillPower);
			num2 = scenarioBattleTurnManager.skillAttackHitDataSubList.Count;
			hitMemberList = scenarioBattleTurnManager.skillAttackHitDataSubList.Select((SkillAttackHitData data) => data.memberNum).ToList();
			Debug.Log("付与攻撃orデバフスキル");
		}
		bool targetForce;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			list2 = PlayerBattleConditionManager.enemyBuffCondition;
			list3 = PlayerBattleConditionManager.enemyIsDead;
			targetForce = false;
		}
		else
		{
			list2 = PlayerBattleConditionManager.playerBuffCondition;
			list3 = PlayerBattleConditionManager.playerIsDead;
			targetForce = true;
		}
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			int i;
			for (i = 0; i < num2; i++)
			{
				if (!list3.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == hitMemberList[i]).isDead)
				{
					int num3 = 0;
					num3 = ((!scenarioBattleTurnManager.isUseSkillPlayer) ? PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == hitMemberList[i]) : PlayerBattleConditionManager.enemyIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == hitMemberList[i]));
					list = (from ano in list2[num3].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == typeName
						select ano.Index).ToList();
					if (list.Any())
					{
						if (list2[num3][list[0]].power >= 0)
						{
							int index2 = list[0];
							list2[num3].RemoveAt(index2);
							utageBattleSceneManager.SetBuffIcon(typeName, hitMemberList[i], targetForce, setVisible: false, isPositive: true);
						}
						else if (list2[num3][list[0]].power > num)
						{
							list2[num3][list[0]].power = num;
							list2[num3][list[0]].continutyTurn = battleSkillData.skillContinuity;
						}
						else
						{
							list2[num3][list[0]].continutyTurn = battleSkillData.skillContinuity;
						}
					}
					else
					{
						AddNewDebuffData(typeName, num, list2, targetForce, num3, hitMemberList[i]);
					}
				}
				else
				{
					Debug.Log("デバフ対象は戦闘不能／ナンバー：" + hitMemberList[i]);
				}
			}
		}
		else
		{
			int num4 = 0;
			int num5 = 0;
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				num5 = scenarioBattleTurnManager.playerTargetNum;
				num4 = PlayerBattleConditionManager.enemyIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			}
			else
			{
				num5 = scenarioBattleTurnManager.enemyTargetNum;
				num4 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum);
			}
			if (list2[num4].Any((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == typeName))
			{
				if (list2[num4][list[0]].power >= 0)
				{
					int index3 = list[0];
					list2[num4].RemoveAt(index3);
					utageBattleSceneManager.SetBuffIcon(typeName, num4, targetForce, setVisible: false, isPositive: true);
				}
				else if (list2[num4][list[0]].power > num)
				{
					list2[num4][list[0]].power = num;
					list2[num4][list[0]].continutyTurn = battleSkillData.skillContinuity;
				}
				else
				{
					list2[num4][list[0]].continutyTurn = battleSkillData.skillContinuity;
				}
			}
			else
			{
				AddNewDebuffData(typeName, num, list2, targetForce, num4, num5);
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

	private void AddNewDebuffData(string typeName, int skillPower, List<List<PlayerBattleConditionManager.MemberBuffCondition>> targetBuffList, bool targetForce, int targetIndex, int num)
	{
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = battleSkillData.buffType.ToString(),
			power = skillPower,
			continutyTurn = battleSkillData.skillContinuity
		};
		targetBuffList[targetIndex].Add(item);
		utageBattleSceneManager.SetBuffIcon(typeName, num, targetForce, setVisible: true, isPositive: false);
		Debug.Log("デバフデータ追加");
	}
}
