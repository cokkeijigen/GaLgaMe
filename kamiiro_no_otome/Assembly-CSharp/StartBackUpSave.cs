using System;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class StartBackUpSave : StateBehaviour
{
	public StateLink stateLink;

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
		if (ES3.FileExists("SaveFile_BackUp.es3"))
		{
			ES3.CacheFile("SaveFile_BackUp.es3");
		}
		eS3Settings = new ES3Settings("SaveFile_BackUp.es3", ES3.Location.Cache);
		selectSlotNum = PlayerNonSaveDataManager.selectSlotPageNum * 9 + PlayerNonSaveDataManager.selectSlotNum + 1;
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
			savePlaceLocTerm = "area" + PlayerDataManager.currentDungeonName;
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
		ES3.StoreCachedFile("SaveFile_BackUp.es3");
		Debug.Log("バックアップセーブ完了");
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
