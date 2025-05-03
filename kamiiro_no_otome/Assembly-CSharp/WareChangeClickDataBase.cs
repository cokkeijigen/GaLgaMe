using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/WareChangeClickDataBase")]
public class WareChangeClickDataBase : SerializedScriptableObject
{
	public List<WareChangeClickData> wareChangeClickDataList;
}
