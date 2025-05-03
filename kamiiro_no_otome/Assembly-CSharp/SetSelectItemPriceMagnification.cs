using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetSelectItemPriceMagnification : StateBehaviour
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
		PlayerDataManager.carriageStoreSellMagnification = carriageManager.tempSetPriceMagnification;
		switch (carriageManager.selectCategoryNum)
		{
		case 0:
		{
			for (int k = 0; k < carriageManager.itemSelectScrollContentGo.transform.childCount; k++)
			{
				Transform child2 = carriageManager.itemSelectScrollContentGo.transform.GetChild(k);
				ParameterContainer component2 = child2.GetComponent<ParameterContainer>();
				CarriageItemListClickAction carriageItemAction2 = child2.GetComponent<CarriageItemListClickAction>();
				HaveWeaponData haveWeaponData2 = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == carriageItemAction2.itemID && data.itemUniqueID == carriageItemAction2.instanceID);
				haveWeaponData2.sellPriceMagnification = carriageManager.tempSetPriceMagnification;
				int equipItemSellPrice3 = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(haveWeaponData2.itemID, haveWeaponData2.itemUniqueID);
				float num3 = (float)haveWeaponData2.sellPriceMagnification / 100f;
				float f3 = (float)equipItemSellPrice3 * num3;
				haveWeaponData2.sellPrice = Mathf.RoundToInt(f3);
				component2.GetVariable<UguiTextVariable>("priceText").text.text = haveWeaponData2.sellPrice.ToString();
			}
			for (int l = 0; l < PlayerInventoryDataManager.haveArmorList.Count; l++)
			{
				HaveArmorData haveArmorData2 = PlayerInventoryDataManager.haveArmorList[l];
				haveArmorData2.sellPriceMagnification = carriageManager.tempSetPriceMagnification;
				int equipItemSellPrice4 = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(haveArmorData2.itemID, haveArmorData2.itemUniqueID);
				float num4 = (float)haveArmorData2.sellPriceMagnification / 100f;
				float f4 = (float)equipItemSellPrice4 * num4;
				haveArmorData2.sellPrice = Mathf.RoundToInt(f4);
			}
			break;
		}
		case 1:
		{
			for (int i = 0; i < PlayerInventoryDataManager.haveWeaponList.Count; i++)
			{
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList[i];
				haveWeaponData.sellPriceMagnification = carriageManager.tempSetPriceMagnification;
				int equipItemSellPrice = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(haveWeaponData.itemID, haveWeaponData.itemUniqueID);
				float num = (float)haveWeaponData.sellPriceMagnification / 100f;
				float f = (float)equipItemSellPrice * num;
				haveWeaponData.sellPrice = Mathf.RoundToInt(f);
			}
			for (int j = 0; j < carriageManager.itemSelectScrollContentGo.transform.childCount; j++)
			{
				Transform child = carriageManager.itemSelectScrollContentGo.transform.GetChild(j);
				ParameterContainer component = child.GetComponent<ParameterContainer>();
				CarriageItemListClickAction carriageItemAction = child.GetComponent<CarriageItemListClickAction>();
				HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == carriageItemAction.itemID && data.itemUniqueID == carriageItemAction.instanceID);
				haveArmorData.sellPriceMagnification = carriageManager.tempSetPriceMagnification;
				int equipItemSellPrice2 = PlayerInventoryDataEquipAccess.GetEquipItemSellPrice(haveArmorData.itemID, haveArmorData.itemUniqueID);
				float num2 = (float)haveArmorData.sellPriceMagnification / 100f;
				float f2 = (float)equipItemSellPrice2 * num2;
				haveArmorData.sellPrice = Mathf.RoundToInt(f2);
				component.GetVariable<UguiTextVariable>("priceText").text.text = haveArmorData.sellPrice.ToString();
			}
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
}
