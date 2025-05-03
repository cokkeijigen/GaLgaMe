using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckStatusFactorCustomToggle : StateBehaviour
{
	private StatusCustomManager statusCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
	}

	public override void OnStateBegin()
	{
		if (statusCustomManager.tempIsEquip)
		{
			if (statusCustomManager.tempEquipFactorList.FindIndex((HaveFactorData data) => data.factorID == statusCustomManager.clickFactorId && data.uniqueID == statusCustomManager.clickFactorUniqueId) == -1)
			{
				statusCustomManager.tempEquipFactorList.Add(statusCustomManager.tempHaveFactorData);
				statusCustomManager.skillSolotNumArray[0]++;
			}
		}
		else
		{
			int index = statusCustomManager.tempEquipFactorList.FindIndex((HaveFactorData data) => data.factorID == statusCustomManager.clickFactorId && data.uniqueID == statusCustomManager.clickFactorUniqueId);
			statusCustomManager.tempEquipFactorList.RemoveAt(index);
			statusCustomManager.skillSolotNumArray[0]--;
		}
		statusCustomManager.SetCustomApplyButtonCanvasGroup();
		statusCustomManager.SetCustomSlotNumText();
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
