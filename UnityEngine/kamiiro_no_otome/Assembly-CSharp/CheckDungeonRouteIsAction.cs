using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonRouteIsAction : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		if (dungeonMapManager.isDungeonRouteAction)
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
