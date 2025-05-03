using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class StartMapRest : StateBehaviour
{
	private MapCampManager mapCampManager;

	private TotalMapAccessManager totalMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	private ClockTimeManagerForPM clockTimeManagerForPM;

	public float fadeTime;

	private bool isInitialized;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		mapCampManager = GameObject.Find("Map Camp Manager").GetComponent<MapCampManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		clockTimeManagerForPM = GameObject.Find("ClockTime Manager").GetComponent<ClockTimeManagerForPM>();
	}

	public override void OnStateBegin()
	{
		headerStatusManager.isWeekIconInitialize = false;
		isInitialized = false;
		MasterAudio.PlaySound("SeMapRest", 1f, null, 0f, null, null);
		MasterAudio.FadeAllPlaylistsToVolume(0f, 0.3f);
		mapCampManager.campBlackImage.DOFade(1f, fadeTime).OnComplete(FadeToBlackEnd);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
		if (!headerStatusManager.isWeekIconInitialize || isInitialized)
		{
			return;
		}
		isInitialized = true;
		if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			GameObject[] array = new GameObject[totalMapAccessManager.worldAreaParentGo.transform.childCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = totalMapAccessManager.worldAreaParentGo.transform.GetChild(i).gameObject;
			}
			GameObject[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].transform.GetComponentInChildren<ArborFSM>().SendTrigger("ResetWorldMapPoint");
			}
		}
		else
		{
			foreach (GameObject item in totalMapAccessManager.localAreaParentGo.transform.Find(PlayerDataManager.currentAccessPointName).gameObject.GetComponent<ParameterContainer>().GetGameObjectList("localMapButtonList").ToList())
			{
				item.GetComponent<ArborFSM>().SendTrigger("ResetLocalMapPoint");
			}
		}
		Transition(stateLink);
	}

	private void FadeToBlackEnd()
	{
		for (int i = 0; i < 5; i++)
		{
			PlayerStatusDataManager.characterHp[i] = PlayerStatusDataManager.characterMaxHp[i];
			PlayerStatusDataManager.characterMp[i] = PlayerStatusDataManager.characterMaxMp[i];
		}
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
		PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
		PlayerNonSaveDataManager.addTimeZoneNum = 1;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
		PlayerNonSaveDataManager.isAddTimeFromScenario = true;
		PlayerNonSaveDataManager.isAddTimeFromMapRest = true;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		headerStatusManager.headerFSM.SendTrigger("HeaderStatusRefresh");
	}
}
