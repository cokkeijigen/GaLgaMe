using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GetStaticInt : StateBehaviour
{
	public enum Type
	{
		playerHaveMoney,
		playerLibido
	}

	public Type type;

	public OutputSlotInt outputInt;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.playerHaveMoney:
			outputInt.SetValue(PlayerDataManager.playerHaveMoney);
			break;
		case Type.playerLibido:
			outputInt.SetValue(PlayerDataManager.playerLibido);
			break;
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
