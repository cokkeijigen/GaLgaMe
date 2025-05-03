using System.Collections.Generic;

public class HavePartyArmorData
{
	public int itemSortID;

	public int itemID;

	public int itemUniqueID;

	public List<HaveFactorData> armorHaveFactor = new List<HaveFactorData>();

	public List<HaveFactorData> armorSetFactor = new List<HaveFactorData>();

	public List<int> armorSetSkill = new List<int>();

	public int equipCharacter = 9;
}
