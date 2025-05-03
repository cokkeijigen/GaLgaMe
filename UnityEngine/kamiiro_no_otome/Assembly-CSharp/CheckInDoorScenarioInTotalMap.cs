using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorScenarioInTotalMap : StateBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	private InDoorEventCheckData inDoorEventCheckData;

	private bool isTalkEventExist;

	private bool isSexEventExist;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
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
				pointname = "Kingdom1";
			}
		}
		else
		{
			pointname = "City1";
		}
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
			inDoorEventCheckData = PlayerMoveScenarioDataManager.CheckInDoorMoveEvent(item2);
			if (string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
			{
				Debug.Log("ワールドからのローカルチェック／ローカル＆インドアのシナリオ名空白：" + item2);
				continue;
			}
			Debug.Log("ワールドからのローカルチェック／インドアのシナリオ名あり：" + item2 + "／シナリオ名：" + inDoorEventCheckData.currentScenarioName);
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
			localMapAccessManager.localExitButtonImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
			localMapAccessManager.localExitButtonBalloonGo.SetActive(value: true);
			Debug.Log("Totalからのインドアチェック／別都市のシナリオあり：えっち");
		}
		else if (isTalkEventExist)
		{
			localMapAccessManager.localExitButtonImage.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
			localMapAccessManager.localExitButtonBalloonGo.SetActive(value: true);
			Debug.Log("Totalからのインドアチェック／別都市のシナリオあり：会話");
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
