using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryFactorManager : MonoBehaviour
{
	public static void GetPlayerHaveWeaponFactorList(int itemID, int instanceID, ref List<HaveFactorData> dict)
	{
		if (itemID < 1300)
		{
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(haveWeaponData.weaponHaveFactor);
		}
		else
		{
			HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(havePartyWeaponData.weaponHaveFactor);
		}
	}

	public static void GetPlayerHaveArmorFactorList(int itemID, int instanceID, ref List<HaveFactorData> dict)
	{
		if (itemID < 2300)
		{
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(haveArmorData.armorHaveFactor);
		}
		else
		{
			HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(havePartyArmorData.armorHaveFactor);
		}
	}

	public static void GetPlayerHaveWeaponSetFactorList(int itemID, int instanceID, ref List<HaveFactorData> dict)
	{
		if (itemID < 1300)
		{
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(haveWeaponData.weaponSetFactor);
		}
		else
		{
			HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(havePartyWeaponData.weaponSetFactor);
		}
	}

	public static void GetPlayerHaveArmorSetFactorList(int itemID, int instanceID, ref List<HaveFactorData> dict)
	{
		if (itemID < 2300)
		{
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(haveArmorData.armorSetFactor);
		}
		else
		{
			HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			dict = new List<HaveFactorData>(havePartyArmorData.armorSetFactor);
		}
	}

	public static int SetPlayerHaveWeaponFactor(int itemID, int instanceID, int factorID, int factorLV)
	{
		int factorPower = 0;
		FactorData factorData = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == factorID);
		if (factorData != null)
		{
			int index = factorLV;
			if (factorData.powerList.Count <= factorLV)
			{
				index = factorData.powerList.Count - 1;
			}
			factorPower = factorData.powerList[index];
		}
		return SetPlayerHaveWeaponFactor(itemID, instanceID, factorID, factorLV, factorPower);
	}

	public static int SetPlayerHaveWeaponFactor(int itemID, int instanceID, int factorID, int factorLV, int factorPower)
	{
		ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData m) => m.itemID == itemID);
		HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
		if (itemWeaponData != null && haveWeaponData != null)
		{
			int availableMinUniqueID_FactorList = GetAvailableMinUniqueID_FactorList(haveWeaponData.weaponHaveFactor);
			HaveFactorData item = new HaveFactorData
			{
				factorID = factorID,
				factorLV = factorLV,
				factorPower = factorPower,
				uniqueID = availableMinUniqueID_FactorList
			};
			haveWeaponData.weaponHaveFactor.Add(item);
			return availableMinUniqueID_FactorList;
		}
		return -1;
	}

	public static int SetPlayerHaveArmorFactor(int itemID, int instanceID, int factorID, int factorLV)
	{
		int factorPower = 0;
		FactorData factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorID == factorID);
		if (factorData != null)
		{
			int index = factorLV;
			if (factorData.powerList.Count > factorLV)
			{
				index = factorData.powerList.Count - 1;
			}
			factorPower = factorData.powerList[index];
		}
		return SetPlayerHaveArmorFactor(itemID, instanceID, factorID, factorLV, factorPower);
	}

	public static int SetPlayerHaveArmorFactor(int itemID, int instanceID, int factorID, int factorLV, int factorPower)
	{
		ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData m) => m.itemID == itemID);
		HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
		if (itemArmorData != null && haveArmorData != null)
		{
			int availableMinUniqueID_FactorList = GetAvailableMinUniqueID_FactorList(haveArmorData.armorHaveFactor);
			HaveFactorData item = new HaveFactorData
			{
				factorID = factorID,
				factorLV = factorLV,
				factorPower = factorPower,
				uniqueID = availableMinUniqueID_FactorList
			};
			haveArmorData.armorHaveFactor.Add(item);
			return availableMinUniqueID_FactorList;
		}
		return -1;
	}

	public static int SetPlayerHavePartyWeaponFactor(int itemID, int factorID, int factorLV)
	{
		int factorPower = 0;
		FactorData factorData = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == factorID);
		if (factorData != null)
		{
			int index = factorLV;
			if (factorData.powerList.Count > factorLV)
			{
				index = factorData.powerList.Count - 1;
			}
			factorPower = factorData.powerList[index];
		}
		return SetPlayerHavePartyWeaponFactor(itemID, factorID, factorLV, factorPower);
	}

	public static int SetPlayerHavePartyWeaponFactor(int itemID, int factorID, int factorLV, int factorPower)
	{
		ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData m) => m.itemID == itemID);
		HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData m) => m.itemID == itemID);
		if (itemPartyWeaponData != null && havePartyWeaponData != null)
		{
			int availableMinUniqueID_FactorList = GetAvailableMinUniqueID_FactorList(havePartyWeaponData.weaponHaveFactor);
			HaveFactorData item = new HaveFactorData
			{
				factorID = factorID,
				factorLV = factorLV,
				factorPower = factorPower,
				uniqueID = availableMinUniqueID_FactorList
			};
			havePartyWeaponData.weaponHaveFactor.Add(item);
			return availableMinUniqueID_FactorList;
		}
		return -1;
	}

	public static int SetPlayerHavePartyArmorFactor(int itemID, int factorID, int factorLV)
	{
		int factorPower = 0;
		FactorData factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorID == factorID);
		if (factorData != null)
		{
			int index = factorLV;
			if (factorData.powerList.Count > factorLV)
			{
				index = factorData.powerList.Count - 1;
			}
			factorPower = factorData.powerList[index];
		}
		return SetPlayerHavePartyArmorFactor(itemID, factorID, factorLV, factorPower);
	}

	public static int SetPlayerHavePartyArmorFactor(int itemID, int factorID, int factorLV, int factorPower)
	{
		ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData m) => m.itemID == itemID);
		HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData m) => m.itemID == itemID);
		if (itemPartyArmorData != null && havePartyArmorData != null)
		{
			int availableMinUniqueID_FactorList = GetAvailableMinUniqueID_FactorList(havePartyArmorData.armorHaveFactor);
			HaveFactorData item = new HaveFactorData
			{
				factorID = factorID,
				factorLV = factorLV,
				factorPower = factorPower,
				uniqueID = availableMinUniqueID_FactorList
			};
			havePartyArmorData.armorHaveFactor.Add(item);
			return availableMinUniqueID_FactorList;
		}
		return -1;
	}

	private static void PlayerHaveFactorRemove(List<HaveFactorData> haveList, List<HaveFactorData> setList, int factorUniqueID)
	{
		int num = haveList.FindIndex((HaveFactorData m) => m.uniqueID == factorUniqueID);
		if (num != -1)
		{
			haveList.RemoveAt(num);
		}
		int num2 = setList.FindIndex((HaveFactorData m) => m.uniqueID == factorUniqueID);
		if (num2 != -1)
		{
			setList.RemoveAt(num2);
		}
	}

	public static void PlayerHaveWeaponFactorRemove(int itemID, int instanceID, int factorUniqueID)
	{
		HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
		if (haveWeaponData != null)
		{
			PlayerHaveFactorRemove(haveWeaponData.weaponHaveFactor, haveWeaponData.weaponSetFactor, factorUniqueID);
		}
	}

	public static void PlayerHaveArmorFactorRemove(int itemID, int instanceID, int factorUniqueID)
	{
		HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
		if (haveArmorData != null)
		{
			PlayerHaveFactorRemove(haveArmorData.armorHaveFactor, haveArmorData.armorSetFactor, factorUniqueID);
		}
	}

	public static void PlayerHavePartyWeaponFactorRemove(int itemID, int factorUniqueID)
	{
		HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData m) => m.itemID == itemID);
		if (havePartyWeaponData != null)
		{
			PlayerHaveFactorRemove(havePartyWeaponData.weaponHaveFactor, havePartyWeaponData.weaponSetFactor, factorUniqueID);
		}
	}

	public static void PlayerHavePartyArmorFactorRemove(int itemID, int factorUniqueID)
	{
		HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData m) => m.itemID == itemID);
		if (havePartyArmorData != null)
		{
			PlayerHaveFactorRemove(havePartyArmorData.armorHaveFactor, havePartyArmorData.armorSetFactor, factorUniqueID);
		}
	}

	private static int GetAvailableMinUniqueID_FactorList(List<HaveFactorData> factorList)
	{
		List<HaveFactorData> list = null;
		int num = 0;
		if (factorList.Count > 0)
		{
			list = factorList.OrderBy((HaveFactorData m) => m.uniqueID).ToList();
			for (int i = 0; i < list.Count && num >= list[i].uniqueID; i++)
			{
				num = list[i].uniqueID + 1;
			}
		}
		return num;
	}

	public static void HaveItemFactorSort()
	{
		int num = 0;
		num = PlayerInventoryDataManager.haveWeaponList.Count();
		for (int i = 0; i < num; i++)
		{
			List<HaveFactorData> weaponHaveFactor = (from x in PlayerInventoryDataManager.haveWeaponList[i].weaponHaveFactor
				orderby x.factorID, x.factorPower
				select x).ToList();
			PlayerInventoryDataManager.haveWeaponList[i].weaponHaveFactor = weaponHaveFactor;
		}
		num = PlayerInventoryDataManager.haveArmorList.Count();
		for (int j = 0; j < num; j++)
		{
			List<HaveFactorData> armorHaveFactor = (from x in PlayerInventoryDataManager.haveArmorList[j].armorHaveFactor
				orderby x.factorID, x.factorPower
				select x).ToList();
			PlayerInventoryDataManager.haveArmorList[j].armorHaveFactor = armorHaveFactor;
		}
		num = PlayerInventoryDataManager.havePartyWeaponList.Count();
		for (int k = 0; k < num; k++)
		{
			List<HaveFactorData> weaponHaveFactor2 = (from x in PlayerInventoryDataManager.havePartyWeaponList[k].weaponHaveFactor
				orderby x.factorID, x.factorPower
				select x).ToList();
			PlayerInventoryDataManager.havePartyWeaponList[k].weaponHaveFactor = weaponHaveFactor2;
		}
		num = PlayerInventoryDataManager.havePartyArmorList.Count();
		for (int l = 0; l < num; l++)
		{
			List<HaveFactorData> armorHaveFactor2 = (from x in PlayerInventoryDataManager.havePartyArmorList[l].armorHaveFactor
				orderby x.factorID, x.factorPower
				select x).ToList();
			PlayerInventoryDataManager.havePartyArmorList[l].armorHaveFactor = armorHaveFactor2;
		}
	}

	public static int[] GetPlayerHaveItemFactorLvArray(int itemID, int instanceID)
	{
		int[] array = new int[4];
		int index = 0;
		if (itemID < 2000)
		{
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			for (int i = 0; i < 4; i++)
			{
				index = i + 1;
				array[i] = haveWeaponData.weaponHaveFactor.Where((HaveFactorData data) => data.factorLV == index).Count();
			}
		}
		else
		{
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemID && m.itemUniqueID == instanceID);
			for (int j = 0; j < 4; j++)
			{
				index = j + 1;
				array[j] = haveArmorData.armorHaveFactor.Where((HaveFactorData data) => data.factorLV == index).Count();
			}
		}
		return array;
	}
}
