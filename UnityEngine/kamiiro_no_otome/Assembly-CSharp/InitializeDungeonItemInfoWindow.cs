using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeDungeonItemInfoWindow : StateBehaviour
{
	private DungeonItemInfoManager dungeonItemInfoManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonItemInfoManager = GameObject.Find("Dungeon Item Info Manager").GetComponent<DungeonItemInfoManager>();
	}

	public override void OnStateBegin()
	{
		DungeonMapData dungeonData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		GameDataManager.instance.dungeonItemInfoDataBase.dungeonGetItemInfoDataList.Find((DungeonGetItemInfoData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		dungeonItemInfoManager.dungeonFloorListByTenLevels.Clear();
		dungeonItemInfoManager.dungeonFloorListByMinLevels.Clear();
		dungeonItemInfoManager.dungeonFloorListByMaxLevels.Clear();
		PlayerDungeonBorderGetItemManager.separatorInMostLargeFloorList.Clear();
		bool flag = false;
		if (dungeonData.deepDungeonQuestFlag != int.MaxValue)
		{
			flag = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == dungeonData.deepDungeonQuestFlag).isOrdered;
			Debug.Log("深く潜れるクエストの受注：" + flag);
		}
		if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag) || dungeonData.deepDungeonQuestFlag != int.MaxValue || flag)
		{
			if (flag)
			{
				CheckDungeonBossCount(dungeonData, flag);
				Debug.Log("ダンジョン階数：クエスト受注済みで深く潜れる");
			}
			else if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag) && PlayerFlagDataManager.scenarioFlagDictionary[dungeonData.deepDungeonFlag])
			{
				CheckDungeonBossCount(dungeonData, flag);
				Debug.Log("ダンジョン階数：フラグクリア済みで深く潜れる");
			}
			else
			{
				dungeonItemInfoManager.dungeonMaxFloorNum = dungeonData.beforeFreeMaxFloor;
				int num = 0;
				for (int i = 0; i < dungeonData.borderFloorCount; i++)
				{
					if (dungeonData.dungeonBorderFloor[i] <= dungeonData.beforeFreeMaxFloor)
					{
						num++;
					}
				}
				dungeonItemInfoManager.dungeonVisibleBorderFloorCount = num;
				Debug.Log("ダンジョン階数：深く潜れるがフラグ未クリア");
			}
		}
		else
		{
			dungeonItemInfoManager.dungeonMaxFloorNum = dungeonData.beforeFreeMaxFloor;
			dungeonItemInfoManager.dungeonVisibleBorderFloorCount = dungeonData.borderFloorCount;
			Debug.Log("ダンジョン階数：通常ダンジョン");
		}
		if (dungeonData.dungeonName == "Dungeon4")
		{
			SetFloorByFiveLevels();
		}
		else
		{
			SetFloorByTenLevels();
		}
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

	private void CheckDungeonBossCount(DungeonMapData dungeonData, bool isQuestOrdered)
	{
		if (isQuestOrdered)
		{
			dungeonItemInfoManager.dungeonMaxFloorNum = dungeonData.maxFloor;
			dungeonItemInfoManager.dungeonVisibleBorderFloorCount = dungeonData.borderFloorCount;
			return;
		}
		if (dungeonData.dungeonName == "Dungeon4")
		{
			dungeonItemInfoManager.dungeonMaxFloorNum = dungeonData.maxFloor;
			dungeonItemInfoManager.dungeonVisibleBorderFloorCount = dungeonData.borderFloorCount;
			return;
		}
		int num = PlayerFlagDataManager.deepDungeonFlagDictionary[dungeonData.dungeonName];
		if (dungeonData.dungeonName == "Shrine1" && num > 0)
		{
			num--;
			Debug.Log("クワネロの10層分のクリア数を0にする");
		}
		int num2 = dungeonData.beforeFreeMaxFloor + num * 10;
		dungeonItemInfoManager.dungeonMaxFloorNum = num2;
		int num3 = 0;
		for (int i = 0; i < dungeonData.borderFloorCount; i++)
		{
			if (dungeonData.dungeonBorderFloor[i] < num2)
			{
				num3++;
			}
		}
		dungeonItemInfoManager.dungeonVisibleBorderFloorCount = num3;
	}

	private void SetFloorByTenLevels()
	{
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		int num = 0;
		int num2 = 0;
		if (dungeonMapData.maxFloor > 10)
		{
			int num3 = 1;
			dungeonItemInfoManager.dungeonFloorListByMinLevels.Add(1);
			for (int i = 0; i < dungeonItemInfoManager.dungeonVisibleBorderFloorCount; i++)
			{
				if (i == dungeonItemInfoManager.dungeonVisibleBorderFloorCount - 1)
				{
					if (dungeonMapData.dungeonBorderFloor[i] >= 41)
					{
						dungeonItemInfoManager.dungeonFloorListByTenLevels.Add(num);
						PlayerDungeonBorderGetItemManager.AddMostLargeFloorList(num2 - 1);
						dungeonItemInfoManager.dungeonFloorListByMinLevels.Add(dungeonMapData.dungeonBorderFloor[i]);
						dungeonItemInfoManager.dungeonFloorListByMaxLevels.Add(dungeonMapData.dungeonBorderFloor[i] - 1);
						num = 1;
						num2++;
						num3++;
						dungeonItemInfoManager.dungeonFloorListByTenLevels.Add(num);
						PlayerDungeonBorderGetItemManager.AddMostLargeFloorList(num2 - 1);
						dungeonItemInfoManager.dungeonFloorListByMaxLevels.Add(dungeonItemInfoManager.dungeonMaxFloorNum);
						Debug.Log("最後の境界：" + dungeonMapData.dungeonBorderFloor[i]);
					}
					else
					{
						num++;
						num2++;
						dungeonItemInfoManager.dungeonFloorListByTenLevels.Add(num);
						PlayerDungeonBorderGetItemManager.AddMostLargeFloorList(num2 - 1);
						dungeonItemInfoManager.dungeonFloorListByMaxLevels.Add(dungeonItemInfoManager.dungeonMaxFloorNum);
						Debug.Log("最後の境界：" + dungeonMapData.dungeonBorderFloor[i]);
					}
				}
				else if (dungeonMapData.dungeonBorderFloor[i] < 10 * num3)
				{
					num++;
					num2++;
					Debug.Log("区切りを超えていない：" + dungeonMapData.dungeonBorderFloor[i]);
				}
				else
				{
					dungeonItemInfoManager.dungeonFloorListByTenLevels.Add(num);
					PlayerDungeonBorderGetItemManager.AddMostLargeFloorList(num2 - 1);
					dungeonItemInfoManager.dungeonFloorListByMinLevels.Add(dungeonMapData.dungeonBorderFloor[i]);
					dungeonItemInfoManager.dungeonFloorListByMaxLevels.Add(dungeonMapData.dungeonBorderFloor[i] - 1);
					num = 1;
					num2++;
					num3++;
					Debug.Log("区切りを超えた：" + dungeonMapData.dungeonBorderFloor[i]);
				}
			}
			return;
		}
		for (int j = 0; j < dungeonItemInfoManager.dungeonVisibleBorderFloorCount; j++)
		{
			if (dungeonMapData.dungeonBorderFloor[j] < dungeonMapData.maxFloor)
			{
				num++;
				num2++;
			}
		}
		dungeonItemInfoManager.dungeonFloorListByTenLevels.Add(num);
		PlayerDungeonBorderGetItemManager.AddMostLargeFloorList(num2 - 1);
		dungeonItemInfoManager.dungeonFloorListByMinLevels.Add(1);
		dungeonItemInfoManager.dungeonFloorListByMaxLevels.Add(dungeonItemInfoManager.dungeonMaxFloorNum);
	}

	private void SetFloorByFiveLevels()
	{
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
		int num = 0;
		int num2 = 0;
		int num3 = 1;
		dungeonItemInfoManager.dungeonFloorListByMinLevels.Add(1);
		for (int i = 0; i < dungeonItemInfoManager.dungeonVisibleBorderFloorCount; i++)
		{
			if (i == dungeonItemInfoManager.dungeonVisibleBorderFloorCount - 1)
			{
				num++;
				num2++;
				dungeonItemInfoManager.dungeonFloorListByTenLevels.Add(num);
				PlayerDungeonBorderGetItemManager.AddMostLargeFloorList(num2 - 1);
				dungeonItemInfoManager.dungeonFloorListByMaxLevels.Add(dungeonItemInfoManager.dungeonMaxFloorNum);
				Debug.Log("男々島／最後の境界：" + dungeonMapData.dungeonBorderFloor[i]);
			}
			else if (dungeonMapData.dungeonBorderFloor[i] < 5 * num3)
			{
				num++;
				num2++;
				Debug.Log("男々島／区切りを超えていない：" + dungeonMapData.dungeonBorderFloor[i]);
			}
			else
			{
				dungeonItemInfoManager.dungeonFloorListByTenLevels.Add(num);
				PlayerDungeonBorderGetItemManager.AddMostLargeFloorList(num2 - 1);
				dungeonItemInfoManager.dungeonFloorListByMinLevels.Add(dungeonMapData.dungeonBorderFloor[i]);
				dungeonItemInfoManager.dungeonFloorListByMaxLevels.Add(dungeonMapData.dungeonBorderFloor[i] - 1);
				num = 1;
				num2++;
				num3++;
				Debug.Log("男々島／区切りを超えた：" + dungeonMapData.dungeonBorderFloor[i]);
			}
		}
	}
}
