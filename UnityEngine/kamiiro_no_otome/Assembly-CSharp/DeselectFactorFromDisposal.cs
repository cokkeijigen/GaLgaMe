using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DeselectFactorFromDisposal : StateBehaviour
{
	private CraftManager craftManager;

	private FactorDisposalManager factorDisposalManager;

	public StateLink nextState;

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
		int selectSiblingIndex = factorDisposalManager.selectSiblingIndex;
		factorDisposalManager.haveFactorScrollContent.transform.GetChild(selectSiblingIndex).gameObject.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
		factorDisposalManager.selectSiblingIndex = 0;
		factorDisposalManager.disposalFrameGo.SetActive(value: false);
		factorDisposalManager.applyButton.alpha = 0.5f;
		factorDisposalManager.applyButton.interactable = false;
		Transition(nextState);
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
