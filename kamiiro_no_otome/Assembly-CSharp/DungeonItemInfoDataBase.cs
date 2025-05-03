using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Dungeon Item Info Data Base")]
public class DungeonItemInfoDataBase : SerializedScriptableObject
{
	public List<DungeonGetItemInfoData> dungeonGetItemInfoDataList;
}
