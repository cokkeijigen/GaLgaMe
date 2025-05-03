using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcBadStatePower : StateBehaviour
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
		string badStateType = battleSkillData.subType.ToString();
		List<List<PlayerBattleConditionManager.MemberBadState>> list = new List<List<PlayerBattleConditionManager.MemberBadState>>();
		List<PlayerBattleConditionManager.MemberIsDead> list2 = new List<PlayerBattleConditionManager.MemberIsDead>();
		bool targetForce;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			list = PlayerBattleConditionManager.enemyBadState;
			list2 = PlayerBattleConditionManager.enemyIsDead;
			targetForce = false;
		}
		else
		{
			list = PlayerBattleConditionManager.playerBadState;
			list2 = PlayerBattleConditionManager.playerIsDead;
			targetForce = true;
		}
		if (battleSkillData.subType == BattleSkillData.SubType.poison || battleSkillData.subType == BattleSkillData.SubType.paralyze)
		{
			int i;
			for (i = 0; i < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; i++)
			{
				if (!list2.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.skillAttackHitDataSubList[i].memberNum).isDead)
				{
					int num = scenarioBattleTurnManager.skillAttackHitDataSubList[i].memberNum;
					int index = list2.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == num);
					PlayerBattleConditionManager.MemberBadState memberBadState = list[index].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == battleSkillData.subType.ToString());
					if (memberBadState != null)
					{
						memberBadState.continutyTurn = battleSkillData.skillContinuity;
						utageBattleSceneManager.SetBadStateIcon(badStateType, num, targetForce, setVisible: true);
						Debug.Log("状態異常ターン増加／num" + num + "／味方：" + targetForce);
						continue;
					}
					PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
					{
						type = battleSkillData.subType.ToString(),
						continutyTurn = battleSkillData.skillContinuity
					};
					list[index].Add(item);
					utageBattleSceneManager.SetBadStateIcon(badStateType, num, targetForce, setVisible: true);
					Debug.Log("状態異常データ追加／num" + num + "／味方：" + targetForce);
				}
				else
				{
					Debug.Log("状態異常対象は戦闘不能／ナンバー：" + scenarioBattleTurnManager.skillAttackHitDataSubList[i].memberNum);
				}
			}
		}
		else if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			if (scenarioBattleTurnManager.playerSkillData.skillTarget == BattleSkillData.SkillTarget.all)
			{
				for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; j++)
				{
					int memberNum = scenarioBattleTurnManager.skillAttackHitDataSubList[j].memberNum;
					float num2 = PlayerStatusDataManager.enemyChargeTurnList[memberNum];
					if (num2 != 0f)
					{
						num2 /= 2f;
						num2 = Mathf.FloorToInt(num2);
						PlayerStatusDataManager.enemyChargeTurnList[memberNum] = (int)num2;
						utageBattleSceneManager.enemyButtonGoList[memberNum].GetComponent<ArborFSM>().SendTrigger("ResetEnemyCharge");
						Debug.Log($"よろけ／対象Num：{memberNum}／チャージターン数：{num2}");
					}
				}
			}
			else
			{
				float num3 = PlayerStatusDataManager.enemyChargeTurnList[scenarioBattleTurnManager.playerTargetNum];
				if (num3 != 0f)
				{
					num3 /= 2f;
					num3 = Mathf.FloorToInt(num3);
					PlayerStatusDataManager.enemyChargeTurnList[scenarioBattleTurnManager.playerTargetNum] = (int)num3;
					utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<ArborFSM>().SendTrigger("ResetEnemyCharge");
					Debug.Log($"よろけ／対象Num：{scenarioBattleTurnManager.playerTargetNum}／チャージターン数：{num3}");
				}
			}
		}
		else if (scenarioBattleTurnManager.enemySkillData.skillTarget == BattleSkillData.SkillTarget.all)
		{
			for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; k++)
			{
				int memberNum2 = scenarioBattleTurnManager.skillAttackHitDataSubList[k].memberNum;
				float num4 = PlayerStatusDataManager.characterSp[memberNum2];
				if (num4 != 0f)
				{
					num4 /= 2f;
					num4 = Mathf.FloorToInt(num4);
					PlayerStatusDataManager.characterSp[memberNum2] = (int)num4;
					utageBattleSceneManager.playerFrameGoList[memberNum2].GetComponent<ArborFSM>().SendTrigger("ConsumeCharacterSp");
					Debug.Log($"よろけ／対象Num：{memberNum2}／SP：{num4}");
				}
			}
		}
		else
		{
			float num5 = PlayerStatusDataManager.characterSp[scenarioBattleTurnManager.enemyTargetNum];
			if (num5 != 0f)
			{
				num5 /= 2f;
				num5 = Mathf.FloorToInt(num5);
				PlayerStatusDataManager.characterSp[scenarioBattleTurnManager.enemyTargetNum] = (int)num5;
				utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].GetComponent<ArborFSM>().SendTrigger("ConsumeCharacterSp");
				Debug.Log($"よろけ／対象Num：{scenarioBattleTurnManager.enemyTargetNum}／SP：{num5}");
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
}
