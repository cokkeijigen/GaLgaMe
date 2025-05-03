using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendEventToGameObject : StateBehaviour
{
	public string findGameObjectName;

	public string sendEventName;

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
		go.GetComponent<PlayMakerFSM>().SendEvent(sendEventName);
		string text = base.gameObject.name;
		Debug.Log("発信元GO：" + text + "／イベント：" + sendEventName);
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
