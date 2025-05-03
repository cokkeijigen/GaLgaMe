using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimeManagerForPM : MonoBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private WorldMapAccessManager worldMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	public GameObject longHandGo;

	public GameObject weekDayImageGo;

	public Image clockTimeZoneImage;

	public Image clockTimeZoneBeforeImage;

	public Sprite[] clockTimeZoneSpriteArray;

	private void Awake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		clockTimeZoneBeforeImage.color = new Color(1f, 1f, 1f, 0f);
	}

	public int GetCurrentTimeZone()
	{
		return PlayerDataManager.currentTimeZone;
	}

	public int GetNewTimeZone()
	{
		return PlayerDataManager.totalTimeZoneCount;
	}

	public int GetOldTimeZone()
	{
		return PlayerNonSaveDataManager.oldTimeZone;
	}

	public bool GetIsClockChangeEnable()
	{
		return PlayerNonSaveDataManager.isClockChangeEnable;
	}

	public int GetCurrentTotalDayCount()
	{
		return PlayerDataManager.currentTotalDay;
	}

	public int GetAddTimeDayCount()
	{
		return PlayerNonSaveDataManager.addTimeZoneNum / 4;
	}

	public void SetClockTimeZoneImage(float fadeTime)
	{
		clockTimeZoneBeforeImage.sprite = clockTimeZoneImage.sprite;
		clockTimeZoneBeforeImage.color = Color.white;
		clockTimeZoneImage.sprite = clockTimeZoneSpriteArray[PlayerDataManager.currentTimeZone];
		clockTimeZoneBeforeImage.DOFade(0f, fadeTime);
		localMapAccessManager.SetLocalMapTimeZoneImage();
		worldMapAccessManager.SetWorldMapTimeZoneImage();
	}

	public void SetRealTimeWeekDayIconSprite()
	{
		headerStatusManager.isWeekIconInitialize = false;
		Image[] weekIconImageArray = headerStatusManager.weekIconImageArray;
		for (int i = 0; i < weekIconImageArray.Length; i++)
		{
			weekIconImageArray[i].color = new Color(1f, 1f, 1f, 0.5f);
		}
		PlayerNonSaveDataManager.oldDayCount++;
		int num = PlayerNonSaveDataManager.oldDayCount / 7;
		int num2 = PlayerNonSaveDataManager.oldDayCount - 7 * num;
		headerStatusManager.weekIconImageArray[num2].color = Color.white;
		SetWeekDayString(num2);
	}

	public void SetWeekDayIconSprite()
	{
		Image[] weekIconImageArray = headerStatusManager.weekIconImageArray;
		for (int i = 0; i < weekIconImageArray.Length; i++)
		{
			weekIconImageArray[i].color = new Color(1f, 1f, 1f, 0.5f);
		}
		int num = Mathf.FloorToInt((float)PlayerDataManager.currentTotalDay / 7f);
		int num2 = PlayerDataManager.currentTotalDay - 7 * num;
		headerStatusManager.weekIconImageArray[num2].color = Color.white;
		SetWeekDayString(num2);
	}

	private void SetWeekDayString(int day)
	{
		switch (day)
		{
		case 0:
			PlayerDataManager.currentWeekDay = "日";
			break;
		case 1:
			PlayerDataManager.currentWeekDay = "月";
			break;
		case 2:
			PlayerDataManager.currentWeekDay = "火";
			break;
		case 3:
			PlayerDataManager.currentWeekDay = "水";
			break;
		case 4:
			PlayerDataManager.currentWeekDay = "木";
			break;
		case 5:
			PlayerDataManager.currentWeekDay = "金";
			break;
		case 6:
			PlayerDataManager.currentWeekDay = "土";
			break;
		}
		headerStatusManager.isWeekIconInitialize = true;
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			headerStatusManager.RefreshFollowHeroineMenstrualDay();
		}
		Debug.Log("曜日の更新を完了");
	}

	public bool CheckWeekIconInitializeEnd()
	{
		return headerStatusManager.isWeekIconInitialize;
	}

	public bool CheckAddTimeFromeScenario()
	{
		return PlayerNonSaveDataManager.isAddTimeFromScenario;
	}

	public bool CheckAddTimeFromeDungeon()
	{
		return PlayerNonSaveDataManager.isAddTimeFromDungeon;
	}

	public void AddDungeonTimeZone()
	{
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
		PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
		PlayerNonSaveDataManager.addTimeZoneNum = PlayerNonSaveDataManager.addDungeonTimeZoneNum;
	}

	public bool CheckChangeDay()
	{
		bool result = false;
		if (PlayerDataManager.currentTimeZone + PlayerNonSaveDataManager.addTimeZoneNum >= 4)
		{
			result = true;
		}
		return result;
	}

	public void ResetAddTimeVariables()
	{
		PlayerNonSaveDataManager.addDungeonTimeZoneNum = 0;
		PlayerNonSaveDataManager.isAddTimeFromDungeon = false;
		PlayerNonSaveDataManager.isAddTimeFromScenario = false;
		PlayerNonSaveDataManager.isAddTimeFromMapRest = false;
	}

	public void ResetAddTimeZoneNum()
	{
		PlayerNonSaveDataManager.addTimeZoneNum = 0;
	}

	public void SendRefreshUnFollowHeroineStatus()
	{
		PlayerStatusDataManager.RefreshUnFollowHeroineStatus();
	}

	public void DateElapsedProcess(int num)
	{
		if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
		{
			headerStatusManager.rareDropPanelGo.SetActive(value: true);
			headerStatusManager.rareDropRemainingDaysText.text = PlayerDataManager.rareDropRateRaiseRaimingDaysNum.ToString();
		}
		else
		{
			headerStatusManager.rareDropPanelGo.SetActive(value: false);
		}
		Debug.Log("レアドロップ率を確認");
	}
}
