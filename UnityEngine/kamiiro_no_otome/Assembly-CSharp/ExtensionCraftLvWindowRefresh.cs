using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ExtensionCraftLvWindowRefresh : StateBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
		parameterContainer = craftExtensionManager.craftLvParameterContainer;
	}

	public override void OnStateBegin()
	{
		CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
		parameterContainer.GetVariable<TmpText>("craftLvNumText").textMeshProUGUI.text = PlayerCraftStatusManager.playerCraftLv.ToString();
		int playerCraftLv = PlayerCraftStatusManager.playerCraftLv;
		int num = 0;
		int num2 = 0;
		Slider slider = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").slider;
		if (playerCraftLv < 10)
		{
			num = GameDataManager.instance.needExpDataBase.needCraftLvExpList[playerCraftLv];
			parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI2.text = num.ToString();
		}
		else
		{
			num = 99999;
			parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI2.text = "99999";
		}
		num2 = ((playerCraftLv < 2) ? GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv - 1] : 0);
		slider.minValue = num2;
		slider.maxValue = num;
		parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI1.text = PlayerCraftStatusManager.playerCraftExp.ToString();
		slider.value = PlayerCraftStatusManager.playerCraftExp;
		parameterContainer.GetVariable<UguiTextVariable>("workShopLvText").text.text = craftWorkShopData.workShopLv.ToString();
		parameterContainer.GetVariable<UguiTextVariable>("tooLvText").text.text = craftWorkShopData.workShopToolLv.ToString();
		parameterContainer.GetVariable<UguiTextVariable>("furnaceLvText").text.text = craftWorkShopData.furnaceLv.ToString();
		parameterContainer.GetVariable<UguiTextVariable>("addOnLvText").text.text = craftWorkShopData.enableAddOnLv.ToString();
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
