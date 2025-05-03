using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Heroine Location DataBase")]
public class HeroineLocationDataBase : SerializedScriptableObject
{
	public List<HeroineLocationData> heroineLocationDataList;
}
