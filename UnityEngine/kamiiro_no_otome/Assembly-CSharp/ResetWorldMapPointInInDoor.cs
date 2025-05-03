using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetWorldMapPointInInDoor : StateBehaviour
{
	private WorldMapAccessManager worldMapAccessManager;

	private InDoorCommandManager inDoorCommandManager;

	private WorldEventCheckData worldEventCheckData;

	private WorldEventCheckData inDoorEventCheckData;

	private WorldEventCheckData dungeonEventCheckData;

	private bool isTalkEventExist;

	private bool isMapEventExist;

	private bool isSexEventExist;

	private bool isDungeonEventExist;

	public StateLink noScenarioLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		isTalkEventExist = false;
		isMapEventExist = false;
		isSexEventExist = false;
		isDungeonEventExist = false;
		List<string> list = new List<string>();
		List<WorldMapUnlockData> list2 = worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList.Where((WorldMapUnlockData data) => data.sortID < 90).ToList();
		WorldMapUnlockData item = worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList.Find((WorldMapUnlockData data) => data.currentPointName == PlayerDataManager.currentAccessPointName);
		list2.Remove(item);
		foreach (WorldMapUnlockData item2 in list2)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[item2.needFlagName])
			{
				list.Add(item2.currentPointName);
			}
		}
		foreach (string pointName in list)
		{
			if (worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList.Find((WorldMapUnlockData data) => data.currentPointName == pointName).isDungeon)
			{
				dungeonEventCheckData = PlayerScenarioDataManager.CheckDungeonCurrentEvent(pointName);
				if (!string.IsNullOrEmpty(dungeonEventCheckData.currentScenarioName) && dungeonEventCheckData.currentScenarioId != 900 && dungeonEventCheckData.currentScenarioId != 901)
				{
					ScenarioFlagData scenarioFlagData = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == dungeonEventCheckData.currentScenarioName);
					if (scenarioFlagData.isSexEvent)
					{
						isSexEventExist = true;
					}
					else
					{
						isDungeonEventExist = true;
					}
					Debug.Log("インドアチェック／ダンジョン内のシナリオ名あり：" + pointName + "／シナリオ名：" + scenarioFlagData.scenarioName);
				}
				continue;
			}
			worldEventCheckData = PlayerScenarioDataManager.CheckWorldAccessPointCurrentEvent(pointName);
			if (string.IsNullOrEmpty(worldEventCheckData.currentScenarioName))
			{
				if (pointName != "Kingdom1" && pointName != "City1")
				{
					inDoorEventCheckData = PlayerScenarioDataManager.CheckWorldAccessPointCurrentTalkEvent(pointName);
					if (!string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
					{
						isTalkEventExist = true;
					}
				}
				continue;
			}
			ScenarioFlagData scenarioFlagData2 = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == worldEventCheckData.currentScenarioName);
			if (scenarioFlagData2.isSexEvent)
			{
				isSexEventExist = true;
			}
			else
			{
				isMapEventExist = true;
			}
			Debug.Log("インドアチェック／ワールドのシナリオ名あり：" + pointName + "／シナリオ名：" + scenarioFlagData2.scenarioName);
			break;
		}
		if (isSexEventExist || isMapEventExist || isTalkEventExist || isDungeonEventExist)
		{
			if (isSexEventExist)
			{
				inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
				inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
				Debug.Log("インドアチェック／ワールドのシナリオあり：えっち");
			}
			else if (isMapEventExist)
			{
				inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["mapEvent"];
				inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
				Debug.Log("インドアチェック／ワールドのシナリオあり：マップ");
			}
			else if (isTalkEventExist)
			{
				inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
				inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
				Debug.Log("インドアチェック／ワールドのシナリオあり：会話");
			}
			else if (isDungeonEventExist)
			{
				inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["dungeonEvent"];
				inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
				Debug.Log("インドアチェック／ワールドのシナリオあり：ダンジョン");
			}
			Transition(stateLink);
		}
		else
		{
			inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: false);
			Transition(noScenarioLink);
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
