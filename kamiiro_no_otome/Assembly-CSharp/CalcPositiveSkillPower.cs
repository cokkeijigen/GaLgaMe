using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcPositiveSkillPower : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private BattleSkillData battleSkillData;

	public StateLink buffLink;

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
		scenarioBattleTurnManager.isMedicineNoTarget = false;
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		string text = battleSkillData.skillType.ToString();
		switch (battleSkillData.skillTarget)
		{
		case BattleSkillData.SkillTarget.solo:
		case BattleSkillData.SkillTarget.self:
			scenarioBattleTurnManager.isAllTargetSkill = false;
			break;
		case BattleSkillData.SkillTarget.all:
			scenarioBattleTurnManager.isAllTargetSkill = true;
			break;
		}
		switch (text)
		{
		case "buff":
		case "deSpell":
		case "regeneration":
			Transition(buffLink);
			break;
		case "heal":
			CalcHealSkill();
			break;
		case "medic":
			CalcMedicSkill();
			break;
		case "revive":
			CalcReviveSkill();
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

	private void CalcHealSkill()
	{
		int num;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == scenarioBattleTurnManager.useSkillPartyMemberID).memberNum;
			num = PlayerStatusDataManager.characterMagicAttack[scenarioBattleTurnManager.useSkillPartyMemberID] + 100;
			num += PlayerEquipDataManager.accessorySkillPowerUp[scenarioBattleTurnManager.useSkillPartyMemberID];
			num = battleSkillData.skillPower * (num / 100);
			int battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[memberNum], "magicAttack");
			float buffPower = GetBuffPower(battleBuffCondition);
			num = Mathf.RoundToInt((float)num * buffPower);
			float num2;
			int num3;
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					num2 = Random.Range(0.9f, 1.1f);
					num3 = Mathf.FloorToInt((float)num * num2);
					bool flag = false;
					int num4 = PlayerEquipDataManager.accessorySkillCritical[scenarioBattleTurnManager.useSkillPartyMemberID];
					if (num4 > 0)
					{
						int num5 = Random.Range(0, 100);
						if (num4 >= num5)
						{
							flag = true;
						}
					}
					if (flag)
					{
						num3 *= 2;
					}
					int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
					int memberNum2 = PlayerBattleConditionManager.playerIsDead[i].memberNum;
					if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).isDead)
					{
						SkillAttackHitData item = new SkillAttackHitData
						{
							isHealHit = true,
							memberID = id,
							memberNum = memberNum2,
							healValue = num3
						};
						scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
					}
				}
				return;
			}
			num2 = Random.Range(0.9f, 1.1f);
			num3 = Mathf.FloorToInt((float)num * num2);
			bool flag2 = false;
			int num6 = PlayerEquipDataManager.accessorySkillCritical[scenarioBattleTurnManager.useSkillPartyMemberID];
			if (num6 > 0)
			{
				int num7 = Random.Range(0, 100);
				if (num6 >= num7)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				num3 *= 2;
			}
			int num8 = scenarioBattleTurnManager.playerTargetNum;
			int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == num8).memberID;
			SkillAttackHitData item2 = new SkillAttackHitData
			{
				isHealHit = true,
				memberID = memberID,
				memberNum = num8,
				healValue = num3
			};
			scenarioBattleTurnManager.skillAttackHitDataList.Add(item2);
			return;
		}
		num = PlayerStatusDataManager.enemyMagicAttack[scenarioBattleTurnManager.useSkillEnemyMemberNumList[scenarioBattleTurnManager.enemyAttackCount]] + 100;
		num = battleSkillData.skillPower * (num / 100);
		scenarioBattleTurnManager.enemyTargetNum = Random.Range(0, PlayerStatusDataManager.enemyMember.Length - 1);
		int enemyTargetNum = scenarioBattleTurnManager.enemyTargetNum;
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			int j;
			for (j = 0; j < PlayerStatusDataManager.enemyMember.Length; j++)
			{
				float num2 = Random.Range(0.9f, 1.1f);
				int num3 = Mathf.FloorToInt((float)num * num2);
				if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
				{
					SkillAttackHitData item3 = new SkillAttackHitData
					{
						memberNum = j,
						healValue = num3
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item3);
				}
			}
		}
		else
		{
			float num2 = Random.Range(0.9f, 1.1f);
			int num3 = Mathf.FloorToInt((float)num * num2);
			SkillAttackHitData item4 = new SkillAttackHitData
			{
				memberNum = enemyTargetNum,
				healValue = num3
			};
			scenarioBattleTurnManager.skillAttackHitDataList.Add(item4);
		}
	}

	private void CalcMedicSkill()
	{
		List<int> list = new List<int>();
		switch (battleSkillData.skillPower)
		{
		case 0:
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				for (int j = 0; j < PlayerBattleConditionManager.playerBadState.Count; j++)
				{
					int id = PlayerBattleConditionManager.playerIsDead[j].memberID;
					int num2 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id);
					list = (from ano in PlayerBattleConditionManager.playerBadState[j].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "poison"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[j][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("poison", num2, targetForce: true, setVisible: false);
					}
				}
				break;
			}
			int num3 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			Debug.Log("毒回復／単体Index：" + num3);
			if (PlayerBattleConditionManager.playerBadState[num3].Count > 0)
			{
				if (PlayerBattleConditionManager.playerBadState[num3].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[num3].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
					utageBattleSceneManager.SetBadStateIcon("poison", num3, targetForce: true, setVisible: false);
					Debug.Log("毒回復完了／単体Index：" + num3);
				}
				else
				{
					scenarioBattleTurnManager.isMedicineNoTarget = true;
				}
			}
			else
			{
				scenarioBattleTurnManager.isMedicineNoTarget = true;
			}
			break;
		}
		case 1:
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				for (int k = 0; k < PlayerBattleConditionManager.playerBadState.Count; k++)
				{
					int id2 = PlayerBattleConditionManager.playerIsDead[k].memberID;
					int num4 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id2);
					list = (from ano in PlayerBattleConditionManager.playerBadState[k].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "paralyze"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[k][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("paralyze", num4, targetForce: true, setVisible: false);
					}
				}
				break;
			}
			int num5 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			Debug.Log("麻痺回復／単体Index：" + num5);
			if (PlayerBattleConditionManager.playerBadState[num5].Count > 0)
			{
				if (PlayerBattleConditionManager.playerBadState[num5].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[num5].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
					utageBattleSceneManager.SetBadStateIcon("paralyze", num5, targetForce: true, setVisible: false);
					Debug.Log("麻痺回復完了／単体Index：" + num5);
				}
				else
				{
					scenarioBattleTurnManager.isMedicineNoTarget = true;
				}
			}
			else
			{
				scenarioBattleTurnManager.isMedicineNoTarget = true;
			}
			break;
		}
		case 2:
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				for (int i = 0; i < PlayerBattleConditionManager.playerBadState.Count; i++)
				{
					list = (from ano in PlayerBattleConditionManager.playerBadState[i].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "poison"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[i][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("poison", i, targetForce: true, setVisible: false);
					}
					list = (from ano in PlayerBattleConditionManager.playerBadState[i].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "paralyze"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[i][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("paralyze", i, targetForce: true, setVisible: false);
					}
				}
				break;
			}
			int num = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			if (PlayerBattleConditionManager.playerBadState[num].Count > 0)
			{
				PlayerBattleConditionManager.MemberBadState memberBadState = PlayerBattleConditionManager.playerBadState[num].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison");
				if (memberBadState != null && PlayerBattleConditionManager.playerBadState[num].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[num].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
					utageBattleSceneManager.SetBadStateIcon("poison", num, targetForce: true, setVisible: false);
				}
				PlayerBattleConditionManager.MemberBadState memberBadState2 = PlayerBattleConditionManager.playerBadState[num].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze");
				if (memberBadState2 != null && PlayerBattleConditionManager.playerBadState[num].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn > 0)
				{
					PlayerBattleConditionManager.playerBadState[num].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
					utageBattleSceneManager.SetBadStateIcon("paralyze", scenarioBattleTurnManager.playerTargetNum, targetForce: true, setVisible: false);
				}
				if (memberBadState == null && memberBadState2 == null)
				{
					scenarioBattleTurnManager.isMedicineNoTarget = true;
				}
			}
			else
			{
				scenarioBattleTurnManager.isMedicineNoTarget = true;
			}
			break;
		}
		}
	}

	private void CalcReviveSkill()
	{
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			int i;
			for (i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
			{
				if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == i).isDead)
				{
					PlayerBattleConditionManager.playerIsDead[i].isDead = false;
					int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
					int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
					int healValue = PlayerStatusDataManager.characterMaxHp[id] * (battleSkillData.skillPower / 100);
					SkillAttackHitData item = new SkillAttackHitData
					{
						isHealHit = true,
						memberID = id,
						memberNum = memberNum,
						healValue = healValue
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
				}
				scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[i], isEnable: true, i);
				utageBattleSceneManager.SetBadStateIcon("death", i, targetForce: true, setVisible: false);
			}
		}
		else
		{
			int playerTargetNum = scenarioBattleTurnManager.playerTargetNum;
			int id2 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
			PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id2).isDead = false;
			float num = (float)battleSkillData.skillPower / 100f;
			int healValue = Mathf.FloorToInt((float)PlayerStatusDataManager.characterMaxHp[id2] * num);
			Debug.Log("蘇生後のHP：" + healValue + "／倍率：" + num);
			SkillAttackHitData item2 = new SkillAttackHitData
			{
				isHealHit = true,
				memberID = id2,
				memberNum = playerTargetNum,
				healValue = healValue
			};
			scenarioBattleTurnManager.skillAttackHitDataList.Add(item2);
			scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[playerTargetNum], isEnable: true, playerTargetNum);
			utageBattleSceneManager.SetBadStateIcon("death", scenarioBattleTurnManager.playerTargetNum, targetForce: true, setVisible: false);
		}
	}

	private float GetBuffPower(int num)
	{
		return num / 100 + 1;
	}
}
