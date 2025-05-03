using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SceneGarellyData")]
public class SceneGarellyData : SerializedScriptableObject
{
	public List<string> garellyDataList = new List<string>();
}
