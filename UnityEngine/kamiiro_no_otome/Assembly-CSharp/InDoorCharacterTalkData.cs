using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/InDoor Character Talk Data")]
public class InDoorCharacterTalkData : SerializedScriptableObject
{
	public string accessPointName;

	public string placeName;

	public int sortID;

	public string characterName;

	public bool isAlwaysCommandTalk;

	public bool isOccurEvent;

	public List<string> talkSectionFlagNameList = new List<string>();

	public List<string> talkSectionTermList = new List<string>();

	public List<Sprite> talkCharacterSpriteList = new List<Sprite>();

	public List<int> talkSectionWordCountList = new List<int>();
}
