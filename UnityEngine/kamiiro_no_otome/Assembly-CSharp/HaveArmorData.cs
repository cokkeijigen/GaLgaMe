using System.Collections.Generic;

public class HaveArmorData
{
	public int itemSortID;

	public int itemID;

	public int itemUniqueID;

	public List<HaveFactorData> armorHaveFactor = new List<HaveFactorData>();

	public List<HaveFactorData> armorSetFactor = new List<HaveFactorData>();

	public List<int> armorSetSkill = new List<int>();

	public int sellPrice;

	public int sellPriceMagnification;

	public bool isItemStoreDisplay;

	public bool isItemStoreDisplayLock;

	public int itemEnhanceCount;

	public int remainingDaysToCraft;

	public int equipCharacter = 9;
}
