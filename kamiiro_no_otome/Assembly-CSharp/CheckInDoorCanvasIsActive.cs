using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorCanvasIsActive : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GameObject gameObject = GameObject.Find("InDoor Canvas");
		if (gameObject.activeInHierarchy)
		{
			gameObject.SetActive(value: false);
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
