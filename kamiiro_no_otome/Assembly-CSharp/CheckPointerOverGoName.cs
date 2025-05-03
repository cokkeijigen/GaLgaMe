using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("")]
public class CheckPointerOverGoName : StateBehaviour
{
	private PointerEventData pointerEventData;

	public string checkGoName;

	public bool isPointerOverCheckGo;

	public StateLink overLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		isPointerOverCheckGo = false;
		pointerEventData = new PointerEventData(EventSystem.current);
		List<RaycastResult> list = new List<RaycastResult>();
		pointerEventData.position = Input.mousePosition;
		EventSystem.current.RaycastAll(pointerEventData, list);
		foreach (RaycastResult item in list)
		{
			if (item.gameObject.name == checkGoName)
			{
				isPointerOverCheckGo = true;
				break;
			}
		}
		if (EventSystem.current.IsPointerOverGameObject() && isPointerOverCheckGo)
		{
			Transition(overLink);
		}
		else
		{
			Transition(stateLink);
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
