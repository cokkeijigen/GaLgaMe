using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckCarriageNotice : StateBehaviour
{
	private CarriageStoreNoticeManager carriageStoreNoticeManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageStoreNoticeManager = GameObject.Find("Store Tending Manager").GetComponent<CarriageStoreNoticeManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.carriageStoreTradeMoneyNum > 0)
		{
			carriageStoreNoticeManager.StartCarriageStoreNotice();
		}
		else
		{
			Transition(stateLink);
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
