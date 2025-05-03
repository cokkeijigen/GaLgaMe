using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBossCommandBattle : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponentInParent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.currentDungeonName == "Shrine1" && dungeonMapManager.dungeonCurrentFloorNum == 50)
		{
			PlayerNonSaveDataManager.selectScenarioName = "Shrine1_50BossBattle";
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
