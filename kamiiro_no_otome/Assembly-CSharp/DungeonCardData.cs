using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Dungeon Card Data")]
public class DungeonCardData : SerializedScriptableObject
{
	public enum Type
	{
		none,
		battle,
		collect,
		heroine
	}

	public enum SubType
	{
		battle,
		hardBattle,
		move,
		vigilant,
		collect,
		corpse,
		treasure,
		heroineImage,
		heroineTalk,
		erection,
		camp,
		buffAttack,
		buffDefense,
		deBuffAttack,
		deBuffDefense,
		deBuffAgility,
		clearAgility,
		buffRetreat,
		deBuffRetreat,
		bigTreasure,
		healFountain,
		medicFountain,
		detour
	}

	public string cardName;

	public string cardLocalizeTerm;

	public int sortID;

	public Sprite cardSprite;

	public Type multiType;

	public SubType cardType;

	public int cardPower;

	public int powerAccumulateLimit;

	public float drawWait;
}
