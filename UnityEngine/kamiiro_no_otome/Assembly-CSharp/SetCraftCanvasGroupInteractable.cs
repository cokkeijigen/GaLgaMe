using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetCraftCanvasGroupInteractable : StateBehaviour
{
	private CraftManager craftManager;

	public bool setValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
	}

	public override void OnStateBegin()
	{
		CanvasGroup[] canvasGroupArray = craftManager.canvasGroupArray;
		for (int i = 0; i < canvasGroupArray.Length; i++)
		{
			canvasGroupArray[i].interactable = setValue;
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
