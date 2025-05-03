using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Dungeon Card Data Base")]
public class DungeonMapCardDataBase : SerializedScriptableObject
{
	public List<DungeonCardData> dungeonCardDataList;
}
