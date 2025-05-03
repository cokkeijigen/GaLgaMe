using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class FinishTotalMapInitialize : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	private MapHeroineUnFollowManager mapHeroineUnFollowManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		mapHeroineUnFollowManager = GameObject.Find("Map Rest Manager").GetComponent<MapHeroineUnFollowManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.systemSaveEnable = true;
		PlayerNonSaveDataManager.isClockChangeEnable = true;
		PlayerNonSaveDataManager.isInitializeMapData = false;
		PlayerNonSaveDataManager.isInterruptedAfterSave = false;
		PlayerNonSaveDataManager.isScenarioBattle = false;
		PlayerNonSaveDataManager.isMoveToDungeonBattle = false;
		PlayerNonSaveDataManager.isRetreatFromBattle = false;
		PlayerNonSaveDataManager.isDungeonBossBattle = false;
		PlayerNonSaveDataManager.isDungeonNoRetryBossBattle = false;
		PlayerNonSaveDataManager.isInDoorExitLock = false;
		PlayerNonSaveDataManager.isRefreshLocalMap = false;
		PlayerNonSaveDataManager.isUtageToWorldMapInDoor = false;
		PlayerNonSaveDataManager.isWorldMapToInDoor = false;
		mapHeroineUnFollowManager.RefreshUnFollowButtonVisible();
		worldMapAccessManager.SetWorldMapCanvasInteractable(value: true);
		totalMapAccessManager.mapCanvasGroupArray[2].interactable = true;
		totalMapAccessManager.mapCanvasGroupArray[2].alpha = 1f;
		if (PlayerNonSaveDataManager.isHeroineSpecifyFollowLocalMapNotice)
		{
			headerStatusManager.headerFSM.SendTrigger("HeaderStatusRefresh");
			totalMapAccessManager.SetLocalMapCanvasInteractable(value: false);
			Debug.Log("指定同行の通知を表示");
			GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("OpenNotice");
		}
		else
		{
			totalMapAccessManager.SetLocalMapCanvasInteractable(value: true);
		}
		Transition(stateLink);
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
}
