using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PreLoadManager : MonoBehaviour
{
	public PlayMakerFSM preLoadFSM;

	public bool isDemo;

	private bool isSplashEnd;

	private void Awake()
	{
		PlayerNonSaveDataManager.isDemo = isDemo;
		isSplashEnd = false;
		ES3Settings.defaultSettings.path = "SaveFile.es3";
	}

	private IEnumerator PreLoadWait()
	{
		SplashScreen.Begin();
		while (!SplashScreen.isFinished)
		{
			SplashScreen.Draw();
			yield return null;
		}
		preLoadFSM.SendEvent("SplashEnd");
	}

	private void Update()
	{
		if (SplashScreen.isFinished && !isSplashEnd)
		{
			isSplashEnd = true;
			preLoadFSM.SendEvent("SplashEnd");
		}
	}

	public void ReadScenarioFlagData()
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		Dictionary<string, bool> dictionary2 = new Dictionary<string, bool>();
		new Dictionary<string, int>();
		PlayerFlagDataManager.scenarioFlagDictionary.Clear();
		PlayerFlagDataManager.sceneGarellyFlagDictionary.Clear();
		List<ScenarioFlagData> scenarioFlagDataList = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList;
		for (int i = 0; i < scenarioFlagDataList.Count; i++)
		{
			if (!dictionary.ContainsKey(scenarioFlagDataList[i].scenarioName))
			{
				dictionary.Add(scenarioFlagDataList[i].scenarioName, value: false);
			}
		}
		PlayerFlagDataManager.scenarioFlagDictionary = dictionary;
		List<ScenarioFlagData> list = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.isGarellyScene).ToList();
		for (int j = 0; j < list.Count; j++)
		{
			if (!dictionary2.ContainsKey(list[j].scenarioName))
			{
				dictionary2.Add(list[j].scenarioName, value: false);
			}
		}
		PlayerFlagDataManager.sceneGarellyFlagDictionary = dictionary2;
		List<ScenarioFlagData> list2 = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Where((ScenarioFlagData data) => data.isSaveStartingDay).ToList();
		for (int k = 0; k < list2.Count; k++)
		{
			PlayerFlagDataManager.eventStartingDayDictionary.Add(list2[k].scenarioName, int.MaxValue);
		}
		Debug.Log("シナリオ名の読み込み完了");
	}

	public void ReadQuestFlagData()
	{
		for (int i = 0; i < GameDataManager.instance.questDataBase.questDataList.Count; i++)
		{
			if (GameDataManager.instance.questDataBase.questDataList[i].requirementList.Count > 0)
			{
				QuestClearData item = new QuestClearData
				{
					sortID = GameDataManager.instance.questDataBase.questDataList[i].sortID,
					needRequirementCount = GameDataManager.instance.questDataBase.questDataList[i].requirementList[1],
					currentRequirementCount = 0,
					isOrdered = false,
					isClear = false
				};
				PlayerFlagDataManager.questClearFlagList.Add(item);
			}
			else
			{
				QuestClearData item2 = new QuestClearData
				{
					sortID = GameDataManager.instance.questDataBase.questDataList[i].sortID,
					needRequirementCount = 0,
					currentRequirementCount = 0,
					isOrdered = false,
					isClear = false
				};
				PlayerFlagDataManager.questClearFlagList.Add(item2);
			}
		}
		Debug.Log("クエストフラグの読み込み完了");
	}

	public void ReadUniqueItemFlagData()
	{
		for (int i = 0; i < GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Count; i++)
		{
			if (!string.IsNullOrEmpty(GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList[i].recipeFlagName))
			{
				int itemID = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList[i].itemID;
				PlayerFlagDataManager.keyItemFlagDictionary.Add("campItem" + itemID, value: false);
			}
		}
		for (int j = 0; j < GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Count; j++)
		{
			if (!string.IsNullOrEmpty(GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[j].recipeFlagName))
			{
				int itemID2 = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[j].itemID;
				PlayerFlagDataManager.keyItemFlagDictionary.Add("eventItem" + itemID2, value: false);
			}
		}
		Debug.Log("ユニークアイテムのフラグリストを読み込み完了");
	}

	public void ReadNewCraftItemFlagData()
	{
		for (int i = 0; i < GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Count; i++)
		{
			PlayerFlagDataManager.enableNewCraftFlagDictionary.Add(GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList[i].itemID, value: false);
		}
		for (int j = 0; j < GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Count; j++)
		{
			PlayerFlagDataManager.enableNewCraftFlagDictionary.Add(GameDataManager.instance.itemArmorDataBase.itemArmorDataList[j].itemID, value: false);
		}
		PlayerFlagDataManager.enableNewCraftFlagDictionary[1000] = true;
		PlayerFlagDataManager.enableNewCraftFlagDictionary[2000] = true;
		Debug.Log("エデンの武具アイテムをフラグリストに読み込み完了");
	}

	public void LoadGameOptionData()
	{
		PlayerOptionsDataManager.optionsBgmVolume = ES3.Load("optionsBgmVolume", 0.5f);
		PlayerOptionsDataManager.optionsHBgmVolume = ES3.Load("optionsHBgmVolume", 0.3f);
		PlayerOptionsDataManager.optionsSeVolume = ES3.Load("optionsSeVolume", 0.5f);
		PlayerOptionsDataManager.optionsAmbienceVolume = ES3.Load("optionsAmbienceVolume", 0.5f);
		PlayerOptionsDataManager.optionsVoice1Volume = ES3.Load("optionsVoice1Volume", 0.5f);
		PlayerOptionsDataManager.optionsVoice2Volume = ES3.Load("optionsVoice2Volume", 0.5f);
		PlayerOptionsDataManager.optionsVoice3Volume = ES3.Load("optionsVoice3Volume", 0.5f);
		PlayerOptionsDataManager.optionsVoice4Volume = ES3.Load("optionsVoice4Volume", 0.5f);
		PlayerOptionsDataManager.optionsVoice5Volume = ES3.Load("optionsVoice5Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice1Volume = ES3.Load("optionsSubVoice1Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice2Volume = ES3.Load("optionsSubVoice2Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice3Volume = ES3.Load("optionsSubVoice3Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice4Volume = ES3.Load("optionsSubVoice4Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice5Volume = ES3.Load("optionsSubVoice5Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice6Volume = ES3.Load("optionsSubVoice6Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice7Volume = ES3.Load("optionsSubVoice7Volume", 0.5f);
		PlayerOptionsDataManager.optionsSubVoice8Volume = ES3.Load("optionsSubVoice8Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice1Volume = ES3.Load("optionsMobVoice1Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice2Volume = ES3.Load("optionsMobVoice2Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice3Volume = ES3.Load("optionsMobVoice3Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice4Volume = ES3.Load("optionsMobVoice4Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice5Volume = ES3.Load("optionsMobVoice5Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice6Volume = ES3.Load("optionsMobVoice6Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice7Volume = ES3.Load("optionsMobVoice7Volume", 0.5f);
		PlayerOptionsDataManager.optionsMobVoice8Volume = ES3.Load("optionsMobVoice8Volume", 0.5f);
		PlayerOptionsDataManager.isAllVoiceDisable = ES3.Load("isAllVoiceDisable", defaultValue: false);
		PlayerOptionsDataManager.isLucyVoiceTypeSoft = ES3.Load("isLucyVoiceTypeSoft", defaultValue: true);
		PlayerOptionsDataManager.isLucyVoiceTypeSexy = ES3.Load("isLucyVoiceTypeSexy", defaultValue: false);
		PlayerOptionsDataManager.optionsTextSpeed = ES3.Load("optionsTextSpeed", 0.5f);
		PlayerOptionsDataManager.optionsAutoTextSpeed = ES3.Load("optionsAutoTextSpeed", 0.5f);
		PlayerOptionsDataManager.optionsMouseWheelSend = ES3.Load("optionsMouseWheelSend", defaultValue: true);
		PlayerOptionsDataManager.optionsMouseWheelBacklog = ES3.Load("optionsMouseWheelBacklog", defaultValue: true);
		PlayerOptionsDataManager.optionsMouseWheelPower = ES3.Load("optionsMouseWheelPower", 0.3f);
		PlayerOptionsDataManager.optionsVoiceStopTypeNext = ES3.Load("optionsVoiceStopTypeNext", defaultValue: false);
		PlayerOptionsDataManager.optionsVoiceStopTypeClick = ES3.Load("optionsVoiceStopTypeClick", defaultValue: true);
		PlayerOptionsDataManager.optionsFullScreenMode = ES3.Load("optionsFullScreenMode", defaultValue: false);
		PlayerOptionsDataManager.optionsWindowSize = ES3.Load("optionsWindowSize", 3);
		PlayerDataManager.lastSaveSlotNum = ES3.Load<int>("lastSaveSlotNum");
		PlayerDataManager.lastSaveSlotPageNum = ES3.Load<int>("lastSaveSlotPageNum");
		PlayerDataManager.isRecommendSaveAlertNoOpen = ES3.Load("isRecommendSaveAlertNoOpen", defaultValue: false);
		if (ES3.KeyExists("sceneGarellyFlagDictionary"))
		{
			PlayerFlagDataManager.sceneGarellyFlagDictionary = new Dictionary<string, bool>((from dic in ES3.Load<Dictionary<string, bool>>("sceneGarellyFlagDictionary").Concat(PlayerFlagDataManager.sceneGarellyFlagDictionary)
				group dic by dic.Key).ToDictionary((IGrouping<string, KeyValuePair<string, bool>> dic) => dic.Key, (IGrouping<string, KeyValuePair<string, bool>> dic) => dic.FirstOrDefault().Value));
		}
	}

	public void SetDafaultGameOptionData()
	{
		PlayerOptionsDataManager.optionsBgmVolume = PlayerOptionsDataManager.defaultBgmVolume;
		PlayerOptionsDataManager.optionsHBgmVolume = PlayerOptionsDataManager.defaultHBgmVolume;
		PlayerOptionsDataManager.optionsSeVolume = PlayerOptionsDataManager.defaultSeVolume;
		PlayerOptionsDataManager.optionsAmbienceVolume = PlayerOptionsDataManager.defaultAmbienceVolume;
		PlayerOptionsDataManager.optionsVoice1Volume = PlayerOptionsDataManager.defaultVoice1Volume;
		PlayerOptionsDataManager.optionsVoice2Volume = PlayerOptionsDataManager.defaultVoice2Volume;
		PlayerOptionsDataManager.optionsVoice3Volume = PlayerOptionsDataManager.defaultVoice3Volume;
		PlayerOptionsDataManager.optionsVoice4Volume = PlayerOptionsDataManager.defaultVoice4Volume;
		PlayerOptionsDataManager.optionsVoice5Volume = PlayerOptionsDataManager.defaultVoice5Volume;
		PlayerOptionsDataManager.optionsSubVoice1Volume = PlayerOptionsDataManager.defaultSubVoice1Volume;
		PlayerOptionsDataManager.optionsSubVoice2Volume = PlayerOptionsDataManager.defaultSubVoice2Volume;
		PlayerOptionsDataManager.optionsSubVoice3Volume = PlayerOptionsDataManager.defaultSubVoice3Volume;
		PlayerOptionsDataManager.optionsSubVoice4Volume = PlayerOptionsDataManager.defaultSubVoice4Volume;
		PlayerOptionsDataManager.optionsSubVoice5Volume = PlayerOptionsDataManager.defaultSubVoice5Volume;
		PlayerOptionsDataManager.optionsSubVoice6Volume = PlayerOptionsDataManager.defaultSubVoice6Volume;
		PlayerOptionsDataManager.optionsSubVoice7Volume = PlayerOptionsDataManager.defaultSubVoice7Volume;
		PlayerOptionsDataManager.optionsSubVoice8Volume = PlayerOptionsDataManager.defaultSubVoice8Volume;
		PlayerOptionsDataManager.optionsMobVoice1Volume = PlayerOptionsDataManager.defaultMobVoice1Volume;
		PlayerOptionsDataManager.optionsMobVoice2Volume = PlayerOptionsDataManager.defaultMobVoice2Volume;
		PlayerOptionsDataManager.optionsMobVoice3Volume = PlayerOptionsDataManager.defaultMobVoice3Volume;
		PlayerOptionsDataManager.optionsMobVoice4Volume = PlayerOptionsDataManager.defaultMobVoice4Volume;
		PlayerOptionsDataManager.optionsMobVoice5Volume = PlayerOptionsDataManager.defaultMobVoice5Volume;
		PlayerOptionsDataManager.optionsMobVoice6Volume = PlayerOptionsDataManager.defaultMobVoice6Volume;
		PlayerOptionsDataManager.optionsMobVoice7Volume = PlayerOptionsDataManager.defaultMobVoice7Volume;
		PlayerOptionsDataManager.optionsMobVoice8Volume = PlayerOptionsDataManager.defaultMobVoice8Volume;
		PlayerOptionsDataManager.optionsTextSpeed = PlayerOptionsDataManager.defaultTextSpeed;
		PlayerOptionsDataManager.optionsAutoTextSpeed = PlayerOptionsDataManager.defaultAutoTextSpeed;
		PlayerOptionsDataManager.optionsMouseWheelSend = PlayerOptionsDataManager.defaultMouseWheelSend;
		PlayerOptionsDataManager.optionsMouseWheelBacklog = PlayerOptionsDataManager.defaultMouseWheelBacklog;
		PlayerOptionsDataManager.optionsMouseWheelPower = PlayerOptionsDataManager.defaultMouseWheelPower;
		PlayerOptionsDataManager.optionsVoiceStopTypeNext = PlayerOptionsDataManager.defaultVoiceStopTypeNext;
		PlayerOptionsDataManager.optionsVoiceStopTypeClick = PlayerOptionsDataManager.defaultVoiceStopTypeClick;
		PlayerOptionsDataManager.optionsFullScreenMode = PlayerOptionsDataManager.defaultFullScreenMode;
	}

	public void GetGameWindowSize()
	{
		int width = Screen.currentResolution.width;
		int height = Screen.currentResolution.height;
		PlayerOptionsDataManager.currentDisplayWidth = width;
		PlayerOptionsDataManager.currentDisplayHeight = height;
		Debug.Log("モニタ解像度" + width + "×" + height);
		int num = 50;
		if (height < 720 + num)
		{
			PlayerOptionsDataManager.optionsWindowSize = 0;
		}
		else if (height < 810 + num)
		{
			PlayerOptionsDataManager.optionsWindowSize = 1;
		}
		else if (height < 900 + num)
		{
			PlayerOptionsDataManager.optionsWindowSize = 2;
		}
		else if (height < 1080 + num)
		{
			PlayerOptionsDataManager.optionsWindowSize = 3;
		}
		else if (height < 1260 + num)
		{
			PlayerOptionsDataManager.optionsWindowSize = 4;
		}
		else if (height < 1440 + num)
		{
			PlayerOptionsDataManager.optionsWindowSize = 5;
		}
		else if (height < 2160 + num)
		{
			PlayerOptionsDataManager.optionsWindowSize = 6;
		}
		else
		{
			PlayerOptionsDataManager.optionsWindowSize = 7;
		}
		Debug.Log("ウィンドウ計算サイズ：" + PlayerOptionsDataManager.optionsWindowSize);
	}

	public void SetGameWindowSize()
	{
		PlayerOptionsDataManager.currentDisplayWidth = Screen.currentResolution.width;
		PlayerOptionsDataManager.currentDisplayHeight = Screen.currentResolution.height;
		bool optionsFullScreenMode = PlayerOptionsDataManager.optionsFullScreenMode;
		switch (PlayerOptionsDataManager.optionsWindowSize)
		{
		case 0:
			Screen.SetResolution(1024, 576, optionsFullScreenMode);
			break;
		case 1:
			Screen.SetResolution(1280, 720, optionsFullScreenMode);
			break;
		case 2:
			Screen.SetResolution(1440, 810, optionsFullScreenMode);
			break;
		case 3:
			Screen.SetResolution(1600, 900, optionsFullScreenMode);
			break;
		case 4:
			Screen.SetResolution(1920, 1080, optionsFullScreenMode);
			break;
		case 5:
			Screen.SetResolution(2240, 1260, optionsFullScreenMode);
			break;
		case 6:
			Screen.SetResolution(2560, 1440, optionsFullScreenMode);
			break;
		case 7:
			Screen.SetResolution(3840, 2160, optionsFullScreenMode);
			break;
		}
		Debug.Log("ウィンドウ設定サイズ：" + PlayerOptionsDataManager.optionsWindowSize + "／フルスクリーン：" + PlayerOptionsDataManager.optionsFullScreenMode);
	}

	public void ReadCraftRecipeData()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Count; i++)
		{
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList[i];
			string recipeFlagName = itemWeaponData.recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemWeaponData.itemID);
		}
		for (int j = 0; j < GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Count; j++)
		{
			ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList[j];
			string recipeFlagName = itemPartyWeaponData.recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			if (!string.IsNullOrEmpty(recipeFlagName))
			{
				SetCraftItemIdDictionary(recipeFlagName, itemPartyWeaponData.itemID);
			}
		}
		for (int k = 0; k < GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Count; k++)
		{
			ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList[k];
			string recipeFlagName = GameDataManager.instance.itemArmorDataBase.itemArmorDataList[k].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemArmorData.itemID);
		}
		for (int l = 0; l < GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Count; l++)
		{
			ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList[l];
			string recipeFlagName = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList[l].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			if (!string.IsNullOrEmpty(recipeFlagName))
			{
				SetCraftItemIdDictionary(recipeFlagName, itemPartyArmorData.itemID);
			}
		}
		for (int m = 0; m < GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Count; m++)
		{
			ItemCanMakeMaterialData itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList[m];
			string recipeFlagName = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList[m].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemCanMakeMaterialData.itemID);
		}
		for (int n = 0; n < GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Count; n++)
		{
			ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[n];
			string recipeFlagName = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[n].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			if (!string.IsNullOrEmpty(recipeFlagName))
			{
				SetCraftItemIdDictionary(recipeFlagName, itemEventItemData.itemID);
			}
		}
		for (int num = 0; num < GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Count; num++)
		{
			ItemCampItemData itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList[num];
			string recipeFlagName = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList[num].recipeFlagName;
			if (!list.Contains(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			SetCraftItemIdDictionary(recipeFlagName, itemCampItemData.itemID);
		}
		for (int num2 = 0; num2 < GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Count; num2++)
		{
			ItemEventItemData itemEventItemData2 = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[num2];
			string recipeFlagName = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList[num2].rewardRecipeName;
			if (!list.Contains(recipeFlagName) && !string.IsNullOrEmpty(recipeFlagName))
			{
				list.Add(recipeFlagName);
			}
			if (!string.IsNullOrEmpty(recipeFlagName))
			{
				SetRewardCraftItemIdDictionary(recipeFlagName, itemEventItemData2.itemID);
			}
		}
		Debug.Log("レシピの登録処理を完了");
	}

	private void SetCraftItemIdDictionary(string recipeName, int itemID)
	{
		if (PlayerCraftStatusManager.craftRecipeItemIdDictionary.ContainsKey(recipeName))
		{
			PlayerCraftStatusManager.craftRecipeItemIdDictionary[recipeName].Add(itemID);
		}
		else
		{
			List<int> list = new List<int>();
			list.Add(itemID);
			PlayerCraftStatusManager.craftRecipeItemIdDictionary.Add(recipeName, list);
		}
		if (!PlayerFlagDataManager.recipeFlagDictionary.ContainsKey(recipeName))
		{
			PlayerFlagDataManager.recipeFlagDictionary.Add(recipeName, value: false);
		}
	}

	private void SetRewardCraftItemIdDictionary(string recipeName, int itemID)
	{
		if (!PlayerFlagDataManager.recipeFlagDictionary.ContainsKey(recipeName))
		{
			PlayerFlagDataManager.recipeFlagDictionary.Add(recipeName, value: false);
		}
	}
}
