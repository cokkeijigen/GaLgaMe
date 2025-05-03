using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DisplayHaveFactorList : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private bool isMerge;

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
		craftCanvasManager.ResetHaveFactorList(0, ChangeMaxVal: false);
		isMerge = false;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			Debug.Log("クリックID：" + craftManager.clickedItemID + "／選択インスタンスID：" + craftManager.clickedUniqueID + "／作成後ユニークID：" + craftCheckManager.craftedUniqueID);
			List<HaveFactorData> dict3 = null;
			List<HaveFactorData> dict4 = null;
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
				if (craftCanvasManager.isCraftComplete)
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftCheckManager.craftedItemID, craftCheckManager.craftedUniqueID, ref dict3);
					PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftCheckManager.craftedItemID, craftCheckManager.craftedUniqueID, ref dict4);
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict3);
					PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict4);
				}
				craftCheckManager.tempSetFactorList = dict4;
				break;
			case "newCraft":
				return;
			case "merge":
				isMerge = true;
				if (craftCanvasManager.isCraftComplete)
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftCheckManager.craftedItemID, 0, ref dict3);
					PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftCheckManager.craftedItemID, 0, ref dict4);
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, 0, ref dict3);
					PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftManager.clickedItemID, 0, ref dict4);
				}
				craftCheckManager.tempSetFactorList = dict4;
				break;
			}
			craftCanvasManager.isCraftComplete = false;
			if (dict3 == null)
			{
				break;
			}
			int num3 = 0;
			foreach (HaveFactorData factDat2 in dict3)
			{
				Transform transform2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[5]);
				RefreshItemList(transform2, num3);
				FactorData factorData2 = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == factDat2.factorID);
				if (factorData2 != null)
				{
					ParameterContainer component2 = transform2.GetComponent<ParameterContainer>();
					component2.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "factor_" + factorData2.factorType;
					if (factorData2.isAddPercentText)
					{
						component2.GetVariable<UguiTextVariable>("powerText").text.text = factDat2.factorPower + "%";
					}
					else
					{
						component2.GetVariable<UguiTextVariable>("powerText").text.text = factDat2.factorPower.ToString();
					}
					component2.GetVariable<I2LocalizeComponent>("lvTextLoc").localize.Term = "craftGetFactorPower" + factDat2.factorLV;
					component2.GetVariable<UguiImage>("iconImage").image.sprite = factorData2.factorSprite;
					if (craftCheckManager.tempSetFactorList.Find((HaveFactorData data) => data.uniqueID == factDat2.uniqueID) != null)
					{
						component2.GetGameObject("equipIconGo").SetActive(value: true);
					}
					else
					{
						component2.GetGameObject("equipIconGo").SetActive(value: false);
					}
				}
				num3++;
			}
			int num4 = 0;
			num4 = (isMerge ? GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit : GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit);
			craftCanvasManager.infoFactorGroupText[0].text = dict3.Count.ToString();
			craftCanvasManager.infoFactorGroupText[1].text = num4.ToString();
			break;
		}
		case 1:
		{
			List<HaveFactorData> dict = null;
			List<HaveFactorData> dict2 = null;
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
				if (craftCanvasManager.isCraftComplete)
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftCheckManager.craftedItemID, craftCheckManager.craftedUniqueID, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(craftCheckManager.craftedItemID, craftCheckManager.craftedUniqueID, ref dict2);
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict2);
				}
				craftCheckManager.tempSetFactorList = dict2;
				break;
			case "newCraft":
				return;
			case "merge":
				isMerge = true;
				if (craftCanvasManager.isCraftComplete)
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftCheckManager.craftedItemID, 0, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(craftCheckManager.craftedItemID, 0, ref dict2);
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, 0, ref dict);
					PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(craftManager.clickedItemID, 0, ref dict2);
				}
				craftCheckManager.tempSetFactorList = dict2;
				break;
			}
			craftCanvasManager.isCraftComplete = false;
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
					ParameterContainer component = transform.GetComponent<ParameterContainer>();
					component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "factor_" + factorData.factorType;
					if (factorData.isAddPercentText)
					{
						component.GetVariable<UguiTextVariable>("powerText").text.text = factDat.factorPower + "%";
					}
					else
					{
						component.GetVariable<UguiTextVariable>("powerText").text.text = factDat.factorPower.ToString();
					}
					component.GetVariable<I2LocalizeComponent>("lvTextLoc").localize.Term = "craftGetFactorPower" + factDat.factorLV;
					component.GetVariable<UguiImage>("iconImage").image.sprite = factorData.factorSprite;
					if (craftCheckManager.tempSetFactorList.Find((HaveFactorData data) => data.uniqueID == factDat.uniqueID) != null)
					{
						component.GetGameObject("equipIconGo").SetActive(value: true);
					}
					else
					{
						component.GetGameObject("equipIconGo").SetActive(value: false);
					}
				}
				num++;
			}
			int num2 = 0;
			num2 = (isMerge ? GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit : GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit);
			craftCanvasManager.infoFactorGroupText[0].text = dict.Count.ToString();
			craftCanvasManager.infoFactorGroupText[1].text = num2.ToString();
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(craftCanvasManager.infoFactorScrollContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}
}
