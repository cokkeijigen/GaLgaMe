using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDeathFactorHit : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private int deathSuccessRate;

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
		for (int i = 0; i < scenarioBattleTurnManager.factorEffectSuccessList.Count; i++)
		{
			scenarioBattleTurnManager.factorEffectSuccessList[i] = false;
		}
		int memberID = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID;
		deathSuccessRate = PlayerEquipDataManager.equipFactorDeath[memberID];
		if (deathSuccessRate == 0)
		{
			Debug.Log("即死ファクター効果なし");
		}
		else
		{
			int num = PlayerStatusDataManager.enemyResist[scenarioBattleTurnManager.playerTargetNum];
			if (PlayerStatusDataManager.enemyDeathResist[scenarioBattleTurnManager.playerTargetNum])
			{
				deathSuccessRate = 0;
			}
			else
			{
				deathSuccessRate -= num;
				deathSuccessRate = Mathf.Clamp(deathSuccessRate, 0, 100);
			}
			int num2 = Random.Range(0, 100);
			Debug.Log($"ファクター命中率：{deathSuccessRate}／命中ランダム：{num2}");
			if (deathSuccessRate >= num2)
			{
				scenarioBattleTurnManager.factorEffectSuccessList[3] = true;
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
