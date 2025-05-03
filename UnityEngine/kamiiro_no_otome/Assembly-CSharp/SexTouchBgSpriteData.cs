using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexTouchBgSpriteData")]
public class SexTouchBgSpriteData : SerializedScriptableObject
{
	public string inspectorName;

	public string placeName;

	public List<Sprite> sexTouchBgFaceList;

	public List<Sprite> sexTouchBgUpperList;

	public List<Sprite> sexTouchBgLowerList;

	public List<Sprite> sexBattleBgList;
}
