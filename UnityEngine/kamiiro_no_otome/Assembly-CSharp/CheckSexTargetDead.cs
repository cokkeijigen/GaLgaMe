using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexTargetDead : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleEffectManager sexBattleEffectManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public Type type;

	public StateLink aliveLink;

	public StateLink deathLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		new List<int>().Clear();
		switch (type)
		{
		case Type.player:
			if (PlayerSexStatusDataManager.playerSexHp[0] <= 0)
			{
				PlayerSexBattleConditionAccess.ResetSexBuffCondtion("player");
				PlayerSexBattleConditionAccess.ResetSexSubPower("player");
				sexBattleEffectManager.ResetBuffAndSubPowerIcon(targetIsHeroine: false);
				flag = true;
			}
			break;
		case Type.heroine:
		{
			int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
			if (PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId] <= 0)
			{
				PlayerSexBattleConditionAccess.ResetSexBuffCondtion("heroine");
				PlayerSexBattleConditionAccess.ResetSexSubPower("heroine");
				sexBattleEffectManager.ResetBuffAndSubPowerIcon(targetIsHeroine: true);
				flag = true;
			}
			break;
		}
		}
		if (flag)
		{
			Debug.Log("誰かが死んでいる");
			Transition(deathLink);
		}
		else
		{
			Debug.Log("誰も死んでいない");
			Transition(aliveLink);
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

	private void ResetBuffIcon(ParameterContainer parameterContainer)
	{
		foreach (GameObject gameObject in parameterContainer.GetGameObjectList("buffImageGoList"))
		{
			gameObject.SetActive(value: false);
		}
		foreach (GameObject gameObject2 in parameterContainer.GetGameObjectList("badStateImageGoList"))
		{
			gameObject2.SetActive(value: false);
		}
	}
}
