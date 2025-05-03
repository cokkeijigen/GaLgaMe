using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PlayerInventoryDataManager : SerializedMonoBehaviour
{
	public static List<HaveItemData> haveItemList = new List<HaveItemData>();

	public static List<HaveItemData> haveItemMaterialList = new List<HaveItemData>();

	public static List<HaveItemData> haveItemCanMakeMaterialList = new List<HaveItemData>();

	public static List<HaveCampItemData> haveItemCampItemList = new List<HaveCampItemData>();

	public static List<HaveItemData> haveItemMagicMaterialList = new List<HaveItemData>();

	public static List<HaveItemData> haveCashableItemList = new List<HaveItemData>();

	public static List<HaveWeaponData> haveWeaponList = new List<HaveWeaponData>();

	public static List<HaveArmorData> haveArmorList = new List<HaveArmorData>();

	public static List<HaveAccessoryData> haveAccessoryList = new List<HaveAccessoryData>();

	public static List<HaveEventItemData> haveEventItemList = new List<HaveEventItemData>();

	public static List<HavePartyWeaponData> havePartyWeaponList = new List<HavePartyWeaponData>();

	public static List<HavePartyArmorData> havePartyArmorList = new List<HavePartyArmorData>();

	public static List<LearnedSkillData> playerLearnedSkillList = new List<LearnedSkillData>();
}
