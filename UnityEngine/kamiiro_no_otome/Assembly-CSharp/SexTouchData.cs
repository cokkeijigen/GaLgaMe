using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SexTouchData")]
public class SexTouchData : SerializedScriptableObject
{
	public string characterName;

	public int characterID;

	public List<string> mouthBorderFlagList;

	public List<string> titsBorderFlagList;

	public List<string> nippleBorderFlagList;

	public List<string> wombBorderFlagList;

	public List<string> clitorisBorderFlagList;

	public List<string> vaginaBorderFlagList;

	public List<string> analBorderFlagList;

	public string enableSexFlag;

	public string enableCumShotFlag;

	public List<string> sexPositionBorderFlagList;

	public Sprite defaultBgSprite;
}
