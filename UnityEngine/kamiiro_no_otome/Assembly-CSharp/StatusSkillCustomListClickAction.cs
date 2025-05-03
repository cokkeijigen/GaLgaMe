using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

public class StatusSkillCustomListClickAction : MonoBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	public Toggle equipToggle;

	public bool isInitialized;

	public void SendItemListIndexToStatusManager()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
		statusCustomManager.customScrollContentIndex = base.transform.GetSiblingIndex();
		int @int = GetComponent<ParameterContainer>().GetInt("skillID");
		statusManager.selectSkillId = @int;
		statusCustomManager.skillCustomFSM.SendTrigger("SendCustomListIndex");
	}

	public void SendItemCustomToggle()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
		if (isInitialized)
		{
			MasterAudio.PlaySound("SeTabSwitch", 1f, null, 0f, null, null);
			int @int = GetComponent<ParameterContainer>().GetInt("skillID");
			statusManager.selectSkillId = @int;
			statusCustomManager.tempIsEquip = equipToggle.isOn;
			statusCustomManager.skillCustomFSM.SendTrigger("PushCustomEquipToggle");
		}
	}
}
