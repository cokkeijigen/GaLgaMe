using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/EnemyData")]
public class ItemData : SerializedScriptableObject
{
	public enum Category
	{
		potion,
		allPotion,
		mpPotion,
		allMpPotion,
		elixir,
		medicine,
		revive,
		rareDropRateUp
	}

	public enum Target
	{
		solo,
		all
	}

	public string itemName;

	public Sprite itemSprite;

	public Category category;

	public Target target;

	public int itemID;

	public int sortID;

	public int price;

	public int itemPower;

	public string effectType;

	public Vector2 spawnTransformV2;

	public float animationTime;

	public string shopSaleFlag;
}
