using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckNeedMpRecovery : StateBehaviour
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
		int num = 0;
		if (PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData data) => data.itemID == 850) != null)
		{
			num = PlayerInventoryDataManager.haveItemMagicMaterialList.Find((HaveItemData data) => data.itemID == 850).haveCountNum;
		}
		craftExtensionManager.havePowderText.text = num.ToString();
		ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftExtensionManager.clickedItemID);
		if (num < itemWeaponData.powderCost)
		{
			SetButtonActive(0, isEnable: false);
			Debug.Log("一個使用ボタンを使用不可にする／コスト：" + itemWeaponData.powderCost);
		}
		else
		{
			SetButtonActive(0, isEnable: true);
			Debug.Log("一個使用ボタンを使用可能にする");
		}
		if (num < craftExtensionManager.needAllMpRecoveryPowderNum)
		{
			SetButtonActive(1, isEnable: false);
		}
		else
		{
			SetButtonActive(1, isEnable: true);
		}
		Transform child = craftExtensionManager.scrollContentGoArray[0].transform.GetChild(craftExtensionManager.clickedScrollContentIndex);
		ExtensionItemListClickAction clickScript = child.GetComponent<ExtensionItemListClickAction>();
		HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == clickScript.itemID && data.itemUniqueID == clickScript.instanceID);
		if (haveWeaponData.weaponIncludeMp >= haveWeaponData.weaponIncludeMaxMp)
		{
			SetButtonActive(0, isEnable: false);
		}
		int num2 = 0;
		int num3 = 0;
		foreach (HaveWeaponData haveWeapon in PlayerInventoryDataManager.haveWeaponList)
		{
			num2 += haveWeapon.weaponIncludeMp;
			num3 += haveWeapon.weaponIncludeMaxMp;
		}
		if (num2 >= num3)
		{
			SetButtonActive(1, isEnable: false);
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

	private void SetButtonActive(int index, bool isEnable)
	{
		if (isEnable)
		{
			craftExtensionManager.mpRecoveryButtonArray[index].alpha = 1f;
			craftExtensionManager.mpRecoveryButtonArray[index].interactable = true;
		}
		else
		{
			craftExtensionManager.mpRecoveryButtonArray[index].alpha = 0.5f;
			craftExtensionManager.mpRecoveryButtonArray[index].interactable = false;
		}
	}
}
