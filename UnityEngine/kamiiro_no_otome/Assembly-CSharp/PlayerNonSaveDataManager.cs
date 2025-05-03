using System.Collections.Generic;
using UnityEngine;

public class PlayerNonSaveDataManager : MonoBehaviour
{
	public static string selectPlaceName;

	public static GameObject selectPlaceGO;

	public static string selectAccessPointName;

	public static GameObject selectAccessPointGO;

	public static int needMoveDayCount;

	public static int needMoveTimeCount;

	public static int needCampTimeCount;

	public static bool isClockChangeEnable;

	public static bool isWorldMapVisibleFlag;

	public static bool isWorldMapToUtage;

	public static bool isWorldMapToInDoor;

	public static bool isWorldMapPointDisable;

	public static bool isWorldMapPointDialogAlert;

	public static bool isUtageToLocalMap;

	public static bool totalMapUtageIsPlaying;

	public static bool isMapMenuRightClickDisable;

	public static bool isRefreshLocalMap;

	public static bool isRefreshWorldMap;

	public static bool isDungeonDeepClear;

	public static bool isDungeonGetItemHighlight;

	public static int inDoorHeroineNum;

	public static bool inDoorHeroineExist;

	public static string inDoorCommandName;

	public static int inDoorTalkPhaseNum;

	public static bool isInDoorExitLock;

	public static Dictionary<int, bool> inDoorAllTalkDictionary = new Dictionary<int, bool>();

	public static bool isUtageToWorldMapInDoor;

	public static bool isRetreatScenarioFlagReset;

	public static string[] retreatResetFlagNameArray = new string[2];

	public static bool isScenarioBattle;

	public static bool isMoveToDungeonBattle;

	public static bool isRetreatFromBattle;

	public static bool isEnemyHpZeroAttack;

	public static string skipScenarioBattleName;

	public static bool isBattleMenuRightClickDisable;

	public static bool isDungeonBattleFailedNotice;

	public static Dictionary<int, int> preGetDropMagicMaterial = new Dictionary<int, int>();

	public static bool isDungeonScnearioBattle;

	public static bool isDungeonBossBattle;

	public static bool isDungeonNoRetryBossBattle;

	public static bool isDungeonSexEvent;

	public static string dungeonEventScenarioName;

	public static int dungeonSetStartFloorNum;

	public static bool isDungeonSuddenFloorEvent;

	public static bool isDungeonGetRareItem;

	public static bool isDungeonGetRareItemWithRecipe;

	public static int dungeonGetRareItemId;

	public static List<DungeonSelectCardData> backUpMiniCardList = new List<DungeonSelectCardData>();

	public static Dictionary<int, int> backUpGetDropItemDictionary = new Dictionary<int, int>();

	public static int backUpGetDropMoney;

	public static int backUpDungeonCurrentFloorNum;

	public static int backUpCurrentBorderNum;

	public static bool backUpIsBossRoute;

	public static int backUpPlayerLibido;

	public static int backUpPlayerAllHp;

	public static int backUpDungeonBuffAttack;

	public static int backUpDungeonBuffDefense;

	public static int backUpDungeonDeBuffAgility;

	public static int backUpDungeonDeBuffAgiityRemainFloor;

	public static int backUpDungeonBuffRetreat;

	public static float masterAudioFadeTime;

	public static string currentBgmCategoryName;

	public static bool isHeaderMenuNotice;

	public static string selectStatusCanvasName;

	public static bool isEquipItemChange;

	public static bool isCraftWorkShop;

	public static string selectCraftCanvasName;

	public static int needCraftTimeCount;

	public static string selectExtensionCanvasName;

	public static int selectSexBattleHeroineId;

	public static bool isSexHeroineMenstrualDay;

	public static bool isSexHeroineEnableFertilization;

	public static bool isMenstrualDaySexUseCondom;

	public static bool isSexEnd;

	public static Sprite sexBattleBgSprite;

	public static string startSexSceneTypeName;

	public static bool isShopBuy;

	public static bool isUsedShop;

	public static bool isUsedShopForScnearioCheck;

	public static int storeSelectCategoryNum;

	public static int storeTendingRemainTime;

	public static int storeTendingTradeCount;

	public static bool isOpencarriageStoreResult;

	public static int storeTendingBeforeRandomNum;

	public static bool isCheckShopRankChange;

	public static bool isShopRankChange;

	public static int[] shopRankTempLvArray = new int[2];

	public static bool isShopRankChangeFromNotice;

	public static bool isFreeDungeonNotice;

