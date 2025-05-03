using System.Collections.Generic;
using Arbor;
using DG.Tweening;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class LocalMapAccessManager : SerializedMonoBehaviour
{
	public ArborFSM headerStatusManagerFSM;

	public ArborFSM localMapExitFSM;

	public LocalMapPlaceDataBase localMapPlaceDataBase;

	public LocalMapUnlockDataBase localMapUnlockDataBase;

	public GameObject newCapterDialogGo;

	public Localize newChapterHeaderTextLoc;

	public Localize newChapterMainTextLoc;

	public GameObject localAreaParentGo;

	public Dictionary<string, GameObject> localMapPlaceGoDictionary = new Dictionary<string, GameObject>();

	public Sprite[] heroineSpriteBalloonArray;

	public GameObject localExitButtonBalloonGo;

	public Image localExitButtonImage;

	public Image[] kingdom1BgImageArray;

	public Image[] city1BgImageArray;

	public Sprite[] kingdom1TimeZoneSpriteArray;

	public Sprite[] city1TimeZoneSpriteArray;

	public List<bool> isLocalMapPlaceInitialized = new List<bool>();

	private void Awake()
	{
		kingdom1BgImageArray[1].color = new Color(1f, 1f, 1f, 0f);
		city1BgImageArray[1].color = new Color(1f, 1f, 1f, 0f);
		foreach (KeyValuePair<string, GameObject> item in localMapPlaceGoDictionary)
		{
			item.Value.SetActive(value: false);
		}
	}

	public bool ExistLocalMapPlace(string accessPointName)
	{
		LocalMapPlaceData localMapPlaceData = localMapPlaceDataBase.localMapPlaceDataList.Find((LocalMapPlaceData data) => data.currentPlaceName == accessPointName);
		bool result = false;
		if (localMapPlaceData != null)
		{
			result = true;
		}
		return result;
	}

	public void SetLocalMapExitEnable(bool isEnable)
	{
		localMapExitFSM.enabled = isEnable;
	}

	public void AddLocalMapPlaceInitialize()
	{
		isLocalMapPlaceInitialized.Add(item: true);
	}

	public bool GetScnearioClearFlag(string scenarioName)
	{
		return PlayerFlagDataManager.scenarioFlagDictionary[scenarioName];
	}

	public int GetCurrentTimeZone()
	{
		return PlayerDataManager.currentTimeZone;
	}

	public bool CheckHeroineIsFollow()
	{
		return PlayerDataManager.isDungeonHeroineFollow;
	}

	public int GetFollowHeroineID()
	{
		return PlayerDataManager.DungeonHeroineFollowNum;
	}

	public void SetHeroineExistParameter(bool value, GameObject gameObject)
	{
		gameObject.transform.parent.GetComponent<ParameterContainer>().SetBool("isHeroineExist", value);
	}

	public void SetLocalMapTimeZoneImage()
	{
		string currentAccessPointName = PlayerDataManager.currentAccessPointName;
		if (!(currentAccessPointName == "Kingdom1"))
		{
			if (currentAccessPointName == "City1")
			{
				city1BgImageArray[1].sprite = city1BgImageArray[0].sprite;
				city1BgImageArray[1].color = Color.white;
				city1BgImageArray[0].sprite = city1TimeZoneSpriteArray[PlayerDataManager.currentTimeZone];
				city1BgImageArray[1].DOFade(0f, 0.2f);
			}
		}
		else
		{
			kingdom1BgImageArray[1].sprite = kingdom1BgImageArray[0].sprite;
			kingdom1BgImageArray[1].color = Color.white;
			kingdom1BgImageArray[0].sprite = kingdom1TimeZoneSpriteArray[PlayerDataManager.currentTimeZone];
			kingdom1BgImageArray[1].DOFade(0f, 0.2f);
		}
		Debug.Log("ローカルマップの時間帯の画像を設定する");
	}
}
