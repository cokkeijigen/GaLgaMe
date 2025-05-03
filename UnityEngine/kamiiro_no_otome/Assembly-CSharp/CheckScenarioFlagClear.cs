using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckScenarioFlagClear : StateBehaviour
{
	public enum Type
	{
		all,
		any
	}

	public string[] checkFlagNameArray;

	public Type type;

	public StateLink clearState;

	public StateLink notClearState;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		List<bool> list = new List<bool>();
		for (int i = 0; i < checkFlagNameArray.Length; i++)
		{
			list.Add(PlayerFlagDataManager.scenarioFlagDictionary[checkFlagNameArray[i]]);
		}
		switch (type)
		{
		case Type.all:
			flag = list.All((bool x) => x);
			break;
		case Type.any:
			flag = list.Any((bool x) => x);
			break;
		}
		if (flag)
		{
			Transition(clearState);
		}
		else
		{
			Transition(notClearState);
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
