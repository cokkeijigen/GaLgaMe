using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SwitchDialogOkButton : StateBehaviour
{
	public StateLink stateLink;

	public StateLink dungeonMapRetreatLink;

	public StateLink dungeonBattleRetreatLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "saveLoad":
		{
			ArborFSM component3 = GameObject.Find("SaveLoad Manager").GetComponent<ArborFSM>();
			if (PlayerNonSaveDataManager.selectSystemTabName == "save")
			{
				component3.SendTrigger("ExecuteSave");
			}
			else if (PlayerNonSaveDataManager.selectSystemTabName == "load")
			{
				component3.SendTrigger("ExecuteLoad");
			}
			break;
		}
		case "quit":
			Application.Quit();
			break;
		case "retreat":
			GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>().isRetreat = true;
			GameObject.Find("Scenario Manager").GetComponent<ArborFSM>().SendTrigger("MoveMapSceneStart");
			break;
		case "dungeonMapRetreat":
		{
			DungeonMapManager component2 = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
			Time.timeScale = 1f;
			if (PlayerDataManager.isDungeonMapAuto)
			{
				PlayerDataManager.isDungeonMapAuto = false;
				component2.autoAlertGroup.SetActive(value: false);
				component2.autoButtonLoc.Term = "buttonDungeonMapAutoStart";
			}
			Transition(dungeonMapRetreatLink);
			break;
		}
		case "dungeonBattleRetreat":
			Transition(dungeonBattleRetreatLink);
			break;
		case "dungeonMapAuto":
		{
			DungeonMapManager component = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
			Time.timeScale = 1f;
			if (PlayerDataManager.isDungeonMapAuto)
			{
				PlayerDataManager.isDungeonMapAuto = false;
				component.autoAlertGroup.SetActive(value: false);
				component.autoButtonLoc.Term = "buttonDungeonMapAutoStart";
			}
			else
			{
				PlayerDataManager.isDungeonMapAuto = true;
				component.autoAlertGroup.SetActive(value: true);
				component.autoButtonLoc.Term = "buttonDungeonMapAutoStop";
				GameObject.Find("Dungeon Map Manager").GetComponent<ArborFSM>().SendTrigger("DungeonAutoStart");
			}
			break;
		}
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
