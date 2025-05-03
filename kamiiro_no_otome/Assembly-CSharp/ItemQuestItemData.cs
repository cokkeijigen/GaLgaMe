using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/QuestItemData")]
public class ItemQuestItemData : SerializedScriptableObject
{
	public enum Category
	{
		questItem
	}

	public string itemName;

	public Sprite itemSprite;

	public int itemID;

	public int sortID;

	public Category category;
}
