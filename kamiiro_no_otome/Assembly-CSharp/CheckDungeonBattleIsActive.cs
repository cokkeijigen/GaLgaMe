using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBattleIsActive : StateBehaviour
{
	private DungeonMapStatusManager dungeonMapStatusManager;

	private DungeonMapManager dungeonMapManager;

	public StateLink mapLink;

	public StateLink battleLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		if (dungeonMapManager.dungeonBattleCanvas.activeInHierarchy)
		{
			Transition(battleLink);
			return;
		}
		dungeonMapStatusManager.isPlayerStatusViewSetUp.Clear();
		Transition(mapLink);
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
