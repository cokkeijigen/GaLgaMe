using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcSkipBattleResult : StateBehaviour
{
	private ResultDialogManager resultDialogManager;

	public int itemDropProbability;

	private SortedDictionary<int, int> getDropItemIdDictionary = new SortedDictionary<int, int>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		resultDialogManager = GetComponent<ResultDialogManager>();
	}

	public override void OnStateBegin()
	{
		ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.skipScenarioBattleName);
		PlayerStatusDataManager.enemyGold.Clear();
		PlayerStatusDataManager.enemyExp.Clear();
		PlayerStatusDataManager.enemyLv.Clear();
		foreach (int id in scenarioBattleData.battleEnemyID)
		{
			BattleEnemyData battleEnemyData = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id);
			PlayerStatusDataManager.enemyGold.Add(battleEnemyData.enemyGold);
			PlayerStatusDataManager.enemyExp.Add(battleEnemyData.enemyExp);
			PlayerStatusDataManager.enemyLv.Add(battleEnemyData.enemyExp);
		}
		int num = PlayerStatusDataManager.enemyGold.Sum();
		float num2 = Random.Range(0.9f, 1.1f);
		num = Mathf.FloorToInt((float)num * num2);
		if (PlayerEquipDataManager.accessoryDropMoneyUp > 0)
		{
			num = Mathf.FloorToInt((float)num * 1.5f);
		}
		PlayerStatusDataManager.playerPartyMember = scenarioBattleData.battleCharacterID.ToArray();
		PlayerStatusDataManager.enemyMember = scenarioBattleData.battleEnemyID.ToArray();
		int num3 = PlayerStatusDataManager.enemyExp.Sum();
		float num4 = 0f;
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			num4 += (float)PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[i]];
		}
		num4 /= (float)PlayerStatusDataManager.playerPartyMember.Length;
		float num5 = PlayerStatusDataManager.enemyLv.Sum();
		num5 /= (float)PlayerStatusDataManager.enemyMember.Length;
		Debug.Log("味方LV：" + num4 + "／敵LV：" + num5);
		if (num5 > num4)
		{
			float num6 = Mathf.Clamp(num5 / num4, 0f, 2f);
			float f = (float)num3 * num6;
			num3 = Mathf.FloorToInt(f);
			Debug.Log("LV差のEXPボーナス：" + Mathf.FloorToInt(f));
		}
		if (PlayerEquipDataManager.accessoryExpUp > 0)
		{
			num3 = Mathf.FloorToInt((float)num3 * 1.5f);
		}
		PlayerDataManager.AddHaveMoney(num);
		List<int> itemList = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData item) => item.scenarioName == PlayerNonSaveDataManager.skipScenarioBattleName).getItemID;
		int j;
		for (j = 0; j < itemList.Count; j++)
		{
			int itemSortID = PlayerInventoryDataAccess.GetItemSortID(itemList[j]);
			if (itemSortID < 3000)
			{
				PlayerInventoryDataAccess.PlayerHaveItemAdd(itemList[j], itemSortID, 1);
				continue;
			}
			PlayerInventoryDataEquipAccess.PlayerHaveAccessoryAdd(GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == itemList[j]));
		}
		for (int k = 0; k < PlayerStatusDataManager.playerPartyMember.Length; k++)
		{
			PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[k]] += num3;
		}
		for (int l = 0; l < PlayerStatusDataManager.enemyMember.Length; l++)
		{
			PlayerQuestDataManager.RefreshOrderedQuestEnemyCount(PlayerStatusDataManager.enemyMember[l]);
		}
		PlayerInventoryDataAccess.HaveItemListSortAll();
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
