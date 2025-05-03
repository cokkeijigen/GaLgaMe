using Arbor;
using UnityEngine;

public class QuestListClickAction : MonoBehaviour
{
	private ArborFSM questFSM;

	private QuestManager questManager;

	public int sortID;

	public bool isClearedQuest;

	public void SendItemListIndexToManager()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
		questManager.selectScrollContentIndex = base.transform.GetSiblingIndex();
		questFSM = questManager.GetComponent<ArborFSM>();
		questManager.clickedQuestID = sortID;
		questManager.isQuestCleared = isClearedQuest;
		questFSM.SendTrigger("SendItemListIndex");
	}
}
