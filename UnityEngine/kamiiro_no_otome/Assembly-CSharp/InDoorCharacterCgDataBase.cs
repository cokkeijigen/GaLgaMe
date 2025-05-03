using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/InDoor Character Cg DataBase")]
public class InDoorCharacterCgDataBase : SerializedScriptableObject
{
	public List<InDoorCharacterCgData> inDoorCharacterCgDataList;
}
