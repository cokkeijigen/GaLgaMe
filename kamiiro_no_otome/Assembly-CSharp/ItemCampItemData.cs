using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/CampItem")]
public class ItemCampItemData : SerializedScriptableObject
{
	public enum Category
	{
		camp,
		lanthanum,
		charm,
		medicKit
	}

	public string itemName;

	public Sprite itemSprite;

	public int itemID;

	public int sortID;

	public Category category;

	public string recipeFlagName;

	public int needWorkShopLevel;

	public int craftExp;

	public int power;

	public int subPower;

	public List<NeedMaterialData> needMaterialList = new List<NeedMaterialData>();
}
