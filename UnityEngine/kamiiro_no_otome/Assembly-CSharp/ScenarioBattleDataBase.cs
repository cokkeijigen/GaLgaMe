using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/BattleDataList")]
public class ScenarioBattleDataBase : SerializedScriptableObject
{
	public List<ScenarioBattleData> scenarioBattleDataList;
}
