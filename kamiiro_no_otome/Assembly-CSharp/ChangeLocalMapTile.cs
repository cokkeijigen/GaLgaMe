using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeLocalMapTile : StateBehaviour
{
	public GameObject mapTileGO;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("LocalAreaMap");
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		mapTileGO.transform.Find(PlayerDataManager.currentAccessPointName).gameObject.SetActive(value: true);
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
