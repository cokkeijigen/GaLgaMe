using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugDataSlot : StateBehaviour
{
	public enum Type
	{
		input,
		output
	}

	public Type type;

	public InputSlotAny inputSlot;

	public OutputSlotAny outputSlot;

	public List<int> intList = new List<int>();

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
		case Type.input:
			intList.Clear();
			inputSlot.TryGetValue<List<int>>(out intList);
			Debug.Log(intList[0]);
			break;
		case Type.output:
			outputSlot.SetValue(intList);
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
