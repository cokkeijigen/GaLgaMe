using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Dungeon Libido Data Base")]
public class DungeonLibidoDataBase : SerializedScriptableObject
{
	public List<DungeonLibidoData> dungeonLibidoDataList;
}
