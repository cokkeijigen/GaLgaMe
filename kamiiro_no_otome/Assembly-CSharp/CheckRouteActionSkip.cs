using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckRouteActionSkip : StateBehaviour
{
	private PointerEventData pointer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		pointer = new PointerEventData(EventSystem.current);
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			List<RaycastResult> list = new List<RaycastResult>();
			pointer.position = Input.mousePosition;
			EventSystem.current.RaycastAll(pointer, list);
			bool flag = CheckClickIsButton(list);
			Debug.Log("ボタンをクリックした：" + flag);
			if (!flag)
			{
				DoSkip();
			}
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private bool CheckClickIsButton(List<RaycastResult> results)
	{
		bool result = false;
		for (int i = 0; i < results.Count(); i++)
		{
			if (results[i].gameObject.GetComponent<Button>() != null)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private void DoSkip()
	{
		Debug.Log("ルートアクションをスキップする");
		Transition(stateLink);
	}
}
