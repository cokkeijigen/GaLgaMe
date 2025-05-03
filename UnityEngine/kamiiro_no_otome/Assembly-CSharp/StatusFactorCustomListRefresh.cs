using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusFactorCustomListRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	public OutputSlotBool outputSlotBool;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponent<StatusCustomManager>();
	}

	public override void OnStateBegin()
	{
		statusManager.ResetScrollViewContents(statusCustomManager.statusCustomContentGo.transform, isCustom: true);
		List<HaveFactorData> dict = new List<HaveFactorData>();
		List<HaveFactorData> list = new List<HaveFactorData>();
		HaveWeaponData haveWeaponData = null;
		HaveArmorData haveArmorData = null;
		HavePartyWeaponData havePartyWeaponData = null;
		HavePartyArmorData havePartyArmorData = null;
		bool flag = false;
		int num = 0;
		int num2 = 0;
		if (statusManager.selectCharacterNum == 0)
		{
			if (statusManager.selectItemId < 1300)
			{
				int itemID = statusManager.selectItemId;
				int uniqueID = statusManager.selectItemUniqueId;
				flag = true;
				PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(itemID, uniqueID, ref dict);
				haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == itemID && data.itemUniqueID == uniqueID);
				list = haveWeaponData.weaponSetFactor;
				num2 = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID).factorSlot;
			}
			else
			{
				int itemID2 = statusManager.selectItemId;
				int uniqueID2 = statusManager.selectItemUniqueId;
				flag = false;
				PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(itemID2, uniqueID2, ref dict);
				haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == itemID2 && data.itemUniqueID == uniqueID2);
				list = haveArmorData.armorSetFactor;
				num2 = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == itemID2).factorSlot;
			}
		}
		else if (statusManager.selectItemId < 2300)
		{
			int itemID3 = statusManager.selectItemId;
			flag = true;
			PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(itemID3, 0, ref dict);
			havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData data) => data.itemID == itemID3);
			list = havePartyWeaponData.weaponSetFactor;
			num2 = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == itemID3).factorSlot;
		}
		else
		{
			int itemID4 = statusManager.selectItemId;
			flag = false;
			PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(itemID4, 0, ref dict);
			havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData data) => data.itemID == itemID4);
			list = havePartyArmorData.armorSetFactor;
			num2 = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == itemID4).factorSlot;
		}
		statusCustomManager.tempEquipFactorList = new List<HaveFactorData>(list);
		outputSlotBool.SetValue(flag);
		num = list.Count();
		statusCustomManager.skillSolotNumArray[0] = num;
		statusCustomManager.skillSolotNumArray[1] = num2;
		statusCustomManager.SetCustomSlotNumText();
		for (int i = 0; i < dict.Count; i++)
		{
			FactorData factorData = null;
			HaveFactorData haveFactorData = dict[i];
			factorData = ((haveFactorData.factorID >= 200) ? GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorID == haveFactorData.factorID) : GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorID == haveFactorData.factorID));
			Transform transform = PoolManager.Pools["Status Custom Pool"].Spawn(statusCustomManager.customScrollPrefabArray[1]);
			RefreshItemList(transform, i);
			string term = "factor_" + factorData.factorType;
			transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
			if (factorData.isAddPercentText)
			{
				transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("powerText").text.text = haveFactorData.factorPower + "%";
			}
			else
			{
				transform.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("powerText").text.text = haveFactorData.factorPower.ToString();
			}
			transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("lvText").localize.Term = "craftGetFactorPower" + haveFactorData.factorLV;
			StatusFactorCustomListClickAction component = transform.GetComponent<StatusFactorCustomListClickAction>();
			component.isInitialized = false;
			component.factorData = haveFactorData;
			component.factorId = haveFactorData.factorID;
			component.factorUniqueId = haveFactorData.uniqueID;
			bool flag2 = false;
			if ((statusManager.selectCharacterNum == 0) ? ((statusManager.selectItemId < 1300) ? ((haveWeaponData.weaponSetFactor.Find((HaveFactorData data) => data.uniqueID == haveFactorData.uniqueID) != null) ? true : false) : ((haveArmorData.armorSetFactor.Find((HaveFactorData data) => data.uniqueID == haveFactorData.uniqueID) != null) ? true : false)) : ((statusManager.selectItemId < 2300) ? ((havePartyWeaponData.weaponSetFactor.Find((HaveFactorData data) => data.uniqueID == haveFactorData.uniqueID) != null) ? true : false) : ((havePartyArmorData.armorSetFactor.Find((HaveFactorData data) => data.uniqueID == haveFactorData.uniqueID) != null) ? true : false)))
			{
				transform.GetComponent<ParameterContainer>().GetVariable<UguiToggle>("equipToggle").toggle.isOn = true;
			}
			else
			{
				transform.GetComponent<ParameterContainer>().GetVariable<UguiToggle>("equipToggle").toggle.isOn = false;
			}
			Sprite factorSprite = factorData.factorSprite;
			transform.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = factorSprite;
			component.isInitialized = true;
		}
		ResetItemListSlider();
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
		transform.SetParent(statusCustomManager.statusCustomContentGo.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void ResetItemListSlider()
	{
		statusCustomManager.customWindowArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}
}
