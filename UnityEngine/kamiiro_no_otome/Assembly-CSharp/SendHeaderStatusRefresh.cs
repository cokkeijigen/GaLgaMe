using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendHeaderStatusRefresh : StateBehaviour
{
	private ArborFSM arborFSM1;

	private ArborFSM arborFSM2;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (PlayerNonSaveDataManager.loadSceneName)
		{
		case "main":
			arborFSM1 = GameObject.Find("Header Status Manager").GetComponent<ArborFSM>();
			arborFSM1.SendTrigger("HeaderStatusRefresh");
			break;
		case "scenario":
			arborFSM1 = GameObject.Find("Scenario Manager").GetComponent<ArborFSM>();
			arborFSM1.SendTrigger("UtageStart");
			break;
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
