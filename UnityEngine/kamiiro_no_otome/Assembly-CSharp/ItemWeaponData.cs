using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/WeaponData")]
public class ItemWeaponData : SerializedScriptableObject
{
	public enum Category
	{
		greatSword,
		greatSwordR,
		sword,
		dualSword,
		shoes
	}

	public enum Element
	{
		none,
		fire,
		water,
		ice,
		wind,
		lightning
	}

	public string itemName;

	public Sprite itemSprite;

	public Sprite itemInheritSprite;

	public int itemID;

	public int sortID;

	public Category category;

	public string recipeFlagName;

	public int needWorkShopLevel;

	public int needQuestId = 9999;

	public int powderCost;

	public int craftExp;

	public Element element;

	public int price;

	public int rarity;

	public int attackPower;

	public int magicAttackPower;

	public int accuracy = 100;

	public int critical = 3;

	public int weaponMp;

	public int factorSlot;

	public int factorHaveLimit;

	public List<NeedMaterialData> needMaterialList = new List<NeedMaterialData>();

	public List<NeedMaterialData> needMaterialNewerList = new List<NeedMaterialData>();

	public List<NeedMaterialData> needMaterialEditList = new List<NeedMaterialData>();
}
