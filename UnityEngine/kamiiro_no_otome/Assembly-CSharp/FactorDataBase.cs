using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Factor Data Base")]
public class FactorDataBase : SerializedScriptableObject
{
	public List<FactorData> factorDataList;
}
