using UnityEngine;

public class CarriageItemListClickAction : MonoBehaviour
{
	public int itemID;

	public int instanceID;

	public bool isStoreDisplay;

	private CarriageManager carriageManager;

	public void SendItemListIndexToManager()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
		carriageManager.selectScrollContentIndex = base.transform.GetSiblingIndex();
		carriageManager.clickedItemID = itemID;
		carriageManager.clickedUniqueID = instanceID;
		carriageManager.arborFSM.SendTrigger("SendCarriageItemListIndex");
	}

	public void SendDiplayItemForSale()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
		carriageManager.sellScrollContentIndex = base.transform.GetSiblingIndex();
		carriageManager.sellClickedItemID = itemID;
		carriageManager.sellClickedUniqueID = instanceID;
		carriageManager.SetDiplayItemForSale();
	}
}
