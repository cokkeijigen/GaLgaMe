using System;
using Arbor;
using UnityEngine;

public class MaterialItemListClickAction : MonoBehaviour
{
	public enum ModeType
	{
		addOnOverlay,
		wonderOverlay
	}

	private CraftAddOnManager craftAddOnManager;

	private ArborFSM addOnFSM;

	public int itemID;

	[NonSerialized]
	public ModeType modeType;

	private void Start()
	{
	}

	public void SendItemListIndexToCraftManager()
	{
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		addOnFSM = GameObject.Find("Craft AddOn Manager").GetComponent<ArborFSM>();
		if (addOnFSM != null)
		{
			switch (modeType)
			{
			case ModeType.addOnOverlay:
				Debug.Log("特殊な魔力片アイテムボタン押下：" + itemID);
				break;
			case ModeType.wonderOverlay:
				Debug.Log("不思議な金属アイテムボタン押下：" + itemID);
				break;
			}
			GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData m) => m.itemID == itemID);
			craftAddOnManager.selectScrollContentIndex = base.transform.GetSiblingIndex();
			craftAddOnManager.selectedMagicMaterialID_Temp = itemID;
			addOnFSM.SendTrigger("PushMagicMaterialItemButton");
		}
	}
}
