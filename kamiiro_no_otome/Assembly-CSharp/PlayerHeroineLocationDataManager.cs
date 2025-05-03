using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerHeroineLocationDataManager : SerializedMonoBehaviour
{
	public static HeroineCheckData CheckLocalMapHeroineHere(string placeName)
	{
		HeroineCheckData result = default(HeroineCheckData);
		List<HeroineLocationData> list = GameDataManager.instance.heroineLocationDataBase.heroineLocationDataList.Where((HeroineLocationData data) => data.worldPointName == PlayerDataManager.currentAccessPointName && data.localPlaceName == placeName && data.mapType == "localMap").ToList();
		List<HeroineLocationData> list2 = new List<HeroineLocationData>(list);
		Debug.Log("Whereリスト数：" + list.Count + "／インドア名：" + placeName + "／removeリスト数：" + list2.Count);
		for (int num = list2.Count; num > 0; num--)
		{
			int index = num - 1;
			if (!PlayerFlagDataManager.scenarioFlagDictionary[list[index].needFlagName])
			{
				Debug.Log("ヒロイン／未クリアのものをリストから削除／インドア名：" + placeName + "／ソートID：" + list2[index].sortID);
				list2.RemoveAt(index);
			}
		}
		List<HeroineLocationData> list3 = list2.Where((HeroineLocationData data) => data.heroineID == 1).ToList();
		List<HeroineLocationData> list4 = list2.Where((HeroineLocationData data) => data.heroineID == 2).ToList();
		List<HeroineLocationData> list5 = list2.Where((HeroineLocationData data) => data.heroineID == 3).ToList();
		List<HeroineLocationData> list6 = list2.Where((HeroineLocationData data) => data.heroineID == 4).ToList();
		bool[] array = new bool[4];
		if (list3 != null && list3.Count > 0)
		{
			array[0] = SeparateCheckLocalMapHeroineHere(list3, placeName);
		}
		if (list4 != null && list4.Count > 0)
		{
			array[1] = SeparateCheckLocalMapHeroineHere(list4, placeName);
		}
		if (list5 != null && list5.Count > 0)
		{
			array[2] = SeparateCheckLocalMapHeroineHere(list5, placeName);
		}
		if (list6 != null && list6.Count > 0)
		{
			array[3] = SeparateCheckLocalMapHeroineHere(list6, placeName);
		}
		if (array.Any((bool value) => value))
		{
			switch (Array.IndexOf(array, value: true))
			{
			case 0:
				result.isHeroineHere = true;
				result.heroineID = 1;
				break;
			case 1:
				result.isHeroineHere = true;
				result.heroineID = 2;
				break;
			case 2:
				result.isHeroineHere = true;
				result.heroineID = 3;
				break;
			case 3:
				result.isHeroineHere = true;
				result.heroineID = 4;
				break;
			}
			Debug.Log("ヒロインはいる／ID：" + result.heroineID + "／インドア名：" + placeName);
		}
		else
		{
			Debug.Log("ヒロインはいない／インドア名：" + placeName);
			result.isHeroineHere = false;
		}
		return result;
	}

	public static HeroineCheckData CheckLocalMapHeroineHereWithWorldMap(string pointName, string placeName)
	{
		HeroineCheckData result = default(HeroineCheckData);
		List<HeroineLocationData> list = GameDataManager.instance.heroineLocationDataBase.heroineLocationDataList.Where((HeroineLocationData data) => data.worldPointName == pointName && data.localPlaceName == placeName && data.mapType == "localMap").ToList();
		List<HeroineLocationData> list2 = new List<HeroineLocationData>(list);
		Debug.Log("Whereリスト数：" + list.Count + "／インドア名：" + placeName + "／removeリスト数：" + list2.Count);
		for (int num = list2.Count; num > 0; num--)
		{
			int index = num - 1;
			if (!PlayerFlagDataManager.scenarioFlagDictionary[list[index].needFlagName])
			{
				Debug.Log("ヒロイン／未クリアのものをリストから削除／インドア名：" + placeName + "／ソートID：" + list2[index].sortID);
				list2.RemoveAt(index);
			}
		}
		List<HeroineLocationData> list3 = list2.Where((HeroineLocationData data) => data.heroineID == 1).ToList();
		List<HeroineLocationData> list4 = list2.Where((HeroineLocationData data) => data.heroineID == 2).ToList();
		List<HeroineLocationData> list5 = list2.Where((HeroineLocationData data) => data.heroineID == 3).ToList();
		List<HeroineLocationData> list6 = list2.Where((HeroineLocationData data) => data.heroineID == 4).ToList();
		bool[] array = new bool[4];
		if (list3 != null && list3.Count > 0)
		{
			array[0] = SeparateCheckLocalMapHeroineHereWithWorldMap(list3, pointName, placeName);
		}
		if (list4 != null && list4.Count > 0)
		{
			array[1] = SeparateCheckLocalMapHeroineHereWithWorldMap(list4, pointName, placeName);
		}
		if (list5 != null && list5.Count > 0)
		{
			array[2] = SeparateCheckLocalMapHeroineHereWithWorldMap(list5, pointName, placeName);
		}
		if (list6 != null && list6.Count > 0)
		{
			array[3] = SeparateCheckLocalMapHeroineHereWithWorldMap(list6, pointName, placeName);
		}
		if (array.Any((bool value) => value))
		{
			switch (Array.IndexOf(array, value: true))
			{
			case 0:
				result.isHeroineHere = true;
				result.heroineID = 1;
				break;
			case 1:
				result.isHeroineHere = true;
				result.heroineID = 2;
				break;
			case 2:
				result.isHeroineHere = true;
				result.heroineID = 3;
				break;
			case 3:
				result.isHeroineHere = true;
				result.heroineID = 4;
				break;
			}
			Debug.Log("ヒロインはいる／ID：" + result.heroineID + "／インドア名：" + placeName);
		}
		else
		{
			Debug.Log("ヒロインはいない／インドア名：" + placeName);
			result.isHeroineHere = false;
		}
		return result;
	}

	private static bool SeparateCheckLocalMapHeroineHere(List<HeroineLocationData> heroineList, string placeName)
	{
		bool result = false;
		HeroineLocationData heroineLocationData = null;
		if (heroineList != null && heroineList.Count > 0)
		{
			heroineLocationData = heroineList.OrderByDescending((HeroineLocationData data) => data.sortID).FirstOrDefault();
			for (int i = 0; i < heroineLocationData.weekLocationDataList.Count; i++)
			{
				if (heroineLocationData.weekLocationDataList[i].weekDayList.Contains(PlayerDataManager.currentWeekDay))
				{
					if (heroineLocationData.weekLocationDataList[i].timeZoneList.Contains(PlayerDataManager.currentTimeZone))
					{
						if (heroineLocationData.weekLocationDataList[i].isVisible)
						{
							Debug.Log("１つ以上ある／インドア名：" + heroineLocationData.localPlaceName + "／ヒロインID：" + heroineLocationData.heroineID + "／表示");
							result = true;
							break;
						}
						Debug.Log("ヒロインはいない／非表示／インドア名：" + placeName);
						result = false;
					}
					else
					{
						Debug.Log("ヒロインはいない／時間不一致／インドア名：" + placeName);
						result = false;
					}
				}
				else
				{
					Debug.Log("ヒロインはいない／曜日不一致：" + placeName);
					result = false;
				}
			}
		}
		else
		{
			Debug.Log("ヒロインはいない／リストはnull／インドア名：" + placeName);
			result = false;
		}
		return result;
	}

	private static bool SeparateCheckLocalMapHeroineHereWithWorldMap(List<HeroineLocationData> heroineList, string pointName, string placeName)
	{
		bool result = false;
		string item = "";
		HeroineLocationData heroineLocationData = null;
		WorldMapAccessManager component = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		int num = component.GetNeedAccessDay(PlayerDataManager.currentAccessPointName, pointName);
		int num2 = component.GetNeedAccessTime(PlayerDataManager.currentAccessPointName, pointName);
		if (num2 >= 4)
		{
			int num3 = Mathf.FloorToInt(num2 / 4);
			num += num3;
			num2 -= num3 * 4;
		}
		int num4 = PlayerDataManager.currentTotalDay + num;
		int num5 = PlayerDataManager.currentTimeZone + num2;
		if (num5 >= 4)
		{
			num5 -= 4;
			num4++;
		}
		int num6 = Mathf.FloorToInt((float)num4 / 7f);
		switch (num4 - 7 * num6)
		{
		case 0:
			item = "日";
			break;
		case 1:
			item = "月";
			break;
		case 2:
			item = "火";
			break;
		case 3:
			item = "水";
			break;
		case 4:
			item = "木";
			break;
		case 5:
			item = "金";
			break;
		case 6:
			item = "土";
			break;
		}
		if (heroineList != null && heroineList.Count > 0)
		{
			heroineLocationData = heroineList.OrderByDescending((HeroineLocationData data) => data.sortID).FirstOrDefault();
			for (int i = 0; i < heroineLocationData.weekLocationDataList.Count; i++)
			{
				if (heroineLocationData.weekLocationDataList[i].weekDayList.Contains(item))
				{
					if (heroineLocationData.weekLocationDataList[i].timeZoneList.Contains(num5))
					{
						if (heroineLocationData.weekLocationDataList[i].isVisible)
						{
							Debug.Log("１つ以上ある／インドア名：" + heroineLocationData.localPlaceName + "／ヒロインID：" + heroineLocationData.heroineID + "／表示");
							result = true;
							break;
						}
						Debug.Log("ヒロインはいない／非表示／インドア名：" + placeName);
						result = false;
					}
					else
					{
						Debug.Log("ヒロインはいない／時間不一致／インドア名：" + placeName);
						result = false;
					}
				}
				else
				{
					Debug.Log("ヒロインはいない／曜日不一致：" + placeName);
					result = false;
				}
			}
		}
		else
		{
			Debug.Log("ヒロインはいない／リストはnull／インドア名：" + placeName);
			result = false;
		}
		return result;
	}

	public static HeroineCheckData CheckWorldMapHeroineHere(string pointName)
	{
		string item = "";
		HeroineLocationData heroineLocationData = null;
		HeroineCheckData result = default(HeroineCheckData);
		WorldMapAccessManager component = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		List<HeroineLocationData> list = GameDataManager.instance.heroineLocationDataBase.heroineLocationDataList.Where((HeroineLocationData data) => data.worldPointName == pointName && data.mapType == "worldMap").ToList();
		List<HeroineLocationData> list2 = new List<HeroineLocationData>(list);
		for (int num = list2.Count; num > 0; num--)
		{
			int index = num - 1;
			if (!PlayerFlagDataManager.scenarioFlagDictionary[list[index].needFlagName])
			{
				list2.RemoveAt(index);
			}
		}
		if (list2 != null && list2.Count > 0)
		{
			result.heroineID = list2[0].heroineID;
			int num2 = component.GetNeedAccessDay(PlayerDataManager.currentAccessPointName, pointName);
			int num3 = component.GetNeedAccessTime(PlayerDataManager.currentAccessPointName, pointName);
			if (num3 >= 4)
			{
				int num4 = Mathf.FloorToInt(num3 / 4);
				num2 += num4;
				num3 -= num4 * 4;
			}
			int num5 = PlayerDataManager.currentTotalDay + num2;
			int num6 = PlayerDataManager.currentTimeZone + num3;
			if (num6 >= 4)
			{
				num6 -= 4;
				num5++;
			}
			int num7 = Mathf.FloorToInt((float)num5 / 7f);
			switch (num5 - 7 * num7)
			{
			case 0:
				item = "日";
				break;
			case 1:
				item = "月";
				break;
			case 2:
				item = "火";
				break;
			case 3:
				item = "水";
				break;
			case 4:
				item = "木";
				break;
			case 5:
				item = "金";
				break;
			case 6:
				item = "土";
				break;
			}
			heroineLocationData = list2.OrderByDescending((HeroineLocationData data) => data.sortID).FirstOrDefault();
			for (int i = 0; i < heroineLocationData.weekLocationDataList.Count; i++)
			{
				if (heroineLocationData.weekLocationDataList[i].weekDayList.Contains(item))
				{
					if (heroineLocationData.weekLocationDataList[i].timeZoneList.Contains(num6))
					{
						if (heroineLocationData.weekLocationDataList[i].isVisible)
						{
							Debug.Log("１つ以上ある／インドア名：" + heroineLocationData.localPlaceName + "／ヒロインID：" + heroineLocationData.heroineID + "／表示");
							result.isHeroineHere = true;
							break;
						}
						Debug.Log("ヒロインはいない／インドア名：" + pointName);
						result.isHeroineHere = false;
					}
					else
					{
						Debug.Log("ヒロインはいない／時間不一致／インドア名：" + pointName);
						result.isHeroineHere = false;
					}
				}
				else
				{
					Debug.Log("ヒロインはいない／曜日不一致");
				}
			}
		}
		else
		{
			Debug.Log("ヒロインはいない／ポイント名：" + pointName);
			result.isHeroineHere = false;
		}
		return result;
	}
}
