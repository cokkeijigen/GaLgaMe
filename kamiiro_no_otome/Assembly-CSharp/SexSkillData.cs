using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexSkillData")]
public class SexSkillData : SerializedScriptableObject
{
	public enum SkillCategory
	{
		active,
		passiveProbability
	}

	public enum ActionType
	{
		none,
		piston,
		kiss,
		caress,
		heal
	}

	public enum CounterType
	{
		none,
		piston,
		pistonAndCaress,
		healAndCaress,
		all,
		absorb
	}

	public enum BodyCategory
	{
		none = 10,
		mouth = 0,
		tits = 3,
		nipple = 4,
		womb = 5,
		clitoris = 6,
		vagina = 7,
		anal = 8,
		both = 9,
		hand = 11
	}

	public enum SkillType
	{
		none = 9,
		sexAttack = 0,
		heal = 1,
		buff = 2,
		deBuff = 3,
		caress = 4,
		berserk = 5,
		fertilize = 6
	}

	public enum BuffType
	{
		none = 9,
		attack = 0,
		critical = 1,
		healPower = 2,
		regeneration = 3
	}

	public enum SubType
	{
		none = 9,
		enhancement = 0,
		crazy = 1,
		pistonOnly = 2,
		titsOnly = 3,
		desire = 4,
		trance = 5,
		absorb = 6
	}

	public enum TranceAffect
	{
		none,
		tranceUp,
		tranceDown
	}

	public string skillName;

	public Sprite skillSprite;

	public string characterName;

	public int useCharacterID;

	public int skillID;

	public int skillUnlockLv;

	public int skillPower;

	public int healPower;

	public int skillRecharge;

	public int skillContinuityTurn;

	public int skillNeedTrance;

	public int skillNeedRemainPlayerLimit;

	public SkillCategory skillCategory;

	public ActionType actionType;

	public CounterType counterType;

	public string narrativePartString;

	public BodyCategory bodyCategory;

	public SkillType skillType;

	public BuffType buffType;

	public SubType subType;

	public int subPower;

	public int subProbability;

	public TranceAffect heroineAffect;

	public int heroineAffectPower;

	public TranceAffect playerAffect;

	public int playerAffectPower;

	public string effectType;

	public Vector2 spawnTransformV2;

	public float animationTime;

	public int[] cutInCharacterIdArray;
}
