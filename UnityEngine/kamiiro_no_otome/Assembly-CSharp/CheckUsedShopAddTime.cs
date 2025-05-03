using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckUsedShopAddTime : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isUsedShop)
		{
			PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
			PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
			PlayerNonSaveDataManager.addTimeZoneNum = 1;
			PlayerNonSaveDataManager.isRequiredCalcCarriageStore = true;
			GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
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
