using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine.Scripting;

namespace ES3Types
{
	[Preserve]
	[ES3Properties(new string[]
	{
		"playerHaveMoney", "playerPartyMember", "playerHaveKizunaPoint", "playerPartyKizunaLv", "playerLibido", "retreatProbability", "currentTimeZone", "totalTimeZoneCount", "currentMonthDay", "currentTotalDay",
		"currentWeekDay", "playBgmCategoryName", "currentAccessPointName", "currentPlaceName", "mapPlaceStatusNum", "isSelectDungeon", "worldMapInputBlock", "newMapPointName", "isNewMapNotice", "isNewRecipeNotice",
		"lastSaveSlotPageNum", "lastSaveSlotNum", "scenarioBattleSpeed", "isCaseOfSkillMoveCursor", "currentDungeonName", "currentDungeonScenarioName", "isDungeonHeroineFollow", "DungeonHeroineFollowNum", "dungeonBattleSpeed", "dungeonMoveSpeed",
		"isDungeonMapAuto", "dungeonEnterTimeZoneNum", "isResultAutoClose", "hotSellingCategoryNum", "hotSellingRemainDayCount", "hotSellingPriceBonus", "hotSellingTradeBonus", "carriageStoreTradeCount", "carriageStoreTradeMoneyNum", "storeTradeSuccessItemList",
		"isStoreTending", "serializationData"
	})]
	public class ES3UserType_PlayerDataManager : ES3ComponentType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerDataManager()
			: base(typeof(PlayerDataManager))
		{
			Instance = this;
			priority = 1;
		}

		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PlayerDataManager objectContainingField = (PlayerDataManager)obj;
			writer.WriteProperty("playerHaveMoney", PlayerDataManager.playerHaveMoney, ES3Type_int.Instance);
			writer.WriteProperty("playerPartyMember", PlayerDataManager.playerPartyMember);
			writer.WriteProperty("playerHaveKizunaPoint", PlayerDataManager.playerHaveKizunaPoint, ES3Type_int.Instance);
			writer.WriteProperty("playerPartyKizunaLv", PlayerDataManager.playerPartyKizunaLv, ES3Type_intArray.Instance);
			writer.WriteProperty("playerLibido", PlayerDataManager.playerLibido, ES3Type_int.Instance);
			writer.WriteProperty("retreatProbability", PlayerDataManager.retreatProbability, ES3Type_int.Instance);
			writer.WriteProperty("currentTimeZone", PlayerDataManager.currentTimeZone, ES3Type_int.Instance);
			writer.WriteProperty("totalTimeZoneCount", PlayerDataManager.totalTimeZoneCount, ES3Type_int.Instance);
			writer.WriteProperty("currentMonthDay", PlayerDataManager.currentMonthDay, ES3Type_int.Instance);
			writer.WriteProperty("currentTotalDay", PlayerDataManager.currentTotalDay, ES3Type_int.Instance);
			writer.WriteProperty("currentWeekDay", PlayerDataManager.currentWeekDay, ES3Type_string.Instance);
			writer.WriteProperty("playBgmCategoryName", PlayerDataManager.playBgmCategoryName, ES3Type_string.Instance);
			writer.WriteProperty("currentAccessPointName", PlayerDataManager.currentAccessPointName, ES3Type_string.Instance);
			writer.WriteProperty("currentPlaceName", PlayerDataManager.currentPlaceName, ES3Type_string.Instance);
			writer.WriteProperty("mapPlaceStatusNum", PlayerDataManager.mapPlaceStatusNum, ES3Type_int.Instance);
			writer.WriteProperty("isSelectDungeon", PlayerDataManager.isSelectDungeon, ES3Type_bool.Instance);
			writer.WriteProperty("worldMapInputBlock", PlayerDataManager.worldMapInputBlock, ES3Type_bool.Instance);
			writer.WriteProperty("newMapPointName", PlayerDataManager.newMapPointName, ES3Type_StringArray.Instance);
			writer.WriteProperty("isNewMapNotice", PlayerDataManager.isNewMapNotice, ES3Type_bool.Instance);
			writer.WriteProperty("isNewRecipeNotice", PlayerDataManager.isNewRecipeNotice, ES3Type_bool.Instance);
			writer.WriteProperty("lastSaveSlotPageNum", PlayerDataManager.lastSaveSlotPageNum, ES3Type_int.Instance);
			writer.WriteProperty("lastSaveSlotNum", PlayerDataManager.lastSaveSlotNum, ES3Type_int.Instance);
			writer.WriteProperty("scenarioBattleSpeed", PlayerDataManager.scenarioBattleSpeed, ES3Type_int.Instance);
			writer.WriteProperty("isCaseOfSkillMoveCursor", PlayerDataManager.isCaseOfSkillMoveCursor, ES3Type_bool.Instance);
			writer.WriteProperty("currentDungeonName", PlayerDataManager.currentDungeonName, ES3Type_string.Instance);
			writer.WriteProperty("currentDungeonScenarioName", PlayerDataManager.currentDungeonScenarioName, ES3Type_string.Instance);
			writer.WriteProperty("isDungeonHeroineFollow", PlayerDataManager.isDungeonHeroineFollow, ES3Type_bool.Instance);
			writer.WriteProperty("DungeonHeroineFollowNum", PlayerDataManager.DungeonHeroineFollowNum, ES3Type_int.Instance);
			writer.WriteProperty("dungeonBattleSpeed", PlayerDataManager.dungeonBattleSpeed, ES3Type_int.Instance);
			writer.WriteProperty("dungeonMoveSpeed", PlayerDataManager.dungeonMoveSpeed, ES3Type_int.Instance);
			writer.WriteProperty("isDungeonMapAuto", PlayerDataManager.isDungeonMapAuto, ES3Type_bool.Instance);
			writer.WriteProperty("dungeonEnterTimeZoneNum", PlayerDataManager.dungeonEnterTimeZoneNum, ES3Type_int.Instance);
			writer.WriteProperty("isResultAutoClose", PlayerDataManager.isResultAutoClose, ES3Type_bool.Instance);
			writer.WriteProperty("hotSellingCategoryNum", PlayerDataManager.hotSellingCategoryNum, ES3Type_int.Instance);
			writer.WriteProperty("hotSellingRemainDayCount", PlayerDataManager.hotSellingRemainDayCount, ES3Type_int.Instance);
			writer.WriteProperty("hotSellingPriceBonus", PlayerDataManager.hotSellingPriceBonus, ES3Type_int.Instance);
			writer.WriteProperty("hotSellingTradeBonus", PlayerDataManager.hotSellingTradeBonus, ES3Type_int.Instance);
			writer.WriteProperty("carriageStoreTradeCount", PlayerDataManager.carriageStoreTradeCount, ES3Type_int.Instance);
			writer.WriteProperty("carriageStoreTradeMoneyNum", PlayerDataManager.carriageStoreTradeMoneyNum, ES3Type_int.Instance);
			writer.WriteProperty("storeTradeSuccessItemList", PlayerDataManager.storeTradeSuccessItemList);
			writer.WriteProperty("isStoreTending", PlayerDataManager.isStoreTending, ES3Type_bool.Instance);
			writer.WritePrivateField("serializationData", objectContainingField);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PlayerDataManager objectContainingField = (PlayerDataManager)obj;
			IEnumerator enumerator = reader.Properties.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					switch ((string)enumerator.Current)
					{
					case "playerHaveMoney":
						PlayerDataManager.playerHaveMoney = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "playerPartyMember":
						PlayerDataManager.playerPartyMember = reader.Read<List<int>>();
						break;
					case "playerHaveKizunaPoint":
						PlayerDataManager.playerHaveKizunaPoint = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "playerPartyKizunaLv":
						PlayerDataManager.playerPartyKizunaLv = reader.Read<int[]>(ES3Type_intArray.Instance);
						break;
					case "playerLibido":
						PlayerDataManager.playerLibido = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "retreatProbability":
						PlayerDataManager.retreatProbability = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "currentTimeZone":
						PlayerDataManager.currentTimeZone = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "totalTimeZoneCount":
						PlayerDataManager.totalTimeZoneCount = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "currentMonthDay":
						PlayerDataManager.currentMonthDay = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "currentTotalDay":
						PlayerDataManager.currentTotalDay = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "currentWeekDay":
						PlayerDataManager.currentWeekDay = reader.Read<string>(ES3Type_string.Instance);
						break;
					case "playBgmCategoryName":
						PlayerDataManager.playBgmCategoryName = reader.Read<string>(ES3Type_string.Instance);
						break;
					case "currentAccessPointName":
						PlayerDataManager.currentAccessPointName = reader.Read<string>(ES3Type_string.Instance);
						break;
					case "currentPlaceName":
						PlayerDataManager.currentPlaceName = reader.Read<string>(ES3Type_string.Instance);
						break;
					case "mapPlaceStatusNum":
						PlayerDataManager.mapPlaceStatusNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "isSelectDungeon":
						PlayerDataManager.isSelectDungeon = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "worldMapInputBlock":
						PlayerDataManager.worldMapInputBlock = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "newMapPointName":
						PlayerDataManager.newMapPointName = reader.Read<string[]>(ES3Type_StringArray.Instance);
						break;
					case "isNewMapNotice":
						PlayerDataManager.isNewMapNotice = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "isNewRecipeNotice":
						PlayerDataManager.isNewRecipeNotice = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "lastSaveSlotPageNum":
						PlayerDataManager.lastSaveSlotPageNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "lastSaveSlotNum":
						PlayerDataManager.lastSaveSlotNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "scenarioBattleSpeed":
						PlayerDataManager.scenarioBattleSpeed = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "isCaseOfSkillMoveCursor":
						PlayerDataManager.isCaseOfSkillMoveCursor = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "currentDungeonName":
						PlayerDataManager.currentDungeonName = reader.Read<string>(ES3Type_string.Instance);
						break;
					case "currentDungeonScenarioName":
						PlayerDataManager.currentDungeonScenarioName = reader.Read<string>(ES3Type_string.Instance);
						break;
					case "isDungeonHeroineFollow":
						PlayerDataManager.isDungeonHeroineFollow = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "DungeonHeroineFollowNum":
						PlayerDataManager.DungeonHeroineFollowNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "dungeonBattleSpeed":
						PlayerDataManager.dungeonBattleSpeed = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "dungeonMoveSpeed":
						PlayerDataManager.dungeonMoveSpeed = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "isDungeonMapAuto":
						PlayerDataManager.isDungeonMapAuto = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "dungeonEnterTimeZoneNum":
						PlayerDataManager.dungeonEnterTimeZoneNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "isResultAutoClose":
						PlayerDataManager.isResultAutoClose = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "hotSellingCategoryNum":
						PlayerDataManager.hotSellingCategoryNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "hotSellingRemainDayCount":
						PlayerDataManager.hotSellingRemainDayCount = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "hotSellingPriceBonus":
						PlayerDataManager.hotSellingPriceBonus = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "hotSellingTradeBonus":
						PlayerDataManager.hotSellingTradeBonus = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "carriageStoreTradeCount":
						PlayerDataManager.carriageStoreTradeCount = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "carriageStoreTradeMoneyNum":
						PlayerDataManager.carriageStoreTradeMoneyNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "storeTradeSuccessItemList":
						PlayerDataManager.storeTradeSuccessItemList = reader.Read<List<int[]>>();
						break;
					case "isStoreTending":
						PlayerDataManager.isStoreTending = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "serializationData":
						reader.SetPrivateField("serializationData", reader.Read<SerializationData>(), objectContainingField);
						break;
					default:
						reader.Skip();
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}
