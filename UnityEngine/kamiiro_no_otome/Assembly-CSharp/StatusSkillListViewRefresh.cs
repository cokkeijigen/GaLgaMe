using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusSkillListViewRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private StatusSkillViewManager statusSkillViewManager;

	public StateLink stateLink;

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
		statusSkillViewManager.skillCategoryTabArray[statusManager.selectCharacterNum].GetComponent<Image>().sprite = statusSkillViewManager.skillCategoryTabSpriteArray[1];
		int num = int.MinValue;
		for (int i = 0; i < PlayerEquipDataManager.playerHaveSkillList[statusManager.selectCharacterNum].Count; i++)
		{
			List<int> haveSkillList = PlayerEquipDataManager.playerHaveSkillList[statusManager.selectCharacterNum];
			BattleSkillData battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == haveSkillList[i]);
			if (battleSkillData != null)
			{
				if (num == int.MinValue)
				{
					num = battleSkillData.skillID;
				}
				Transform transform = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[5]);
				RefreshItemList(transform, i);
				string term = "playerSkill" + battleSkillData.skillID;
				transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
				transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("powerText").text.text = battleSkillData.skillPower.ToString();
				transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("mpText").text.text = battleSkillData.useMP.ToString();
				transform.GetComponent<ParameterContainer>().GetGameObject("starImage").SetActive(value: false);
				transform.GetComponent<ParameterContainer>().SetBool("isLearned", value: false);
				string text = battleSkillData.skillType.ToString();
				if (text != "")
				{
					SetItemIconSprite(transform, text);
				}
				transform.GetComponent<ParameterContainer>().SetInt("skillID", battleSkillData.skillID);
			}
		}
		if (statusManager.selectCharacterNum == 0)
		{
			statusSkillViewManager.skillScrollMpTextLoc.Term = "summaryUseTp";
		}
		else
		{
			statusSkillViewManager.skillScrollMpTextLoc.Term = "summaryUseMp";
		}
		statusSkillViewManager.kizunaFrameGo.SetActive(value: false);
		statusSkillViewManager.skillLearnButtonGO.SetActive(value: false);
		statusManager.skillSelectWIndowScrollView.offsetMin = new Vector2(23f, 30f);
		if (num != int.MinValue && !statusSkillViewManager.isLearned)
		{
			statusManager.selectSkillId = num;
			ResetItemListSlider();
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(statusManager.skillContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Transform go, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[category];
		go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = sprite;
	}

	private void ResetItemListSlider()
	{
		statusManager.skillViewArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}
}
