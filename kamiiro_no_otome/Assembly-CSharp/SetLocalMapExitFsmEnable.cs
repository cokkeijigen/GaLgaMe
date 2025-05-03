using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetLocalMapExitFsmEnable : StateBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	public bool isEnable;

	private bool isLocalMapActive;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		isLocalMapActive = false;
		GameObject gameObject = GameObject.Find("LocalMap Access Manager");
		if (gameObject != null)
		{
			isLocalMapActive = true;
			localMapAccessManager = gameObject.GetComponent<LocalMapAccessManager>();
		}
		if (isLocalMapActive)
		{
			localMapAccessManager.SetLocalMapExitEnable(isEnable);
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
