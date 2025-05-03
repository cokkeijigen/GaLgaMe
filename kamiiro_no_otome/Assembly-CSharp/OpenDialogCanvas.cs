using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenDialogCanvas : StateBehaviour
{
	public string dialogType;

	private PlayMakerFSM dialogManagerFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dialogManagerFSM = GameObject.Find("DontDestroy Group").transform.Find("Dialog Manager").GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.openDialogName = dialogType;
		dialogManagerFSM.SendEvent("OpenDialog");
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
