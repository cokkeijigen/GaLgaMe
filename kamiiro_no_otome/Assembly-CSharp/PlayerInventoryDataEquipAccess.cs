using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryDataEquipAccess : MonoBehaviour
{
	public static void PlayerHaveWeaponAdd(ItemWeaponData data, bool isEquip)
	{
		int num = 0;
		if (isEquip)
		{
			num = 0;
			PlayerEquipDataManager.playerEquipWeaponID[0] = data.itemID;
		}
		else
		{
			num = 9;
		}
		HaveWeaponData item = new HaveWeaponData
		{
			itemID = data.itemID,
			itemSortID = data.sortID,
			weaponIncludeMp = Mathf.Clamp(data.weaponMp, 0, 999),
			weaponIncludeMaxMp = data.weaponMp,
			sellPriceMagnification = 100,
			isItemStoreDisplay = false,
			equipCharacter = num
		};
		PlayerInventoryDataManager.haveWeaponList.Add(item);
	}

	public static void PlayerHaveWeaponRemove(int itemID, int uniqueID)
	{
		HaveWeaponData item = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == itemID && data.itemUniqueID == uniqueID);
		PlayerInventoryDataManager.haveWeaponList.Remove(item);
	}

	public static void PlayerHaveArmorAdd(ItemArmorData data, bool isEquip)
	{
		int num = 0;
		if (isEquip)
		{
			num = 0;
			PlayerEquipDataManager.playerEquipArmorID[0] = data.itemID;
		}
		else
		{
			num = 9;
		}
		HaveArmorData item = new HaveArmorData
		{
			itemID = data.itemID,
			itemSortID = data.sortID,
			sellPriceMagnification = 100,
			isItemStoreDisplay = false,
			equipCharacter = num
		};
		PlayerInventoryDataManager.haveArmorList.Add(item);
	}

	public static void PlayerHaveArmorRemove(int itemID, int uniqueID)
	{
		HaveArmorData item = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == itemID && data.itemUniqueID == uniqueID);
		PlayerInventoryDataManager.haveArmorList.Remove(item);
	}

	public static void PlayerHaveAccessoryAdd(ItemAccessoryData data)
	{
		HaveAccessoryData item = new HaveAccessoryData
		{
			itemID = data.itemID,
			itemSortID = data.sortID,
			equipCharacter = 9
		};
		PlayerInventoryDataManager.haveAccessoryList.Add(item);
	}

	public static void PlayerHaveAccessoryRemove(int index)
	{
		PlayerInventoryDataManager.haveAccessoryList.RemoveAt(index);
	}

	public static void PlayerHavePartyWeaponAdd(ItemPartyWeaponData data, bool isEquip)
	{
		int num = 0;
		if (isEquip)
		{
			num = ((data.itemID < 1400) ? 1 : ((data.itemID < 1500) ? 2 : ((data.itemID < 1600) ? 3 : ((data.itemID >= 1700) ? 5 : 4))));
			PlayerEquipDataManager.playerEquipWeaponID[num] = data.itemID;
		}
		else
		{
			num = 9;
		}
		HavePartyWeaponData item = new HavePartyWeaponData
		{
			itemID = data.itemID,
			itemSortID = data.sortID,
			equipCharacter = num
		};
		PlayerInventoryDataManager.havePartyWeaponList.Add(item);
	}

	public static void PlayerHavePartyWeaponRemove(int index)
	{
		PlayerInventoryDataManager.havePartyWeaponList.RemoveAt(index);
	}

	public static void PlayerHavePartyArmorAdd(ItemPartyArmorData data, bool isEquip)
	{
		int num = 0;
		if (isEquip)
		{
			num = ((data.itemID < 2400) ? 1 : ((data.itemID < 2500) ? 2 : ((data.itemID < 2600) ? 3 : ((data.itemID >= 2700) ? 5 : 4))));
			PlayerEquipDataManager.playerEquipArmorID[num] = data.itemID;
		}
		else
		{
			num = 9;
		}
		HavePartyArmorData item = new HavePartyArmorData
		{
			itemID = data.itemID,
			itemSortID = data.sortID,
			equipCharacter = num
		};
		PlayerInventoryDataManager.havePartyArmorList.Add(item);
	}

	public static void PlayerHavePartyArmorRemove(int index)
	{
		PlayerInventoryDataManager.havePartyArmorList.RemoveAt(index);
	}

	public static void PlayerHaveWeaponSetSkill(int itemID, List<int> skillList)
	{
		if (itemID < 1300)
		{
			int index = PlayerInventoryDataManager.haveWeaponList.FindIndex((HaveWeaponData data) => data.itemID == itemID);
			PlayerInventoryDataManager.haveWeaponList[index].weaponSetSkill = skillList;
		}
		else
		{
			int index2 = PlayerInventoryDataManager.havePartyWeaponList.FindIndex((HavePartyWeaponData data) => data.itemID == itemID);
			PlayerInventoryDataManager.havePartyWeaponList[index2].weaponSetSkill = skillList;
		}
	}

	public static void PlayerHaveArmorSetSkill(int itemID, List<int> skillList)
	{
		if (itemID < 2300)
		{
			int index = PlayerInventoryDataManager.haveArmorList.FindIndex((HaveArmorData data) => data.itemID == itemID);
			PlayerInventoryDataManager.haveArmorList[index].armorSetSkill = skillList;
		}
		else
		{
			int index2 = PlayerInventoryDataManager.havePartyArmorList.FindIndex((HavePartyArmorData data) => data.itemID == itemID);
			PlayerInventoryDataManager.havePartyArmorList[index2].armorSetSkill = skillList;
		}
	}

	public static int[] GetPlayerHaveWeaponItemID(int characterID)
	{
		int[] array = new int[2];
		if (characterID == 0)
		{
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.equipCharacter == 0);
			if (haveWeaponData != null)
			{
				array[0] = haveWeaponData.itemID;
				array[1] = haveWeaponData.itemUniqueID;
			}
			else
			{
				array[0] = 0;
				array[1] = 0;
			}
			return array;
		}
		HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData m) => m.equipCharacter == characterID);
		if (havePartyWeaponData != null)
		{
			array[0] = havePartyWeaponData.itemID;
			array[1] = 0;
		}
		else
		{
			array[0] = 0;
			array[1] = 0;
		}
		return array;
	}

	public static int[] GetPlayerHaveArmorItemID(int characterID)
	{
		int[] array = new int[2];
		if (characterID == 0)
		{
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.equipCharacter == 0);
			if (haveArmorData != null)
			{
				array[0] = haveArmorData.itemID;
				array[1] = haveArmorData.itemUniqueID;
			}
			else
			{
				array[0] = 0;
				array[1] = 0;
			}
			return array;
		}
		HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData m) => m.equipCharacter == characterID);
		if (havePartyArmorData != null)
		{
			array[0] = havePartyArmorData.itemID;
			array[1] = 0;
		}
		else
		{
			array[0] = 0;
			array[1] = 0;
		}
		return array;
	}

	public static int GetPlayerHaveAccessoryID(int characterID)
	{
		int result = 0;
		HaveAccessoryData haveAccessoryData = PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData data) => data.equipCharacter == characterID);
		if (haveAccessoryData != null)
		{
			result = haveAccessoryData.itemID;
		}
		return result;
	}

	public static int GetPartyCharacterNumFromItemID(int itemID)
	{
		int result = 0;
		if (itemID < 1400)
		{
			result = 1;
		}
		else if (itemID < 1500)
		{
			result = 2;
		}
		else if (itemID < 1600)
		{
			result = 3;
		}
		else if (itemID < 1700)
		{
			result = 4;
		}
		else if (itemID < 1800)
		{
			result = 5;
		}
		else if (itemID < 2400)
		{
			result = 1;
		}
		else if (itemID < 2500)
		{
			result = 2;
		}
		else if (itemID < 2600)
		{
			result = 3;
		}
		else if (itemID < 2700)
		{
			result = 4;
		}
		else if (itemID < 2800)
		{
			result = 5;
		}
		return result;
	}

	public static bool SetPlayerHaveAccessoryEquip(int index, int characterNum)
	{
		if (PlayerInventoryDataManager.haveAccessoryList.Count > index)
		{
			PlayerInventoryDataManager.haveAccessoryList[index].equipCharacter = characterNum;
			return true;
		}
		return false;
	}

	public static void GetHaveWeaponListbyItemID(int itemID, ref List<HaveWeaponData> findList)
	{
		findList = PlayerInventoryDataManager.haveWeaponList.FindAll((HaveWeaponData m) => m.itemID == itemID);
	}

	public static void GetHaveArmorListbyItemID(int itemID, ref List<HaveArmorData> findList)
	{
		findList = PlayerInventoryDataManager.haveArmorList.FindAll((HaveArmorData m) => m.itemID == itemID);
	}

	public static int PlayerHaveWeaponAdd_INSTANCE(int weaponItemID, bool isEquip)
	{
		int num = 0;
		if (isEquip)
		{
			num = 0;
			PlayerEquipDataManager.playerEquipWeaponID[0] = weaponItemID;
		}
		else
		{
			num = 9;
		}
		ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData m) => m.itemID == weaponItemID);
		List<HaveWeaponData> list = PlayerInventoryDataManager.haveWeaponList.FindAll((HaveWeaponData m) => m.itemID == weaponItemID);
		int num2 = 0;
		if (list.Count > 0)
		{
			List<HaveWeaponData> list2 = list.OrderBy((HaveWeaponData d) => d.itemUniqueID).ToList();
			for (int i = 0; i < list2.Count && list2[i].itemUniqueID <= num2; i++)
			{
				num2++;
			}
		}
		Debug.Log("itemID:" + weaponItemID + " を個体識別番号:" + num2 + " で所持登録");
		int remainingDaysToCraft = GameDataManager.instance.needExpDataBase.remainingDaysToCraftLvList[PlayerCraftStatusManager.playerCraftLv - 1];
		HaveWeaponData item = new HaveWeaponData
		{
			itemID = itemWeaponData.itemID,
			itemSortID = itemWeaponData.sortID,
			itemUniqueID = num2,
			weaponIncludeMp = Mathf.Clamp(itemWeaponData.weaponMp, 0, 999),
			weaponIncludeMaxMp = itemWeaponData.weaponMp,
			sellPriceMagnification = PlayerDataManager.carriageStoreSellMagnification,
			isItemStoreDisplay = false,
			remainingDaysToCraft = remainingDaysToCraft,
			equipCharacter = num
		};
		PlayerInventoryDataManager.haveWeaponList.Add(item);
		return num2;
	}

	public static int PlayerHaveArmorAdd_INSTANCE(int armorItemID, bool isEquip)
	{
		int num = 0;
		if (isEquip)
		{
			num = 0;
			PlayerEquipDataManager.playerEquipArmorID[0] = armorItemID;
		}
		else
		{
			num = 9;
		}
		ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData m) => m.itemID == armorItemID);
		List<HaveArmorData> list = PlayerInventoryDataManager.haveArmorList.FindAll((HaveArmorData m) => m.itemID == armorItemID);
		int num2 = 0;
		if (list.Count > 0)
		{
			List<HaveArmorData> list2 = list.OrderBy((HaveArmorData d) => d.itemUniqueID).ToList();
			for (int i = 0; i < list2.Count && list2[i].itemUniqueID <= num2; i++)
			{
				num2++;
			}
		}
		Debug.Log("itemID:" + armorItemID + " を個体識別番号:" + num2 + " で所持登録");
		int remainingDaysToCraft = GameDataManager.instance.needExpDataBase.remainingDaysToCraftLvList[PlayerCraftStatusManager.playerCraftLv - 1];
		HaveArmorData item = new HaveArmorData
		{
			itemID = itemArmorData.itemID,
			itemSortID = itemArmorData.sortID,
			itemUniqueID = num2,
			sellPriceMagnification = PlayerDataManager.carriageStoreSellMagnification,
			isItemStoreDisplay = false,
			remainingDaysToCraft = remainingDaysToCraft,
			equipCharacter = num
		};
		PlayerInventoryDataManager.haveArmorList.Add(item);
		return num2;
	}

	public static int UpgradePlayerHaveWeapon(int itemID, int instanceID, int newItemID)
	{
		HaveWeaponData haveWeaponData = null;
		int num = 0;
		if (itemID >= 1000 && itemID < 1300)
		{
			haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			if (haveWeaponData != null)
			{
				ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData m) => m.itemID == newItemID);
				if (itemWeaponData != null)
				{
					num = itemWeaponData.sortID;
					int num2 = 0;
					List<HaveWeaponData> list = PlayerInventoryDataManager.haveWeaponList.FindAll((HaveWeaponData m) => m.itemID == newItemID);
					if (list.Count > 0)
					{
						List<HaveWeaponData> list2 = list.OrderBy((HaveWeaponData m) => m.itemUniqueID).ToList();
						for (int i = 0; i < list2.Count && list2[i].itemUniqueID <= num2; i++)
						{
							num2++;
						}
					}
					haveWeaponData.itemID = newItemID;
					haveWeaponData.itemUniqueID = num2;
					haveWeaponData.itemSortID = num;
				}
			}
		}
		return haveWeaponData.itemUniqueID;
	}

	public static void UpgradePlayerHavePartyWeapon(int itemID, int newItemID)
	{
		HavePartyWeaponData havePartyWeaponData = null;
		int num = 0;
		if (itemID < 1300 || itemID >= 2000)
		{
			return;
		}
		havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData m) => m.itemID == itemID);
		if (havePartyWeaponData != null)
		{
			ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData m) => m.itemID == newItemID);
			if (itemPartyWeaponData != null)
			{
				num = itemPartyWeaponData.sortID;
				havePartyWeaponData.itemID = newItemID;
				havePartyWeaponData.itemSortID = num;
			}
		}
	}

	public static int UpgradePlayerHaveArmor(int itemID, int instanceID, int newItemID)
	{
		HaveArmorData haveArmorData = null;
		int num = 0;
		if (itemID >= 2000 && itemID < 2300)
		{
			haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			if (haveArmorData != null)
			{
				ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData m) => m.itemID == newItemID);
				if (itemArmorData != null)
				{
					num = itemArmorData.sortID;
					int num2 = 0;
					List<HaveArmorData> list = PlayerInventoryDataManager.haveArmorList.FindAll((HaveArmorData m) => m.itemID == newItemID);
					if (list.Count > 0)
					{
						List<HaveArmorData> list2 = list.OrderBy((HaveArmorData m) => m.itemUniqueID).ToList();
						for (int i = 0; i < list2.Count && list2[i].itemUniqueID <= num2; i++)
						{
							num2++;
						}
					}
					haveArmorData.itemID = newItemID;
					haveArmorData.itemUniqueID = num2;
					haveArmorData.itemSortID = num;
				}
			}
		}
		return haveArmorData.itemUniqueID;
	}

	public static void UpgradePlayerHavePartyArmor(int itemID, int newItemID)
	{
		HavePartyArmorData havePartyArmorData = null;
		int num = 0;
		if (itemID < 2300 || itemID >= 3000)
		{
			return;
		}
		havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData m) => m.itemID == itemID);
		if (havePartyArmorData != null)
		{
			ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData m) => m.itemID == newItemID);
			if (itemPartyArmorData != null)
			{
				num = itemPartyArmorData.sortID;
				havePartyArmorData.itemID = newItemID;
				havePartyArmorData.itemSortID = num;
			}
		}
	}

	public static int GetEquipItemSellPrice(int itemID, int uniqueID)
	{
		int[] playerHaveItemFactorLvArray = PlayerInventoryFactorManager.GetPlayerHaveItemFactorLvArray(itemID, uniqueID);
		int num = playerHaveItemFactorLvArray[0] * 1000;
		int num2 = playerHaveItemFactorLvArray[1] * 3000;
		int num3 = playerHaveItemFactorLvArray[2] * 6000;
		int num4 = playerHaveItemFactorLvArray[3] * 10000;
		return PlayerInventoryDataAccess.GetItemPriceFromItemID(itemID) + (num + num2 + num3 + num4);
	}

	public static int GetEquipItemFactorPrice(int itemID, int uniqueID)
	{
		int[] playerHaveItemFactorLvArray = PlayerInventoryFactorManager.GetPlayerHaveItemFactorLvArray(itemID, uniqueID);
		int num = playerHaveItemFactorLvArray[0] * 1000;
		int num2 = playerHaveItemFactorLvArray[1] * 3000;
		int num3 = playerHaveItemFactorLvArray[2] * 6000;
		int num4 = playerHaveItemFactorLvArray[3] * 10000;
		return 0 + (num + num2 + num3 + num4);
	}

	public static string GetCraftItemNameTerm(int itemID)
	{
		string text = "";
		text = ((itemID < 630) ? GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == itemID).category.ToString() : ((itemID < 900) ? "campItem" : ((itemID < 1000) ? GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == itemID).category.ToString() : ((itemID < 1300) ? GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID).category.ToString() : ((itemID >= 2000) ? "armor" : GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == itemID).category.ToString())))));
		return text + itemID;
	}

	public static void SetItemStoreDisplayLock(int itemID, int uniqueID, bool setValue)
	{
		if (itemID < 2000)
		{
			PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == itemID && data.itemUniqueID == uniqueID).isItemStoreDisplayLock = setValue;
		}
		else
		{
			PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == itemID && data.itemUniqueID == uniqueID).isItemStoreDisplayLock = setValue;
		}
	}

	public static void AddLearnedSkill(int skillID)
	{
		BattleSkillData battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
		LearnedSkillData item = new LearnedSkillData
		{
			skillID = battleSkillData.skillID,
			skillSortID = battleSkillData.sortID
		};
		if (!PlayerInventoryDataManager.playerLearnedSkillList.Contains(item))
		{
			PlayerInventoryDataManager.playerLearnedSkillList.Add(item);
		}
		PlayerInventoryDataManager.playerLearnedSkillList = PlayerInventoryDataManager.playerLearnedSkillList.OrderBy((LearnedSkillData data) => data.skillSortID).ToList();
	}

	public static void AddDefaultHeroineLearnedSkill(int heroineID)
	{
		switch (heroineID)
		{
		case 1:
			AddLearnedSkill(50);
			AddLearnedSkill(411);
			break;
		case 2:
			AddLearnedSkill(51);
			break;
		case 3:
			AddLearnedSkill(52);
			AddLearnedSkill(450);
			break;
		case 4:
			AddLearnedSkill(53);
			break;
		}
		PlayerInventoryDataManager.playerLearnedSkillList = PlayerInventoryDataManager.playerLearnedSkillList.OrderBy((LearnedSkillData data) => data.skillSortID).ToList();
	}

	public static void AddEventHeroineLearnedSkill(int heroineID)
	{
		switch (heroineID)
		{
		case 1:
			AddLearnedSkill(60);
			AddLearnedSkill(250);
			AddLearnedSkill(251);
			break;
		case 2:
			AddLearnedSkill(262);
			break;
		}
		PlayerInventoryDataManager.playerLearnedSkillList = PlayerInventoryDataManager.playerLearnedSkillList.OrderBy((LearnedSkillData data) => data.skillSortID).ToList();
		Debug.Log("イベントでの習得スキルを追加／ID：" + heroineID);
	}

	public static void AddCreateWeaponEnhanceCount(int itemID, int instanceID, bool isPowerUp)
	{
		HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
		if (isPowerUp)
		{
			int remainingDaysToCraft = GameDataManager.instance.needExpDataBase.remainingDaysToCraftLvList[PlayerCraftStatusManager.playerCraftLv - 1];
			haveWeaponData.remainingDaysToCraft = remainingDaysToCraft;
			haveWeaponData.itemEnhanceCount = 0;
			return;
		}
		haveWeaponData.itemEnhanceCount++;
		ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID);
		haveWeaponData.itemEnhanceCount = Mathf.Clamp(haveWeaponData.itemEnhanceCount, 0, itemWeaponData.factorSlot);
	}

	public static void AddCreateArmorEnhanceCount(int itemID, int instanceID, bool isPowerUp)
	{
		HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
		if (isPowerUp)
		{
			int remainingDaysToCraft = GameDataManager.instance.needExpDataBase.remainingDaysToCraftLvList[PlayerCraftStatusManager.playerCraftLv - 1];
			haveArmorData.remainingDaysToCraft = remainingDaysToCraft;
			haveArmorData.itemEnhanceCount = 0;
			return;
		}
		haveArmorData.itemEnhanceCount++;
		ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == itemID);
		haveArmorData.itemEnhanceCount = Mathf.Clamp(haveArmorData.itemEnhanceCount, 0, itemArmorData.factorSlot);
	}
}
