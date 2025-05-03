using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CloseDungeonBattleResult : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonBattleManager dungeonBattleManager;

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
		dungeonMapManager.isMimicBattle = false;
		PlayerNonSaveDataManager.isDungeonScnearioBattle = false;
		PlayerNonSaveDataManager.isMoveToDungeonBattle = false;
		PlayerNonSaveDataManager.isUtagePlayBattleBgm = false;
		GameObject.Find("Battle Result Canvas").SetActive(value: false);
		dungeonMapManager.dungeonBattleCanvas.SetActive(value: false);
		dungeonBattleManager.battleCommandCanvasGroup.interactable = true;
		dungeonBattleManager.battleCommandCanvasGroup.alpha = 1f;
		dungeonBattleManager.battleSubButtonCanvasGroup.interactable = true;
		dungeonBattleManager.battleSubButtonCanvasGroup.alpha = 1f;
		if (dungeonMapManager.isBossRouteSelect)
		{
			Debug.Log("ボスルートのリザルト");
			dungeonMapManager.dungeonMapCanvas.SetActive(value: false);
			dungeonMapManager.GetItemToHaveData();
			DungeonMapData dungeonData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData item) => item.dungeonName == PlayerDataManager.currentDungeonName);
			bool flag = false;
			bool flag2 = false;
			if (dungeonData.deepDungeonQuestFlag != int.MaxValue)
			{
				flag = true;
				flag2 = PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == dungeonData.deepDungeonQuestFlag).isClear;
			}
			if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag) || flag)
			{
				if (flag && !flag2)
				{
					if (dungeonData.extraQuestBossID != int.MaxValue)
					{
						if (PlayerStatusDataManager.enemyMember.Contains(dungeonData.extraQuestBossID))
						{
							int value = Mathf.Clamp(dungeonMapManager.dungeonMaxFloorNum / 10, 0, dungeonData.deepDungeonNoticeCount);
							PlayerFlagDataManager.deepDungeonFlagDictionary[PlayerDataManager.currentDungeonName] = value;
							PlayerNonSaveDataManager.noticeDungeonTermString = "area" + PlayerDataManager.currentDungeonName;
							PlayerNonSaveDataManager.isDeepDungeonNotice = true;
							Debug.Log("特殊クエストボス／最終ボスではないので、深く潜れる通知はあり");
						}
					}
					else
					{
						Debug.Log("ストーリー上のボスなので、深く潜れる通知はなし");
					}
				}
				else if (!string.IsNullOrEmpty(dungeonData.deepDungeonFlag))
				{
					if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonData.deepDungeonFlag])
					{
						if (PlayerDataManager.currentDungeonName == "Shrine1" && dungeonMapManager.dungeonCurrentFloorNum == 40)
						{
							if (PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == 11).isOrdered)
							{
								int value2 = dungeonMapManager.dungeonMaxFloorNum / 10;
								int value3 = Mathf.Clamp(value2, 0, dungeonData.deepDungeonNoticeCount);
								PlayerFlagDataManager.deepDungeonFlagDictionary[PlayerDataManager.currentDungeonName] = value3;
								Debug.Log("ボスがいた階層num：" + value2 + "／深く潜れるフラグnum：" + value3);
								PlayerNonSaveDataManager.noticeDungeonTermString = "area" + PlayerDataManager.currentDungeonName;
								PlayerNonSaveDataManager.isDeepDungeonNotice = true;
								Debug.Log("クワネロ40層ボス／クエスト受注済み／最終ボスではないので、深く潜れる通知はあり");
							}
							else
							{
								Debug.Log("クエスト受注前なので、深く潜れる通知はなし");
							}
						}
						else
						{
							int num = dungeonMapManager.dungeonMaxFloorNum / 10;
							int value4 = Mathf.Clamp(num, 0, dungeonData.deepDungeonNoticeCount);
							PlayerFlagDataManager.deepDungeonFlagDictionary[PlayerDataManager.currentDungeonName] = value4;
							Debug.Log("ボスがいた階層num：" + num + "／深く潜れるフラグnum：" + value4);
							if (num <= dungeonData.deepDungeonNoticeCount)
							{
								PlayerNonSaveDataManager.noticeDungeonTermString = "area" + PlayerDataManager.currentDungeonName;
								PlayerNonSaveDataManager.isDeepDungeonNotice = true;
								Debug.Log("通常ボス／最終ボスではないので、深く潜れる通知はあり");
							}
							else
							{
								Debug.Log("最終ボスなので、深く潜れる通知はなし");
							}
						}
					}
					else
					{
						Debug.Log("深く潜れるフラグをクリアしていない：" + dungeonData.deepDungeonFlag);
					}
				}
				else
				{
					Debug.Log("深く潜れるクエストを受注していないorダンジョン踏破済み：" + dungeonData.deepDungeonQuestFlag);
				}
			}
			else
			{
				Debug.Log("深く潜れるダンジョンではない");
			}
		}
		else if (PlayerNonSaveDataManager.isDungeonScnearioBattle)
		{
			Debug.Log("ダンジョンのシナリオ戦闘");
			dungeonMapManager.dungeonMapCanvas.SetActive(value: false);
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
}
