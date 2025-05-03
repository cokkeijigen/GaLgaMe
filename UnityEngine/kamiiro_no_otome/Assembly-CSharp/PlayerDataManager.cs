using System.Collections.Generic;
using Arbor;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerDataManager : SerializedMonoBehaviour
{
	public static int playerHaveMoney;

	public static List<int> playerPartyMember = new List<int>();

	public static int[] playerSaveDataHp = new int[5];

	public static int playerHaveKizunaPoint;

	public static int[] playerPartyKizunaLv = new int[4];

	public static int playerLibido;

	public static int retreatProbability = 70;

	public static int currentTimeZone;

	public static int totalTimeZoneCount;

	public static int currentMonthDay;

	public static int currentTotalDay;

	public static string currentWeekDay;

	public static string playBgmCategoryName;

	public static string currentAccessPointName;

	public static string currentPlaceName;

	public static int mapPlaceStatusNum;

	public static bool isSelectDungeon;

	public static bool worldMapInputBlock;

	public static bool isLocalMapActionLimit;

	public static bool isNeedEffectNewWorldMapPoint;

	public static string needEffectNewWorldMapPointName;

	public static Dictionary<string, bool> KingdomMobHeroineVisibleDictionary = new Dictionary<string, bool>();

	public static Dictionary<string, bool> CityMobHeroineVisibleDictionary = new Dictionary<string, bool>();

	public static Dictionary<string, int> KingdomMobCheckTimeDictionary = new Dictionary<string, int>();

	public static Dictionary<string, int> CityMobCheckTimeDictionary = new Dictionary<string, int>();

	public static bool isHeroineSpecifyFollow;

	public static string heroineSpecifyFollowPoint;

	public static int heroineSpecifyFollowId;

	public static bool isHeroineSexVoiceLowStage;

	public static bool isHeroineSexTouchTextVisible;

	public static string[] newMapPointName = new string[2];

	public static bool isNewMapNotice;

	public static bool isNewRecipeNotice;

	public static bool isNewCraftAndExtensionNotice;

	public static bool isRecommendSaveAlertNoOpen;

	public static bool isNoCheckNewQuest;

	public static bool isNoCheckNewSubQuest;

	public static int lastSaveSlotPageNum;

	public static int lastSaveSlotNum;

	public static int scenarioBattleSpeed;

	public static bool isCaseOfSkillMoveCursor;

	public static string currentDungeonName;

	public static string currentDungeonScenarioName;

	public static bool isDungeonHeroineFollow;

	public static int DungeonHeroineFollowNum;

	public static int dungeonBattleSpeed;

	public static int dungeonMoveSpeed;

	public static bool isDungeonMapAuto;

	public static int dungeonEnterTimeZoneNum;

	public static int rareDropRateRaisePowerNum;

	public static int rareDropRateRaiseRaimingDaysNum;

	public static bool isResultAutoClose;

	public static int totalSalesAmount;

	public static int hotSellingCategoryNum;

	public static int hotSellingRemainDayCount;

	public static int hotSellingPriceBonus;

	public static int hotSellingTradeBonus;

	public static int carriageStoreTradeCount;

	public static int carriageStoreTradeMoneyNum;

	public static List<int[]> storeTradeSuccessItemList = new List<int[]>();

	public static bool isStoreTending;

	public static int carriageStoreSellMagnification;

	public static int currentShopRankFirstNum;

	public static int currentShopRankSecondNum;

	public static int itemShopPoint;

	public static void AddHaveMoney(int getMoney)
	{
		playerHaveMoney += getMoney;
		playerHaveMoney = Mathf.Clamp(playerHaveMoney, 0, 9999999);
	}

	public static void AddMoveTime(string timeType, int num, bool isCalcCarriageStore)
	{
		bool flag = false;
		if (!(timeType == "day"))
		{
			if (timeType == "zone")
			{
				currentTimeZone += num;
				if (currentTimeZone > 3)
				{
					currentMonthDay++;
					currentTotalDay++;
					flag = true;
				}
				if (currentMonthDay > 30)
				{
					currentMonthDay = 1;
				}
			}
		}
		else
		{
			currentMonthDay += num;
			currentTotalDay += num;
			if (currentMonthDay > 30)
			{
				currentMonthDay = 1;
			}
			currentTimeZone = 0;
			flag = true;
		}
		if (flag && isCalcCarriageStore)
		{
			if (!isStoreTending)
			{
				PlayerNonSaveDataManager.storeTendingRemainTime = 1;
				GameObject.Find("Store Tending Manager").GetComponent<ArborFSM>().SendTrigger("CalcStoreTending");
			}
			else
			{
				isStoreTending = false;
			}
		}
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("SetClockTime");
		Debug.Log("時間加算完了");
		hotSellingRemainDayCount--;
		if (hotSellingRemainDayCount <= 0)
		{
			RefreshHotSellCategory();
		}
	}

	public static void ChangeHotSellCategoryDayCount()
	{
		int num = PlayerNonSaveDataManager.addTimeZoneNum / 4;
		hotSellingRemainDayCount -= num;
		if (hotSellingRemainDayCount <= 0)
		{
			RefreshHotSellCategory();
		}
	}

	public static void RefreshHotSellCategory()
	{
		if (Random.Range(0, 2) == 0)
		{
			hotSellingCategoryNum = 0;
		}
		else
		{
			hotSellingCategoryNum = 1;
		}
		int num = Random.Range(4, 11);
		int num2 = Random.Range(4, 11);
		hotSellingPriceBonus = num;
		hotSellingTradeBonus = num2;
		hotSellingRemainDayCount = Random.Range(7, 14);
	}

	public static void AddDungeonExploreTime()
	{
		currentMonthDay++;
		currentTotalDay++;
		if (currentMonthDay > 30)
		{
			currentMonthDay = 1;
		}
	}
}
