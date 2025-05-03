using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/WorldMap Unlock Data")]
public class WorldMapUnlockData : SerializedScriptableObject
{
	public string pointName;

	public int sortID;

	public string currentPointName;

	public string needFlagName;

	public bool isDungeon;

	public string bgmCategoryName;
}
