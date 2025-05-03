using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/CanMakeMatrial")]
public class ItemCanMakeMaterialData : SerializedScriptableObject
{
	public enum Category
	{
		ingotMaterial,
		etcMaterial
	}

	public string itemName;

	public Sprite itemSprite;

	public int itemID;

	public int sortID;

	public Category category;

	public string recipeFlagName;

	public int needWorkShopLevel;

	public int needFurnaceLevel;

	public int craftExp;

	public int price;

	public string shopSaleFlag;

	public List<NeedMaterialData> needMaterialList = new List<NeedMaterialData>();
}
