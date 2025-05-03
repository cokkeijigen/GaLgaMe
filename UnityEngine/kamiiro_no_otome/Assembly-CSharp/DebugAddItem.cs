using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugAddItem : StateBehaviour
{
	public int addItemId;

	public int addItemCount;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		int itemSortID = PlayerInventoryDataAccess.GetItemSortID(addItemId);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(addItemId, itemSortID, addItemCount);
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
