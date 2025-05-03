using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckLocalMapScenarioInInDoor : StateBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	private LocalEventCheckData localEventCheckData;

	private InDoorCommandManager inDoorCommandManager;

	public bool isCheckSameLocalMapCity;

	private bool isTalkEventExist;

	private bool isMapEventExist;

	private bool isSexEventExist;

	public StateLink checkInDoorLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: false);
		isTalkEventExist = false;
		isMapEventExist = false;
		isSexEventExist = false;
		string pointname = "";
		string currentAccessPointName = PlayerDataManager.currentAccessPointName;
		if (!(currentAccessPointName == "Kingdom1"))
		{
			if (currentAccessPointName == "City1")
			{
				if (isCheckSameLocalMapCity)
				{
					pointname = "City1";
				}
				else
				{
					pointname = "Kingdom1";
				}
			}
		}
		else if (isCheckSameLocalMapCity)
		{
			pointname = "Kingdom1";
		}
		else
		{
			pointname = "City1";
		}
		List<string> list = new List<string>();
		foreach (LocalMapUnlockData item in localMapAccessManager.localMapUnlockDataBase.localMapUnlockDataList.Where((LocalMapUnlockData data) => data.worldCityName == pointname).ToList())
		{
			bool[] array = new bool[item.needFlagNameList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				if (PlayerFlagDataManager.scenarioFlagDictionary[item.needFlagNameList[i]])
				{
					array[i] = true;
				}
			}
			if (array.All((bool Data) => Data))
			{
				list.Add(item.currentPlaceName);
			}
		}
		foreach (string item2 in list)
		{
			localEventCheckData = PlayerMoveScenarioDataManager.CheckLocalPlaceMoveEvent(item2);
			if (!string.IsNullOrEmpty(localEventCheckData.currentScenarioName))
			{
				Debug.Log("インドアからのローカルチェック／ローカルのシナリオ名あり：" + item2 + "／シナリオ名：" + localEventCheckData.currentScenarioName);
				if (GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == localEventCheckData.currentScenarioName).isSexEvent)
				{
					isSexEventExist = true;
				}
				else
				{
					isMapEventExist = true;
				}
			}
		}
		if (isSexEventExist)
		{
			inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
			inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
			Debug.Log("インドアからのローカルチェック／別都市のシナリオあり：えっち");
			Transition(stateLink);
		}
		else if (isMapEventExist)
		{
			inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["mapEvent"];
			inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
			Debug.Log("インドアからのローカルチェック／別都市のシナリオあり：マップ");
			Transition(stateLink);
		}
		else if (isTalkEventExist)
		{
			inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
			inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
			Debug.Log("インドアからのローカルチェック／別都市のシナリオあり：会話");
			Transition(stateLink);
		}
		else
		{
			Transition(checkInDoorLink);
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
