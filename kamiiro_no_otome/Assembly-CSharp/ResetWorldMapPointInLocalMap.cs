using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetWorldMapPointInLocalMap : StateBehaviour
{
	private WorldMapAccessManager worldMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

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
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		localMapAccessManager.localExitButtonBalloonGo.SetActive(value: false);
		isTalkEventExist = false;
		isMapEventExist = false;
		isSexEventExist = false;
		isDungeonEventExist = false;
		List<string> list = new List<string>();
		foreach (WorldMapUnlockData item in worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList.Where((WorldMapUnlockData data) => data.sortID < 90).ToList())
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[item.needFlagName])
			{
				list.Add(item.currentPointName);
			}
		}
		foreach (string pointName in list)
		{
			if (worldMapAccessManager.worldMapUnlockDataBase.worldMapUnlockDataList.Find((WorldMapUnlockData data) => data.currentPointName == pointName).isDungeon)
			{
				dungeonEventCheckData = PlayerScenarioDataManager.CheckDungeonCurrentEvent(pointName);
				if (string.IsNullOrEmpty(dungeonEventCheckData.currentScenarioName))
				{
					worldEventCheckData = PlayerScenarioDataManager.CheckWorldAccessPointCurrentEvent(pointName);
					if (!string.IsNullOrEmpty(worldEventCheckData.currentScenarioName))
					{
						ScenarioFlagData scenarioFlagData = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == worldEventCheckData.currentScenarioName);
						if (scenarioFlagData.isSexEvent)
						{
							isSexEventExist = true;
						}
						else
						{
							isMapEventExist = true;
						}
						Debug.Log("ローカルチェック／ワールドのシナリオ名あり：" + pointName + "／シナリオ名：" + scenarioFlagData.scenarioName);
						break;
					}
				}
				else if (dungeonEventCheckData.currentScenarioId != 900 && dungeonEventCheckData.currentScenarioId != 901)
				{
					ScenarioFlagData scenarioFlagData2 = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == dungeonEventCheckData.currentScenarioName);
					if (scenarioFlagData2.isSexEvent)
					{
						isSexEventExist = true;
					}
					else
					{
						isDungeonEventExist = true;
					}
					Debug.Log("ローカルチェック／ダンジョン内のシナリオ名あり：" + pointName + "／シナリオ名：" + scenarioFlagData2.scenarioName);
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
			ScenarioFlagData scenarioFlagData3 = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == worldEventCheckData.currentScenarioName);
			if (scenarioFlagData3.isSexEvent)
			{
				isSexEventExist = true;
			}
			else
			{
				isMapEventExist = true;
			}
			Debug.Log("ローカルチェック／ワールドのシナリオ名あり：" + pointName + "／シナリオ名：" + scenarioFlagData3.scenarioName);
			break;
		}
		if (isSexEventExist || isMapEventExist || isTalkEventExist || isDungeonEventExist)
		{
			if (isSexEventExist)
			{
				localMapAccessManager.localExitButtonImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
				localMapAccessManager.localExitButtonBalloonGo.SetActive(value: true);
				Debug.Log("ローカルチェック／ワールドのシナリオあり：えっち");
			}
			else if (isMapEventExist)
			{
				localMapAccessManager.localExitButtonImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["mapEvent"];
				localMapAccessManager.localExitButtonBalloonGo.SetActive(value: true);
				Debug.Log("ローカルチェック／ワールドのシナリオあり：マップ");
			}
			else if (isTalkEventExist)
			{
				localMapAccessManager.localExitButtonImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
				localMapAccessManager.localExitButtonBalloonGo.SetActive(value: true);
				Debug.Log("ローカルチェック／ワールドのシナリオあり：会話");
			}
			else if (isDungeonEventExist)
			{
				localMapAccessManager.localExitButtonImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["dungeonEvent"];
				localMapAccessManager.localExitButtonBalloonGo.SetActive(value: true);
				Debug.Log("ローカルチェック／ワールドのシナリオあり：ダンジョン");
			}
			Transition(stateLink);
		}
		else
		{
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
