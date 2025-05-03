using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CloseBattleResultCanvas : StateBehaviour
{
	private ResultDialogManager resultDialogManager;

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
		resultDialogManager.DespawnResultGetItem();
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
		switch (PlayerNonSaveDataManager.battleResultDialogType)
		{
		case "scenarioBattle":
			PlayerNonSaveDataManager.isScenarioBattle = false;
			PlayerNonSaveDataManager.isUtagePlayBattleBgm = false;
			if (GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData item) => item.scenarioName == PlayerNonSaveDataManager.resultScenarioName).isDefeatBattle)
			{
				for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					PlayerStatusDataManager.characterHp[PlayerStatusDataManager.playerPartyMember[i]] = 1;
					PlayerStatusDataManager.characterMp[PlayerStatusDataManager.playerPartyMember[i]] = PlayerStatusDataManager.characterMaxMp[PlayerStatusDataManager.playerPartyMember[i]];
				}
			}
			else
			{
				for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
				{
					PlayerStatusDataManager.characterHp[PlayerStatusDataManager.playerPartyMember[j]] = PlayerStatusDataManager.characterMaxHp[PlayerStatusDataManager.playerPartyMember[j]];
					PlayerStatusDataManager.characterMp[PlayerStatusDataManager.playerPartyMember[j]] = PlayerStatusDataManager.characterMaxMp[PlayerStatusDataManager.playerPartyMember[j]];
				}
			}
			resultDialogManager.resultCanvasGO.SetActive(value: false);
			GameObject.Find("Battle Result Manager").GetComponent<ArborFSM>().SendTrigger("ScenairoBattleResultEnd");
			Debug.Log("シナリオバトル終了");
			break;
		case "dungeonBattle":
			GameObject.Find("Dungeon Result Manager").GetComponent<ArborFSM>().SendTrigger("DungeonBattleResultEnd");
			Debug.Log("ダンジョンバトル終了");
			break;
		case "onTheWay":
			resultDialogManager.resultCanvasGO.SetActive(value: false);
			PlayerStatusDataManager.CalcAllHpToCharacterHp();
			PlayerDataManager.mapPlaceStatusNum = 0;
			GameObject.Find("Scenario Manager").GetComponent<ArborFSM>().SendTrigger("MoveMapSceneStart");
			break;
		case "dungeonBossBattle":
			PlayerStatusDataManager.CalcAllHpToCharacterHp();
			resultDialogManager.resultCanvasGO.SetActive(value: false);
			GameObject.Find("Battle Result Manager").GetComponent<ArborFSM>().SendTrigger("ScenairoBattleResultEnd");
			break;
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
