using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatusDataManager : SerializedBehaviour
{
	public static readonly int partyMemberCount = 6;

	public static int[] characterExp = new int[partyMemberCount];

	public static int[] characterCurrentLvExp = new int[partyMemberCount];

	public static int[] characterNextLvExp = new int[partyMemberCount];

	public static int[] characterLv = new int[partyMemberCount];

	public static int[] characterHp = new int[partyMemberCount];

	public static int[] characterMaxHp = new int[partyMemberCount];

	public static int[] characterMp = new int[partyMemberCount];

	public static int[] characterMaxMp = new int[partyMemberCount];

	public static int[] characterSp = new int[partyMemberCount];

	public static int[] characterAttack = new int[partyMemberCount];

	public static int[] characterDefense = new int[partyMemberCount];

	public static int[] characterMagicAttack = new int[partyMemberCount];

	public static int[] characterMagicDefense = new int[partyMemberCount];

	public static int[] characterCritical = new int[partyMemberCount];

	public static int[] characterAccuracy = new int[partyMemberCount];

	public static int[] characterEvasion = new int[partyMemberCount];

	public static int[] characterAgility = new int[partyMemberCount];

	public static int[] characterResist = new int[partyMemberCount];

	public static int[] characterMpRecoveryRate = new int[partyMemberCount];

	public static int[] characterComboProbability = new int[partyMemberCount];

	public static int playerChargeAttack;

	public static int playerChargeMagicAttack;

	public static int playerAllHp;

	public static int playerAllMaxHp;

	public static int playerAllAttack;

	public static int playerAllMagicAttack;

	public static int playerAllDefense;

	public static int playerAllMagicDefense;

	public static int playerAllCritical;

	public static int playerAllAccuracy;

	public static int playerAllEvasion;

	public static int playerAllResist;

	public static int playerAllMpRecoveryRate;

	public static int[] playerPartyMember = new int[1];

	public static Dictionary<string, bool> playerBuff = new Dictionary<string, bool>();

	public static Dictionary<string, bool> playerDeBuff = new Dictionary<string, bool>();

	public static List<int> enemyExp = new List<int>();

	public static List<int> enemyGold = new List<int>();

	public static List<int> enemyLv = new List<int>();

	public static List<int> enemyHp = new List<int>();

	public static List<int> enemyMaxHp = new List<int>();

	public static List<int> enemyAttack = new List<int>();

	public static List<int> enemyDefense = new List<int>();

	public static List<int> enemyMagicAttack = new List<int>();

	public static List<int> enemyMagicDefense = new List<int>();

	public static List<int> enemyCritical = new List<int>();

	public static List<int> enemyAccuracy = new List<int>();

	public static List<int> enemyEvasion = new List<int>();

	public static List<int> enemyAgility = new List<int>();

	public static List<int> enemyResist = new List<int>();

	public static List<bool> enemyDeathResist = new List<bool>();

	public static int enemyAllHp;

	public static int enemyAllMaxHp;

	public static int enemyAllAttack;

	public static int enemyAllMagicAttack;

	public static int enemyAllDefense;

	public static int enemyAllMagicDefense;

	public static int enemyAllAccuracy;

	public static int enemyAllEvasion;

	public static int enemyAllCritical;

	public static int[] enemyMember;

	public static Dictionary<string, bool> enemyBuff = new Dictionary<string, bool>();

	public static Dictionary<string, bool> enemyDeBuff = new Dictionary<string, bool>();

	public static List<int> enemyChargeTurnList = new List<int>();

	public static List<int> enemyMaxChargeTurnList = new List<int>();

	public static List<int> needExpDataList = new List<int>();

	public static void SetUpPlayerStatus(bool isSetUp, UnityAction unityAction)
	{
		CharacterStatusDataBase characterStatusDataBase = GameDataManager.instance.characterStatusDataBase;
		for (int i = 0; i < characterExp.Length; i++)
		{
			for (int j = 0; j < GameDataManager.instance.needExpDataBase.needCharacterLvExpList.Count; j++)
			{
				if (characterLv[i] < 50)
				{
					if (characterExp[i] >= GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j])
					{
						characterLv[i] = j + 1;
						continue;
					}
					characterCurrentLvExp[i] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j - 1];
					characterNextLvExp[i] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[j];
					break;
				}
				characterCurrentLvExp[i] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[49];
				characterNextLvExp[i] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[50];
				break;
			}
			if (isSetUp)
			{
				int value = characterStatusDataBase.characterStatusDataList[i].characterHP[characterLv[i] - 1];
				float num = (float)PlayerEquipDataManager.equipFactorHP[i] / 100f + 1f;
				value = CalcCharacterPowerUp(i, value);
				if (i == 0)
				{
					value = Mathf.FloorToInt(Mathf.Floor((float)value / 10f) * 10f);
				}
				value = Mathf.FloorToInt((float)value * num);
				characterHp[i] = value;
				characterMaxHp[i] = value;
				int value2 = characterStatusDataBase.characterStatusDataList[i].characterMP[characterLv[i] - 1];
				float num2 = (float)PlayerEquipDataManager.equipFactorMP[i] / 100f + 1f;
				value2 = CalcCharacterPowerUp(i, value2);
				if (i == 0)
				{
					value2 = Mathf.FloorToInt(Mathf.Floor((float)value2 / 10f) * 10f);
				}
				value2 = Mathf.FloorToInt((float)value2 * num2);
				characterMp[i] = value2;
				characterMaxMp[i] = value2;
			}
			else
			{
				int value3 = characterStatusDataBase.characterStatusDataList[i].characterHP[characterLv[i] - 1];
				float num3 = (float)PlayerEquipDataManager.equipFactorHP[i] / 100f + 1f;
				value3 = CalcCharacterPowerUp(i, value3);
				if (i == 0)
				{
					value3 = Mathf.FloorToInt(Mathf.Floor((float)value3 / 10f) * 10f);
				}
				value3 = Mathf.FloorToInt((float)value3 * num3);
				characterMaxHp[i] = value3;
				if (characterHp[i] > value3)
				{
					characterHp[i] = value3;
				}
				int value4 = characterStatusDataBase.characterStatusDataList[i].characterMP[characterLv[i] - 1];
				float num4 = (float)PlayerEquipDataManager.equipFactorMP[i] / 100f + 1f;
				value4 = CalcCharacterPowerUp(i, value4);
				if (i == 0)
				{
					value4 = Mathf.FloorToInt(Mathf.Floor((float)value4 / 10f) * 10f);
				}
				value4 = Mathf.FloorToInt((float)value4 * num4);
				characterMaxMp[i] = value4;
				if (characterMp[i] > value4)
				{
					characterMp[i] = value4;
				}
			}
			int value5 = characterStatusDataBase.characterStatusDataList[i].characterAttack[characterLv[i] - 1];
			float num5 = (float)PlayerEquipDataManager.equipFactorAttackUp[i] / 100f + 1f;
			value5 = CalcCharacterPowerUp(i, value5);
			value5 += PlayerEquipDataManager.equipWeaponAttack[i];
			value5 = Mathf.CeilToInt((float)value5 * num5);
			characterAttack[i] = value5;
			int value6 = characterStatusDataBase.characterStatusDataList[i].characterMagicAttack[characterLv[i] - 1];
			float num6 = (float)PlayerEquipDataManager.equipFactorMagicAttackUp[i] / 100f + 1f;
			value6 = CalcCharacterPowerUp(i, value6);
			value6 += PlayerEquipDataManager.equipWeaponMagicAttack[i];
			value6 = Mathf.CeilToInt((float)value6 * num6);
			characterMagicAttack[i] = value6;
			int value7 = characterStatusDataBase.characterStatusDataList[i].characterDefense[characterLv[i] - 1];
			float num7 = (float)PlayerEquipDataManager.equipFactorDefenseUp[i] / 100f + 1f;
			value7 = CalcCharacterPowerUp(i, value7);
			value7 += PlayerEquipDataManager.equipArmorDefense[i];
			value7 = Mathf.CeilToInt((float)value7 * num7);
			characterDefense[i] = value7;
			int value8 = characterStatusDataBase.characterStatusDataList[i].characterMagicDefense[characterLv[i] - 1];
			float num8 = (float)PlayerEquipDataManager.equipFactorMagicDefenseUp[i] / 100f + 1f;
			value8 = CalcCharacterPowerUp(i, value8);
			value8 += PlayerEquipDataManager.equipArmorMagicDefense[i];
			value8 = Mathf.CeilToInt((float)value8 * num8);
			characterMagicDefense[i] = value8;
			int num9 = characterStatusDataBase.characterStatusDataList[i].characterCritical[characterLv[i] - 1];
			num9 += PlayerEquipDataManager.equipWeaponCritical[i];
			num9 += PlayerEquipDataManager.equipFactorCriticalUp[i];
			characterCritical[i] = num9;
			int num10 = characterStatusDataBase.characterStatusDataList[i].characterAccuracy[characterLv[i] - 1];
			num10 += PlayerEquipDataManager.equipWeaponAccuracy[i];
			num10 += PlayerEquipDataManager.equipFactorAccuracyUp[i];
			characterAccuracy[i] = num10;
			int num11 = characterStatusDataBase.characterStatusDataList[i].characterEvasion[characterLv[i] - 1];
			num11 += PlayerEquipDataManager.equipArmorEvasion[i];
			characterEvasion[i] = num11;
			int num12 = characterStatusDataBase.characterStatusDataList[i].characterAgility[characterLv[i] - 1];
			if (PlayerEquipDataManager.accessoryAgilityUp[i] > 0)
			{
				float num13 = (float)PlayerEquipDataManager.accessoryAgilityUp[i] / 100f;
				num12 = (int)Mathf.Clamp((float)num12 * num13, 0f, 99f);
			}
			characterAgility[i] = num12;
			int num14 = characterStatusDataBase.characterStatusDataList[i].characterResist[characterLv[i] - 1];
			num14 += PlayerEquipDataManager.equipArmorResist[i];
			num14 += PlayerEquipDataManager.accessoryResistUp[i];
			num14 += PlayerEquipDataManager.accessoryResistAll[i];
			characterResist[i] = num14;
			int num15 = characterStatusDataBase.characterStatusDataList[i].characterMpRecoveryRate[characterLv[i] - 1];
			num15 += PlayerEquipDataManager.equipArmorRecoveryMP[i];
			characterMpRecoveryRate[i] = num15;
			characterComboProbability[i] = PlayerEquipDataManager.equipWeaponComboProbability[i];
		}
		playerAllHp = 0;
		playerAllMaxHp = 0;
		playerAllAttack = 0;
		playerAllDefense = 0;
		playerAllMagicAttack = 0;
		playerAllMagicDefense = 0;
		playerAllCritical = 0;
		playerAllAccuracy = 0;
		playerAllEvasion = 0;
		playerAllResist = 0;
		playerAllMpRecoveryRate = 0;
		for (int k = 0; k < playerPartyMember.Length; k++)
		{
			playerAllHp += characterHp[playerPartyMember[k]];
			playerAllMaxHp += characterMaxHp[playerPartyMember[k]];
			playerAllAttack += characterAttack[playerPartyMember[k]];
			playerAllDefense += characterDefense[playerPartyMember[k]];
			playerAllMagicAttack += characterMagicAttack[playerPartyMember[k]];
			playerAllMagicDefense += characterMagicDefense[playerPartyMember[k]];
			playerAllCritical += characterCritical[playerPartyMember[k]];
			playerAllAccuracy += characterAccuracy[playerPartyMember[k]];
			playerAllEvasion += characterEvasion[playerPartyMember[k]];
			playerAllResist = characterResist[playerPartyMember[k]];
			playerAllMpRecoveryRate = characterMpRecoveryRate[playerPartyMember[k]];
		}
		playerAllAttack /= playerPartyMember.Length;
		playerAllDefense /= playerPartyMember.Length;
		playerAllMagicAttack /= playerPartyMember.Length;
		playerAllMagicDefense /= playerPartyMember.Length;
		playerAllCritical /= playerPartyMember.Length;
		playerAllAccuracy /= playerPartyMember.Length;
		playerAllEvasion /= playerPartyMember.Length;
		playerAllResist /= playerPartyMember.Length;
		playerAllMpRecoveryRate /= playerPartyMember.Length;
		Debug.Log("ステータス更新完了");
		unityAction?.Invoke();
	}

	private static int CalcCharacterPowerUp(int index, int value)
	{
		int num = 0;
		switch (index)
		{
		case 0:
		{
			float num2 = (float)PlayerFlagDataManager.CalcPartyPowerUpCount() / 10f + 1f;
			return Mathf.FloorToInt((float)value * num2);
		}
		case 1:
			return value;
		case 4:
			if (value == 9999)
			{
				return value;
			}
			break;
		}
		if (PlayerFlagDataManager.partyPowerUpFlagList[index])
		{
			return value;
		}
		return Mathf.FloorToInt((float)value * 0.7f);
	}

	public static void CalkPlayerChargeStatus()
	{
		playerChargeAttack = 0;
		playerChargeMagicAttack = 0;
		for (int i = 0; i < playerPartyMember.Length; i++)
		{
			playerChargeAttack += characterAttack[playerPartyMember[i]];
			playerChargeMagicAttack += characterMagicAttack[playerPartyMember[i]];
		}
	}

	public static void CalcAllHpToCharacterHp()
	{
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			int dungeonHeroineFollowNum = PlayerDataManager.DungeonHeroineFollowNum;
			characterHp[0] = characterMaxHp[0] - (playerAllMaxHp - playerAllHp);
			characterHp[0] = Mathf.Clamp(characterHp[0], 1, 99999);
			if (characterHp[dungeonHeroineFollowNum] != 1)
			{
				int num = playerAllMaxHp - playerAllHp;
				if (num > characterMaxHp[0])
				{
					int num2 = num - characterMaxHp[0];
					characterHp[dungeonHeroineFollowNum] = characterMaxHp[dungeonHeroineFollowNum] - num2;
				}
				characterHp[dungeonHeroineFollowNum] = Mathf.Clamp(characterHp[dungeonHeroineFollowNum], 1, 99999);
			}
			characterSp[dungeonHeroineFollowNum] = 0;
		}
		else
		{
			characterHp[0] = playerAllHp;
			characterHp[0] = Mathf.Clamp(characterHp[0], 1, 99999);
		}
		Debug.Log("AllHpをCharacterHpに変換する");
	}

	public static void RefreshUnFollowHeroineStatus()
	{
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			for (int i = 1; i < characterHp.Length; i++)
			{
				if (i != PlayerDataManager.DungeonHeroineFollowNum)
				{
					characterHp[i] = characterMaxHp[i];
					characterMp[i] = characterMaxMp[i];
				}
			}
		}
		else
		{
			for (int j = 1; j < characterHp.Length; j++)
			{
				characterHp[j] = characterMaxHp[j];
				characterMp[j] = characterMaxMp[j];
			}
		}
		Debug.Log("朝になったので、フォロー中ではないヒロインのHPを回復");
	}

	public static void AddCharacterExp(int characterID, int addExp)
	{
		characterExp[characterID] += addExp;
		characterExp[characterID] = Mathf.Clamp(characterExp[characterID], 0, 999999);
	}

	public static int GetCharacterLvFromExp(int characterID)
	{
		int num = 0;
		num = GameDataManager.instance.needExpDataBase.needCharacterLvExpList.Where((int data) => data <= characterExp[characterID]).ToList().Count;
		Debug.Log("キャラID：" + characterID + "／キャラEXP：" + characterExp[characterID] + "／キャラLV：" + num);
		return num;
	}

	public static void LvUpPlayerStatus(int characterID, UnityAction unityAction)
	{
		CharacterStatusDataBase characterStatusDataBase = GameDataManager.instance.characterStatusDataBase;
		for (int i = 0; i < GameDataManager.instance.needExpDataBase.needCharacterLvExpList.Count; i++)
		{
			if (characterExp[characterID] < GameDataManager.instance.needExpDataBase.needCharacterLvExpList[i])
			{
				Debug.Log("キャラID：" + characterID + "／確定LV：" + i + "／キャラLV：" + characterLv[characterID] + "／次のEXP：" + GameDataManager.instance.needExpDataBase.needCharacterLvExpList[i]);
				characterCurrentLvExp[characterID] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[i - 1];
				characterNextLvExp[characterID] = GameDataManager.instance.needExpDataBase.needCharacterLvExpList[i];
				break;
			}
		}
		int value = characterStatusDataBase.characterStatusDataList[characterID].characterHP[characterLv[characterID] - 1];
		float num = (float)PlayerEquipDataManager.equipFactorHP[characterID] / 100f + 1f;
		value = CalcCharacterPowerUp(characterID, value);
		if (characterID == 0)
		{
			value = Mathf.FloorToInt(Mathf.Floor((float)value / 10f) * 10f);
		}
		value = Mathf.FloorToInt((float)value * num);
		characterHp[characterID] = value;
		characterMaxHp[characterID] = value;
		int value2 = characterStatusDataBase.characterStatusDataList[characterID].characterMP[characterLv[characterID] - 1];
		float num2 = (float)PlayerEquipDataManager.equipFactorMP[characterID] / 100f + 1f;
		value2 = CalcCharacterPowerUp(characterID, value2);
		if (characterID == 0)
		{
			value2 = Mathf.FloorToInt(Mathf.Floor((float)value2 / 10f) * 10f);
		}
		value2 = Mathf.FloorToInt((float)value2 * num2);
		characterMp[characterID] = value2;
		characterMaxMp[characterID] = value2;
		int value3 = characterStatusDataBase.characterStatusDataList[characterID].characterAttack[characterLv[characterID] - 1];
		float num3 = (float)PlayerEquipDataManager.equipFactorAttackUp[characterID] / 100f + 1f;
		value3 = CalcCharacterPowerUp(characterID, value3);
		value3 += PlayerEquipDataManager.equipWeaponAttack[characterID];
		value3 = Mathf.FloorToInt((float)value3 * num3);
		characterAttack[characterID] = value3;
		int value4 = characterStatusDataBase.characterStatusDataList[characterID].characterMagicAttack[characterLv[characterID] - 1];
		float num4 = (float)PlayerEquipDataManager.equipFactorMagicDefenseUp[characterID] / 100f + 1f;
		value4 = CalcCharacterPowerUp(characterID, value4);
		value4 += PlayerEquipDataManager.equipWeaponMagicAttack[characterID];
		value4 = Mathf.FloorToInt((float)value4 * num4);
		characterMagicAttack[characterID] = value4;
		int value5 = characterStatusDataBase.characterStatusDataList[characterID].characterDefense[characterLv[characterID] - 1];
		float num5 = (float)PlayerEquipDataManager.equipFactorDefenseUp[characterID] / 100f + 1f;
		value5 = CalcCharacterPowerUp(characterID, value5);
		value5 += PlayerEquipDataManager.equipArmorDefense[characterID];
		value5 = Mathf.FloorToInt((float)value5 * num5);
		characterDefense[characterID] = value5;
		int value6 = characterStatusDataBase.characterStatusDataList[characterID].characterMagicDefense[characterLv[characterID] - 1];
		float num6 = (float)PlayerEquipDataManager.equipFactorMagicDefenseUp[characterID] / 100f + 1f;
		value6 = CalcCharacterPowerUp(characterID, value6);
		value6 += PlayerEquipDataManager.equipArmorMagicDefense[characterID];
		value6 = Mathf.FloorToInt((float)value6 * num6);
		characterMagicDefense[characterID] = value6;
		int num7 = characterStatusDataBase.characterStatusDataList[characterID].characterCritical[characterLv[characterID] - 1];
		num7 += PlayerEquipDataManager.equipWeaponCritical[characterID];
		num7 += PlayerEquipDataManager.equipFactorCriticalUp[characterID];
		characterCritical[characterID] = num7;
		int num8 = characterStatusDataBase.characterStatusDataList[characterID].characterAccuracy[characterLv[characterID] - 1];
		num8 += PlayerEquipDataManager.equipWeaponAccuracy[characterID];
		num8 += PlayerEquipDataManager.equipFactorAccuracyUp[characterID];
		characterAccuracy[characterID] = num8;
		int num9 = characterStatusDataBase.characterStatusDataList[characterID].characterEvasion[characterLv[characterID] - 1];
		num9 += PlayerEquipDataManager.equipArmorEvasion[characterID];
		characterEvasion[characterID] = num9;
		int num10 = characterStatusDataBase.characterStatusDataList[characterID].characterAgility[characterLv[characterID] - 1];
		num10 += PlayerEquipDataManager.accessoryAgilityUp[characterID];
		characterAgility[characterID] = num10;
		int num11 = characterStatusDataBase.characterStatusDataList[characterID].characterResist[characterLv[characterID] - 1];
		num11 += PlayerEquipDataManager.equipArmorResist[characterID];
		num11 += PlayerEquipDataManager.accessoryResistAll[characterID];
		characterResist[characterID] = num11;
		int num12 = characterStatusDataBase.characterStatusDataList[characterID].characterMpRecoveryRate[characterLv[characterID] - 1];
		num12 += PlayerEquipDataManager.equipArmorRecoveryMP[characterID];
		characterMpRecoveryRate[characterID] = num12;
		playerAllHp = 0;
		playerAllMaxHp = 0;
		playerAllAttack = 0;
		playerAllDefense = 0;
		playerAllMagicAttack = 0;
		playerAllMagicDefense = 0;
		playerAllCritical = 0;
		playerAllAccuracy = 0;
		playerAllEvasion = 0;
		playerAllResist = 0;
		playerAllMpRecoveryRate = 0;
		for (int j = 0; j < playerPartyMember.Length; j++)
		{
			playerAllHp += characterHp[playerPartyMember[j]];
			playerAllMaxHp += characterMaxHp[playerPartyMember[j]];
			playerAllAttack += characterAttack[playerPartyMember[j]];
			playerAllDefense += characterDefense[playerPartyMember[j]];
			playerAllMagicAttack += characterMagicAttack[playerPartyMember[j]];
			playerAllMagicDefense += characterMagicDefense[playerPartyMember[j]];
			playerAllCritical += characterCritical[playerPartyMember[j]];
			playerAllAccuracy += characterAccuracy[playerPartyMember[j]];
			playerAllEvasion += characterEvasion[playerPartyMember[j]];
			playerAllResist = characterResist[playerPartyMember[j]];
			playerAllMpRecoveryRate = characterMpRecoveryRate[playerPartyMember[j]];
		}
		playerAllAttack /= playerPartyMember.Length;
		playerAllDefense /= playerPartyMember.Length;
		playerAllMagicAttack /= playerPartyMember.Length;
		playerAllMagicDefense /= playerPartyMember.Length;
		playerAllCritical /= playerPartyMember.Length;
		playerAllAccuracy /= playerPartyMember.Length;
		playerAllEvasion /= playerPartyMember.Length;
		playerAllResist /= playerPartyMember.Length;
		playerAllMpRecoveryRate /= playerPartyMember.Length;
		Debug.Log("ステータス更新完了");
		unityAction?.Invoke();
	}

	public static void SetUpEnemyStatus(UnityAction unityAction)
	{
		enemyExp.Clear();
		enemyGold.Clear();
		enemyLv.Clear();
		enemyHp.Clear();
		enemyMaxHp.Clear();
		enemyAttack.Clear();
		enemyDefense.Clear();
		enemyMagicAttack.Clear();
		enemyMagicDefense.Clear();
		enemyCritical.Clear();
		enemyAccuracy.Clear();
		enemyEvasion.Clear();
		enemyAgility.Clear();
		enemyResist.Clear();
		enemyDeathResist.Clear();
		enemyChargeTurnList.Clear();
		enemyMaxChargeTurnList.Clear();
		BattleEnemyDataBase battleEnemyDataBase = GameDataManager.instance.battleEnemyDataBase;
		int i;
		for (i = 0; i < enemyMember.Length; i++)
		{
			BattleEnemyData battleEnemyData = battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == enemyMember[i]);
			enemyExp.Add(battleEnemyData.enemyExp);
			enemyGold.Add(battleEnemyData.enemyGold);
			enemyLv.Add(battleEnemyData.enemyLV);
			enemyHp.Add(battleEnemyData.enemyHP);
			enemyMaxHp.Add(battleEnemyData.enemyHP);
			enemyAttack.Add(battleEnemyData.enemyAttack);
			enemyDefense.Add(battleEnemyData.enemyDefense);
			enemyMagicAttack.Add(battleEnemyData.enemyMagicAttack);
			enemyMagicDefense.Add(battleEnemyData.enemyMagicDefense);
			enemyCritical.Add(battleEnemyData.enemyCritical);
			enemyAccuracy.Add(battleEnemyData.enemyAccuracy);
			enemyEvasion.Add(battleEnemyData.enemyEvasion);
			enemyAgility.Add(battleEnemyData.enemyAgility);
			enemyResist.Add(battleEnemyData.deBuffSkillResistBonus);
			enemyDeathResist.Add(battleEnemyData.deathAttackResist);
			enemyMaxChargeTurnList.Add(battleEnemyData.enemyChargeTurn);
		}
		enemyAllHp = enemyHp.Sum();
		enemyAllMaxHp = enemyHp.Sum();
		enemyAllAttack = enemyAttack.Sum() / enemyMember.Length;
		enemyAllDefense = enemyDefense.Sum() / enemyMember.Length;
		enemyAllMagicAttack = enemyMagicAttack.Sum() / enemyMember.Length;
		enemyAllMagicDefense = enemyMagicDefense.Sum() / enemyMember.Length;
		enemyAllCritical = enemyCritical.Sum() / enemyMember.Length;
		enemyAllAccuracy = enemyAccuracy.Sum() / enemyMember.Length;
		enemyAllEvasion = enemyEvasion.Sum() / enemyMember.Length;
		foreach (int enemyMaxChargeTurn in enemyMaxChargeTurnList)
		{
			_ = enemyMaxChargeTurn;
			enemyChargeTurnList.Add(0);
		}
		Debug.Log("エネミーステータス更新完了");
		unityAction();
	}
}
