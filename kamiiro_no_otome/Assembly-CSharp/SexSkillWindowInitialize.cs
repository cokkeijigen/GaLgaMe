using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SexSkillWindowInitialize : StateBehaviour
{
	private StatusManager statusManager;

	private SexStatusManager sexStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		int i;
		for (i = 2; i < 5; i++)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == i);
			SetHeroineTabVisible(i, PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonSexUnLockFlag]);
		}
		sexStatusManager.selectSexSkillScrollContentIndex = 0;
		if (statusManager.selectCharacterNum == 0)
		{
			sexStatusManager.isPassiveButtonDisable = true;
			sexStatusManager.sexSkillTypeButtonGoArray[0].GetComponent<CanvasGroup>().interactable = false;
			sexStatusManager.sexSkillTypeButtonGoArray[0].GetComponent<CanvasGroup>().alpha = 0.5f;
			if (sexStatusManager.isSelectTypePassvie)
			{
				sexStatusManager.isSelectTypePassvie = false;
			}
		}
		else
		{
			sexStatusManager.isPassiveButtonDisable = false;
			sexStatusManager.sexSkillTypeButtonGoArray[0].GetComponent<CanvasGroup>().interactable = true;
			sexStatusManager.sexSkillTypeButtonGoArray[0].GetComponent<CanvasGroup>().alpha = 1f;
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

	private void SetHeroineTabVisible(int heroineID, bool isVisible)
	{
		CanvasGroup component = sexStatusManager.sexSkillTabButtonGoArray[heroineID - 1].GetComponent<CanvasGroup>();
		if (isVisible)
		{
			component.interactable = true;
			component.alpha = 1f;
			return;
		}
		component.interactable = false;
		component.alpha = 0.5f;
		CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == heroineID);
		SetHeroineNameVisible(heroineID, PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonFollowUnLockFlag]);
	}

	private void SetHeroineNameVisible(int heroineID, bool isVisible)
	{
		if (isVisible)
		{
			sexStatusManager.sexSkillTabButtonLocArray[heroineID - 1].Term = "character" + heroineID;
		}
		else
		{
			sexStatusManager.sexSkillTabButtonLocArray[heroineID - 1].Term = "unKnown";
		}
	}
}
