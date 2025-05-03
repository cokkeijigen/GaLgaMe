using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonMoveSpeed : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		switch (PlayerDataManager.dungeonMoveSpeed)
		{
		case 1:
			PlayerDataManager.dungeonMoveSpeed = 2;
			dungeonMapManager.speedTmpGO.text = "2";
			break;
		case 2:
			PlayerDataManager.dungeonMoveSpeed = 4;
			dungeonMapManager.speedTmpGO.text = "4";
			break;
		case 4:
			PlayerDataManager.dungeonMoveSpeed = 1;
			dungeonMapManager.speedTmpGO.text = "1";
			break;
		default:
			PlayerDataManager.dungeonMoveSpeed = 1;
			dungeonMapManager.speedTmpGO.text = "1";
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
