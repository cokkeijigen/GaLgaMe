using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshQuestInfoWindow : StateBehaviour
{
	private QuestManager questManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
	}

	public override void OnStateBegin()
	{
		QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questManager.clickedQuestID);
		questManager.questInfoTextLoc.Term = "quest_" + questData.sortID + "_summary";
		questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentCount";
		questManager.questInfoFrameGo.SetActive(value: true);
		questManager.requirementFrameGo.SetActive(value: true);
		questManager.rewardFrameGo.SetActive(value: true);
		questManager.questApplyButtonGo.SetActive(value: true);
		questManager.questNotSelectFrameGo.SetActive(value: false);
		questManager.questRequireItemImage.gameObject.SetActive(value: true);
		questManager.questRequireEnemyImage.gameObject.SetActive(value: false);
		switch (questData.questType)
		{
		case QuestData.QuestType.extermination:
			if (questData.sortID == 1100)
			{
				questManager.questRequireNameTextLoc.Term = "questType_quest1100";
				questManager.questRequireTypeTextLoc.Term = "empty";
				questManager.questRequireEnemyImage.sprite = questData.questRequirementItemImage;
			}
			else
			{
				questManager.questRequireNameTextLoc.Term = "enemy" + questData.requirementList[0];
				questManager.questRequireTypeTextLoc.Term = "questType_extermination";
				questManager.questRequireEnemyImage.sprite = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == questData.requirementList[0]).enemyImageMiniSprite;
			}
			questManager.questRequireItemImage.gameObject.SetActive(value: false);
			questManager.questRequireEnemyImage.gameObject.SetActive(value: true);
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		case QuestData.QuestType.supply:
		{
			string itemNameTerm4 = PlayerInventoryDataAccess.GetItemNameTerm(questData.requirementList[0]);
			Sprite itemSprite5 = PlayerInventoryDataAccess.GetItemSprite(questData.requirementList[0]);
			questManager.questRequireNameTextLoc.Term = itemNameTerm4;
			questManager.questRequireTypeTextLoc.Term = "questType_supply";
			questManager.questRequireItemImage.sprite = itemSprite5;
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentHaveCount";
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.totalSalesAmount:
		{
			PlayerInventoryDataAccess.GetItemNameTerm(questData.requirementList[0]);
			Sprite itemSprite4 = PlayerInventoryDataAccess.GetItemSprite(questData.requirementList[0]);
			questManager.questRequireNameTextLoc.Term = "questType_totalSalesAmount";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = itemSprite4;
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentCount";
			questManager.questRequireCurrentCountText.text = PlayerDataManager.totalSalesAmount.ToString();
			break;
		}
		case QuestData.QuestType.scenario:
			questManager.requirementFrameGo.SetActive(value: false);
			questManager.rewardFrameGo.SetActive(value: false);
			questManager.questApplyButtonGo.SetActive(value: false);
			break;
		case QuestData.QuestType.contract:
		{
			questManager.questRequireNameTextLoc.Term = "enemyDivineSprit_" + questData.requirementList[0];
			questManager.questRequireTypeTextLoc.Term = "questType_contract";
			questManager.questRequireItemImage.gameObject.SetActive(value: false);
			questManager.questRequireEnemyImage.gameObject.SetActive(value: true);
			string text = questData.sortID.ToString().Substring(1, 1);
			Debug.Log($"クエストID：{questData.sortID}／ヒロインID：{text}");
			questManager.questRequireEnemyImage.sprite = GameDataManager.instance.playerResultFrameSprite[int.Parse(text)];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.heroineContract:
		{
			string term3 = "";
			int num = 0;
			switch (questData.sortID)
			{
			case 1199:
				term3 = "character2";
				num = 2;
				break;
			case 1299:
				term3 = "character3";
				num = 3;
				break;
			case 1399:
				term3 = "character4";
				num = 4;
				break;
			}
			questManager.questRequireNameTextLoc.Term = term3;
			questManager.questRequireTypeTextLoc.Term = "questType_contract";
			questManager.questRequireItemImage.gameObject.SetActive(value: false);
			questManager.questRequireEnemyImage.gameObject.SetActive(value: true);
			questManager.questRequireEnemyImage.sprite = GameDataManager.instance.playerResultFrameSprite[num];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.sexScenario:
		{
			string term2 = "";
			switch (questData.sortID)
			{
			case 1150:
			case 1151:
				term2 = "character1";
				break;
			case 1250:
			case 1251:
				term2 = "character2";
				break;
			case 1350:
			case 1351:
				term2 = "character3";
				break;
			case 1450:
				term2 = "character4";
				break;
			case 3010:
				term2 = "characterCharlo";
				break;
			}
			questManager.questRequireNameTextLoc.Term = term2;
			questManager.questRequireTypeTextLoc.Term = "questType_sexScenario";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["sexScenario"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentCount";
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.fertilize:
		{
			string term = "";
			switch (questData.sortID)
			{
			case 1252:
				term = "character2";
				break;
			case 1352:
				term = "character3";
				break;
			case 1451:
				term = "character4";
				break;
			}
			questManager.questRequireNameTextLoc.Term = term;
			questManager.questRequireTypeTextLoc.Term = "questType_fertilize";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["sexScenario"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentCount";
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.gameClear:
			questManager.questRequireNameTextLoc.Term = "questType_gameClear";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["gameClear"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentCount";
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		case QuestData.QuestType.sexScenarioClear:
			questManager.questRequireNameTextLoc.Term = "questType_quest" + questData.sortID;
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["sexScenario"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentCount";
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		case QuestData.QuestType.craft:
		{
			string itemNameTerm3 = PlayerInventoryDataAccess.GetItemNameTerm(questData.requirementList[0]);
			Sprite itemSprite3 = PlayerInventoryDataAccess.GetItemSprite(questData.requirementList[0]);
			questManager.questRequireNameTextLoc.Term = itemNameTerm3;
			questManager.questRequireTypeTextLoc.Term = "questType_craft";
			questManager.questRequireItemImage.sprite = itemSprite3;
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentTextLoc.Term = "questRequireCurrentHaveCount";
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.carriageStore:
			questManager.questRequireNameTextLoc.Term = "questType_carriageStore";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["carriageStore"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		case QuestData.QuestType.itemGet:
		{
			string itemNameTerm2 = PlayerInventoryDataAccess.GetItemNameTerm(questData.requirementList[0]);
			Sprite itemSprite2 = PlayerInventoryDataAccess.GetItemSprite(questData.requirementList[0]);
			questManager.questRequireNameTextLoc.Term = itemNameTerm2;
			questManager.questRequireTypeTextLoc.Term = "questType_itemGet";
			questManager.questRequireItemImage.sprite = itemSprite2;
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.itemShop:
		{
			string itemNameTerm = PlayerInventoryDataAccess.GetItemNameTerm(questData.requirementList[0]);
			Sprite itemSprite = PlayerInventoryDataAccess.GetItemSprite(questData.requirementList[0]);
			questManager.questRequireNameTextLoc.Term = itemNameTerm;
			questManager.questRequireTypeTextLoc.Term = "questType_itemShop";
			questManager.questRequireItemImage.sprite = itemSprite;
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		}
		case QuestData.QuestType.skillLearn:
			questManager.questRequireNameTextLoc.Term = "questType_skillLearn";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["skillLearn"];
			questManager.questRequireNeedCountText.text = "1";
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		case QuestData.QuestType.dungeonClear:
			switch (questData.sortID)
			{
			case 1030:
				questManager.questRequireNameTextLoc.Term = "questType_dungeonDeepCount1";
				break;
			case 1031:
				questManager.questRequireNameTextLoc.Term = "questType_dungeonDeepCount2";
				break;
			}
			questManager.questRequireTypeTextLoc.Term = "questType_dungeonDeepClear";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["dungeonClear"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questData.sortID).currentRequirementCount.ToString();
			break;
		case QuestData.QuestType.facilityLv:
			questManager.questRequireNameTextLoc.Term = "questType_facilityLv";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["carriage"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv.ToString();
			break;
		case QuestData.QuestType.facilityToolLv:
			questManager.questRequireNameTextLoc.Term = "questType_facilityToolLv";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["carriage"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv.ToString();
			break;
		case QuestData.QuestType.furnaceLv:
			questManager.questRequireNameTextLoc.Term = "questType_furnaceLv";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["carriage"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].furnaceLv.ToString();
			break;
		case QuestData.QuestType.addOnLv:
			questManager.questRequireNameTextLoc.Term = "questType_addOnLv";
			questManager.questRequireTypeTextLoc.Term = "empty";
			questManager.questRequireItemImage.sprite = questManager.questRequireImageDictionary["carriage"];
			questManager.questRequireNeedCountText.text = questData.requirementList[1].ToString();
			questManager.questRequireCurrentCountText.text = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv.ToString();
			break;
		}
		questManager.questInfoScrollBar.value = 1f;
		if (questData.rewardList != null)
		{
			questManager.ResetScrollViewContents("reward");
			for (int i = 0; i < questData.rewardList.Count; i++)
			{
				Transform obj = PoolManager.Pools["questPool"].Spawn(questManager.questRewardPrefabGo, questManager.questRewardContentGo.transform);
				obj.localScale = new Vector3(1f, 1f, 1f);
				ParameterContainer component = obj.GetComponent<ParameterContainer>();
				string itemNameTerm5 = PlayerInventoryDataAccess.GetItemNameTerm(questData.rewardList[i][0]);
				Sprite itemSprite6 = PlayerInventoryDataAccess.GetItemSprite(questData.rewardList[i][0]);
				component.SetInt("itemID", questData.rewardList[i][0]);
				component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = itemNameTerm5;
				component.GetVariable<UguiImage>("itemImage").image.sprite = itemSprite6;
				if (questData.rewardList[i][0] == 9030)
				{
					component.GetVariable<TmpText>("itemCountText").textMeshProUGUI.text = "10";
				}
				else
				{
					component.GetVariable<TmpText>("itemCountText").textMeshProUGUI.text = questData.rewardList[i][1].ToString();
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
