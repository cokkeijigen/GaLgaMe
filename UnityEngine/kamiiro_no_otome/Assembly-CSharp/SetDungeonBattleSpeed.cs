using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonBattleSpeed : StateBehaviour
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
		switch (PlayerDataManager.dungeonBattleSpeed)
		{
		case 1:
			PlayerDataManager.dungeonBattleSpeed = 2;
			dungeonBattleManager.speedTmpGO.text = "2";
			break;
		case 2:
			PlayerDataManager.dungeonBattleSpeed = 4;
			dungeonBattleManager.speedTmpGO.text = "4";
			break;
		case 4:
			PlayerDataManager.dungeonBattleSpeed = 1;
			dungeonBattleManager.speedTmpGO.text = "1";
			break;
		default:
			PlayerDataManager.dungeonBattleSpeed = 1;
			dungeonBattleManager.speedTmpGO.text = "1";
			break;
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
