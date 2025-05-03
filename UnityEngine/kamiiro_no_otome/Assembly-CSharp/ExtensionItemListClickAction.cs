using System;
using UnityEngine;

public class ExtensionItemListClickAction : MonoBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	[NonSerialized]
	public int itemID;

	[NonSerialized]
	public int instanceID;

	private void OnEnable()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
	}

	public void SendItemListIndex()
	{
		craftExtensionManager.clickedItemID = itemID;
		craftExtensionManager.clickedItemUniqueID = instanceID;
		craftExtensionManager.clickedScrollContentIndex = base.transform.GetSiblingIndex();
		craftExtensionManager.PushMpRecoveryScrollButton();
		Debug.Log("レシピボタン押下：" + itemID + "／インスタンスID：" + instanceID);
	}
}
