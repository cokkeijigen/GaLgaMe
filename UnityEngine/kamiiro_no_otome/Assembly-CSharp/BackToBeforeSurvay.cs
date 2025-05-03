using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class BackToBeforeSurvay : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
	}

	public override void OnStateBegin()
	{
		PlayerDataManager.playerLibido = sexTouchStatusManager.beforePlayerLibido;
		PlayerNonSaveDataManager.isSexHeroineEnableFertilization = false;
		PlayerNonSaveDataManager.isMenstrualDaySexUseCondom = false;
		PlayMakerFSM component = GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>();
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 2:
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			PlayerNonSaveDataManager.currentSceneName = "sex";
			if (sexTouchManager.battleCanvas.activeInHierarchy)
			{
				PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentAccessPointName + "_SexBattleBackEnd";
			}
			else
			{
				PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentAccessPointName + "_SurveyBackEnd";
			}
			break;
		case 3:
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			PlayerNonSaveDataManager.currentSceneName = "sex";
			PlayerNonSaveDataManager.selectScenarioName = PlayerDataManager.currentDungeonName + "_SexBattleBackEnd";
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
