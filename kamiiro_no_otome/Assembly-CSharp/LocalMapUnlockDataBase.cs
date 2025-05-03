using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/LocalMap Unlock DataBase")]
public class LocalMapUnlockDataBase : SerializedScriptableObject
{
	public List<LocalMapUnlockData> localMapUnlockDataList;
}
