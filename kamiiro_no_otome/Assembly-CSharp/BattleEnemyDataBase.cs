using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/EnemyDataList")]
public class BattleEnemyDataBase : ScriptableObject
{
	public List<BattleEnemyData> enemyDataList;
}
