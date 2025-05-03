using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/WareChangeClickData")]
public class WareChangeClickData : SerializedScriptableObject
{
	public enum LevelStage
	{
		A,
		B
	}

	public enum WareStatus
	{
		nude,
		ware
	}

	public string characterName;

	public int characterID;

	public int sortID;

	public LevelStage levelStage;

	public WareStatus wareStatus;

	public List<string> wareChange_TermList = new List<string>();

	public List<Sprite> wareChange_SpiteList = new List<Sprite>();

	public Sprite wareChange_IdolSprite;

	public Vector2 characterPositionVector2;

	public float characterImageScale;

	public Vector2 BalloonPositionVector2;

	public List<Sprite> wareChange_Kingdom1BgList = new List<Sprite>();

	public List<Sprite> wareChange_City1BgList = new List<Sprite>();
}
