using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckLocalMapScenario : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private LocalEventCheckData localEventCheckData;

	private ParameterContainer parameterContainer;

	public StateLink noScenarioLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		parameterContainer = base.gameObject.GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		string text = base.transform.parent.gameObject.name;
		localEventCheckData = PlayerScenarioDataManager.CheckLocalPlaceCurrentEvent(text);
		parameterContainer.SetString("scenarioName", localEventCheckData.currentScenarioName);
		parameterContainer.SetInt("scenarioSortId", localEventCheckData.currentScenarioId);
		if (string.IsNullOrEmpty(localEventCheckData.currentScenarioName))
		{
			parameterContainer.SetString("scenarioName", "");
			if (parameterContainer.GetBool("isHeroinePlaceCheck"))
			{
				HeroineCheckData heroineCheckData = PlayerHeroineLocationDataManager.CheckLocalMapHeroineHere(text);
				if (heroineCheckData.isHeroineHere)
				{
					if (heroineCheckData.heroineID == PlayerDataManager.DungeonHeroineFollowNum && PlayerDataManager.isDungeonHeroineFollow)
					{
						parameterContainer.SetInt("heroineNum", PlayerDataManager.DungeonHeroineFollowNum);
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
						Debug.Log("プレース名：" + text + "／いるヒロインID：" + heroineCheckData.heroineID);
					}
				}
				else
				{
					parameterContainer.SetBool("isHeroineExist", value: false);
					parameterContainer.SetInt("heroineNum", 0);
					SetHeroineAbsence();
					if (text == "ItemShop" && PlayerDataManager.currentTimeZone < 2)
					{
						parameterContainer.GetGameObject("heroineBalloon").SetActive(value: true);
						Debug.Log("シャーロアイコンを表示");
					}
				}
			}
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: false);
			Debug.Log("シナリオ名空白＆ローカルマップ／" + text);
			Transition(noScenarioLink);
		}
		else
		{
			if (parameterContainer.GetBool("isHeroinePlaceCheck"))
			{
				parameterContainer.SetInt("heroineNum", 0);
				SetHeroineAbsence();
			}
			SetAlertBallonIconSprite();
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
			localMapAccessManager.AddLocalMapPlaceInitialize();
			CanvasGroup componentInParent = GetComponentInParent<CanvasGroup>();
			componentInParent.interactable = true;
			componentInParent.alpha = 1f;
			Debug.Log("シナリオ名あり＆ローカルマップ／" + text);
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

	private void SetAlertBallonIconSprite()
	{
		Image component = parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>();
		ScenarioFlagData scenarioFlagData = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == localEventCheckData.currentScenarioName);
		if (scenarioFlagData.isSexEvent)
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
		}
		else if (scenarioFlagData.scenarioName == "MH_Rina_001")
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["firstEvent2"];
		}
		else if (scenarioFlagData.scenarioName == "MH_Shia_001")
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["firstEvent3"];
		}
		else if (scenarioFlagData.scenarioName == "MH_Levy_016")
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["mapEvent"];
		}
		else
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["mapEvent"];
		}
		component.SetNativeSize();
	}

	private void SetHeroineAbsence()
	{
		parameterContainer.SetBool("isHeroineExist", value: false);
		parameterContainer.GetGameObject("heroineBalloon").SetActive(value: false);
		Debug.Log("ヒロインは不在");
	}
}
