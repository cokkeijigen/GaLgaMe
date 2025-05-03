using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendPlayMakerEvent : StateBehaviour
{
	public bool isSelf;

	public string targetGoName;

	public string sendEventName;

	public StateLink shopRankCheckLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayMakerFSM playMakerFSM = null;
		playMakerFSM = ((!isSelf) ? GameObject.Find(targetGoName).GetComponent<PlayMakerFSM>() : GetComponent<PlayMakerFSM>());
		playMakerFSM.SendEvent(sendEventName);
		if (PlayerNonSaveDataManager.isCheckShopRankChange)
		{
			Transition(shopRankCheckLink);
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
