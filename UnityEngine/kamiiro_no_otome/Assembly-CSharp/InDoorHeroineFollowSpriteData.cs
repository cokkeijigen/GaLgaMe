using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/InDoor Heroine Sprite Data")]
public class InDoorHeroineFollowSpriteData : SerializedScriptableObject
{
	public string characterName;

	public int sortID;

	public List<Sprite> followRequestSpriteList;

	public List<Sprite> followRemoveSpriteList;

	public List<Sprite> followRemoveAtNightSpriteList;

	public List<Sprite> followOnlyMorningSpriteList;

	public Dictionary<string, Sprite> followingSpriteDictionary;
}
