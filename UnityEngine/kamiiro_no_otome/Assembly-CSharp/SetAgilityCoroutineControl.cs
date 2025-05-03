using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetAgilityCoroutineControl : StateBehaviour
{
	public enum Type
	{
		stop,
		restart
	}

	private DungeonBattleManager dungeonBattleManager;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		if (type == Type.stop)
		{
			dungeonBattleManager.StopAglityCoroutine();
		}
		else
		{
			dungeonBattleManager.RestartAglityCoroutine();
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
