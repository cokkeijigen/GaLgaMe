using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SkillData")]
public class BattleSkillData : SerializedScriptableObject
{
	public enum SkillType
	{
		attack,
		magicAttack,
		mixAttack,
		chargeAttack,
		buff,
		deBuff,
		heal,
		regeneration,
		medic,
		revive
	}

	public enum SkillTarget
	{
		solo,
		all,
		self
	}

	public enum Element
	{
		none,
		fire,
		water,
		ice,
		wind,
		lightning,
		super
	}

	public enum BuffType
	{
		none,
		attack,
		magicAttack,
		defense,
		magicDefense,
		allDefense,
		accuracy,
		evasion,
		critical,
		abnormalResist,
		hateUp,
		parry,
		endurance
	}

	public enum SubType
	{
		none,
		buff,
		debuff,
		poison,
		paralyze,
		libido,
		stagger,
		oblivion
	}

	public string skillName;

	public Sprite skillSprite;

	public int skillID;

	public int sortID;

	public int useMP;

	public int unlockLv;

	public int unlockFlagCharacter;

	public int useCharacterID;

	public int learnCost;

	public string learnScenarioName;

	public int skillPower;

	public int skillAccuracy;

	public int skillRecharge;

	public int skillContinuity;

	public SkillType skillType;

	public SkillTarget skillTarget;

	public string skillElement;

	public BuffType buffType;

	public SubType subType;

	public int subSkillPower;

	public int successRate;

	public string effectType;

	public Vector2 spawnTransformV2;

	public float animationTime;

	public int[] cutInCharacterIdArray;
}
