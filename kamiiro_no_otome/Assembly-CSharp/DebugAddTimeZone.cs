using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugAddTimeZone : StateBehaviour
{
	public enum Type
	{
		timeZone,
		timeBlock
	}

	public int addValue;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.timeZone:
			PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.totalTimeZoneCount;
			PlayerNonSaveDataManager.oldDayCount = PlayerDataManager.currentTotalDay;
			PlayerNonSaveDataManager.addTimeZoneNum = addValue;
			PlayerNonSaveDataManager.isRequiredCalcCarriageStore = false;
			GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
			break;
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
