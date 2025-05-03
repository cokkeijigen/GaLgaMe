using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckIsDungeonBattle : StateBehaviour
{
	public StateLink dungeonLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isSelectDungeon)
		{
			Transition(dungeonLink);
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
