using Arbor;
using UnityEngine;

public class StatusSkillListClickAction : MonoBehaviour
{
	private ArborFSM scriptFSM;

	private StatusManager statusManager;

	public void SendItemListIndexToStatusManager()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusManager.selectSkillScrollContentIndex = base.transform.GetSiblingIndex();
		scriptFSM = statusManager.skillFSM;
		int @int = GetComponent<ParameterContainer>().GetInt("skillID");
		statusManager.selectSkillId = @int;
		scriptFSM.SendTrigger("SendSkillListIndex");
	}
}
