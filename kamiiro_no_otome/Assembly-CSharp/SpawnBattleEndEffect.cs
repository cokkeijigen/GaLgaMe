using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SpawnBattleEndEffect : StateBehaviour
{
	public enum Type
	{
		victory,
		defeat
	}

	private ScenarioBattleResultManager scenarioBattleResultManager;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleResultManager = GameObject.Find("Battle Result Manager").GetComponent<ScenarioBattleResultManager>();
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.victory:
			scenarioBattleResultManager.StartBattleResultEffect(isVictory: true);
			break;
		case Type.defeat:
			scenarioBattleResultManager.StartBattleResultEffect(isVictory: false);
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
