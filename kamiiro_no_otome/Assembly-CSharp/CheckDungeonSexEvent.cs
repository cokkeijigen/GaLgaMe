using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonSexEvent : StateBehaviour
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
		GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == name);
		if (PlayerDataManager.playerLibido >= 100)
		{
			string text = PlayerDungeonScenarioDataManager.CheckDungeonSexEvent(name, dungeonMapManager.dungeonCurrentFloorNum);
			if (!string.IsNullOrEmpty(text))
			{
				dungeonMapManager.isSexLibidoEventEnable = true;
				PlayerNonSaveDataManager.dungeonEventScenarioName = text;
				Transition(stateLink);
			}
			else
			{
				dungeonMapManager.isSexLibidoEventEnable = false;
				Debug.Log("イチャイチャイベントなし");
				ClearMiniCardList();
				Transition(stateLink);
			}
		}
		else
		{
			dungeonMapManager.isSexLibidoEventEnable = false;
			Debug.Log("ダンジョン／性欲100未満");
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
