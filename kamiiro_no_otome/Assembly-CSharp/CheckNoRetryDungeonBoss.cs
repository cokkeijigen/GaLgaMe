using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckNoRetryDungeonBoss : StateBehaviour
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
		if (GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData item) => item.dungeonName == PlayerDataManager.currentDungeonName).dungeonBossNoRetryFloorList.Contains(dungeonMapManager.dungeonCurrentFloorNum))
		{
			PlayerNonSaveDataManager.isDungeonNoRetryBossBattle = true;
		}
		else
		{
			PlayerNonSaveDataManager.isDungeonNoRetryBossBattle = false;
		}
		if (PlayerNonSaveDataManager.isDungeonNoRetryBossBattle)
		{
			GameObject.Find("Dungeon Dialog Manager").GetComponent<ArborFSM>().SendTrigger("OpenNoRetryDialog");
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
