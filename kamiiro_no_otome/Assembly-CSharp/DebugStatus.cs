using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugStatus : StateBehaviour
{
	public Sprite bgSprite;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerDataManager.playerHaveMoney = 12345;
		PlayerDataManager.playerHaveKizunaPoint = 1000;
		PlayerDataManager.currentAccessPointName = "Village1";
		PlayerNonSaveDataManager.resultScenarioName = "M_Main_001-1";
		PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle1"] = true;
		PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle2"] = true;
		PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle3"] = true;
		PlayerFlagDataManager.tutorialFlagDictionary["chargeAttack"] = true;
		PlayerFlagDataManager.tutorialFlagDictionary["carriageStore"] = true;
		PlayerFlagDataManager.tutorialFlagDictionary["dungeonMap"] = true;
		PlayerFlagDataManager.tutorialFlagDictionary["dungeonBattle"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["H_Rina_001-5"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["H_Shia_001-5"] = true;
		PlayerStatusDataManager.characterExp[0] = 190;
		PlayerStatusDataManager.characterExp[1] = 1000;
		PlayerStatusDataManager.characterExp[5] = 100000;
		PlayerStatusDataManager.characterLv[0] = 1;
		PlayerStatusDataManager.characterLv[1] = 5;
		PlayerStatusDataManager.characterLv[2] = 1;
		PlayerStatusDataManager.characterLv[3] = 1;
		PlayerStatusDataManager.characterLv[4] = 1;
		PlayerStatusDataManager.characterLv[5] = 1;
		PlayerFlagDataManager.scenarioFlagDictionary["MH_Rina_009"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["MH_Shia_005"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["MH_Levy_001"] = true;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[2] = true;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[3] = true;
		PlayerFlagDataManager.heroineAllTimeFollowFlagList[4] = true;
		PlayerInventoryDataEquipAccess.AddDefaultHeroineLearnedSkill(1);
		PlayerFlagDataManager.recipeFlagDictionary["recipe1000"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe1001"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe2000"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe1300"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe1400"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe2300"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe2400"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe600"] = true;
		PlayerFlagDataManager.recipeFlagDictionary["recipe630"] = true;
		PlayerDataManager.scenarioBattleSpeed = 1;
		PlayerDataManager.dungeonBattleSpeed = 1;
		PlayerDataManager.dungeonMoveSpeed = 1;
		PlayerDataManager.isDungeonHeroineFollow = true;
		PlayerDataManager.DungeonHeroineFollowNum = 3;
		PlayerDataManager.currentTimeZone = 0;
		PlayerDataManager.totalTimeZoneCount = 0;
		PlayerDataManager.currentMonthDay = 0;
		PlayerDataManager.currentTotalDay = 0;
		PlayerCraftStatusManager.playerCraftLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].furnaceLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv = 1;
		PlayerFlagDataManager.scenarioFlagDictionary["ScenarioInDoorFlag"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["M_Main_001"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["M_Main_001-4"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["MH_Rina_004"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["MH_Rina_018"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["MH_Shia_001"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["MH_Shia_005"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["H_Shia_001-1"] = true;
		PlayerFlagDataManager.scenarioFlagDictionary["H_Shia_001-5"] = true;
		PlayerFlagDataManager.partyPowerUpFlagList[3] = true;
		PlayerNonSaveDataManager.sexBattleBgSprite = bgSprite;
		DebugAddItem();
		DebugAddSkill();
		DebugAddGarellyFlag();
		DebugSexStatus();
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
		PlayerDataManager.RefreshHotSellCategory();
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

	private void DebugAddItem()
	{
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponAdd(GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData item) => item.itemID == 1000), isEquip: false);
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponAdd(GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData item) => item.itemID == 1010), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1010, 0, 0);
		PlayerInventoryDataEquipAccess.PlayerHaveArmorAdd(GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData item) => item.itemID == 2000), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2000, 0, 0);
		PlayerInventoryDataEquipAccess.PlayerHaveArmorAdd(GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData item) => item.itemID == 2010), isEquip: false);
		PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == 3000));
		PlayerInventoryDataEquipAccess.SetPlayerHaveAccessoryEquip(0, 0);
		PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == 3001));
		PlayerInventoryDataEquipAccess.SetPlayerHaveAccessoryEquip(1, 4);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(0, 0, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(1, 1, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(10, 10, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(20, 20, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(30, 30, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(31, 31, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(40, 40, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(2, 2, 15);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(50, 50, 5);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(51, 51, 10);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(60, 60, 5);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(100, 100, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(101, 101, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(200, 200, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(300, 300, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(301, 301, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(302, 302, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(303, 303, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(600, 600, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(680, 680, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(681, 681, 100);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(850, 850, 10);
		PlayerInventoryDataAccess.PlayerHaveCampItemAdd(630, 630);
		PlayerInventoryDataAccess.PlayerHaveEventItemAdd(901, 901);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(950, 950, 5);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(951, 951, 6);
		PlayerDataManager.playerPartyMember.Add(0);
		PlayerDataManager.playerPartyMember.Add(1);
		PlayerDataManager.playerPartyMember.Add(2);
		PlayerDataManager.playerPartyMember.Add(3);
		PlayerDataManager.playerPartyMember.Add(4);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1310), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1310, 0, 1);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1400), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1400, 0, 2);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1510), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1510, 0, 3);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1610), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1610, 0, 4);
		PlayerInventoryDataEquipAccess.PlayerHavePartyWeaponAdd(GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == 1700), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerWeapon(1700, 0, 5);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2300), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2300, 0, 1);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2400), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2400, 0, 2);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2510), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2510, 0, 3);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2610), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2610, 0, 4);
		PlayerInventoryDataEquipAccess.PlayerHavePartyArmorAdd(GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == 2700), isEquip: true);
		PlayerEquipDataManager.SetEquipPlayerArmor(2700, 0, 5);
	}

	private void DebugAddSkill()
	{
		List<int> list = new List<int>();
		PlayerEquipDataManager.playerEquipSkillList[0] = new List<int> { 0 };
		list = new List<int> { 0 };
		PlayerInventoryDataEquipAccess.PlayerHaveWeaponSetSkill(1000, list);
	}

	private void DebugAddGarellyFlag()
	{
	}

	private void DebugSexStatus()
	{
		PlayerSexStatusDataManager.playerSexLv = 1;
		PlayerNonSaveDataManager.selectSexBattleHeroineId = 3;
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
		}
	}
}
