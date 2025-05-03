using Arbor;
using UnityEngine;

public class StatusSexSkillListClickAction : MonoBehaviour
{
	private ArborFSM scriptFSM;

	private SexStatusManager sexStatusManager;

	public void SendItemListIndexToStatusManager()
	{
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
		sexStatusManager.selectSexSkillScrollContentIndex = base.transform.GetSiblingIndex();
		scriptFSM = sexStatusManager.sexStatusFSM;
		int @int = GetComponent<ParameterContainer>().GetInt("skillID");
		sexStatusManager.selectSexSkillId = @int;
		scriptFSM.SendTrigger("SendSexSkillListIndex");
	}
}
