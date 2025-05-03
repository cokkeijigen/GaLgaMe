using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SkillEffectData")]
public class SkillEffectData : SerializedScriptableObject
{
	public enum Category
	{
		heal,
		medic,
		attack,
		magicAttack,
		defense,
		magicDefense,
		powerUp,
		powerDown,
		start,
		sexTouch,
		sexBattle
	}

	public string effectName;

	public int sortID;

	public Category effectCategory;

	public GameObject effectPrefabGo;

	public Vector2 effectLocalPosition;

	public float localScale;
}
