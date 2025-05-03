using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInterruptedLoaded : StateBehaviour
{
	public StateLink interruptedLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isInterruptedAfterSave)
		{
			Debug.Log("中断セーブをロード");
			PlayerNonSaveDataManager.isInitializeMapData = false;
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			PlayerNonSaveDataManager.selectScenarioName = PlayerNonSaveDataManager.currentScenarioLabelName + "_Interrupted";
			Transition(interruptedLink);
		}
		else
		{
			Debug.Log("中断セーブではないデータをロード");
			Transition(stateLink);
		}
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
