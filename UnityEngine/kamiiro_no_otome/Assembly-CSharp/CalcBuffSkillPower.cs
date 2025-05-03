using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcBuffSkillPower : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private BattleSkillData battleSkillData;

	public StateLink allDefenseLink;

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
		if (battleSkillData.skillType == BattleSkillData.SkillType.buff && battleSkillData.buffType == BattleSkillData.BuffType.allDefense)
		{
			Transition(allDefenseLink);
			return;
		}
		string typeName;
		if (battleSkillData.skillType == BattleSkillData.SkillType.regeneration)
		{
			typeName = "regeneration";
		}
		else
		{
			typeName = battleSkillData.buffType.ToString();
		}
		int num = 0;
		List<int> list = new List<int>();
		List<List<PlayerBattleConditionManager.MemberBuffCondition>> list2 = new List<List<PlayerBattleConditionManager.MemberBuffCondition>>();
		List<PlayerBattleConditionManager.MemberIsDead> list3 = new List<PlayerBattleConditionManager.MemberIsDead>();
		int skillPower = battleSkillData.skillPower;
		int[] memberArray;
		bool targetForce;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			list2 = PlayerBattleConditionManager.playerBuffCondition;
			num = PlayerStatusDataManager.playerPartyMember.Length;
			memberArray = PlayerStatusDataManager.playerPartyMember;
			list3 = PlayerBattleConditionManager.playerIsDead;
			targetForce = true;
		}
		else
		{
			list2 = PlayerBattleConditionManager.enemyBuffCondition;
			num = PlayerStatusDataManager.enemyMember.Length;
			memberArray = PlayerStatusDataManager.enemyMember;
			list3 = PlayerBattleConditionManager.enemyIsDead;
			targetForce = false;
		}
		Debug.Log("ループ回数" + num);
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			int i;
			for (i = 0; i < num; i++)
			{
				if (!list3.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == memberArray[i]).isDead)
				{
					int num2 = 0;
					num2 = ((!scenarioBattleTurnManager.isUseSkillPlayer) ? PlayerBattleConditionManager.enemyIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == memberArray[i]) : PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == memberArray[i]));
					list = (from ano in list2[num2].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == typeName
						select ano.Index).ToList();
					if (list.Any())
					{
						if (list2[num2][list[0]].power < 0)
						{
							int index2 = list[0];
							list2[i].RemoveAt(index2);
							Debug.Log("デバフ無効化");
							utageBattleSceneManager.SetBuffIcon(typeName, i, targetForce, setVisible: false, isPositive: false);
						}
						else if (list2[num2][list[0]].power < skillPower)
						{
							list2[num2][list[0]].power = skillPower;
							list2[num2][list[0]].continutyTurn = battleSkillData.skillContinuity;
							utageBattleSceneManager.SetBuffIcon(typeName, i, targetForce, setVisible: true, isPositive: true);
							Debug.Log("今回のスキルの方が強い");
						}
						else
						{
							list2[num2][list[0]].continutyTurn = battleSkillData.skillContinuity;
							utageBattleSceneManager.SetBuffIcon(typeName, i, targetForce, setVisible: true, isPositive: true);
							Debug.Log("今回のスキルの方が弱い");
						}
					}
					else
					{
						AddNewBuffData(typeName, skillPower, list2, targetForce, num2, i);
					}
				}
				else
				{
					Debug.Log("バフ対象は戦闘不能／ナンバー：" + memberArray[i]);
				}
			}
		}
		else
		{
			int num3 = 0;
			int num4 = 0;
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				num4 = scenarioBattleTurnManager.playerTargetNum;
				num3 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			}
			else
			{
				num4 = scenarioBattleTurnManager.enemyTargetNum;
				num3 = PlayerBattleConditionManager.enemyIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum);
			}
			list = (from ano in list2[num3].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == typeName
				select ano.Index).ToList();
			if (list2[num3].Any((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == typeName))
			{
				if (list2[num3][list[0]].power < 0)
				{
					int index3 = list[0];
					list2[num3].RemoveAt(index3);
					Debug.Log("デバフ無効化");
					utageBattleSceneManager.SetBuffIcon(typeName, num4, targetForce, setVisible: false, isPositive: false);
				}
				else if (list2[num3][list[0]].power < skillPower)
				{
					list2[num3][list[0]].power = skillPower;
					list2[num3][list[0]].continutyTurn = battleSkillData.skillContinuity;
					utageBattleSceneManager.SetBuffIcon(typeName, num4, targetForce, setVisible: true, isPositive: true);
					Debug.Log("今回のスキルの方が強い");
				}
				else
				{
					list2[num3][list[0]].continutyTurn = battleSkillData.skillContinuity;
					utageBattleSceneManager.SetBuffIcon(typeName, num4, targetForce, setVisible: true, isPositive: true);
					Debug.Log("今回のスキルの方が弱い");
				}
			}
			else
			{
				AddNewBuffData(typeName, skillPower, list2, targetForce, num3, num4);
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

	private void AddNewBuffData(string typeName, int skillPower, List<List<PlayerBattleConditionManager.MemberBuffCondition>> targetBuffList, bool targetForce, int targetIndex, int num)
	{
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = typeName,
			power = skillPower,
			continutyTurn = battleSkillData.skillContinuity
		};
		targetBuffList[targetIndex].Add(item);
		utageBattleSceneManager.SetBuffIcon(typeName, num, targetForce, setVisible: true, isPositive: true);
		Debug.Log("バフデータ追加／タイプ：" + typeName + "／num：" + num + "／味方陣営：" + targetForce);
	}
}
