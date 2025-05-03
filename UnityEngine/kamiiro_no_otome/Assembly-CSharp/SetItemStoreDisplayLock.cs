using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class SetItemStoreDisplayLock : StateBehaviour
{
	private CarriageManager carriageManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
	}

	public override void OnStateBegin()
	{
		int clickedItemID = carriageManager.clickedItemID;
		int clickedUniqueID = carriageManager.clickedUniqueID;
		Debug.Log("アイテムのロック／ID：" + clickedItemID + "／ユニークID：" + clickedUniqueID + "／ロック設定：" + carriageManager.isSelectItemLock);
		if (carriageManager.isSelectItemLock)
		{
			PlayerInventoryDataEquipAccess.SetItemStoreDisplayLock(clickedItemID, clickedUniqueID, setValue: false);
			MasterAudio.PlaySound("SeItemUnLock", 1f, null, 0f, null, null);
		}
		else
		{
			PlayerInventoryDataEquipAccess.SetItemStoreDisplayLock(clickedItemID, clickedUniqueID, setValue: true);
			MasterAudio.PlaySound("SeItemLock", 1f, null, 0f, null, null);
		}
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
