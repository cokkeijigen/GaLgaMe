using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class HelpSelectScrollListRefresh : StateBehaviour
{
	private HelpDataManager helpDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		helpDataManager = GameObject.Find("Help Data Manager").GetComponent<HelpDataManager>();
	}

	public override void OnStateBegin()
	{
		helpDataManager.DespawnHelpScrollContent();
		int num = int.MinValue;
		switch (helpDataManager.selectTabTypeNum)
		{
		case 0:
		{
			for (int n = 0; n < GameDataManager.instance.helpDataBase.helpCarriageList.Count; n++)
			{
				HelpData helpData6 = GameDataManager.instance.helpDataBase.helpCarriageList[n];
				if (num == int.MinValue)
				{
					num = helpData6.sortID;
				}
				Transform transform6 = PoolManager.Pools["helpPool"].Spawn(helpDataManager.helpScrollPrefabGo);
				RefreshItemList(transform6, n);
				string helpTermName6 = helpData6.helpTermName;
				transform6.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = helpTermName6;
				transform6.GetComponent<HelpListClickAction>().sortID = helpData6.sortID;
			}
			break;
		}
		case 1:
		{
			for (int j = 0; j < GameDataManager.instance.helpDataBase.helpCommandBattleList.Count; j++)
			{
				HelpData helpData2 = GameDataManager.instance.helpDataBase.helpCommandBattleList[j];
				if (num == int.MinValue)
				{
					num = helpData2.sortID;
				}
				Transform transform2 = PoolManager.Pools["helpPool"].Spawn(helpDataManager.helpScrollPrefabGo);
				RefreshItemList(transform2, j);
				string helpTermName2 = helpData2.helpTermName;
				transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = helpTermName2;
				transform2.GetComponent<HelpListClickAction>().sortID = helpData2.sortID;
			}
			break;
		}
		case 2:
		{
			for (int l = 0; l < GameDataManager.instance.helpDataBase.helpDungeonList.Count; l++)
			{
				HelpData helpData4 = GameDataManager.instance.helpDataBase.helpDungeonList[l];
				if (num == int.MinValue)
				{
					num = helpData4.sortID;
				}
				Transform transform4 = PoolManager.Pools["helpPool"].Spawn(helpDataManager.helpScrollPrefabGo);
				RefreshItemList(transform4, l);
				string helpTermName4 = helpData4.helpTermName;
				transform4.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = helpTermName4;
				transform4.GetComponent<HelpListClickAction>().sortID = helpData4.sortID;
			}
			break;
		}
		case 3:
		{
			for (int num2 = 0; num2 < GameDataManager.instance.helpDataBase.helpSurveyList.Count; num2++)
			{
				HelpData helpData7 = GameDataManager.instance.helpDataBase.helpSurveyList[num2];
				if (num == int.MinValue)
				{
					num = helpData7.sortID;
				}
				Transform transform7 = PoolManager.Pools["helpPool"].Spawn(helpDataManager.helpScrollPrefabGo);
				RefreshItemList(transform7, num2);
				string helpTermName7 = helpData7.helpTermName;
				transform7.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = helpTermName7;
				transform7.GetComponent<HelpListClickAction>().sortID = helpData7.sortID;
			}
			break;
		}
		case 4:
		{
			for (int m = 0; m < GameDataManager.instance.helpDataBase.helpSexBattleList.Count; m++)
			{
				HelpData helpData5 = GameDataManager.instance.helpDataBase.helpSexBattleList[m];
				if (num == int.MinValue)
				{
					num = helpData5.sortID;
				}
				Transform transform5 = PoolManager.Pools["helpPool"].Spawn(helpDataManager.helpScrollPrefabGo);
				RefreshItemList(transform5, m);
				string helpTermName5 = helpData5.helpTermName;
				transform5.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = helpTermName5;
				transform5.GetComponent<HelpListClickAction>().sortID = helpData5.sortID;
			}
			break;
		}
		case 5:
		{
			for (int k = 0; k < GameDataManager.instance.helpDataBase.helpMapList.Count; k++)
			{
				HelpData helpData3 = GameDataManager.instance.helpDataBase.helpMapList[k];
				if (num == int.MinValue)
				{
					num = helpData3.sortID;
				}
				Transform transform3 = PoolManager.Pools["helpPool"].Spawn(helpDataManager.helpScrollPrefabGo);
				RefreshItemList(transform3, k);
				string helpTermName3 = helpData3.helpTermName;
				transform3.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = helpTermName3;
				transform3.GetComponent<HelpListClickAction>().sortID = helpData3.sortID;
			}
			break;
		}
		case 6:
		{
			for (int i = 0; i < GameDataManager.instance.helpDataBase.helpStatusList.Count; i++)
			{
				HelpData helpData = GameDataManager.instance.helpDataBase.helpStatusList[i];
				if (num == int.MinValue)
				{
					num = helpData.sortID;
				}
				Transform transform = PoolManager.Pools["helpPool"].Spawn(helpDataManager.helpScrollPrefabGo);
				RefreshItemList(transform, i);
				string helpTermName = helpData.helpTermName;
				transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = helpTermName;
				transform.GetComponent<HelpListClickAction>().sortID = helpData.sortID;
			}
			break;
		}
		}
		helpDataManager.clickedHelpID = num;
		helpDataManager.SetSelectHelpData(num);
		ResetItemListSlider();
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
		transform.SetParent(helpDataManager.helpScrollContentGo.transform);
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
		helpDataManager.helpScrollBar.value = 1f;
	}
}
