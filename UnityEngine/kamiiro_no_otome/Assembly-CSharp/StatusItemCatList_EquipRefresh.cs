using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class StatusItemCatList_EquipRefresh : StateBehaviour
{
	private StatusManager statusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponent<StatusManager>();
	}

	public override void OnStateBegin()
	{
		GameObject itemContentGO = statusManager.itemContentGO;
		if (statusManager.selectItemCategoryNum == 7 || statusManager.selectItemCategoryNum == 8 || statusManager.selectItemCategoryNum == 9)
		{
			foreach (Transform item in itemContentGO.transform)
			{
				if (!item.gameObject.activeSelf)
				{
					continue;
				}
				StatusItemListClickAction itemIDset = item.GetComponent<StatusItemListClickAction>();
				if (!(itemIDset != null))
				{
					continue;
				}
				switch (statusManager.selectItemCategoryNum)
				{
				case 7:
				{
					HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == itemIDset.itemID && m.itemUniqueID == itemIDset.instanceID);
					if (haveWeaponData != null)
					{
						if (haveWeaponData.equipCharacter == 0)
						{
							item.Find("Equip Icon Image").gameObject.SetActive(value: true);
						}
						else
						{
							item.Find("Equip Icon Image").gameObject.SetActive(value: false);
						}
					}
					break;
				}
				case 8:
				{
					HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == itemIDset.itemID && m.itemUniqueID == itemIDset.instanceID);
					if (haveArmorData != null)
					{
						if (haveArmorData.equipCharacter == 0)
						{
							item.Find("Equip Icon Image").gameObject.SetActive(value: true);
						}
						else
						{
							item.Find("Equip Icon Image").gameObject.SetActive(value: false);
						}
					}
					break;
				}
				case 9:
				{
					HaveAccessoryData haveAccessoryData = PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData m) => m.itemID == itemIDset.itemID);
					if (haveAccessoryData.equipCharacter != 9)
					{
						int equipCharacter = haveAccessoryData.equipCharacter;
						item.Find("Equip Name Text").GetComponent<Localize>().Term = "character" + equipCharacter;
						item.Find("Equip Icon Image").gameObject.SetActive(value: true);
					}
					else
					{
						item.Find("Equip Name Text").GetComponent<Localize>().Term = "empty";
						item.Find("Equip Icon Image").gameObject.SetActive(value: false);
					}
					break;
				}
				}
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
