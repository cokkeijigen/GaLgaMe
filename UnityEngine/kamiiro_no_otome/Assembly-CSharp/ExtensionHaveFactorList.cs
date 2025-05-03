using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class ExtensionHaveFactorList : StateBehaviour
{
	private CraftExtensionManager craftExtensionManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftExtensionManager = GameObject.Find("Craft Extension Manager").GetComponent<CraftExtensionManager>();
	}

	public override void OnStateBegin()
	{
		ResetHaveFactorList();
		List<HaveFactorData> dict = null;
		List<HaveFactorData> dict2 = null;
		PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(craftExtensionManager.clickedItemID, craftExtensionManager.clickedItemUniqueID, ref dict);
		PlayerInventoryFactorManager.GetPlayerHaveWeaponSetFactorList(craftExtensionManager.clickedItemID, craftExtensionManager.clickedItemUniqueID, ref dict2);
		if (dict != null)
		{
			int num = 0;
			foreach (HaveFactorData factDat in dict)
			{
				Transform transform = PoolManager.Pools["Extension Item Pool"].Spawn(craftExtensionManager.scrollPrefabGoArray[1]);
				RefreshItemList(transform, num);
				FactorData factorData = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorID == factDat.factorID);
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
			int factorHaveLimit = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftExtensionManager.clickedItemID).factorHaveLimit;
			craftExtensionManager.haveFactorTextArray[0].text = dict.Count.ToString();
			craftExtensionManager.haveFactorTextArray[1].text = factorHaveLimit.ToString();
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
		transform.SetParent(craftExtensionManager.scrollContentGoArray[1].transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	public void ResetHaveFactorList()
	{
		int childCount = craftExtensionManager.scrollContentGoArray[1].transform.childCount;
		Transform[] array = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = craftExtensionManager.scrollContentGoArray[1].transform.GetChild(i);
		}
		Transform[] array2 = array;
		foreach (Transform instance in array2)
		{
			PoolManager.Pools["Extension Item Pool"].Despawn(instance, 0f, craftExtensionManager.spawnPoolParent.transform);
		}
		craftExtensionManager.haveFactorTextArray[0].text = "0";
		craftExtensionManager.haveFactorTextArray[1].text = "0";
	}
}
