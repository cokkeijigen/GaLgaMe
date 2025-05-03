using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Quest DataBase")]
public class QuestDataBase : SerializedScriptableObject
{
	public List<QuestData> questDataList;
}
