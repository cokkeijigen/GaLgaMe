using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/CanMakeMatrial")]
public class ItemArmorData : SerializedScriptableObject
{
	public string itemName;

	public Sprite itemSprite;

	public Sprite itemInheritSprite;

	public int itemID;

	public int sortID;

	public string recipeFlagName;

	public int needWorkShopLevel;

	public int needQuestId = 9999;

	public int craftExp;

	public int price;

	public int rarity;

	public int defensePower;

	public int magicDefensePower;

	public int evasion = 5;

	public int abnormalResist;

	public int recoveryMp;

	public int factorSlot;

	public int factorHaveLimit;

	public List<NeedMaterialData> needMaterialList = new List<NeedMaterialData>();

	public List<NeedMaterialData> needMaterialNewerList = new List<NeedMaterialData>();

	public List<NeedMaterialData> needMaterialEditList = new List<NeedMaterialData>();
}
