using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerQuestDataManager : SerializedMonoBehaviour
{
	public static List<QuestData> enableRequestQuestList = new List<QuestData>();

	public static List<QuestData> enableOrderedQuestList = new List<QuestData>();

	public static List<QuestData> enableStoryQuestList = new List<QuestData>();

	public static List<QuestData> clearedOrderedQuestList = new List<QuestData>();

	public static List<QuestData> clearedStoryQuestList = new List<QuestData>();

	public static void RefreshOrderedQuestEnemyCount(int targetID)
	{
		List<QuestData> first = enableOrderedQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.extermination).ToList();
		List<QuestData> second = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.extermination).ToList();
		List<QuestData> second2 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.contract).ToList();
		List<QuestData> first2 = first.Union(second).ToList();
		first2 = first2.Union(second2).ToList();
		if (first2 != null)
		{
			for (int i = 0; i < first2.Count; i++)
			{
				int questID = first2[i].sortID;
				if (GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID).requirementList[0] == targetID)
				{
					PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID).currentRequirementCount++;
					Debug.Log("受注クエストの討伐数を追加／クエストID：" + questID + "／ターゲットID：" + targetID);
				}
			}
		}
		else
		{
			Debug.Log("クエストの統合リストはNULL");
		}
	}

	public static void RefreshSupplyQuestHaveItemCount()
	{
		List<QuestData> first = enableRequestQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.supply).ToList();
		List<QuestData> second = enableOrderedQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.supply).ToList();
		List<QuestData> list = first.Union(second).ToList();
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			int questID = list[i].sortID;
			QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID);
			int playerHaveItemNum = PlayerInventoryDataAccess.GetPlayerHaveItemNum(questData.requirementList[0]);
			PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID).currentRequirementCount = playerHaveItemNum;
			Debug.Log("クエストID：" + questID + "／アイテムID：" + questData.requirementList[0] + "／アイテム所持数：" + playerHaveItemNum);
		}
	}

	public static void RefreshCraftQuestHaveItemCount()
	{
		List<QuestData> list = enableOrderedQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.craft).ToList();
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			int questID = list[i].sortID;
			QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID);
			int playerHaveItemNum = PlayerInventoryDataAccess.GetPlayerHaveItemNum(questData.requirementList[0]);
			PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID).currentRequirementCount = playerHaveItemNum;
			Debug.Log("クエストID：" + questID + "／アイテムID：" + questData.requirementList[0] + "／アイテム所持数：" + playerHaveItemNum);
		}
	}

	public static void RefreshStoryQuestFlagData(string addType, int addValue = 0, string scenarioName = "")
	{
		switch (addType)
		{
		case "scenario":
		{
			List<QuestData> list14 = enableStoryQuestList.Where((QuestData data) => data.questEndFlagName == scenarioName).ToList();
			if (list14 == null)
			{
				break;
			}
			for (int num9 = 0; num9 < list14.Count; num9++)
			{
				int questID8 = list14[num9].sortID;
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID8).isClear = true;
			}
			break;
		}
		case "sexScenario":
		{
			List<QuestData> list12 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.sexScenario).ToList();
			Debug.Log("えっちクエストリスト数：" + list12.Count);
			if (list12 == null)
			{
				break;
			}
			for (int num6 = 0; num6 < list12.Count; num6++)
			{
				int questID6 = list12[num6].sortID;
				QuestData questData6 = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID6);
				QuestSexScenarioData questSexScenarioData2 = GameDataManager.instance.questSexScenarioDataBase.questSexScenarioDataList.Find((QuestSexScenarioData data) => data.questItemID == questData6.requirementList[0]);
				int num7 = 0;
				foreach (string needSexScenatioName in questSexScenarioData2.needSexScenatioNameList)
				{
					if (PlayerFlagDataManager.scenarioFlagDictionary[needSexScenatioName])
					{
						num7++;
					}
				}
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID6).currentRequirementCount = num7;
				Debug.Log("クエストID：" + questID6 + "／クリア数：" + num7 + "／必要数：" + questData6.requirementList[1]);
			}
			break;
		}
		case "fertilize":
		{
			List<QuestData> list5 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.fertilize).ToList();
			if (list5 == null)
			{
				break;
			}
			for (int k = 0; k < list5.Count; k++)
			{
				int questID2 = list5[k].sortID;
				QuestData questData2 = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID2);
				QuestSexScenarioData questSexScenarioData = GameDataManager.instance.questSexScenarioDataBase.questSexScenarioDataList.Find((QuestSexScenarioData data) => data.questItemID == questData2.requirementList[0]);
				int num = 0;
				foreach (string needSexScenatioName2 in questSexScenarioData.needSexScenatioNameList)
				{
					if (PlayerFlagDataManager.scenarioFlagDictionary[needSexScenatioName2])
					{
						num++;
					}
				}
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID2).currentRequirementCount = num;
				Debug.Log("クエストID：" + questID2 + "／クリア数：" + num + "／必要数：" + questData2.requirementList[1]);
			}
			break;
		}
		case "heroineContract":
		{
			List<QuestData> list16 = new List<QuestData>();
			List<QuestData> list17 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.heroineContract).ToList();
			for (int num10 = 0; num10 < list17.Count; num10++)
			{
				int num11;
				for (num11 = 0; num11 < list17[num10].questClearFlagNameList.Count; num11++)
				{
					QuestData questData8 = list17.Find((QuestData data) => data.questClearFlagNameList[num11] == scenarioName);
					if (questData8 != null)
					{
						list16.Add(questData8);
					}
				}
			}
			if (list16 == null)
			{
				break;
			}
			for (int num12 = 0; num12 < list16.Count; num12++)
			{
				int questID9 = list16[num12].sortID;
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID9).currentRequirementCount++;
			}
			break;
		}
		case "gameClear":
		{
			List<QuestData> list9 = new List<QuestData>();
			List<QuestData> list10 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.gameClear).ToList();
			for (int num3 = 0; num3 < list10.Count; num3++)
			{
				int num4;
				for (num4 = 0; num4 < list10[num3].questClearFlagNameList.Count; num4++)
				{
					QuestData questData5 = list10.Find((QuestData data) => data.questClearFlagNameList[num4] == scenarioName);
					if (questData5 != null)
					{
						list9.Add(questData5);
					}
				}
			}
			if (list9 == null)
			{
				break;
			}
			for (int num5 = 0; num5 < list9.Count; num5++)
			{
				int questID5 = list9[num5].sortID;
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID5).currentRequirementCount++;
			}
			break;
		}
		case "sexScenarioClear":
		{
			List<QuestData> list2 = new List<QuestData>();
			List<QuestData> list3 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.sexScenarioClear).ToList();
			for (int i = 0; i < list3.Count; i++)
			{
				int n;
				for (n = 0; n < list3[i].questClearFlagNameList.Count; n++)
				{
					QuestData questData = list3.Find((QuestData data) => data.questClearFlagNameList[n] == scenarioName);
					if (questData != null)
					{
						list2.Add(questData);
					}
				}
			}
			if (list2 == null)
			{
				break;
			}
			for (int j = 0; j < list2.Count; j++)
			{
				int questID = list2[j].sortID;
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID).currentRequirementCount++;
			}
			break;
		}
		case "craft":
		{
			List<QuestData> list13 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.craft).ToList();
			if (list13 == null)
			{
				break;
			}
			for (int num8 = 0; num8 < list13.Count; num8++)
			{
				int questID7 = list13[num8].sortID;
				QuestData questData7 = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID7);
				int playerHaveItemNum2 = PlayerInventoryDataAccess.GetPlayerHaveItemNum(questData7.requirementList[0]);
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID7).currentRequirementCount = playerHaveItemNum2;
				Debug.Log("クエストID：" + questID7 + "／アイテムID：" + questData7.requirementList[0] + "／アイテム所持数：" + playerHaveItemNum2);
			}
			break;
		}
		case "itemGet":
		{
			List<QuestData> list19 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.itemGet).ToList();
			if (list19 == null)
			{
				break;
			}
			for (int num13 = 0; num13 < list19.Count; num13++)
			{
				int questID10 = list19[num13].sortID;
				QuestData questData9 = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID10);
				int playerHaveItemNum3 = PlayerInventoryDataAccess.GetPlayerHaveItemNum(questData9.requirementList[0]);
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID10).currentRequirementCount = playerHaveItemNum3;
				Debug.Log("クエストID：" + questID10 + "／アイテムID：" + questData9.requirementList[0] + "／アイテム所持数：" + playerHaveItemNum3);
			}
			break;
		}
		case "itemShop":
		{
			List<QuestData> list8 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.itemShop).ToList();
			if (list8 == null)
			{
				break;
			}
			for (int m = 0; m < list8.Count; m++)
			{
				int questID4 = list8[m].sortID;
				QuestData questData4 = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID4);
				int playerHaveItemNum = PlayerInventoryDataAccess.GetPlayerHaveItemNum(questData4.requirementList[0]);
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID4).currentRequirementCount = playerHaveItemNum;
				Debug.Log("クエストID：" + questID4 + "／アイテムID：" + questData4.requirementList[0] + "／アイテム所持数：" + playerHaveItemNum);
			}
			break;
		}
		case "skillLearn":
		{
			List<QuestData> list18 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.skillLearn).ToList();
			if (list18 != null && list18.Count > 0)
			{
				RefreshQuestFlagData_Add(list18, addValue);
			}
			break;
		}
		case "dungeonClear":
		{
			List<QuestData> list7 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.dungeonClear).ToList();
			if (list7 == null)
			{
				break;
			}
			for (int l = 0; l < list7.Count; l++)
			{
				int questID3 = list7[l].sortID;
				QuestData questData3 = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID3);
				QuestDungeonClearData questDungeonClearData = GameDataManager.instance.questDungeonClearDataBase.questDungeonClearDataList.Find((QuestDungeonClearData data) => data.questItemID == questData3.requirementList[0]);
				int num2 = 0;
				foreach (string needClearDungeonName in questDungeonClearData.needClearDungeonNameList)
				{
					if (PlayerFlagDataManager.dungeonDeepClearFlagDictionary[needClearDungeonName])
					{
						num2++;
					}
				}
				PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID3).currentRequirementCount = num2;
				Debug.Log("クエストID：" + questID3 + "／クリア数：" + num2 + "／必要数：" + questData3.requirementList[1]);
			}
			break;
		}
		case "carriageStore":
		{
			List<QuestData> list4 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.carriageStore).ToList();
			if (list4 != null)
			{
				RefreshQuestFlagData_Add(list4, addValue);
			}
			break;
		}
		case "facilityLv":
		{
			List<QuestData> list15 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.facilityLv).ToList();
			if (list15 != null)
			{
				RefreshQuestFlagData_New(list15, addValue);
			}
			break;
		}
		case "facilityToolLv":
		{
			List<QuestData> list11 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.facilityToolLv).ToList();
			if (list11 != null)
			{
				RefreshQuestFlagData_New(list11, addValue);
			}
			break;
		}
		case "furnaceLv":
		{
			List<QuestData> list6 = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.furnaceLv).ToList();
			if (list6 != null)
			{
				RefreshQuestFlagData_New(list6, addValue);
			}
			break;
		}
		case "addOnLv":
		{
			List<QuestData> list = enableStoryQuestList.Where((QuestData data) => data.questType == QuestData.QuestType.addOnLv).ToList();
			if (list != null)
			{
				RefreshQuestFlagData_New(list, addValue);
			}
			break;
		}
		}
	}

	private static void RefreshQuestFlagData_Add(List<QuestData> scenarioList, int addValue)
	{
		for (int i = 0; i < scenarioList.Count; i++)
		{
			int questID = scenarioList[i].sortID;
			PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID).currentRequirementCount += addValue;
		}
	}

	private static void RefreshQuestFlagData_New(List<QuestData> scenarioList, int newValue)
	{
		for (int i = 0; i < scenarioList.Count; i++)
		{
			int questID = scenarioList[i].sortID;
			PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID).currentRequirementCount = newValue;
		}
	}

	public static void RefreshEnableRequestQuestList()
	{
		enableRequestQuestList.Clear();
		for (int i = 0; i < PlayerFlagDataManager.questClearFlagList.Count; i++)
		{
			int questID = PlayerFlagDataManager.questClearFlagList[i].sortID;
			QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID);
			bool flag = false;
			for (int j = 0; j < questData.questStartFlagNameList.Count; j++)
			{
				if (!PlayerFlagDataManager.scenarioFlagDictionary[questData.questStartFlagNameList[j]])
				{
					flag = true;
					break;
				}
			}
			if (questData.questCategory == QuestData.QuestCategory.order && !flag && !PlayerFlagDataManager.questClearFlagList[i].isClear && !PlayerFlagDataManager.questClearFlagList[i].isOrdered)
			{
				enableRequestQuestList.Add(questData);
			}
		}
		enableRequestQuestList = enableRequestQuestList.OrderBy((QuestData data) => data.sortID).ToList();
	}

	public static void RefreshEnableOrderedQuestList()
	{
		enableOrderedQuestList.Clear();
		clearedOrderedQuestList.Clear();
		for (int i = 0; i < PlayerFlagDataManager.questClearFlagList.Count; i++)
		{
			int questID = PlayerFlagDataManager.questClearFlagList[i].sortID;
			QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID);
			if (questData.questCategory == QuestData.QuestCategory.order)
			{
				if (PlayerFlagDataManager.questClearFlagList[i].isOrdered && !PlayerFlagDataManager.questClearFlagList[i].isClear)
				{
					enableOrderedQuestList.Add(questData);
				}
				if (PlayerFlagDataManager.questClearFlagList[i].isOrdered && PlayerFlagDataManager.questClearFlagList[i].isClear)
				{
					clearedOrderedQuestList.Add(questData);
				}
			}
		}
		enableOrderedQuestList = enableOrderedQuestList.OrderBy((QuestData data) => data.sortID).ToList();
		clearedOrderedQuestList = clearedOrderedQuestList.OrderBy((QuestData data) => data.sortID).ToList();
	}

	public static void RefreshEnableStoryQuestList()
	{
		enableStoryQuestList.Clear();
		clearedStoryQuestList.Clear();
		for (int i = 0; i < PlayerFlagDataManager.questClearFlagList.Count; i++)
		{
			int questID = PlayerFlagDataManager.questClearFlagList[i].sortID;
			QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questID);
			bool flag = false;
			for (int j = 0; j < questData.questStartFlagNameList.Count; j++)
			{
				if (!PlayerFlagDataManager.scenarioFlagDictionary[questData.questStartFlagNameList[j]])
				{
					flag = true;
					break;
				}
			}
			if (questData.questCategory == QuestData.QuestCategory.story)
			{
				if (!flag && !PlayerFlagDataManager.questClearFlagList[i].isClear)
				{
					enableStoryQuestList.Add(questData);
				}
				if (!flag && PlayerFlagDataManager.questClearFlagList[i].isClear)
				{
					clearedStoryQuestList.Add(questData);
				}
			}
		}
		enableStoryQuestList = enableStoryQuestList.OrderBy((QuestData data) => data.sortID).ToList();
		clearedStoryQuestList = clearedStoryQuestList.OrderBy((QuestData data) => data.sortID).ToList();
	}

	public static void CheckUnreportedQuest()
	{
		PlayerNonSaveDataManager.isUnreportedQuest = false;
		for (int i = 0; i < enableOrderedQuestList.Count; i++)
		{
			int questID = enableOrderedQuestList[i].sortID;
			QuestClearData questClearData = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID);
			if (questClearData.currentRequirementCount >= questClearData.needRequirementCount && questClearData.isOrdered)
			{
				PlayerNonSaveDataManager.isUnreportedQuest = true;
				Debug.Log("未報告のクエストID：" + questClearData.sortID);
				return;
			}
		}
		for (int j = 0; j < enableStoryQuestList.Count; j++)
		{
			int questID2 = enableStoryQuestList[j].sortID;
			QuestClearData questClearData2 = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questID2);
			if (questClearData2.currentRequirementCount >= questClearData2.needRequirementCount)
			{
				PlayerNonSaveDataManager.isUnreportedQuest = true;
				Debug.Log("未報告のクエストID：" + questClearData2.sortID);
				break;
			}
		}
	}

	public static bool CheckStoryQuestEnable(int questId)
	{
		bool flag = true;
		QuestData questData = GameDataManager.instance.questDataBase.questDataList.Find((QuestData data) => data.sortID == questId);
		for (int i = 0; i < questData.questStartFlagNameList.Count; i++)
		{
			if (!PlayerFlagDataManager.scenarioFlagDictionary[questData.questStartFlagNameList[i]])
			{
				flag = false;
				break;
			}
		}
		if (flag && PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questId).isClear)
		{
			flag = false;
		}
		return flag;
	}
}
