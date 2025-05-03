using Arbor;
using UnityEngine;

public class StatusPartyListClickAction : MonoBehaviour
{
	public int EquipCharacterNum = 9;

	public int itemID;

	private StatusManager statusManager;

	private ArborFSM scriptFSM;

	public void SendCharacterNumToStatusManager()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusManager.selectItemScrollContentIndex = base.transform.GetSiblingIndex();
		if (EquipCharacterNum != 9)
		{
			statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
			scriptFSM = statusManager.statusFSM;
			statusManager.selectCharacterNum = EquipCharacterNum;
			statusManager.namePanelFSM.FsmVariables.GetFsmInt("characterNum").Value = EquipCharacterNum;
			statusManager.selectItemId = itemID;
			scriptFSM.SendTrigger("SendItemListIndex");
		}
	}
}
