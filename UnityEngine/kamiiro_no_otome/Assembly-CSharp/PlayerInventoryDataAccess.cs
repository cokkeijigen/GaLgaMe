using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryDataAccess : MonoBehaviour
{
	public static void HaveItemListSortAll()
	{
		PlayerInventoryDataManager.haveItemList.Sort((HaveItemData x, HaveItemData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveItemMaterialList.Sort((HaveItemData x, HaveItemData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveItemMagicMaterialList.Sort((HaveItemData x, HaveItemData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveEventItemList.Sort((HaveEventItemData x, HaveEventItemData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveItemCampItemList.Sort((HaveCampItemData x, HaveCampItemData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveItemCanMakeMaterialList.Sort((HaveItemData x, HaveItemData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveCashableItemList.Sort((HaveItemData x, HaveItemData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveAccessoryList.Sort((HaveAccessoryData x, HaveAccessoryData y) => x.itemSortID - y.itemSortID);
		PlayerInventoryDataManager.haveWeaponList = (from x in PlayerInventoryDataManager.haveWeaponList
			orderby x.itemSortID, x.weaponHaveFactor.Count
			select x).ToList();
		PlayerInventoryDataManager.haveArmorList = (from x in PlayerInventoryDataManager.haveArmorList
			orderby x.itemSortID, x.armorHaveFactor.Count
			select x).ToList();
		PlayerInventoryDataManager.havePartyWeaponList = (from x in PlayerInventoryDataManager.havePartyWeaponList
			orderby x.itemSortID, x.weaponHaveFactor.Count
			select x).ToList();
		PlayerInventoryDataManager.havePartyArmorList = (from x in PlayerInventoryDataManager.havePartyArmorList
			orderby x.itemSortID, x.armorHaveFactor.Count
			select x).ToList();
	}

	public static void HaveItemListSort()
	{
		PlayerInventoryDataManager.haveItemList.Sort((HaveItemData x, HaveItemData y) => x.itemSortID - y.itemSortID);
	}

	public static void PlayerHaveItemAdd(int addItemID, int addSortID, int addNum)
	{
		HaveItemData haveItemData = null;
		haveItemData = ((addItemID < 100) ? PlayerInventoryDataManager.haveItemList.Find((HaveItemData item) => item.itemID == addItemID) : ((addItemID < 600) ? PlayerInventoryDataManager.haveItemMaterialList.Find((HaveItemData item) => item.itemID == addItemID) : ((addItemID < 630) ? PlayerInventoryDataManager.haveItemCanMakeMaterialList.Find((HaveItemData item) => item.itemID == addItemID) : ((addItemID >= 900) ? PlayerInventoryDataManager.haveCashableItemList.Find((HaveItemData item) => item.itemID == addItemID) : PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData item) => item.itemID == addItemID)))));
		if (haveItemData != null)
		{
			haveItemData.haveCountNum += addNum;
			haveItemData.haveCountNum = Mathf.Clamp(haveItemData.haveCountNum, 0, 999);
			if (haveItemData.haveCountNum <= 0)
			{
				if (addItemID < 100)
				{
					PlayerInventoryDataManager.haveItemList.Remove(haveItemData);
				}
				else if (addItemID < 600)
				{
					PlayerInventoryDataManager.haveItemMaterialList.Remove(haveItemData);
				}
				else if (addItemID < 630)
				{
					PlayerInventoryDataManager.haveItemCanMakeMaterialList.Remove(haveItemData);
				}
				else if (addItemID < 900)
				{
					PlayerInventoryDataManager.haveItemMagicMaterialList.Remove(haveItemData);
				}
				else
				{
					PlayerInventoryDataManager.haveCashableItemList.Remove(haveItemData);
				}
			}
		}
		else
		{
			HaveItemData item2 = new HaveItemData
			{
				itemID = addItemID,
				itemSortID = addSortID,
				haveCountNum = addNum
			};
			if (addItemID < 100)
			{
				PlayerInventoryDataManager.haveItemList.Add(item2);
			}
			else if (addItemID < 600)
			{
				PlayerInventoryDataManager.haveItemMaterialList.Add(item2);
			}
			else if (addItemID < 630)
			{
				PlayerInventoryDataManager.haveItemCanMakeMaterialList.Add(item2);
			}
			else if (addItemID < 950)
			{
				PlayerInventoryDataManager.haveItemMagicMaterialList.Add(item2);
			}
			else
			{
				PlayerInventoryDataManager.haveCashableItemList.Add(item2);
			}
		}
		PlayerQuestDataManager.RefreshSupplyQuestHaveItemCount();
	}

	public static void PlayerHaveCampItemAdd(int addItemID, int addSortID)
	{
		string itemType = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData item) => item.itemID == addItemID).category.ToString();
		HaveCampItemData item2 = new HaveCampItemData
		{
			itemID = addItemID,
			itemSortID = addSortID,
			itemType = itemType,
			haveCountNum = 1
		};
		PlayerInventoryDataManager.haveItemCampItemList.Add(item2);
		PlayerQuestDataManager.RefreshSupplyQuestHaveItemCount();
	}

	public static void PlayerHaveEventItemAdd(int addItemID, int addSortID)
	{
		if (PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData item) => item.itemID == addItemID) == null)
		{
			HaveEventItemData item2 = new HaveEventItemData
			{
				itemID = addItemID,
				itemSortID = addSortID
			};
			PlayerInventoryDataManager.haveEventItemList.Add(item2);
		}
		ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == addItemID);
		if (!string.IsNullOrEmpty(itemEventItemData.rewardRecipeName))
		{
			if (itemEventItemData.rewardRecipeName == "recipe9000")
			{
				PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv = 1;
			}
			else
			{
				PlayerFlagDataManager.recipeFlagDictionary[itemEventItemData.rewardRecipeName] = true;
			}
		}
		if (itemEventItemData.getItemShopPoint != 0)
		{
			PlayerDataManager.itemShopPoint += itemEventItemData.getItemShopPoint;
		}
		PlayerQuestDataManager.RefreshSupplyQuestHaveItemCount();
		PlayerQuestDataManager.RefreshStoryQuestFlagData("itemGet");
	}

	public static int GetPlayerHaveItemNum(int itemID)
	{
		if (itemID < 100)
		{
			HaveItemData haveItemData = PlayerInventoryDataManager.haveItemList.Find((HaveItemData item) => item.itemID == itemID);
			if (haveItemData != null)
			{
				return haveItemData.haveCountNum;
			}
		}
		else if (itemID < 600)
		{
			HaveItemData haveItemData2 = PlayerInventoryDataManager.haveItemMaterialList.Find((HaveItemData item) => item.itemID == itemID);
			if (haveItemData2 != null)
			{
				return haveItemData2.haveCountNum;
			}
		}
		else if (itemID < 630)
		{
			HaveItemData haveItemData3 = PlayerInventoryDataManager.haveItemCanMakeMaterialList.Find((HaveItemData item) => item.itemID == itemID);
			if (haveItemData3 != null)
			{
				return haveItemData3.haveCountNum;
			}
		}
		else if (itemID < 680)
		{
			HaveCampItemData haveCampItemData = PlayerInventoryDataManager.haveItemCampItemList.Find((HaveCampItemData item) => item.itemID == itemID);
			if (haveCampItemData != null)
			{
				return haveCampItemData.haveCountNum;
			}
		}
		else if (itemID < 900)
		{
			HaveItemData haveItemData4 = PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData item) => item.itemID == itemID);
			if (haveItemData4 != null)
			{
				return haveItemData4.haveCountNum;
			}
		}
		else if (itemID < 950)
		{
			if (PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData item) => item.itemID == itemID) != null)
			{
				return 1;
			}
		}
		else if (itemID < 1000)
		{
			HaveItemData haveItemData5 = PlayerInventoryDataManager.haveCashableItemList.Find((HaveItemData item) => item.itemID == itemID);
			if (haveItemData5 != null)
			{
				return haveItemData5.haveCountNum;
			}
		}
		else
		{
			if (itemID < 1300)
			{
				return PlayerInventoryDataManager.haveWeaponList.FindAll((HaveWeaponData item) => item.itemID == itemID).Count;
			}
			if (itemID < 2000)
			{
				if (PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData item) => item.itemID == itemID) != null)
				{
					return 1;
				}
			}
			else
			{
				if (itemID < 2300)
				{
					return PlayerInventoryDataManager.haveArmorList.FindAll((HaveArmorData item) => item.itemID == itemID).Count;
				}
				if (itemID < 3000)
				{
					if (PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData item) => item.itemID == itemID) != null)
					{
						return 1;
					}
				}
				else if (PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData item) => item.itemID == itemID) != null)
				{
					return 1;
				}
			}
		}
		return 0;
	}

	public static void GetPlayerHaveAddOnItems(out List<HaveItemData> typeAddOnList, out List<HaveItemData> powerAddOnList, int equipType)
	{
		GetPlayerHaveMagicMaterialItems(out typeAddOnList, out powerAddOnList, 0, equipType);
	}

	public static void GetPlayerHaveWonderItems(out List<HaveItemData> typeWonderList, out List<HaveItemData> powerWonderList, int equipType)
	{
		GetPlayerHaveMagicMaterialItems(out typeWonderList, out powerWonderList, 1, equipType);
	}

	public static void GetPlayerHaveMagicMaterialItems(out List<HaveItemData> typeMagicMatList, out List<HaveItemData> powerMagicMatList, int magicMaterialType, int equipType)
	{
		List<HaveItemData> list = new List<HaveItemData>();
		List<HaveItemData> list2 = new List<HaveItemData>();
		int[] array = new int[2];
		switch (magicMaterialType)
		{
		case 0:
			array[0] = 700;
			array[1] = 800;
			break;
		case 1:
			array[0] = 800;
			array[1] = 900;
			break;
		default:
			typeMagicMatList = null;
			powerMagicMatList = null;
			return;
		}
		foreach (HaveItemData data in PlayerInventoryDataManager.haveItemMagicMaterialList)
		{
			if (data.itemID < array[0] || data.itemID >= array[1])
			{
				continue;
			}
			ItemMagicMaterialData itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData m) => m.itemID == data.itemID);
			if (!(itemMagicMaterialData != null))
			{
				continue;
			}
			if (itemMagicMaterialData.addOnType == ItemMagicMaterialData.AddOnType.type)
			{
				switch (equipType)
				{
				case 0:
					if (itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.weapon || itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.all)
					{
						list.Add(data);
					}
					break;
				case 1:
					if (itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.armor || itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.all)
					{
						list.Add(data);
					}
					break;
				}
			}
			else
			{
				if (itemMagicMaterialData.addOnType != ItemMagicMaterialData.AddOnType.power)
				{
					continue;
				}
				switch (equipType)
				{
				case 0:
					if (itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.weapon || itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.all)
					{
						list2.Add(data);
					}
					break;
				case 1:
					if (itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.armor || itemMagicMaterialData.equipType == ItemMagicMaterialData.EquipType.all)
					{
						list2.Add(data);
					}
					break;
				}
			}
		}
		typeMagicMatList = list;
		powerMagicMatList = list2;
	}

	public static bool ConsumePlayerHaveItems_COUNT(int itemID, int Count)
	{
		HaveItemData haveItemData = null;
		HaveCampItemData haveCampItemData = null;
		if (itemID < 100)
		{
			haveItemData = PlayerInventoryDataManager.haveItemList.Find((HaveItemData m) => m.itemID == itemID);
		}
		else if (itemID < 600)
		{
			haveItemData = PlayerInventoryDataManager.haveItemMaterialList.Find((HaveItemData m) => m.itemID == itemID);
		}
		else if (itemID < 630)
		{
			haveItemData = PlayerInventoryDataManager.haveItemCanMakeMaterialList.Find((HaveItemData m) => m.itemID == itemID);
		}
		else if (itemID < 680)
		{
			haveCampItemData = PlayerInventoryDataManager.haveItemCampItemList.Find((HaveCampItemData m) => m.itemID == itemID);
		}
		else if (itemID < 900)
		{
			haveItemData = PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData m) => m.itemID == itemID);
		}
		else if (itemID < 1000)
		{
			haveItemData = PlayerInventoryDataManager.haveCashableItemList.Find((HaveItemData m) => m.itemID == itemID);
		}
		if (haveItemData != null)
		{
			if (haveItemData.haveCountNum >= Count)
			{
				haveItemData.haveCountNum -= Count;
				if (haveItemData.haveCountNum == 0)
				{
					RemovePlayerHaveItems(itemID);
				}
				return true;
			}
		}
		else if (haveCampItemData != null && haveCampItemData.haveCountNum >= Count)
		{
			haveCampItemData.haveCountNum -= Count;
			if (haveCampItemData.haveCountNum == 0)
			{
				RemovePlayerHaveItems(itemID);
			}
			return true;
		}
		return false;
	}

	private static void RemovePlayerHaveItems(int itemID)
	{
		int num = -1;
		if (itemID < 100)
		{
			num = PlayerInventoryDataManager.haveItemList.FindIndex((HaveItemData m) => m.itemID == itemID);
			PlayerInventoryDataManager.haveItemList.RemoveAt(num);
		}
		else if (itemID < 600)
		{
			num = PlayerInventoryDataManager.haveItemMaterialList.FindIndex((HaveItemData m) => m.itemID == itemID);
			PlayerInventoryDataManager.haveItemMaterialList.RemoveAt(num);
		}
		else if (itemID < 630)
		{
			num = PlayerInventoryDataManager.haveItemCanMakeMaterialList.FindIndex((HaveItemData m) => m.itemID == itemID);
			PlayerInventoryDataManager.haveItemCanMakeMaterialList.RemoveAt(num);
		}
		else if (itemID < 680)
		{
			num = PlayerInventoryDataManager.haveItemCampItemList.FindIndex((HaveCampItemData m) => m.itemID == itemID);
			PlayerInventoryDataManager.haveItemCampItemList.RemoveAt(num);
		}
		else if (itemID < 900)
		{
			num = PlayerInventoryDataManager.haveItemMagicMaterialList.FindIndex((HaveItemData m) => m.itemID == itemID);
			PlayerInventoryDataManager.haveItemMagicMaterialList.RemoveAt(num);
		}
		else if (itemID < 1000)
		{
			num = PlayerInventoryDataManager.haveCashableItemList.FindIndex((HaveItemData m) => m.itemID == itemID);
			PlayerInventoryDataManager.haveCashableItemList.RemoveAt(num);
		}
	}

	public static bool ConsumePlayerHaveItems_SINGLE(int itemID)
	{
		int num = -1;
		if (itemID >= 900 && itemID < 1000)
		{
			num = PlayerInventoryDataManager.haveEventItemList.FindIndex((HaveEventItemData m) => m.itemID == itemID);
			if (num != -1)
			{
				PlayerInventoryDataManager.haveEventItemList.RemoveAt(num);
				return true;
			}
		}
		else if (itemID >= 3000)
		{
			num = PlayerInventoryDataManager.haveAccessoryList.FindIndex((HaveAccessoryData m) => m.itemID == itemID);
			if (num != -1)
			{
				PlayerInventoryDataEquipAccess.PlayerHaveAccessoryRemove(num);
				return true;
			}
		}
		return false;
	}

	public static int GetItemSortID(int itemID)
	{
		int num = 0;
		if (itemID < 100)
		{
			return GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 600)
		{
			return GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 630)
		{
			return GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 680)
		{
			return GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 900)
		{
			return GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 950)
		{
			return GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 1000)
		{
			return GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 1300)
		{
			return GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 2000)
		{
			return GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 2300)
		{
			return GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 3000)
		{
			return GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData m) => m.itemID == itemID).sortID;
		}
		if (itemID < 4000)
		{
			return GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData m) => m.itemID == itemID).sortID;
		}
		return GameDataManager.instance.questItemDataBase.questItemDataList.Find((ItemQuestItemData data) => data.itemID == itemID).sortID;
	}

	public static int GetItemPriceFromItemID(int itemID)
	{
		int num = 0;
		if (itemID < 100)
		{
			return GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData m) => m.itemID == itemID).price;
		}
		if (itemID < 600)
		{
			return GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData m) => m.itemID == itemID).price;
		}
		if (itemID < 630)
		{
			return GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData m) => m.itemID == itemID).price;
		}
		if (itemID < 900)
		{
			return GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData m) => m.itemID == itemID).price;
		}
		if (itemID < 950)
		{
			return GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData m) => m.itemID == itemID).price;
		}
		if (itemID < 1000)
		{
			return GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData m) => m.itemID == itemID).price;
		}
		if (itemID < 1300)
		{
			return GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData m) => m.itemID == itemID).price;
		}
		if (itemID < 2000)
		{
			return GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData m) => m.itemID == itemID).price;
		}
		if (itemID < 2300)
		{
			return GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData m) => m.itemID == itemID).price;
		}
		if (itemID < 3000)
		{
			return GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData m) => m.itemID == itemID).price;
		}
		return GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData m) => m.itemID == itemID).price;
	}

	public static int GetCharacterIdForInventoryItem(int itemID)
	{
		int result = 0;
		if (itemID < 1300)
		{
			result = 0;
		}
		else if (itemID < 1400)
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
		else if (itemID < 2300)
		{
			result = 0;
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
		return result;
	}

	public static Sprite GetItemSprite(int itemID)
	{
		Sprite sprite = null;
		if (itemID < 100)
		{
			return GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 600)
		{
			return GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 630)
		{
			return GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 680)
		{
			return GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 900)
		{
			return GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 950)
		{
			return GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 1000)
		{
			return GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 1300)
		{
			return GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 2300)
		{
			return GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == itemID).itemSprite;
		}
		if (itemID < 4000)
		{
			return GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == itemID).itemSprite;
		}
		return GameDataManager.instance.questItemDataBase.questItemDataList.Find((ItemQuestItemData data) => data.itemID == itemID).itemSprite;
	}

	public static string GetItemNameTerm(int itemID)
	{
		string text = "";
		text = ((itemID < 100) ? GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemID).category.ToString() : ((itemID < 600) ? GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData data) => data.itemID == itemID).category.ToString() : ((itemID < 630) ? GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == itemID).category.ToString() : ((itemID < 680) ? GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == itemID).category.ToString() : ((itemID < 850) ? GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == itemID).category.ToString() : ((itemID < 900) ? GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == itemID).category.ToString() : ((itemID < 950) ? GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == itemID).category.ToString() : ((itemID < 1000) ? GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData data) => data.itemID == itemID).category.ToString() : ((itemID < 1300) ? GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID).category.ToString() : ((itemID < 2300) ? "armor" : ((itemID >= 4000) ? GameDataManager.instance.questItemDataBase.questItemDataList.Find((ItemQuestItemData data) => data.itemID == itemID).category.ToString() : GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == itemID).category.ToString())))))))))));
		return text + itemID;
	}

	public static string GetItemCategory(int itemID)
	{
		string text = "";
		if (itemID < 100)
		{
			return GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemID).category.ToString();
		}
		if (itemID < 600)
		{
			return GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData data) => data.itemID == itemID).category.ToString();
		}
		if (itemID < 630)
		{
			return GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == itemID).category.ToString();
		}
		if (itemID < 680)
		{
			return GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == itemID).category.ToString();
		}
		if (itemID < 900)
		{
			return GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == itemID).category.ToString();
		}
		if (itemID < 950)
		{
			return GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == itemID).category.ToString();
		}
		if (itemID < 1000)
		{
			return GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData data) => data.itemID == itemID).category.ToString();
		}
		if (itemID < 4000)
		{
			return GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == itemID).category.ToString();
		}
		return GameDataManager.instance.questItemDataBase.questItemDataList.Find((ItemQuestItemData data) => data.itemID == itemID).category.ToString();
	}
}
