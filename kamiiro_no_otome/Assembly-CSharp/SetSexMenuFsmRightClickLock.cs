using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexMenuFsmRightClickLock : StateBehaviour
{
	public bool setValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GameObject gameObject = GameObject.Find("Sex Menu Canvas");
		GameObject gameObject2 = GameObject.Find("Battle Menu Canvas");
		if (gameObject != null)
		{
			gameObject.transform.Find("Black Panel/Window/Close Button").GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("isRightClickLock").Value = setValue;
		}
		else if (gameObject2 != null)
		{
			gameObject2.transform.Find("Black Panel/Window/Close Button").GetComponent<PlayMakerFSM>().FsmVariables.FindFsmBool("isRightClickLock").Value = setValue;
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
