using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/InDoor Location DataBase")]
public class InDoorLocationDataBase : SerializedScriptableObject
{
	public List<InDoorCharacterLocationData> inDoorCharacterLocationDataList;
}
