using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class ApplyMpRecovery : StateBehaviour
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
		if (!craftExtensionManager.isAllMpRecovery)
		{
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == craftExtensionManager.clickedItemID && data.itemUniqueID == craftExtensionManager.clickedItemUniqueID);
			haveWeaponData.weaponIncludeMp = Mathf.Clamp(haveWeaponData.weaponIncludeMaxMp, 0, 999);
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftExtensionManager.clickedItemID);
			PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData data) => data.itemID == 850).haveCountNum -= itemWeaponData.powderCost;
		}
		else
		{
			foreach (HaveWeaponData haveWeapon in PlayerInventoryDataManager.haveWeaponList)
			{
				haveWeapon.weaponIncludeMp = Mathf.Clamp(haveWeapon.weaponIncludeMaxMp, 0, 999);
			}
			PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData data) => data.itemID == 850).haveCountNum -= craftExtensionManager.needAllMpRecoveryPowderNum;
		}
		MasterAudio.PlaySound("SeMpRecovery", 1f, null, 0f, null, null);
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
