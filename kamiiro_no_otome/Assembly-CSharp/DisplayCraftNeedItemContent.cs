using System;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DisplayCraftNeedItemContent : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	private CraftTalkManager craftTalkManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		newCraftCanvasManager.craftQuantity = 1;
		newCraftCanvasManager.multiNumText.text = "1";
		craftManager.canvasGroupArray[0].gameObject.SetActive(value: false);
		craftManager.canvasGroupArray[1].gameObject.SetActive(value: true);
		craftCanvasManager.craftNeedItemGoArray[3].SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[0].SetActive(value: true);
		craftCanvasManager.needCostFrameImage.sprite = craftCanvasManager.needCostFrameSpriteArray[0];
		craftCanvasManager.craftWindowGoArray[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -402f);
		craftCanvasManager.craftWindowGoArray[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -597f);
		craftCanvasManager.applyButtonLoc.Term = "buttonCraftStart";
		craftCanvasManager.applyButtonGroup[0].GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[0];
		craftTalkManager.TalkBalloonItemSelectAfter();
		int num = 0;
		int num2 = 0;
		int quantity = 1;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemWeaponData itemWeaponData = null;
			if (!craftCanvasManager.isRemainingDaysZero || craftCanvasManager.isNextItemNothing)
			{
				itemWeaponData = ((!craftCanvasManager.isCompleteEnhanceCount) ? GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID) : GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID));
			}
			else
			{
				itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.nextItemID);
				craftCanvasManager.applyButtonGroup[0].GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[1];
				craftCanvasManager.needCostFrameImage.sprite = craftCanvasManager.needCostFrameSpriteArray[1];
			}
			craftCanvasManager.itemWeaponData = itemWeaponData;
			if (!craftCanvasManager.isRemainingDaysZero)
			{
				num = ((!craftCanvasManager.isCompleteEnhanceCount) ? itemWeaponData.needMaterialEditList.Count : itemWeaponData.needMaterialEditList.Count);
			}
			else
			{
				num = itemWeaponData.needMaterialNewerList.Count;
				float num4 = (float)itemWeaponData.factorSlot / 2f;
				num4 = (float)Math.Round(num4, MidpointRounding.AwayFromZero);
				quantity = (int)num4;
				newCraftCanvasManager.craftQuantity = (int)num4;
			}
			num2 = 0;
			for (int k = 0; k < num; k++)
			{
				craftCanvasManager.SetRecipeContent(k, quantity);
				num2++;
			}
			for (int l = num2; l < 4; l++)
			{
				craftCanvasManager.craftNeedItemGoArray[l].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[l] = true;
			}
			craftCanvasManager.AdjustApplyButtonActive();
			craftCanvasManager.ResetHaveFactorList(itemWeaponData.factorHaveLimit, ChangeMaxVal: true);
			SetInfoWindowType();
			break;
		}
		case 1:
		{
			ItemArmorData itemArmorData = null;
			if (craftCanvasManager.isRemainingDaysZero && !craftCanvasManager.isNextItemNothing)
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.nextItemID);
				craftCanvasManager.applyButtonGroup[0].GetComponent<Image>().sprite = craftCanvasManager.craftStartButtonSpriteArray[1];
			}
			else if (craftCanvasManager.isCompleteEnhanceCount)
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
				craftCanvasManager.needCostFrameImage.sprite = craftCanvasManager.needCostFrameSpriteArray[1];
			}
			else
			{
				itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
			}
			craftCanvasManager.itemArmorData = itemArmorData;
			if (!craftCanvasManager.isRemainingDaysZero)
			{
				num = ((!craftCanvasManager.isCompleteEnhanceCount) ? itemArmorData.needMaterialEditList.Count : itemArmorData.needMaterialEditList.Count);
			}
			else
			{
				num = itemArmorData.needMaterialNewerList.Count;
				float num3 = (float)itemArmorData.factorSlot / 2f;
				num3 = (float)Math.Round(num3, MidpointRounding.AwayFromZero);
				quantity = (int)num3;
				newCraftCanvasManager.craftQuantity = (int)num3;
			}
			num2 = 0;
			for (int i = 0; i < num; i++)
			{
				craftCanvasManager.SetRecipeContent(i, quantity);
				num2++;
			}
			for (int j = num2; j < 4; j++)
			{
				craftCanvasManager.craftNeedItemGoArray[j].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[j] = true;
			}
			craftCanvasManager.AdjustApplyButtonActive();
			craftCanvasManager.ResetHaveFactorList(itemArmorData.factorHaveLimit, ChangeMaxVal: true);
			SetInfoWindowType();
			break;
		}
		}
		craftAddOnManager.MagicMaterialListRefresh();
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

	private void SetInfoWindowType()
	{
		int enableAddOnLv = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv;
		if (!craftCanvasManager.isPowerUpCraft && enableAddOnLv > 0)
		{
			craftManager.craftAddOnGoArray[0].SetActive(value: true);
			craftManager.craftAddOnGoArray[1].SetActive(value: false);
		}
		else
		{
			craftManager.craftAddOnGoArray[0].SetActive(value: false);
			craftManager.craftAddOnGoArray[1].SetActive(value: true);
			if (craftCanvasManager.isPowerUpCraft)
			{
				craftManager.addOnLockTextLoc.Term = "craftAddOnLock_Inherit";
			}
			else
			{
				craftManager.addOnLockTextLoc.Term = "craftAddOnLock_Craft";
			}
		}
		craftCanvasManager.craftWindowGoArray[1].SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[2].SetActive(value: true);
		craftCanvasManager.ResetGetFactorInfo();
		craftCanvasManager.craftWindowGoArray[3].SetActive(value: false);
		craftManager.addOnHeaderTextLoc.Term = "itemTypeSummary_addOnMaterial";
	}

	private void SetSummaryAlertGoVisible()
	{
		ParameterContainer selectItemInfoParam = craftCanvasManager.selectItemInfoParam;
		if (craftCanvasManager.isWorkShopLvEnough)
		{
			selectItemInfoParam.GetGameObjectList("alertGoList")[0].SetActive(value: false);
			selectItemInfoParam.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 1f, 1f);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 1f, 1f);
			selectItemInfoParam.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 1f, 1f);
		}
		else
		{
			selectItemInfoParam.GetGameObjectList("alertGoList")[0].SetActive(value: true);
			selectItemInfoParam.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 0.55f, 0.3f);
			selectItemInfoParam.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 0.55f, 0.3f);
			selectItemInfoParam.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 0.55f, 0.3f);
		}
	}
}
