using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/SkillDataList")]
public class BattleSkillDataBase : ScriptableObject
{
	public List<BattleSkillData> skillDataList;
}
