using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/BattleData")]
public class ScenarioBattleData : SerializedScriptableObject
{
	public string scenarioName;

	public int id;

	public string battleType;

	public bool isVictoryBattle;

	public bool isDefeatBattle;

	public bool isEventBattle;

	public bool isNotRewindWhenDefeat;

	public int enemyHpRate;

	public Sprite[] battleBgSprite = new Sprite[3];

	public float battleBgRectPosX;

	public float battleBgRectPosY;

	public float battleBgRectScale;

	public float enemyCgSizeWidth;

	public float enemyCgSizeHeight;

	public string battleBgmName;

	public List<int> battleCharacterID;

	public int supportBattleCharacterID;

	public bool isSupportCharacterTakeDamage;

	public List<int> battleEnemyID;

	public List<int> getItemID;

	public List<bool> getMaterialIcon;
}
