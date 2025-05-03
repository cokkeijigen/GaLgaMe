using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorScenarioInInDoor : StateBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	private InDoorEventCheckData inDoorEventCheckData;

	private InDoorCommandManager inDoorCommandManager;

	public bool isCheckSameLocalMapCity;

	private bool isTalkEventExist;

	private bool isSexEventExist;

	public StateLink noScenarioLink;

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
		isTalkEventExist = false;
		isSexEventExist = false;
		string pointname = "";
		List<string> list = new List<string>();
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
		List<LocalMapUnlockData> list2 = localMapAccessManager.localMapUnlockDataBase.localMapUnlockDataList.Where((LocalMapUnlockData data) => data.worldCityName == pointname).ToList();
		if (isCheckSameLocalMapCity)
		{
			LocalMapUnlockData localMapUnlockData = localMapAccessManager.localMapUnlockDataBase.localMapUnlockDataList.Find((LocalMapUnlockData data) => data.currentPlaceName == PlayerDataManager.currentPlaceName);
			Debug.Log("現在いるインドア名をリストから削除する：" + localMapUnlockData.currentPlaceName);
			list2.Remove(localMapUnlockData);
		}
		foreach (LocalMapUnlockData item in list2)
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
			inDoorEventCheckData = PlayerMoveScenarioDataManager.CheckInDoorMoveEvent(item2);
			if (string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
			{
				Debug.Log("インドアからのインドアチェック／ローカル＆インドアのシナリオ名空白：" + item2);
				continue;
			}
			Debug.Log("インドアからのインドアチェック／インドアのシナリオ名あり：" + item2 + "／シナリオ名：" + inDoorEventCheckData.currentScenarioName);
			bool isSexEvent = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == inDoorEventCheckData.currentScenarioName).isSexEvent;
			string scenarioTalkCharacter = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == inDoorEventCheckData.currentScenarioName).scenarioTalkCharacter;
			switch (scenarioTalkCharacter)
			{
			case "ルーシー":
			case "リィナ":
			case "シア":
			case "レヴィ":
			{
				int num = 1;
				switch (scenarioTalkCharacter)
				{
				case "ルーシー":
					num = 1;
					break;
				case "リィナ":
					num = 2;
					break;
				case "シア":
					num = 3;
					break;
				case "レヴィ":
					num = 4;
					break;
				}
				if (PlayerHeroineLocationDataManager.CheckLocalMapHeroineHere(item2).isHeroineHere)
				{
					if (isSexEvent)
					{
						isSexEventExist = true;
					}
					else
					{
						isTalkEventExist = true;
					}
				}
				else if (num == PlayerDataManager.DungeonHeroineFollowNum && PlayerDataManager.isDungeonHeroineFollow)
				{
					if (isSexEvent)
					{
						isSexEventExist = true;
					}
					else
					{
						isTalkEventExist = true;
					}
				}
				break;
			}
			default:
				if (isSexEvent)
				{
					isSexEventExist = true;
				}
				else
				{
					isTalkEventExist = true;
				}
				break;
			}
		}
		if (isSexEventExist)
		{
			inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
			inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
			Debug.Log("インドアからのインドアチェック／別都市のシナリオあり：えっち");
			Transition(stateLink);
		}
		else if (isTalkEventExist)
		{
			inDoorCommandManager.inDoorExitBurronAlertImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
			inDoorCommandManager.inDoorExitBurronAlertGo.SetActive(value: true);
			Debug.Log("インドアからのインドアチェック／別都市のシナリオあり：会話");
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
