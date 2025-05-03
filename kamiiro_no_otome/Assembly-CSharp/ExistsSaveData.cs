using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ExistsSaveData : StateBehaviour
{
	private int selectSlotNum;

	public StateLink existsSaveData;

	public StateLink notSaveData;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.selectSystemTabName == "load")
		{
			selectSlotNum = PlayerNonSaveDataManager.selectSlotPageNum * 6 + PlayerNonSaveDataManager.selectSlotNum + 1;
			if (ES3.KeyExists("saveDayTime" + selectSlotNum))
			{
				Transition(existsSaveData);
			}
			else
			{
				Transition(notSaveData);
			}
		}
		else
		{
			Transition(existsSaveData);
		}
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
