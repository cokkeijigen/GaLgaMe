using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckBattleUiMaterialSetType : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public StateLink noneLink;

	public StateLink playerLink;

	public StateLink enemyLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		switch (scenarioBattleTurnManager.setUiMaterialTypeName)
		{
		case "none":
			Transition(noneLink);
			break;
		case "player":
			Transition(playerLink);
			break;
		case "enemy":
			Transition(enemyLink);
			break;
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
