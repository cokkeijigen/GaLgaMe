using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckGameObjectsActive : StateBehaviour
{
	public GameObject[] gameobjects;

	private bool isActive;

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
		isActive = false;
		GameObject[] array = gameobjects;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].activeInHierarchy)
			{
				isActive = true;
				break;
			}
		}
		if (isActive)
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
