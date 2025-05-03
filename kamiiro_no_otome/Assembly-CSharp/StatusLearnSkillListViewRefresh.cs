using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusLearnSkillListViewRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private StatusSkillViewManager statusSkillViewManager;

	public StateLink stateLink;

	public StateLink noSpawnLink;

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
		statusSkillViewManager.skillCategoryTabArray[5].GetComponent<Image>().sprite = statusSkillViewManager.skillCategoryTabSpriteArray[1];
		int num = int.MinValue;
		for (int i = 0; i < PlayerEquipDataManager.playerHaveSkillList[PlayerStatusDataManager.partyMemberCount].Count; i++)
		{
			List<int> haveSkillList = PlayerEquipDataManager.playerHaveSkillList[PlayerStatusDataManager.partyMemberCount];
			BattleSkillData battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == haveSkillList[i]);
			bool flag = false;
			bool flag2 = false;
			if (!string.IsNullOrEmpty(battleSkillData.learnScenarioName))
			{
				flag = PlayerFlagDataManager.scenarioFlagDictionary[battleSkillData.learnScenarioName];
				Debug.Log("スキルID：" + battleSkillData.skillID + "／習得シナリオをクリア済み");
			}
			else
			{
				string characterPowerUpFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[battleSkillData.unlockFlagCharacter].characterPowerUpFlag;
				flag2 = PlayerFlagDataManager.scenarioFlagDictionary[characterPowerUpFlag];
			}
			if ((battleSkillData != null && flag2) || (battleSkillData != null && flag))
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
				int learnCost = battleSkillData.learnCost;
				if (PlayerDataManager.playerHaveKizunaPoint >= learnCost)
				{
					statusSkillViewManager.skillLearnButtonGO.GetComponent<CanvasGroup>().alpha = 1f;
					statusSkillViewManager.skillLearnButtonGO.GetComponent<CanvasGroup>().interactable = true;
				}
				else
				{
					statusSkillViewManager.skillLearnButtonGO.GetComponent<CanvasGroup>().alpha = 0.5f;
					statusSkillViewManager.skillLearnButtonGO.GetComponent<CanvasGroup>().interactable = false;
				}
				bool flag3 = statusManager.CheckSkillIsLearned(battleSkillData.skillID);
				transform.GetComponent<ParameterContainer>().GetGameObject("starImage").SetActive(flag3);
				transform.GetComponent<ParameterContainer>().SetBool("isLearned", flag3);
				bool active = statusManager.CheckSkillIsEquiped(battleSkillData.skillID, statusManager.selectCharacterNum);
				transform.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(active);
				string text = battleSkillData.skillType.ToString();
				if (text != "")
				{
					SetItemIconSprite(transform, text);
				}
				transform.GetComponent<ParameterContainer>().SetInt("skillID", battleSkillData.skillID);
			}
		}
		statusSkillViewManager.skillScrollMpTextLoc.Term = "summaryUseMp";
		statusSkillViewManager.kizunaPointText.text = PlayerDataManager.playerHaveKizunaPoint.ToString();
		statusSkillViewManager.kizunaFrameGo.SetActive(value: true);
		statusSkillViewManager.skillLearnButtonGO.SetActive(value: true);
		statusManager.skillSelectWIndowScrollView.offsetMin = new Vector2(23f, 104f);
		if (num != int.MinValue && !statusSkillViewManager.isLearned)
		{
			statusManager.selectSkillId = num;
			ResetItemListSlider();
		}
		if (statusManager.skillContentGO.transform.childCount > 0)
		{
			Transition(stateLink);
		}
		else
		{
			Transition(noSpawnLink);
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
