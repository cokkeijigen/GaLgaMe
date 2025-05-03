using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusSkillListViewInitialize : StateBehaviour
{
	private StatusManager statusManager;

	private StatusSkillViewManager statusSkillViewManager;

	public StateLink normalLink;

	public StateLink learnLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusSkillViewManager = GameObject.Find("Skill View Manager").GetComponent<StatusSkillViewManager>();
	}

	public override void OnStateBegin()
	{
		GameObject[] skillCategoryTabArray = statusSkillViewManager.skillCategoryTabArray;
		for (int i = 0; i < skillCategoryTabArray.Length; i++)
		{
			skillCategoryTabArray[i].GetComponent<Image>().sprite = statusSkillViewManager.skillCategoryTabSpriteArray[0];
		}
		statusManager.ResetScrollViewContents(statusManager.skillContentGO.transform, isCustom: false);
		if (!statusSkillViewManager.isLearned)
		{
			statusManager.selectSkillScrollContentIndex = 0;
		}
		for (int j = 0; j < 5; j++)
		{
			string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[j].characterDungeonFollowUnLockFlag;
			statusSkillViewManager.skillCategoryTabArray[j].SetActive(PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonFollowUnLockFlag]);
		}
		if (statusSkillViewManager.isSelectSkillLearnTab)
		{
			Transition(learnLink);
		}
		else
		{
			Transition(normalLink);
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
