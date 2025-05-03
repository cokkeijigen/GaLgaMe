using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/UseItemDataList")]
public class ItemDataBase : ScriptableObject
{
	public List<ItemData> itemDataList;
}
