using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonBattleFailed : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	private DungeonItemManager dungeonItemManager;

	public StateLink stateLink;

	public StateLink bossLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonItemManager = GameObject.Find("Dungeon Item Manager").GetComponent<DungeonItemManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isDungeonNoRetryBossBattle)
		{
			BattleFaileMethod();
		}
		else if (dungeonMapManager.isBossRouteSelect || PlayerNonSaveDataManager.isDungeonScnearioBattle)
		{
			Transition(bossLink);
		}
		else
		{
			BattleFaileMethod();
		}
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

	private void BattleFaileMethod()
	{
		PlayerNonSaveDataManager.isDungeonBattleFailedNotice = true;
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			dungeonBattleManager.messageWindowLoc[0].Term = "battleTextPlayerAllTarget";
		}
		else
		{
			dungeonBattleManager.messageWindowLoc[0].Term = "character0";
		}
		dungeonBattleManager.messageWindowGO.SetActive(value: true);
		dungeonBattleManager.messageTextArray[0].SetActive(value: true);
		dungeonBattleManager.messageWindowLoc[1].Term = "dungeonBattleFailed";
		float f = (float)dungeonMapManager.getDropMoney * 0.5f;
		dungeonMapManager.getDropMoney = Mathf.CeilToInt(f);
		int num = 0;
		foreach (KeyValuePair<int, int> item in dungeonMapManager.getDropItemDictionary)
		{
			num += item.Value;
		}
		int num2 = Mathf.CeilToInt((float)num * 0.5f);
		Debug.Log("獲得アイテム／減った後の数：" + num2);
		List<int> list = new List<int>();
		int num3 = 0;
		for (int num4 = num; num4 != num2; num4--)
		{
			list = new List<int>(dungeonMapManager.getDropItemDictionary.Keys);
			num3 = list[Random.Range(0, list.Count)];
			if (680 < num3 && num3 < 900)
			{
				Debug.Log("魔力片は減らさない");
			}
			else
			{
				dungeonMapManager.getDropItemDictionary[num3]--;
				if (dungeonMapManager.getDropItemDictionary[num3] == 0)
				{
					dungeonMapManager.getDropItemDictionary.Remove(num3);
				}
				Debug.Log("現在の所持数：" + num + "／削除したランダムKey：" + num3);
			}
		}
		dungeonMapManager.GetItemToHaveData();
		float time = 0.8f;
		Invoke("InvokeMethod", time);
	}

	private void InvokeMethod()
	{
		dungeonBattleManager.messageWindowGO.SetActive(value: false);
		MasterAudio.PlaySound("SeBattleDefeat", 1f, null, 0f, null, null);
		Transition(stateLink);
	}
}
