using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class BackUpDungeonMapData : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.backUpMiniCardList = new List<DungeonSelectCardData>(dungeonMapManager.miniCardList);
		PlayerNonSaveDataManager.backUpGetDropItemDictionary = new Dictionary<int, int>(dungeonMapManager.getDropItemDictionary);
		PlayerNonSaveDataManager.backUpGetDropMoney = dungeonMapManager.getDropMoney;
		PlayerNonSaveDataManager.backUpDungeonCurrentFloorNum = dungeonMapManager.dungeonCurrentFloorNum;
		PlayerNonSaveDataManager.backUpCurrentBorderNum = dungeonMapManager.currentBorderNum;
		PlayerNonSaveDataManager.backUpIsBossRoute = dungeonMapManager.isBossRouteSelect;
		PlayerNonSaveDataManager.backUpPlayerLibido = PlayerDataManager.playerLibido;
		PlayerNonSaveDataManager.backUpPlayerAllHp = PlayerStatusDataManager.playerAllHp;
		PlayerNonSaveDataManager.backUpDungeonBuffAttack = dungeonMapStatusManager.dungeonBuffAttack;
		PlayerNonSaveDataManager.backUpDungeonBuffDefense = dungeonMapStatusManager.dungeonBuffDefense;
		PlayerNonSaveDataManager.backUpDungeonBuffRetreat = dungeonMapStatusManager.dungeonBuffRetreat;
		PlayerNonSaveDataManager.backUpDungeonDeBuffAgiityRemainFloor = dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor;
		PlayerNonSaveDataManager.backUpDungeonDeBuffAgility = dungeonMapStatusManager.dungeonDeBuffAgility;
		for (int i = 0; i < dungeonMapManager.miniCardList.Count; i++)
		{
			GameObject gameObject = dungeonMapManager.miniCardList[i].gameObject;
			PoolManager.Pools["DungeonObject"].Despawn(gameObject.transform, dungeonMapManager.poolManagerGO);
		}
		dungeonMapManager.miniCardList.Clear();
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
