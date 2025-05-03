using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckFlagWhenMoveMapForDungeonSexEvent : StateBehaviour
{
	public string checkStartFlagName;

	public string checkEndFlagName;

	public StateLink sexEventLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerFlagDataManager.scenarioFlagDictionary[checkEndFlagName])
		{
			Transition(stateLink);
		}
		else if (PlayerFlagDataManager.scenarioFlagDictionary[checkStartFlagName])
		{
			PlayerNonSaveDataManager.isDungeonSexEvent = true;
			Debug.Log("ダンジョンのイチャイチャフラグ：" + PlayerNonSaveDataManager.isDungeonSexEvent);
			Transition(sexEventLink);
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
