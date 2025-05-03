using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckStatusSkillCustomToggle : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
	}

	public override void OnStateBegin()
	{
		if (statusCustomManager.tempIsEquip)
		{
			statusCustomManager.tempEquipSkillList.Add(statusManager.selectSkillId);
			statusCustomManager.skillSolotNumArray[0]++;
		}
		else
		{
			statusCustomManager.tempEquipSkillList.Remove(statusManager.selectSkillId);
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
