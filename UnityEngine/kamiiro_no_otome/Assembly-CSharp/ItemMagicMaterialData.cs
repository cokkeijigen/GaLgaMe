using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/EnemyData")]
public class ItemMagicMaterialData : SerializedScriptableObject
{
	public enum Category
	{
		magicMaterial,
		addOnMaterial,
		wonderMaterial,
		magicMaterialPowder,
		addOnMaterialParts
	}

	public enum FactorType
	{
		attackUp = 0,
		magicAttackUp = 10,
		accuracyUp = 20,
		criticalUp = 30,
		hpUp = 500,
		mpUp = 600,
		skillPower = 700,
		defenseUp = 200,
		magicDefenseUp = 210,
		evasionUp = 220,
		criticalResistUp = 230,
		parry = 240,
		vampire = 250,
		abnormalResistUp = 260,
		mpSaving = 270
	}

	public enum AddOnType
	{
		type,
		power
	}

	public enum EquipType
	{
		weapon,
		armor,
		all
	}

	public string itemName;

	public Sprite itemSprite;

	public int itemID;

	public int sortID;

	public Category category;

	public FactorType factorType;

	public AddOnType addOnType;

	public int addOnPower;

	public EquipType equipType;

	public int price;

	public string shopSaleFlag;
}
