using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class TestTutorialFlagBool : StateBehaviour
{
	public string checkFlagName;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerFlagDataManager.tutorialFlagDictionary[checkFlagName])
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
