using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetInDoorExitFsmEnable : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public bool isEnable;

	private bool isInDoorActive;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		isInDoorActive = false;
		GameObject gameObject = GameObject.Find("InDoor Talk Manager");
		if (gameObject != null)
		{
			isInDoorActive = true;
			inDoorTalkManager = gameObject.GetComponent<InDoorTalkManager>();
		}
		if (isInDoorActive)
		{
			inDoorTalkManager.SetExitButtonFsmEnable(isEnable);
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
