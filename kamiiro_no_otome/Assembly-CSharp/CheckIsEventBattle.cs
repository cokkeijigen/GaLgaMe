using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckIsEventBattle : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public float waitTime;

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
		if (utageBattleSceneManager.isEventBattle)
		{
			if (scenarioBattleTurnManager.elapsedTurnCount >= 2)
			{
				Debug.Log("イベントバトル終了");
				utageBattleSceneManager.battleCanvas.SetActive(value: false);
				PlayerNonSaveDataManager.isScenarioBattle = false;
				for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					PlayerStatusDataManager.characterHp[PlayerStatusDataManager.playerPartyMember[i]] = PlayerStatusDataManager.characterMaxHp[PlayerStatusDataManager.playerPartyMember[i]];
					PlayerStatusDataManager.characterMp[PlayerStatusDataManager.playerPartyMember[i]] = PlayerStatusDataManager.characterMaxMp[PlayerStatusDataManager.playerPartyMember[i]];
				}
				Invoke("InvokeMethod", waitTime);
			}
			else
			{
				Transition(stateLink);
			}
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

	private void InvokeMethod()
	{
		GameObject.Find("Battle Result Manager").GetComponent<ArborFSM>().SendTrigger("ScenairoBattleResultEnd");
	}
}
