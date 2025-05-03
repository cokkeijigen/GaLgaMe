using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Quest Item DataBase")]
public class QuestItemDataBase : SerializedScriptableObject
{
	public List<ItemQuestItemData> questItemDataList;
}
