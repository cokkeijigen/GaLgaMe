using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckStoreStartButtonInteractable : StateBehaviour
{
	private CarriageManager carriageManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
	}

	public override void OnStateBegin()
	{
		if (!carriageManager.isItemInStore)
		{
			carriageManager.startButtonAlertFrameGo.SetActive(value: true);
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
