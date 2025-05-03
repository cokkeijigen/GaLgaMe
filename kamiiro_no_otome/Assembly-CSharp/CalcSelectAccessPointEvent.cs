using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcSelectAccessPointEvent : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public OutputSlotAny outputSlot;

	public StateLink calcEventLink;

	public StateLink noEventLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		Transform transform = totalMapAccessManager.worldAreaParentGo.transform.Find(PlayerNonSaveDataManager.selectAccessPointName);
		ParameterContainer component = transform.GetComponent<ParameterContainer>();
		string @string = component.GetString("scenarioName");
		int childCount = totalMapAccessManager.worldAreaParentGo.transform.childCount;
		List<string> list = new List<string>();
		for (int i = 0; i < childCount; i++)
		{
			list.Add(totalMapAccessManager.worldAreaParentGo.transform.GetChild(i).gameObject.name);
		}
		if (!list.Contains(@string))
		{
			component.SetBool("isInitialize", value: false);
			outputSlot.SetValue(component);
			transform.transform.GetComponentInChildren<ArborFSM>().SendTrigger("ResetWorldMapPoint");
			Transition(calcEventLink);
		}
		else
		{
			Transition(noEventLink);
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
