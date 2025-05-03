using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckNeedInDoorCommandTalkInitialize : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public StateLink noInitializeLink;

	public StateLink needInitializeLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		if (inDoorTalkManager.isInitializeCommandTalk)
		{
			if (inDoorTalkManager.commandTalkOriginGo == inDoorTalkManager.clickTargetGo)
			{
				Transition(noInitializeLink);
			}
			else
			{
				Transition(needInitializeLink);
			}
		}
		else
		{
			Transition(needInitializeLink);
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
