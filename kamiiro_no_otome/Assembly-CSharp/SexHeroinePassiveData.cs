using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexHeroinePassiveData")]
public class SexHeroinePassiveData : SerializedScriptableObject
{
	public enum PassiveType
	{
		statusUp,
		statusDown
	}

	public enum StatusType
	{
		attack,
		defense
	}

	public enum BodyCategory
	{
		mouth = 0,
		tits = 3,
		nipple = 4,
		womb = 5,
		clitoris = 6,
		vagina = 7,
		anal = 8,
		hand = 9
	}

	public string skillName;

	public Sprite skillSprite;

	public string characterName;

	public int useCharacterID;

	public int skillID;

	public int skillUnlockLv;

	public int skillPower;

	public int skillRecharge;

	public PassiveType passiveType;

	public StatusType statusType;

	public BodyCategory bodyCategory;
}
