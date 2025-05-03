using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendStartAgility : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		foreach (GameObject playerAgilityGo in dungeonBattleManager.playerAgilityGoList)
		{
			playerAgilityGo.GetComponent<ArborFSM>().SendTrigger("StartAgility");
		}
		foreach (GameObject enemyAgilityGo in dungeonBattleManager.enemyAgilityGoList)
		{
			enemyAgilityGo.GetComponent<ArborFSM>().SendTrigger("StartAgility");
		}
		dungeonBattleManager.itemWaitSlider.GetComponent<DungeonCharacterAgility>().isCoroutineStop = false;
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
