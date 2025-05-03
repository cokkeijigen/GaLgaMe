using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushStatusEquipButton : StateBehaviour
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
		switch (statusManager.selectItemCategoryNum)
		{
		case 7:
		{
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.itemID == statusManager.selectItemId && m.itemUniqueID == statusManager.selectItemUniqueId);
			if (haveWeaponData != null && haveWeaponData.equipCharacter == 9)
			{
				HaveWeaponData haveWeaponData2 = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData m) => m.equipCharacter == 0);
				if (haveWeaponData2 != null)
				{
					haveWeaponData2.equipCharacter = 9;
				}
				haveWeaponData.equipCharacter = 0;
				PlayerEquipDataManager.SetEquipPlayerWeapon(statusManager.selectItemId, statusManager.selectItemUniqueId, 0);
			}
			break;
		}
		case 8:
		{
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.itemID == statusManager.selectItemId && m.itemUniqueID == statusManager.selectItemUniqueId);
			if (haveArmorData != null && haveArmorData.equipCharacter == 9)
			{
				HaveArmorData haveArmorData2 = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData m) => m.equipCharacter == 0);
				if (haveArmorData2 != null)
				{
					haveArmorData2.equipCharacter = 9;
				}
				haveArmorData.equipCharacter = 0;
				PlayerEquipDataManager.SetEquipPlayerArmor(statusManager.selectItemId, statusManager.selectItemUniqueId, 0);
			}
			break;
		}
		case 9:
		{
			HaveAccessoryData haveAccessoryData = PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData m) => m.itemID == statusManager.selectItemId);
			if (haveAccessoryData == null)
			{
				break;
			}
			if (haveAccessoryData.equipCharacter != statusManager.selectCharacterNum)
			{
				HaveAccessoryData haveAccessoryData2 = PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData m) => m.equipCharacter == statusManager.selectCharacterNum);
				if (haveAccessoryData2 != null)
				{
					haveAccessoryData2.equipCharacter = 9;
				}
				haveAccessoryData.equipCharacter = statusManager.selectCharacterNum;
				PlayerEquipDataManager.SetEquipPlayerAccessory(statusManager.selectItemId, statusManager.selectCharacterNum);
			}
			else
			{
				haveAccessoryData.equipCharacter = 9;
				PlayerEquipDataManager.playerEquipAccessoryID[statusManager.selectCharacterNum] = 0;
			}
			break;
		}
		}
		PlayerNonSaveDataManager.isEquipItemChange = true;
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
