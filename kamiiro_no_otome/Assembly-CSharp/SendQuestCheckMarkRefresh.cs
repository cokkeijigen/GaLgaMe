using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendQuestCheckMarkRefresh : StateBehaviour
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
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
		case 1:
		case 2:
			GameObject.Find("Quest Check Manager").GetComponent<ArborFSM>().SendTrigger("RefreshEnableQuestList");
			break;
		case 3:
			GameObject.Find("Quest Check Manager").GetComponent<ArborFSM>().SendTrigger("RefreshEnableQuestList");
			break;
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
