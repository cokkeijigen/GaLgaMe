using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckScriptableObjectData : MonoBehaviour
{
	public BattleEnemyDataBase battleEnemyDataBase;

	public ItemWeaponDataBase itemWeaponDataBase;

	public List<string> resultList;

	private void Start()
	{
		resultList.Clear();
		foreach (BattleEnemyData item in (from data in battleEnemyDataBase.enemyDataList
			group data by data.enemyID into data
			where data.Count() > 1
			select data).SelectMany((IGrouping<int, BattleEnemyData> g) => g).ToList())
		{
			resultList.Add(item.enemyName);
		}
	}
}
