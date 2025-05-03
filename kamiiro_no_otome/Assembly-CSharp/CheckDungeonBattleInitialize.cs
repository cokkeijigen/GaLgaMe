using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBattleInitialize : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private bool isCharacterSetUp;

	private bool isEnemySetUp;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		foreach (GameObject playerAgilityGo in dungeonBattleManager.playerAgilityGoList)
		{
			playerAgilityGo.GetComponent<ArborFSM>().SendTrigger("InitializePlayerAgility");
		}
		foreach (GameObject enemyAgilityGo in dungeonBattleManager.enemyAgilityGoList)
		{
			enemyAgilityGo.GetComponent<ArborFSM>().SendTrigger("InitializeEnemyAgility");
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		isCharacterSetUp = dungeonBattleManager.isCharacterAgilitySetUp.All((bool x) => x);
		isEnemySetUp = dungeonBattleManager.isEnemyAgilitySetUp.All((bool y) => y);
		if (isCharacterSetUp && isEnemySetUp)
		{
			Debug.Log("ダンジョン戦闘の初期化完了");
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
