using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/DungeonLibidoData")]
public class DungeonLibidoData : SerializedScriptableObject
{
	public string characterName;

	public int characterID;

	public Sprite normalSprite;

	public Sprite battleSprite;

	public Sprite libidoHighSprite;

	public Sprite libidoMaxSprite;

	public Vector2 dungeonMoveV2;

	public Vector2 dungeonBattleV2;

	public Vector2 dungeonBattleSizeV2;

	public bool dungeonMoveIsLeadPosition;

	public bool dungeonBattleIsLeadPosition;

	public Vector2 dungeonBattleFollowEdenV2;

	public Vector2 dungeonBattleFollowEdenSizeV2;

	public float dungeonLibidoY;

	public Vector2 libidoHighEffectV2;

	public Vector2 libidoMaxEffectV2;
}
