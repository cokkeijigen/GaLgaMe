using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckCarriageNoticeWindowInteractable : StateBehaviour
{
	private CarriageStoreNoticeManager carriageStoreNoticeManager;

	public StateLink notInteractableLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageStoreNoticeManager = GameObject.Find("Store Tending Manager").GetComponent<CarriageStoreNoticeManager>();
	}

	public override void OnStateBegin()
	{
		if (carriageStoreNoticeManager.GetCarriageStoreNoticeButtonInteractable())
		{
			Transition(stateLink);
		}
		else
		{
			Transition(notInteractableLink);
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
