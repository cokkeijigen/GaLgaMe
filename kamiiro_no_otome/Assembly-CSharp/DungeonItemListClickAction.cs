using Arbor;
using UnityEngine;

public class DungeonItemListClickAction : MonoBehaviour
{
	private DungeonItemManager dungeonItemManager;

	public void SendItemListIndexToStatusManager()
	{
		dungeonItemManager = GameObject.Find("Dungeon Item Manager").GetComponent<DungeonItemManager>();
		dungeonItemManager.GetComponent<ArborFSM>().SendTrigger("SendItemListIndex");
	}
}
