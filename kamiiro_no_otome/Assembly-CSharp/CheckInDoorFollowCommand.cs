using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorFollowCommand : StateBehaviour
{
	private InDoorCommandManager inDoorCommandManager;

	public StateLink selectLink;

	public StateLink noticeLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		if (inDoorCommandManager.dialogType == InDoorCommandManager.DialogType.select)
		{
			Transition(selectLink);
		}
		else
		{
			Transition(noticeLink);
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
