using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAttackCharacterAlive : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public StateLink ignoreLink;

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
		bool flag = false;
		Type type = this.type;
		if (type != 0)
		{
			if (type == Type.enemy)
			{
				while (PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].isDead)
				{
					scenarioBattleTurnManager.enemyAttackCount++;
					if (scenarioBattleTurnManager.enemyAttackCount >= PlayerBattleConditionManager.enemyIsDead.Count)
					{
						flag = true;
						break;
					}
				}
			}
		}
		else
		{
			while (PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].isDead)
			{
				scenarioBattleTurnManager.playerAttackCount++;
				if (scenarioBattleTurnManager.playerAttackCount >= PlayerBattleConditionManager.playerIsDead.Count)
				{
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			Transition(ignoreLink);
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
