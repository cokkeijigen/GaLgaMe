using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMoveScenarioDataManager : SerializedMonoBehaviour
{
	public static WorldEventCheckData CheckWorldAccessPointMoveEvent(string pointName)
	{
		WorldEventCheckData result = default(WorldEventCheckData);
		ScenarioFlagData scenarioFlagData = null;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == pointName && data.locationType == "worldMap" && data.scenarioType == "map").ToList();
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
			Debug.Log("１つ以上ある／ポイント名：" + scenarioFlagData.scenarioLocationName + "／シナリオ名：" + scenarioFlagData.scenarioName);
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
			dictionary = CheckCommonMoveScenarioFlag(scenarioFlagData);
			if (!string.IsNullOrEmpty(scenarioFlagData.targetStartingDayScenarioName))
			{
				Debug.Log("戻り値のTerm：" + scenarioFlagData.disablePointTerm);
				if (string.IsNullOrEmpty(scenarioFlagData.disablePointTerm))
				{
					result.disablePointTerm = "";
					Debug.Log("disablePointTermを空にする");
				}
				else
				{
					dictionary.Add("isNotStartingDayFlag", value: true);
					result.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
					Debug.Log("disablePointTermを代入");
				}
			}
			if (scenarioFlagData.needFollowHeroineNum == 0 && PlayerDataManager.isDungeonHeroineFollow)
			{
				flag2 = true;
				result.disablePointTerm = "dialogWorldMapDisable_RequiredSolo";
			}
			if (scenarioFlagData.needFollowHeroineNum != 9 && scenarioFlagData.needFollowHeroineNum != 0)
			{
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					if (PlayerDataManager.DungeonHeroineFollowNum != scenarioFlagData.needFollowHeroineNum)
					{
						flag3 = true;
						Debug.Log("要同行ヒロインが同行していない：" + pointName);
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
					Debug.Log("要同行ヒロインが同行していない：" + pointName);
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
		bool flag4 = dictionary.All((KeyValuePair<string, bool> data) => !data.Value);
		if (!flag && flag4)
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
			Debug.Log("ワールドイベント発生あり：" + pointName + "／シナリオ名：" + scenarioFlagData.scenarioName + "／" + text);
		}
		else
		{
			result.currentScenarioName = "";
			SetUnachievedFlagDebugLog(dictionary, "worldMap", pointName);
		}
		return result;
	}

	public static WorldEventCheckData CheckWorldAccessPointMoveTalkEvent(string pointName)
	{
		WorldEventCheckData result = default(WorldEventCheckData);
		ScenarioFlagData scenarioFlagData = null;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == pointName && data.locationType == "inDoor" && data.scenarioType == "talk").ToList();
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
			Debug.Log("１つ以上ある／ポイント名：" + scenarioFlagData.scenarioLocationName + "／シナリオ名：" + scenarioFlagData.scenarioName);
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
			dictionary = CheckCommonMoveScenarioFlag(scenarioFlagData);
			if (!string.IsNullOrEmpty(scenarioFlagData.targetStartingDayScenarioName))
			{
				if (string.IsNullOrEmpty(scenarioFlagData.disablePointTerm))
				{
					result.disablePointTerm = "";
					Debug.Log("disablePointTermを空にする");
				}
				else
				{
					dictionary.Add("isNotStartingDayFlag", value: true);
					result.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
					Debug.Log("disablePointTermを代入");
				}
			}
			if (scenarioFlagData.needFollowHeroineNum == 0 && PlayerDataManager.isDungeonHeroineFollow)
			{
				flag2 = true;
				dictionary.Add("isSoloOnly", value: true);
				result.disablePointTerm = "dialogWorldMapDisable_RequiredSolo";
			}
			if (scenarioFlagData.needFollowHeroineNum != 9 && scenarioFlagData.needFollowHeroineNum != 0)
			{
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					if (PlayerDataManager.DungeonHeroineFollowNum != scenarioFlagData.needFollowHeroineNum)
					{
						flag3 = true;
						dictionary.Add("isNotNeedFollowHeroine", value: true);
						Debug.Log("要同行ヒロインが同行していない：" + pointName);
					}
				}
				else
				{
					flag3 = true;
					dictionary.Add("isNotNeedFollowHeroine", value: true);
					Debug.Log("要同行ヒロインが同行していない：" + pointName);
				}
			}
		}
		if (scenarioFlagData != null)
		{
			result.locationType = scenarioFlagData.locationType;
			result.scenarioType = scenarioFlagData.scenarioType;
		}
		bool flag4 = dictionary.All((KeyValuePair<string, bool> data) => !data.Value);
		if (!flag && !flag3 && !flag2 && flag4)
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
			Debug.Log("ワールド会話イベント発生あり：" + pointName + "／シナリオ名：" + scenarioFlagData.scenarioName + text);
		}
		else
		{
			result.currentScenarioName = "";
			SetUnachievedFlagDebugLog(dictionary, "worldMapTalk", pointName);
		}
		return result;
	}

	public static LocalEventCheckData CheckLocalPlaceMoveEvent(string placeName)
	{
		LocalEventCheckData result = default(LocalEventCheckData);
		ScenarioFlagData scenarioFlagData = null;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		bool flag = false;
		bool flag2 = false;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == placeName && data.locationType == "localMap" && data.scenarioType == "map").ToList();
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
			list2 = list2.OrderBy((ScenarioFlagData data) => data.eventOrderId).ToList();
			for (int i = 0; i < list2.Count; i++)
			{
				scenarioFlagData = list2[i];
				dictionary.Clear();
				dictionary = CheckCommonMoveScenarioFlag(scenarioFlagData);
				if (!string.IsNullOrEmpty(scenarioFlagData.targetStartingDayScenarioName) && !string.IsNullOrEmpty(scenarioFlagData.disablePointTerm))
				{
					dictionary.Add("isNotStartingDayFlag", value: true);
					result.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
				}
				if (scenarioFlagData.needFollowHeroineNum == 0 && PlayerDataManager.isDungeonHeroineFollow)
				{
					flag = true;
					result.disablePointTerm = "dialogWorldMapDisable_RequiredSolo";
					Debug.Log("ソロオンリー：：" + placeName);
				}
				if (scenarioFlagData.needFollowHeroineNum != 9 && scenarioFlagData.needFollowHeroineNum != 0)
				{
					if (PlayerDataManager.isDungeonHeroineFollow)
					{
						if (PlayerDataManager.DungeonHeroineFollowNum != scenarioFlagData.needFollowHeroineNum)
						{
							flag2 = true;
							Debug.Log("要同行ヒロインが同行していない：" + placeName);
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
						flag2 = true;
						Debug.Log("要同行ヒロインが同行していない：" + placeName);
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
				if (dictionary.All((KeyValuePair<string, bool> data) => !data.Value))
				{
					Debug.Log("ローカル名：" + placeName + "／ローカルイベントあり／ループを抜ける");
					break;
				}
				SetUnachievedFlagDebugLog(dictionary, "localMap", placeName);
			}
		}
		else
		{
			scenarioFlagData = list.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			dictionary.Add("isNotScenario", value: true);
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
			if (flag2)
			{
				text += "ヒロイン同行不一致\u3000";
			}
			if (flag)
			{
				text += "ソロオンリー\u3000";
			}
			Debug.Log("ローカルイベント発生あり／場所名：" + placeName + "／シナリオ名：" + scenarioFlagData.scenarioName + "／" + text);
		}
		else
		{
			result.currentScenarioName = "";
			SetUnachievedFlagDebugLog(dictionary, "localMap", placeName);
		}
		return result;
	}

	public static InDoorEventCheckData CheckInDoorMoveEvent(string placeName)
	{
		InDoorEventCheckData result = default(InDoorEventCheckData);
		ScenarioFlagData scenarioFlagData = null;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == placeName && data.locationType == "inDoor" && data.scenarioType == "talk").ToList();
		List<ScenarioFlagData> list2 = new List<ScenarioFlagData>(list);
		Debug.Log("インドア名：" + placeName + "／Whereリスト数：" + list.Count);
		for (int num = list2.Count - 1; num >= 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[list[num].scenarioName])
			{
				list2.RemoveAt(num);
			}
		}
		if (list2 != null && list2.Count > 0)
		{
			list2 = list2.OrderBy((ScenarioFlagData data) => data.eventOrderId).ToList();
			for (int i = 0; i < list2.Count; i++)
			{
				scenarioFlagData = list2[i];
				dictionary.Clear();
				dictionary = CheckCommonMoveScenarioFlag(scenarioFlagData);
				if (!string.IsNullOrEmpty(scenarioFlagData.targetStartingDayScenarioName) && !string.IsNullOrEmpty(scenarioFlagData.disablePointTerm))
				{
					dictionary.Add("isNotStartingDayFlag", value: true);
					result.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
				}
				if (scenarioFlagData.needFollowHeroineNum == 0)
				{
					_ = PlayerDataManager.isDungeonHeroineFollow;
				}
				if (scenarioFlagData.needFollowHeroineNum != 9 && scenarioFlagData.needFollowHeroineNum != 0)
				{
					if (PlayerDataManager.isDungeonHeroineFollow)
					{
						if (PlayerDataManager.DungeonHeroineFollowNum != scenarioFlagData.needFollowHeroineNum)
						{
							dictionary.Add("isNotNeedFollowHeroine", value: true);
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
						dictionary.Add("isNotNeedFollowHeroine", value: true);
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
				if (dictionary.All((KeyValuePair<string, bool> data) => !data.Value))
				{
					Debug.Log("インドア名：" + placeName + "／インドアイベントあり／ループを抜ける");
					break;
				}
				SetUnachievedFlagDebugLog(dictionary, "inDoor", placeName);
			}
		}
		else
		{
			scenarioFlagData = list.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			dictionary.Add("isNotScenario", value: true);
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
			Debug.Log("インドア会話イベント発生あり／場所名：" + placeName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			result.currentScenarioName = "";
			SetUnachievedFlagDebugLog(dictionary, "inDoor", placeName);
		}
		return result;
	}

	public static InDoorEventCheckData CheckInDoorMoveTalkEvent(string placeName, string characterName)
	{
		InDoorEventCheckData result = default(InDoorEventCheckData);
		ScenarioFlagData scenarioFlagData = null;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.scenarioLocationName == placeName && data.locationType == "inDoor" && data.scenarioType == "talk" && data.scenarioTalkCharacter == characterName).ToList();
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
			list2 = list2.OrderBy((ScenarioFlagData data) => data.eventOrderId).ToList();
			for (int i = 0; i < list2.Count; i++)
			{
				scenarioFlagData = list2[i];
				dictionary.Clear();
				dictionary = CheckCommonMoveScenarioFlag(scenarioFlagData);
				if (!string.IsNullOrEmpty(scenarioFlagData.targetStartingDayScenarioName) && !string.IsNullOrEmpty(scenarioFlagData.disablePointTerm))
				{
					dictionary.Add("isNotStartingDayFlag", value: true);
					result.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
				}
				if (scenarioFlagData.needFollowHeroineNum == 0)
				{
					if (PlayerDataManager.isDungeonHeroineFollow)
					{
						result.isDisableTalkEvent = true;
						Debug.Log("ソロ限定イベント：ヒロイン同行中");
					}
					else
					{
						result.isDisableTalkEvent = false;
						Debug.Log("ソロ限定イベント：単独同行中");
					}
				}
				if (scenarioFlagData.needFollowHeroineNum != 9 && scenarioFlagData.needFollowHeroineNum != 0)
				{
					if (PlayerDataManager.isDungeonHeroineFollow)
					{
						if (PlayerDataManager.DungeonHeroineFollowNum != scenarioFlagData.needFollowHeroineNum)
						{
							dictionary.Add("isNotNeedFollowHeroine", value: true);
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
						dictionary.Add("isNotNeedFollowHeroine", value: true);
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
				if (dictionary.All((KeyValuePair<string, bool> data) => !data.Value))
				{
					Debug.Log("インドア名：" + placeName + "／インドアイベントあり／ループを抜ける");
					break;
				}
				SetUnachievedFlagDebugLog(dictionary, "inDoor", placeName);
			}
		}
		else
		{
			scenarioFlagData = list.OrderBy((ScenarioFlagData data) => data.eventOrderId).FirstOrDefault();
			dictionary.Add("isNotScenario", value: true);
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
			Debug.Log("インドア会話イベント発生あり／場所名：" + placeName + "／シナリオ名：" + scenarioFlagData.scenarioName);
		}
		else
		{
			result.currentScenarioName = "";
			SetUnachievedFlagDebugLog(dictionary, "inDoor", placeName);
		}
		return result;
	}

	private static Dictionary<string, bool> CheckCommonMoveScenarioFlag(ScenarioFlagData checkScenarioFlagData)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>
		{
			{ "isFlagNotClear", false },
			{ "isQuestFlagNotClear", false },
			{ "isEventItemNotHave", false },
			{ "isWeekMissMatch", false },
			{ "isTimeMissMatch", false },
			{ "isNotItemShopPoint", false }
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
		string text2 = PlayerDataManager.currentWeekDay;
		int num = PlayerDataManager.currentTimeZone;
		int num2 = PlayerDataManager.currentTotalDay;
		WorldMapAccessManager component = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		if (PlayerNonSaveDataManager.isUsedShopForScnearioCheck)
		{
			num++;
			if (num >= 4)
			{
				num = 0;
				num2++;
				switch (PlayerDataManager.currentWeekDay)
				{
				case "日":
					text2 = "月";
					break;
				case "月":
					text2 = "火";
					break;
				case "火":
					text2 = "水";
					break;
				case "水":
					text2 = "木";
					break;
				case "木":
					text2 = "金";
					break;
				case "金":
					text2 = "土";
					break;
				case "土":
					text2 = "日";
					break;
				}
			}
			Debug.Log("店で買い物をしている／afterTime：" + num + "／afterWeek：" + text2);
		}
		string worldCityName = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>().localMapUnlockDataBase.localMapUnlockDataList.Find((LocalMapUnlockData data) => data.currentPlaceName == checkScenarioFlagData.scenarioLocationName).worldCityName;
		int num3 = component.GetNeedAccessDay(PlayerDataManager.currentAccessPointName, worldCityName);
		int num4 = component.GetNeedAccessTime(PlayerDataManager.currentAccessPointName, worldCityName);
		if (num4 >= 4)
		{
			int num5 = Mathf.FloorToInt(num4 / 4);
			num3 += num5;
			num4 -= num5 * 4;
		}
		num2 += num3;
		num += num4;
		if (num >= 4)
		{
			num -= 4;
			num2++;
			Debug.Log("日付を超えた");
		}
		int num6 = Mathf.FloorToInt((float)num2 / 7f);
		switch (num2 - 7 * num6)
		{
		case 0:
			text2 = "日";
			break;
		case 1:
			text2 = "月";
			break;
		case 2:
			text2 = "火";
			break;
		case 3:
			text2 = "水";
			break;
		case 4:
			text2 = "木";
			break;
		case 5:
			text2 = "金";
			break;
		case 6:
			text2 = "土";
			break;
		}
		if (checkScenarioFlagData.weekOfConditions.Contains(text2))
		{
			dictionary["isWeekMissMatch"] = false;
		}
		else
		{
			dictionary["isWeekMissMatch"] = true;
		}
		if (checkScenarioFlagData.timeOfConditions.Contains(num))
		{
			dictionary["isTimeMissMatch"] = false;
		}
		else
		{
			dictionary["isTimeMissMatch"] = true;
		}
		if (!string.IsNullOrEmpty(checkScenarioFlagData.targetStartingDayScenarioName))
		{
			if (checkScenarioFlagData.scenarioName.Contains("Mob_"))
			{
				int num7 = PlayerFlagDataManager.eventStartingDayDictionary[checkScenarioFlagData.targetStartingDayScenarioName];
				num2 += 30;
				if (num2 > num7)
				{
					checkScenarioFlagData.disablePointTerm = "";
				}
				else
				{
					checkScenarioFlagData.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
					Debug.Log("経過日数の対象イベント：" + checkScenarioFlagData.targetStartingDayScenarioName + "／イベントクリア日" + num7);
				}
			}
			else
			{
				Debug.Log("経過日数の対象イベント：" + checkScenarioFlagData.targetStartingDayScenarioName);
				int num8 = PlayerFlagDataManager.eventStartingDayDictionary[checkScenarioFlagData.targetStartingDayScenarioName];
				if (num2 > num8)
				{
					checkScenarioFlagData.disablePointTerm = "";
				}
				else
				{
					checkScenarioFlagData.disablePointTerm = "dialogWorldMapDisable_RequiredAddTime";
					Debug.Log("経過日数の対象イベント：" + checkScenarioFlagData.targetStartingDayScenarioName + "／イベントクリア日" + num8);
				}
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
		switch (type)
		{
		case "worldMap":
			text = "ワールドイベント発生なし／場所名：";
			break;
		case "worldMapTalk":
			text = "ワールド会話イベント発生なし／場所名：";
			break;
		case "localMap":
			text = "ローカルイベント発生なし／場所名：";
			break;
		case "dungeon":
			text = "ダンジョンイベント発生なし／場所名：";
			break;
		case "inDoor":
			text = "インドア会話イベント発生なし／場所名：";
			break;
		}
		Debug.Log(text + placeName + "／" + text2);
	}
}
