using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/LocalMap Unlock Data")]
public class LocalMapUnlockData : SerializedScriptableObject
{
	public string placeName;

	public int sortID;

	public string worldCityName;

	public string currentPlaceName;

	public List<string> needFlagNameList;

	public int needFlagQuestId;

	public Sprite[] inDoorBgSpriteArray = new Sprite[4];

	public Sprite[] inDoorSurveyBgSpriteArray = new Sprite[4];

	public string inDoorBgmName;
}
