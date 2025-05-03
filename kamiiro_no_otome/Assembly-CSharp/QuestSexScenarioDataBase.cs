using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Quest SexScenario DataBase")]
public class QuestSexScenarioDataBase : SerializedScriptableObject
{
	public List<QuestSexScenarioData> questSexScenarioDataList;
}
