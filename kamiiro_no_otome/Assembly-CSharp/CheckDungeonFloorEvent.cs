using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonFloorEvent : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponentInParent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		string name = PlayerDataManager.currentDungeonName;
		if (GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == name).dungeonEventFloorList.Any((int data) => data == dungeonMapManager.dungeonCurrentFloorNum))
		{
			string scnearioName = PlayerDungeonScenarioDataManager.CheckDungeonFloorEvent(name, dungeonMapManager.dungeonCurrentFloorNum);
			if (!string.IsNullOrEmpty(scnearioName))
			{
				ScenarioFlagData scenarioFlagData = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == scnearioName);
				if (dungeonMapManager.dungeonCurrentFloorNum == dungeonMapManager.dungeonMaxFloorNum)
				{
					if (scenarioFlagData.isSexEvent)
					{
						PlayerNonSaveDataManager.selectScenarioName = scnearioName;
						dungeonMapManager.mapFSM.SendTrigger("StartDungeonFloorEvent");
					}
					else
					{
						PlayerNonSaveDataManager.selectScenarioName = scnearioName;
						dungeonMapManager.mapFSM.SendTrigger("StartDungeonFloorEvent");
					}
				}
				else
				{
					PlayerNonSaveDataManager.selectScenarioName = scnearioName;
					dungeonMapManager.mapFSM.SendTrigger("StartDungeonFloorEvent");
				}
			}
			else
			{
				dungeonMapManager.isSexFloorEventEnable = false;
				ClearMiniCardList();
				Transition(stateLink);
			}
		}
		else
		{
			dungeonMapManager.isSexFloorEventEnable = false;
			Debug.Log("ダンジョンイベントなし");
			ClearMiniCardList();
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

	private void ClearMiniCardList()
	{
		for (int i = 0; i < dungeonMapManager.miniCardList.Count; i++)
		{
			GameObject gameObject = dungeonMapManager.miniCardList[i].gameObject;
			PoolManager.Pools["DungeonObject"].Despawn(gameObject.transform, dungeonMapManager.poolManagerGO);
		}
		dungeonMapManager.miniCardList.Clear();
	}
}
