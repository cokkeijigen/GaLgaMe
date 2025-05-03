using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/QuestDungeonClearData")]
public class QuestDungeonClearData : SerializedScriptableObject
{
	public int questItemID;

	public List<string> needClearDungeonNameList = new List<string>();
}
