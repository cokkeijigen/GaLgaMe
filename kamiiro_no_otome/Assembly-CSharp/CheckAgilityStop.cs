using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAgilityStop : StateBehaviour
{
	private DungeonCharacterAgility dungeonCharacterAgility;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonCharacterAgility = GetComponent<DungeonCharacterAgility>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!dungeonCharacterAgility.isCoroutineStop)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
