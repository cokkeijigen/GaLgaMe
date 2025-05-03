using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DisplayMergeRecipeContent : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	private CraftTalkManager craftTalkManager;

	private ItemPartyWeaponData itemPartyWeaponData;

	private ItemPartyArmorData itemPartyArmorData;

	public StateLink stateLink;

	private int[] statusParam = new int[6];

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		for (int num = craftCanvasManager.infoFactorScrollContentGO.transform.childCount - 1; num >= 0; num--)
		{
			craftCanvasManager.infoFactorScrollContentGO.transform.GetChild(num).transform.SetParent(craftManager.poolParentGO.transform);
		}
	}

	public override void OnStateBegin()
	{
		craftManager.canvasGroupArray[0].gameObject.SetActive(value: false);
		craftManager.canvasGroupArray[1].gameObject.SetActive(value: true);
		craftCanvasManager.craftNeedItemGoArray[3].SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[0].SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -402f);
		craftCanvasManager.craftWindowGoArray[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(-386f, -597f);
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
				craftManager.addOnLockTextLoc.Term = "craftAddOnLock_PowerUp";
			}
			else
			{
				craftManager.addOnLockTextLoc.Term = "craftAddOnLock_Merge";
			}
		}
		craftCanvasManager.craftWindowGoArray[1].SetActive(value: true);
		craftCanvasManager.craftWindowGoArray[2].SetActive(value: true);
		craftCanvasManager.ResetGetFactorInfo();
		craftCanvasManager.craftWindowGoArray[3].SetActive(value: false);
		craftManager.addOnHeaderTextLoc.Term = "itemTypeSummary_wonderMaterial";
		int partyCharacterNumFromItemID = PlayerInventoryDataEquipAccess.GetPartyCharacterNumFromItemID(craftManager.clickedItemID);
		craftTalkManager.selectItemEquipCharacterId = partyCharacterNumFromItemID;
		craftTalkManager.TalkBalloonItemSelectAfter();
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			itemPartyWeaponData = null;
			if (craftCanvasManager.isPowerUpCraft)
			{
				itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData m) => m.itemID == craftManager.nextItemID);
			}
			else
			{
				itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData m) => m.itemID == craftManager.clickedItemID);
			}
			int num3 = 0;
			int num4 = 0;
			num3 = ((!craftCanvasManager.isPowerUpCraft) ? itemPartyWeaponData.needMaterialEditList.Count : itemPartyWeaponData.needMaterialList.Count);
			for (int k = 0; k < num3; k++)
			{
				SetRecipeContent(k);
				num4++;
			}
			for (int l = num4; l < 4; l++)
			{
				craftCanvasManager.craftNeedItemGoArray[l].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[l] = true;
			}
			CraftWorkShopData craftWorkShopData2 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemPartyWeaponData.needWorkShopLevel <= craftWorkShopData2.workShopLv;
			SetSummaryAlertGoVisible();
			craftCanvasManager.ResetHaveFactorList(itemPartyWeaponData.factorHaveLimit, ChangeMaxVal: true);
			craftCanvasManager.AdjustApplyButtonActive_MERGE();
			break;
		}
		case 1:
		{
			itemPartyArmorData = null;
			if (craftCanvasManager.isPowerUpCraft)
			{
				itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData m) => m.itemID == craftManager.nextItemID);
			}
			else
			{
				itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData m) => m.itemID == craftManager.clickedItemID);
			}
			int num = 0;
			int num2 = 0;
			num = ((!craftCanvasManager.isPowerUpCraft) ? itemPartyArmorData.needMaterialEditList.Count : itemPartyArmorData.needMaterialList.Count);
			for (int i = 0; i < num; i++)
			{
				SetRecipeContent(i);
				num2++;
			}
			for (int j = num2; j < 4; j++)
			{
				craftCanvasManager.craftNeedItemGoArray[j].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[j] = true;
			}
			CraftWorkShopData craftWorkShopData = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"];
			craftCanvasManager.isWorkShopLvEnough = itemPartyArmorData.needWorkShopLevel <= craftWorkShopData.workShopLv;
			SetSummaryAlertGoVisible();
			craftCanvasManager.ResetHaveFactorList(itemPartyArmorData.factorHaveLimit, ChangeMaxVal: true);
			craftCanvasManager.AdjustApplyButtonActive_MERGE();
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(craftCanvasManager.infoFactorScrollContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void DisplayHaveFactorList_MERGE()
	{
		craftCanvasManager.ResetHaveFactorList(0, ChangeMaxVal: false);
		int[] array = new int[4];
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID);
			List<HaveFactorData> dict2 = null;
			PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, 0, ref dict2);
			if (dict2 == null)
			{
				break;
			}
			int num2 = 0;
			foreach (HaveFactorData factDat2 in dict2)
			{
				Transform transform2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[5]);
				RefreshItemList(transform2, num2);
				FactorData factorData2 = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == factDat2.factorID);
				if (factorData2 != null)
				{
					transform2.transform.Find("Text Group/Name Text").GetComponent<Localize>().Term = "factor_" + factorData2.factorType;
					transform2.transform.Find("Text Group/Power Group/Power Text").GetComponent<Text>().text = factDat2.factorPower.ToString();
				}
				transform2.transform.Find("Icon Image").GetComponent<Image>().sprite = factorData2.factorSprite;
				switch (factDat2.factorID)
				{
				case 0:
					array[0] += factDat2.factorPower;
					break;
				case 10:
					array[1] += factDat2.factorPower;
					break;
				case 20:
					array[2] += factDat2.factorPower;
					break;
				case 30:
					array[3] += factDat2.factorPower;
					break;
				}
				num2++;
			}
			craftCanvasManager.infoFactorGroupText[0].text = dict2.Count.ToString();
			break;
		}
		case 1:
		{
			GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID);
			List<HaveFactorData> dict = null;
			PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, 0, ref dict);
			if (dict == null)
			{
				break;
			}
			int num = 0;
			foreach (HaveFactorData factDat in dict)
			{
				Transform transform = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[5]);
				RefreshItemList(transform, num);
				FactorData factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorID == factDat.factorID);
				if (factorData != null)
				{
					transform.transform.Find("Text Group/Name Text").GetComponent<Localize>().Term = "factor_" + factorData.factorType;
					transform.transform.Find("Text Group/Power Group/Power Text").GetComponent<Text>().text = factDat.factorPower.ToString();
				}
				transform.transform.Find("Icon Image").GetComponent<Image>().sprite = factorData.factorSprite;
				switch (factDat.factorID)
				{
				case 200:
					array[0] += factDat.factorPower;
					break;
				case 210:
					array[1] += factDat.factorPower;
					break;
				case 220:
					array[2] += factDat.factorPower;
					break;
				case 240:
					array[3] += factDat.factorPower;
					break;
				}
				num++;
			}
			craftCanvasManager.infoFactorGroupText[0].text = dict.Count.ToString();
			break;
		}
		}
	}

	private void SetRecipeContent(int i)
	{
		GameObject gameObject = craftCanvasManager.craftNeedItemGoArray[i];
		ParameterContainer component = craftCanvasManager.craftNeedItemGoArray[i].GetComponent<ParameterContainer>();
		int num = 0;
		int num2 = 0;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			if (craftCanvasManager.isPowerUpCraft)
			{
				num = itemPartyWeaponData.needMaterialList[i].itemID;
				num2 = itemPartyWeaponData.needMaterialList[i].needNum;
			}
			else
			{
				num = itemPartyWeaponData.needMaterialEditList[i].itemID;
				num2 = itemPartyWeaponData.needMaterialEditList[i].needNum;
			}
			break;
		case 1:
			if (craftCanvasManager.isPowerUpCraft)
			{
				num = itemPartyArmorData.needMaterialList[i].itemID;
				num2 = itemPartyArmorData.needMaterialList[i].needNum;
			}
			else
			{
				num = itemPartyArmorData.needMaterialEditList[i].itemID;
				num2 = itemPartyArmorData.needMaterialEditList[i].needNum;
			}
			break;
		}
		Debug.Log("必要素材のID：" + num);
		string itemName = "";
		Sprite itemIconSprite = null;
		Sprite itemSprite = null;
		string CategoryName = "";
		if (craftManager.GetCommonDataFromItemID(num, ref itemName, ref itemIconSprite, ref CategoryName, ref itemSprite))
		{
			gameObject.transform.Find("Group").gameObject.SetActive(value: true);
			component.GetVariable<UguiImage>("iconImage").image.sprite = itemIconSprite;
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = CategoryName + num;
			component.GetVariable<UguiTextVariable>("needNumText").text.text = num2.ToString();
			int playerHaveItemNum = PlayerInventoryDataAccess.GetPlayerHaveItemNum(num);
			component.GetVariable<UguiTextVariable>("haveNumText").text.text = playerHaveItemNum.ToString();
			component.SetInt("itemId", num);
			if (num2 <= playerHaveItemNum)
			{
				craftManager.requiredItemENOUGH[i] = true;
				component.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.enableColor;
			}
			else
			{
				craftManager.requiredItemENOUGH[i] = false;
				component.GetVariable<UguiTextVariable>("nameText").text.color = craftManager.disableColor;
			}
			component.GetVariable<UguiTextVariable>("nameText").text.fontStyle = FontStyle.Normal;
		}
	}

	private void SetSummaryAlertGoVisible()
	{
		ParameterContainer component = craftManager.canvasGroupArray[1].GetComponent<ParameterContainer>();
		if (craftCanvasManager.isWorkShopLvEnough)
		{
			component.GetGameObjectList("alertGoList")[0].SetActive(value: false);
			component.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 1f, 1f);
			component.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 1f, 1f);
			component.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 1f, 1f);
		}
		else
		{
			component.GetGameObjectList("alertGoList")[0].SetActive(value: true);
			component.GetVariableList<UguiImage>("needLvIconList")[0].image.color = new Color(1f, 0.55f, 0.3f);
			component.GetVariable<UguiTextVariable>("needWorkShopText").text.color = new Color(1f, 0.55f, 0.3f);
			component.GetVariableList<UguiTextVariable>("needSummaryTextList")[0].text.color = new Color(1f, 0.55f, 0.3f);
		}
	}
}
