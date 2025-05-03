using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class StatusSkillCustomListInitialize : StateBehaviour
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
		int num = 0;
		if (statusManager.selectCharacterNum == 0)
		{
			if (statusManager.selectItemId < 1300)
			{
				List<int> tempEquipSkillList = PlayerEquipDataManager.playerEquipSkillList[0];
				statusCustomManager.tempEquipSkillList = tempEquipSkillList;
			}
			else
			{
				int num2 = PlayerStatusDataManager.partyMemberCount - 1;
				List<int> tempEquipSkillList2 = PlayerEquipDataManager.playerEquipSkillList[num2];
				statusCustomManager.tempEquipSkillList = tempEquipSkillList2;
			}
		}
		else
		{
			List<int> tempEquipSkillList3 = PlayerEquipDataManager.playerEquipSkillList[statusManager.selectCharacterNum];
			statusCustomManager.tempEquipSkillList = tempEquipSkillList3;
		}
		statusCustomManager.skillSolotNumArray[0] = statusCustomManager.tempEquipSkillList.Count();
		statusCustomManager.skillSolotNumArray[1] = num;
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
