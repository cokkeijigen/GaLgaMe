using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonRevengeBattle : StateBehaviour
{
	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GameObject.Find("Battle Failed Canvas").SetActive(value: false);
		Invoke("InvokeMethod", waitTime);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
