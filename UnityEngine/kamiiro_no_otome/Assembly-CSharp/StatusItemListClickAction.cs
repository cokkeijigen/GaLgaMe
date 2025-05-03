using Arbor;
using UnityEngine;

public class StatusItemListClickAction : MonoBehaviour
{
	public int itemID;

	public int instanceID;

	private ArborFSM scriptFSM;

	private StatusManager statusManager;

	public void SendItemListIndexToStatusManager()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusManager.selectItemScrollContentIndex = base.transform.GetSiblingIndex();
		scriptFSM = statusManager.statusFSM;
		statusManager.selectItemId = itemID;
		statusManager.selectItemUniqueId = instanceID;
		scriptFSM.SendTrigger("SendItemListIndex");
	}
}
