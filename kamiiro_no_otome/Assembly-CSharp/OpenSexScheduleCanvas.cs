using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenSexScheduleCanvas : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	private SexStatusManager sexStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		int i;
		for (i = 1; i < 5; i++)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == i);
			SetHeroineScheduleVisible(i, PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterMenstruationViewFlag]);
		}
		statusCustomManager.statusOverlayCanvas.SetActive(value: true);
		statusCustomManager.overlayBlackImageGo.SetActive(value: true);
		sexStatusManager.sexScheduleWindowGo.SetActive(value: true);
		statusCustomManager.customWindowArray[0].SetActive(value: false);
		statusCustomManager.customWindowArray[1].SetActive(value: false);
		statusCustomManager.overlayBlackImageGo.transform.SetSiblingIndex(3);
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

	private void SetHeroineScheduleVisible(int heroineID, bool isVisible)
	{
		if (isVisible)
		{
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetVariable<UguiImage>("iconImage").image.sprite = sexStatusManager.heroineScheduleIconSpriteArray[heroineID];
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "character" + heroineID;
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObject("unKnownGroup").SetActive(value: false);
			if (heroineID == 3)
			{
				string characterMenstruationFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[3].characterMenstruationFlag;
				if (PlayerFlagDataManager.scenarioFlagDictionary[characterMenstruationFlag])
				{
					sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[0].SetActive(value: false);
					sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[1].SetActive(value: true);
				}
				else
				{
					sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[0].SetActive(value: true);
					sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[1].SetActive(value: false);
				}
			}
			else
			{
				sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObject("scheduleGroup").SetActive(value: true);
			}
		}
		else
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == heroineID);
			SetHeroineNameVisible(heroineID, PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonFollowUnLockFlag]);
		}
	}

	private void SetHeroineNameVisible(int heroineID, bool isVisible)
	{
		sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObject("unKnownGroup").SetActive(value: true);
		if (isVisible)
		{
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetVariable<UguiImage>("iconImage").image.sprite = sexStatusManager.heroineScheduleIconSpriteArray[heroineID];
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "character" + heroineID;
			if (heroineID == 3)
			{
				sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[0].SetActive(value: false);
				sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[1].SetActive(value: false);
			}
			else
			{
				sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObject("scheduleGroup").SetActive(value: false);
			}
		}
		else
		{
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetVariable<UguiImage>("iconImage").image.sprite = sexStatusManager.heroineScheduleIconSpriteArray[0];
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "unKnown";
			sexStatusManager.heroineScheduleParamArray[heroineID - 1].GetGameObject("scheduleGroup").SetActive(value: false);
		}
	}
}
