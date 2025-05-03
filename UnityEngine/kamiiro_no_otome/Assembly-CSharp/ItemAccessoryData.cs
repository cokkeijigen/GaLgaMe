using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/CanMakeMatrial")]
public class ItemAccessoryData : SerializedScriptableObject
{
	public enum Category
	{
		jewel,
		earRing
	}

	public enum PowerType
	{
		poisonNoEffect,
		paralyzeNoEffect,
		abnormalStateNoEffect,
		abnormalResistUp,
		debuffNoEffect,
		agilityUp,
		weakAttackUp,
		vampire,
		skillCritical,
		skillPowerUp,
		getExpUp,
		getMoneyUp,
		itemDiscoveryUp,
		libidoChargeUp,
		libidoChargeDown,
		libidoNoChange
	}

	public string itemName;

	public Sprite itemSprite;

	public Category category;

	public int itemID;

	public int sortID;

	public int price;

	public int equipCharacterType;

	public PowerType powerType;

	public int itemPower;

	public string shopSaleFlag;
}
