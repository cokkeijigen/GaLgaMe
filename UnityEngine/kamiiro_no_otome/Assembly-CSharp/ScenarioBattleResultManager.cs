using System.Collections.Generic;
using Arbor;
using Coffee.UIExtensions;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using Utage;

public class ScenarioBattleResultManager : SerializedMonoBehaviour
{
	public UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBackupManager scenarioBackupManager;

	public ArborFSM resultFSM;

	public GameObject resultEffectGroupGo;

	public UIParticle uIParticle;

	public GameObject[] resultEffectPrefabGoArray;

	public Transform resultEffectSpawnGo;

	public PlayableDirector playableDirector;

	public PlayableAsset[] resultEffectAssetArray;

	private void Awake()
	{
		scenarioBackupManager = GameObject.Find("Scenario Manager").GetComponent<ScenarioBackupManager>();
		resultEffectGroupGo.SetActive(value: false);
	}

	public void StartBattleResultEffect(bool isVictory)
	{
		if (isVictory)
		{
			playableDirector.playableAsset = resultEffectAssetArray[0];
		}
		else
		{
			playableDirector.playableAsset = resultEffectAssetArray[1];
		}
		resultEffectGroupGo.SetActive(value: true);
		resultEffectGroupGo.GetComponent<AudioSource>().volume = PlayerOptionsDataManager.optionsSeVolume;
		playableDirector.time = 0.0;
		playableDirector.Play();
	}

	public void SpawnBattleResultEffect(bool isVictory)
	{
		if (isVictory)
		{
			resultEffectSpawnGo = PoolManager.Pools["BattleObject"].Spawn(resultEffectPrefabGoArray[0], uIParticle.transform);
		}
		else
		{
			resultEffectSpawnGo = PoolManager.Pools["BattleObject"].Spawn(resultEffectPrefabGoArray[1], uIParticle.transform);
		}
		resultEffectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
		resultEffectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("リザルトエフェクトをスポーン");
		uIParticle.RefreshParticles();
	}

