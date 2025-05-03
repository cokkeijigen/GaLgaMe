using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenMapBrunch : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	public StateLink worldMap;

	public StateLink localMap;

	public StateLink inDoor;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		worldMapAccessManager.SetWorldMapCanvasInteractable(value: false);
		PlayerDataManager.worldMapInputBlock = true;
		totalMapAccessManager.SetLocalMapCanvasInteractable(value: false);
		PlayerNonSaveDataManager.currentSceneName = "main";
		PlayerDataManager.isSelectDungeon = false;
		Debug.Log("マップ番号分岐：" + PlayerDataManager.mapPlaceStatusNum);
		if (PlayerNonSaveDataManager.isRetreatScenarioFlagReset)
		{
			string text = PlayerNonSaveDataManager.retreatResetFlagNameArray[0];
			string key = PlayerNonSaveDataManager.retreatResetFlagNameArray[1];
			if (!PlayerFlagDataManager.scenarioFlagDictionary[key])
			{
				PlayerFlagDataManager.scenarioFlagDictionary[text] = false;
				Debug.Log("前段シナリオをリセット：" + text);
			}
		}
		PlayerNonSaveDataManager.isRetreatScenarioFlagReset = false;
		Debug.Log("フラグのリセット用変数：" + PlayerNonSaveDataManager.isRetreatScenarioFlagReset);
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
			Debug.Log("マップ分岐：ワールドマップ");
			Transition(worldMap);
			break;
		case 1:
			Debug.Log("マップ分岐：ローカルマップ");
			Transition(localMap);
			break;
		case 2:
			Debug.Log("マップ分岐：インドア");
			Transition(inDoor);
			break;
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
}
