using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetActiveGameObjects : StateBehaviour
{
	public List<GameObject> targetObjects;

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
		foreach (GameObject targetObject in targetObjects)
		{
			targetObject.SetActive(setValue);
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
