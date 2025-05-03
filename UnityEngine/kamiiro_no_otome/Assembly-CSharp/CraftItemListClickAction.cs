using System;
using Arbor;
using UnityEngine;

public class CraftItemListClickAction : MonoBehaviour
{
	public enum ModeType
	{
		craftRecipe,
		requiredOverlay
	}

	[NonSerialized]
	public int itemID;

	[NonSerialized]
	public int instanceID;

	[NonSerialized]
	public ModeType modeType;

	private void Start()
	{
	}

	public void SendItemListIndexToCraftManager()
	{
		CraftManager component = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		if (!(component != null))
		{
			return;
		}
		ArborFSM arborFSM = component.arborFSM;
		switch (modeType)
		{
		case ModeType.craftRecipe:
			component.clickedItemID = itemID;
			if (PlayerNonSaveDataManager.selectCraftCanvasName == "craft" || PlayerNonSaveDataManager.selectCraftCanvasName == "merge")
			{
				component.clickedUniqueID = instanceID;
			}
			else
			{
				component.clickedUniqueID = int.MaxValue;
			}
			component.selectScrollContentIndex = base.transform.GetSiblingIndex();
			component.PushSelectScrollButton();
			Debug.Log("レシピボタン押下：" + itemID + "／インスタンスID：" + component.clickedUniqueID);
			break;
		case ModeType.requiredOverlay:
			component.clickedUniqueID = itemID;
			component.selectScrollContentIndex = base.transform.GetSiblingIndex();
			arborFSM.SendTrigger("PushNeededSourceItemButton");
			Debug.Log("素材アイテムボタン押下：" + itemID);
			break;
		}
	}
}
