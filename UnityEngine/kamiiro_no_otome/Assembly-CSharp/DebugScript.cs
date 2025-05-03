using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugScript : StateBehaviour
{
	public enum Type
	{
		save,
		load,
		weaponSort
	}

	public Type type;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.save:
			ES3.Save("haveItemList", PlayerInventoryDataManager.haveItemList);
			break;
		case Type.load:
			PlayerInventoryDataManager.haveItemList = ES3.Load<List<HaveItemData>>("haveItemList");
			break;
		case Type.weaponSort:
			PlayerInventoryDataAccess.HaveItemListSortAll();
			break;
		}
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
