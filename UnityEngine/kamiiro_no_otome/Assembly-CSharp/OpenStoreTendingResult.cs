using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenStoreTendingResult : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private CarriageStoreNoticeManager carriageStoreNoticeManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		carriageStoreNoticeManager = GameObject.Find("Store Tending Manager").GetComponent<CarriageStoreNoticeManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isOpencarriageStoreResult)
		{
			Debug.Log("売り上げ通知を表示する／売上個数：" + PlayerDataManager.carriageStoreTradeCount);
			carriageStoreNoticeManager.OpenCarriageStoreNotice();
			PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
			PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
			PlayerNonSaveDataManager.addTimeZoneNum = 4;
			PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
			GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
		}
		else if (PlayerDataManager.carriageStoreTradeCount > 0)
		{
			headerStatusManager.carriageStoreNoticeIconGo.SetActive(value: true);
		}
		else
		{
			headerStatusManager.carriageStoreNoticeIconGo.SetActive(value: false);
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
