using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Scene Garelly Data Base")]
public class SceneGarellyDataBase : SerializedScriptableObject
{
	public List<SceneGarellyData> sceneGarellyDataList;

	public SceneFlagNameData sceneFlagNameData;
}
