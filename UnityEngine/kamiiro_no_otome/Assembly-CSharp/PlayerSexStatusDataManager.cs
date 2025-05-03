using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerSexStatusDataManager : SerializedMonoBehaviour
{
	public class MemberSexBuffCondition
	{
		public string type;

		public int power;

		public int continutyTurn;
	}

	public class MemberSexSubPower
	{
		public string type;

		public int continutyTurn;
	}

	public class MemberSexSkillReChargeTurn
	{
		public int skillID;

		public int needRechargeTurn;

		public int maxRechargeTurn;
	}

	public static int playerSexLv;

	public static int playerSexExp;

	public static int[] heroineSexLv = new int[4];

	public static int[] heroineSexExp = new int[4];

	public static int[] heroineMouthLv = new int[4];

	public static int[] heroineHandLv = new int[4];

	public static int[] heroineTitsLv = new int[4];

	public static int[] heroineNippleLv = new int[4];

	public static int[] heroineWombsLv = new int[4];

	public static int[] heroineClitorisLv = new int[4];

	public static int[] heroineVaginaLv = new int[4];

	public static int[] heroineAnalLv = new int[4];

	public static bool[] heroineTouchSexFlagArray = new bool[4];

	public static bool[] heroineTouchCumShotFlagArray = new bool[4];

	public static int[] totalPistonCount = new int[5];

	public static int[] totalMouthCount = new int[5];

	public static int[] totalOutShotCount = new int[5];

	public static int[] totalInShotCount = new int[5];

	public static int[] totalCondomShotCount = new int[5];

	public static int[] totalEcstasyCount = new int[5];

	public static int[] totalSexCount = new int[5];

	public static int[] totalUniqueSexCount = new int[5];

	public static readonly int remainingSemenDefaultValue = 8;

	public static int[] heroineRemainingSemenCount_vagina = new int[5];

	public static int[] heroineRemainingSemenCount_anal = new int[5];

	public static List<List<SexHeroinePassiveData>> playerUseSexPassiveSkillList = new List<List<SexHeroinePassiveData>>();

	public static List<List<SexSkillData>> playerUseSexActiveSkillList = new List<List<SexSkillData>>();

	public static int HeroineSexLibido;

	public static int[] playerSexHp = new int[5];

	public static int[] playerSexMaxHp = new int[5];

	public static int[] playerSexTrance = new int[2];

	public static int[] playerSexExtasyLimit = new int[2];

	public static int[] playerSexAttack = new int[5];

	public static int[] playerSexHealPower = new int[5];

	public static int[] playerSexSensitivity = new int[5];

	public static int[] playerSexCritical = new int[5];

	public static int[] playerSexPassiveBuffVoiceAttack = new int[5];

	public static int[] playerSexPassiveBuffTitsAttack = new int[5];

	public static int[] playerSexPassiveBuffTitsSensetivity = new int[5];

	public static int[] playerSexPassiveBuffClitorisSensetivity = new int[5];

	public static int[] playerSexPassiveBuffVaginaSensetivity = new int[5];

	public static int[] playerSexPassiveBuffVaginaAttack = new int[5];

	public static int[] playerSexPassiveBuffAnalAttack = new int[5];

	public static int[] playerSexCurrentLvExp = new int[5];

	public static int[] playerSexNextLvExp = new int[5];

	public static List<List<MemberSexBuffCondition>> playerSexBuffCondition = new List<List<MemberSexBuffCondition>>();

	public static List<List<MemberSexSubPower>> playerSexSubPower = new List<List<MemberSexSubPower>>();

	public static List<List<MemberSexSkillReChargeTurn>> playerSexSkillRechargeTurn = new List<List<MemberSexSkillReChargeTurn>>();

	public static void SetUpPlayerSexStatus(bool isBattle)
	{
		playerSexLv = 0;
		for (int i = 0; i < heroineSexLv.Length; i++)
		{
			heroineSexLv[i] = 0;
		}
		for (int j = 0; j < heroineMouthLv.Length; j++)
		{
			heroineMouthLv[j] = 0;
		}
		for (int k = 0; k < heroineHandLv.Length; k++)
		{
			heroineHandLv[k] = 0;
		}
		for (int l = 0; l < heroineTitsLv.Length; l++)
		{
			heroineTitsLv[l] = 0;
		}
		for (int m = 0; m < heroineTitsLv.Length; m++)
		{
			heroineTitsLv[m] = 0;
		}
		for (int n = 0; n < heroineNippleLv.Length; n++)
		{
			heroineNippleLv[n] = 0;
		}
		for (int num = 0; num < heroineWombsLv.Length; num++)
		{
			heroineWombsLv[num] = 0;
		}
		for (int num2 = 0; num2 < heroineClitorisLv.Length; num2++)
		{
			heroineClitorisLv[num2] = 0;
		}
		for (int num3 = 0; num3 < heroineVaginaLv.Length; num3++)
		{
			heroineVaginaLv[num3] = 0;
		}
		for (int num4 = 0; num4 < heroineAnalLv.Length; num4++)
		{
			heroineAnalLv[num4] = 0;
		}
		for (int num5 = 0; num5 < 2; num5++)
		{
			playerSexTrance[num5] = 0;
		}
		CharacterStatusDataBase characterStatusDataBase = GameDataManager.instance.characterStatusDataBase;
		int num6 = 0;
		for (int num7 = 0; num7 < GameDataManager.instance.needExpDataBase.needSexLvExpList.Count; num7++)
		{
			num6 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[0].characterSexLvCapFlag[num7];
			if (playerSexExp < GameDataManager.instance.needExpDataBase.needSexLvExpList[num7] || totalUniqueSexCount[0] < num6)
			{
				break;
			}
			playerSexLv++;
			if (playerSexLv >= 5)
			{
				break;
			}
		}
		for (int num8 = 0; num8 < 4; num8++)
		{
			for (int num9 = 0; num9 < GameDataManager.instance.needExpDataBase.needSexLvExpList.Count; num9++)
			{
				num6 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[num8 + 1].characterSexLvCapFlag[num9];
				if (heroineSexExp[num8] < GameDataManager.instance.needExpDataBase.needSexLvExpList[num9] || totalUniqueSexCount[num8 + 1] < num6)
				{
					break;
				}
				heroineSexLv[num8]++;
				if (heroineSexLv[num8] >= 5)
				{
					break;
				}
			}
		}
		for (int num10 = 0; num10 < 4; num10++)
		{
			for (int num11 = 0; num11 < 4; num11++)
			{
				string key = GameDataManager.instance.sexTouchDataBase.sexTouchDataList[num10].mouthBorderFlagList[num11];
				if (!PlayerFlagDataManager.scenarioFlagDictionary[key])
				{
					break;
				}
				heroineMouthLv[num10]++;
			}
			heroineHandLv[num10] = 1;
			for (int num12 = 0; num12 < 4; num12++)
			{
				string key2 = GameDataManager.instance.sexTouchDataBase.sexTouchDataList[num10].titsBorderFlagList[num12];
				if (!PlayerFlagDataManager.scenarioFlagDictionary[key2])
				{
					break;
				}
				heroineTitsLv[num10]++;
			}
			for (int num13 = 0; num13 < 4; num13++)
			{
				string key3 = GameDataManager.instance.sexTouchDataBase.sexTouchDataList[num10].nippleBorderFlagList[num13];
				if (!PlayerFlagDataManager.scenarioFlagDictionary[key3])
				{
					break;
				}
				heroineNippleLv[num10]++;
			}
			for (int num14 = 0; num14 < 4; num14++)
			{
				string key4 = GameDataManager.instance.sexTouchDataBase.sexTouchDataList[num10].wombBorderFlagList[num14];
				if (!PlayerFlagDataManager.scenarioFlagDictionary[key4])
				{
					break;
				}
				heroineWombsLv[num10]++;
			}
			for (int num15 = 0; num15 < 4; num15++)
			{
				string key5 = GameDataManager.instance.sexTouchDataBase.sexTouchDataList[num10].clitorisBorderFlagList[num15];
				if (!PlayerFlagDataManager.scenarioFlagDictionary[key5])
				{
					break;
				}
				heroineClitorisLv[num10]++;
			}
			for (int num16 = 0; num16 < 4; num16++)
			{
				string key6 = GameDataManager.instance.sexTouchDataBase.sexTouchDataList[num10].vaginaBorderFlagList[num16];
				if (!PlayerFlagDataManager.scenarioFlagDictionary[key6])
				{
					break;
				}
				heroineVaginaLv[num10]++;
			}
			for (int num17 = 0; num17 < 4; num17++)
			{
				string key7 = GameDataManager.instance.sexTouchDataBase.sexTouchDataList[num10].analBorderFlagList[num17];
				if (!PlayerFlagDataManager.scenarioFlagDictionary[key7])
				{
					break;
				}
				heroineAnalLv[num10]++;
			}
		}
		playerSexHp[0] = characterStatusDataBase.characterStatusDataList[0].characterSexHP[playerSexLv - 1];
		playerSexMaxHp[0] = characterStatusDataBase.characterStatusDataList[0].characterSexHP[playerSexLv - 1];
		playerSexAttack[0] = characterStatusDataBase.characterStatusDataList[0].characterSexAttack[playerSexLv - 1];
		playerSexHealPower[0] = characterStatusDataBase.characterStatusDataList[0].characterSexHealPower[playerSexLv - 1];
		playerSexSensitivity[0] = characterStatusDataBase.characterStatusDataList[0].characterSexSensetivity[playerSexLv - 1];
		playerSexCritical[0] = characterStatusDataBase.characterStatusDataList[0].characterSexCritical[playerSexLv - 1];
		for (int num18 = 0; num18 < 4; num18++)
		{
			int num19 = num18 + 1;
			playerSexHp[num19] = characterStatusDataBase.characterStatusDataList[num19].characterSexHP[heroineSexLv[num18] - 1];
			playerSexMaxHp[num19] = characterStatusDataBase.characterStatusDataList[num19].characterSexHP[heroineSexLv[num18] - 1];
			playerSexAttack[num19] = characterStatusDataBase.characterStatusDataList[num19].characterSexAttack[heroineSexLv[num18] - 1];
			playerSexHealPower[num19] = characterStatusDataBase.characterStatusDataList[num19].characterSexHealPower[heroineSexLv[num18] - 1];
			playerSexSensitivity[num19] = characterStatusDataBase.characterStatusDataList[num19].characterSexSensetivity[heroineSexLv[num18] - 1];
			playerSexCritical[num19] = characterStatusDataBase.characterStatusDataList[num19].characterSexCritical[heroineSexLv[num18] - 1];
		}
		int num20 = GameDataManager.instance.needExpDataBase.needSexLvExpList[playerSexLv - 1];
		int num21 = GameDataManager.instance.needExpDataBase.needSexLvExpList[playerSexLv];
		playerSexCurrentLvExp[0] = num20;
		playerSexNextLvExp[0] = num21;
		for (int num22 = 0; num22 < 4; num22++)
		{
			num20 = GameDataManager.instance.needExpDataBase.needSexLvExpList[heroineSexLv[num22] - 1];
			num21 = GameDataManager.instance.needExpDataBase.needSexLvExpList[heroineSexLv[num22]];
			playerSexCurrentLvExp[num22 + 1] = num20;
			playerSexNextLvExp[num22 + 1] = num21;
		}
		if (isBattle)
		{
			int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
			playerSexExtasyLimit[0] = characterStatusDataBase.characterStatusDataList[0].characterSexExtasyLimitNum[playerSexLv - 1];
			playerSexExtasyLimit[1] = characterStatusDataBase.characterStatusDataList[selectSexBattleHeroineId].characterSexExtasyLimitNum[heroineSexLv[selectSexBattleHeroineId - 1] - 1];
			SetUpHeroinePassiveBuffData(selectSexBattleHeroineId);
		}
		SetUpUseSexPassiveSkill();
		SetUpUseSexActiveSkill();
		Debug.Log("えっちステータス更新完了");
	}

	private static void SetUpHeroinePassiveBuffData(int heroineID)
	{
		int num = heroineID - 1;
		int num2 = 0;
		string skillIdString = "";
		playerSexPassiveBuffVoiceAttack[heroineID] = 0;
		playerSexPassiveBuffTitsAttack[heroineID] = 0;
		playerSexPassiveBuffTitsSensetivity[heroineID] = 0;
		playerSexPassiveBuffClitorisSensetivity[heroineID] = 0;
		playerSexPassiveBuffVaginaAttack[heroineID] = 0;
		playerSexPassiveBuffVaginaSensetivity[heroineID] = 0;
		playerSexPassiveBuffAnalAttack[heroineID] = 0;
		skillIdString = heroineID + "0" + heroineMouthLv[num];
		num2 = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString)).skillPower;
		playerSexPassiveBuffVoiceAttack[heroineID] += num2;
		skillIdString = heroineID + "3" + heroineTitsLv[num];
		num2 = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString)).skillPower;
		playerSexPassiveBuffTitsAttack[heroineID] += num2;
		skillIdString = heroineID + "4" + heroineNippleLv[num];
		num2 = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString)).skillPower;
		playerSexPassiveBuffTitsSensetivity[heroineID] += num2;
		skillIdString = heroineID + "5" + heroineWombsLv[num];
		num2 = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString)).skillPower;
		playerSexPassiveBuffVaginaSensetivity[heroineID] += num2;
		skillIdString = heroineID + "6" + heroineClitorisLv[num];
		num2 = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString)).skillPower;
		playerSexPassiveBuffClitorisSensetivity[heroineID] += num2;
		skillIdString = heroineID + "7" + heroineVaginaLv[num];
		num2 = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString)).skillPower;
		playerSexPassiveBuffVaginaAttack[heroineID] += num2;
		skillIdString = heroineID + "8" + heroineAnalLv[num];
		num2 = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString)).skillPower;
		playerSexPassiveBuffAnalAttack[heroineID] += num2;
	}

	private static void SetUpUseSexPassiveSkill()
	{
		playerUseSexPassiveSkillList.Clear();
		for (int i = 1; i < 5; i++)
		{
			int num = i - 1;
			string skillIdString = "";
			SexHeroinePassiveData sexHeroinePassiveData = null;
			List<SexHeroinePassiveData> list = new List<SexHeroinePassiveData>();
			skillIdString = i + "0" + heroineMouthLv[num];
			Debug.Log(skillIdString);
			sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString));
			list.Add(sexHeroinePassiveData);
			skillIdString = i + "3" + heroineTitsLv[num];
			sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString));
			list.Add(sexHeroinePassiveData);
			skillIdString = i + "4" + heroineNippleLv[num];
			sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString));
			list.Add(sexHeroinePassiveData);
			skillIdString = i + "5" + heroineWombsLv[num];
			sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString));
			list.Add(sexHeroinePassiveData);
			skillIdString = i + "6" + heroineClitorisLv[num];
			sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString));
			list.Add(sexHeroinePassiveData);
			skillIdString = i + "7" + heroineVaginaLv[num];
			sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString));
			list.Add(sexHeroinePassiveData);
			skillIdString = i + "8" + heroineAnalLv[num];
			sexHeroinePassiveData = GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[num].sexHeroinePassiveDataList.Find((SexHeroinePassiveData data) => data.skillID == int.Parse(skillIdString));
			list.Add(sexHeroinePassiveData);
			playerUseSexPassiveSkillList.Add(list);
		}
	}

	private static void SetUpUseSexActiveSkill()
	{
		playerUseSexActiveSkillList.Clear();
		List<SexSkillData> list = new List<SexSkillData>();
		List<SexSkillData> list2 = new List<SexSkillData>();
		List<SexSkillData> list3 = new List<SexSkillData>();
		List<SexSkillData> list4 = new List<SexSkillData>();
		List<SexSkillData> list5 = new List<SexSkillData>();
		for (int i = 0; i < GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Count; i++)
		{
			SexSkillData sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList[i];
			if (sexSkillData.skillID < 1000 && sexSkillData.skillUnlockLv <= playerSexLv)
			{
				list.Add(sexSkillData);
			}
		}
		playerUseSexActiveSkillList.Add(list);
		for (int j = 1; j < 5; j++)
		{
			for (int k = 0; k < GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Count; k++)
			{
				SexSkillData sexSkillData2 = GameDataManager.instance.sexSkillDataBase.sexSkillDataList[k];
				if (sexSkillData2.skillID >= 1000 && sexSkillData2.skillUnlockLv <= heroineSexLv[j - 1])
				{
					switch (j)
					{
					case 1:
						list2.Add(sexSkillData2);
						break;
					case 2:
						list3.Add(sexSkillData2);
						break;
					case 3:
						list4.Add(sexSkillData2);
						break;
					case 4:
						list5.Add(sexSkillData2);
						break;
					}
				}
			}
		}
		playerUseSexActiveSkillList.Add(list2);
		playerUseSexActiveSkillList.Add(list3);
		playerUseSexActiveSkillList.Add(list4);
		playerUseSexActiveSkillList.Add(list5);
	}

	public static void AddPlayerSexExp(bool isHeroine, int addExp)
	{
		int num = 0;
		int num2 = 0;
		if (!isHeroine)
		{
			int tempExp = playerSexExp + addExp;
			int num3 = GameDataManager.instance.needExpDataBase.needSexLvExpList.FindLastIndex((int data) => data <= tempExp) + 1;
			Debug.Log("エデン仮最大LV：" + num3 + "／仮EXP：" + tempExp);
			while (true)
			{
				if (GameDataManager.instance.characterStatusDataBase.characterStatusDataList[0].characterSexLvCapFlag[num3 - 1] <= totalUniqueSexCount[0])
				{
					num = num3;
					break;
				}
				if (num3 > 0)
				{
					num3--;
					continue;
				}
				num = 1;
				break;
			}
			num = Mathf.Clamp(num, 0, 5);
			num2 = ((num <= 1) ? GameDataManager.instance.needExpDataBase.needSexLvExpList[num] : GameDataManager.instance.needExpDataBase.needSexLvExpList[num]);
			if (num != 5)
			{
				num2--;
			}
			num2 = Mathf.Clamp(num2, 0, 99999);
			Debug.Log("エデン到達可能LV：" + num + "／到達可能EXP：" + num2);
			playerSexExp = Mathf.Clamp(playerSexExp + addExp, 0, num2);
			return;
		}
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int tempExp2 = heroineSexExp[selectSexBattleHeroineId - 1] + addExp;
		int num4 = GameDataManager.instance.needExpDataBase.needSexLvExpList.FindLastIndex((int data) => data <= tempExp2) + 1;
		Debug.Log("ヒロイン仮最大LV：" + num4 + "／仮EXP：" + tempExp2);
		while (true)
		{
			if (GameDataManager.instance.characterStatusDataBase.characterStatusDataList[selectSexBattleHeroineId].characterSexLvCapFlag[num4 - 1] <= totalUniqueSexCount[selectSexBattleHeroineId])
			{
				num = num4;
				break;
			}
			if (num4 > 0)
			{
				num4--;
				continue;
			}
			num = 1;
			break;
		}
		num = Mathf.Clamp(num, 0, 5);
		num2 = ((num <= 1) ? GameDataManager.instance.needExpDataBase.needSexLvExpList[num] : GameDataManager.instance.needExpDataBase.needSexLvExpList[num]);
		if (num != 5)
		{
			num2--;
		}
		num2 = Mathf.Clamp(num2, 0, 99999);
		Debug.Log("ヒロイン到達可能LV：" + num + "／到達可能EXP：" + num2);
		heroineSexExp[selectSexBattleHeroineId - 1] = Mathf.Clamp(heroineSexExp[selectSexBattleHeroineId - 1] + addExp, 0, num2);
	}

	public static void AddTotalSexCount(string type, int characterId, int addCount)
	{
		switch (type)
		{
		case "piston":
			totalPistonCount[characterId] += addCount;
			totalPistonCount[characterId] = Mathf.Clamp(totalPistonCount[characterId], 0, 99999);
			break;
		case "mouth":
			totalMouthCount[characterId] += addCount;
			totalMouthCount[characterId] = Mathf.Clamp(totalMouthCount[characterId], 0, 99999);
			break;
		case "outShot":
			totalOutShotCount[characterId] += addCount;
			totalOutShotCount[characterId] = Mathf.Clamp(totalOutShotCount[characterId], 0, 99999);
			break;
		case "inShot":
			totalInShotCount[characterId] += addCount;
			totalInShotCount[characterId] = Mathf.Clamp(totalInShotCount[characterId], 0, 99999);
			break;
		case "condomShot":
			totalCondomShotCount[characterId] += addCount;
			totalCondomShotCount[characterId] = Mathf.Clamp(totalCondomShotCount[characterId], 0, 99999);
			break;
		case "ecstasy":
			totalEcstasyCount[characterId] += addCount;
			totalEcstasyCount[characterId] = Mathf.Clamp(totalEcstasyCount[characterId], 0, 99999);
			break;
		case "sexCount":
			totalSexCount[characterId] += addCount;
			totalSexCount[characterId] = Mathf.Clamp(totalSexCount[characterId], 0, 99999);
			break;
		case "uniqueSexCount":
			totalUniqueSexCount[characterId] += addCount;
			totalUniqueSexCount[characterId] = Mathf.Clamp(totalUniqueSexCount[characterId], 0, 99999);
			break;
		}
	}

	public static void CheckSexHeroineMenstrualDay()
	{
		CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerDataManager.DungeonHeroineFollowNum);
		if (characterStatusData.characterMenstrualDayList.Contains(PlayerDataManager.currentWeekDay))
		{
			if (PlayerDataManager.DungeonHeroineFollowNum == 3)
			{
				if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterMenstruationFlag])
				{
					PlayerNonSaveDataManager.isSexHeroineMenstrualDay = true;
				}
				else
				{
					PlayerNonSaveDataManager.isSexHeroineMenstrualDay = false;
				}
			}
			else
			{
				PlayerNonSaveDataManager.isSexHeroineMenstrualDay = true;
			}
		}
		else
		{
			PlayerNonSaveDataManager.isSexHeroineMenstrualDay = false;
		}
		if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterFertilizationFlag])
		{
			PlayerNonSaveDataManager.isSexHeroineEnableFertilization = true;
		}
		else
		{
			PlayerNonSaveDataManager.isSexHeroineEnableFertilization = false;
		}
		Debug.Log("危険日：" + PlayerNonSaveDataManager.isSexHeroineMenstrualDay + "／中出し可否：" + PlayerNonSaveDataManager.isSexHeroineEnableFertilization);
	}
}
