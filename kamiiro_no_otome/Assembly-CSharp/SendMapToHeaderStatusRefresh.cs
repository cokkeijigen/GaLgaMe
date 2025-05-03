using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendMapToHeaderStatusRefresh : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private bool isInitialized;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		headerStatusManager.isWeekIconInitialize = false;
		isInitialized = false;
		headerStatusManager.headerFSM.SendTrigger("HeaderStatusRefresh");
		headerStatusManager.questFSM.SendTrigger("RefreshEnableQuestList");
		headerStatusManager.headerFSM.SendTrigger("ResetPlacePanel");
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
		if (headerStatusManager.isWeekIconInitialize && !isInitialized)
		{
			isInitialized = true;
			Transition(stateLink);
		}
	}
}
