using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckScenarioBattleChargeTutorial : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isChargeAttackTutorial)
		{
			float num = (float)GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName).enemyHpRate / 100f;
			for (int i = 0; i < PlayerStatusDataManager.enemyHp.Count; i++)
			{
				int value = Mathf.FloorToInt((float)PlayerStatusDataManager.enemyHp[i] * num);
				PlayerStatusDataManager.enemyHp[i] = value;
			}
			for (int j = 0; j < utageBattleSceneManager.enemyButtonGoList.Count; j++)
			{
				utageBattleSceneManager.enemyButtonGoList[j].GetComponent<ArborFSM>().SendTrigger("ResetEnemyButton");
			}
			PlayerStatusDataManager.characterSp[1] = 100;
			for (int k = 0; k < utageBattleSceneManager.playerFrameGoList.Count; k++)
			{
				utageBattleSceneManager.playerFrameGoList[k].GetComponent<ArborFSM>().SendTrigger("ResetCharacterFrame");
			}
			Debug.Log("チャージ攻撃のチュートリアル予約がある");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("チャージ攻撃のチュートリアル予約なし");
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
