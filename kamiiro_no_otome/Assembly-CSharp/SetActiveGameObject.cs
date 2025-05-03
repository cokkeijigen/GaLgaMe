using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetActiveGameObject : StateBehaviour
{
	public GameObject targetGo;

	public string targetName;

	public bool setValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		if (targetGo == null)
		{
			targetGo = GameObject.Find(targetName);
		}
	}

	public override void OnStateBegin()
	{
		targetGo.SetActive(setValue);
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
