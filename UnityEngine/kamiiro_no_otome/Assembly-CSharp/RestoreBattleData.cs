using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RestoreBattleData : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonBattleManager dungeonBattleManager;

	public float waitTime;

	public bool isReengeBattle;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		GameObject.Find("Battle Failed Canvas").SetActive(value: false);
		if (!isReengeBattle)
		{
			if (dungeonMapManager.isBossRouteSelect)
			{
				dungeonMapManager.dungeonCurrentFloorNum = dungeonMapManager.dungeonMaxFloorNum;
				dungeonMapManager.dungeonCurrentFloorText.text = dungeonMapManager.dungeonCurrentFloorNum.ToString();
				dungeonMapManager.bossButton.SetActive(value: true);
				dungeonMapManager.isBossRouteSelect = false;
				PlayerNonSaveDataManager.isDungeonBossBattle = false;
				PlayerNonSaveDataManager.isDungeonNoRetryBossBattle = false;
				Debug.Log("ボスルートに戻る");
			}
			else
			{
				dungeonMapManager.dungeonCurrentFloorNum--;
				dungeonMapManager.dungeonCurrentFloorText.text = dungeonMapManager.dungeonCurrentFloorNum.ToString();
				Debug.Log("現在の階数から一階上に戻る");
			}
		}
		PlayerStatusDataManager.playerAllHp = PlayerNonSaveDataManager.beforePlayerAllHp;
		for (int i = 0; i < 5; i++)
		{
			PlayerStatusDataManager.characterSp[i] = PlayerNonSaveDataManager.beforePlayerSp[i];
		}
		PlayerInventoryDataManager.haveItemList.Clear();
		for (int j = 0; j < PlayerNonSaveDataManager.beforeHaveItemSortIdList.Count; j++)
		{
			HaveItemData item = new HaveItemData
			{
				itemSortID = PlayerNonSaveDataManager.beforeHaveItemSortIdList[j],
				itemID = PlayerNonSaveDataManager.beforeHaveItemIdList[j],
				haveCountNum = PlayerNonSaveDataManager.beforeHaveItemHaveNumList[j]
			};
			PlayerInventoryDataManager.haveItemList.Add(item);
		}
		PlayerInventoryDataAccess.HaveItemListSort();
		PlayerNonSaveDataManager.isRestoreDungeonBattleFailed = false;
		PlayerNonSaveDataManager.isScenarioBattle = false;
		PlayerNonSaveDataManager.isUtagePlayBattleBgm = false;
		Invoke("InvokeMethod", waitTime);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
