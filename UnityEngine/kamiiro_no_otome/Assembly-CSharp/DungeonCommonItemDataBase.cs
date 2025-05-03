using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Dungeon Common Item Data Base")]
public class DungeonCommonItemDataBase : SerializedScriptableObject
{
	public List<DungeonCommonItemData> dungeonCommonItemDataList;
}
