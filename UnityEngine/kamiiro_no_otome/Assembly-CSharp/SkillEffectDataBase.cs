using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SkillEffectDataBase")]
public class SkillEffectDataBase : SerializedScriptableObject
{
	public List<SkillEffectData> skillEffectDataList;
}
