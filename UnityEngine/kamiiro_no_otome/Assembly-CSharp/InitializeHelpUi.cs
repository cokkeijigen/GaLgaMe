using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeHelpUi : StateBehaviour
{
	private HelpDataManager helpDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		helpDataManager = GameObject.Find("Help Data Manager").GetComponent<HelpDataManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.sendHelpMarkButtonIndex == 9)
		{
			helpDataManager.selectTabTypeNum = 0;
		}
		else
		{
			helpDataManager.selectTabTypeNum = PlayerNonSaveDataManager.sendHelpMarkButtonIndex;
		}
		helpDataManager.selectScrollContentIndex = 0;
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
