using System.Collections.Generic;

public class HaveWeaponData
{
	public int itemSortID;

	public int itemID;

	public int itemUniqueID;

	public int weaponIncludeMp;

	public int weaponIncludeMaxMp;

	public List<HaveFactorData> weaponHaveFactor = new List<HaveFactorData>();

	public List<HaveFactorData> weaponSetFactor = new List<HaveFactorData>();

	public List<int> weaponSetSkill = new List<int>();

	public int sellPrice;

	public int sellPriceMagnification;

	public bool isItemStoreDisplay;

	public bool isItemStoreDisplayLock;

	public int itemEnhanceCount;

	public int remainingDaysToCraft;

	public int equipCharacter = 9;
}
