using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckWorldMapDungeonScenario : StateBehaviour
{
	private WorldEventCheckData dungeonEventCheckData;

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
		string @string = parameterContainer.GetString("scenarioName");
		if (parameterContainer.GetBool("isDungeon") && @string == text)
		{
			Debug.Log("ダンジョンイベントを確認");
			parameterContainer.SetBool("isDungeonSexEvent", value: false);
			dungeonEventCheckData = PlayerScenarioDataManager.CheckDungeonCurrentEvent(text);
			parameterContainer.SetString("scenarioName", dungeonEventCheckData.currentScenarioName);
			parameterContainer.SetString("disablePointTerm", dungeonEventCheckData.disablePointTerm);
			if (string.IsNullOrEmpty(dungeonEventCheckData.disablePointTerm))
			{
				parameterContainer.SetBool("isWorldMapPointDisable", value: false);
			}
			else
			{
				parameterContainer.SetBool("isWorldMapPointDisable", value: true);
			}
			if (string.IsNullOrEmpty(dungeonEventCheckData.currentScenarioName))
			{
				parameterContainer.GetGameObject("alertBalloon").SetActive(value: false);
				parameterContainer.SetBool("isWorldMapPointDisable", value: false);
				CommonProcess(text, dungeonEventCheckData);
			}
			else
			{
				parameterContainer.SetBool("isWorldMapToUtage", value: false);
				parameterContainer.SetBool("isWorldMapToInDoor", value: false);
				if (dungeonEventCheckData.scenarioType == "dungeonSex")
				{
					parameterContainer.SetBool("isDungeonSexEvent", value: true);
				}
				SetAlertBallonIconSprite();
				parameterContainer.GetGameObject("alertBalloon").SetActive(value: true);
				Debug.Log("ダンジョン内のシナリオ名あり：" + text);
				CommonProcess(text, dungeonEventCheckData);
			}
		}
		parameterContainer.SetBool("isInitialize", value: true);
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
		if (GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == dungeonEventCheckData.currentScenarioName).isSexEvent)
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["sexEvent"];
		}
		else
		{
			component.sprite = GameDataManager.instance.itemCategoryDataBase.eventIconDictionary["dungeonEvent"];
		}
	}

	private void CommonProcess(string pointName, WorldEventCheckData eventCheckData)
	{
		if (eventCheckData.locationType == "dungeon")
		{
			parameterContainer.SetString("scenarioName", pointName);
			parameterContainer.SetBool("isWorldMapToUtage", value: true);
			parameterContainer.SetBool("isWorldMapToInDoor", value: false);
			Debug.Log("ダンジョンシナリオ名あり＆ダンジョン／" + pointName);
		}
		else
		{
			parameterContainer.SetString("scenarioName", pointName);
			parameterContainer.SetBool("isWorldMapToUtage", value: true);
			parameterContainer.SetBool("isWorldMapToInDoor", value: false);
			Debug.Log("シナリオ名空白＆ダンジョン／" + pointName);
		}
	}
}
