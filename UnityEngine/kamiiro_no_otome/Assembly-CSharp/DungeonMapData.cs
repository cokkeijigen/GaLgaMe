using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Editor/DungeonMapData")]
public class DungeonMapData : SerializedScriptableObject
{
	public enum Type
	{
		free,
		deepDungeon,
		scenario
	}

	public string dungeonName;

	public Type dungeonType;

	public string freeDungeonFlag;

	public string deepDungeonFlag;

	public int deepDungeonQuestFlag;

	public string battleCardProbabilityNotChangeFlag;

	public int beforeFreeNeedHeroineID;

	public int beforeFreeMaxFloor;

	public int maxFloor;

	public int bossCount;

	public int deepDungeonNoticeCount;

	public bool isBeforeFlagAbsenceBoss;

	public string bossAppearFlag;

	public bool isTowerDungeon;

	public bool isExtraQuestBossExist;

	public int extraQuestID;

	public int extraQuestBossFloor;

	public List<int> dungeonBorderFloor;

	public int borderFloorCount;

	public List<int> maxEnemyCount;

	public List<int[]> battleEnemyID;

	public List<int[]> hardBattleEnemyID;

	public List<int> bossEnemyID;

	public int extraQuestBossID;

	public List<int> battleMimicID;

	public List<int> collectCommonMaterialItemTable;

	public List<int> collectCommonItemTable;

	public List<int> corpseCommonItemTable;

	public List<int> treasureCommonItemTable;

	public List<int> rareAddOnCommonItemTable;

	public List<int> rareWonderCommonItemTable;

	public List<int> rarePowerCommonItemTable;

	public List<int[]> getRareItemID;

	public List<int[]> getTreasureItemID;

	public List<int[]> getCollectItemID;

	public List<int[]> getCashableItemID;

	public List<int[]> getBossItemID;

	public int getDeepLastBossItemID;

	public List<int> getExtraQuestBossItemID;

	public List<int> maxDropMoney;

	public List<string> drawCardNameList;

	public int drawCardTypeCount;

	public List<int> battleCardProbabilityChangeFloorList;

	public List<int> battleCardProbabilityChangePowerList;

	public float dungeonBgScale;

	public float dungeonBgPositonX;

	public float dungeonBgPositonY;

	public List<Sprite> dungeonBgList;

	public List<Sprite> dungeonNightBgList;

	public List<Sprite> dungeonAnimationBgList;

	public List<Sprite> dungeonAnimationNightBgList;

	public List<int> dungeonEventFloorList;

	public List<int> dungeonBossFloorList;

	public List<int> dungeonBossNoRetryFloorList;

	public List<int> needSkipTpList;

	public string dungeonBgmName;

	public string dungeonBossBgmName;

	public string dungeonDeepBossBgmName;
}
