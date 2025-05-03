using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class JumpToAfterSurvay : StateBehaviour
{
	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.isSexHeroineEnableFertilization = false;
		PlayerNonSaveDataManager.isMenstrualDaySexUseCondom = false;
		PlayMakerFSM component = GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>();
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 2:
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			PlayerNonSaveDataManager.currentSceneName = "sex";
			PlayerNonSaveDataManager.isUtageToLocalMap = true;
			PlayerNonSaveDataManager.addTimeZoneNum = 1;
			PlayerNonSaveDataManager.isAddTimeFromScenario = true;
			GameObject.Find("AddTime Manager").GetComponent<PlayMakerFSM>().SendEvent("AddTimeZone");
			if (sexBattleFertilizationManager.isFertilizationSuccess)
			{
				PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentAccessPointName + "_SurveyFertilizeEnd";
			}
			else
			{
				PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentAccessPointName + "_SurveyEnd";
			}
			if (sexBattleTurnManager.sexBattlePlayerInShotCount > 0)
			{
				PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[PlayerDataManager.DungeonHeroineFollowNum] = PlayerSexStatusDataManager.remainingSemenDefaultValue;
			}
			break;
		case 3:
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			PlayerNonSaveDataManager.currentSceneName = "sex";
			PlayerNonSaveDataManager.addDungeonTimeZoneNum++;
			if (sexBattleFertilizationManager.isFertilizationSuccess)
			{
				PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentDungeonName + "_SurveyFertilizeEnd";
			}
			else
			{
				PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentDungeonName + "_SurveyEnd";
			}
			if (sexBattleTurnManager.sexBattlePlayerInShotCount > 0)
			{
				PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[PlayerDataManager.DungeonHeroineFollowNum] = PlayerSexStatusDataManager.remainingSemenDefaultValue + PlayerNonSaveDataManager.addDungeonTimeZoneNum;
			}
			break;
		}
		component.SendEvent("StartFadeIn");
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
