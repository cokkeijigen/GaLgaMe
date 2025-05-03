using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Need WorldMap Date Data")]
public class NeedWorldMapDateData : SerializedScriptableObject
{
	public List<string[]> needAccessDayList = new List<string[]>();

	public List<string[]> needAccessTimeZoneList = new List<string[]>();
}
