using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonScenarioClear : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	public StateLink scenarioLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		if (dungeonMapManager.isBossRouteSelect)
		{
			PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentDungeonName + "_BossEvent";
			Transition(scenarioLink);
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
}
