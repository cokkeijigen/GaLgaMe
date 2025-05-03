using Arbor;
using UnityEngine;

public class DungeonGetItemListClickAction : MonoBehaviour
{
	private DungeonGetItemManager dungeonGetItemManager;

	public void SendItemListIndexToGetItemManager()
	{
		dungeonGetItemManager = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
		ParameterContainer component = GetComponent<ParameterContainer>();
		dungeonGetItemManager.selectItemID = component.GetInt("itemID");
		dungeonGetItemManager.selectItemSiblingIndex = base.transform.GetSiblingIndex();
		GameObject.Find("Dungeon Get Item Manager").GetComponent<ArborFSM>().SendTrigger("PushScrollContent");
		Debug.Log("スクロールボタン押下：" + component.GetInt("itemID"));
	}
}
