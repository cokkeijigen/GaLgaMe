using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAttackParalyze : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public int paralyzeProbability;

	public Type type;

	public StateLink stateLink;

	public StateLink paralyzeLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		int num = Random.Range(0, 100);
		bool flag = false;
		Debug.Log("麻痺判定：" + num + "/ 50");
		switch (type)
		{
		case Type.player:
		{
			int playerAttackCount = scenarioBattleTurnManager.playerAttackCount;
			int battleBadState = PlayerBattleConditionAccess.GetBattleBadState(PlayerBattleConditionManager.playerBadState[playerAttackCount], "paralyze");
			Debug.Log("プレイヤーの行動順：" + scenarioBattleTurnManager.playerAttackCount + "／num：" + playerAttackCount + "／麻痺ターン：" + battleBadState);
			if (battleBadState > 0 && num <= paralyzeProbability)
			{
				flag = true;
			}
			break;
		}
		case Type.enemy:
		{
			int enemyAttackCount = scenarioBattleTurnManager.enemyAttackCount;
			int battleBadState = PlayerBattleConditionAccess.GetBattleBadState(PlayerBattleConditionManager.enemyBadState[enemyAttackCount], "paralyze");
			Debug.Log("敵の行動順：" + scenarioBattleTurnManager.enemyAttackCount + "／num：" + enemyAttackCount + "／麻痺ターン：" + battleBadState);
			if (battleBadState > 0 && num <= paralyzeProbability)
			{
				flag = true;
			}
			break;
		}
		}
		if (flag)
		{
			Transition(paralyzeLink);
		}
		else
		{
			Transition(stateLink);
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
