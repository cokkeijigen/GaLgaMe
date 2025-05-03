using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexTouchClickData")]
public class SexTouchClickData : SerializedScriptableObject
{
	public enum BodyCategory
	{
		head,
		upperBody,
		lowerBody,
		fellatio
	}

	public string characterName;

	public int characterID;

	public BodyCategory bodyCategory;

	public string areaName;

	public int areaIndex;

	public Vector2 areaPointVector2;

	public Vector2 areaPointVector2WithInfoWindow;

	public List<int> areaSensitivityList;

	public int areaLibidoUpNum;
}
