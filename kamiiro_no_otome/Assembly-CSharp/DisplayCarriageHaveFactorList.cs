using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DisplayCarriageHaveFactorList : StateBehaviour
{
	private CarriageManager carriageManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
	}

	public override void OnStateBegin()
	{
		ResetHaveFactorList(0);
		switch (carriageManager.selectCategoryNum)
		{
		case 0:
		{
			List<HaveFactorData> dict3 = null;
			List<HaveFactorData> dict4 = null;
			PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(carriageManager.clickedItemID, carriageManager.clickedUniqueID, ref dict3);
			PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(carriageManager.clickedItemID, carriageManager.clickedUniqueID, ref dict4);
			if (dict3 == null)
			{
				break;
			}
			int num2 = 0;
			foreach (HaveFactorData factDat2 in dict3)
			{
				Transform transform2 = PoolManager.Pools["Carriage Item Pool"].Spawn(carriageManager.factorItemPrefabGo);
				RefreshItemList(transform2, num2);
				FactorData factorData2 = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == factDat2.factorID);
				if (factorData2 != null)
				{
					ParameterContainer component2 = transform2.GetComponent<ParameterContainer>();
					component2.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "factor_" + factorData2.factorType;
					component2.GetVariable<UguiTextVariable>("powerText").text.text = factDat2.factorPower.ToString();
					component2.GetVariable<I2LocalizeComponent>("lvTextLoc").localize.Term = "craftGetFactorPower" + factDat2.factorLV;
					component2.GetVariable<UguiImage>("iconImage").image.sprite = factorData2.factorSprite;
					if (dict4.Find((HaveFactorData data) => data.uniqueID == factDat2.uniqueID) != null)
					{
						component2.GetGameObject("equipIconGo").SetActive(value: true);
					}
					else
					{
						component2.GetGameObject("equipIconGo").SetActive(value: false);
					}
				}
				num2++;
			}
			int factorHaveLimit2 = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == carriageManager.clickedItemID).factorHaveLimit;
			carriageManager.infoFactorGroupText[0].text = dict3.Count.ToString();
			carriageManager.infoFactorGroupText[1].text = factorHaveLimit2.ToString();
			break;
		}
		case 1:
		{
			List<HaveFactorData> dict = null;
			List<HaveFactorData> dict2 = null;
			PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(carriageManager.clickedItemID, carriageManager.clickedUniqueID, ref dict);
			PlayerInventoryFactorManager.GetPlayerHaveArmorSetFactorList(carriageManager.clickedItemID, carriageManager.clickedUniqueID, ref dict2);
			if (dict == null)
			{
				break;
			}
			int num = 0;
			foreach (HaveFactorData factDat in dict)
			{
				Transform transform = PoolManager.Pools["Carriage Item Pool"].Spawn(carriageManager.factorItemPrefabGo);
				RefreshItemList(transform, num);
				FactorData factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorID == factDat.factorID);
				if (factorData != null)
				{
					ParameterContainer component = transform.GetComponent<ParameterContainer>();
					component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "factor_" + factorData.factorType;
					component.GetVariable<UguiTextVariable>("powerText").text.text = factDat.factorPower.ToString();
					component.GetVariable<I2LocalizeComponent>("lvTextLoc").localize.Term = "craftGetFactorPower" + factDat.factorLV;
					component.GetVariable<UguiImage>("iconImage").image.sprite = factorData.factorSprite;
					if (dict2.Find((HaveFactorData data) => data.uniqueID == factDat.uniqueID) != null)
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
			int factorHaveLimit = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == carriageManager.clickedItemID).factorHaveLimit;
			carriageManager.infoFactorGroupText[0].text = dict.Count.ToString();
			carriageManager.infoFactorGroupText[1].text = factorHaveLimit.ToString();
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
		GameObject factorScrollContentGo = carriageManager.factorScrollContentGo;
		transform.SetParent(factorScrollContentGo.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void ResetHaveFactorList(int MaxVal)
	{
		Transform[] array = new Transform[carriageManager.factorScrollContentGo.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = carriageManager.factorScrollContentGo.transform.GetChild(i);
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (PoolManager.Pools["Carriage Item Pool"].IsSpawned(array[j]))
			{
				PoolManager.Pools["Carriage Item Pool"].Despawn(array[j], 0f, carriageManager.poolParentGo.transform);
			}
		}
		carriageManager.infoFactorGroupText[0].text = "0";
		carriageManager.infoFactorGroupText[1].text = MaxVal.ToString();
	}
}
