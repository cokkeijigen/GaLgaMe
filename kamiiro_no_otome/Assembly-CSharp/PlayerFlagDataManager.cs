using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PlayerFlagDataManager : SerializedMonoBehaviour
{
	public static Dictionary<string, bool> scenarioFlagDictionary = new Dictionary<string, bool>();

	public static Dictionary<string, bool> tutorialFlagDictionary = new Dictionary<string, bool>
	{
		{ "scenarioBattle1", false },
		{ "scenarioBattle2", false },
		{ "scenarioBattle3", false },
		{ "chargeAttack", false },
		{ "lucyVoiceSelect", false },
		{ "craft", false },
		{ "carriageStore", false },
		{ "dungeonMap", false },
		{ "dungeonBoss", false },
		{ "dungeonBattle", false },
		{ "heroineFollow", false },
		{ "survey", false },
		{ "sexBattle", false }
	};

	public static Dictionary<string, bool> sceneGarellyFlagDictionary = new Dictionary<string, bool>();

	public static List<bool> partyPowerUpFlagList = new List<bool> { false, false, false, false, false, true };

	public static List<bool> heroineAllTimeFollowFlagList = new List<bool> { true, true, false, false, false, false };

	public static List<bool> heroineFirstSexTouchFlagList = new List<bool> { false, false, false, false, false, false };

	public static Dictionary<string, bool> dungeonFlagDictionary = new Dictionary<string, bool>
	{
		{ "Dungeon_Debug", false },
		{ "Dungeon1", false },
		{ "Dungeon2", false },
		{ "Shrine1", false },
		{ "Forest1", false },
		{ "Forest2", false },
		{ "Forest3", false }
	};

	public static Dictionary<string, int> deepDungeonFlagDictionary = new Dictionary<string, int>
	{
		{ "Shrine1", 0 },
		{ "Forest1", 0 },
		{ "Forest2", 0 },
		{ "Forest3", 0 },
		{ "Dungeon1", 0 },
		{ "Dungeon2", 0 },
		{ "Dungeon3", 0 },
		{ "Dungeon4", 0 }
	};

	public static Dictionary<string, bool> dungeonDeepClearFlagDictionary = new Dictionary<string, bool>
	{
		{ "Dungeon1", false },
		{ "Dungeon2", false },
		{ "Dungeon3", false },
		{ "Dungeon4", false },
		{ "Shrine1", false },
		{ "Forest1", false },
		{ "Forest2", false },
		{ "Forest3", false }
	};

	public static Dictionary<string, bool> recipeFlagDictionary = new Dictionary<string, bool>();

	public static Dictionary<int, bool> enableNewCraftFlagDictionary = new Dictionary<int, bool>();

	public static List<QuestClearData> questClearFlagList = new List<QuestClearData>();

	public static Dictionary<string, bool> keyItemFlagDictionary = new Dictionary<string, bool> { { "campItem630", true } };

	public static Dictionary<string, int> eventStartingDayDictionary = new Dictionary<string, int>();

	public static Dictionary<string, bool> priceSettingNoticeFlagDictionary = new Dictionary<string, bool>
	{
		{ "shopRank_second2", false },
		{ "shopRank_second3", false },
		{ "shopRank_second4", false },
		{ "shopRank_second5", false },
		{ "shopRank_second6", false },
		{ "shopRank_second7", false },
		{ "shopRank_second8", false }
	};

	public static Dictionary<string, bool> extraNoticeFlagDictionary = new Dictionary<string, bool> { { "shopRank_first4", false } };

	public static bool CheckScenarioFlagIsClear(string key)
	{
		return scenarioFlagDictionary[key];
	}

	public static int CalcPartyPowerUpCount()
	{
		int num = -2;
		for (int i = 0; i < partyPowerUpFlagList.Count; i++)
		{
			if (partyPowerUpFlagList[i])
			{
				num++;
			}
		}
		return num;
	}

	public static bool GetHeroineFollowEnable(int id)
	{
		string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[id].characterDungeonFollowUnLockFlag;
		return scenarioFlagDictionary[characterDungeonFollowUnLockFlag];
	}
}
