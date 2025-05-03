using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/CanMakeMatrial")]
public class ItemPartyWeaponData : SerializedScriptableObject
{
	public enum Category
	{
		greatSwordR,
		sword,
		dualSword,
		shoes
	}

	public string itemName;

	public Sprite itemSprite;

	public Sprite itemInheritSprite;

	public int itemEquipCharacterID;

	public int itemID;

	public int sortID;

	public Category category;

	public string recipeFlagName;

	public int needWorkShopLevel;

	public int craftExp;

	public int rarity;

	public int price;

	public int editPrice;

	public int attackPower;

	public int magicAttackPower;

	public int accuracy = 100;

	public int critical = 3;

	public int comboProbability;

	public int factorSlot;

	public int factorHaveLimit;

	public List<NeedMaterialData> needMaterialList = new List<NeedMaterialData>();

	public List<NeedMaterialData> needMaterialEditList = new List<NeedMaterialData>();
}
