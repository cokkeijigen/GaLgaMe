using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/InDoor Character Cg Data")]
public class InDoorCharacterCgData : SerializedScriptableObject
{
	public string characterName;

	public string characterNameTalkKey;

	public int sortID;

	public Vector2 talkPositionV2;

	public Vector2 heroinePositionV2;

	public Vector2 heroineInnPositionV2;

	public Vector2 talkBalloonV2;

	public Vector2 heroineBalloonV2;

	public Vector2 heroineInnBalloonV2;

	public Vector2 talkAlertV2;

	public Vector2 heroineAlertV2;

	public float talkImageDisplayMagnification;

	public int visibleProbability;

	public List<string> commandButtonKeyList = new List<string>();

	public Sprite characterSprite;

	public Sprite characterExceptionSprite;

	public Sprite characterFollowSprite;

	public Sprite characterFollowInnSprite;
}
