using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SelectFactorToDisposal : StateBehaviour
{
	private CraftManager craftManager;

	private FactorDisposalManager factorDisposalManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		factorDisposalManager = GameObject.Find("Factor Disposal Manager").GetComponent<FactorDisposalManager>();
	}

	public override void OnStateBegin()
	{
		factorDisposalManager.ResetScrollContentSprite();
		int selectSiblingIndex = factorDisposalManager.selectSiblingIndex;
		GameObject gameObject = factorDisposalManager.haveFactorScrollContent.transform.GetChild(selectSiblingIndex).gameObject;
		ParameterContainer component = gameObject.GetComponent<ParameterContainer>();
		component.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[1];
		factorDisposalManager.disposalFrameTextLoc.Term = component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term;
		factorDisposalManager.disposalFrameNumText.text = component.GetVariable<UguiTextVariable>("numText").text.text;
		factorDisposalManager.disposalFrameIcon.sprite = component.GetVariable<UguiImage>("iconImage").image.sprite;
		factorDisposalManager.disposalFrameGo.GetComponent<FactorDisposalButtonClickAction>().uniqueID = gameObject.GetComponent<FactorDisposalButtonClickAction>().uniqueID;
		factorDisposalManager.disposalFrameGo.SetActive(value: true);
		factorDisposalManager.applyButton.alpha = 1f;
		factorDisposalManager.applyButton.interactable = true;
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
