using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Quest DungeonClear DataBase")]
public class QuestDungeonClearDataBase : SerializedScriptableObject
{
	public List<QuestDungeonClearData> questDungeonClearDataList;
}
