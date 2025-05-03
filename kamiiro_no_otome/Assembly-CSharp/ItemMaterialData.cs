using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/EnemyData")]
public class ItemMaterialData : SerializedScriptableObject
{
	public enum Category
	{
		monsterMaterial = 0,
		natureMaterial = 1,
		mysticMaterial = 2,
		stoneMaterial = 3,
		metalMaterial = 4,
		fuelMaterial = 5,
		addOnMaterialParts = 6,
		wonderMaterialParts = 7,
		crystal = 8,
		girl = 9,
		magicMaterialPowder = 11,
		etcMaterial = 10
	}

	public string itemName;

	public Sprite itemSprite;

	public int itemID;

	public int sortID;

	public Category category;

	public int price;

	public string shopSaleFlag;
}
