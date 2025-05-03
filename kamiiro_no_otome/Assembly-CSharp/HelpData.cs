using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Help Data")]
public class HelpData : SerializedScriptableObject
{
	public enum HelpType
	{
		carriage,
		commandBattle,
		dungeon,
		survey,
		sexBattle,
		map,
		status
	}

	public string helpTermName;

	public int sortID;

	public HelpType helpType;

	public bool isContentSpriteActive;

	public Sprite helpContentSprite;
}
