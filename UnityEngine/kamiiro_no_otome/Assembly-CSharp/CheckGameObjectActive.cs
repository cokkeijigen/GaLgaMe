using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckGameObjectActive : StateBehaviour
{
	public string findName;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (GameObject.Find(findName) != null)
		{
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
		}
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
