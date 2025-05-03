using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckLocalMapScenarioInWorldMap : StateBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	private LocalEventCheckData localEventCheckData;

	private ParameterContainer parameterContainer;

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
		parameterContainer = base.gameObject.GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		isTalkEventExist = false;
		isMapEventExist = false;
		isSexEventExist = false;
		string pointname = base.transform.parent.name;
		if (parameterContainer.GetString("scenarioName") == base.transform.parent.gameObject.name)
		{
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
					Debug.Log("ワールドからのローカルチェック／ローカルのシナリオ名あり：" + item2 + "／シナリオ名：" + localEventCheckData.currentScenarioName);
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
				parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
				parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
				Debug.Log("ワールドからのローカルチェック／ローカルのシナリオあり：えっち");
				Transition(stateLink);
			}
			else if (isMapEventExist)
			{
				parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["mapEvent"];
				parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
				Debug.Log("ワールドからのローカルチェック／ローカルのシナリオあり：マップ");
				Transition(stateLink);
			}
			else if (isTalkEventExist)
			{
				parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
				parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
				Debug.Log("ワールドからのローカルチェック／ローカルのシナリオあり：会話");
				Transition(stateLink);
			}
			else
			{
				Transition(checkInDoorLink);
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
