using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetClickMapPlace : StateBehaviour
{
	private ParameterContainer parameterContainer;

	public bool isSelectDungeon;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		string text = parameterContainer.GetString("scenarioName");
		if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			PlayerDataManager.isSelectDungeon = isSelectDungeon;
			if (isSelectDungeon)
			{
				PlayerNonSaveDataManager.dungeonSetStartFloorNum = 0;
				PlayerNonSaveDataManager.isDungeonSexEvent = parameterContainer.GetBool("isDungeonSexEvent");
			}
			else
			{
				PlayerNonSaveDataManager.isDungeonSexEvent = false;
			}
			PlayerNonSaveDataManager.isWorldMapToUtage = parameterContainer.GetBool("isWorldMapToUtage");
			PlayerNonSaveDataManager.selectAccessPointName = base.transform.parent.gameObject.name;
			PlayerNonSaveDataManager.selectAccessPointGO = base.transform.parent.gameObject;
			PlayerNonSaveDataManager.isWorldMapToInDoor = parameterContainer.GetBool("isWorldMapToInDoor");
			PlayerNonSaveDataManager.isWorldMapPointDisable = parameterContainer.GetBool("isWorldMapPointDisable");
			PlayerNonSaveDataManager.selectDisableMapPointTerm = parameterContainer.GetString("disablePointTerm");
			if (!string.IsNullOrEmpty(text))
			{
				PlayerNonSaveDataManager.selectScenarioName = text;
			}
			else
			{
				PlayerNonSaveDataManager.selectScenarioName = "";
			}
			if (parameterContainer.GetBool("isHeroinePlaceCheck"))
			{
				int @int = parameterContainer.GetInt("heroineNum");
				PlayerNonSaveDataManager.inDoorHeroineExist = parameterContainer.GetBool("isHeroineExist");
				if (@int > 0)
				{
					PlayerNonSaveDataManager.inDoorHeroineNum = @int;
				}
				else
				{
					PlayerNonSaveDataManager.inDoorHeroineNum = 0;
					PlayerNonSaveDataManager.inDoorTalkPhaseNum = parameterContainer.GetInt("talkPhaseNum");
				}
			}
		}
		else
		{
			PlayerNonSaveDataManager.selectPlaceName = base.transform.parent.gameObject.name;
			PlayerNonSaveDataManager.selectPlaceGO = base.gameObject;
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = text.Substring(0, 6);
				Debug.Log("ジャンプするシナリオ名の前半部分：" + text2);
				if (text2 == "H_Lucy" && PlayerOptionsDataManager.isLucyVoiceTypeSexy)
				{
					text += "_sexy";
				}
				PlayerNonSaveDataManager.selectScenarioName = text;
			}
			else
			{
				PlayerNonSaveDataManager.selectScenarioName = "";
				int int2 = parameterContainer.GetInt("heroineNum");
				PlayerNonSaveDataManager.inDoorHeroineExist = parameterContainer.GetBool("isHeroineExist");
				if (int2 > 0)
				{
					PlayerNonSaveDataManager.inDoorHeroineNum = int2;
				}
				else
				{
					PlayerNonSaveDataManager.inDoorHeroineNum = 0;
					PlayerNonSaveDataManager.inDoorTalkPhaseNum = parameterContainer.GetInt("talkPhaseNum");
				}
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
