using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckIsFreeDungeon : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public bool isThisDungeon;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		totalMapAccessManager.dialogAlertGroupGo.SetActive(value: false);
		if (PlayerDataManager.isSelectDungeon || isThisDungeon)
		{
			DungeonMapData dungeonMapData;
			if (PlayerDataManager.isSelectDungeon)
			{
				dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == base.transform.parent.name);
				if (dungeonMapData == null)
				{
					dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerNonSaveDataManager.selectAccessPointName);
					Debug.Log("ダンジョンデータがnullだった");
				}
			}
			else
			{
				dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == base.transform.parent.name);
			}
			if (dungeonMapData.dungeonType == DungeonMapData.Type.scenario)
			{
				if (PlayerFlagDataManager.scenarioFlagDictionary[dungeonMapData.freeDungeonFlag])
				{
					Debug.Log("フリー化フラグをクリア済み");
					Transition(stateLink);
					return;
				}
				int beforeFreeNeedHeroineID = dungeonMapData.beforeFreeNeedHeroineID;
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					if (beforeFreeNeedHeroineID == 0)
					{
						totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredSolo";
						Debug.Log("フリーフラグ未クリア／ソロオンリー");
						OpenMapAlertDialog();
						return;
					}
					if (PlayerDataManager.DungeonHeroineFollowNum != beforeFreeNeedHeroineID)
					{
						switch (beforeFreeNeedHeroineID)
						{
						case 9:
							break;
						case 1:
							PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredLucy";
							goto default;
						case 2:
							PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredRina";
							goto default;
						case 3:
							PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredShia";
							goto default;
						case 4:
							PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredLevy";
							goto default;
						default:
							Debug.Log("フリーフラグ未クリア／要同行ヒロインがいない");
							OpenMapAlertDialog();
							return;
						}
					}
					Transition(stateLink);
				}
				else
				{
					switch (beforeFreeNeedHeroineID)
					{
					case 0:
					case 9:
						Transition(stateLink);
						return;
					case 1:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredLucy";
						break;
					case 2:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredRina";
						break;
					case 3:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredShia";
						break;
					case 4:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredLevy";
						break;
					}
					Debug.Log("フリーフラグ未クリア／要同行ヒロインがいない");
					OpenMapAlertDialog();
				}
				return;
			}
			Debug.Log("シナリオダンジョンではない");
			if (dungeonMapData.name == "Dungeon2")
			{
				if (PlayerFlagDataManager.scenarioFlagDictionary["H_Rina_001-3"] && !PlayerFlagDataManager.scenarioFlagDictionary["MH_Rina_014"])
				{
					if (PlayerDataManager.isDungeonHeroineFollow)
					{
						Debug.Log("東砦周辺／ヒロイン同行中");
					}
					else
					{
						PlayerNonSaveDataManager.isWorldMapPointDialogAlert = true;
						Debug.Log("東砦周辺／リィナ編の最中");
					}
				}
				else
				{
					Debug.Log("東砦周辺／指定範囲ではない");
				}
			}
			Transition(stateLink);
		}
		else
		{
			Transition(stateLink);
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

	private void OpenMapAlertDialog()
	{
		totalMapAccessManager.worldMapFSM.SendTrigger("WorldMapPointClickDisable");
	}
}
