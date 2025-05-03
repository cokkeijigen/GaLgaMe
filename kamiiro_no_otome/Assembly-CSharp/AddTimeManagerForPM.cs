using Arbor;
using UnityEngine;

public class AddTimeManagerForPM : MonoBehaviour
{
	public void AddTotalTimeZone()
	{
		PlayerNonSaveDataManager.beforeTotalTimeZoneCount = PlayerDataManager.totalTimeZoneCount;
		PlayerDataManager.totalTimeZoneCount += PlayerNonSaveDataManager.addTimeZoneNum;
	}

	public int GetCurrentTimeZone()
	{
		return PlayerDataManager.currentTimeZone;
	}

	public int GetTotalTimeZone()
	{
		return PlayerDataManager.totalTimeZoneCount;
	}

	public void SetCurrentTimeZoneToZero()
	{
		int num = 4 - PlayerDataManager.currentTimeZone;
		PlayerDataManager.totalTimeZoneCount += num;
		PlayerDataManager.currentTimeZone = 0;
	}

	public void SetCurrentTimeZone(int num)
	{
		PlayerDataManager.currentTimeZone = num;
	}

	public int GetCurrentTotalDayCount()
	{
		return PlayerDataManager.currentTotalDay;
	}

	public int GetAddTimeZoneCount()
	{
		return PlayerNonSaveDataManager.addTimeZoneNum;
	}

	public void SetNewTimeZone(int num)
	{
		PlayerDataManager.totalTimeZoneCount += num;
	}

	public void SetOldTimeZone(int num)
	{
		PlayerNonSaveDataManager.oldTimeZone += num;
	}

	public int GetNeedMoveDayCount()
	{
		return PlayerNonSaveDataManager.needMoveDayCount;
	}

	public void AddCurrentDayCount(int num)
	{
		PlayerDataManager.currentMonthDay += num;
		PlayerDataManager.currentTotalDay += num;
	}

	public int GetCurrentMonthDay()
	{
		return PlayerDataManager.currentMonthDay;
	}

	public void SetCurrentTotalDay(int num)
	{
		PlayerDataManager.currentTotalDay = Mathf.Clamp(num, 0, 99999);
	}

	public void SetCurrentMonthDay(int num)
	{
		PlayerDataManager.currentMonthDay = num;
	}

	public void SetAddTimeEnd(bool value)
	{
		PlayerNonSaveDataManager.isAddTimeEnd = value;
	}

	public bool GetIsAddTimeFromScenario()
	{
		return PlayerNonSaveDataManager.isAddTimeFromScenario;
	}

	public void ResetScenarioAddTimeFlag()
	{
		PlayerNonSaveDataManager.isAddTimeFromScenario = false;
		PlayerNonSaveDataManager.isAddTimeFromMapRest = false;
	}

	public bool GetIsAddTimeFromMapRest()
	{
		return PlayerNonSaveDataManager.isAddTimeFromMapRest;
	}

	public bool GetIsRequiredCalcCarriageStore()
	{
		return PlayerNonSaveDataManager.isRequiredCalcCarriageStore;
	}

	public bool GetIsStoreTending()
	{
		return PlayerNonSaveDataManager.isOpencarriageStoreResult;
	}

	public void SendCalcStoreTending()
	{
		PlayerNonSaveDataManager.storeTendingRemainTime = 2;
		GameObject.Find("Store Tending Manager").GetComponent<ArborFSM>().SendTrigger("CalcStoreTending");
	}

	public void SubtractHotSellCategory()
	{
		PlayerDataManager.ChangeHotSellCategoryDayCount();
	}

	public void DateElapsedProcess(int num)
	{
		PlayerDataManager.rareDropRateRaiseRaimingDaysNum = Mathf.Clamp(PlayerDataManager.rareDropRateRaiseRaimingDaysNum - num, 0, 99);
		foreach (HaveWeaponData haveWeapon in PlayerInventoryDataManager.haveWeaponList)
		{
			haveWeapon.remainingDaysToCraft = Mathf.Clamp(haveWeapon.remainingDaysToCraft - num, 0, 999);
		}
		foreach (HaveArmorData haveArmor in PlayerInventoryDataManager.haveArmorList)
		{
			haveArmor.remainingDaysToCraft = Mathf.Clamp(haveArmor.remainingDaysToCraft - num, 0, 999);
		}
		Debug.Log("日付経過による減産処理を行う：" + num + "日");
	}

	public void TimeElapsedProcsee()
	{
		for (int i = 0; i < PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina.Length; i++)
		{
			PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[i] -= PlayerNonSaveDataManager.addTimeZoneNum;
			PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[i] = Mathf.Clamp(PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[i], 0, 99);
		}
		for (int j = 0; j < PlayerSexStatusDataManager.heroineRemainingSemenCount_anal.Length; j++)
		{
			PlayerSexStatusDataManager.heroineRemainingSemenCount_anal[j] -= PlayerNonSaveDataManager.addTimeZoneNum;
			PlayerSexStatusDataManager.heroineRemainingSemenCount_anal[j] = Mathf.Clamp(PlayerSexStatusDataManager.heroineRemainingSemenCount_anal[j], 0, 99);
		}
	}
}
