using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ShopFactorCustomListRefresh : StateBehaviour
{
	private ShopManager shopManager;

	private ShopCustomManager shopCustomManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
		shopCustomManager = GameObject.Find("Shop Custom Manager").GetComponent<ShopCustomManager>();
	}

	public override void OnStateBegin()
	{
		shopCustomManager.ShopCustomScrollItemDesapwnAll();
		List<HaveFactorData> dict = new List<HaveFactorData>();
		List<HaveFactorData> list = new List<HaveFactorData>();
		HaveWeaponData haveWeaponData = null;
		HaveArmorData haveArmorData = null;
		int num = 0;
		int num2 = 0;
		if (shopManager.clickedItemID < 2000)
		{
			int itemID = shopManager.clickedItemID;
			int uniqueID = shopManager.clickedItemUniqueID;
			PlayerInventoryFactorManager.GetPlayerHaveWeaponFactorList(itemID, uniqueID, ref dict);
			haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == itemID && data.itemUniqueID == uniqueID);
			list = haveWeaponData.weaponSetFactor;
			num2 = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID).factorSlot;
		}
		else
		{
			int itemID2 = shopManager.clickedItemID;
			int uniqueID2 = shopManager.clickedItemUniqueID;
			PlayerInventoryFactorManager.GetPlayerHaveArmorFactorList(itemID2, uniqueID2, ref dict);
			haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == itemID2 && data.itemUniqueID == uniqueID2);
			list = haveArmorData.armorSetFactor;
			num2 = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == itemID2).factorSlot;
		}
		shopCustomManager.tempEquipFactorList = list;
		num = list.Count();
		shopCustomManager.skillSlotNumArray[0] = num;
		shopCustomManager.skillSlotNumArray[1] = num2;
		shopCustomManager.SetCustomSlotNumText();
		for (int i = 0; i < dict.Count; i++)
		{
			FactorData factorData = null;
			HaveFactorData factorData2 = dict[i];
			factorData = ((factorData2.factorID >= 200) ? GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorID == factorData2.factorID) : GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorID == factorData2.factorID));
			Transform transform = PoolManager.Pools["Shop Pool Item"].Spawn(shopCustomManager.customScrollPrefabArray[1]);
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			RefreshItemList(transform, i);
			string term = "factor_" + factorData.factorType;
			component.GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
			component.GetVariable<UguiTextVariable>("powerText").text.text = factorData2.factorPower.ToString();
			component.GetVariable<I2LocalizeComponent>("lvText").localize.Term = "craftGetFactorPower" + factorData2.factorLV;
			bool active = ((shopManager.clickedItemID >= 2000) ? haveArmorData.armorSetFactor.Contains(factorData2) : haveWeaponData.weaponSetFactor.Contains(factorData2));
			component.GetGameObject("equipIconImage").SetActive(active);
			Sprite factorSprite = factorData.factorSprite;
			component.GetVariable<UguiImage>("iconImage").image.sprite = factorSprite;
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
		transform.SetParent(shopCustomManager.shopCustomContentGo);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void ResetItemListSlider()
	{
		shopCustomManager.customWindowArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}
}
