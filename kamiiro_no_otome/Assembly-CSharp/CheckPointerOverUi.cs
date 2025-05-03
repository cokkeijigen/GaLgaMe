using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("")]
public class CheckPointerOverUi : StateBehaviour
{
	private PointerEventData pointerEventData;

	public bool isPointerOverUi;

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
		pointerEventData = new PointerEventData(EventSystem.current);
		List<RaycastResult> list = new List<RaycastResult>();
		pointerEventData.position = Input.mousePosition;
		EventSystem.current.RaycastAll(pointerEventData, list);
		foreach (RaycastResult item in list)
		{
			isPointerOverUi = item.gameObject.CompareTag("UiButton");
			if (isPointerOverUi)
			{
				break;
			}
		}
		if (EventSystem.current.IsPointerOverGameObject() && isPointerOverUi)
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
