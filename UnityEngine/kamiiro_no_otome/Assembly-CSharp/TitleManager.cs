using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
	public PlayMakerFSM startButtonFSM;

	public GameObject voiceDisableDialogCanvasGo;

	public Text versionNumText;

	public GameObject demoTextGo;

	private void Awake()
	{
		versionNumText.text = Application.version.ToString();
		if (PlayerNonSaveDataManager.isDemo)
		{
			demoTextGo.SetActive(value: true);
		}
		else
		{
			demoTextGo.SetActive(value: false);
		}
	}

	public void ClearPlayerGameData()
	{
		PlayerInventoryDataManager.haveItemList.Clear();
		PlayerInventoryDataManager.haveItemMaterialList.Clear();
		PlayerInventoryDataManager.haveItemCanMakeMaterialList.Clear();
		PlayerInventoryDataManager.haveItemCampItemList.Clear();
		PlayerInventoryDataManager.haveItemMagicMaterialList.Clear();
		PlayerInventoryDataManager.haveCashableItemList.Clear();
		PlayerInventoryDataManager.haveEventItemList.Clear();
		PlayerInventoryDataManager.haveWeaponList.Clear();
		PlayerInventoryDataManager.haveArmorList.Clear();
		PlayerInventoryDataManager.havePartyWeaponList.Clear();
		PlayerInventoryDataManager.havePartyArmorList.Clear();
		PlayerInventoryDataManager.haveAccessoryList.Clear();
		PlayerInventoryDataAccess.PlayerHaveItemAdd(0, 0, 2);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(300, 300, 3);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(301, 301, 3);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(600, 600, 5);
		PlayerInventoryDataAccess.PlayerHaveCampItemAdd(630, 630);
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponAdd(GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData item) => item.itemID == 1000), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1000, 0, 0);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1300), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1300, 0, 1);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1400), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1400, 0, 2);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1500), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1500, 0, 3);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1600), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1600, 0, 4);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1700), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1700, 0, 5);
		PlayerInventoryDataEquipAccess.PlayerHaveArmorAdd(GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData item) => item.itemID == 2000), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2000, 0, 0);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2300), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2300, 0, 1);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2400), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2400, 0, 2);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2500), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2500, 0, 3);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2600), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2600, 0, 4);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2700), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2700, 0, 5);
		HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == 1000 && data.itemUniqueID == 0);
		haveWeaponData.weaponIncludeMp = haveWeaponData.weaponIncludeMaxMp;
		for (int i = 0; i < PlayerStatusDataManager.characterExp.Length; i++)
		{
			PlayerStatusDataManager.characterExp[i] = 0;
		}
		for (int j = 0; j < PlayerStatusDataManager.characterLv.Length; j++)
		{
			PlayerStatusDataManager.characterLv[j] = 1;
		}
		PlayerStatusDataManager.characterExp[1] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[4];
		PlayerStatusDataManager.characterExp[2] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[4];
		PlayerStatusDataManager.characterExp[4] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[24];
		PlayerStatusDataManager.characterExp[5] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[49];
		RestoreSexStatus();
		PlayerInventoryDataManager.playerLearnedSkillList.Clear();
		List<int>[] playerEquipSkillList = PlayerEquipDataManager.playerEquipSkillList;
		for (int k = 0; k < playerEquipSkillList.Length; k++)
		{
			playerEquipSkillList[k].Clear();
		}
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
		List<int> list = new List<int>();
		list = new List<int> { 0 };
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponSetSkill(1000, list);
		PlayerEquipDataManager.playerEquipSkillList[0] = new List<int> { 0 };
		list = new List<int> { 10, 410 };
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponSetSkill(1300, list);
		PlayerEquipDataManager.playerEquipSkillList[1] = new List<int> { 10, 410 };
		list = new List<int> { 20, 21 };
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponSetSkill(1400, list);
		PlayerEquipDataManager.playerEquipSkillList[2] = new List<int> { 20, 21 };
		list = new List<int> { 130, 430 };
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponSetSkill(1500, list);
		PlayerEquipDataManager.playerEquipSkillList[3] = new List<int> { 130, 430 };
		list = new List<int> { 40, 41 };
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponSetSkill(1600, list);
		PlayerEquipDataManager.playerEquipSkillList[4] = new List<int> { 40, 41 };
		list = new List<int> { 70, 71 };
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponSetSkill(1700, list);
		PlayerEquipDataManager.playerEquipSkillList[5] = new List<int> { 70, 71 };
		PlayerInventoryDataEquipAccess.AddDefaultHeroineLearnedSkill(1);
		PlayerDataManager.playerPartyMember.Clear();
		PlayerDataManager.playerPartyMember.Add(0);
		PlayerDataManager.currentTimeZone = 0;
		PlayerDataManager.totalTimeZoneCount = 4;
		PlayerDataManager.currentMonthDay = 1;
		PlayerDataManager.currentTotalDay = 1;
		PlayerDataManager.playerHaveMoney = 500;
		PlayerDataManager.playerHaveKizunaPoint = 0;
		PlayerDataManager.playerLibido = 0;
		PlayerDataManager.currentAccessPointName = "Village1";
		PlayerNonSaveDataManager.resultScenarioName = "M_Main_001-1";
		PlayerDataManager.isDungeonHeroineFollow = false;
		PlayerDataManager.scenarioBattleSpeed = 1;
		PlayerDataManager.dungeonBattleSpeed = 1;
		PlayerDataManager.dungeonMoveSpeed = 1;
		PlayerCraftStatusManager.playerCraftLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].furnaceLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv = 0;
		PlayerDataManager.currentShopRankFirstNum = 1;
		PlayerDataManager.currentShopRankSecondNum = 1;
		PlayerDataManager.RefreshHotSellCategory();
		PlayerDataManager.carriageStoreSellMagnification = 100;
		foreach (string item in PlayerFlagDataManager.scenarioFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.scenarioFlagDictionary[item] = false;
		}
		foreach (string item2 in PlayerFlagDataManager.tutorialFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.tutorialFlagDictionary[item2] = false;
		}
		for (int l = 0; l < PlayerFlagDataManager.partyPowerUpFlagList.Count; l++)
		{
			PlayerFlagDataManager.partyPowerUpFlagList[l] = false;
		}
		PlayerFlagDataManager.partyPowerUpFlagList[1] = true;
		PlayerFlagDataManager.partyPowerUpFlagList[5] = true;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[0] = true;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[1] = true;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[2] = false;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[3] = false;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[4] = false;
		foreach (string item3 in PlayerFlagDataManager.dungeonFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.dungeonFlagDictionary[item3] = false;
		}
		foreach (string item4 in PlayerFlagDataManager.deepDungeonFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.deepDungeonFlagDictionary[item4] = 0;
		}
		foreach (string item5 in PlayerFlagDataManager.recipeFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.recipeFlagDictionary[item5] = false;
		}
		for (int m = 0; m < PlayerFlagDataManager.questClearFlagList.Count; m++)
		{
			PlayerFlagDataManager.questClearFlagList[m].isClear = false;
		}
		foreach (string item6 in PlayerFlagDataManager.keyItemFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.keyItemFlagDictionary[item6] = false;
		}
		foreach (string item7 in PlayerFlagDataManager.eventStartingDayDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.eventStartingDayDictionary[item7] = int.MaxValue;
		}
		foreach (string item8 in PlayerFlagDataManager.priceSettingNoticeFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.priceSettingNoticeFlagDictionary[item8] = false;
		}
		PlayerFlagDataManager.scenarioFlagDictionary["ScenarioInDoorFlag"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe1000"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe2000"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe1300"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe2300"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe600"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe630"] = true;
		PlayerFlagDataManager.keyItemFlagDictionary["campItem630"] = true;
	}

	public void ClearPlayerInventoryFactorData()
	{
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
	}

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: true, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		Debug.Log("Equipデータの更新完了");
	}

	private void RestoreSexStatus()
	{
		PlayerSexStatusDataManager.playerSexLv = 1;
		for (int i = 0; i < 4; i++)
		{
			PlayerSexStatusDataManager.heroineSexLv[i] = 1;
			PlayerSexStatusDataManager.heroineMouthLv[i] = 1;
			PlayerSexStatusDataManager.heroineHandLv[i] = 1;
			PlayerSexStatusDataManager.heroineTitsLv[i] = 1;
			PlayerSexStatusDataManager.heroineNippleLv[i] = 1;
			PlayerSexStatusDataManager.heroineWombsLv[i] = 1;
			PlayerSexStatusDataManager.heroineClitorisLv[i] = 1;
			PlayerSexStatusDataManager.heroineVaginaLv[i] = 1;
			PlayerSexStatusDataManager.heroineAnalLv[i] = 1;
			PlayerSexStatusDataManager.heroineTouchSexFlagArray[i] = false;
			PlayerSexStatusDataManager.heroineTouchCumShotFlagArray[i] = false;
		}
		for (int j = 0; j < 5; j++)
		{
			PlayerSexStatusDataManager.totalPistonCount[j] = 0;
			PlayerSexStatusDataManager.totalMouthCount[j] = 0;
			PlayerSexStatusDataManager.totalOutShotCount[j] = 0;
			PlayerSexStatusDataManager.totalInShotCount[j] = 0;
			PlayerSexStatusDataManager.totalCondomShotCount[j] = 0;
			PlayerSexStatusDataManager.totalEcstasyCount[j] = 0;
			PlayerSexStatusDataManager.totalSexCount[j] = 0;
			PlayerSexStatusDataManager.totalUniqueSexCount[j] = 0;
			PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[j] = 0;
			PlayerSexStatusDataManager.heroineRemainingSemenCount_anal[j] = 0;
		}
	}

	public void PushNewGameStartButton()
	{
		voiceDisableDialogCanvasGo.SetActive(value: true);
	}

	public void PushVoiseDisableDialogToggle(bool value)
	{
		PlayerOptionsDataManager.isAllVoiceDisable = value;
	}

	public void PushVoiceDisableDialogOkButton()
	{
		voiceDisableDialogCanvasGo.SetActive(value: false);
		startButtonFSM.SendEvent("NewGameStart");
		PlayerNonSaveDataManager.gameStartTime = Time.time;
	}

	public void PushRecommendSaveDialogToggle(bool value)
	{
		PlayerDataManager.isRecommendSaveAlertNoOpen = value;
	}

	public bool GetisRecommendSaveAlertNoOpen()
	{
		return PlayerDataManager.isRecommendSaveAlertNoOpen;
	}
}
