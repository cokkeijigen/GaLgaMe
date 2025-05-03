using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeLocalAreaMap : StateBehaviour
{
	public GameObject[] localMapArray;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		localMapArray = GameObject.FindGameObjectsWithTag("LocalAreaMap");
	}

	public override void OnStateBegin()
	{
		GameObject[] array = localMapArray;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		if (localMapArray.Any((GameObject go) => go.name == PlayerDataManager.currentAccessPointName))
		{
			array = localMapArray;
			foreach (GameObject gameObject in array)
			{
				if (gameObject.name == PlayerDataManager.currentAccessPointName)
				{
					gameObject.SetActive(value: true);
				}
			}
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
