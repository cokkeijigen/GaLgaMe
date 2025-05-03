using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckHaveEventItemAndAfterDay : StateBehaviour
{
	public string[] checkFlagNameArray;

	public int needAfterDay;

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
		bool flag2 = false;
		List<bool> list = new List<bool>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < checkFlagNameArray.Length; i++)
		{
			list.Add(PlayerFlagDataManager.keyItemFlagDictionary[checkFlagNameArray[i]]);
		}
		flag = list.All((bool x) => x);
		for (int j = 0; j < checkFlagNameArray.Length; j++)
		{
			list2.Add(PlayerFlagDataManager.eventStartingDayDictionary[checkFlagNameArray[j]]);
		}
		int num = list2.Max();
		flag2 = ((PlayerDataManager.currentTotalDay - num >= needAfterDay) ? true : false);
		if (flag && flag2)
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
