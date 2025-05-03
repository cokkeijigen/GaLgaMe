using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexTouchClickDataBase")]
public class SexTouchClickDataBase : SerializedScriptableObject
{
	public List<SexTouchClickData> sexTouchClickDataList;
}
