using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class ScenarioToDungeonBattlePreStart : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private PlayMakerFSM bgmManegerFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponentInChildren<DungeonMapStatusManager>();
		bgmManegerFSM = GameObject.Find("Bgm Play Manager").GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		PoolManager.Pools["BattleEffect"].DespawnAll();
		PoolManager.Pools["SkillEffect"].DespawnAll();
		dungeonMapManager.isDungeonRouteAction = false;
		dungeonMapManager.isBossRouteSelect = false;
		dungeonMapManager.dungeonMapCanvas.SetActive(value: false);
		dungeonMapManager.dungeonBattleCanvas.SetActive(value: true);
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData item) => item.dungeonName == PlayerDataManager.currentDungeonName);
		for (int num = dungeonMapData.borderFloorCount; num > 0; num--)
		{
			Debug.Log("境界数：" + dungeonMapData.dungeonBorderFloor[num - 1] + "／カウント：" + (num - 1));
			if (dungeonMapManager.dungeonCurrentFloorNum <= dungeonMapData.dungeonBorderFloor[num - 1])
			{
				dungeonMapManager.currentBorderNum = dungeonMapData.dungeonBorderFloor[num - 1];
			}
		}
		ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			PlayerStatusDataManager.playerPartyMember = scenarioBattleData.battleCharacterID.ToArray();
		}
		else
		{
			PlayerStatusDataManager.playerPartyMember = new int[1];
		}
		dungeonMapStatusManager.dungeonBuffAttack = 0;
		dungeonMapStatusManager.dungeonBuffDefense = 0;
		dungeonMapStatusManager.dungeonDeBuffAgility = 0;
		dungeonMapStatusManager.dungeonDeBuffAgiityRemainFloor = 0;
		dungeonMapStatusManager.dungeonBuffRetreat = 0;
		PlayerNonSaveDataManager.isDungeonScnearioBattle = true;
		dungeonMapStatusManager.isTpSkipEnable = false;
		if (!PlayerNonSaveDataManager.isUtagePlayBattleBgm)
		{
			PlayerDataManager.playBgmCategoryName = scenarioBattleData.battleBgmName;
			bgmManegerFSM.SendEvent("ChangeMasterAudioPlaylist");
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
