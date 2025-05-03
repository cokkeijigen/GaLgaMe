using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/EnemyData")]
public class ItemEventItemData : SerializedScriptableObject
{
	public enum Category
	{
		eventItem
	}

	public string itemName;

	public Sprite itemSprite;

	public int itemID;

	public int sortID;

	public Category category;

	public string recipeFlagName;

	public string rewardRecipeName;

	public int needWorkShopLevel;

	public int getItemShopPoint;

	public int craftExp;

	public int price;

	public List<NeedMaterialData> needMaterialList = new List<NeedMaterialData>();

	public string shopSaleFlag;
}
