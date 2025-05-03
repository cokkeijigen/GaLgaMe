using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckInDoorScenarioInWorldMap : StateBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	private InDoorEventCheckData inDoorEventCheckData;

	private ParameterContainer parameterContainer;

	private bool isTalkEventExist;

	private bool isSexEventExist;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		parameterContainer = base.gameObject.GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		isTalkEventExist = false;
		isSexEventExist = false;
		List<string> list = new List<string>();
		string pointname = base.transform.parent.name;
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
				if (PlayerHeroineLocationDataManager.CheckLocalMapHeroineHereWithWorldMap(pointname, item2).isHeroineHere)
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
		Debug.Log("ワールドからのインドアチェック／都市名：" + pointname + "／えっちbool：" + isSexEventExist);
		if (isSexEventExist)
		{
			parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
			Debug.Log("ワールドからのインドアチェック／インドアのシナリオあり：えっち／" + pointname);
		}
		else if (isTalkEventExist)
		{
			parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
			Debug.Log("ワールドからのインドアチェック／インドアのシナリオあり：会話／" + pointname);
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
