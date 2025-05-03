using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBossClear : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	private PlayMakerFSM bgmManegerFSM;

	public StateLink stateLink;

	public StateLink bossLink;

	public StateLink scenarioLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		bgmManegerFSM = GameObject.Find("Bgm Play Manager").GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager.battleCommandCanvasGroup.interactable = false;
		dungeonBattleManager.battleCommandCanvasGroup.alpha = 0.5f;
		dungeonBattleManager.battleSubButtonCanvasGroup.interactable = false;
		dungeonBattleManager.battleSubButtonCanvasGroup.alpha = 0.5f;
		if (PlayerNonSaveDataManager.isDungeonScnearioBattle)
		{
			if (PlayerStatusDataManager.enemyAllHp <= 0)
			{
				Debug.Log("ダンジョンのシナリオ戦闘に勝利");
				Transition(scenarioLink);
			}
		}
		else if (dungeonMapManager.isBossRouteSelect)
		{
			if (PlayerStatusDataManager.enemyAllHp > 0)
			{
				return;
			}
			DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
			int num = 0;
			for (int i = 0; i < dungeonMapData.dungeonBossFloorList.Count; i++)
			{
				if (dungeonMapData.dungeonBossFloorList[i] != int.MaxValue && num < dungeonMapData.dungeonBossFloorList[i])
				{
					num = dungeonMapData.dungeonBossFloorList[i];
				}
			}
			Debug.Log("現在の階層：" + dungeonMapManager.dungeonCurrentFloorNum + "／最奥ボスの階層：" + num);
			if (dungeonMapManager.dungeonCurrentFloorNum == num && !PlayerFlagDataManager.dungeonDeepClearFlagDictionary[dungeonMapData.dungeonName])
			{
				PlayerNonSaveDataManager.isDungeonDeepClear = true;
				PlayerFlagDataManager.dungeonDeepClearFlagDictionary[dungeonMapData.dungeonName] = true;
				Debug.Log(dungeonMapData.dungeonName + "：最奥ボス撃破");
			}
			Transition(bossLink);
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
