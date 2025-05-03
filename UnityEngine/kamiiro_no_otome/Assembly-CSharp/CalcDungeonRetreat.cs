using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonRetreat : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	private string result;

	public StateLink successLink;

	public StateLink failedLink;

	public StateLink retreatLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		string openDialogName = PlayerNonSaveDataManager.openDialogName;
		if (!(openDialogName == "dungeonMapRetreat"))
		{
			if (openDialogName == "dungeonBattleRetreat")
			{
				dungeonBattleManager.messageWindowGO.SetActive(value: true);
				dungeonBattleManager.messageTextArray[0].SetActive(value: true);
				dungeonBattleManager.messageWindowLoc[1].Term = "dungeonRetreatStart";
				float time = 0.6f / (float)PlayerDataManager.dungeonBattleSpeed;
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					dungeonBattleManager.messageWindowLoc[0].Term = "battleTextPlayerAllTarget";
				}
				else
				{
					dungeonBattleManager.messageWindowLoc[0].Term = "character0";
				}
				int num = PlayerDataManager.retreatProbability + dungeonMapStatusManager.dungeonBuffRetreat;
				int num2 = Random.Range(0, 100);
				Debug.Log("脱出確率：" + num + "／ランダム値：" + num2);
				if (num2 < num)
				{
					result = "battleSuccess";
					GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>().isRetreat = true;
					dungeonMapManager.GetItemToHaveData();
					Invoke("InvokeMethod", time);
				}
				else
				{
					result = "battleFailed";
					Invoke("InvokeMethod", time);
				}
			}
		}
		else
		{
			GameObject.Find("Dialog Canvas").SetActive(value: false);
			result = "mapRetreat";
			dungeonMapManager.GetItemToHaveData();
			InvokeMethod();
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

	private void InvokeMethod()
	{
		float time = 0.8f / (float)PlayerDataManager.dungeonBattleSpeed;
		switch (result)
		{
		case "mapRetreat":
			dungeonBattleManager.messageWindowGO.SetActive(value: true);
			dungeonBattleManager.messageTextArray[0].SetActive(value: true);
			dungeonBattleManager.messageWindowLoc[1].Term = "dungeonMapRetreat";
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				dungeonBattleManager.messageWindowLoc[0].Term = "battleTextPlayerAllTarget";
			}
			else
			{
				dungeonBattleManager.messageWindowLoc[0].Term = "character0";
			}
			Invoke("InvokeMethod2", time);
			break;
		case "battleSuccess":
			dungeonBattleManager.messageTextArray[0].SetActive(value: false);
			dungeonBattleManager.messageWindowLoc[1].Term = "dungeonRetreatSuccess";
			Invoke("InvokeMethod2", time);
			break;
		case "battleFailed":
			dungeonBattleManager.messageTextArray[0].SetActive(value: false);
			dungeonBattleManager.messageWindowLoc[1].Term = "dungeonRetreatFailed";
			Invoke("InvokeMethod2", time);
			break;
		}
	}

	private void InvokeMethod2()
	{
		dungeonBattleManager.messageWindowGO.SetActive(value: false);
		switch (result)
		{
		case "mapRetreat":
			PlayerDataManager.mapPlaceStatusNum = 0;
			GameObject.Find("Scenario Manager").GetComponent<ArborFSM>().SendTrigger("MoveMapSceneStart");
			Transition(successLink);
			break;
		case "battleSuccess":
			Transition(retreatLink);
			break;
		case "battleFailed":
			Transition(failedLink);
			break;
		}
	}
}
