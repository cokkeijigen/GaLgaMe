using Arbor;
using I2.Loc;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public Localize headerTextLoc;

	public Localize tutorialTextLoc;

	public CanvasGroup endButtonCanvasGroup;

	public void Awake()
	{
		switch (PlayerNonSaveDataManager.selectTutorialName)
		{
		case "scenarioBattle1":
			headerTextLoc.Term = "help_battle_title";
			tutorialTextLoc.Term = "tutorial_scenarioBattle1";
			break;
		case "scenarioBattle2":
			headerTextLoc.Term = "help_battle_title";
			tutorialTextLoc.Term = "tutorial_scenarioBattle2";
			break;
		case "scenarioBattle3":
			headerTextLoc.Term = "help_battle_title";
			tutorialTextLoc.Term = "tutorial_scenarioBattle3";
			break;
		case "chargeAttack":
			headerTextLoc.Term = "buttonBattleChargeAttack";
			tutorialTextLoc.Term = "tutorial_scenarioBattle_charge";
			break;
		case "dungeonMap":
			headerTextLoc.Term = "help_dungeon_title";
			tutorialTextLoc.Term = "tutorial_dungeonMap";
			break;
		case "dungeonBoss":
			headerTextLoc.Term = "help_dungeon_boss";
			tutorialTextLoc.Term = "tutorial_dungeonBoss";
			break;
		case "dungeonBattle":
			headerTextLoc.Term = "help_dungeon_battle";
			tutorialTextLoc.Term = "tutorial_dungeonBattle";
			break;
		case "craft":
			headerTextLoc.Term = "help_carriage_title";
			tutorialTextLoc.Term = "tutorial_craft";
			break;
		case "carriageStore":
			headerTextLoc.Term = "help_carriage_store";
			tutorialTextLoc.Term = "tutorial_carriageStore";
			break;
		case "heroineFollow":
			headerTextLoc.Term = "help_map_heroineFollow";
			tutorialTextLoc.Term = "tutorial_heroineFollow";
			break;
		case "survey":
			headerTextLoc.Term = "help_sexTouch_title";
			tutorialTextLoc.Term = "tutorial_survey";
			break;
		case "sexBattle":
			headerTextLoc.Term = "help_sexBattle_title";
			tutorialTextLoc.Term = "tutorial_sexBattle";
			break;
		}
	}

	public void PushTutorialEndButton()
	{
		endButtonCanvasGroup.interactable = false;
		PlayerNonSaveDataManager.unLoadSceneName = "tutorialUI";
		switch (PlayerNonSaveDataManager.selectTutorialName)
		{
		case "scenarioBattle1":
			PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle1"] = true;
			GameObject.Find("Scenario Battle Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "scenarioBattle2":
			GameObject.Find("Battle Turn End").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "scenarioBattle3":
			PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle3"] = true;
			GameObject.Find("Scenario Battle Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "chargeAttack":
			PlayerNonSaveDataManager.isChargeAttackTutorial = false;
			PlayerFlagDataManager.tutorialFlagDictionary["chargeAttack"] = true;
			GameObject.Find("Scenario Battle Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "dungeonMap":
		case "dungeonBoss":
			PlayerFlagDataManager.tutorialFlagDictionary["dungeonMap"] = true;
			GameObject.Find("Dungeon Map Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "dungeonBattle":
			PlayerFlagDataManager.tutorialFlagDictionary["dungeonBattle"] = true;
			GameObject.Find("Dungeon Battle Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "craft":
			GameObject.Find("New Craft Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "carriageStore":
			PlayerNonSaveDataManager.isCarriageStoreTutorial = false;
			GameObject.Find("Carriage Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "heroineFollow":
			switch (PlayerDataManager.mapPlaceStatusNum)
			{
			case 0:
			case 1:
				GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("EndHeroineFollowTutorial");
				break;
			case 2:
				PlayerNonSaveDataManager.isInDoorExitLock = false;
				GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().inDoorCanvasGo.GetComponent<CanvasGroup>().interactable = true;
				GameObject.Find("InDoor Command Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
				break;
			}
			break;
		case "survey":
			GameObject.Find("Sex Touch Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		case "sexBattle":
			GameObject.Find("Sex Battle Manager").GetComponent<ArborFSM>().SendTrigger("EndTutorialUI");
			break;
		}
	}
}
