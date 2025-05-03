using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckUsedShopLocalMapRefresh : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isUsedShop)
		{
			PlayerNonSaveDataManager.isRefreshLocalMap = true;
			GameObject.Find("TotalMap Access Manager").GetComponent<ArborFSM>().SendTrigger("AfterHeroineUnFollow");
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
