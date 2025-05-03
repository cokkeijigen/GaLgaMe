using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendItemListRefresh : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
	}

	public override void OnStateBegin()
	{
		craftCanvasManager.isCompleteEnhanceCount = false;
		craftCanvasManager.isPowerUpCraft = false;
		if (PlayerNonSaveDataManager.selectCraftCanvasName == "newCraft")
		{
			craftManager.newCraftFSM.SendTrigger("RefreshNewCraftCanvas");
		}
		else
		{
			craftManager.craftAndMergeFSM.SendTrigger("RefreshCraftCanvas");
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
