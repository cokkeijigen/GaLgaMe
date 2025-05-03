using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerDungeonScenarioDataManager : SerializedMonoBehaviour
{
	public static string CheckDungeonFloorEvent(string dungeonName, int floorNum)
	{
		string text = "";
		ScenarioFlagData scenarioFlagData = new ScenarioFlagData();
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == PlayerDataManager.currentDungeonName && data.scenarioType == "dungeonFloor" && data.dungeonFloorNum == floorNum).ToList();
		List<ScenarioFlagData> list2 = new List<ScenarioFlagData>(list);
		Debug.Log("Whereリスト数：" + list.Count + "／ダンジョン名：" + dungeonName);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[list[num].scenarioName])
			{
				list2.RemoveAt(num);
				Debug.Log("ダンジョン／クリア済みのものをリストから削除／ダンジョン名：" + dungeonName);
			}
		}
		if (list2 != null && list2.Count > 0)
		{
			scenarioFlagData = list2.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			Debug.Log("１つ以上ある／ダンジョン名：" + scenarioFlagData.scenarioLocationName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			flag = true;
			scenarioFlagData = list.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
		}
		if (!flag)
		{
			dictionary = CheckDungeonScenarioFlag(scenarioFlagData);
		}
		bool flag2 = dictionary.All((KeyValuePair<string, bool> data) => !data.Value);
		if (!flag && flag2)
		{
			text = scenarioFlagData.scenarioName;
			Debug.Log("イベント発生あり／ダンジョン名：" + dungeonName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			text = "";
			SetUnachievedFlagDebugLog(dictionary, "dungeonFloor", PlayerDataManager.currentDungeonName);
		}
		return text;
	}

	public static string CheckDungeonSexEvent(string dungeonName, int floorNum)
	{
		string text = "";
		ScenarioFlagData scenarioFlagData = new ScenarioFlagData();
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == PlayerDataManager.currentDungeonName && data.scenarioType == "dungeonSex").ToList();
		List<ScenarioFlagData> list2 = new List<ScenarioFlagData>(list);
		Debug.Log("Whereリスト数：" + list.Count + "／ダンジョン名：" + dungeonName);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[list[num].scenarioName])
			{
				list2.RemoveAt(num);
				Debug.Log("ダンジョンえっち／クリア済みのものをリストから削除／ダンジョン名：" + dungeonName);
			}
		}
		if (list2 != null && list2.Count > 0)
		{
			scenarioFlagData = list2.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			Debug.Log("１つ以上ある／ダンジョン名：" + scenarioFlagData.scenarioLocationName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			flag = true;
			scenarioFlagData = list.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
		}
		if (!flag)
		{
			dictionary = CheckDungeonScenarioFlag(scenarioFlagData);
		}
		bool flag2 = dictionary.All((KeyValuePair<string, bool> data) => !data.Value);
		if (!flag && flag2)
		{
			text = scenarioFlagData.scenarioName;
			Debug.Log("イチャイチャイベント発生あり／ダンジョン名：" + dungeonName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			text = "";
			SetUnachievedFlagDebugLog(dictionary, "dungeonSex", PlayerDataManager.currentDungeonName);
		}
		return text;
	}

	private static Dictionary<string, bool> CheckDungeonScenarioFlag(ScenarioFlagData checkScenarioFlagData)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>
		{
			{ "isFlagNotClear", false },
			{ "isQuestFlagNotClear", false },
			{ "isEventItemNotHave", false },
			{ "isWeekMissMatch", false },
			{ "isTimeMissMatch", false },
			{ "isSoloOnly", false },
			{ "isNotNeedFollowHeroine", false },
			{ "isNotStartingDayFlag", false }
		};
		for (int i = 0; i < checkScenarioFlagData.needScenarioFlagNameList.Count; i++)
		{
			string key = checkScenarioFlagData.needScenarioFlagNameList[i];
			if (!PlayerFlagDataManager.scenarioFlagDictionary[key])
			{
				dictionary["isFlagNotClear"] = true;
				break;
			}
		}
		if (checkScenarioFlagData.needQuestIdList.Count > 0)
		{
			for (int j = 0; j < checkScenarioFlagData.needQuestIdList.Count; j++)
			{
				int questId = checkScenarioFlagData.needQuestIdList[j];
				if (!PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == questId).isClear)
				{
					dictionary["isQuestFlagNotClear"] = true;
					break;
				}
			}
		}
		if (checkScenarioFlagData.needEventItemIdList.Count > 0)
		{
			for (int k = 0; k < checkScenarioFlagData.needEventItemIdList.Count; k++)
			{
				int itemId = checkScenarioFlagData.needEventItemIdList[k];
				if (PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData data) => data.itemID == itemId) == null)
				{
					dictionary["isEventItemNotHave"] = true;
					break;
				}
			}
		}
		for (int l = 0; l < checkScenarioFlagData.weekOfConditions.Count; l++)
		{
			if (PlayerDataManager.currentWeekDay == checkScenarioFlagData.weekOfConditions[l])
			{
				dictionary["isWeekMissMatch"] = false;
				break;
			}
			dictionary["isWeekMissMatch"] = true;
		}
		for (int m = 0; m < checkScenarioFlagData.timeOfConditions.Count; m++)
		{
			if (PlayerDataManager.currentTimeZone == checkScenarioFlagData.timeOfConditions[m])
			{
				dictionary["isTimeMissMatch"] = false;
				break;
			}
			dictionary["isTimeMissMatch"] = true;
		}
		if (!string.IsNullOrEmpty(checkScenarioFlagData.targetStartingDayScenarioName))
		{
			int num = PlayerFlagDataManager.eventStartingDayDictionary[checkScenarioFlagData.targetStartingDayScenarioName];
			if (PlayerDataManager.currentTotalDay > num)
			{
				dictionary["isNotStartingDayFlag"] = false;
			}
			else
			{
				dictionary["isNotStartingDayFlag"] = true;
			}
		}
		else
		{
			dictionary["isNotStartingDayFlag"] = false;
		}
		if (checkScenarioFlagData.needFollowHeroineNum == 0 && PlayerDataManager.isDungeonHeroineFollow)
		{
			dictionary["isNotNeedFollowHeroine"] = true;
		}
		if (checkScenarioFlagData.needFollowHeroineNum != 9)
		{
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				if (PlayerDataManager.DungeonHeroineFollowNum != checkScenarioFlagData.needFollowHeroineNum)
				{
					dictionary["isNotNeedFollowHeroine"] = true;
				}
			}
			else
			{
				dictionary["isNotNeedFollowHeroine"] = true;
			}
		}
		return dictionary;
	}

	private static void SetUnachievedFlagDebugLog(Dictionary<string, bool> checkFlagDictionary, string type, string placeName)
	{
		string text = "";
		string text2 = "";
		if (checkFlagDictionary.ContainsKey("isNotScenario") && checkFlagDictionary["isNotScenario"])
		{
			text2 += "シナリオ空白\u3000";
		}
		if (checkFlagDictionary.ContainsKey("isFlagNotClear") && checkFlagDictionary["isFlagNotClear"])
		{
			text2 += "フラグ未クリア\u3000";
		}
		if (checkFlagDictionary.ContainsKey("isQuestFlagNotClear") && checkFlagDictionary["isQuestFlagNotClear"])
		{
			text2 += "クエスト未クリア\u3000";
		}
		if (checkFlagDictionary.ContainsKey("isEventItemNotHave") && checkFlagDictionary["isEventItemNotHave"])
		{
			text2 += "イベントアイテムなし\u3000";
		}
		if (checkFlagDictionary.ContainsKey("isWeekMissMatch") && checkFlagDictionary["isWeekMissMatch"])
		{
			text2 += "曜日不一致\u3000";
		}
		if (checkFlagDictionary.ContainsKey("isTimeMissMatch") && checkFlagDictionary["isTimeMissMatch"])
		{
			text2 += "時間不一致\u3000";
		}
		if (checkFlagDictionary.ContainsKey("isNotStartingDayFlag") && checkFlagDictionary["isNotStartingDayFlag"])
		{
			text2 += "前イベントからの経過日数未達";
		}
		if (checkFlagDictionary.ContainsKey("isNotNeedFollowHeroine") && checkFlagDictionary["isNotNeedFollowHeroine"])
		{
			text2 += "ヒロイン同行不一致\u3000";
		}
		if (checkFlagDictionary.ContainsKey("isSoloOnly") && checkFlagDictionary["isSoloOnly"])
		{
			text2 += "ソロオンリー\u3000";
		}
		if (!(type == "dungeonFloor"))
		{
			if (type == "dungeonSex")
			{
				text = "イチャイチャイベント発生なし／ダンジョン名：";
			}
		}
		else
		{
			text = "階数イベント発生なし／ダンジョン名：";
		}
		Debug.Log(text + placeName + "／" + text2);
	}
}
