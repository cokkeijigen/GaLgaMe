using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAccessPointInLocalMapCity : StateBehaviour
{
	public StateLink ContainLink;

	public StateLink NotContainLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.currentAccessPointName == "Kingdom1" || PlayerDataManager.currentAccessPointName == "City1")
		{
			Transition(ContainLink);
		}
		else
		{
			Transition(NotContainLink);
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
