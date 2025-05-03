using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckOpenItemCategory : StateBehaviour
{
	private StatusManager statusManager;

	public StateLink equipLink;

	public StateLink itemLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GetComponent<StatusManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.selectStatusCanvasName == "equipItem")
		{
			Transition(equipLink);
		}
		else
		{
			Transition(itemLink);
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
