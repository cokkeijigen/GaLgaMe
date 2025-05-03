using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Heroine Location Data")]
public class HeroineLocationData : SerializedScriptableObject
{
	public struct WeekLocationData
	{
		public List<string> weekDayList;

		public List<int> timeZoneList;

		public bool isVisible;
	}

	public string heroineName;

	public int heroineID;

	public string needFlagName;

	public int sortID;

	public string mapType;

	public string worldPointName;

	public string localPlaceName;

	public List<WeekLocationData> weekLocationDataList = new List<WeekLocationData>();
}
