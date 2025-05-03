using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcStoreTendingResult : StateBehaviour
{
	public enum Type
	{
		weapon,
		armor
	}

	public Type type;

	public StateLink continueLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		List<HaveWeaponData> list = PlayerInventoryDataManager.haveWeaponList.Where((HaveWeaponData data) => data.isItemStoreDisplay).ToList();
		List<HaveArmorData> list2 = PlayerInventoryDataManager.haveArmorList.Where((HaveArmorData data) => data.isItemStoreDisplay).ToList();
		int num = 0;
		int num2 = 0;
		float num3 = 0f;
		float num4 = 0f;
		if (list.Any())
		{
			num = list.Count;
		}
		if (list2.Any())
		{
			num2 = list2.Count;
		}
		if (num > 0)
		{
			if (num2 > 0)
			{
				if (num > num2)
				{
					num3 = num / num2;
					num4 = 1f - num3;
				}
				else
				{
					num4 = num2 / num;
					num3 = 1f - num4;
				}
			}
			else
			{
				num3 = 1f;
			}
		}
		else
		{
			num4 = 1f;
		}
		int num5 = 0;
		int workShopLv = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv;
		switch (type)
		{
		case Type.weapon:
		{
			if (num <= 0)
			{
				break;
			}
			for (int j = 0; j < list.Count; j++)
			{
				int num10 = 0;
				int num11 = Mathf.Abs(list[j].sellPriceMagnification - 100);
				Debug.Log("倍率：" + list[j].sellPriceMagnification + "／倍率-100の値：" + num11);
				if (num11 != 0)
				{
					num10 = num11 / 10 * 4;
					if (list[j].sellPriceMagnification > 100)
					{
						num10 *= -1;
					}
				}
				if (PlayerDataManager.hotSellingCategoryNum == 0)
				{
					num10 += PlayerDataManager.hotSellingTradeBonus;
				}
				int num12 = Random.Range(0, 100);
				int num13 = num10 + PlayerCraftStatusManager.storeTendingProbability;
				num13 += PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv;
				num13 = Mathf.Clamp(num13, 0, 100);
				Debug.Log("ランダム：" + num12 + "／成功率：" + num13 + "／確率ボーナス：" + num10);
				if (num12 < num13)
				{
					int[] item2 = new int[2]
					{
						list[j].itemID,
						list[j].itemUniqueID
					};
					PlayerDataManager.storeTradeSuccessItemList.Add(item2);
					PlayerDataManager.carriageStoreTradeCount++;
					num5++;
					PlayerDataManager.carriageStoreTradeMoneyNum += list[j].sellPrice;
					PlayerInventoryDataEquipAccess.PlayerHaveWeaponRemove(list[j].itemID, list[j].itemUniqueID);
				}
				if (num5 >= workShopLv)
				{
					break;
				}
			}
			break;
		}
		case Type.armor:
		{
			if (num2 <= 0)
			{
				break;
			}
			for (int i = 0; i < list2.Count; i++)
			{
				int num6 = 0;
				int num7 = Mathf.Abs(list2[i].sellPriceMagnification - 100);
				Debug.Log("倍率：" + list2[i].sellPriceMagnification + "／倍率-100の値：" + num7);
				if (num7 != 0)
				{
					num6 = num7 / 10 * 4;
					if (list2[i].sellPriceMagnification > 100)
					{
						num6 *= -1;
					}
				}
				if (PlayerDataManager.hotSellingCategoryNum == 1)
				{
					num6 += PlayerDataManager.hotSellingTradeBonus;
				}
				int num8 = Random.Range(0, 100);
				int num9 = num6 + PlayerCraftStatusManager.storeTendingProbability;
				num9 += PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopLv;
				num9 = Mathf.Clamp(num9, 0, 100);
				Debug.Log("ランダム：" + num8 + "／成功率：" + num9 + "／確率ボーナス：" + num6);
				if (num8 < num9)
				{
					int[] item = new int[2]
					{
						list2[i].itemID,
						list2[i].itemUniqueID
					};
					PlayerDataManager.storeTradeSuccessItemList.Add(item);
					PlayerDataManager.carriageStoreTradeCount++;
					num5++;
					PlayerDataManager.carriageStoreTradeMoneyNum += list2[i].sellPrice;
					PlayerInventoryDataEquipAccess.PlayerHaveArmorRemove(list2[i].itemID, list2[i].itemUniqueID);
				}
				if (num5 >= workShopLv)
				{
					break;
				}
			}
			break;
		}
		}
		PlayerNonSaveDataManager.storeTendingRemainTime--;
		if (PlayerNonSaveDataManager.storeTendingRemainTime > 0)
		{
			Transition(continueLink);
		}
		else
		{
			Transition(stateLink);
		}
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
