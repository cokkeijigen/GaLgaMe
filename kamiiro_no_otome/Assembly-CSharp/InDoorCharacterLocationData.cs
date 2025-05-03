using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/InDoor Character Location Data")]
public class InDoorCharacterLocationData : SerializedScriptableObject
{
	public string accessPointName;

	public string placeName;

	public int sortID;

	public string sectionFlagName;

	public List<InDoorLocationData> talkCharacterList0 = new List<InDoorLocationData>();

	public List<InDoorLocationData> talkCharacterList1 = new List<InDoorLocationData>();

	public List<InDoorLocationData> talkCharacterList2 = new List<InDoorLocationData>();

	public List<InDoorLocationData> talkCharacterList3 = new List<InDoorLocationData>();
}
