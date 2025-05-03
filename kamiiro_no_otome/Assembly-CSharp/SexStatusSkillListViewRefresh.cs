using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SexStatusSkillListViewRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private StatusSkillViewManager statusSkillViewManager;

	private SexStatusManager sexStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusSkillViewManager = GameObject.Find("Skill View Manager").GetComponent<StatusSkillViewManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		statusManager.ResetScrollViewContents(sexStatusManager.sexStatusContentGO.transform, isCustom: false);
		int num = int.MinValue;
		if (!sexStatusManager.isSelectTypePassvie)
		{
			for (int i = 0; i < PlayerSexStatusDataManager.playerUseSexActiveSkillList[statusManager.selectCharacterNum].Count; i++)
			{
				List<SexSkillData> haveSkillList = PlayerSexStatusDataManager.playerUseSexActiveSkillList[statusManager.selectCharacterNum];
				SexSkillData sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == haveSkillList[i].skillID);
				if (sexSkillData != null)
				{
					if (num == int.MinValue)
					{
						num = sexSkillData.skillID;
					}
					Transform transform = PoolManager.Pools["Status Item Pool"].Spawn(sexStatusManager.sexSkillScrollPrefabGo);
					RefreshItemList(transform, i);
					string term = "sexSkill" + sexSkillData.skillID;
					transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
					transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("typeTextLoc").localize.Term = "sexSkillType_" + sexSkillData.skillType;
					string text = sexSkillData.skillType.ToString();
					if (text != "")
					{
						SetItemIconSprite(transform, text);
					}
					transform.GetComponent<ParameterContainer>().SetInt("skillID", sexSkillData.skillID);
				}
				if (num != int.MinValue && !statusSkillViewManager.isLearned)
				{
					sexStatusManager.selectSexSkillId = num;
					ResetItemListSlider();
				}
			}
		}
		else
		{
			int index = statusManager.selectCharacterNum - 1;
			for (int j = 0; j < PlayerSexStatusDataManager.playerUseSexPassiveSkillList[index].Count; j++)
			{
				List<SexHeroinePassiveData> haveSkillList2 = PlayerSexStatusDataManager.playerUseSexPassiveSkillList[index];
				SexHeroinePassiveData sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[index].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == haveSkillList2[j].skillID);
				if (sexHeroinePassiveData != null)
				{
					if (num == int.MinValue)
					{
						num = sexHeroinePassiveData.skillID;
					}
					Transform transform2 = PoolManager.Pools["Status Item Pool"].Spawn(sexStatusManager.sexSkillScrollPrefabGo);
					RefreshItemList(transform2, j);
					string term2 = "sexBodyPassive_" + sexHeroinePassiveData.bodyCategory.ToString() + sexHeroinePassiveData.skillID;
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term2;
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("typeTextLoc").localize.Term = "sexPassiveType_" + sexHeroinePassiveData.passiveType.ToString() + "_short";
					string text2 = sexHeroinePassiveData.passiveType.ToString();
					if (text2 != "")
					{
						SetItemIconSprite(transform2, text2);
					}
					transform2.GetComponent<ParameterContainer>().SetInt("skillID", sexHeroinePassiveData.skillID);
				}
				if (num != int.MinValue && !statusSkillViewManager.isLearned)
				{
					sexStatusManager.selectSexSkillId = num;
					ResetItemListSlider();
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(sexStatusManager.sexStatusContentGO.transform);
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
		sexStatusManager.sexStatusViewArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}
}
