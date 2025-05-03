using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckCharacterFrameSetType : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public StateLink targetAliveLink;

	public StateLink targetDeadLink;

	public StateLink targetSelfLink;

	public StateLink scrollSelectLink;

	public StateLink resetLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		switch (scenarioBattleTurnManager.setFrameTypeName)
		{
		case "alive":
			Transition(targetAliveLink);
			break;
		case "dead":
			Transition(targetDeadLink);
			break;
		case "self":
			Transition(targetSelfLink);
			break;
		case "select":
			Transition(scrollSelectLink);
			break;
		case "reset":
			Transition(resetLink);
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
