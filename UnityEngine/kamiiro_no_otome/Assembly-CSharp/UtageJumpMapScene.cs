using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class UtageJumpMapScene : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.isWorldMapToUtage = false;
		PlayerNonSaveDataManager.loadSceneName = "scenario";
		PlayerNonSaveDataManager.currentSceneName = "main";
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
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
