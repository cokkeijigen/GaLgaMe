using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusSkillCustomListRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
	}

	public override void OnStateBegin()
	{
		statusManager.ResetScrollViewContents(statusCustomManager.statusCustomContentGo.transform, isCustom: true);
		statusCustomManager.customScrollContentIndex = 0;
		int num = int.MinValue;
		if (statusManager.selectCharacterNum == 0 && statusManager.selectItemId > 1300)
		{
			for (int i = 0; i < PlayerInventoryDataManager.playerLearnedSkillList.Count; i++)
			{
				List<LearnedSkillData> learnSkillList = PlayerInventoryDataManager.playerLearnedSkillList;
				BattleSkillData battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == learnSkillList[i].skillID);
				if (battleSkillData != null)
				{
					Transform transform = PoolManager.Pools["Status Custom Pool"].Spawn(statusCustomManager.customScrollPrefabArray[0]);
					RefreshItemList(transform, i);
					string text = "playerSkill" + battleSkillData.skillID;
					Debug.Log("エデンの防具を表示／ID：" + text);
					transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = text;
					transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("powerText").text.text = battleSkillData.skillPower.ToString();
					transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("mpText").text.text = battleSkillData.useMP.ToString();
					StatusSkillCustomListClickAction component = transform.GetComponent<StatusSkillCustomListClickAction>();
					component.isInitialized = false;
					if (statusCustomManager.tempEquipSkillList.Contains(battleSkillData.skillID))
					{
						transform.GetComponent<ParameterContainer>().GetVariable<UguiToggle>("equipToggle").toggle.isOn = true;
					}
					else
					{
						transform.GetComponent<ParameterContainer>().GetVariable<UguiToggle>("equipToggle").toggle.isOn = false;
					}
					transform.GetComponent<ParameterContainer>().GetGameObject("starImage").SetActive(value: true);
					transform.GetComponent<ParameterContainer>().SetBool("isLearned", value: true);
					string text2 = battleSkillData.skillType.ToString();
					if (text2 != "")
					{
						SetItemIconSprite(transform, text2);
					}
					transform.GetComponent<ParameterContainer>().SetInt("skillID", battleSkillData.skillID);
					component.isInitialized = true;
				}
			}
		}
		else
		{
			for (int j = 0; j < PlayerEquipDataManager.playerHaveSkillList[statusManager.selectCharacterNum].Count; j++)
			{
				List<int> haveSkillList = PlayerEquipDataManager.playerHaveSkillList[statusManager.selectCharacterNum];
				BattleSkillData battleSkillData2 = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == haveSkillList[j]);
				if (battleSkillData2 != null)
				{
					if (num == int.MinValue)
					{
						num = battleSkillData2.skillID;
					}
					Transform transform2 = PoolManager.Pools["Status Custom Pool"].Spawn(statusCustomManager.customScrollPrefabArray[0]);
					RefreshItemList(transform2, j);
					string text3 = "playerSkill" + battleSkillData2.skillID;
					Debug.Log("エデンの武器or仲間の武器を表示／ID：" + text3);
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = text3;
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("powerText").text.text = battleSkillData2.skillPower.ToString();
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("mpText").text.text = battleSkillData2.useMP.ToString();
					StatusSkillCustomListClickAction component2 = transform2.GetComponent<StatusSkillCustomListClickAction>();
					component2.isInitialized = false;
					if (statusCustomManager.tempEquipSkillList.Contains(battleSkillData2.skillID))
					{
						transform2.GetComponent<ParameterContainer>().GetVariable<UguiToggle>("equipToggle").toggle.isOn = true;
					}
					else
					{
						transform2.GetComponent<ParameterContainer>().GetVariable<UguiToggle>("equipToggle").toggle.isOn = false;
					}
					transform2.GetComponent<ParameterContainer>().GetGameObject("starImage").SetActive(value: false);
					transform2.GetComponent<ParameterContainer>().SetBool("isLearned", value: false);
					string text4 = battleSkillData2.skillType.ToString();
					if (text4 != "")
					{
						SetItemIconSprite(transform2, text4);
					}
					transform2.GetComponent<ParameterContainer>().SetInt("skillID", battleSkillData2.skillID);
					component2.isInitialized = true;
				}
			}
		}
		if (num != int.MinValue)
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
		transform.SetParent(statusCustomManager.statusCustomContentGo.transform);
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
		statusCustomManager.customWindowArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}
}
