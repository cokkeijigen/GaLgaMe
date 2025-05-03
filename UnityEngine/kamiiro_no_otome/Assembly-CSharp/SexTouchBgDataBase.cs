using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexTouchBgDataBase")]
public class SexTouchBgDataBase : SerializedScriptableObject
{
	public List<SexTouchBgSpriteData> sexTouchBgSpriteDataList;
}
