using Arbor;
using UnityEngine;

public class ShopItemListClickAction : MonoBehaviour
{
	private ArborFSM scriptFSM;

	private ShopManager shopManager;

	public int itemID;

	public int instanceID;

	public void SendItemListIndexToManager()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
		shopManager.clickedItemIndex = base.transform.GetSiblingIndex();
		scriptFSM = shopManager.shopFSM;
		shopManager.clickedItemID = itemID;
		shopManager.clickedItemUniqueID = instanceID;
		scriptFSM.SendTrigger("SendItemListIndex");
	}
}
