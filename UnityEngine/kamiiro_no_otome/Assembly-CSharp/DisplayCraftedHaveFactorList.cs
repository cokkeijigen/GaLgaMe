using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DisplayCraftedHaveFactorList : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

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
		bool flag = false;
		int num = 0;
		SetFactorSummaryTerm();
		int[] array = new int[4];
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			List<HaveFactorData> dict2 = new List<HaveFactorData>();
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict2);
					num = 2;
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict2);
					num = 0;
				}
				break;
			case "newCraft":
			{
				HaveFactorData item2 = new HaveFactorData
				{
					uniqueID = 0,
					factorID = craftCheckManager.newFactorID,
					factorLV = craftCheckManager.newPowerLevel,
					factorPower = craftCheckManager.newFactorPower
				};
				dict2.Add(item2);
				num = 3;
				break;
			}
			case "merge":
				flag = true;
				if (craftCanvasManager.isPowerUpCraft)
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, 0, ref dict2);
					num = 1;
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftManager.clickedItemID, 0, ref dict2);
					num = 0;
				}
				break;
			}
			if (dict2 == null)
			{
				break;
			}
			int num4 = 0;
			foreach (HaveFactorData factData2 in dict2)
			{
				Transform transform2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftCheckManager.infoFactorScrollPrefab);
				ParameterContainer component2 = transform2.GetComponent<ParameterContainer>();
				RefreshItemList(transform2, num4);
				FactorData factorData2 = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == factData2.factorID);
				if (factorData2 != null)
				{
					transform2.transform.Find("Text Group/Name Text").GetComponent<Localize>().Term = "factor_" + factorData2.factorType;
					if (factorData2.isAddPercentText)
					{
						transform2.transform.Find("Text Group/Power Group/Power Text").GetComponent<Text>().text = factData2.factorPower + "%";
					}
					else
					{
						transform2.transform.Find("Text Group/Power Group/Power Text").GetComponent<Text>().text = factData2.factorPower.ToString();
					}
					component2.GetVariable<I2LocalizeComponent>("lvTextLoc").localize.Term = "craftGetFactorPower" + factData2.factorLV;
				}
				transform2.transform.Find("Icon Image").GetComponent<Image>().sprite = factorData2.factorSprite;
				switch (factData2.factorID)
				{
				case 0:
					array[0] += factData2.factorPower;
					break;
				case 10:
					array[1] += factData2.factorPower;
					break;
				case 20:
					array[2] += factData2.factorPower;
					break;
				case 30:
					array[3] += factData2.factorPower;
					break;
				}
				num4++;
			}
			int num5 = 0;
			num5 = (flag ? GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit : GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit);
			craftCheckManager.infoFactorGroupText[0].text = dict2.Count.ToString();
			craftCheckManager.infoFactorGroupText[1].text = num5.ToString();
			break;
		}
		case 1:
		{
			List<HaveFactorData> dict = new List<HaveFactorData>();
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
				if (craftCanvasManager.isPowerUpCraft || craftCanvasManager.isCompleteEnhanceCount)
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict);
					num = 1;
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, craftManager.clickedUniqueID, ref dict);
					num = 0;
				}
				break;
			case "newCraft":
			{
				HaveFactorData item = new HaveFactorData
				{
					uniqueID = 0,
					factorID = craftCheckManager.newFactorID,
					factorLV = craftCheckManager.newPowerLevel,
					factorPower = craftCheckManager.newFactorPower
				};
				dict.Add(item);
				num = 3;
				break;
			}
			case "merge":
				flag = true;
				if (craftCanvasManager.isPowerUpCraft)
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, 0, ref dict);
					num = 1;
				}
				else
				{
					PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(craftManager.clickedItemID, 0, ref dict);
					num = 0;
				}
				break;
			}
			if (dict == null)
			{
				break;
			}
			int num2 = 0;
			foreach (HaveFactorData factData in dict)
			{
				Transform transform = PoolManager.Pools["Craft Item Pool"].Spawn(craftCheckManager.infoFactorScrollPrefab);
				ParameterContainer component = transform.GetComponent<ParameterContainer>();
				RefreshItemList(transform, num2);
				FactorData factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorID == factData.factorID);
				if (factorData != null)
				{
					transform.transform.Find("Text Group/Name Text").GetComponent<Localize>().Term = "factor_" + factorData.factorType;
					if (factorData.isAddPercentText)
					{
						transform.transform.Find("Text Group/Power Group/Power Text").GetComponent<Text>().text = factData.factorPower + "%";
					}
					else
					{
						transform.transform.Find("Text Group/Power Group/Power Text").GetComponent<Text>().text = factData.factorPower.ToString();
					}
					component.GetVariable<I2LocalizeComponent>("lvTextLoc").localize.Term = "craftGetFactorPower" + factData.factorLV;
				}
				transform.transform.Find("Icon Image").GetComponent<Image>().sprite = factorData.factorSprite;
				switch (factData.factorID)
				{
				case 200:
					array[0] += factData.factorPower;
					break;
				case 210:
					array[1] += factData.factorPower;
					break;
				case 220:
					array[2] += factData.factorPower;
					break;
				case 240:
					array[3] += factData.factorPower;
					break;
				}
				num2++;
			}
			int num3 = 0;
			num3 = (flag ? GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit : GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID).factorHaveLimit);
			craftCheckManager.infoFactorGroupText[0].text = dict.Count.ToString();
			craftCheckManager.infoFactorGroupText[1].text = num3.ToString();
			break;
		}
		}
		Transform transform3 = PoolManager.Pools["Craft Item Pool"].Spawn(craftCheckManager.craftedEffectPrefabGoArray[num], craftCheckManager.uIParticle.transform);
		craftCheckManager.craftedEffectSpawnGo = transform3;
		transform3.localPosition = new Vector3(0f, 0f, 0f);
		transform3.localScale = new Vector3(1f, 1f, 1f);
		craftCheckManager.uIParticle.RefreshParticles();
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
		GameObject infoFactorScrollContentGO = craftCheckManager.infoFactorScrollContentGO;
		transform.SetParent(infoFactorScrollContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetFactorSummaryTerm()
	{
		ParameterContainer component = craftCheckManager.craftedSummaryWindow.GetComponent<ParameterContainer>();
		if (craftCanvasManager.isPowerUpCraft)
		{
			component.GetVariable<I2LocalizeComponent>("factorSummaryTextLoc").localize.Term = "headerInheritFacorAll";
			component.GetVariable<I2LocalizeComponent>("factorSlotNumTextLoc").localize.Term = "summaryInheritFactorTagNum";
		}
		else if (craftCanvasManager.isCompleteEnhanceCount)
		{
			component.GetVariable<I2LocalizeComponent>("factorSummaryTextLoc").localize.Term = "headerInheritFacorAll";
			component.GetVariable<I2LocalizeComponent>("factorSlotNumTextLoc").localize.Term = "summaryInheritFactorTagNum";
		}
		else
		{
			component.GetVariable<I2LocalizeComponent>("factorSummaryTextLoc").localize.Term = "headerHaveFacorAll";
			component.GetVariable<I2LocalizeComponent>("factorSlotNumTextLoc").localize.Term = "summaryHaveFactorTagNum";
		}
	}
}
