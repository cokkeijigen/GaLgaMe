using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetInActiveLocalMap : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		GameObject[] localAreaGoArray = totalMapAccessManager.localAreaGoArray;
		for (int i = 0; i < localAreaGoArray.Length; i++)
		{
			localAreaGoArray[i].SetActive(value: false);
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
