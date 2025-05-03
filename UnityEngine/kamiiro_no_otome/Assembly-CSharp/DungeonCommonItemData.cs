using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/DungeonCommonItemData")]
public class DungeonCommonItemData : SerializedScriptableObject
{
	public enum Category
	{
		collect,
		corpse,
		treasure,
		rareItem
	}

	public string commonDataName;

	public Category itemCategory;

	public int sortId;

	public List<int> itemDataIdList = new List<int>();
}
