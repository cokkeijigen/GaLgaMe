using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeCanvasManager : MonoBehaviour
{
	private CraftManager craftManager;

	public GameObject[] mergeNeedItemGoArray;

	public GameObject[] mergeAddOnItemGoArray;

	public GameObject[] mergeWindowGoArray;

	public GameObject mergerSummaryWindowGo;

	public GameObject factorScrollContentGO;

	public TextMeshProUGUI[] factorGroupText;

	public Localize[] itemTextLocGroup;

	public Toggle viewChangeToggle;

	public GameObject[] typeTextArray;

	public GameObject[] powerTextArray;

	public GameObject applyButtonParentGo;

	public Button[] applyButtonGroup;

	public Localize applyButtonLoc;

	public GameObject[] tabButtonGoArray;

	public Sprite[] tabButtonSpriteArray;

	public GameObject[] PowerUpGaugeArray;

	public Sprite[] PowerUpGaugeSpriteArray;

	public Localize[] GetFactorTypeLocGroup;

	public int nextItemIndex;

	public bool isWorkShopLvEnough;

	private void Start()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
	}

	public void ShowStatusParameters()
	{
		_ = viewChangeToggle.isOn;
		int[] itemStatusParam = craftManager.itemStatusParam;
		ParameterContainer component = mergerSummaryWindowGo.GetComponent<ParameterContainer>();
		IList<I2LocalizeComponent> variableList = component.GetVariableList<I2LocalizeComponent>("statusTypeLoc");
		IList<UguiTextVariable> variableList2 = component.GetVariableList<UguiTextVariable>("statusPowerText");
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			variableList[0].localize.Term = "statusAttack";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicAttack";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusAccuracy";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusCritical";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			variableList[4].localize.Term = "statusMpRecover";
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "statusSkillSlotCount";
			variableList2[5].text.text = itemStatusParam[5].ToString();
			variableList[6].localize.Term = "statusFactorSlot";
			variableList2[6].text.text = itemStatusParam[5].ToString();
			variableList[7].localize.Term = "statusFactorLimitNum";
			variableList2[7].text.text = itemStatusParam[5].ToString();
			break;
		case 1:
			variableList[0].localize.Term = "statusDefense";
			variableList2[0].text.text = itemStatusParam[0].ToString();
			variableList[1].localize.Term = "statusMagicDefense";
			variableList2[1].text.text = itemStatusParam[1].ToString();
			variableList[2].localize.Term = "statusEvasion";
			variableList2[2].text.text = itemStatusParam[2].ToString();
			variableList[3].localize.Term = "statusAgility";
			variableList2[3].text.text = itemStatusParam[3].ToString();
			variableList[4].localize.Term = "statusMpRecover";
			variableList2[4].text.text = itemStatusParam[4].ToString();
			variableList[5].localize.Term = "statusSkillSlotCount";
			variableList2[5].text.text = itemStatusParam[5].ToString();
			variableList[6].localize.Term = "statusFactorSlot";
			variableList2[6].text.text = itemStatusParam[5].ToString();
			variableList[7].localize.Term = "statusFactorLimitNum";
			variableList2[7].text.text = itemStatusParam[5].ToString();
			break;
		}
	}

	public void ResetHaveFactorList(int MaxVal, bool ChangeMaxVal)
	{
		for (int num = factorScrollContentGO.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = factorScrollContentGO.transform.GetChild(num);
			if (child.gameObject.tag == "CraftScrollItem")
			{
				child.transform.SetParent(craftManager.poolParentGO.transform);
				if (PoolManager.Pools["Craft Item Pool"].IsSpawned(child))
				{
					PoolManager.Pools["Craft Item Pool"].Despawn(child);
				}
			}
		}
		factorGroupText[0].text = "0";
		if (ChangeMaxVal)
		{
			factorGroupText[1].text = MaxVal.ToString();
		}
	}

	public void AdjustApplyButtonActive()
	{
		bool[] requiredItemENOUGH = craftManager.requiredItemENOUGH;
		bool num = requiredItemENOUGH[0] && requiredItemENOUGH[1] && requiredItemENOUGH[2] && requiredItemENOUGH[3];
		CanvasGroup component = applyButtonGroup[0].GetComponent<CanvasGroup>();
		if (num && isWorkShopLvEnough)
		{
			component.alpha = 1f;
			component.interactable = true;
		}
		else
		{
			component.alpha = 0.5f;
			component.interactable = false;
		}
		if (isWorkShopLvEnough)
		{
			applyButtonLoc.Term = "buttonEditStart";
		}
		else
		{
			applyButtonLoc.Term = "buttonEditImpossible";
		}
	}

	public void UpdateMergeList_MergedItem(int SourceItemID, int NewItemID)
	{
		for (int num = craftManager.itemSelectScrollContentGO.transform.childCount - 1; num >= 0; num--)
		{
			Transform child = craftManager.itemSelectScrollContentGO.transform.GetChild(num);
			MergeItemListClickAction component = child.gameObject.GetComponent<MergeItemListClickAction>();
			if (component != null && component.itemID == SourceItemID)
			{
				component.itemID = NewItemID;
				string itemName = "";
				Sprite itemIconSprite = null;
				Sprite itemSprite = null;
				string CategoryName = "";
				if (craftManager.GetCommonDataFromItemID(NewItemID, ref itemName, ref itemIconSprite, ref CategoryName, ref itemSprite))
				{
					child.Find("Icon Image").GetComponent<Image>().sprite = itemIconSprite;
					child.Find("Name Text").GetComponent<Localize>().Term = CategoryName + NewItemID;
					child.Find("Power Text").GetComponent<Text>().text = (NewItemID % 10 + 1).ToString();
				}
				break;
			}
		}
	}

	public void ChangeMergeInfoTab(int tabNum)
	{
		switch (tabNum)
		{
		case 0:
			mergeWindowGoArray[0].SetActive(value: true);
			mergeWindowGoArray[1].SetActive(value: true);
			mergeWindowGoArray[2].SetActive(value: true);
			mergeWindowGoArray[3].SetActive(value: true);
			mergeWindowGoArray[4].SetActive(value: false);
			tabButtonGoArray[0].GetComponent<Image>().sprite = tabButtonSpriteArray[1];
			tabButtonGoArray[1].GetComponent<Image>().sprite = tabButtonSpriteArray[0];
			break;
		case 1:
			mergeWindowGoArray[0].SetActive(value: false);
			mergeWindowGoArray[1].SetActive(value: false);
			mergeWindowGoArray[2].SetActive(value: false);
			mergeWindowGoArray[3].SetActive(value: false);
			mergeWindowGoArray[4].SetActive(value: true);
			tabButtonGoArray[0].GetComponent<Image>().sprite = tabButtonSpriteArray[0];
			tabButtonGoArray[1].GetComponent<Image>().sprite = tabButtonSpriteArray[1];
			break;
		}
	}
}
