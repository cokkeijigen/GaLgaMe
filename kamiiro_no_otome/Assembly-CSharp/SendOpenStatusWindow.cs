using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendOpenStatusWindow : StateBehaviour
{
	public enum Type
	{
		item,
		skill,
		equip
	}

	private ArborFSM targetFsm;

	private string sendString;

	public Type type;

	public StateLink nextState;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		targetFsm = GameObject.Find("Status Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.item:
			sendString = "ItemVisible";
			break;
		case Type.skill:
			sendString = "SkillVisible";
			break;
		case Type.equip:
			sendString = "EquipVisible";
			break;
		}
		targetFsm.SendTrigger(sendString);
		Transition(nextState);
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
