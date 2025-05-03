using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ReStartDungeonMap : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		utageBattleSceneManager.battleCanvas.SetActive(value: false);
		dungeonMapManager.dungeonMapCanvas.SetActive(value: true);
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
