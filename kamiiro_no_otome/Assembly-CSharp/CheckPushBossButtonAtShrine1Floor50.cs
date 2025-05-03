using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckPushBossButtonAtShrine1Floor50 : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	public StateLink disableBossButtonLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData item) => item.dungeonName == PlayerDataManager.currentDungeonName);
		if (PlayerDataManager.currentDungeonName == "Shrine1" && dungeonMapManager.dungeonCurrentFloorNum == 50)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary["MH_Levy_019"])
			{
				Transition(stateLink);
			}
			else
			{
				Transition(disableBossButtonLink);
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
}
