using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckExtraCommandButtonVisible : StateBehaviour
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
		SexTouchData sexTouchData = GameDataManager.instance.sexTouchDataBase.sexTouchDataList.Find((SexTouchData data) => data.characterID == PlayerNonSaveDataManager.selectSexBattleHeroineId);
		if (!sexTouchManager.commandButtonArray[0].gameObject.activeInHierarchy && PlayerFlagDataManager.scenarioFlagDictionary[sexTouchData.enableSexFlag] && sexTouchStatusManager.heroineLibidoPoint >= 80f)
		{
			sexTouchManager.commandButtonArray[0].gameObject.SetActive(value: true);
		}
		if (!sexTouchManager.commandButtonArray[1].gameObject.activeInHierarchy && PlayerFlagDataManager.scenarioFlagDictionary[sexTouchData.enableCumShotFlag] && sexTouchStatusManager.heroineLibidoPoint >= 80f)
		{
			sexTouchManager.commandButtonArray[1].gameObject.SetActive(value: true);
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
