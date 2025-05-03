using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CheckRouteAnimationEnd : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapEffectManager dungeonMapEffectManager;

	private DungeonRouteAnimationManager dungeonRouteAnimationManager;

	private string subType;

	public StateLink battleLink;

	public StateLink bossLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponent<DungeonMapManager>();
		dungeonMapEffectManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapEffectManager>();
		dungeonRouteAnimationManager = GameObject.Find("Dungeon Card Manager").GetComponent<DungeonRouteAnimationManager>();
	}

	public override void OnStateBegin()
	{
		if (!dungeonMapManager.isBossRouteSelect)
		{
			subType = dungeonMapManager.selectCardList[dungeonMapManager.thisFloorActionNum].subTypeString;
			dungeonMapManager.routeResultGroupArray[1].SetActive(value: true);
		}
	}

	public override void OnStateEnd()
	{
		dungeonMapManager.dungeonMapDirector.Stop();
		if (PoolManager.Pools["DungeonObject"].IsSpawned(dungeonRouteAnimationManager.treasureSpawnGo))
		{
			PoolManager.Pools["DungeonObject"].Despawn(dungeonRouteAnimationManager.treasureSpawnGo, dungeonMapManager.poolManagerGO.transform);
		}
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			TransitionStart();
		}
		if (dungeonMapManager.dungeonMapDirector.time >= dungeonMapManager.dungeonMapDirector.duration)
		{
			TransitionStart();
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void TransitionStart()
	{
		dungeonMapManager.battleConsecutiveTotalNum = 0;
		dungeonMapManager.battleConsecutiveRoundNum = 0;
		int thisFloorActionNum = dungeonMapManager.thisFloorActionNum;
		if (dungeonMapManager.isBossRouteSelect)
		{
			PlayerNonSaveDataManager.isDungeonScnearioBattle = false;
			PlayerNonSaveDataManager.isDungeonBossBattle = true;
			dungeonMapEffectManager.DespawnDungeonLibidoEffect();
			Transition(bossLink);
			Debug.Log("ボス戦闘ルート");
		}
		else if (subType == "battle" || subType == "hardBattle")
		{
			if (dungeonMapManager.thisFloorActionNum < 2 && (dungeonMapManager.selectCardList[thisFloorActionNum + 1].subTypeString == "battle" || dungeonMapManager.selectCardList[thisFloorActionNum + 1].subTypeString == "hardBattle"))
			{
				dungeonMapManager.battleConsecutiveTotalNum++;
				if (dungeonMapManager.thisFloorActionNum == 0 && (dungeonMapManager.selectCardList[thisFloorActionNum + 2].subTypeString == "battle" || dungeonMapManager.selectCardList[thisFloorActionNum + 2].subTypeString == "hardBattle"))
				{
					dungeonMapManager.battleConsecutiveTotalNum++;
				}
				Debug.Log("連続戦闘回数：" + dungeonMapManager.battleConsecutiveTotalNum);
			}
			dungeonMapEffectManager.DespawnDungeonLibidoEffect();
			Transition(battleLink);
		}
		else if (dungeonMapManager.isMimicBattle)
		{
			dungeonMapEffectManager.DespawnDungeonLibidoEffect();
			Transition(battleLink);
		}
		else
		{
			Transition(stateLink);
		}
	}
}
