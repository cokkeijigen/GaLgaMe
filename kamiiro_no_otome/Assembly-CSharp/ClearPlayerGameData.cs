using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ClearPlayerGameData : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerInventoryDataManager.haveItemList.Clear();
		PlayerInventoryDataManager.haveItemMaterialList.Clear();
		PlayerInventoryDataManager.haveItemCanMakeMaterialList.Clear();
		PlayerInventoryDataManager.haveItemMagicMaterialList.Clear();
		PlayerInventoryDataManager.haveCashableItemList.Clear();
		PlayerInventoryDataManager.haveEventItemList.Clear();
		PlayerInventoryDataManager.haveWeaponList.Clear();
		PlayerInventoryDataManager.haveArmorList.Clear();
		PlayerInventoryDataManager.havePartyWeaponList.Clear();
		PlayerInventoryDataManager.havePartyArmorList.Clear();
		PlayerInventoryDataManager.haveAccessoryList.Clear();
		for (int i = 0; i < PlayerStatusDataManager.characterExp.Length; i++)
		{
			PlayerStatusDataManager.characterExp[i] = 0;
		}
		for (int j = 0; j < PlayerStatusDataManager.characterLv.Length; j++)
		{
			PlayerStatusDataManager.characterLv[j] = 1;
		}
		PlayerInventoryDataManager.playerLearnedSkillList.Clear();
		List<int>[] playerEquipSkillList = PlayerEquipDataManager.playerEquipSkillList;
		for (int k = 0; k < playerEquipSkillList.Length; k++)
		{
			playerEquipSkillList[k].Clear();
		}
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
		PlayerCraftStatusManager.playerCraftLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].furnaceLv = 1;
		PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv = 0;
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
		foreach (string item4 in PlayerFlagDataManager.recipeFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.recipeFlagDictionary[item4] = false;
		}
		for (int m = 0; m < PlayerFlagDataManager.questClearFlagList.Count; m++)
		{
			PlayerFlagDataManager.questClearFlagList[m].isClear = false;
		}
		foreach (string item5 in PlayerFlagDataManager.keyItemFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.keyItemFlagDictionary[item5] = false;
		}
		PlayerFlagDataManager.keyItemFlagDictionary["campItem630"] = true;
		foreach (string item6 in PlayerFlagDataManager.eventStartingDayDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.eventStartingDayDictionary[item6] = int.MaxValue;
		}
		foreach (string item7 in PlayerFlagDataManager.priceSettingNoticeFlagDictionary.Keys.ToList())
		{
			PlayerFlagDataManager.priceSettingNoticeFlagDictionary[item7] = false;
		}
		PlayerNonSaveDataManager.isUtageToWorldMapInDoor = false;
		PlayerNonSaveDataManager.isScenarioBattle = false;
		PlayerNonSaveDataManager.isMoveToDungeonBattle = false;
		PlayerNonSaveDataManager.isInDoorExitLock = false;
		PlayerNonSaveDataManager.inDoorHeroineExist = false;
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
