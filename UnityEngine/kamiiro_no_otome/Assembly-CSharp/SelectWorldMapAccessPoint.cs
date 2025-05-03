using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SelectWorldMapAccessPoint : StateBehaviour
{
	private WorldMapAccessManager worldMapAccessManager;

	public ArborFSM worldMapManagerFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.selectAccessPointName = base.transform.parent.gameObject.name;
		PlayerNonSaveDataManager.selectAccessPointGO = base.transform.parent.gameObject;
		worldMapManagerFSM.SendTrigger("MapPointClick");
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
