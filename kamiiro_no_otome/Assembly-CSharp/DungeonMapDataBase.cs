using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Dungeon Map Data Base")]
public class DungeonMapDataBase : SerializedScriptableObject
{
	public List<DungeonMapData> dungeonMapDataList;
}
