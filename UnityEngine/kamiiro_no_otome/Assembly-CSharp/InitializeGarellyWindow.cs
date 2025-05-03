using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeGarellyWindow : StateBehaviour
{
	private GarellyManager garellyManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		garellyManager = GameObject.Find("Garelly Manager").GetComponent<GarellyManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isUtageToJumpFromGarelly)
		{
			garellyManager.selectTabNum = PlayerNonSaveDataManager.garellySelectTabNum;
			garellyManager.selectPageNum = PlayerNonSaveDataManager.garellySelectPageNum;
			PlayerNonSaveDataManager.isGarellyOpenWithTitle = false;
			PlayerNonSaveDataManager.isUtageToJumpFromGarelly = false;
		}
		else
		{
			garellyManager.selectTabNum = 0;
			garellyManager.selectPageNum = 0;
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
