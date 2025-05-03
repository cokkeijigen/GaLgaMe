using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/DungeonGetItemInfoData")]
public class DungeonGetItemInfoData : SerializedScriptableObject
{
	public string dungeonName;

	public List<int[]> normalEnemyDropItemInfoTable;

	public List<int[]> hardEnemyDropItemInfoTable;

	public List<int[]> collectCommonMaterialItemInfoTable;

	public List<int[]> collectCommonItemInfoTable;

	public List<int[]> corpseCommonItemInfoTable;

	public List<int[]> treasureCommonItemInfoTable;

	public List<int[]> getTreasureItemInfoTable;

	public List<int[]> getCollectItemInfoTable;

	public List<int[]> getCashableItemInfoTable;

	public List<int> lockedBorderByTenFloorList;

	public List<int> unLockBorderByTenFloorList;
}
