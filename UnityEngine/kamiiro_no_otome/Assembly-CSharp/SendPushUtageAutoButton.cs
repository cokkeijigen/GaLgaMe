using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendPushUtageAutoButton : StateBehaviour
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
		GameObject.Find("Utage Auto Manager").GetComponent<UtageAutoManager>().PushUtageAutoButton(base.gameObject);
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
