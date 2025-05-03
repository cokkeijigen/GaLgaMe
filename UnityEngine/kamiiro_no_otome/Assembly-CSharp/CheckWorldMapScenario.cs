using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckWorldMapScenario : StateBehaviour
{
	private WorldEventCheckData worldEventCheckData;

	private WorldEventCheckData inDoorEventCheckData;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = base.gameObject.GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		string text = base.transform.parent.gameObject.name;
		worldEventCheckData = PlayerScenarioDataManager.CheckWorldAccessPointCurrentEvent(text);
		parameterContainer.SetString("scenarioName", worldEventCheckData.currentScenarioName);
		parameterContainer.SetInt("scenarioSortId", worldEventCheckData.currentScenarioId);
		parameterContainer.SetString("disablePointTerm", worldEventCheckData.disablePointTerm);
		if (string.IsNullOrEmpty(worldEventCheckData.disablePointTerm))
		{
			parameterContainer.SetBool("isWorldMapPointDisable", value: false);
		}
		else
		{
			parameterContainer.SetBool("isWorldMapPointDisable", value: true);
		}
		if (string.IsNullOrEmpty(worldEventCheckData.currentScenarioName))
		{
			if (text != "Kingdom1" && text != "City1")
			{
				inDoorEventCheckData = PlayerScenarioDataManager.CheckWorldAccessPointCurrentTalkEvent(text);
				Debug.Log("ワールドイベント確認／ローカルなし：" + text);
				if (string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
				{
					NotExistScnearioName(text);
				}
				else
				{
					parameterContainer.SetString("disablePointTerm", inDoorEventCheckData.disablePointTerm);
					if (string.IsNullOrEmpty(inDoorEventCheckData.disablePointTerm))
					{
						parameterContainer.SetBool("isWorldMapPointDisable", value: false);
					}
					else
					{
						parameterContainer.SetBool("isWorldMapPointDisable", value: true);
					}
					parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
					parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
					if (parameterContainer.GetBool("isHeroinePlaceCheck"))
					{
						HeroineCheckData heroineCheckData = PlayerHeroineLocationDataManager.CheckWorldMapHeroineHere(text);
						if (heroineCheckData.isHeroineHere)
						{
							if (heroineCheckData.heroineID == PlayerDataManager.DungeonHeroineFollowNum && PlayerDataManager.isDungeonHeroineFollow)
							{
								SetHeroineAbsence();
							}
							else
							{
								parameterContainer.SetInt("heroineNum", heroineCheckData.heroineID);
								parameterContainer.SetBool("isHeroineExist", value: true);
								Sprite[] heroineSpriteBalloonArray = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>().heroineSpriteBalloonArray;
								GameObject obj = parameterContainer.GetGameObject("heroineBalloon");
								obj.GetComponent<Image>().sprite = heroineSpriteBalloonArray[heroineCheckData.heroineID];
								obj.SetActive(value: true);
								Debug.Log("ヒロインがいる");
							}
						}
						else
						{
							SetHeroineAbsence();
						}
					}
					CommonProcess(text, inDoorEventCheckData);
				}
			}
			else
			{
				Debug.Log("ワールドイベント確認／ローカルあり：" + text);
				NotExistScnearioName(text);
			}
		}
		else
		{
			if (parameterContainer.GetBool("isHeroinePlaceCheck"))
			{
				SetHeroineAbsence();
			}
			parameterContainer.SetBool("isWorldMapToUtage", value: true);
			parameterContainer.SetBool("isWorldMapToInDoor", value: false);
			SetAlertBallonIconSprite();
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
			Debug.Log("シナリオ名あり：" + text);
		}
		if (!parameterContainer.GetBool("isDungeon"))
		{
			parameterContainer.SetBool("isInitialize", value: true);
		}
		if (PlayerNonSaveDataManager.isUtageToWorldMapInDoor && parameterContainer.GetBool("isHeroineExist") && PlayerDataManager.currentAccessPointName == parameterContainer.gameObject.name)
		{
			PlayerNonSaveDataManager.inDoorHeroineExist = true;
			Debug.Log("宴からインドアに行く時、現在地にヒロインがいた");
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

	private void SetAlertBallonIconSprite()
	{
		Image component = parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>();
		if (GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == worldEventCheckData.currentScenarioName).isSexEvent)
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
		}
		else
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["mapEvent"];
		}
	}

	private void SetHeroineAbsence()
	{
		parameterContainer.SetInt("heroineNum", 0);
		parameterContainer.SetBool("isHeroineExist", value: false);
		parameterContainer.GetGameObject("heroineBalloon").SetActive(value: false);
		Debug.Log("ヒロインは不在");
	}

	private void NotExistScnearioName(string pointName)
	{
		parameterContainer.GetGameObject("alertBalloon").SetActive(value: false);
		parameterContainer.SetBool("isWorldMapPointDisable", value: false);
		if (parameterContainer.GetBool("isHeroinePlaceCheck"))
		{
			HeroineCheckData heroineCheckData = PlayerHeroineLocationDataManager.CheckWorldMapHeroineHere(pointName);
			if (heroineCheckData.isHeroineHere)
			{
				if (heroineCheckData.heroineID == PlayerDataManager.DungeonHeroineFollowNum && PlayerDataManager.isDungeonHeroineFollow)
				{
					SetHeroineAbsence();
				}
				else
				{
					parameterContainer.SetInt("heroineNum", heroineCheckData.heroineID);
					parameterContainer.SetBool("isHeroineExist", value: true);
					Sprite[] heroineSpriteBalloonArray = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>().heroineSpriteBalloonArray;
					GameObject obj = parameterContainer.GetGameObject("heroineBalloon");
					obj.GetComponent<Image>().sprite = heroineSpriteBalloonArray[heroineCheckData.heroineID];
					obj.SetActive(value: true);
					Debug.Log("ヒロインがいる");
				}
			}
			else
			{
				SetHeroineAbsence();
			}
		}
		CommonProcess(pointName, worldEventCheckData);
	}

	private void CommonProcess(string pointName, WorldEventCheckData eventCheckData)
	{
		string locationType = eventCheckData.locationType;
		if (!(locationType == "worldMap"))
		{
			if (locationType == "localMap")
			{
				parameterContainer.SetString("scenarioName", pointName);
				parameterContainer.SetBool("isWorldMapToUtage", value: false);
				parameterContainer.SetBool("isWorldMapToInDoor", value: true);
				Debug.Log("シナリオ名空白＆ローカルマップ／" + pointName);
			}
			else if (pointName == "City1")
			{
				parameterContainer.SetString("scenarioName", pointName);
				parameterContainer.SetBool("isWorldMapToUtage", value: false);
				parameterContainer.SetBool("isWorldMapToInDoor", value: false);
				Debug.Log("シナリオ名空白ワールドtoローカル／" + pointName);
			}
			else
			{
				parameterContainer.SetString("scenarioName", pointName);
				parameterContainer.SetBool("isWorldMapToUtage", value: false);
				parameterContainer.SetBool("isWorldMapToInDoor", value: true);
				Debug.Log("シナリオ名空白＆ワールドtoインドア／" + pointName);
			}
		}
		else if (pointName == "Kingdom1" || pointName == "City1")
		{
			parameterContainer.SetString("scenarioName", pointName);
			parameterContainer.SetBool("isWorldMapToUtage", value: false);
			parameterContainer.SetBool("isWorldMapToInDoor", value: false);
			Debug.Log("シナリオ名空白ワールドtoローカル／" + pointName);
		}
		else if (parameterContainer.GetBool("isDungeon"))
		{
			parameterContainer.SetString("scenarioName", pointName);
			parameterContainer.SetBool("isWorldMapToUtage", value: true);
			parameterContainer.SetBool("isWorldMapToInDoor", value: false);
			Debug.Log("シナリオ名空白＆ダンジョン／" + pointName);
		}
		else
		{
			parameterContainer.SetString("scenarioName", pointName);
			parameterContainer.SetBool("isWorldMapToUtage", value: false);
			parameterContainer.SetBool("isWorldMapToInDoor", value: true);
			Debug.Log("シナリオ名空白＆ワールドtoインドア／" + pointName);
		}
	}
}
