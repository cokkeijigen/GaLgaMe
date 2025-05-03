using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAttackCount : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public StateLink trueLink;

	public StateLink falseLink;

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
		scenarioBattleTurnManager.battleUseSkillID = 0;
		scenarioBattleTurnManager.isAllTargetSkill = false;
		for (int i = 0; i < scenarioBattleTurnManager.factorEffectSuccessList.Count; i++)
		{
			scenarioBattleTurnManager.factorEffectSuccessList[i] = false;
		}
		switch (type)
		{
		case Type.player:
			scenarioBattleTurnManager.playerAttackCount++;
			flag = ((scenarioBattleTurnManager.playerAttackCount >= PlayerBattleConditionManager.playerIsDead.Count) ? true : false);
			break;
		case Type.enemy:
			scenarioBattleTurnManager.enemyAttackCount++;
			flag = ((scenarioBattleTurnManager.enemyAttackCount >= PlayerBattleConditionManager.enemyIsDead.Count) ? true : false);
			break;
		}
		if (flag)
		{
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
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
