using System.Collections.Generic;
using UnityEngine;

public class ScenarioFlagData
{
	public string scenarioName;

	public int sortId;

	public int eventOrderId;

	public Sprite thumbnailSprite;

	public string locationType;

	public string scenarioType;

	public string scenarioLocationName;

	public int dungeonFloorNum;

	public string scenarioTalkCharacter;

	public List<string> weekOfConditions;

	public List<int> timeOfConditions;

	public bool isSameEventCheck;

	public List<int> cannotExecuteWhileFollowingIdList;

	public int needFollowHeroineNum;

	public bool otherHeroineExceptionIsExists;

	public List<int> needQuestIdList;

	public List<int> needEventItemIdList;

	public int needItemShopPoint;

	public List<string> needScenarioFlagNameList;

	public bool isGarellyScene;

	public bool isSexEvent;

	public bool isSaveStartingDay;

	public bool isInterruptedSave;

	public string targetStartingDayScenarioName;

	public string disablePointTerm;

	public int getKizunaPoint;

	public List<int> sexCharacterIdList;

	public List<int> sexUniqueCountList;

	public List<int> sexInsertCountList;

	public List<int> sexPistonCountList;

	public List<int> sexMouthCountList;

	public List<int> sexOutShotCountList;

	public List<int> sexInShotCountList;

	public List<int> sexHeroineEcstasyCountList;
}
