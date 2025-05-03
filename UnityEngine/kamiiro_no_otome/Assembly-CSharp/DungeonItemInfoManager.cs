using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;

public class DungeonItemInfoManager : SerializedMonoBehaviour
{
	public ArborFSM arborFSM;

	public GameObject dungeonInfoWindow;

	public Localize dungeonInfoNameTextLoc;

	public RectTransform dungeonInfoScrollFrame;

	public RectTransform dungeonExInfoFrame;

	public GameObject dungeonExInfoHighlightTextGo;

	public GameObject dungeonInfoContentGo;

	public Transform dungeonFloorDataPrefabGo;

	public Transform dungeonFloorTextPrefabGo;

	public Transform totalMapPoolParent;

	public int dungeonMaxFloorNum;

	public int dungeonVisibleBorderFloorCount;

	public List<int> dungeonFloorListByTenLevels;

	public List<int> dungeonFloorListByLastBorderLevels;

	public List<int> dungeonFloorListByMinLevels;

	public List<int> dungeonFloorListByMaxLevels;

	public List<int> collectGetItemIdList = new List<int>();

	public List<int> battleGetItemIdList = new List<int>();

	public SortedDictionary<int, bool> mergeGetItemIdDictionary = new SortedDictionary<int, bool>();

	public List<int> collectAllGetItemIdList = new List<int>();

	public List<int> corpseAllGetItemIdList = new List<int>();

	public List<Transform> spawnedFloorPrefabGoList = new List<Transform>();

	public List<Transform> spawnedTextPrefabGoList = new List<Transform>();

	public void OpenDungeonItemInfoWindow()
	{
		string selectAccessPointName = PlayerNonSaveDataManager.selectAccessPointName;
		dungeonInfoNameTextLoc.Term = "area" + selectAccessPointName;
		arborFSM.SendTrigger("OpenDungeonItemInfoWindow");
	}

	public void DespawnScrollPrefabGo()
	{
		if (spawnedTextPrefabGoList.Any())
		{
			foreach (Transform spawnedTextPrefabGo in spawnedTextPrefabGoList)
			{
				if (spawnedTextPrefabGo != null && PoolManager.Pools["totalMapPool"].IsSpawned(spawnedTextPrefabGo))
				{
					PoolManager.Pools["totalMapPool"].Despawn(spawnedTextPrefabGo, 0f, totalMapPoolParent);
				}
			}
		}
		if (spawnedFloorPrefabGoList.Any())
		{
			foreach (Transform spawnedFloorPrefabGo in spawnedFloorPrefabGoList)
			{
				if (spawnedFloorPrefabGo != null && PoolManager.Pools["totalMapPool"].IsSpawned(spawnedFloorPrefabGo))
				{
					PoolManager.Pools["totalMapPool"].Despawn(spawnedFloorPrefabGo, 0f, totalMapPoolParent);
				}
			}
		}
		spawnedTextPrefabGoList.Clear();
		spawnedFloorPrefabGoList.Clear();
	}
}
