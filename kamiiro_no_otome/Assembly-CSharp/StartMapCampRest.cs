using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class StartMapCampRest : StateBehaviour
{
	private MapCampManager mapCampManager;

	private TotalMapAccessManager totalMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	public float fadeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		mapCampManager = GameObject.Find("Map Camp Manager").GetComponent<MapCampManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		MasterAudio.PlaySound("SeRest", 1f, null, 0f, null, null);
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
	}

	private void FadeToBlackEnd()
	{
		for (int i = 0; i < 5; i++)
		{
			PlayerStatusDataManager.characterHp[i] = PlayerStatusDataManager.characterMaxHp[i];
			PlayerStatusDataManager.characterMp[i] = PlayerStatusDataManager.characterMaxMp[i];
		}
		headerStatusManager.headerFSM.SendTrigger("HeaderStatusRefresh");
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
		PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
		PlayerNonSaveDataManager.addTimeZoneNum = PlayerNonSaveDataManager.needCampTimeCount;
		PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
		GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		GameObject[] array = new GameObject[totalMapAccessManager.worldAreaParentGo.transform.childCount];
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = totalMapAccessManager.worldAreaParentGo.transform.GetChild(j).gameObject;
		}
		GameObject[] array2 = array;
		for (int k = 0; k < array2.Length; k++)
		{
			array2[k].transform.GetComponentInChildren<ArborFSM>().SendTrigger("ResetWorldMapPoint");
		}
		Transition(stateLink);
	}
}
