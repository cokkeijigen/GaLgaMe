using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendTriggerToGameObject : StateBehaviour
{
	public string findGameObjectName;

	public string sendTriggerName;

	private GameObject go;

	public StateLink nextState;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		go = GameObject.Find(findGameObjectName);
		go.GetComponent<ArborFSM>().SendTrigger(sendTriggerName);
		_ = base.gameObject.name;
		Transition(nextState);
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
