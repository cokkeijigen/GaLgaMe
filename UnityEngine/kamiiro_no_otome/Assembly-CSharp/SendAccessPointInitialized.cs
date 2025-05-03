using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendAccessPointInitialized : StateBehaviour
{
	private WorldMapAccessManager worldMapAccessManager;

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
		worldMapAccessManager.AddWorldMapAccessPointInitialize();
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
