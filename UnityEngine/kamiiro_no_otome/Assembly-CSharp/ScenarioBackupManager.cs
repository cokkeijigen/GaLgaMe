using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScenarioBackupManager : SerializedMonoBehaviour
{
	public int beforePlayerMoney;

	public int[] beforePlayerExp = new int[PlayerStatusDataManager.partyMemberCount];

	public int[] beforePlayerHp = new int[PlayerStatusDataManager.partyMemberCount];

	public int[] beforePlayerMp = new int[PlayerStatusDataManager.partyMemberCount];

	public int beforeEquipWeaponTp;

	public List<int> beforeHaveItemSortIdList = new List<int>();

	public List<int> beforeHaveItemIdList = new List<int>();

	public List<int> beforeHaveItemHaveNumList = new List<int>();

	public List<int> beforeHaveMaterialSortIdList = new List<int>();

	public List<int> beforeHaveMaterialIdList = new List<int>();

	public List<int> beforeHaveMaterialHaveNumList = new List<int>();

	public List<int> beforeHaveMagicMaterialSortIdList = new List<int>();

	public List<int> beforeHaveMagicMaterialIdList = new List<int>();

	public List<int> beforeHaveMagicMaterialHaveNumList = new List<int>();

	public List<int> beforeHaveCashableItemSortIdList = new List<int>();

	public List<int> beforeHaveCashableItemIdList = new List<int>();

	public List<int> beforeHaveCashableItemHaveNumList = new List<int>();

	public Dictionary<string, bool> beforeScenarioFlagDictionary = new Dictionary<string, bool>();

	public List<LearnedSkillData> beforeLearnedSkillList = new List<LearnedSkillData>();

	public void BackupBeforeScenarioStart()
	{
		if (PlayerNonSaveDataManager.isTitleDebugToUtage || PlayerNonSaveDataManager.isUtageToJumpFromGarelly)
		{
			Debug.Log("デバッグor鑑賞モードなのでバックアップしない");
			return;
		}
		beforePlayerMoney = PlayerDataManager.playerHaveMoney;
		for (int i = 0; i < PlayerStatusDataManager.partyMemberCount; i++)
		{
			beforePlayerExp[i] = PlayerStatusDataManager.characterExp[i];
			beforePlayerHp[i] = PlayerStatusDataManager.characterHp[i];
			beforePlayerMp[i] = PlayerStatusDataManager.characterMp[i];
		}
		HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.equipCharacter == 0);
		beforeEquipWeaponTp = haveWeaponData.weaponIncludeMp;
		beforeHaveItemSortIdList.Clear();
		beforeHaveItemIdList.Clear();
		beforeHaveItemHaveNumList.Clear();
		for (int j = 0; j < PlayerInventoryDataManager.haveItemList.Count; j++)
		{
			beforeHaveItemSortIdList.Add(PlayerInventoryDataManager.haveItemList[j].itemSortID);
			beforeHaveItemIdList.Add(PlayerInventoryDataManager.haveItemList[j].itemID);
			beforeHaveItemHaveNumList.Add(PlayerInventoryDataManager.haveItemList[j].haveCountNum);
		}
		beforeHaveMaterialSortIdList.Clear();
		beforeHaveMaterialIdList.Clear();
		beforeHaveMaterialHaveNumList.Clear();
		for (int k = 0; k < PlayerInventoryDataManager.haveItemMaterialList.Count; k++)
		{
			beforeHaveMaterialSortIdList.Add(PlayerInventoryDataManager.haveItemMaterialList[k].itemSortID);
			beforeHaveMaterialIdList.Add(PlayerInventoryDataManager.haveItemMaterialList[k].itemID);
			beforeHaveMaterialHaveNumList.Add(PlayerInventoryDataManager.haveItemMaterialList[k].haveCountNum);
		}
		beforeHaveMagicMaterialSortIdList.Clear();
		beforeHaveMagicMaterialIdList.Clear();
		beforeHaveMagicMaterialHaveNumList.Clear();
		for (int l = 0; l < PlayerInventoryDataManager.haveItemMagicMaterialList.Count; l++)
		{
			beforeHaveMagicMaterialSortIdList.Add(PlayerInventoryDataManager.haveItemMagicMaterialList[l].itemSortID);
			beforeHaveMagicMaterialIdList.Add(PlayerInventoryDataManager.haveItemMagicMaterialList[l].itemID);
			beforeHaveMagicMaterialHaveNumList.Add(PlayerInventoryDataManager.haveItemMagicMaterialList[l].haveCountNum);
		}
		beforeHaveCashableItemSortIdList.Clear();
		beforeHaveCashableItemIdList.Clear();
		beforeHaveCashableItemHaveNumList.Clear();
		for (int m = 0; m < PlayerInventoryDataManager.haveCashableItemList.Count; m++)
		{
			beforeHaveCashableItemSortIdList.Add(PlayerInventoryDataManager.haveCashableItemList[m].itemSortID);
			beforeHaveCashableItemIdList.Add(PlayerInventoryDataManager.haveCashableItemList[m].itemID);
			beforeHaveCashableItemHaveNumList.Add(PlayerInventoryDataManager.haveCashableItemList[m].haveCountNum);
		}
		beforeScenarioFlagDictionary.Clear();
		foreach (KeyValuePair<string, bool> item in PlayerFlagDataManager.scenarioFlagDictionary)
		{
			beforeScenarioFlagDictionary.Add(item.Key, item.Value);
		}
		beforeLearnedSkillList.Clear();
		for (int n = 0; n < PlayerInventoryDataManager.playerLearnedSkillList.Count; n++)
		{
			beforeLearnedSkillList.Add(PlayerInventoryDataManager.playerLearnedSkillList[n]);
		}
		Debug.Log("敗戦時の巻き戻し用のステータスをバックアップ完了");
	}
}
