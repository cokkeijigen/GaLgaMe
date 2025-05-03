using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SelectLocalAreaVisible : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	[SerializeField]
	private int visiblePlaceCount;

	private List<GameObject> localMapPlaceGoList = new List<GameObject>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		visiblePlaceCount = 0;
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		if (PlayerDataManager.isLocalMapActionLimit)
		{
			totalMapAccessManager.restShortCutButtonGo.GetComponent<CanvasGroup>().interactable = false;
			totalMapAccessManager.restShortCutButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
		}
		else if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			totalMapAccessManager.restShortCutButtonGo.GetComponent<CanvasGroup>().interactable = false;
			totalMapAccessManager.restShortCutButtonGo.GetComponent<CanvasGroup>().alpha = 0.5f;
		}
		else
		{
			totalMapAccessManager.restShortCutButtonGo.GetComponent<CanvasGroup>().interactable = true;
			totalMapAccessManager.restShortCutButtonGo.GetComponent<CanvasGroup>().alpha = 1f;
		}
		foreach (Transform item in totalMapAccessManager.localAreaParentGo.transform)
		{
			item.gameObject.SetActive(value: false);
		}
		GameObject gameObject = totalMapAccessManager.localAreaParentGo.transform.Find(PlayerDataManager.currentAccessPointName).gameObject;
		gameObject.SetActive(value: true);
		Debug.Log("表示するアクセスポイント名：" + gameObject.name);
		localMapAccessManager.isLocalMapPlaceInitialized.Clear();
		localMapPlaceGoList = gameObject.GetComponent<ParameterContainer>().GetGameObjectList("localMapPlaceList").ToList();
		List<GameObject> list = gameObject.GetComponent<ParameterContainer>().GetGameObjectList("localMapButtonList").ToList();
		foreach (GameObject localMapPlaceGo in localMapPlaceGoList)
		{
			localMapPlaceGo.SetActive(value: true);
		}
		List<LocalMapUnlockData> whereList = localMapAccessManager.localMapUnlockDataBase.localMapUnlockDataList.Where((LocalMapUnlockData data) => data.worldCityName == PlayerDataManager.currentAccessPointName).ToList();
		int i;
		for (i = 0; i < localMapPlaceGoList.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < whereList[i].needFlagNameList.Count; j++)
			{
				string text = whereList[i].needFlagNameList[j];
				Debug.Log("場所名：" + whereList[i].currentPlaceName + "／チェックするフラグ名：" + text);
				flag = PlayerFlagDataManager.scenarioFlagDictionary[text];
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				if (whereList[i].needFlagQuestId != int.MaxValue)
				{
					QuestClearData questClearData = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == whereList[i].needFlagQuestId);
					if (questClearData != null && questClearData.isClear)
					{
						visiblePlaceCount++;
					}
					else
					{
						gameObject.transform.Find(whereList[i].currentPlaceName).gameObject.SetActive(value: false);
					}
				}
				else
				{
					gameObject.transform.Find(whereList[i].currentPlaceName).gameObject.SetActive(value: false);
				}
			}
			else
			{
				visiblePlaceCount++;
			}
		}
		if (!PlayerNonSaveDataManager.isInitializeMapData)
		{
			PlayerDataManager.KingdomMobHeroineVisibleDictionary.Clear();
			PlayerDataManager.CityMobHeroineVisibleDictionary.Clear();
			PlayerDataManager.KingdomMobCheckTimeDictionary.Clear();
			PlayerDataManager.CityMobCheckTimeDictionary.Clear();
			if (!PlayerNonSaveDataManager.isUtageToLocalMap && !PlayerNonSaveDataManager.isSexEnd && PlayerDataManager.mapPlaceStatusNum != 2 && !PlayerNonSaveDataManager.isRefreshLocalMap)
			{
				PlayerDataManager.currentPlaceName = "";
			}
		}
		else
		{
			Debug.Log("ロード後");
			if (PlayerDataManager.currentAccessPointName == "Kingdom1")
			{
				Debug.Log("ロード後／ウェンディ：辞書カウント：" + PlayerDataManager.KingdomMobHeroineVisibleDictionary.Count);
				foreach (KeyValuePair<string, bool> item2 in PlayerDataManager.KingdomMobHeroineVisibleDictionary)
				{
					GameObject.Find(item2.Key).GetComponent<ParameterContainer>().SetBool("isMobHeroineVisible", item2.Value);
					Debug.Log("モブヒロインの在否／" + item2.Key + "：" + item2.Value);
				}
				foreach (KeyValuePair<string, int> item3 in PlayerDataManager.KingdomMobCheckTimeDictionary)
				{
					GameObject.Find(item3.Key).GetComponent<ParameterContainer>().SetInt("mobHeroineVisibleCheckedTimeCount", item3.Value);
				}
			}
			else
			{
				foreach (KeyValuePair<string, bool> item4 in PlayerDataManager.CityMobHeroineVisibleDictionary)
				{
					GameObject.Find(item4.Key).GetComponent<ParameterContainer>().SetBool("isMobHeroineVisible", item4.Value);
					Debug.Log("モブヒロインの在否／" + item4.Key + "：" + item4.Value);
				}
				foreach (KeyValuePair<string, int> item5 in PlayerDataManager.CityMobCheckTimeDictionary)
				{
					GameObject.Find(item5.Key).GetComponent<ParameterContainer>().SetInt("mobHeroineVisibleCheckedTimeCount", item5.Value);
				}
			}
		}
		foreach (GameObject item6 in list)
		{
			item6.GetComponent<ArborFSM>().SendTrigger("ResetLocalMapPoint");
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (localMapAccessManager.isLocalMapPlaceInitialized.All((bool value) => value) && localMapAccessManager.isLocalMapPlaceInitialized.Count == visiblePlaceCount)
		{
			totalMapAccessManager.mapGroupArray[0].SetActive(value: false);
			PlayerNonSaveDataManager.isUtageToLocalMap = false;
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
