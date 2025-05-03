using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class QuestSelectScrollListRefresh : StateBehaviour
{
	private QuestManager questManager;

	public StateLink stateLink;

	public StateLink noQuestLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
	}

	public override void OnStateBegin()
	{
		questManager.isQuestCleared = false;
		int num = int.MinValue;
		int num2 = int.MinValue;
		switch (questManager.selectTabTypeNum)
		{
		case 0:
		{
			if (PlayerQuestDataManager.enableRequestQuestList.Count <= 0)
			{
				break;
			}
			for (int k = 0; k < PlayerQuestDataManager.enableRequestQuestList.Count; k++)
			{
				QuestData questData3 = PlayerQuestDataManager.enableRequestQuestList[k];
				if (num == int.MinValue)
				{
					num = PlayerQuestDataManager.enableRequestQuestList[k].sortID;
				}
				Transform transform3 = PoolManager.Pools["questPool"].Spawn(questManager.questScrollPrefabGo);
				RefreshItemList(transform3, k);
				string text2 = (questManager.questInfoTextLoc.Term = "quest_" + questData3.sortID + "_title");
				string term3 = text2;
				transform3.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term3;
				transform3.GetComponent<ParameterContainer>().GetVariable<UguiImage>("checkImage").image.gameObject.SetActive(value: false);
				string text7 = questData3.questType.ToString();
				if (text7 != "")
				{
					SetItemIconSprite(transform3, text7);
				}
				QuestListClickAction component4 = transform3.GetComponent<QuestListClickAction>();
				component4.sortID = questData3.sortID;
				component4.isClearedQuest = false;
			}
			break;
		}
		case 1:
		{
			if (PlayerQuestDataManager.enableOrderedQuestList.Count > 0)
			{
				for (int n = 0; n < PlayerQuestDataManager.enableOrderedQuestList.Count; n++)
				{
					QuestData questData6 = PlayerQuestDataManager.enableOrderedQuestList[n];
					QuestClearData questClearData3 = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData6.sortID);
					if (num == int.MinValue)
					{
						num = PlayerQuestDataManager.enableOrderedQuestList[n].sortID;
					}
					Transform transform6 = PoolManager.Pools["questPool"].Spawn(questManager.questScrollPrefabGo);
					RefreshItemList(transform6, n);
					string text2 = (questManager.questInfoTextLoc.Term = "quest_" + questData6.sortID + "_title");
					string term6 = text2;
					ParameterContainer component8 = transform6.GetComponent<ParameterContainer>();
					component8.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term6;
					if (questClearData3.needRequirementCount <= questClearData3.currentRequirementCount)
					{
						component8.GetVariable<UguiImage>("checkImage").image.gameObject.SetActive(value: true);
					}
					else
					{
						component8.GetVariable<UguiImage>("checkImage").image.gameObject.SetActive(value: false);
					}
					string text13 = questData6.questType.ToString();
					if (text13 != "")
					{
						SetItemIconSprite(transform6, text13);
					}
					QuestListClickAction component9 = transform6.GetComponent<QuestListClickAction>();
					component9.sortID = questData6.sortID;
					component9.isClearedQuest = false;
				}
			}
			if (PlayerQuestDataManager.clearedOrderedQuestList.Count <= 0)
			{
				break;
			}
			for (int num3 = 0; num3 < PlayerQuestDataManager.clearedOrderedQuestList.Count; num3++)
			{
				QuestData questData7 = PlayerQuestDataManager.clearedOrderedQuestList[num3];
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData7.sortID);
				if (num2 == int.MinValue)
				{
					num2 = PlayerQuestDataManager.clearedOrderedQuestList[num3].sortID;
				}
				Transform transform7 = PoolManager.Pools["questPool"].Spawn(questManager.clearedQuestScrollPrefabGo);
				RefreshItemList(transform7, num3 + PlayerQuestDataManager.enableOrderedQuestList.Count);
				string text2 = (questManager.questInfoTextLoc.Term = "quest_" + questData7.sortID + "_title");
				string term7 = text2;
				transform7.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term7;
				Debug.Log("クリア済みクエストSortID：" + questData7.sortID);
				string text15 = questData7.questType.ToString();
				if (text15 != "")
				{
					SetItemIconSprite(transform7, text15);
				}
				QuestListClickAction component10 = transform7.GetComponent<QuestListClickAction>();
				component10.sortID = questData7.sortID;
				component10.isClearedQuest = true;
			}
			break;
		}
		case 2:
		{
			if (PlayerQuestDataManager.enableStoryQuestList.Count > 0)
			{
				for (int l = 0; l < PlayerQuestDataManager.enableStoryQuestList.Count; l++)
				{
					QuestData questData4 = PlayerQuestDataManager.enableStoryQuestList[l];
					QuestClearData questClearData2 = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData4.sortID);
					if (questData4.questSubCategory == QuestData.QuestSubCategory.story)
					{
						if (num == int.MinValue)
						{
							num = PlayerQuestDataManager.enableStoryQuestList[l].sortID;
						}
						Transform transform4 = PoolManager.Pools["questPool"].Spawn(questManager.questScrollPrefabGo);
						RefreshItemList(transform4, l);
						string text2 = (questManager.questInfoTextLoc.Term = "quest_" + questData4.sortID + "_title");
						string term4 = text2;
						ParameterContainer component5 = transform4.GetComponent<ParameterContainer>();
						component5.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term4;
						if (questClearData2.needRequirementCount <= questClearData2.currentRequirementCount && questData4.questType != QuestData.QuestType.scenario)
						{
							component5.GetVariable<UguiImage>("checkImage").image.gameObject.SetActive(value: true);
						}
						else
						{
							component5.GetVariable<UguiImage>("checkImage").image.gameObject.SetActive(value: false);
						}
						Debug.Log("クエストSortID：" + questData4.sortID + "／必要数：" + questClearData2.needRequirementCount + "／現在数：" + questClearData2.currentRequirementCount);
						string text9 = questData4.questType.ToString();
						if (text9 != "")
						{
							SetItemIconSprite(transform4, text9);
						}
						QuestListClickAction component6 = transform4.GetComponent<QuestListClickAction>();
						component6.sortID = questData4.sortID;
						component6.isClearedQuest = false;
					}
				}
			}
			if (PlayerQuestDataManager.clearedStoryQuestList.Count <= 0)
			{
				break;
			}
			for (int m = 0; m < PlayerQuestDataManager.clearedStoryQuestList.Count; m++)
			{
				QuestData questData5 = PlayerQuestDataManager.clearedStoryQuestList[m];
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData5.sortID);
				if (questData5.questSubCategory == QuestData.QuestSubCategory.story)
				{
					if (num2 == int.MinValue)
					{
						num2 = PlayerQuestDataManager.clearedStoryQuestList[m].sortID;
					}
					Transform transform5 = PoolManager.Pools["questPool"].Spawn(questManager.clearedQuestScrollPrefabGo);
					RefreshItemList(transform5, m + PlayerQuestDataManager.enableStoryQuestList.Count);
					string text2 = (questManager.questInfoTextLoc.Term = "quest_" + questData5.sortID + "_title");
					string term5 = text2;
					transform5.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term5;
					Debug.Log("クリア済みクエストSortID：" + questData5.sortID);
					string text11 = questData5.questType.ToString();
					if (text11 != "")
					{
						SetItemIconSprite(transform5, text11);
					}
					QuestListClickAction component7 = transform5.GetComponent<QuestListClickAction>();
					component7.sortID = questData5.sortID;
					component7.isClearedQuest = true;
				}
			}
			break;
		}
		case 3:
		{
			if (PlayerQuestDataManager.enableStoryQuestList.Count > 0)
			{
				for (int i = 0; i < PlayerQuestDataManager.enableStoryQuestList.Count; i++)
				{
					QuestData questData = PlayerQuestDataManager.enableStoryQuestList[i];
					QuestClearData questClearData = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID);
					if (questData.questSubCategory == QuestData.QuestSubCategory.sub)
					{
						if (num == int.MinValue)
						{
							num = PlayerQuestDataManager.enableStoryQuestList[i].sortID;
						}
						Transform transform = PoolManager.Pools["questPool"].Spawn(questManager.questScrollPrefabGo);
						RefreshItemList(transform, i);
						string text2 = (questManager.questInfoTextLoc.Term = "quest_" + questData.sortID + "_title");
						string term = text2;
						ParameterContainer component = transform.GetComponent<ParameterContainer>();
						component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
						if (questClearData.needRequirementCount <= questClearData.currentRequirementCount && questData.questType != QuestData.QuestType.scenario)
						{
							component.GetVariable<UguiImage>("checkImage").image.gameObject.SetActive(value: true);
						}
						else
						{
							component.GetVariable<UguiImage>("checkImage").image.gameObject.SetActive(value: false);
						}
						Debug.Log("クエストSortID：" + questData.sortID + "／必要数：" + questClearData.needRequirementCount + "／現在数：" + questClearData.currentRequirementCount);
						string text3 = questData.questType.ToString();
						if (text3 != "")
						{
							SetItemIconSprite(transform, text3);
						}
						QuestListClickAction component2 = transform.GetComponent<QuestListClickAction>();
						component2.sortID = questData.sortID;
						component2.isClearedQuest = false;
					}
				}
			}
			if (PlayerQuestDataManager.clearedStoryQuestList.Count <= 0)
			{
				break;
			}
			for (int j = 0; j < PlayerQuestDataManager.clearedStoryQuestList.Count; j++)
			{
				QuestData questData2 = PlayerQuestDataManager.clearedStoryQuestList[j];
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData2.sortID);
				if (questData2.questSubCategory == QuestData.QuestSubCategory.sub)
				{
					if (num2 == int.MinValue)
					{
						num2 = PlayerQuestDataManager.clearedStoryQuestList[j].sortID;
					}
					Transform transform2 = PoolManager.Pools["questPool"].Spawn(questManager.clearedQuestScrollPrefabGo);
					RefreshItemList(transform2, j + PlayerQuestDataManager.enableStoryQuestList.Count);
					string text2 = (questManager.questInfoTextLoc.Term = "quest_" + questData2.sortID + "_title");
					string term2 = text2;
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term2;
					Debug.Log("クリア済みクエストSortID：" + questData2.sortID);
					string text5 = questData2.questType.ToString();
					if (text5 != "")
					{
						SetItemIconSprite(transform2, text5);
					}
					QuestListClickAction component3 = transform2.GetComponent<QuestListClickAction>();
					component3.sortID = questData2.sortID;
					component3.isClearedQuest = true;
				}
			}
			break;
		}
		}
		if (num != int.MinValue)
		{
			questManager.clickedQuestID = num;
			ResetItemListSlider();
			Transition(stateLink);
		}
		else if (num2 != int.MinValue)
		{
			Debug.Log("未クリアのクエストなし");
			questManager.clickedQuestID = num2;
			questManager.isQuestCleared = true;
			ResetItemListSlider();
			Transition(stateLink);
		}
		else
		{
			Transition(noQuestLink);
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
		transform.SetParent(questManager.questScrollContentGo.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Transform go, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.questIconDictionary[category];
		go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = sprite;
	}

	private void ResetItemListSlider()
	{
		questManager.questScrollBar.value = 1f;
	}
}
