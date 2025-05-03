using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/LocaldMap Place DataBase")]
public class LocalMapPlaceDataBase : SerializedScriptableObject
{
	public List<LocalMapPlaceData> localMapPlaceDataList;
}
