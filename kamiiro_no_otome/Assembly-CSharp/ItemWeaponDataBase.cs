using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/WeaponDataList")]
public class ItemWeaponDataBase : ScriptableObject
{
	public List<ItemWeaponData> itemWeaponDataList;
}
