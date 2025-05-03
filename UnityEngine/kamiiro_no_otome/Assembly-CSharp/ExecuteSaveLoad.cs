using System;
using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("")]
public class ExecuteSaveLoad : StateBehaviour
{
	public StateLink saveEnd;

	public StateLink loadEnd;

	private ES3Settings eS3Settings;

	private string saveDayTime;

	private string savePlaceLocTerm;

	private int selectSlotNum;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (ES3.FileExists())
		{
			ES3.CacheFile();
		}
		eS3Settings = new ES3Settings("SaveFile.es3", ES3.Location.Cache);
		selectSlotNum = PlayerNonSaveDataManager.selectSlotPageNum * 9 + PlayerNonSaveDataManager.selectSlotNum + 1;
		string selectSystemTabName = PlayerNonSaveDataManager.selectSystemTabName;
		if (!(selectSystemTabName == "save"))
		{
			if (selectSystemTabName == "load")
			{
				StartDataLoad(DataLoadEnd);
			}
		}
		else
		{
			StartDataSave(DataSaveEnd);
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

	private void StartDataSave(UnityAction unityAction)
	{
		ES3.Save("optionsBgmVolume", PlayerOptionsDataManager.optionsBgmVolume, eS3Settings);
		ES3.Save("optionsHBgmVolume", PlayerOptionsDataManager.optionsHBgmVolume, eS3Settings);
		ES3.Save("optionsSeVolume", PlayerOptionsDataManager.optionsSeVolume, eS3Settings);
		ES3.Save("optionsAmbienceVolume", PlayerOptionsDataManager.optionsAmbienceVolume, eS3Settings);
		ES3.Save("optionsVoice1Volume", PlayerOptionsDataManager.optionsVoice1Volume, eS3Settings);
		ES3.Save("optionsVoice2Volume", PlayerOptionsDataManager.optionsVoice2Volume, eS3Settings);
		ES3.Save("optionsVoice3Volume", PlayerOptionsDataManager.optionsVoice3Volume, eS3Settings);
		ES3.Save("optionsVoice4Volume", PlayerOptionsDataManager.optionsVoice4Volume, eS3Settings);
		ES3.Save("optionsVoice5Volume", PlayerOptionsDataManager.optionsVoice5Volume, eS3Settings);
		ES3.Save("optionsSubVoice1Volume", PlayerOptionsDataManager.optionsSubVoice1Volume, eS3Settings);
		ES3.Save("optionsSubVoice2Volume", PlayerOptionsDataManager.optionsSubVoice2Volume, eS3Settings);
		ES3.Save("optionsSubVoice3Volume", PlayerOptionsDataManager.optionsSubVoice3Volume, eS3Settings);
		ES3.Save("optionsSubVoice4Volume", PlayerOptionsDataManager.optionsSubVoice4Volume, eS3Settings);
		ES3.Save("optionsSubVoice5Volume", PlayerOptionsDataManager.optionsSubVoice5Volume, eS3Settings);
		ES3.Save("optionsSubVoice6Volume", PlayerOptionsDataManager.optionsSubVoice6Volume, eS3Settings);
		ES3.Save("optionsSubVoice7Volume", PlayerOptionsDataManager.optionsSubVoice7Volume, eS3Settings);
		ES3.Save("optionsSubVoice8Volume", PlayerOptionsDataManager.optionsSubVoice8Volume, eS3Settings);
		ES3.Save("optionsMobVoice1Volume", PlayerOptionsDataManager.optionsMobVoice1Volume, eS3Settings);
		ES3.Save("optionsMobVoice2Volume", PlayerOptionsDataManager.optionsMobVoice2Volume, eS3Settings);
		ES3.Save("optionsMobVoice3Volume", PlayerOptionsDataManager.optionsMobVoice3Volume, eS3Settings);
		ES3.Save("optionsMobVoice4Volume", PlayerOptionsDataManager.optionsMobVoice4Volume, eS3Settings);
		ES3.Save("optionsMobVoice5Volume", PlayerOptionsDataManager.optionsMobVoice5Volume, eS3Settings);
		ES3.Save("optionsMobVoice6Volume", PlayerOptionsDataManager.optionsMobVoice6Volume, eS3Settings);
		ES3.Save("optionsMobVoice7Volume", PlayerOptionsDataManager.optionsMobVoice7Volume, eS3Settings);
		ES3.Save("optionsMobVoice8Volume", PlayerOptionsDataManager.optionsMobVoice8Volume, eS3Settings);
		ES3.Save("isAllVoiceDisable", PlayerOptionsDataManager.isAllVoiceDisable, eS3Settings);
		ES3.Save("isLucyVoiceTypeSoft", PlayerOptionsDataManager.isLucyVoiceTypeSoft, eS3Settings);
		ES3.Save("isLucyVoiceTypeSexy", PlayerOptionsDataManager.isLucyVoiceTypeSexy, eS3Settings);
		ES3.Save("optionsTextSpeed", PlayerOptionsDataManager.optionsTextSpeed, eS3Settings);
		ES3.Save("optionsAutoTextSpeed", PlayerOptionsDataManager.optionsAutoTextSpeed, eS3Settings);
		ES3.Save("optionsMouseWheelSend", PlayerOptionsDataManager.optionsMouseWheelSend, eS3Settings);
		ES3.Save("defaultMouseWheelBacklog", PlayerOptionsDataManager.defaultMouseWheelBacklog, eS3Settings);
		ES3.Save("optionsMouseWheelPower", PlayerOptionsDataManager.optionsMouseWheelPower, eS3Settings);
		ES3.Save("optionsVoiceStopTypeNext", PlayerOptionsDataManager.optionsVoiceStopTypeNext, eS3Settings);
		ES3.Save("optionsVoiceStopTypeClick", PlayerOptionsDataManager.optionsVoiceStopTypeClick, eS3Settings);
		ES3.Save("optionsWindowSize", PlayerOptionsDataManager.optionsWindowSize, eS3Settings);
		ES3.Save("optionsFullScreenMode", PlayerOptionsDataManager.optionsFullScreenMode, eS3Settings);
		ES3.Save("isRecommendSaveAlertNoOpen", PlayerDataManager.isRecommendSaveAlertNoOpen, eS3Settings);
		saveDayTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
		ES3.Save("saveDayTime" + selectSlotNum, saveDayTime, eS3Settings);
		float value = Time.time - PlayerNonSaveDataManager.gameStartTime;
		ES3.Save("gamePlayTime" + selectSlotNum, value, eS3Settings);
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
		case 1:
		case 2:
			savePlaceLocTerm = "area" + PlayerDataManager.currentAccessPointName;
			break;
		case 3:
			savePlaceLocTerm = "area" + PlayerDataManager.currentPlaceName;
			break;
		}
		ES3.Save("savePlaceName" + selectSlotNum, savePlaceLocTerm, eS3Settings);
		ES3.Save("savePlayerLv" + selectSlotNum, PlayerStatusDataManager.characterLv[0], eS3Settings);
		for (int i = 0; i < 5; i++)
		{
			PlayerDataManager.playerSaveDataHp[i] = PlayerStatusDataManager.characterHp[i];
		}
		ES3.Save("playerSaveDataHp" + selectSlotNum, PlayerDataManager.playerSaveDataHp, eS3Settings);
		if (PlayerNonSaveDataManager.isInterruptedSave)
		{
			PlayerNonSaveDataManager.isInterruptedAfterSave = true;
		}
		ES3.Save("isInterruptedAfterSave" + selectSlotNum, PlayerNonSaveDataManager.isInterruptedAfterSave, eS3Settings);
		ES3.Save("currentScenarioLabelName" + selectSlotNum, PlayerNonSaveDataManager.currentScenarioLabelName, eS3Settings);
		string[] shopRankNameArray = PlayerShopRankDataManager.GetShopRankNameArray();
		ES3.Save("saveShopRankName0" + selectSlotNum, shopRankNameArray[0], eS3Settings);
		ES3.Save("saveShopRankName1" + selectSlotNum, shopRankNameArray[1], eS3Settings);
		ES3.Save("currentShopRankFirstNum" + selectSlotNum, PlayerDataManager.currentShopRankFirstNum, eS3Settings);
		ES3.Save("currentShopRankSecondNum" + selectSlotNum, PlayerDataManager.currentShopRankSecondNum, eS3Settings);
		ES3.Save("playerHaveMoney" + selectSlotNum, PlayerDataManager.playerHaveMoney, eS3Settings);
		ES3.Save("playerPartyMember" + selectSlotNum, PlayerDataManager.playerPartyMember, eS3Settings);
		ES3.Save("playerHaveKizunaPoint" + selectSlotNum, PlayerDataManager.playerHaveKizunaPoint, eS3Settings);
		ES3.Save("playerPartyKizunaLv" + selectSlotNum, PlayerDataManager.playerPartyKizunaLv, eS3Settings);
		ES3.Save("currentTimeZone" + selectSlotNum, PlayerDataManager.currentTimeZone, eS3Settings);
		ES3.Save("totalTimeZoneCount" + selectSlotNum, PlayerDataManager.totalTimeZoneCount, eS3Settings);
		ES3.Save("currentMonthDay" + selectSlotNum, PlayerDataManager.currentMonthDay, eS3Settings);
		ES3.Save("currentTotalDay" + selectSlotNum, PlayerDataManager.currentTotalDay, eS3Settings);
		ES3.Save("currentWeekDay" + selectSlotNum, PlayerDataManager.currentWeekDay, eS3Settings);
		ES3.Save("currentAccessPointName" + selectSlotNum, PlayerDataManager.currentAccessPointName, eS3Settings);
		ES3.Save("currentPlaceName" + selectSlotNum, PlayerDataManager.currentPlaceName, eS3Settings);
		ES3.Save("mapPlaceStatusNum" + selectSlotNum, PlayerDataManager.mapPlaceStatusNum, eS3Settings);
		ES3.Save("worldMapInputBlock" + selectSlotNum, PlayerDataManager.worldMapInputBlock, eS3Settings);
		ES3.Save("newMapPointName" + selectSlotNum, PlayerDataManager.newMapPointName, eS3Settings);
		ES3.Save("isLocalMapActionLimit" + selectSlotNum, PlayerDataManager.isLocalMapActionLimit, eS3Settings);
		ES3.Save("needEffectNewWorldMapPointName" + selectSlotNum, PlayerDataManager.needEffectNewWorldMapPointName, eS3Settings);
		ES3.Save("isNeedEffectNewWorldMapPoint" + selectSlotNum, PlayerDataManager.isNeedEffectNewWorldMapPoint, eS3Settings);
		ES3.Save("KingdomMobHeroineVisibleDictionary" + selectSlotNum, PlayerDataManager.KingdomMobHeroineVisibleDictionary, eS3Settings);
		ES3.Save("CityMobHeroineVisibleDictionary" + selectSlotNum, PlayerDataManager.CityMobHeroineVisibleDictionary, eS3Settings);
		ES3.Save("KingdomMobCheckTimeDictionary" + selectSlotNum, PlayerDataManager.KingdomMobCheckTimeDictionary, eS3Settings);
		ES3.Save("CityMobCheckTimeDictionary" + selectSlotNum, PlayerDataManager.CityMobCheckTimeDictionary, eS3Settings);
		ES3.Save("isHeroineSpecifyFollow" + selectSlotNum, PlayerDataManager.isHeroineSpecifyFollow, eS3Settings);
		ES3.Save("heroineSpecifyFollowPoint" + selectSlotNum, PlayerDataManager.heroineSpecifyFollowPoint, eS3Settings);
		ES3.Save("heroineSpecifyFollowId" + selectSlotNum, PlayerDataManager.heroineSpecifyFollowId, eS3Settings);
		ES3.Save("isHeroineSexVoiceLowStage" + selectSlotNum, PlayerDataManager.isHeroineSexVoiceLowStage, eS3Settings);
		ES3.Save("isHeroineSexTouchTextVisible" + selectSlotNum, PlayerDataManager.isHeroineSexTouchTextVisible, eS3Settings);
		ES3.Save("isNoCheckNewQuest" + selectSlotNum, PlayerDataManager.isNoCheckNewQuest, eS3Settings);
		ES3.Save("isNoCheckNewSubQuest" + selectSlotNum, PlayerDataManager.isNoCheckNewSubQuest, eS3Settings);
		ES3.Save("totalSalesAmount" + selectSlotNum, PlayerDataManager.totalSalesAmount, eS3Settings);
		ES3.Save("hotSellingCategoryNum" + selectSlotNum, PlayerDataManager.hotSellingCategoryNum, eS3Settings);
		ES3.Save("hotSellingRemainDayCount" + selectSlotNum, PlayerDataManager.hotSellingRemainDayCount, eS3Settings);
		ES3.Save("hotSellingPriceBonus" + selectSlotNum, PlayerDataManager.hotSellingPriceBonus, eS3Settings);
		ES3.Save("hotSellingTradeBonus" + selectSlotNum, PlayerDataManager.hotSellingTradeBonus, eS3Settings);
		ES3.Save("carriageStoreTradeCount" + selectSlotNum, PlayerDataManager.carriageStoreTradeCount, eS3Settings);
		ES3.Save("carriageStoreTradeMoneyNum" + selectSlotNum, PlayerDataManager.carriageStoreTradeMoneyNum, eS3Settings);
		ES3.Save("carriageStoreSellMagnification" + selectSlotNum, PlayerDataManager.carriageStoreSellMagnification, eS3Settings);
		ES3.Save("itemShopPoint" + selectSlotNum, PlayerDataManager.itemShopPoint, eS3Settings);
		PlayerDataManager.lastSaveSlotNum = PlayerNonSaveDataManager.selectSlotNum;
		PlayerDataManager.lastSaveSlotPageNum = PlayerNonSaveDataManager.selectSlotPageNum;
		ES3.Save("lastSaveSlotNum", PlayerDataManager.lastSaveSlotNum, eS3Settings);
		ES3.Save("lastSaveSlotPageNum", PlayerDataManager.lastSaveSlotPageNum, eS3Settings);
		ES3.Save("scenarioBattleSpeed" + selectSlotNum, PlayerDataManager.scenarioBattleSpeed, eS3Settings);
		ES3.Save("isDungeonHeroineFollow" + selectSlotNum, PlayerDataManager.isDungeonHeroineFollow, eS3Settings);
		ES3.Save("DungeonHeroineFollowNum" + selectSlotNum, PlayerDataManager.DungeonHeroineFollowNum, eS3Settings);
		ES3.Save("dungeonBattleSpeed" + selectSlotNum, PlayerDataManager.dungeonBattleSpeed, eS3Settings);
		ES3.Save("dungeonMoveSpeed" + selectSlotNum, PlayerDataManager.dungeonMoveSpeed, eS3Settings);
		ES3.Save("isDungeonMapAuto" + selectSlotNum, PlayerDataManager.isDungeonMapAuto, eS3Settings);
		ES3.Save("isResultAutoClose" + selectSlotNum, PlayerDataManager.isResultAutoClose, eS3Settings);
		ES3.Save("dungeonEnterTimeZoneNum" + selectSlotNum, PlayerDataManager.dungeonEnterTimeZoneNum, eS3Settings);
		ES3.Save("rareDropRateRaisePowerNum" + selectSlotNum, PlayerDataManager.rareDropRateRaisePowerNum, eS3Settings);
		ES3.Save("rareDropRateRaiseRaimingDaysNum" + selectSlotNum, PlayerDataManager.rareDropRateRaiseRaimingDaysNum, eS3Settings);
		ES3.Save("characterExp" + selectSlotNum, PlayerStatusDataManager.characterExp, eS3Settings);
		ES3.Save("characterLv" + selectSlotNum, PlayerStatusDataManager.characterLv, eS3Settings);
		ES3.Save("characterHp" + selectSlotNum, PlayerStatusDataManager.characterHp, eS3Settings);
		ES3.Save("characterMp" + selectSlotNum, PlayerStatusDataManager.characterMp, eS3Settings);
		ES3.Save("haveItemList" + selectSlotNum, PlayerInventoryDataManager.haveItemList, eS3Settings);
		ES3.Save("haveItemMaterialList" + selectSlotNum, PlayerInventoryDataManager.haveItemMaterialList, eS3Settings);
		ES3.Save("haveItemCanMakeMaterialList" + selectSlotNum, PlayerInventoryDataManager.haveItemCanMakeMaterialList, eS3Settings);
		ES3.Save("haveItemCampItemlList" + selectSlotNum, PlayerInventoryDataManager.haveItemCampItemList, eS3Settings);
		ES3.Save("haveItemMagicMaterialList" + selectSlotNum, PlayerInventoryDataManager.haveItemMagicMaterialList, eS3Settings);
		ES3.Save("haveCashableItemList" + selectSlotNum, PlayerInventoryDataManager.haveCashableItemList, eS3Settings);
		ES3.Save("haveEventItemList" + selectSlotNum, PlayerInventoryDataManager.haveEventItemList, eS3Settings);
		ES3.Save("haveWeaponList" + selectSlotNum, PlayerInventoryDataManager.haveWeaponList, eS3Settings);
		ES3.Save("haveArmorList" + selectSlotNum, PlayerInventoryDataManager.haveArmorList, eS3Settings);
		ES3.Save("havePartyWeaponList" + selectSlotNum, PlayerInventoryDataManager.havePartyWeaponList, eS3Settings);
		ES3.Save("havePartyArmorList" + selectSlotNum, PlayerInventoryDataManager.havePartyArmorList, eS3Settings);
		ES3.Save("haveAccessoryList" + selectSlotNum, PlayerInventoryDataManager.haveAccessoryList, eS3Settings);
		ES3.Save("playerLearnedSkillList" + selectSlotNum, PlayerInventoryDataManager.playerLearnedSkillList, eS3Settings);
		ES3.Save("playerEquipSkillList" + selectSlotNum, PlayerEquipDataManager.playerEquipSkillList, eS3Settings);
		ES3.Save("scenarioFlagDictionary" + selectSlotNum, PlayerFlagDataManager.scenarioFlagDictionary, eS3Settings);
		ES3.Save("tutorialFlagDictionary" + selectSlotNum, PlayerFlagDataManager.tutorialFlagDictionary, eS3Settings);
		ES3.Save("sceneGarellyFlagDictionary", PlayerFlagDataManager.sceneGarellyFlagDictionary, eS3Settings);
		ES3.Save("partyPowerUpFlagList" + selectSlotNum, PlayerFlagDataManager.partyPowerUpFlagList, eS3Settings);
		ES3.Save("heroineAllTimeFollowFlagList" + selectSlotNum, PlayerFlagDataManager.heroineAllTimeFollowFlagList, eS3Settings);
		ES3.Save("heroineFirstSexTouchFlagList" + selectSlotNum, PlayerFlagDataManager.heroineFirstSexTouchFlagList, eS3Settings);
		ES3.Save("dungeonFlagDictionary" + selectSlotNum, PlayerFlagDataManager.dungeonFlagDictionary, eS3Settings);
		ES3.Save("deepDungeonFlagDictionary" + selectSlotNum, PlayerFlagDataManager.deepDungeonFlagDictionary, eS3Settings);
		ES3.Save("dungeonDeepClearFlagDictionary" + selectSlotNum, PlayerFlagDataManager.dungeonDeepClearFlagDictionary, eS3Settings);
		ES3.Save("recipeFlagDictionary" + selectSlotNum, PlayerFlagDataManager.recipeFlagDictionary, eS3Settings);
		ES3.Save("enableNewCraftFlagDictionary" + selectSlotNum, PlayerFlagDataManager.enableNewCraftFlagDictionary, eS3Settings);
		ES3.Save("questClearFlagList" + selectSlotNum, PlayerFlagDataManager.questClearFlagList, eS3Settings);
		ES3.Save("keyItemFlagDictionary" + selectSlotNum, PlayerFlagDataManager.keyItemFlagDictionary, eS3Settings);
		ES3.Save("eventStartingDayDictionary" + selectSlotNum, PlayerFlagDataManager.eventStartingDayDictionary, eS3Settings);
		ES3.Save("priceSettingNoticeFlagDictionary" + selectSlotNum, PlayerFlagDataManager.priceSettingNoticeFlagDictionary, eS3Settings);
		ES3.Save("extraNoticeFlagDictionary" + selectSlotNum, PlayerFlagDataManager.extraNoticeFlagDictionary, eS3Settings);
		ES3.Save("playerCraftLv" + selectSlotNum, PlayerCraftStatusManager.playerCraftLv, eS3Settings);
		ES3.Save("playerCraftExp" + selectSlotNum, PlayerCraftStatusManager.playerCraftExp, eS3Settings);
		ES3.Save("craftFacilityDataDictionary" + selectSlotNum, PlayerCraftStatusManager.craftFacilityDataDictionary, eS3Settings);
		ES3.Save("playerSexExp" + selectSlotNum, PlayerSexStatusDataManager.playerSexExp, eS3Settings);
		ES3.Save("heroineSexExp" + selectSlotNum, PlayerSexStatusDataManager.heroineSexExp, eS3Settings);
		ES3.Save("totalPistonCount" + selectSlotNum, PlayerSexStatusDataManager.totalPistonCount, eS3Settings);
		ES3.Save("totalMouthCount" + selectSlotNum, PlayerSexStatusDataManager.totalMouthCount, eS3Settings);
		ES3.Save("totalOutShotCount" + selectSlotNum, PlayerSexStatusDataManager.totalOutShotCount, eS3Settings);
		ES3.Save("totalInShotCount" + selectSlotNum, PlayerSexStatusDataManager.totalInShotCount, eS3Settings);
		ES3.Save("totalEcstasyCount" + selectSlotNum, PlayerSexStatusDataManager.totalEcstasyCount, eS3Settings);
		ES3.Save("totalSexCount" + selectSlotNum, PlayerSexStatusDataManager.totalSexCount, eS3Settings);
		ES3.Save("totalUniqueSexCount" + selectSlotNum, PlayerSexStatusDataManager.totalUniqueSexCount, eS3Settings);
		ES3.Save("heroineRemainingSemenCount_vagina" + selectSlotNum, PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina, eS3Settings);
		ES3.Save("heroineRemainingSemenCount_anal" + selectSlotNum, PlayerSexStatusDataManager.heroineRemainingSemenCount_anal, eS3Settings);
		ES3.StoreCachedFile();
		unityAction();
	}

