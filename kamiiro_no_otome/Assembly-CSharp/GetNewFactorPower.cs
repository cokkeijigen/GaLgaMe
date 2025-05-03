using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GetNewFactorPower : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCheckManager craftCheckManager;

	private CraftAddOnManager craftAddOnManager;

	private int[] levelPossibilityArray = new int[4] { 100, 80, 50, 10 };

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
		FactorData factorData = null;
		int num = 0;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			factorData = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorID == craftCheckManager.newFactorID);
			break;
		case 1:
			factorData = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorID == craftCheckManager.newFactorID);
			break;
		}
		int minInclusive = 1;
		int num2 = 1;
		int num3 = 1;
		int num4 = 0;
		if (craftAddOnManager.selectedMagicMatrialID[1] != 0)
		{
			int addOnID = craftAddOnManager.selectedMagicMatrialID[1];
			num4 = GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData data) => data.itemID == addOnID).addOnPower;
		}
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "craft":
		case "newCraft":
			num3 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv;
			num2 = Mathf.Clamp(num3 + num4, 1, 4);
			minInclusive = Mathf.Clamp(num3 - 2 + num4, 1, 4);
			break;
		case "merge":
			num3 = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].workShopToolLv;
			num2 = Mathf.Clamp(num3 + num4, 1, 4);
			minInclusive = Mathf.Clamp(num3 - 2 + num4, 1, 4);
			break;
		}
		num = Random.Range(minInclusive, num2 + 1);
		Debug.Log("最小LV：" + minInclusive + "／最大LV：" + num2 + "／ツールLV：" + num3);
		int minInclusive2 = 0;
		int num5 = 0;
		int newFactorPower = 0;
		int num6 = 0;
		switch (num)
		{
		case 1:
			minInclusive2 = 1;
			num5 = factorData.powerList[0];
			newFactorPower = Random.Range(minInclusive2, num5 + 1);
			num6 = CalcCraftLvPower(num5);
			newFactorPower += num6;
			break;
		case 2:
			minInclusive2 = factorData.powerList[0];
			num5 = factorData.powerList[1];
			newFactorPower = Random.Range(minInclusive2, num5 + 1);
			num6 = PlayerCraftStatusManager.playerCraftLv / num5;
			newFactorPower += num6;
			break;
		case 3:
			minInclusive2 = factorData.powerList[1];
			num5 = factorData.powerList[2];
			newFactorPower = Random.Range(minInclusive2, num5 + 1);
			num6 = PlayerCraftStatusManager.playerCraftLv / num5;
			newFactorPower += num6;
			break;
		case 4:
			minInclusive2 = factorData.powerList[2];
			num5 = factorData.powerList[3];
			newFactorPower = Random.Range(minInclusive2, num5 + 1);
			num6 = PlayerCraftStatusManager.playerCraftLv / num5;
			newFactorPower += num6;
			break;
		}
		Debug.Log("パワーLV：" + num + "／最小値：" + minInclusive2 + "／最大値：" + num5 + "／追加の値：" + num6);
		craftCheckManager.newPowerLevel = num;
		craftCheckManager.newFactorPower = newFactorPower;
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

	private int CalcCraftLvPower(int maxPower)
	{
		return Mathf.RoundToInt(PlayerCraftStatusManager.playerCraftLv / 50 * maxPower);
	}
}
