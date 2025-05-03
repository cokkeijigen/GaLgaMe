using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckLocalMapToInDoorScenario : StateBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	private InDoorEventCheckData inDoorEventCheckData;

	private ParameterContainer parameterContainer;

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
		string placeName = base.transform.parent.gameObject.name;
		inDoorEventCheckData = PlayerScenarioDataManager.CheckInDoorCurrentEvent(placeName);
		if (string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
		{
			parameterContainer.GetGameObject("alertBalloon").SetActive(value: false);
			if (!(placeName == "Inn") && !(placeName == "InnStreet1") && !(placeName == "Carriage") && !(placeName == "CityCarriage") && !(placeName == "ItemShop"))
			{
				CanvasGroup componentInParent = GetComponentInParent<CanvasGroup>();
				if (GameDataManager.instance.inDoorLocationDataBase.inDoorCharacterLocationDataList.Find((InDoorCharacterLocationData data) => data.placeName == placeName) == null)
				{
					componentInParent.interactable = false;
					componentInParent.alpha = 0.5f;
				}
				else
				{
					componentInParent.interactable = true;
					componentInParent.alpha = 1f;
				}
			}
			Debug.Log("シナリオ名空白＆インドア／" + placeName);
		}
		else
		{
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
				if (parameterContainer.GetBool("isHeroineExist"))
				{
					SetEventBalloon(placeName, isSexEvent);
				}
				else if (num == PlayerDataManager.DungeonHeroineFollowNum && PlayerDataManager.isDungeonHeroineFollow)
				{
					SetEventBalloon(placeName, isSexEvent);
				}
				break;
			}
			default:
				SetEventBalloon(placeName, isSexEvent);
				break;
			}
		}
		localMapAccessManager.AddLocalMapPlaceInitialize();
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

	private void SetEventBalloon(string placeName, bool isSexEvent)
	{
		Image component = parameterContainer.GetGameObject("alertBalloon").GetComponent<Image>();
		if (isSexEvent)
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
		}
		else
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["talkEvent"];
		}
		parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
		Debug.Log("シナリオ名あり＆インドア／" + placeName);
	}
}
