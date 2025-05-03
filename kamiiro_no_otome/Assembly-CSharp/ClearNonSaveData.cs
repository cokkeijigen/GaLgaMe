using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ClearNonSaveData : StateBehaviour
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
		PlayerNonSaveDataManager.selectPlaceName = "";
		PlayerNonSaveDataManager.selectAccessPointName = "";
		PlayerNonSaveDataManager.isWorldMapVisibleFlag = false;
		PlayerNonSaveDataManager.isWorldMapToInDoor = false;
		PlayerNonSaveDataManager.isWorldMapPointDisable = false;
		PlayerNonSaveDataManager.totalMapUtageIsPlaying = false;
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
