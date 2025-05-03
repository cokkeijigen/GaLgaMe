using Arbor;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CraftLvCheck : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	public ParameterContainer parameterContainer;

	private Slider expSlider;

	private TextMeshProUGUI[] expTextArray = new TextMeshProUGUI[2];

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
		int num = 0;
		int playerCraftExp = PlayerCraftStatusManager.playerCraftExp;
		if (PlayerCraftStatusManager.playerCraftLv < 10)
		{
			_ = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv];
		}
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			switch (craftManager.selectCategoryNum)
			{
			case 0:
				num = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftCheckManager.craftedItemID).craftExp;
				break;
			case 1:
				num = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftCheckManager.craftedItemID).craftExp;
				break;
			case 2:
				num = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftCheckManager.craftedItemID).craftExp;
				break;
			case 3:
				num = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == craftCheckManager.craftedItemID).craftExp;
				break;
			case 4:
				num = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftCheckManager.craftedItemID).craftExp;
				break;
			}
			if (!craftCanvasManager.isPowerUpCraft && !craftCanvasManager.isCompleteEnhanceCount && PlayerNonSaveDataManager.selectCraftCanvasName == "craft")
			{
				num /= 2;
			}
			break;
		case "merge":
			switch (craftManager.selectCategoryNum)
			{
			case 0:
				num = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftCheckManager.craftedItemID).craftExp;
				break;
			case 1:
				num = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftCheckManager.craftedItemID).craftExp;
				break;
			}
			if (!craftCanvasManager.isPowerUpCraft)
			{
				num /= 2;
			}
			break;
		}
		playerCraftExp += num;
		playerCraftExp = (PlayerCraftStatusManager.playerCraftExp = Mathf.Clamp(playerCraftExp, 0, 99999));
		expSlider = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").slider;
		expTextArray[0] = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI1;
		expTextArray[1] = parameterContainer.GetVariable<SliderAndTmpMaxTextVariable>("craftLvExpSlider").textMeshProUGUI2;
		expSlider.DOValue(playerCraftExp, 0.3f).OnComplete(delegate
		{
			CompleteMethod();
		});
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (expTextArray[0] != null && expSlider != null)
		{
			expTextArray[0].text = expSlider.value.ToString();
			if (expSlider.value >= expSlider.maxValue && PlayerCraftStatusManager.playerCraftLv < 10)
			{
				PlayerCraftStatusManager.playerCraftLv++;
				int num = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv - 1];
				int num2 = GameDataManager.instance.needExpDataBase.needCraftLvExpList[PlayerCraftStatusManager.playerCraftLv];
				expSlider.minValue = num;
				expSlider.maxValue = num2;
				expTextArray[1].text = num2.ToString();
				parameterContainer.GetVariable<TmpText>("craftLvNumText").textMeshProUGUI.text = PlayerCraftStatusManager.playerCraftLv.ToString();
			}
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CompleteMethod()
	{
		Transition(stateLink);
	}
}