	private void StartDataLoad(UnityAction unityAction)
	{
		PlayerDataManager.playerHaveMoney = ES3.Load<int>("playerHaveMoney" + selectSlotNum, eS3Settings);
		PlayerDataManager.playerPartyMember = ES3.Load<List<int>>("playerPartyMember" + selectSlotNum, eS3Settings);
		PlayerDataManager.playerHaveKizunaPoint = ES3.Load<int>("playerHaveKizunaPoint" + selectSlotNum, eS3Settings);
		PlayerDataManager.playerPartyKizunaLv = ES3.Load<int[]>("playerPartyKizunaLv" + selectSlotNum, eS3Settings);
		PlayerDataManager.playerSaveDataHp = ES3.Load("playerSaveDataHp" + selectSlotNum, PlayerDataManager.playerSaveDataHp, eS3Settings);
		PlayerNonSaveDataManager.isInterruptedAfterSave = ES3.Load("isInterruptedAfterSave" + selectSlotNum, defaultValue: false, eS3Settings);
		PlayerNonSaveDataManager.currentScenarioLabelName = ES3.Load<string>("currentScenarioLabelName" + selectSlotNum, "", eS3Settings);
		PlayerNonSaveDataManager.isInterruptedSave = false;
		PlayerNonSaveDataManager.gameStartTime = ES3.Load("gamePlayTime" + selectSlotNum, 0f, eS3Settings);
		PlayerDataManager.currentTimeZone = ES3.Load<int>("currentTimeZone" + selectSlotNum, eS3Settings);
		PlayerDataManager.totalTimeZoneCount = ES3.Load<int>("totalTimeZoneCount" + selectSlotNum, eS3Settings);
		PlayerDataManager.currentMonthDay = ES3.Load<int>("currentMonthDay" + selectSlotNum, eS3Settings);
		PlayerDataManager.currentTotalDay = ES3.Load<int>("currentTotalDay" + selectSlotNum, eS3Settings);
		PlayerDataManager.currentWeekDay = ES3.Load<string>("currentWeekDay" + selectSlotNum, eS3Settings);
		PlayerDataManager.currentAccessPointName = ES3.Load<string>("currentAccessPointName" + selectSlotNum, eS3Settings);
		PlayerDataManager.currentPlaceName = ES3.Load<string>("currentPlaceName" + selectSlotNum, eS3Settings);
		PlayerDataManager.mapPlaceStatusNum = ES3.Load<int>("mapPlaceStatusNum" + selectSlotNum, eS3Settings);
		PlayerDataManager.worldMapInputBlock = ES3.Load<bool>("worldMapInputBlock" + selectSlotNum, eS3Settings);
		PlayerDataManager.newMapPointName = ES3.Load<string[]>("newMapPointName" + selectSlotNum, eS3Settings);
		PlayerDataManager.isLocalMapActionLimit = ES3.Load("isLocalMapActionLimit" + selectSlotNum, defaultValue: false, eS3Settings);
		PlayerDataManager.needEffectNewWorldMapPointName = ES3.Load<string>("needEffectNewWorldMapPointName" + selectSlotNum, "", eS3Settings);
		PlayerDataManager.isNeedEffectNewWorldMapPoint = ES3.Load("isNeedEffectNewWorldMapPoint" + selectSlotNum, defaultValue: false, eS3Settings);
		if (PlayerDataManager.mapPlaceStatusNum == 1)
		{
			if (PlayerDataManager.currentAccessPointName == "Kingdom1")
			{
				Dictionary<string, bool> defaultValue = new Dictionary<string, bool>
				{
					["HunterGuild"] = false,
					["Street1"] = false,
					["Church"] = false
				};
				Dictionary<string, int> defaultValue2 = new Dictionary<string, int>
				{
					["HunterGuild"] = 0,
					["Street1"] = 0,
					["Church"] = 0
				};
				PlayerDataManager.KingdomMobHeroineVisibleDictionary = ES3.Load("KingdomMobHeroineVisibleDictionary" + selectSlotNum, defaultValue, eS3Settings);
				PlayerDataManager.KingdomMobCheckTimeDictionary = ES3.Load("KingdomMobCheckTimeDictionary" + selectSlotNum, defaultValue2, eS3Settings);
			}
			else
			{
				Dictionary<string, bool> defaultValue3 = new Dictionary<string, bool> { ["Port1"] = false };
				Dictionary<string, int> defaultValue4 = new Dictionary<string, int> { ["Port1"] = 0 };
				PlayerDataManager.CityMobHeroineVisibleDictionary = ES3.Load("CityMobHeroineVisibleDictionary" + selectSlotNum, defaultValue3, eS3Settings);
				PlayerDataManager.CityMobCheckTimeDictionary = ES3.Load("CityMobCheckTimeDictionary" + selectSlotNum, defaultValue4, eS3Settings);
			}
		}
		PlayerDataManager.isHeroineSpecifyFollow = ES3.Load<bool>("isHeroineSpecifyFollow" + selectSlotNum, eS3Settings);
		PlayerDataManager.heroineSpecifyFollowPoint = ES3.Load<string>("heroineSpecifyFollowPoint" + selectSlotNum, eS3Settings);
		PlayerDataManager.heroineSpecifyFollowId = ES3.Load<int>("heroineSpecifyFollowId" + selectSlotNum, eS3Settings);
		PlayerDataManager.isHeroineSexVoiceLowStage = ES3.Load("isHeroineSexVoiceLowStage" + selectSlotNum, defaultValue: false, eS3Settings);
		PlayerDataManager.isHeroineSexTouchTextVisible = ES3.Load("isHeroineSexTouchTextVisible" + selectSlotNum, defaultValue: false, eS3Settings);
		PlayerDataManager.isNoCheckNewQuest = ES3.Load("isNoCheckNewQuest" + selectSlotNum, defaultValue: false, eS3Settings);
		PlayerDataManager.isNoCheckNewSubQuest = ES3.Load("isNoCheckNewSubQuest" + selectSlotNum, defaultValue: false, eS3Settings);
		PlayerDataManager.totalSalesAmount = ES3.Load("totalSalesAmount" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.hotSellingCategoryNum = ES3.Load("hotSellingCategoryNum" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.hotSellingRemainDayCount = ES3.Load("hotSellingRemainDayCount" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.hotSellingPriceBonus = ES3.Load("hotSellingPriceBonus" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.hotSellingTradeBonus = ES3.Load("hotSellingTradeBonus" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.carriageStoreTradeCount = ES3.Load("carriageStoreTradeCount" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.carriageStoreTradeMoneyNum = ES3.Load("carriageStoreTradeMoneyNum" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.carriageStoreSellMagnification = ES3.Load("carriageStoreSellMagnification" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.currentShopRankFirstNum = ES3.Load("currentShopRankFirstNum" + selectSlotNum, 1, eS3Settings);
		PlayerDataManager.currentShopRankSecondNum = ES3.Load("currentShopRankSecondNum" + selectSlotNum, 1, eS3Settings);
		PlayerDataManager.itemShopPoint = ES3.Load("itemShopPoint" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.scenarioBattleSpeed = ES3.Load("scenarioBattleSpeed" + selectSlotNum, 1, eS3Settings);
		PlayerDataManager.isDungeonHeroineFollow = ES3.Load<bool>("isDungeonHeroineFollow" + selectSlotNum, eS3Settings);
		PlayerDataManager.DungeonHeroineFollowNum = ES3.Load<int>("DungeonHeroineFollowNum" + selectSlotNum, eS3Settings);
		PlayerDataManager.dungeonBattleSpeed = ES3.Load<int>("dungeonBattleSpeed" + selectSlotNum, eS3Settings);
		PlayerDataManager.dungeonMoveSpeed = ES3.Load<int>("dungeonMoveSpeed" + selectSlotNum, eS3Settings);
		PlayerDataManager.isDungeonMapAuto = ES3.Load<bool>("isDungeonMapAuto" + selectSlotNum, eS3Settings);
		PlayerDataManager.isResultAutoClose = ES3.Load<bool>("isResultAutoClose" + selectSlotNum, eS3Settings);
		PlayerDataManager.dungeonEnterTimeZoneNum = ES3.Load<int>("dungeonEnterTimeZoneNum" + selectSlotNum, eS3Settings);
		PlayerDataManager.rareDropRateRaisePowerNum = ES3.Load("rareDropRateRaisePowerNum" + selectSlotNum, 0, eS3Settings);
		PlayerDataManager.rareDropRateRaiseRaimingDaysNum = ES3.Load("rareDropRateRaiseRaimingDaysNum" + selectSlotNum, 0, eS3Settings);
		PlayerStatusDataManager.characterExp = ES3.Load<int[]>("characterExp" + selectSlotNum, eS3Settings);
		PlayerStatusDataManager.characterLv = ES3.Load<int[]>("characterLv" + selectSlotNum, eS3Settings);
		PlayerStatusDataManager.characterHp = ES3.Load<int[]>("characterHp" + selectSlotNum, eS3Settings);
		PlayerStatusDataManager.characterMp = ES3.Load<int[]>("characterMp" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveItemList = ES3.Load<List<HaveItemData>>("haveItemList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveItemMaterialList = ES3.Load<List<HaveItemData>>("haveItemMaterialList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveItemCanMakeMaterialList = ES3.Load<List<HaveItemData>>("haveItemCanMakeMaterialList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveItemCampItemList = ES3.Load<List<HaveCampItemData>>("haveItemCampItemlList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveItemMagicMaterialList = ES3.Load<List<HaveItemData>>("haveItemMagicMaterialList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveCashableItemList = ES3.Load<List<HaveItemData>>("haveCashableItemList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveEventItemList = ES3.Load<List<HaveEventItemData>>("haveEventItemList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveWeaponList = ES3.Load<List<HaveWeaponData>>("haveWeaponList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveArmorList = ES3.Load<List<HaveArmorData>>("haveArmorList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.havePartyWeaponList = ES3.Load<List<HavePartyWeaponData>>("havePartyWeaponList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.havePartyArmorList = ES3.Load<List<HavePartyArmorData>>("havePartyArmorList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.haveAccessoryList = ES3.Load<List<HaveAccessoryData>>("haveAccessoryList" + selectSlotNum, eS3Settings);
		PlayerInventoryDataManager.playerLearnedSkillList = ES3.Load<List<LearnedSkillData>>("playerLearnedSkillList" + selectSlotNum, eS3Settings);
		PlayerEquipDataManager.playerEquipSkillList = ES3.Load<List<int>[]>("playerEquipSkillList" + selectSlotNum, eS3Settings);
		PlayerFlagDataManager.scenarioFlagDictionary = new Dictionary<string, bool>((from dic in ES3.Load<Dictionary<string, bool>>("scenarioFlagDictionary" + selectSlotNum, eS3Settings).Concat(PlayerFlagDataManager.scenarioFlagDictionary)
			group dic by dic.Key).ToDictionary((IGrouping<string, KeyValuePair<string, bool>> dic) => dic.Key, (IGrouping<string, KeyValuePair<string, bool>> dic) => dic.FirstOrDefault().Value));
		PlayerFlagDataManager.tutorialFlagDictionary = new Dictionary<string, bool>((from dic in ES3.Load<Dictionary<string, bool>>("tutorialFlagDictionary" + selectSlotNum, eS3Settings).Concat(PlayerFlagDataManager.tutorialFlagDictionary)
			group dic by dic.Key).ToDictionary((IGrouping<string, KeyValuePair<string, bool>> dic) => dic.Key, (IGrouping<string, KeyValuePair<string, bool>> dic) => dic.FirstOrDefault().Value));
		PlayerFlagDataManager.sceneGarellyFlagDictionary = new Dictionary<string, bool>((from dic in ES3.Load<Dictionary<string, bool>>("sceneGarellyFlagDictionary").Concat(PlayerFlagDataManager.sceneGarellyFlagDictionary)
			group dic by dic.Key).ToDictionary((IGrouping<string, KeyValuePair<string, bool>> dic) => dic.Key, (IGrouping<string, KeyValuePair<string, bool>> dic) => dic.FirstOrDefault().Value));
		PlayerFlagDataManager.eventStartingDayDictionary = new Dictionary<string, int>((from dic in ES3.Load<Dictionary<string, int>>("eventStartingDayDictionary" + selectSlotNum, eS3Settings).Concat(PlayerFlagDataManager.eventStartingDayDictionary)
			group dic by dic.Key).ToDictionary((IGrouping<string, KeyValuePair<string, int>> dic) => dic.Key, (IGrouping<string, KeyValuePair<string, int>> dic) => dic.FirstOrDefault().Value));
		List<bool> defaultValue5 = new List<bool> { false, false, false, false, false, true };
		PlayerFlagDataManager.partyPowerUpFlagList = ES3.Load("partyPowerUpFlagList" + selectSlotNum, defaultValue5, eS3Settings);
		PlayerFlagDataManager.heroineAllTimeFollowFlagList = ES3.Load<List<bool>>("heroineAllTimeFollowFlagList" + selectSlotNum, eS3Settings);
		PlayerFlagDataManager.heroineFirstSexTouchFlagList = ES3.Load("heroineFirstSexTouchFlagList" + selectSlotNum, defaultValue5, eS3Settings);
		PlayerFlagDataManager.dungeonFlagDictionary = ES3.Load<Dictionary<string, bool>>("dungeonFlagDictionary" + selectSlotNum, eS3Settings);
		PlayerFlagDataManager.recipeFlagDictionary = ES3.Load<Dictionary<string, bool>>("recipeFlagDictionary" + selectSlotNum, eS3Settings);
		PlayerFlagDataManager.keyItemFlagDictionary = ES3.Load<Dictionary<string, bool>>("keyItemFlagDictionary" + selectSlotNum, eS3Settings);
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (KeyValuePair<string, int> item in PlayerFlagDataManager.deepDungeonFlagDictionary)
		{
			dictionary.Add(item.Key, 0);
		}
		PlayerFlagDataManager.deepDungeonFlagDictionary = ES3.Load("deepDungeonFlagDictionary" + selectSlotNum, dictionary, eS3Settings);
		Dictionary<string, bool> dictionary2 = new Dictionary<string, bool>();
		foreach (KeyValuePair<string, bool> item2 in PlayerFlagDataManager.dungeonDeepClearFlagDictionary)
		{
			dictionary2.Add(item2.Key, value: false);
		}
		PlayerFlagDataManager.dungeonDeepClearFlagDictionary = ES3.Load("dungeonDeepClearFlagDictionary" + selectSlotNum, dictionary2, eS3Settings);
		Dictionary<int, bool> dictionary3 = new Dictionary<int, bool>();
		foreach (KeyValuePair<int, bool> item3 in PlayerFlagDataManager.enableNewCraftFlagDictionary)
		{
			dictionary3.Add(item3.Key, value: false);
		}
		dictionary3[1000] = true;
		dictionary3[2000] = true;
		PlayerFlagDataManager.enableNewCraftFlagDictionary = ES3.Load("enableNewCraftFlagDictionary" + selectSlotNum, dictionary3, eS3Settings);
		Dictionary<string, bool> dictionary4 = new Dictionary<string, bool>();
		foreach (KeyValuePair<string, bool> item4 in PlayerFlagDataManager.priceSettingNoticeFlagDictionary)
		{
			dictionary4.Add(item4.Key, value: false);
		}
		PlayerFlagDataManager.priceSettingNoticeFlagDictionary = ES3.Load("priceSettingNoticeFlagDictionary" + selectSlotNum, dictionary4, eS3Settings);
		Dictionary<string, bool> dictionary5 = new Dictionary<string, bool>();
		foreach (KeyValuePair<string, bool> item5 in PlayerFlagDataManager.extraNoticeFlagDictionary)
		{
			dictionary5.Add(item5.Key, value: false);
		}
		PlayerFlagDataManager.extraNoticeFlagDictionary = ES3.Load("extraNoticeFlagDictionary" + selectSlotNum, dictionary5, eS3Settings);
		List<QuestClearData> list = ES3.Load<List<QuestClearData>>("questClearFlagList" + selectSlotNum, eS3Settings);
		for (int i = 0; i < PlayerFlagDataManager.questClearFlagList.Count; i++)
		{
			QuestClearData defaultQuestData = PlayerFlagDataManager.questClearFlagList[i];
			QuestClearData questClearData = list.Find((QuestClearData data) => data.sortID == defaultQuestData.sortID);
			if (questClearData == null)
			{
				list.Add(defaultQuestData);
			}
			else if (questClearData.needRequirementCount != defaultQuestData.needRequirementCount)
			{
				list.Find((QuestClearData data) => data.sortID == defaultQuestData.sortID).needRequirementCount = defaultQuestData.needRequirementCount;
			}
		}
		PlayerFlagDataManager.questClearFlagList = list;
		PlayerCraftStatusManager.playerCraftLv = ES3.Load<int>("playerCraftLv" + selectSlotNum, eS3Settings);
		PlayerCraftStatusManager.playerCraftExp = ES3.Load<int>("playerCraftExp" + selectSlotNum, eS3Settings);
		PlayerCraftStatusManager.craftFacilityDataDictionary = ES3.Load<Dictionary<string, CraftWorkShopData>>("craftFacilityDataDictionary" + selectSlotNum, eS3Settings);
		PlayerSexStatusDataManager.playerSexExp = ES3.Load("playerSexExp" + selectSlotNum, 0, eS3Settings);
		PlayerSexStatusDataManager.heroineSexExp = ES3.Load("heroineSexExp" + selectSlotNum, new int[4], eS3Settings);
		PlayerSexStatusDataManager.totalPistonCount = ES3.Load("totalPistonCount" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.totalMouthCount = ES3.Load("totalMouthCount" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.totalOutShotCount = ES3.Load("totalOutShotCount" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.totalInShotCount = ES3.Load("totalInShotCount" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.totalEcstasyCount = ES3.Load("totalEcstasyCount" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.totalSexCount = ES3.Load("totalSexCount" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.totalUniqueSexCount = ES3.Load("totalUniqueSexCount" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina = ES3.Load("heroineRemainingSemenCount_vagina" + selectSlotNum, new int[5], eS3Settings);
		PlayerSexStatusDataManager.heroineRemainingSemenCount_anal = ES3.Load("heroineRemainingSemenCount_anal" + selectSlotNum, new int[5], eS3Settings);
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
		PlayerNonSaveDataManager.isSexEnd = false;
		unityAction();
	}

	private void DataSaveEnd()
	{
		Debug.Log("セーブ終了");
		Transition(saveEnd);
	}

	private void DataLoadEnd()
	{
		Debug.Log("ロード終了");
		Transition(loadEnd);
	}
}
