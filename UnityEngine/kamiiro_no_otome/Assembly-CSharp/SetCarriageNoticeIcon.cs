using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetCarriageNoticeIcon : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.carriageStoreTradeCount > 0)
		{
			headerStatusManager.carriageStoreNoticeIconGo.SetActive(value: true);
		}
		else
		{
			headerStatusManager.carriageStoreNoticeIconGo.SetActive(value: false);
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
