using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CallDebugLog : StateBehaviour
{
	public string callLogText;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		string text = base.gameObject.name;
		Debug.Log(callLogText + "／アタッチGO名は：" + text);
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
