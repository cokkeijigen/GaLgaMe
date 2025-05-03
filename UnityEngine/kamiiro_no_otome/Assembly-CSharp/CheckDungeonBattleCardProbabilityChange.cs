using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBattleCardProbabilityChange : StateBehaviour
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
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
		if (PlayerNonSaveDataManager.isDungeonSexEvent)
		{
			Transition(stateLink);
		}
		else if (dungeonMapData.battleCardProbabilityChangeFloorList != null && dungeonMapData.battleCardProbabilityChangeFloorList.Count > 0)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonMapData.battleCardProbabilityNotChangeFlag])
			{
				Transition(stateLink);
			}
			else if (dungeonMapData.battleCardProbabilityChangeFloorList.Contains(dungeonMapManager.dungeonCurrentFloorNum))
			{
				int index = dungeonMapData.battleCardProbabilityChangeFloorList.FindIndex((int data) => data == dungeonMapManager.dungeonCurrentFloorNum);
				int num = dungeonMapData.battleCardProbabilityChangePowerList[index];
				dungeonMapManager.chooseCardDictionary["dungeonCardBattle"] = num;
				Debug.Log("戦闘カードの重み変更：" + num);
				Transition(stateLink);
			}
			else
			{
				Transition(stateLink);
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