	public static bool isDeepDungeonNotice;

	public static bool isEnableMapMenuNotice;

	public static bool isNewItemShopNotice;

	public static bool isHeroineFollowNotice;

	public static bool isHeroineAllTimeFollowNotice;

	public static bool isHeroineSpecifyFollowNotice;

	public static bool isHeroineSpecifyFollowLocalMapNotice;

	public static bool isHeroinePowerUpNotice;

	public static bool isHeroineMergeNotice;

	public static bool isHeroineMoveNotice;

	public static bool isDungeonSexNotice;

	public static bool isSexTouchxNotice;

	public static bool isFellatioNotice;

	public static bool isSexBattleNotice;

	public static bool isHeroineSexStatusViewNotice;

	public static bool isHeroineMenstruationStartNotice;

	public static bool isHeroineMenstruationViewNotice;

	public static bool isHeroineEnableFertilizeNotice;

	public static bool isHeroineScnearioAllClearNotice;

	public static string noticeTermString;

	public static string noticeDungeonTermString;

	public static bool isUnreportedQuest;

	public static bool isNewStoryQuestNotice;

	public static bool isNewStorySubQuestNotice;

	public static string openDialogName;

	public static string selectDisableMapPointTerm;

	public static string battleResultDialogType;

	public static string battleBeforePointType;

	public static string beforeWorldMapPointName;

	public static string beforeLocalMapPlaceName;

	public static int beforeTotalTimeZoneCount;

	public static int beforeCurrentTimeZone;

	public static int beforeCurrentMonthDay;

	public static int beforeCurrentTotalDay;

	public static bool isRestoreDungeonBattleFailed;

	public static string selectTutorialName;

	public static bool isTutorialOpened;

	public static bool isTutorialCrafted;

	public static bool isChargeAttackTutorial;

	public static bool isHeroineFollowTutorial;

	public static bool isCarriageStoreTutorial;

	public static string loadSceneName;

	public static string unLoadSceneName;

	public static string currentSceneName;

	public static string heroineUnFollowBeforeStateName;

	public static float heroineUnFollowBlackImageAlpha;

	public static bool isHeroineUnFollowRightClickBlock;

	public static bool isHeroineUnFollowReserveAtLocalMap;

	public static string selectScenarioName;

	public static string victoryScenarioName;

	public static string rematchScenarioName;

	public static string resultScenarioName;

	public static string currentScenarioLabelName;

	public static string utagePlayBattleBgmName;

	public static string backToBeforeScenarioLabelName;

	public static bool isBackBeforeWorldMap;

	public static bool isRequiedUtageResume;

	public static bool isUtagePlayBattleBgm;

	public static bool isUtagePlayBattleBgmNonStop;

	public static bool isUtageAutoPlayWait;

	public static bool isUtageHmode;

	public static string utageInterruptedDialogType;

	public static int addTimeZoneNum;

	public static int addDungeonTimeZoneNum;

	public static int oldTimeZone;

	public static int oldDayCount;

	public static int oldTimeBlock;

	public static bool isRequiredCalcCarriageStore;

	public static bool isAddTimeFromDungeon;

	public static bool isAddTimeFromScenario;

	public static bool isAddTimeEnd;

	public static bool isAddTimeFromMapRest;

	public static bool isSaveDataLoad;

	public static bool isInitializeMapData;

	public static bool systemSaveEnable;

	public static string selectSystemTabName;

	public static int selectSlotPageNum;

	public static int selectSlotNum;

	public static bool isDemo;

	public static bool isInterruptedSave;

	public static bool isInterruptedAfterSave;

	public static bool isEndGame;

	public static float gameStartTime;

	public static int openUiScreenLevel;

	public static int beforeUiScreenLevel;

	public static int sendHelpMarkButtonIndex;

	public static bool isSendHelpMarkButton;

	public static int garellySelectTabNum;

	public static int garellySelectPageNum;

	public static bool isGarellyOpenWithTitle;

	public static bool isUtageToJumpFromGarelly;

	public static bool isTitleDebugToUtage;

	public static int[] beforePlayerHp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] beforePlayerMp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int beforeEquipWeaponTp;

	public static int beforePlayerAllHp;

	public static int[] beforePlayerSp = new int[PlayerStatusDataManager.partyMemberCount];

	public static List<int> beforeHaveItemSortIdList = new List<int>();

	public static List<int> beforeHaveItemIdList = new List<int>();

	public static List<int> beforeHaveItemHaveNumList = new List<int>();
}
