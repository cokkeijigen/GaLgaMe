using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/EnemyData")]
public class BattleEnemyData : SerializedScriptableObject
{
	public enum Element
	{
		none,
		fire,
		water,
		ice,
		wind,
		lightning
	}

	public string enemyName;

	public Sprite enemyImageSprite;

	public Sprite enemyImageMiniSprite;

	public int enemyID;

	public int enemyLV;

	public int enemyExp;

	public int enemyHP;

	public int enemyGold;

	public string normalAttackElement;

	public List<string> weakElementList;

	public List<string> resistElementList;

	public int deBuffSkillResistBonus;

	public bool deathAttackResist;

	public Dictionary<int, float> useSkillDictionary;

	public int enemyChargeTurn;

	public int enemyAttack;

	public int enemyMagicAttack;

	public int enemyDefense;

	public int enemyMagicDefense;

	public int enemyAccuracy;

	public int enemyEvasion;

	public int enemyCritical;

	public int enemyAgility;

	public Dictionary<int, float> dropItemDictionary;

	public int dropMagicMaterial;

	public string normalEffectType;

	public float effectDespawnTime;

	public Vector2 normalEffectV2;
}
