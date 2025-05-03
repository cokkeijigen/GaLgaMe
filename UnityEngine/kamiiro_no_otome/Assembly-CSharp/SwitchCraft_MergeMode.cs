using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SwitchCraft_MergeMode : StateBehaviour
{
	private CraftManager craftManager;

	private CraftTalkManager craftTalkManager;

	public StateLink craftLink;

	public StateLink mergeLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
	}

	public override void OnStateBegin()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			Transition(craftLink);
			break;
		case "merge":
			Transition(mergeLink);
			break;
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
