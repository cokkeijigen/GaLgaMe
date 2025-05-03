using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcFactorBadStatePower : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

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
		int playerTargetNum = scenarioBattleTurnManager.playerTargetNum;
		if (scenarioBattleTurnManager.factorEffectSuccessList[0])
		{
			PlayerBattleConditionManager.MemberBadState memberBadState = PlayerBattleConditionManager.enemyBadState[playerTargetNum].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison");
			if (memberBadState != null)
			{
				memberBadState.continutyTurn = 3;
				Debug.Log("毒ターン増加");
			}
			else
			{
				PlayerBattleConditionManager.MemberBadState item = new PlayerBattleConditionManager.MemberBadState
				{
					type = "poison",
					continutyTurn = 3
				};
				PlayerBattleConditionManager.enemyBadState[playerTargetNum].Add(item);
				utageBattleSceneManager.SetBadStateIcon("poison", playerTargetNum, targetForce: false, setVisible: true);
				Debug.Log("毒データ追加");
			}
		}
		if (scenarioBattleTurnManager.factorEffectSuccessList[1])
		{
			PlayerBattleConditionManager.MemberBadState memberBadState2 = PlayerBattleConditionManager.enemyBadState[playerTargetNum].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze");
			if (memberBadState2 != null)
			{
				memberBadState2.continutyTurn = 3;
				Debug.Log("麻痺ターン増加");
			}
			else
			{
				PlayerBattleConditionManager.MemberBadState item2 = new PlayerBattleConditionManager.MemberBadState
				{
					type = "paralyze",
					continutyTurn = 3
				};
				PlayerBattleConditionManager.enemyBadState[playerTargetNum].Add(item2);
				utageBattleSceneManager.SetBadStateIcon("paralyze", playerTargetNum, targetForce: false, setVisible: true);
				Debug.Log("麻痺データ追加");
			}
		}
		if (scenarioBattleTurnManager.factorEffectSuccessList[2])
		{
			float num = PlayerStatusDataManager.enemyChargeTurnList[scenarioBattleTurnManager.playerTargetNum];
			if (num != 0f)
			{
				num /= 2f;
				num = Mathf.FloorToInt(num);
				PlayerStatusDataManager.enemyChargeTurnList[scenarioBattleTurnManager.playerTargetNum] = (int)num;
				utageBattleSceneManager.enemyButtonGoList[scenarioBattleTurnManager.playerTargetNum].GetComponent<ArborFSM>().SendTrigger("ResetEnemyCharge");
				Debug.Log($"崩し成功／対象Num：{scenarioBattleTurnManager.playerTargetNum}／チャージターン数：{num}");
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
