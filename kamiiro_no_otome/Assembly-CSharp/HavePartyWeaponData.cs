using System.Collections.Generic;

public class HavePartyWeaponData
{
	public int itemSortID;

	public int itemID;

	public int itemUniqueID;

	public List<HaveFactorData> weaponHaveFactor = new List<HaveFactorData>();

	public List<HaveFactorData> weaponSetFactor = new List<HaveFactorData>();

	public List<int> weaponSetSkill = new List<int>();

	public int equipCharacter = 9;
}
