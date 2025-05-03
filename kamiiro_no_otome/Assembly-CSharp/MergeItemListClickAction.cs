using System;
using UnityEngine;

public class MergeItemListClickAction : MonoBehaviour
{
	[NonSerialized]
	public int itemID;

	private void Start()
	{
	}

	public void SendItemListIndexToCraftManager()
	{
		CraftManager component = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		if (component != null)
		{
			component.clickedItemID = itemID;
			component.selectScrollContentIndex = base.transform.GetSiblingIndex();
			component.PushSelectScrollButton();
			Debug.Log("合成アイテムボタン押下：" + itemID);
		}
	}
}
