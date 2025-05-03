using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcBuffSkillAllDefense : StateBehaviour
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
		string text = battleSkillData.buffType.ToString();
		int num = 0;
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<List<PlayerBattleConditionManager.MemberBuffCondition>> list3 = new List<List<PlayerBattleConditionManager.MemberBuffCondition>>();
		List<PlayerBattleConditionManager.MemberIsDead> list4 = new List<PlayerBattleConditionManager.MemberIsDead>();
		int skillPower = battleSkillData.skillPower;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			list3 = PlayerBattleConditionManager.playerBuffCondition;
			num = PlayerStatusDataManager.playerPartyMember.Length;
			list4 = PlayerBattleConditionManager.playerIsDead;
		}
		Debug.Log("ループ回数" + num);
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			int i;
			for (i = 0; i < num; i++)
			{
				if (!list4.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == PlayerStatusDataManager.playerPartyMember[i]).isDead)
				{
					int num2 = 0;
					num2 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == PlayerStatusDataManager.playerPartyMember[i]);
					list = (from ano in list3[num2].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "defense"
						select ano.Index).ToList();
					if (list.Any())
					{
						text = "defense";
						if (list3[num2][list[0]].power < 0)
						{
							int index2 = list[0];
							Debug.Log("物防デバフ無効化／num：" + index2 + "／for回数：" + i);
							list3[num2].RemoveAt(index2);
							utageBattleSceneManager.SetBuffIcon(text, i, targetForce: true, setVisible: false, isPositive: false);
						}
						else if (list3[num2][list[0]].power < skillPower)
						{
							list3[num2][list[0]].power = skillPower;
							list3[num2][list[0]].continutyTurn = battleSkillData.skillContinuity;
							utageBattleSceneManager.SetBuffIcon(text, i, targetForce: true, setVisible: true, isPositive: true);
							Debug.Log("今回のスキルの方が強い");
						}
						else
						{
							list3[num2][list[0]].continutyTurn = battleSkillData.skillContinuity;
							utageBattleSceneManager.SetBuffIcon(text, i, targetForce: true, setVisible: true, isPositive: true);
							Debug.Log("今回のスキルの方が弱い");
						}
					}
					else
					{
						AddNewBuffData("defense", skillPower, list3, targetForce: true, num2, i);
					}
					list2 = (from ano in list3[num2].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "magicDefense"
						select ano.Index).ToList();
					if (list2.Any())
					{
						text = "magicDefense";
						if (list3[num2][list2[0]].power < 0)
						{
							int index3 = list2[0];
							list3[num2].RemoveAt(index3);
							Debug.Log("魔防デバフ無効化");
							utageBattleSceneManager.SetBuffIcon(text, i, targetForce: true, setVisible: false, isPositive: false);
						}
						else if (list3[num2][list2[0]].power < skillPower)
						{
							list3[num2][list2[0]].power = skillPower;
							list3[num2][list2[0]].continutyTurn = battleSkillData.skillContinuity;
							utageBattleSceneManager.SetBuffIcon(text, i, targetForce: true, setVisible: true, isPositive: true);
							Debug.Log("今回のスキルの方が強い");
						}
						else
						{
							list3[num2][list2[0]].continutyTurn = battleSkillData.skillContinuity;
							utageBattleSceneManager.SetBuffIcon(text, i, targetForce: true, setVisible: true, isPositive: true);
							Debug.Log("今回のスキルの方が弱い");
						}
					}
					else
					{
						AddNewBuffData("magicDefense", skillPower, list3, targetForce: true, num2, i);
					}
				}
				else
				{
					Debug.Log("バフ対象は戦闘不能／ナンバー：" + PlayerStatusDataManager.playerPartyMember[i]);
				}
			}
		}
		else
		{
			int num3 = 0;
			int num4 = 0;
			num4 = scenarioBattleTurnManager.playerTargetNum;
			num3 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			list = (from ano in list3[num3].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "defense"
				select ano.Index).ToList();
			if (list3[num3].Any((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == "defense"))
			{
				text = "defense";
				if (list3[num3][list[0]].power < 0)
				{
					int index4 = list[0];
					list3[num3].RemoveAt(index4);
					Debug.Log("物防デバフ無効化");
					utageBattleSceneManager.SetBuffIcon(text, num4, targetForce: true, setVisible: false, isPositive: false);
				}
				else if (list3[num3][list[0]].power < skillPower)
				{
					list3[num3][list[0]].power = skillPower;
					list3[num3][list[0]].continutyTurn = battleSkillData.skillContinuity;
					utageBattleSceneManager.SetBuffIcon(text, num4, targetForce: true, setVisible: true, isPositive: true);
					Debug.Log("今回のスキルの方が強い");
				}
				else
				{
					list3[num3][list[0]].continutyTurn = battleSkillData.skillContinuity;
					utageBattleSceneManager.SetBuffIcon(text, num4, targetForce: true, setVisible: true, isPositive: true);
					Debug.Log("今回のスキルの方が弱い");
				}
			}
			else
			{
				AddNewBuffData("defense", skillPower, list3, targetForce: true, num3, num4);
			}
			list2 = (from ano in list3[num3].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "magicDefense"
				select ano.Index).ToList();
			if (list3[num3].Any((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == "magicDefense"))
			{
				text = "magicDefense";
				if (list3[num3][list2[0]].power < 0)
				{
					int index5 = list2[0];
					list3[num3].RemoveAt(index5);
					Debug.Log("魔防デバフ無効化");
					utageBattleSceneManager.SetBuffIcon(text, num4, targetForce: true, setVisible: false, isPositive: false);
				}
				else if (list3[num3][list2[0]].power < skillPower)
				{
					list3[num3][list2[0]].power = skillPower;
					list3[num3][list2[0]].continutyTurn = battleSkillData.skillContinuity;
					utageBattleSceneManager.SetBuffIcon(text, num4, targetForce: true, setVisible: true, isPositive: true);
					Debug.Log("今回のスキルの方が強い");
				}
				else
				{
					list3[num3][list2[0]].continutyTurn = battleSkillData.skillContinuity;
					utageBattleSceneManager.SetBuffIcon(text, num4, targetForce: true, setVisible: true, isPositive: true);
					Debug.Log("今回のスキルの方が弱い");
				}
			}
			else
			{
				AddNewBuffData("magicDefense", skillPower, list3, targetForce: true, num3, num4);
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
		utageBattleSceneManager.SetBuffIcon("defense", num, targetForce, setVisible: true, isPositive: true);
		utageBattleSceneManager.SetBuffIcon("magicDefense", num, targetForce, setVisible: true, isPositive: true);
		Debug.Log("バフデータ追加／num：" + num + "／味方陣営：" + targetForce);
	}
}
