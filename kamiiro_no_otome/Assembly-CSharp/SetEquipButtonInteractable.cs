using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetEquipButtonInteractable : StateBehaviour
{
	private StatusManager statusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
	}

	public override void OnStateBegin()
	{
		if (statusManager.selectCharacterNum == 0 && (statusManager.selectItemCategoryNum == 7 || statusManager.selectItemCategoryNum == 8))
		{
			CanvasGroup component = statusManager.itemEquipButtonGO.GetComponent<CanvasGroup>();
			component.alpha = 0.5f;
			component.interactable = false;
			statusManager.itemEquipButtonLoc.Term = "buttonNowEquip";
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
