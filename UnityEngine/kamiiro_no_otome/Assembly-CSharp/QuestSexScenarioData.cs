using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/QuestSexScenarioData")]
public class QuestSexScenarioData : SerializedScriptableObject
{
	public int questItemID;

	public List<string> needSexScenatioNameList = new List<string>();
}
