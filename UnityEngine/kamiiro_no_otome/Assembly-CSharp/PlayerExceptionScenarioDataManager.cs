using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerExceptionScenarioDataManager : MonoBehaviour
{
	public static ExceptionEventCheckData CheckCariageStoreCurrentEvent()
	{
		ExceptionEventCheckData result = default(ExceptionEventCheckData);
		ScenarioFlagData scenarioFlagData = null;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == "Carriage" && data.locationType == "carriageStore" && data.scenarioType == "map").ToList();
		List<ScenarioFlagData> list2 = new List<ScenarioFlagData>(list);
		for (int num = list2.Count - 1; num >= 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[list[num].scenarioName])
			{
				list2.RemoveAt(num);
			}
		}
		if (list2 != null && list2.Count > 0)
		{
			scenarioFlagData = list2.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			Debug.Log("１つ以上ある／場所名：" + scenarioFlagData.scenarioLocationName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			flag = true;
			scenarioFlagData = list.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			dictionary.Add("isNotScenario", value: true);
		}
		if (!flag)
		{
			flag = false;
			dictionary.Clear();
			dictionary = CheckCommonScenarioFlag(scenarioFlagData, isWorldMap: false);
			if (!string.IsNullOrEmpty(scenarioFlagData.targetStartingDayScenarioName) && !string.IsNullOrEmpty(scenarioFlagData.disablePointTerm))
			{
				dictionary.Add("isNotStartingDayFlag", value: true);
				result.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
			}
			if (scenarioFlagData.needFollowHeroineNum == 0 && PlayerDataManager.isDungeonHeroineFollow)
			{
				flag2 = true;
				result.disablePointTerm = "dialogWorldMapDisable_RequiredSolo";
				Debug.Log("ソロオンリー：：CarriageStore");
			}
			if (scenarioFlagData.needFollowHeroineNum != 9 && scenarioFlagData.needFollowHeroineNum != 0)
			{
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					if (PlayerDataManager.DungeonHeroineFollowNum != scenarioFlagData.needFollowHeroineNum)
					{
						flag3 = true;
						Debug.Log("要同行ヒロインが同行していない：CarriageStore");
						switch (scenarioFlagData.needFollowHeroineNum)
						{
						case 1:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredLucy";
							break;
						case 2:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredRina";
							break;
						case 3:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredShia";
							break;
						case 4:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredLevy";
							break;
						}
					}
				}
				else
				{
					flag3 = true;
					Debug.Log("要同行ヒロインが同行していない：CarriageStore");
					switch (scenarioFlagData.needFollowHeroineNum)
					{
					case 1:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredLucy";
						break;
					case 2:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredRina";
						break;
					case 3:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredShia";
						break;
					case 4:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredLevy";
						break;
					}
				}
			}
		}
		if (scenarioFlagData != null)
		{
			result.locationType = scenarioFlagData.locationType;
			result.scenarioType = scenarioFlagData.scenarioType;
		}
		if (dictionary.All((KeyValuePair<string, bool> data) => !data.Value))
		{
			result.currentScenarioName = scenarioFlagData.scenarioName;
			result.currentScenarioId = scenarioFlagData.sortId;
			string text = "";
			if (flag3)
			{
				text += "ヒロイン同行不一致\u3000";
			}
			if (flag2)
			{
				text += "ソロオンリー\u3000";
			}
			Debug.Log("ローカルイベント発生あり／場所名：CarriageStore／シナリオ名：" + scenarioFlagData.scenarioName + "／" + text);
		}
		else
		{
			result.currentScenarioName = "";
			SetUnachievedFlagDebugLog(dictionary, "carriage", "CarriageStore");
		}
		return result;
	}

	public static ExceptionEventCheckData CheckItemShopCurrentEvent()
	{
		ExceptionEventCheckData result = default(ExceptionEventCheckData);
		ScenarioFlagData scenarioFlagData = null;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == "ItemShop" && data.locationType == "itemShop" && data.scenarioType == "map").ToList();
		List<ScenarioFlagData> list2 = new List<ScenarioFlagData>(list);
		for (int num = list2.Count - 1; num >= 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[list[num].scenarioName])
			{
				list2.RemoveAt(num);
			}
		}
		if (list2 != null && list2.Count > 0)
		{
			scenarioFlagData = list2.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			Debug.Log("１つ以上ある／場所名：" + scenarioFlagData.scenarioLocationName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			flag = true;
			scenarioFlagData = list.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			dictionary.Add("isNotScenario", value: true);
		}
		if (!flag)
		{
			flag = false;
			dictionary.Clear();
			dictionary = CheckCommonScenarioFlag(scenarioFlagData, isWorldMap: false);
			if (!string.IsNullOrEmpty(scenarioFlagData.targetStartingDayScenarioName) && !string.IsNullOrEmpty(scenarioFlagData.disablePointTerm))
			{
				dictionary.Add("isNotStartingDayFlag", value: true);
				result.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
			}
			if (scenarioFlagData.needFollowHeroineNum == 0 && PlayerDataManager.isDungeonHeroineFollow)
			{
				flag2 = true;
				result.disablePointTerm = "dialogWorldMapDisable_RequiredSolo";
				Debug.Log("ソロオンリー：：ItemShop");
			}
			if (scenarioFlagData.needFollowHeroineNum != 9 && scenarioFlagData.needFollowHeroineNum != 0)
			{
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					if (PlayerDataManager.DungeonHeroineFollowNum != scenarioFlagData.needFollowHeroineNum)
					{
						flag3 = true;
						Debug.Log("要同行ヒロインが同行していない：ItemShop");
						switch (scenarioFlagData.needFollowHeroineNum)
						{
						case 1:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredLucy";
							break;
						case 2:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredRina";
							break;
						case 3:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredShia";
							break;
						case 4:
							result.disablePointTerm = "dialogWorldMapDisable_RequiredLevy";
							break;
						}
					}
				}
				else
				{
					flag3 = true;
					Debug.Log("要同行ヒロインが同行していない：ItemShop");
					switch (scenarioFlagData.needFollowHeroineNum)
					{
					case 1:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredLucy";
						break;
					case 2:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredRina";
						break;
					case 3:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredShia";
						break;
					case 4:
						result.disablePointTerm = "dialogWorldMapDisable_RequiredLevy";
						break;
					}
				}
			}
		}
		if (scenarioFlagData != null)
		{
			result.locationType = scenarioFlagData.locationType;
			result.scenarioType = scenarioFlagData.scenarioType;
		}
		if (dictionary.All((KeyValuePair<string, bool> data) => !data.Value))
		{
			result.currentScenarioName = scenarioFlagData.scenarioName;
			result.currentScenarioId = scenarioFlagData.sortId;
			string text = "";
			if (flag3)
			{
				text += "ヒロイン同行不一致\u3000";
			}
			if (flag2)
			{
				text += "ソロオンリー\u3000";
			}
			Debug.Log("ローカルイベント発生あり／場所名：ItemShop／シナリオ名：" + scenarioFlagData.scenarioName + "／" + text);
		}
		else
		{
			result.currentScenarioName = "";
			SetUnachievedFlagDebugLog(dictionary, "itemShop", "ItemShop");
		}
		return result;
	}

	private static Dictionary<string, bool> CheckCommonScenarioFlag(ScenarioFlagData checkScenarioFlagData, bool isWorldMap)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>
		{
			{ "isFlagNotClear", false },
			{ "isQuestFlagNotClear", false },
			{ "isEventItemNotHave", false },
			{ "isWeekMissMatch", false },
			{ "isTimeMissMatch", false }
		};
		for (int i = 0; i < checkScenarioFlagData.needScenarioFlagNameList.Count; i++)
		{
			string text = checkScenarioFlagData.needScenarioFlagNameList[i];
			Debug.Log("必要フラグは：" + text);
			if (!PlayerFlagDataManager.scenarioFlagDictionary[text])
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
		if (checkScenarioFlagData.needItemShopPoint > 0 && PlayerDataManager.itemShopPoint < checkScenarioFlagData.needItemShopPoint)
		{
			dictionary["isNotItemShopPoint"] = true;
		}
		string currentWeekDay = PlayerDataManager.currentWeekDay;
		int currentTimeZone = PlayerDataManager.currentTimeZone;
		int currentTotalDay = PlayerDataManager.currentTotalDay;
		GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		if (checkScenarioFlagData.weekOfConditions.Contains(currentWeekDay))
		{
			dictionary["isWeekMissMatch"] = false;
		}
		else
		{
			dictionary["isWeekMissMatch"] = true;
		}
		if (checkScenarioFlagData.timeOfConditions.Contains(currentTimeZone))
		{
			dictionary["isTimeMissMatch"] = false;
		}
		else
		{
			dictionary["isTimeMissMatch"] = true;
		}
		if (!string.IsNullOrEmpty(checkScenarioFlagData.targetStartingDayScenarioName))
		{
			int num = PlayerFlagDataManager.eventStartingDayDictionary[checkScenarioFlagData.targetStartingDayScenarioName];
			if (currentTotalDay > num)
			{
				checkScenarioFlagData.disablePointTerm = "";
			}
			else
			{
				checkScenarioFlagData.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
				Debug.Log("経過日数の対象イベント：" + checkScenarioFlagData.targetStartingDayScenarioName + "／イベントクリア日" + num);
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
		if (checkFlagDictionary.ContainsKey("isNotItemShopPoint") && checkFlagDictionary["isNotItemShopPoint"])
		{
			text2 += "雑貨店ポイント足りない\u3000";
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
		if (!(type == "carriage"))
		{
			if (type == "itemShop")
			{
				text = "雑貨店買い物後イベント発生なし／場所名：";
			}
		}
		else
		{
			text = "陳列販売後イベント発生なし／場所名：";
		}
		Debug.Log(text + placeName + "／" + text2);
	}
}
