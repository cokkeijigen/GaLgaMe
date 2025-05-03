using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class DungeonResultOnTheWay : StateBehaviour
{
	private ResultDialogManager resultDialogManager;

	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	public int itemDropProbability;

	private SortedDictionary<int, int> getDropItemIdDictionary = new SortedDictionary<int, int>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		resultDialogManager = GameObject.Find("Result Dialog Manager").GetComponent<ResultDialogManager>();
		resultDialogManager.isAutoCloseToggleInitialize = false;
		resultDialogManager.autoCloseToggle.isOn = PlayerDataManager.isResultAutoClose;
		resultDialogManager.autoCloseGroupGo.SetActive(value: false);
		resultDialogManager.isResultAnimationEnd = false;
		OpenOnTheWayResult();
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		resultDialogManager.isAutoCloseToggleInitialize = true;
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void OpenOnTheWayResult()
	{
		PlayerNonSaveDataManager.battleResultDialogType = "onTheWay";
		ResultReset();
		Sprite sprite = null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		resultDialogManager.resultCanvasGO.SetActive(value: true);
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			Transform transform = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.characterImagePrefabGo, resultDialogManager.characterImageParentGo.transform);
			int num4 = PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[i]];
			transform.transform.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>().text = num4.ToString();
			transform.transform.Find("Mask/Face Image").GetComponent<Image>().sprite = GameDataManager.instance.playerResultFrameSprite[PlayerStatusDataManager.playerPartyMember[i]];
			resultDialogManager.characterImageSpawnGoList.Add(transform.gameObject);
			int num5 = PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[i]];
			Slider component = transform.transform.Find("Exp Slider").GetComponent<Slider>();
			component.maxValue = PlayerStatusDataManager.characterNextLvExp[PlayerStatusDataManager.playerPartyMember[i]];
			component.minValue = PlayerStatusDataManager.characterCurrentLvExp[PlayerStatusDataManager.playerPartyMember[i]];
			component.value = num5;
			transform.gameObject.GetComponent<ParameterContainer>().SetInt("characterID", PlayerStatusDataManager.playerPartyMember[i]);
			transform.gameObject.GetComponent<ParameterContainer>().SetInt("levelUpCount", 0);
		}
		num += dungeonMapManager.consecutiveResultData[0];
		if (PlayerEquipDataManager.accessoryDropMoneyUp > 0)
		{
			num = Mathf.FloorToInt((float)num * 1.5f);
		}
		num2 += dungeonMapManager.consecutiveResultData[1];
		float num6 = 0f;
		for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
		{
			num6 += (float)PlayerStatusDataManager.characterLv[PlayerStatusDataManager.playerPartyMember[j]];
		}
		num6 /= (float)PlayerStatusDataManager.playerPartyMember.Length;
		float num7 = PlayerStatusDataManager.enemyLv.Sum();
		num7 /= (float)PlayerStatusDataManager.enemyMember.Length;
		Debug.Log("味方LV：" + num6 + "／敵LV：" + num7);
		if (num7 > num6)
		{
			float num8 = Mathf.Clamp(num7 / num6, 0f, 2f);
			float f = (float)num2 * num8;
			num2 = Mathf.FloorToInt(f);
			Debug.Log("LV差のEXPボーナス：" + Mathf.FloorToInt(f));
		}
		if (PlayerEquipDataManager.accessoryExpUp > 0)
		{
			num2 = Mathf.FloorToInt((float)num2 * 1.5f);
		}
		resultDialogManager.expText.text = num2.ToString();
		int accessoryItemDiscover = PlayerEquipDataManager.accessoryItemDiscover;
		for (int k = 0; k < dungeonMapManager.consecutiveResultEnemyMember.Count; k++)
		{
			int num9 = Random.Range(0, 100);
			Debug.Log("獲得判定：" + num9 + "/アイテム取得確率：" + (itemDropProbability + accessoryItemDiscover));
			if (itemDropProbability + accessoryItemDiscover >= num9)
			{
				int id = dungeonMapManager.consecutiveResultEnemyMember[k];
				Dictionary<int, float> dropItemDictionary = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id).dropItemDictionary;
				int dungeonBattleDropItem = dungeonMapManager.GetDungeonBattleDropItem(dropItemDictionary);
				AddDropItemIdDictionary(dungeonBattleDropItem);
				dungeonMapManager.SetDropItemDictionary(dungeonBattleDropItem, bonusChance: false);
			}
		}
		if (!dungeonMapManager.isBossRouteSelect)
		{
			int num10 = Random.Range(0, 100);
			Debug.Log("粉末獲得判定：" + num10 + "/アイテム取得確率：" + (itemDropProbability / 3 + accessoryItemDiscover));
			if (itemDropProbability / 3 + accessoryItemDiscover >= num10)
			{
				int num11 = 850;
				AddDropItemIdDictionary(num11);
				dungeonMapManager.SetDropItemDictionary(num11, bonusChance: false);
			}
			int num12 = Random.Range(0, 100);
			Debug.Log("亡骸獲得判定：" + num12 + "/アイテム取得確率：" + (itemDropProbability / 3 + accessoryItemDiscover));
			if (itemDropProbability / 3 + accessoryItemDiscover >= num12)
			{
				int num13 = Random.Range(0, dungeonMapData.getCashableItemID[dungeonMapManager.currentBorderNum].Length);
				int num14 = dungeonMapData.getCashableItemID[dungeonMapManager.currentBorderNum][num13];
				AddDropItemIdDictionary(num14);
				dungeonMapManager.SetDropItemDictionary(num14, bonusChance: false);
			}
			int num15 = Random.Range(0, 100);
			Debug.Log("亡骸獲得判定：" + num15 + "/アイテム取得確率：" + (itemDropProbability / 3 + accessoryItemDiscover));
			if (itemDropProbability / 3 + accessoryItemDiscover >= num15)
			{
				int num16 = Random.Range(0, dungeonMapData.getTreasureItemID[dungeonMapManager.currentBorderNum].Length);
				int num17 = dungeonMapData.getTreasureItemID[dungeonMapManager.currentBorderNum][num16];
				AddDropItemIdDictionary(num17);
				dungeonMapManager.SetDropItemDictionary(num17, bonusChance: false);
			}
			foreach (KeyValuePair<int, int> item in getDropItemIdDictionary)
			{
				Transform transform2 = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.itemImagePrefabGo, resultDialogManager.itemImageParentGo.transform);
				resultDialogManager.itemImageSpawnGoList.Add(transform2.gameObject);
				transform2.GetComponent<ParameterContainer>().SetInt("itemID", item.Key);
				transform2.transform.Find("Count Group/Count Text").GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
				sprite = PlayerInventoryDataAccess.GetItemSprite(item.Key);
				transform2.transform.Find("Mask/Image").GetComponent<Image>().sprite = sprite;
			}
			if (PlayerNonSaveDataManager.preGetDropMagicMaterial.Count > 0)
			{
				dungeonMapManager.GetDropMagicMaterialDictionary();
				foreach (KeyValuePair<int, int> item2 in dungeonMapManager.getDropMagicMaterialDictionary)
				{
					dungeonMapManager.SetDropMagicMateterialDictionary(item2.Key, item2.Value);
					int key = item2.Key;
					Transform transform3 = PoolManager.Pools["ResultDialogPool"].Spawn(resultDialogManager.itemImagePrefabGo, resultDialogManager.itemImageParentGo.transform);
					resultDialogManager.itemImageSpawnGoList.Add(transform3.gameObject);
					transform3.GetComponent<ParameterContainer>().SetInt("itemID", key);
					transform3.transform.Find("Count Group/Count Text").GetComponent<TextMeshProUGUI>().text = item2.Value.ToString();
					sprite = PlayerInventoryDataAccess.GetItemSprite(key);
					transform3.transform.Find("Mask/Image").GetComponent<Image>().sprite = sprite;
					transform3.GetComponent<ArborFSM>().SendTrigger("ResultIteminitialize");
				}
				Debug.Log("魔力片を戦利品に追加");
			}
		}
		if (dungeonMapManager.selectCardBonusArray[dungeonMapManager.thisFloorActionNum] > 1)
		{
			if (dungeonMapManager.thisFloorActionNum > 0)
			{
				num3 = GetExpBonus(num2);
			}
			resultDialogManager.bonusExpGroupGo.SetActive(value: true);
		}
		else
		{
			resultDialogManager.bonusExpGroupGo.SetActive(value: false);
		}
		resultDialogManager.bonusExpText.text = num3.ToString();
		for (int l = 0; l < PlayerStatusDataManager.playerPartyMember.Length; l++)
		{
			PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[l]] += num2;
			PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[l]] += num3;
			PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[l]] = Mathf.Clamp(PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[l]], 0, 999999);
		}
		for (int m = 0; m < dungeonMapManager.consecutiveResultEnemyMember.Count; m++)
		{
			PlayerQuestDataManager.RefreshOrderedQuestEnemyCount(dungeonMapManager.consecutiveResultEnemyMember[m]);
		}
		PlayerDataManager.AddHaveMoney(num);
		resultDialogManager.goldText.text = num.ToString();
		resultDialogManager.SetDropItemGroupUiElements();
	}

	private void ResultReset()
	{
		resultDialogManager.DespawnResultGetItem();
		resultDialogManager.characterImageSpawnGoList.Clear();
		resultDialogManager.itemImageSpawnGoList.Clear();
	}

	private int GetExpBonus(int originExp)
	{
		DungeonMapManager component = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		int num = component.thisFloorActionNum - 1;
		float num2 = component.selectCardBonusArray[num] - 1;
		num2 /= 10f;
		int result = (int)((float)originExp * num2);
		Debug.Log("ボーナスEXP：" + result + "／ボーナス倍率：" + num2);
		return result;
	}

	private void AddDropItemIdDictionary(int itemId)
	{
		if (getDropItemIdDictionary.ContainsKey(itemId))
		{
			getDropItemIdDictionary[itemId]++;
			Debug.Log("ドロップアイテム同じKeyがある／Key：" + itemId + "／数量：" + getDropItemIdDictionary[itemId]);
		}
		else
		{
			getDropItemIdDictionary.Add(itemId, 1);
			Debug.Log("ドロップアイテム同じKeyはない／Key：" + itemId);
		}
	}
}
