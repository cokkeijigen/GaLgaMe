using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/InDoor Talk DataBase")]
public class InDoorTalkDataBase : SerializedScriptableObject
{
	public List<InDoorCharacterTalkData> inDoorCharacterTalkDataList;
}
