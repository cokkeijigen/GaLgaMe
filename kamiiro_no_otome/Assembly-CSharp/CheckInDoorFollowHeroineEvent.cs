using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckInDoorFollowHeroineEvent : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isDungeonHeroineFollow && !(PlayerDataManager.currentPlaceName == "Inn") && !(PlayerDataManager.currentPlaceName == "InnStreet1"))
		{
			ParameterContainer param = inDoorTalkManager.positionTalkImageArray[1].GetComponent<ParameterContainer>();
			InDoorCharacterCgData inDoorCharacterCgData = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == param.GetString("characterName"));
			inDoorTalkManager.heroineAlertGroupGo.GetComponent<RectTransform>().anchoredPosition = inDoorCharacterCgData.heroineAlertV2;
			InDoorEventCheckData inDoorEventCheckData = PlayerScenarioDataManager.CheckInDoorCurrentTalkEvent(PlayerDataManager.currentPlaceName, param.GetString("characterName"));
			if (!string.IsNullOrEmpty(inDoorEventCheckData.currentScenarioName))
			{
				string scenarioName = inDoorEventCheckData.currentScenarioName;
				if (GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == scenarioName).isSexEvent)
				{
					param.GetGameObject("sexBalloon").GetComponent<Image>().enabled = true;
					param.GetGameObject("eventBalloon").GetComponent<Image>().enabled = false;
					param.GetStringList("scenarioNameList")[0] = scenarioName;
					param.GetStringList("scenarioNameList")[1] = "";
				}
				else
				{
					param.GetGameObject("sexBalloon").GetComponent<Image>().enabled = false;
					param.GetGameObject("eventBalloon").GetComponent<Image>().enabled = true;
					param.GetStringList("scenarioNameList")[0] = "";
					param.GetStringList("scenarioNameList")[1] = scenarioName;
				}
				param.GetGameObject("alertGroupGo").SetActive(value: true);
			}
			else
			{
				param.GetGameObject("alertGroupGo").SetActive(value: false);
				param.GetStringList("scenarioNameList")[0] = "";
				param.GetStringList("scenarioNameList")[1] = "";
			}
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
