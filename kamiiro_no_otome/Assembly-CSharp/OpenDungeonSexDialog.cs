using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenDungeonSexDialog : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonDialogManager dungeonDialogManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonDialogManager = GameObject.Find("Dungeon Dialog Manager").GetComponent<DungeonDialogManager>();
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		string name = PlayerDataManager.currentDungeonName;
		if (GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == name).dungeonEventFloorList.Any((int data) => data == dungeonMapManager.dungeonCurrentFloorNum))
		{
			string text = PlayerDungeonScenarioDataManager.CheckDungeonFloorEvent(name, dungeonMapManager.dungeonCurrentFloorNum);
			if (!string.IsNullOrEmpty(text) && dungeonMapManager.dungeonCurrentFloorNum == dungeonMapManager.dungeonMaxFloorNum)
			{
				flag = true;
				PlayerNonSaveDataManager.dungeonEventScenarioName = text;
				PlayerNonSaveDataManager.openDialogName = "dungeonSexEvent";
				dungeonDialogManager.dungeonDialogTextLocArray[0].Term = "alertDungeonSexEventStart";
				dungeonDialogManager.dungeonDialogTextLocArray[1].Term = "alertAfterDungeonSexEvent";
			}
		}
		if (PlayerDataManager.playerLibido >= 100)
		{
			string text2 = PlayerDungeonScenarioDataManager.CheckDungeonSexEvent(name, dungeonMapManager.dungeonCurrentFloorNum);
			if (!string.IsNullOrEmpty(text2))
			{
				flag = true;
				PlayerNonSaveDataManager.dungeonEventScenarioName = text2;
				PlayerNonSaveDataManager.openDialogName = "dungeonSexEvent";
				dungeonDialogManager.dungeonDialogTextLocArray[0].Term = "alertDungeonSexEventStart";
				dungeonDialogManager.dungeonDialogTextLocArray[1].Term = "alertAfterDungeonSexEvent";
			}
		}
		if (!flag)
		{
			PlayerNonSaveDataManager.openDialogName = "dungeonSexBattle";
			dungeonDialogManager.dungeonDialogTextLocArray[0].Term = "alertDungeonSexBattleStart";
			dungeonDialogManager.dungeonDialogTextLocArray[1].Term = "alertAfterDungeonSexBattle";
		}
		dungeonDialogManager.dungeonDialogTextGroup.SetActive(value: true);
		dungeonDialogManager.dungeonMenstrualDialogTextGroup.SetActive(value: false);
		dungeonDialogManager.dungeonNoRetryDialogTextGroup.SetActive(value: false);
		dungeonDialogManager.dungeonShrine1DeepBossDialogTextGroup.SetActive(value: false);
		dungeonDialogManager.normalButtonGroup.SetActive(value: true);
		dungeonDialogManager.menstrualDayButtonGroup.SetActive(value: false);
		dungeonDialogManager.noRetryButtonGroup.SetActive(value: false);
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
