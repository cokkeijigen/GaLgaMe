using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GetNewFactor : StateBehaviour
{
	private enum EquipType
	{
		weapon,
		armor
	}

	private CraftManager craftManager;

	private CraftCheckManager craftCheckManager;

	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponentInParent<CraftManager>();
		craftCheckManager = GetComponent<CraftCheckManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		FactorData.FactorType newFactorType = FactorData.FactorType.attackUp;
		int newFactorID = 0;
		bool flag = false;
		if (craftAddOnManager.selectedMagicMatrialID[0] != 0)
		{
			ItemMagicMaterialData itemMagicMaterialData = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData m) => m.itemID == craftAddOnManager.selectedMagicMatrialID[0]);
			flag = GetFactorTypeFromMaterial(itemMagicMaterialData.factorType, ref newFactorType);
		}
		else
		{
			switch (craftManager.selectCategoryNum)
			{
			case 0:
				newFactorType = GetRandomFactorType(EquipType.weapon);
				flag = true;
				break;
			case 1:
				newFactorType = GetRandomFactorType(EquipType.armor);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Debug.LogWarning("ファクタータイプの取得に失敗");
			newFactorType = FactorData.FactorType.attackUp;
		}
		FactorData factorData = null;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			newFactorID = 0;
			factorData = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData m) => m.factorType == newFactorType);
			break;
		case 1:
			newFactorID = 200;
			factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData m) => m.factorType == newFactorType);
			break;
		}
		if (factorData != null)
		{
			newFactorID = factorData.factorID;
		}
		craftCheckManager.newFactorID = newFactorID;
		craftCheckManager.newFactorType = newFactorType;
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

	private bool GetFactorTypeFromMaterial(ItemMagicMaterialData.FactorType materialFactorType, ref FactorData.FactorType factorDataType)
	{
		bool result = true;
		switch (materialFactorType)
		{
		case ItemMagicMaterialData.FactorType.attackUp:
			factorDataType = FactorData.FactorType.attackUp;
			break;
		case ItemMagicMaterialData.FactorType.magicAttackUp:
			factorDataType = FactorData.FactorType.magicAttackUp;
			break;
		case ItemMagicMaterialData.FactorType.accuracyUp:
			factorDataType = FactorData.FactorType.accuracyUp;
			break;
		case ItemMagicMaterialData.FactorType.criticalUp:
			factorDataType = FactorData.FactorType.criticalUp;
			break;
		case ItemMagicMaterialData.FactorType.hpUp:
			factorDataType = FactorData.FactorType.hpUp;
			break;
		case ItemMagicMaterialData.FactorType.mpUp:
			factorDataType = FactorData.FactorType.mpUp;
			break;
		case ItemMagicMaterialData.FactorType.skillPower:
			factorDataType = FactorData.FactorType.skillPower;
			break;
		case ItemMagicMaterialData.FactorType.defenseUp:
			factorDataType = FactorData.FactorType.defenseUp;
			break;
		case ItemMagicMaterialData.FactorType.magicDefenseUp:
			factorDataType = FactorData.FactorType.magicDefenseUp;
			break;
		case ItemMagicMaterialData.FactorType.evasionUp:
			factorDataType = FactorData.FactorType.evasionUp;
			break;
		case ItemMagicMaterialData.FactorType.criticalResistUp:
			factorDataType = FactorData.FactorType.criticalResistUp;
			break;
		case ItemMagicMaterialData.FactorType.parry:
			factorDataType = FactorData.FactorType.parry;
			break;
		case ItemMagicMaterialData.FactorType.vampire:
			factorDataType = FactorData.FactorType.vampire;
			break;
		case ItemMagicMaterialData.FactorType.abnormalResistUp:
			factorDataType = FactorData.FactorType.abnormalResistUp;
			break;
		case ItemMagicMaterialData.FactorType.mpSaving:
			factorDataType = FactorData.FactorType.mpSaving;
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	private FactorData.FactorType GetRandomFactorType(EquipType equip)
	{
		List<FactorData> list = new List<FactorData>();
		float num = 0f;
		list = ((equip != 0) ? GameDataManager.instance.factorDataBaseArmor.factorDataList : GameDataManager.instance.factorDataBaseWeapon.factorDataList);
		list = list.Where((FactorData data) => data.factorUnlockLv <= PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv).ToList();
		foreach (FactorData item in list)
		{
			num += item.probability;
			Debug.Log("ファクター名：" + item.factorName + "／取得ファクターLV：" + item.factorUnlockLv);
		}
		float num2 = Random.value * num;
		foreach (FactorData item2 in list)
		{
			if (num2 < item2.probability)
			{
				Debug.Log("引いたファクターは：" + item2.factorName);
				return item2.factorType;
			}
			num2 -= item2.probability;
		}
		Debug.Log("ファクタータイプ重み抽選で例外発生：" + num2);
		return list[0].factorType;
	}
}