	public void DeSpawnBattleResultEffect(bool isVictory)
	{
		PoolManager.Pools["BattleObject"].Despawn(resultEffectSpawnGo, utageBattleSceneManager.poolManagerGO);
		resultEffectGroupGo.SetActive(value: false);
		if (isVictory)
		{
			utageBattleSceneManager.isStatusBackUp = false;
			GameObject.Find("Result Dialog Manager").GetComponent<ArborFSM>().SendTrigger("OpenBattleResult");
		}
		else if (GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData item) => item.scenarioName == PlayerNonSaveDataManager.resultScenarioName).isDefeatBattle)
		{
			utageBattleSceneManager.battleCanvas.SetActive(value: false);
			PlayerNonSaveDataManager.isScenarioBattle = false;
			utageBattleSceneManager.isStatusBackUp = false;
			for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
			{
				PlayerStatusDataManager.characterHp[PlayerStatusDataManager.playerPartyMember[i]] = 1;
				PlayerStatusDataManager.characterMp[PlayerStatusDataManager.playerPartyMember[i]] = PlayerStatusDataManager.characterMaxMp[PlayerStatusDataManager.playerPartyMember[i]];
			}
			Debug.Log("負け確定バトル");
			GameObject.Find("Result Dialog Manager").GetComponent<ArborFSM>().SendTrigger("CloseBattleResult");
		}
		else
		{
			GameObject.Find("Battle Dialog Manager").GetComponent<PlayMakerFSM>().SendEvent("OpenBattleFailedDialog");
		}
	}

	public void ProcessAtScenarioBattleDefeat()
	{
		PlayerNonSaveDataManager.isScenarioBattle = false;
		PlayerNonSaveDataManager.isMoveToDungeonBattle = false;
		PlayerNonSaveDataManager.isRetreatFromBattle = false;
		PlayerNonSaveDataManager.isUtagePlayBattleBgm = false;
		if (PlayerNonSaveDataManager.isRestoreDungeonBattleFailed)
		{
			PlayerDataManager.playerHaveMoney = scenarioBackupManager.beforePlayerMoney;
			for (int i = 0; i < PlayerStatusDataManager.partyMemberCount; i++)
			{
				PlayerStatusDataManager.characterExp[i] = scenarioBackupManager.beforePlayerExp[i];
				PlayerStatusDataManager.characterHp[i] = scenarioBackupManager.beforePlayerHp[i];
				PlayerStatusDataManager.characterMp[i] = scenarioBackupManager.beforePlayerMp[i];
			}
			int index = PlayerInventoryDataManager.haveWeaponList.FindIndex((HaveWeaponData m) => m.equipCharacter == 0);
			PlayerInventoryDataManager.haveWeaponList[index].weaponIncludeMp = scenarioBackupManager.beforeEquipWeaponTp;
			PlayerInventoryDataManager.haveItemList.Clear();
			for (int j = 0; j < scenarioBackupManager.beforeHaveItemSortIdList.Count; j++)
			{
				HaveItemData item = new HaveItemData
				{
					itemSortID = scenarioBackupManager.beforeHaveItemSortIdList[j],
					itemID = scenarioBackupManager.beforeHaveItemIdList[j],
					haveCountNum = scenarioBackupManager.beforeHaveItemHaveNumList[j]
				};
				PlayerInventoryDataManager.haveItemList.Add(item);
			}
			PlayerInventoryDataManager.haveItemMaterialList.Clear();
			for (int k = 0; k < scenarioBackupManager.beforeHaveMaterialSortIdList.Count; k++)
			{
				HaveItemData item2 = new HaveItemData
				{
					itemSortID = scenarioBackupManager.beforeHaveMaterialSortIdList[k],
					itemID = scenarioBackupManager.beforeHaveMaterialIdList[k],
					haveCountNum = scenarioBackupManager.beforeHaveMaterialHaveNumList[k]
				};
				PlayerInventoryDataManager.haveItemMaterialList.Add(item2);
			}
			PlayerInventoryDataManager.haveItemMagicMaterialList.Clear();
			for (int l = 0; l < scenarioBackupManager.beforeHaveMagicMaterialSortIdList.Count; l++)
			{
				HaveItemData item3 = new HaveItemData
				{
					itemSortID = scenarioBackupManager.beforeHaveMagicMaterialSortIdList[l],
					itemID = scenarioBackupManager.beforeHaveMagicMaterialIdList[l],
					haveCountNum = scenarioBackupManager.beforeHaveMagicMaterialHaveNumList[l]
				};
				PlayerInventoryDataManager.haveItemMagicMaterialList.Add(item3);
			}
			PlayerInventoryDataManager.haveCashableItemList.Clear();
			for (int n = 0; n < scenarioBackupManager.beforeHaveCashableItemSortIdList.Count; n++)
			{
				HaveItemData item4 = new HaveItemData
				{
					itemSortID = scenarioBackupManager.beforeHaveCashableItemSortIdList[n],
					itemID = scenarioBackupManager.beforeHaveCashableItemIdList[n],
					haveCountNum = scenarioBackupManager.beforeHaveCashableItemHaveNumList[n]
				};
				PlayerInventoryDataManager.haveCashableItemList.Add(item4);
			}
			PlayerInventoryDataAccess.HaveItemListSort();
			PlayerInventoryDataManager.playerLearnedSkillList.Clear();
			for (int num = 0; num < scenarioBackupManager.beforeLearnedSkillList.Count; num++)
			{
				PlayerInventoryDataManager.playerLearnedSkillList.Add(scenarioBackupManager.beforeLearnedSkillList[num]);
			}
			PlayerDataManager.currentAccessPointName = PlayerNonSaveDataManager.beforeWorldMapPointName;
			PlayerDataManager.currentPlaceName = PlayerNonSaveDataManager.beforeLocalMapPlaceName;
			string battleBeforePointType = PlayerNonSaveDataManager.battleBeforePointType;
			if (!(battleBeforePointType == "WorldMap"))
			{
				if (battleBeforePointType == "LocalMap")
				{
					PlayerDataManager.mapPlaceStatusNum = 1;
					PlayerNonSaveDataManager.isUtageToLocalMap = true;
				}
			}
			else
			{
				PlayerDataManager.mapPlaceStatusNum = 0;
				PlayerDataManager.currentTimeZone = PlayerNonSaveDataManager.beforeCurrentTimeZone;
				PlayerDataManager.totalTimeZoneCount = PlayerNonSaveDataManager.beforeTotalTimeZoneCount;
				PlayerDataManager.currentMonthDay = PlayerNonSaveDataManager.beforeCurrentMonthDay;
				PlayerDataManager.currentTotalDay = PlayerNonSaveDataManager.beforeCurrentTotalDay;
			}
		}
		else
		{
			for (int num2 = 0; num2 < PlayerStatusDataManager.partyMemberCount; num2++)
			{
				PlayerStatusDataManager.characterHp[num2] = 1;
				PlayerStatusDataManager.characterSp[num2] = 0;
			}
			string battleBeforePointType = PlayerNonSaveDataManager.battleBeforePointType;
			if (!(battleBeforePointType == "WorldMap"))
			{
				if (battleBeforePointType == "LocalMap")
				{
					PlayerDataManager.mapPlaceStatusNum = 1;
					PlayerNonSaveDataManager.isUtageToLocalMap = true;
				}
			}
			else
			{
				PlayerDataManager.mapPlaceStatusNum = 0;
			}
		}
		string battleName = PlayerNonSaveDataManager.resultScenarioName;
		if (!GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == battleName).isNotRewindWhenDefeat)
		{
			PlayerFlagDataManager.scenarioFlagDictionary.Clear();
			foreach (KeyValuePair<string, bool> item5 in scenarioBackupManager.beforeScenarioFlagDictionary)
			{
				PlayerFlagDataManager.scenarioFlagDictionary.Add(item5.Key, item5.Value);
			}
		}
		GameObject.Find("Scenario Manager").GetComponent<ArborFSM>().SendTrigger("MoveMapSceneStart");
	}

	public void AddRewardWithoutBattle(AdvCommandSendMessageByName command)
	{
		string battleName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		ScenarioBattleData battleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == battleName);
		int i;
		for (i = 0; i < battleData.battleEnemyID.Count; i++)
		{
			PlayerDataManager.AddHaveMoney(GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == battleData.battleEnemyID[i]).enemyGold);
		}
		int j;
		for (j = 0; j < battleData.getItemID.Count; j++)
		{
			int itemSortID = PlayerInventoryDataAccess.GetItemSortID(battleData.getItemID[j]);
			if (itemSortID < 3000)
			{
				PlayerInventoryDataAccess.PlayerHaveItemAdd(battleData.getItemID[j], itemSortID, 1);
				continue;
			}
			PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == battleData.getItemID[j]));
		}
		int num = 0;
		float num2 = 0f;
		int k;
		for (k = 0; k < battleData.battleEnemyID.Count; k++)
		{
			BattleEnemyData battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == battleData.battleEnemyID[k]);
			num += battleEnemyData.enemyExp;
			num2 += (float)battleEnemyData.enemyLV;
		}
		float num3 = 0f;
		for (int l = 0; l < battleData.battleCharacterID.Count; l++)
		{
			num3 += (float)PlayerStatusDataManager.characterLv[battleData.battleCharacterID[l]];
		}
		num3 /= (float)battleData.battleCharacterID.Count;
		float num4 = num2;
		num4 /= (float)battleData.battleEnemyID.Count;
		Debug.Log("味方LV：" + num3 + "／敵LV：" + num4);
		if (num4 > num3)
		{
			float num5 = Mathf.Clamp(num4 / num3, 0f, 2f);
			float f = (float)num * num5;
			num = Mathf.FloorToInt(f);
			Debug.Log("LV差のEXPボーナス：" + Mathf.FloorToInt(f));
		}
		if (PlayerEquipDataManager.accessoryExpUp > 0)
		{
			num = Mathf.FloorToInt((float)num * 1.5f);
		}
		Debug.Log("獲得EXP：" + num);
		for (int m = 0; m < battleData.battleCharacterID.Count; m++)
		{
			PlayerStatusDataManager.characterExp[battleData.battleCharacterID[m]] += num;
		}
		for (int n = 0; n < battleData.battleEnemyID.Count; n++)
		{
			PlayerQuestDataManager.RefreshOrderedQuestEnemyCount(battleData.battleEnemyID[n]);
		}
	}
}
