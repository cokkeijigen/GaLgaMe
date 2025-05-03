using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/ShopRankDataBase")]
public class ShopRankDataBase : SerializedScriptableObject
{
	public List<ShopRankData> shopRankDataList;
}
