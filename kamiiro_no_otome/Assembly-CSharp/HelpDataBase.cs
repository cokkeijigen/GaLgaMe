using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Help DataBase")]
public class HelpDataBase : SerializedScriptableObject
{
	public List<HelpData> helpCarriageList;

	public List<HelpData> helpCommandBattleList;

	public List<HelpData> helpDungeonList;

	public List<HelpData> helpSurveyList;

	public List<HelpData> helpSexBattleList;

	public List<HelpData> helpMapList;

	public List<HelpData> helpStatusList;
}
