using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetWorldMapPoint : StateBehaviour
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
		GameObject[] array = new GameObject[totalMapAccessManager.worldAreaParentGo.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = totalMapAccessManager.worldAreaParentGo.transform.GetChild(i).gameObject;
		}
		GameObject[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].transform.GetComponentInChildren<ArborFSM>().SendTrigger("ResetWorldMapPoint");
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
