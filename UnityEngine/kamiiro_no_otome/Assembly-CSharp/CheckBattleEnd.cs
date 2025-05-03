using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckBattleEnd : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	public Type type;

	private List<PlayerBattleConditionManager.MemberIsDead> checkResult;

	public StateLink continueLink;

	public StateLink battleEndLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.player:
		{
			ScenarioBattleData battleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			if (battleData.supportBattleCharacterID != int.MaxValue && battleData.isSupportCharacterTakeDamage)
			{
				List<PlayerBattleConditionManager.MemberIsDead> list = new List<PlayerBattleConditionManager.MemberIsDead>(PlayerBattleConditionManager.playerIsDead);
				int index = list.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == battleData.supportBattleCharacterID);
				list.RemoveAt(index);
				checkResult = list.FindAll((PlayerBattleConditionManager.MemberIsDead data) => !data.isDead);
				if (checkResult != null && checkResult.Count > 0)
				{
					Transition(continueLink);
				}
				else
				{
					Transition(battleEndLink);
				}
			}
			else
			{
				checkResult = PlayerBattleConditionManager.playerIsDead.FindAll((PlayerBattleConditionManager.MemberIsDead data) => !data.isDead);
				if (checkResult != null && checkResult.Count > 0)
				{
					Transition(continueLink);
				}
				else
				{
					Transition(battleEndLink);
				}
			}
			break;
		}
		case Type.enemy:
			checkResult = PlayerBattleConditionManager.enemyIsDead.FindAll((PlayerBattleConditionManager.MemberIsDead data) => !data.isDead);
			if (checkResult != null && checkResult.Count > 0)
			{
				Transition(continueLink);
			}
			else
			{
				Transition(battleEndLink);
			}
			break;
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
