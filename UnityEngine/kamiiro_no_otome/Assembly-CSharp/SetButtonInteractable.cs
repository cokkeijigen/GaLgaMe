using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetButtonInteractable : StateBehaviour
{
	public bool isEnable;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GetComponent<Button>().interactable = isEnable;
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
