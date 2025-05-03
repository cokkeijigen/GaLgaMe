using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/EnemyData")]
public class ItemCashableItemData : SerializedScriptableObject
{
	public enum Category
	{
		cashableItem
	}

	public string itemName;

	public Sprite itemSprite;

	public int itemID;

	public int sortID;

	public Category category;

	public int price;
}
