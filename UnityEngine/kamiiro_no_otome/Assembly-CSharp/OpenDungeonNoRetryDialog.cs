using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenDungeonNoRetryDialog : StateBehaviour
{
	private DungeonDialogManager dungeonDialogManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonDialogManager = GameObject.Find("Dungeon Dialog Manager").GetComponent<DungeonDialogManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.openDialogName = "dungeonNoRetry";
		if (PlayerDataManager.currentDungeonName == "Shrine1")
		{
			Debug.Log("クワネロ50層ボスアラート");
			dungeonDialogManager.dungeonDialogTextGroup.SetActive(value: false);
			dungeonDialogManager.dungeonMenstrualDialogTextGroup.SetActive(value: false);
			dungeonDialogManager.dungeonNoRetryDialogTextGroup.SetActive(value: false);
			dungeonDialogManager.dungeonShrine1DeepBossDialogTextGroup.SetActive(value: true);
			if (PlayerFlagDataManager.scenarioFlagDictionary["MH_Levy_019"])
			{
				dungeonDialogManager.dungeonShrine1DeepBossDialogTextLoc.Term = "alertDungeonExtraBoss2";
			}
			else
			{
				dungeonDialogManager.dungeonShrine1DeepBossDialogTextLoc.Term = "alertDungeonExtraBoss_beforeLevy";
			}
		}
		else
		{
			Debug.Log("再戦不可ボスアラート");
			dungeonDialogManager.dungeonDialogTextGroup.SetActive(value: false);
			dungeonDialogManager.dungeonMenstrualDialogTextGroup.SetActive(value: false);
			dungeonDialogManager.dungeonNoRetryDialogTextGroup.SetActive(value: true);
			dungeonDialogManager.dungeonShrine1DeepBossDialogTextGroup.SetActive(value: false);
		}
		dungeonDialogManager.normalButtonGroup.SetActive(value: false);
		dungeonDialogManager.menstrualDayButtonGroup.SetActive(value: false);
		dungeonDialogManager.noRetryButtonGroup.SetActive(value: true);
		dungeonDialogManager.dungeonDialogCanvasGo.SetActive(value: true);
		dungeonDialogManager.dungeonDialogWindowGo.SetActive(value: true);
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
