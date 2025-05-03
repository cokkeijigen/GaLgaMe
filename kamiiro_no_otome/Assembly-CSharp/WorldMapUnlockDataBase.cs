using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/WorldMap Unlock DataBase")]
public class WorldMapUnlockDataBase : SerializedScriptableObject
{
	public List<WorldMapUnlockData> worldMapUnlockDataList;
}
