using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorRightClickEnable : StateBehaviour
{
	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		if (PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock || PlayerNonSaveDataManager.isInDoorExitLock || GameObject.Find("Notice Canvas") != null || PlayerNonSaveDataManager.isRequiedUtageResume || !(GameObject.Find("InDoor Canvas") != null))
		{
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
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
