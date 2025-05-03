using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/LocalMap Place Data")]
public class LocalMapPlaceData : SerializedScriptableObject
{
	public string placeName;

	public int sortID;

	public string currentPlaceName;

	public Dictionary<string, Sprite> localMapBgDictionary;

	public Dictionary<string, string> placeVisibleNeedFlagDictionary;
}
