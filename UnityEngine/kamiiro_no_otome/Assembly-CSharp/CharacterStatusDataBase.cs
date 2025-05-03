using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/StatusDataList")]
public class CharacterStatusDataBase : ScriptableObject
{
	public List<CharacterStatusData> characterStatusDataList;
}
