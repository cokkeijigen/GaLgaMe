using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexTouchDataBase")]
public class SexTouchDataBase : SerializedScriptableObject
{
	public List<SexTouchData> sexTouchDataList;
}
