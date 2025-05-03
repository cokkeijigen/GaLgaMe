using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

public class StatusFactorCustomListClickAction : MonoBehaviour
{
	private StatusCustomManager statusCustomManager;

	public HaveFactorData factorData;

	public int factorId;

	public int factorUniqueId;

	public Toggle equipToggle;

	public bool isInitialized;

	public void SendItemCustomToggle()
	{
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
		if (isInitialized)
		{
			MasterAudio.PlaySound("SeTabSwitch", 1f, null, 0f, null, null);
			statusCustomManager.tempHaveFactorData = factorData;
			statusCustomManager.clickFactorId = factorId;
			statusCustomManager.clickFactorUniqueId = factorUniqueId;
			statusCustomManager.tempIsEquip = equipToggle.isOn;
			statusCustomManager.factorCsutomFSM.SendTrigger("PushCustomEquipToggle");
		}
	}
}
