using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/CanMakeMatrial")]
public class ItemPartyArmorData : SerializedScriptableObject
{
	public string itemName;

	public Sprite itemSprite;

	public Sprite itemInheritSprite;

	public int itemEquipCharacterID;

	public int itemID;

	public int sortID;

	public string recipeFlagName;

	public int needWorkShopLevel;

	public int craftExp;

	public int rarity;

	public int price;

	public int editPrice;

	public int defensePower;

	public int magicDefensePower;

	public int evasion = 5;

	public int abnormalResist;

	public int recoveryMp;

	public int factorSlot;

	public int factorHaveLimit;

	public List<NeedMaterialData> needMaterialList = new List<NeedMaterialData>();

	public List<NeedMaterialData> needMaterialEditList = new List<NeedMaterialData>();
}
