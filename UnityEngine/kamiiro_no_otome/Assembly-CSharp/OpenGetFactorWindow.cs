using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenGetFactorWindow : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCheckManager craftCheckManager;

	private CraftTalkManager craftTalkManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponentInParent<CraftManager>();
		craftCheckManager = GetComponent<CraftCheckManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		parameterContainer = craftCheckManager.getFactorWindow.GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		craftCheckManager.getFactorWindow.SetActive(value: true);
		craftCheckManager.blackImageGo.transform.SetSiblingIndex(3);
		craftCheckManager.newFactorFrame.SetActive(value: true);
		craftCheckManager.newItemFrame.SetActive(value: false);
		FactorData factorData = null;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			factorData = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == craftCheckManager.newFactorData.factorID);
			break;
		case 1:
			factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorID == craftCheckManager.newFactorData.factorID);
			break;
		}
		parameterContainer.GetVariable<UguiImage>("iconImage").image.sprite = factorData.factorSprite;
		parameterContainer.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "factor_" + factorData.factorType;
		if (factorData.isAddPercentText)
		{
			parameterContainer.GetVariable<UguiTextVariable>("powerText").text.text = craftCheckManager.newFactorData.factorPower + "%";
		}
		else
		{
			parameterContainer.GetVariable<UguiTextVariable>("powerText").text.text = craftCheckManager.newFactorData.factorPower.ToString();
		}
		craftTalkManager.TalkBalloonItemCrafted();
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
