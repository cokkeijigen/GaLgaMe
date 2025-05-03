using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEquipDataManager : SerializedBehaviour
{
	public static int[] playerEquipWeaponID = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] playerEquipArmorID = new int[PlayerStatusDataManager.partyMemberCount];

	public static int playerEquipWeaponUniqueID;

	public static int playerEquipArmorUniqueID;

	public static int[] playerEquipAccessoryID = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipWeaponAttack = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipWeaponMagicAttack = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipWeaponCritical = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipWeaponAccuracy = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipWeaponComboProbability = new int[PlayerStatusDataManager.partyMemberCount];

	public static int equipWeaponMP;

	public static int[] equipArmorDefense = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipArmorMagicDefense = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipArmorEvasion = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipArmorResist = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipArmorRecoveryMP = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorAttackUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorMagicAttackUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorAccuracyUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorCriticalUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorPoison = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorParalyze = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorStagger = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorDeath = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorHP = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorMP = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorSkillPowerUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorDefenseUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorMagicDefenseUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorEvasionUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorCriticalResist = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorParry = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorVampire = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorResist = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] equipFactorRecoveryMp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryResistPoison = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryResistParalyze = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryResistUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryResistAll = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryResistDebuff = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryAgilityUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryWeakPointDamageUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessoryVampire = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessorySkillCritical = new int[PlayerStatusDataManager.partyMemberCount];

	public static int[] accessorySkillPowerUp = new int[PlayerStatusDataManager.partyMemberCount];

	public static int accessoryExpUp;

	public static int accessoryDropMoneyUp;

	public static int accessoryItemDiscover;

	public static int accessoryLibidoUpRate;

	public static List<List<int>> playerHaveSkillList = new List<List<int>>();

	public static List<int>[] playerEquipSkillList = new List<int>[7]
	{
		new List<int>(),
		new List<int>(),
		new List<int>(),
		new List<int>(),
		new List<int>(),
		new List<int>(),
		new List<int>()
	};

	public static void CheckPlayerSkillLevelUnLock()
	{
		playerHaveSkillList.Clear();
		int i;
		for (i = 0; i < PlayerStatusDataManager.partyMemberCount; i++)
		{
			List<BattleSkillData> list = GameDataManager.instance.playerSkillDataBase.skillDataList.Where((BattleSkillData data) => data.useCharacterID == i).ToList();
			List<int> list2 = new List<int>();
			for (int j = 0; j < list.Count(); j++)
			{
				int unlockLv = list[j].unlockLv;
				Debug.Log("確認スキルID：" + list[j].skillID + "／アンロックLV：" + unlockLv + "／キャラID：" + i + "／キャラLV：" + PlayerStatusDataManager.characterLv[i]);
				if (!string.IsNullOrEmpty(list[j].learnScenarioName))
				{
					if (PlayerFlagDataManager.scenarioFlagDictionary[list[j].learnScenarioName])
					{
						list2.Add(list[j].skillID);
					}
				}
				else if (PlayerStatusDataManager.characterLv[i] >= unlockLv)
				{
					list2.Add(list[j].skillID);
				}
			}
			playerHaveSkillList.Add(list2);
		}
		List<BattleSkillData> list3 = GameDataManager.instance.playerSkillDataBase.skillDataList.Where((BattleSkillData data) => data.useCharacterID == PlayerStatusDataManager.partyMemberCount).ToList();
		List<int> list4 = new List<int>();
		for (int k = 0; k < list3.Count(); k++)
		{
			int unlockLv2 = list3[k].unlockLv;
			if (PlayerStatusDataManager.characterLv[0] >= unlockLv2)
			{
				list4.Add(list3[k].skillID);
			}
		}
		List<BattleSkillData> list5 = GameDataManager.instance.playerSkillDataBase.skillDataList.Where((BattleSkillData data) => data.useCharacterID == 999).ToList();
		List<int> list6 = new List<int>();
		for (int l = 0; l < list5.Count(); l++)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[list5[l].learnScenarioName])
			{
				list6.Add(list5[l].skillID);
			}
		}
		List<int> list7 = new List<int>();
		list7 = list4.Concat(list6).ToList();
		list7.Sort();
		playerHaveSkillList.Add(list7);
		Debug.Log("スキルのLVでのアンロックを確認完了");
	}

	public static void SetEquipPlayerWeapon(int itemID, int UniqueID, int characterNum)
	{
		playerEquipWeaponID[characterNum] = itemID;
		playerEquipWeaponUniqueID = UniqueID;
		if (characterNum == 0)
		{
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID);
			equipWeaponAttack[characterNum] = itemWeaponData.attackPower;
			equipWeaponMagicAttack[characterNum] = itemWeaponData.magicAttackPower;
			equipWeaponCritical[characterNum] = itemWeaponData.critical;
			equipWeaponAccuracy[characterNum] = itemWeaponData.accuracy;
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == itemID && data.itemUniqueID == UniqueID);
			int weaponMp = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == itemID).weaponMp;
			equipWeaponMP = Mathf.Clamp(haveWeaponData.weaponIncludeMp, 0, 999);
			haveWeaponData.weaponIncludeMaxMp = Mathf.Clamp(weaponMp, 0, 999);
			Debug.Log("エデン装備武器ID：" + itemID + "／現在のMP：" + equipWeaponMP + "／最大MP：" + weaponMp);
		}
		else
		{
			ItemPartyWeaponData itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == itemID);
			equipWeaponAttack[characterNum] = itemPartyWeaponData.attackPower;
			equipWeaponMagicAttack[characterNum] = itemPartyWeaponData.magicAttackPower;
			equipWeaponCritical[characterNum] = itemPartyWeaponData.critical;
			equipWeaponAccuracy[characterNum] = itemPartyWeaponData.accuracy;
			equipWeaponComboProbability[characterNum] = itemPartyWeaponData.comboProbability;
		}
	}

	public static void SetEquipPlayerArmor(int itemID, int UniqueID, int characterNum)
	{
		playerEquipArmorID[characterNum] = itemID;
		playerEquipArmorUniqueID = UniqueID;
		if (characterNum == 0)
		{
			ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == itemID);
			equipArmorDefense[characterNum] = itemArmorData.defensePower;
			equipArmorMagicDefense[characterNum] = itemArmorData.magicDefensePower;
			equipArmorEvasion[characterNum] = itemArmorData.evasion;
			equipArmorResist[characterNum] = itemArmorData.abnormalResist;
			equipArmorRecoveryMP[characterNum] = itemArmorData.recoveryMp;
		}
		else
		{
			ItemPartyArmorData itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == itemID);
			equipArmorDefense[characterNum] = itemPartyArmorData.defensePower;
			equipArmorMagicDefense[characterNum] = itemPartyArmorData.magicDefensePower;
			equipArmorEvasion[characterNum] = itemPartyArmorData.evasion;
			equipArmorResist[characterNum] = itemPartyArmorData.abnormalResist;
			equipArmorRecoveryMP[characterNum] = itemPartyArmorData.recoveryMp;
		}
	}

	public static void SetEquipPlayerAccessory(int itemID, int characterNum)
	{
		int num = 0;
		num = Array.IndexOf(playerEquipAccessoryID, itemID);
		if (num != -1)
		{
			playerEquipAccessoryID[num] = 0;
		}
		playerEquipAccessoryID[characterNum] = itemID;
	}

	public static void CalcEquipPlayerHaveWeaponFactor(UnityAction unityAction, bool isAllCalc)
	{
		int i;
		for (i = 0; i < PlayerStatusDataManager.partyMemberCount; i++)
		{
			List<HaveFactorData> source = new List<HaveFactorData>();
			equipFactorAttackUp[i] = 0;
			equipFactorMagicAttackUp[i] = 0;
			equipFactorAccuracyUp[i] = 0;
			equipFactorCriticalUp[i] = 0;
			equipFactorPoison[i] = 0;
			equipFactorParalyze[i] = 0;
			equipFactorStagger[i] = 0;
			equipFactorDeath[i] = 0;
			equipFactorHP[i] = 0;
			equipFactorMP[i] = 0;
			equipFactorSkillPowerUp[i] = 0;
			if (i == 0)
			{
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.equipCharacter == 0);
				if (haveWeaponData != null)
				{
					source = haveWeaponData.weaponSetFactor;
				}
			}
			else
			{
				HavePartyWeaponData havePartyWeaponData = PlayerInventoryDataManager.havePartyWeaponList.Find((HavePartyWeaponData data) => data.itemID == playerEquipWeaponID[i]);
				if (havePartyWeaponData != null)
				{
					source = havePartyWeaponData.weaponSetFactor;
				}
			}
			List<int> list = (from data in source
				where data.factorID == 0
				select data.factorPower).ToList();
			if (list != null)
			{
				int factorPowerLimit = GetFactorPowerLimit(0);
				equipFactorAttackUp[i] = Mathf.Clamp(list.Sum(), 0, factorPowerLimit);
			}
			List<int> list2 = (from data in source
				where data.factorID == 10
				select data.factorPower).ToList();
			if (list2 != null)
			{
				int factorPowerLimit2 = GetFactorPowerLimit(10);
				equipFactorMagicAttackUp[i] = Mathf.Clamp(list2.Sum(), 0, factorPowerLimit2);
			}
			List<int> list3 = (from data in source
				where data.factorID == 20
				select data.factorPower).ToList();
			if (list3 != null)
			{
				int factorPowerLimit3 = GetFactorPowerLimit(20);
				equipFactorAccuracyUp[i] = Mathf.Clamp(list3.Sum(), 0, factorPowerLimit3);
			}
			List<int> list4 = (from data in source
				where data.factorID == 30
				select data.factorPower).ToList();
			if (list4 != null)
			{
				int factorPowerLimit4 = GetFactorPowerLimit(30);
				equipFactorCriticalUp[i] = Mathf.Clamp(list4.Sum(), 0, factorPowerLimit4);
			}
			List<int> list5 = (from data in source
				where data.factorID == 40
				select data.factorPower).ToList();
			if (list5 != null)
			{
				int factorPowerLimit5 = GetFactorPowerLimit(40);
				equipFactorPoison[i] = Mathf.Clamp(list5.Sum(), 0, factorPowerLimit5);
			}
			List<int> list6 = (from data in source
				where data.factorID == 50
				select data.factorPower).ToList();
			if (list6 != null)
			{
				int factorPowerLimit6 = GetFactorPowerLimit(50);
				equipFactorParalyze[i] = Mathf.Clamp(list6.Sum(), 0, factorPowerLimit6);
			}
			List<int> list7 = (from data in source
				where data.factorID == 60
				select data.factorPower).ToList();
			if (list7 != null)
			{
				int factorPowerLimit7 = GetFactorPowerLimit(60);
				equipFactorStagger[i] = Mathf.Clamp(list7.Sum(), 0, factorPowerLimit7);
			}
			List<int> list8 = (from data in source
				where data.factorID == 70
				select data.factorPower).ToList();
			if (list8 != null)
			{
				int factorPowerLimit8 = GetFactorPowerLimit(70);
				equipFactorDeath[i] = Mathf.Clamp(list8.Sum(), 0, factorPowerLimit8);
			}
			List<int> list9 = (from data in source
				where data.factorID == 500
				select data.factorPower).ToList();
			if (list9 != null)
			{
				int factorPowerLimit9 = GetFactorPowerLimit(500);
				equipFactorHP[i] = Mathf.Clamp(list9.Sum(), 0, factorPowerLimit9);
			}
			List<int> list10 = (from data in source
				where data.factorID == 600
				select data.factorPower).ToList();
			if (list10 != null)
			{
				int factorPowerLimit10 = GetFactorPowerLimit(600);
				equipFactorMP[i] = Mathf.Clamp(list10.Sum(), 0, factorPowerLimit10);
			}
			List<int> list11 = (from data in source
				where data.factorID == 700
				select data.factorPower).ToList();
			if (list11 != null)
			{
				int factorPowerLimit11 = GetFactorPowerLimit(700);
				equipFactorSkillPowerUp[i] = Mathf.Clamp(list11.Sum(), 0, factorPowerLimit11);
			}
		}
		Debug.Log("武器のファクターデータ計算完了");
		if (isAllCalc)
		{
			CalcEquipPlayerHaveArmorFactor(unityAction, isAllCalc: true);
		}
		else
		{
			unityAction();
		}
	}

	public static void CalcEquipPlayerHaveArmorFactor(UnityAction unityAction, bool isAllCalc)
	{
		int i;
		for (i = 0; i < PlayerStatusDataManager.partyMemberCount; i++)
		{
			List<HaveFactorData> source = new List<HaveFactorData>();
			equipFactorDefenseUp[i] = 0;
			equipFactorMagicDefenseUp[i] = 0;
			equipFactorEvasionUp[i] = 0;
			equipFactorCriticalResist[i] = 0;
			equipFactorParry[i] = 0;
			equipFactorVampire[i] = 0;
			equipFactorResist[i] = 0;
			equipFactorRecoveryMp[0] = 0;
			if (i == 0)
			{
				HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.equipCharacter == 0);
				if (haveArmorData != null)
				{
					source = haveArmorData.armorSetFactor;
				}
			}
			else
			{
				HavePartyArmorData havePartyArmorData = PlayerInventoryDataManager.havePartyArmorList.Find((HavePartyArmorData data) => data.itemID == playerEquipArmorID[i]);
				if (havePartyArmorData != null)
				{
					source = havePartyArmorData.armorSetFactor;
				}
			}
			List<int> list = (from data in source
				where data.factorID == 200
				select data.factorPower).ToList();
			if (list != null)
			{
				int factorPowerLimit = GetFactorPowerLimit(200);
				equipFactorDefenseUp[i] = Mathf.Clamp(list.Sum(), 0, factorPowerLimit);
			}
			List<int> list2 = (from data in source
				where data.factorID == 210
				select data.factorPower).ToList();
			if (list2 != null)
			{
				int factorPowerLimit2 = GetFactorPowerLimit(210);
				equipFactorMagicDefenseUp[i] = Mathf.Clamp(list2.Sum(), 0, factorPowerLimit2);
			}
			List<int> list3 = (from data in source
				where data.factorID == 220
				select data.factorPower).ToList();
			if (list3 != null)
			{
				int factorPowerLimit3 = GetFactorPowerLimit(220);
				equipFactorEvasionUp[i] = Mathf.Clamp(list3.Sum(), 0, factorPowerLimit3);
			}
			List<int> list4 = (from data in source
				where data.factorID == 230
				select data.factorPower).ToList();
			if (list4 != null)
			{
				int factorPowerLimit4 = GetFactorPowerLimit(230);
				equipFactorCriticalResist[i] = Mathf.Clamp(list4.Sum(), 0, factorPowerLimit4);
			}
			List<int> list5 = (from data in source
				where data.factorID == 240
				select data.factorPower).ToList();
			if (list5 != null)
			{
				int factorPowerLimit5 = GetFactorPowerLimit(240);
				equipFactorParry[i] = Mathf.Clamp(list5.Sum(), 0, factorPowerLimit5);
			}
			List<int> list6 = (from data in source
				where data.factorID == 250
				select data.factorPower).ToList();
			if (list6 != null)
			{
				int factorPowerLimit6 = GetFactorPowerLimit(250);
				equipFactorVampire[i] = Mathf.Clamp(list6.Sum(), 0, factorPowerLimit6);
			}
			List<int> list7 = (from data in source
				where data.factorID == 260
				select data.factorPower).ToList();
			if (list7 != null)
			{
				int factorPowerLimit7 = GetFactorPowerLimit(260);
				equipFactorResist[i] = Mathf.Clamp(list7.Sum(), 0, factorPowerLimit7);
			}
			List<int> list8 = (from data in source
				where data.factorID == 270
				select data.factorPower).ToList();
			if (list8 != null)
			{
				int factorPowerLimit8 = GetFactorPowerLimit(270);
				equipFactorRecoveryMp[i] = Mathf.Clamp(list8.Sum(), 0, factorPowerLimit8);
			}
			List<int> list9 = (from data in source
				where data.factorID == 500
				select data.factorPower).ToList();
			if (list9 != null)
			{
				int factorPowerLimit9 = GetFactorPowerLimit(500);
				equipFactorHP[i] = Mathf.Clamp(equipFactorHP[i] + list9.Sum(), 0, factorPowerLimit9);
			}
			List<int> list10 = (from data in source
				where data.factorID == 600
				select data.factorPower).ToList();
			if (list10 != null)
			{
				int factorPowerLimit10 = GetFactorPowerLimit(600);
				equipFactorMP[i] = Mathf.Clamp(equipFactorMP[i] + list10.Sum(), 0, factorPowerLimit10);
			}
			List<int> list11 = (from data in source
				where data.factorID == 700
				select data.factorPower).ToList();
			if (list11 != null)
			{
				int factorPowerLimit11 = GetFactorPowerLimit(700);
				equipFactorSkillPowerUp[i] = Mathf.Clamp(equipFactorSkillPowerUp[i] + list11.Sum(), 0, factorPowerLimit11);
			}
		}
		Debug.Log("防具のファクターデータ計算完了");
		if (isAllCalc)
		{
			CalCEquipPlayerAccessroy(unityAction, isAllCalc: true);
		}
		else
		{
			unityAction();
		}
	}

	public static void CalCEquipPlayerAccessroy(UnityAction unityAction, bool isAllCalc)
	{
		int num = 0;
		for (int i = 0; i < accessoryResistPoison.Length; i++)
		{
			accessoryResistPoison[i] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3000);
		if (num != -1)
		{
			accessoryResistPoison[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3000).itemPower;
		}
		for (int j = 0; j < accessoryResistPoison.Length; j++)
		{
			accessoryResistParalyze[j] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3001);
		if (num != -1)
		{
			accessoryResistParalyze[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3001).itemPower;
		}
		for (int k = 0; k < accessoryResistUp.Length; k++)
		{
			accessoryResistUp[k] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3002);
		if (num != -1)
		{
			accessoryResistUp[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3002).itemPower;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3003);
		if (num != -1)
		{
			accessoryResistAll[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3003).itemPower;
		}
		for (int l = 0; l < accessoryResistAll.Length; l++)
		{
			accessoryResistDebuff[l] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3004);
		if (num != -1)
		{
			accessoryResistDebuff[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3004).itemPower;
		}
		for (int m = 0; m < accessoryAgilityUp.Length; m++)
		{
			accessoryAgilityUp[m] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3005);
		if (num != -1)
		{
			accessoryAgilityUp[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3005).itemPower;
		}
		for (int n = 0; n < accessoryAgilityUp.Length; n++)
		{
			accessoryAgilityUp[n] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3010);
		if (num != -1)
		{
			accessoryAgilityUp[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3010).itemPower;
		}
		for (int num2 = 0; num2 < accessoryWeakPointDamageUp.Length; num2++)
		{
			accessoryWeakPointDamageUp[num2] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3011);
		if (num != -1)
		{
			accessoryWeakPointDamageUp[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3011).itemPower;
		}
		for (int num3 = 0; num3 < accessoryVampire.Length; num3++)
		{
			accessoryVampire[num3] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3012);
		if (num != -1)
		{
			accessoryVampire[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3012).itemPower;
		}
		for (int num4 = 0; num4 < accessorySkillCritical.Length; num4++)
		{
			accessorySkillCritical[num4] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3013);
		if (num != -1)
		{
			accessorySkillCritical[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3013).itemPower;
		}
		for (int num5 = 0; num5 < accessorySkillPowerUp.Length; num5++)
		{
			accessorySkillPowerUp[num5] = 0;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3020);
		if (num != -1)
		{
			accessorySkillPowerUp[num] = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3020).itemPower;
		}
		accessoryExpUp = 0;
		num = Array.IndexOf(playerEquipAccessoryID, 3021);
		if (num != -1)
		{
			accessoryExpUp = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3021).itemPower;
		}
		accessoryDropMoneyUp = 0;
		num = Array.IndexOf(playerEquipAccessoryID, 3022);
		if (num != -1)
		{
			accessoryDropMoneyUp = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3022).itemPower;
		}
		accessoryItemDiscover = 0;
		num = Array.IndexOf(playerEquipAccessoryID, 3023);
		if (num != -1)
		{
			accessoryItemDiscover = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3023).itemPower;
		}
		accessoryLibidoUpRate = 0;
		num = Array.IndexOf(playerEquipAccessoryID, 3050);
		if (num != -1)
		{
			accessoryLibidoUpRate = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3050).itemPower;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3051);
		if (num != -1)
		{
			accessoryLibidoUpRate = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3051).itemPower;
		}
		num = Array.IndexOf(playerEquipAccessoryID, 3052);
		if (num != -1)
		{
			accessoryLibidoUpRate = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData data) => data.itemID == 3052).itemPower;
		}
		Debug.Log("装飾品のデータ計算完了");
		unityAction();
	}

	private static int GetFactorPowerLimit(int factorID)
	{
		int num = 0;
		if (factorID < 200)
		{
			return GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorID == factorID).factorPowerLimit;
		}
		return GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorID == factorID).factorPowerLimit;
	}

	public static void CalcWeaponEquipFactorTotalPower(List<HaveFactorData> haveFactorList, ref int[] resultTotalPowerArray, ref int[] resultTotalPowerLimitArray, ref bool[] resultIsAddPercentArray)
	{
		List<int> list = (from data in haveFactorList
			where data.factorID == 0
			select data.factorPower).ToList();
		if (list != null)
		{
			int factorPowerLimit = GetFactorPowerLimit(0);
			resultTotalPowerArray[0] = Mathf.Clamp(list.Sum(), 0, factorPowerLimit);
			resultTotalPowerLimitArray[0] = factorPowerLimit;
			resultIsAddPercentArray[0] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.attackUp).isAddPercentText;
		}
		List<int> list2 = (from data in haveFactorList
			where data.factorID == 10
			select data.factorPower).ToList();
		if (list2 != null)
		{
			int factorPowerLimit2 = GetFactorPowerLimit(10);
			resultTotalPowerArray[1] = Mathf.Clamp(list2.Sum(), 0, factorPowerLimit2);
			resultTotalPowerLimitArray[1] = factorPowerLimit2;
			resultIsAddPercentArray[1] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.magicAttackUp).isAddPercentText;
		}
		List<int> list3 = (from data in haveFactorList
			where data.factorID == 20
			select data.factorPower).ToList();
		if (list3 != null)
		{
			int factorPowerLimit3 = GetFactorPowerLimit(20);
			resultTotalPowerArray[2] = Mathf.Clamp(list3.Sum(), 0, factorPowerLimit3);
			resultTotalPowerLimitArray[2] = factorPowerLimit3;
			resultIsAddPercentArray[2] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.accuracyUp).isAddPercentText;
		}
		List<int> list4 = (from data in haveFactorList
			where data.factorID == 30
			select data.factorPower).ToList();
		if (list4 != null)
		{
			int factorPowerLimit4 = GetFactorPowerLimit(30);
			resultTotalPowerArray[3] = Mathf.Clamp(list4.Sum(), 0, factorPowerLimit4);
			resultTotalPowerLimitArray[3] = factorPowerLimit4;
			resultIsAddPercentArray[3] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.criticalUp).isAddPercentText;
		}
		List<int> list5 = (from data in haveFactorList
			where data.factorID == 40
			select data.factorPower).ToList();
		if (list5 != null)
		{
			int factorPowerLimit5 = GetFactorPowerLimit(40);
			resultTotalPowerArray[4] = Mathf.Clamp(list5.Sum(), 0, factorPowerLimit5);
			resultTotalPowerLimitArray[4] = factorPowerLimit5;
			resultIsAddPercentArray[4] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.poison).isAddPercentText;
		}
		List<int> list6 = (from data in haveFactorList
			where data.factorID == 50
			select data.factorPower).ToList();
		if (list6 != null)
		{
			int factorPowerLimit6 = GetFactorPowerLimit(50);
			resultTotalPowerArray[5] = Mathf.Clamp(list6.Sum(), 0, factorPowerLimit6);
			resultTotalPowerLimitArray[5] = factorPowerLimit6;
			resultIsAddPercentArray[5] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.paralyze).isAddPercentText;
		}
		List<int> list7 = (from data in haveFactorList
			where data.factorID == 60
			select data.factorPower).ToList();
		if (list7 != null)
		{
			int factorPowerLimit7 = GetFactorPowerLimit(60);
			resultTotalPowerArray[6] = Mathf.Clamp(list7.Sum(), 0, factorPowerLimit7);
			resultTotalPowerLimitArray[6] = factorPowerLimit7;
			resultIsAddPercentArray[6] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.stagger).isAddPercentText;
		}
		List<int> list8 = (from data in haveFactorList
			where data.factorID == 70
			select data.factorPower).ToList();
		if (list8 != null)
		{
			int factorPowerLimit8 = GetFactorPowerLimit(70);
			resultTotalPowerArray[7] = Mathf.Clamp(list8.Sum(), 0, factorPowerLimit8);
			resultTotalPowerLimitArray[7] = factorPowerLimit8;
			resultIsAddPercentArray[7] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.death).isAddPercentText;
		}
		List<int> list9 = (from data in haveFactorList
			where data.factorID == 500
			select data.factorPower).ToList();
		if (list9 != null)
		{
			int factorPowerLimit9 = GetFactorPowerLimit(500);
			resultTotalPowerArray[8] = Mathf.Clamp(list9.Sum(), 0, factorPowerLimit9);
			resultTotalPowerLimitArray[8] = factorPowerLimit9;
			resultIsAddPercentArray[8] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.hpUp).isAddPercentText;
		}
		List<int> list10 = (from data in haveFactorList
			where data.factorID == 600
			select data.factorPower).ToList();
		if (list10 != null)
		{
			int factorPowerLimit10 = GetFactorPowerLimit(600);
			resultTotalPowerArray[9] = Mathf.Clamp(list10.Sum(), 0, factorPowerLimit10);
			resultTotalPowerLimitArray[9] = factorPowerLimit10;
			resultIsAddPercentArray[9] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.mpUp).isAddPercentText;
		}
		List<int> list11 = (from data in haveFactorList
			where data.factorID == 700
			select data.factorPower).ToList();
		if (list11 != null)
		{
			int factorPowerLimit11 = GetFactorPowerLimit(700);
			resultTotalPowerArray[10] = Mathf.Clamp(list11.Sum(), 0, factorPowerLimit11);
			resultTotalPowerLimitArray[10] = factorPowerLimit11;
			resultIsAddPercentArray[10] = GameDataManager.instance.factorDataBaseWeapon.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.skillPower).isAddPercentText;
		}
	}

	public static int[] CalcArmorEquipFactorTotalPower(List<HaveFactorData> haveFactorList, ref int[] resultTotalPowerArray, ref int[] resultTotalPowerLimitArray, ref bool[] resultIsAddPercentArray)
	{
		List<int> list = (from data in haveFactorList
			where data.factorID == 200
			select data.factorPower).ToList();
		if (list != null)
		{
			int factorPowerLimit = GetFactorPowerLimit(200);
			resultTotalPowerArray[0] = Mathf.Clamp(list.Sum(), 0, factorPowerLimit);
			resultTotalPowerLimitArray[0] = factorPowerLimit;
			resultIsAddPercentArray[0] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.defenseUp).isAddPercentText;
		}
		List<int> list2 = (from data in haveFactorList
			where data.factorID == 210
			select data.factorPower).ToList();
		if (list2 != null)
		{
			int factorPowerLimit2 = GetFactorPowerLimit(210);
			resultTotalPowerArray[1] = Mathf.Clamp(list2.Sum(), 0, factorPowerLimit2);
			resultTotalPowerLimitArray[1] = factorPowerLimit2;
			resultIsAddPercentArray[1] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.magicDefenseUp).isAddPercentText;
		}
		List<int> list3 = (from data in haveFactorList
			where data.factorID == 220
			select data.factorPower).ToList();
		if (list3 != null)
		{
			int factorPowerLimit3 = GetFactorPowerLimit(220);
			resultTotalPowerArray[2] = Mathf.Clamp(list3.Sum(), 0, factorPowerLimit3);
			resultTotalPowerLimitArray[2] = factorPowerLimit3;
			resultIsAddPercentArray[2] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.evasionUp).isAddPercentText;
		}
		List<int> list4 = (from data in haveFactorList
			where data.factorID == 230
			select data.factorPower).ToList();
		if (list4 != null)
		{
			int factorPowerLimit4 = GetFactorPowerLimit(230);
			resultTotalPowerArray[3] = Mathf.Clamp(list4.Sum(), 0, factorPowerLimit4);
			resultTotalPowerLimitArray[3] = factorPowerLimit4;
			resultIsAddPercentArray[3] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.criticalResistUp).isAddPercentText;
		}
		List<int> list5 = (from data in haveFactorList
			where data.factorID == 240
			select data.factorPower).ToList();
		if (list5 != null)
		{
			int factorPowerLimit5 = GetFactorPowerLimit(240);
			resultTotalPowerArray[4] = Mathf.Clamp(list5.Sum(), 0, factorPowerLimit5);
			resultTotalPowerLimitArray[4] = factorPowerLimit5;
			resultIsAddPercentArray[4] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.parry).isAddPercentText;
		}
		List<int> list6 = (from data in haveFactorList
			where data.factorID == 250
			select data.factorPower).ToList();
		if (list6 != null)
		{
			int factorPowerLimit6 = GetFactorPowerLimit(250);
			resultTotalPowerArray[5] = Mathf.Clamp(list6.Sum(), 0, factorPowerLimit6);
			resultTotalPowerLimitArray[5] = factorPowerLimit6;
			resultIsAddPercentArray[5] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.vampire).isAddPercentText;
		}
		List<int> list7 = (from data in haveFactorList
			where data.factorID == 260
			select data.factorPower).ToList();
		if (list7 != null)
		{
			int factorPowerLimit7 = GetFactorPowerLimit(260);
			resultTotalPowerArray[6] = Mathf.Clamp(list7.Sum(), 0, factorPowerLimit7);
			resultTotalPowerLimitArray[6] = factorPowerLimit7;
			resultIsAddPercentArray[6] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.abnormalResistUp).isAddPercentText;
		}
		List<int> list8 = (from data in haveFactorList
			where data.factorID == 270
			select data.factorPower).ToList();
		if (list8 != null)
		{
			int factorPowerLimit8 = GetFactorPowerLimit(270);
			resultTotalPowerArray[7] = Mathf.Clamp(list8.Sum(), 0, factorPowerLimit8);
			resultTotalPowerLimitArray[7] = factorPowerLimit8;
			resultIsAddPercentArray[7] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.mpSaving).isAddPercentText;
		}
		List<int> list9 = (from data in haveFactorList
			where data.factorID == 500
			select data.factorPower).ToList();
		if (list9 != null)
		{
			int factorPowerLimit9 = GetFactorPowerLimit(500);
			resultTotalPowerArray[8] = Mathf.Clamp(list9.Sum(), 0, factorPowerLimit9);
			resultTotalPowerLimitArray[8] = factorPowerLimit9;
			resultIsAddPercentArray[8] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.hpUp).isAddPercentText;
		}
		List<int> list10 = (from data in haveFactorList
			where data.factorID == 600
			select data.factorPower).ToList();
		if (list10 != null)
		{
			int factorPowerLimit10 = GetFactorPowerLimit(600);
			resultTotalPowerArray[9] = Mathf.Clamp(list10.Sum(), 0, factorPowerLimit10);
			resultTotalPowerLimitArray[9] = factorPowerLimit10;
			resultIsAddPercentArray[9] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.mpUp).isAddPercentText;
		}
		List<int> list11 = (from data in haveFactorList
			where data.factorID == 700
			select data.factorPower).ToList();
		if (list11 != null)
		{
			int factorPowerLimit11 = GetFactorPowerLimit(700);
			resultTotalPowerArray[10] = Mathf.Clamp(list11.Sum(), 0, factorPowerLimit11);
			resultTotalPowerLimitArray[10] = factorPowerLimit11;
			resultIsAddPercentArray[10] = GameDataManager.instance.factorDataBaseArmor.factorDataList.Find((FactorData data) => data.factorType == FactorData.FactorType.skillPower).isAddPercentText;
		}
		return resultTotalPowerArray;
	}
}
