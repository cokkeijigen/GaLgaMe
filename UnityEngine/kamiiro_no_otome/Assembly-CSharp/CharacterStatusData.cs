using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/StatusData")]
public class CharacterStatusData : SerializedScriptableObject
{
	public string characterName;

	public int characteID;

	public List<int> characterHP;

	public List<int> characterMP;

	public List<int> characterAttack;

	public List<int> characterMagicAttack;

	public List<int> characterDefense;

	public List<int> characterMagicDefense;

	public List<int> characterAccuracy;

	public List<int> characterCritical;

	public List<int> characterEvasion;

	public List<int> characterAgility;

	public List<int> characterResist;

	public List<int> characterMpRecoveryRate;

	public string normalEffectType;

	public string dungeonEffectType;

	public float effectDespawnTime;

	public Vector2 normalEffectV2;

	public string normalAttackElement;

	public List<string> weakElementsList;

	public List<string> resistElementsList;

	public List<int> characterSexHP;

	public List<int> characterSexExtasyLimitNum;

	public List<int> characterSexAttack;

	public List<int> characterSexHealPower;

	public List<int> characterSexSensetivity;

	public List<int> characterSexCritical;

	public List<int> characterSexLvCapFlag;

	public string characterSexTouchUnLockFlag;

	public string characterDungeonSexUnLockFlag;

	public string characterSexLvStageHighUnLockFlag;

	public string characterDungeonFollowUnLockFlag;

	public string characterMenstruationFlag;

	public string characterMenstruationViewFlag;

	public List<string> characterMenstrualDayList;

	public string characterFertilizationFlag;

	public string characterSexyWareFlag;

	public string characterBerserkPistonFlag;

	public string characterPowerUpFlag;
}
