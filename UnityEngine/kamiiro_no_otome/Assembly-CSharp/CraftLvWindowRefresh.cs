using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CraftLvWindowRefresh : StateBehaviour
{
	private CraftManager craftManager;

	public ParameterContainer parameterContainer;

	public GameObject furnaceGo;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponentInParent<CraftManager>();
	}

	public override void OnStateBegin()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
		case "merge":
		case "blacksmith":
		{
			CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			parameterContainer.GetVariable<TmpText>("craftLvNumText").textMeshProUGUI.text = PlayerCraftStatusManager.playerCraftLv.ToString();
			parameterContainer.GetVariable<UguiTextVariable>("workShopLvText").text.text = craftWorkShopData.workShopLv.ToString();
			parameterContainer.GetVariable<UguiTextVariable>("tooLvText").text.text = craftWorkShopData.workShopToolLv.ToString();
			parameterContainer.GetVariable<UguiTextVariable>("furnaceLvText").text.text = craftWorkShopData.furnaceLv.ToString();
			parameterContainer.GetVariable<UguiTextVariable>("addOnLvText").text.text = craftWorkShopData.enableAddOnLv.ToString();
			int num = 0;
			int num2 = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv];
			if (PlayerCraftStatusManager.playerCraftLv > 1)
			{
				num = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv - 1];
			}
			Slider slider = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").slider;
			slider.minValue = num;
			slider.maxValue = num2;
			slider.value = PlayerCraftStatusManager.playerCraftExp;
			parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI1.text = PlayerCraftStatusManager.playerCraftExp.ToString();
			parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI2.text = num2.ToString();
			furnaceGo.SetActive(value: true);
			parameterContainer.GetGameObjectList("facilityGroupArray")[0].SetActive(value: true);
			parameterContainer.GetGameObjectList("facilityGroupArray")[1].SetActive(value: false);
			craftManager.characterImage.sprite = craftManager.characterImageSpriteArray[0];
			break;
		}
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
