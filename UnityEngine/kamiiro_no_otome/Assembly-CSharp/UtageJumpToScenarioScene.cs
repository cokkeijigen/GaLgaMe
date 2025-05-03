using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class UtageJumpToScenarioScene : StateBehaviour
{
	public bool isInDoor;

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
		Debug.Log("宴開始");
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
