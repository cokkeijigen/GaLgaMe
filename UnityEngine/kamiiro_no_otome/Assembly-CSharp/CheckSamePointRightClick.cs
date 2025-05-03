using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSamePointRightClick : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	public StateLink samePointClick;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
	}

	public override void OnStateBegin()
	{
		if (!string.IsNullOrEmpty(sexTouchManager.beforeSelectAreaPointName))
		{
			if (sexTouchManager.clickSelectAreaPointName == sexTouchManager.beforeSelectAreaPointName && sexTouchManager.clickSelectAreaPointIndex == sexTouchManager.beforeSelectAreaPointIndex)
			{
				sexTouchManager.sexTouchCancelManagerFSM.SendEvent("PushCloseButton");
				sexTouchManager.beforeSelectAreaPointName = "";
				Transition(samePointClick);
			}
			else
			{
				sexTouchManager.beforeSelectAreaPointName = sexTouchManager.clickSelectAreaPointName;
				sexTouchManager.beforeSelectAreaPointIndex = sexTouchManager.clickSelectAreaPointIndex;
				Transition(stateLink);
			}
		}
		else
		{
			sexTouchManager.beforeSelectAreaPointName = sexTouchManager.clickSelectAreaPointName;
			sexTouchManager.beforeSelectAreaPointIndex = sexTouchManager.clickSelectAreaPointIndex;
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
