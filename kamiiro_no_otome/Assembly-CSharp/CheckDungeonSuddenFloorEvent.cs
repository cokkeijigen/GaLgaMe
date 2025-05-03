using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonSuddenFloorEvent : StateBehaviour
{
	public StateLink eventLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isDungeonSuddenFloorEvent)
		{
			PlayerNonSaveDataManager.isDungeonSuddenFloorEvent = false;
			Transition(eventLink);
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
