using UnityEngine;

public class HelpListClickAction : MonoBehaviour
{
	private HelpDataManager helpDataManager;

	public int sortID;

	public void SendItemListIndexToManager()
	{
		helpDataManager = GameObject.Find("Help Data Manager").GetComponent<HelpDataManager>();
		helpDataManager.selectScrollContentIndex = base.transform.GetSiblingIndex();
		helpDataManager.SetSelectHelpData(sortID);
		helpDataManager.helpFSM.SendTrigger("SendItemListIndex");
	}
}
