using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckDungeonMapTutorial : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	public StateLink noTutorialLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		if (dungeonMapManager.dungeonBattleCanvas.activeInHierarchy)
		{
			PlayerNonSaveDataManager.selectTutorialName = "dungeonBattle";
			PlayerFlagDataManager.tutorialFlagDictionary["dungeonBattle"] = true;
			Debug.Log("ダンジョンバトルのチュートリアル開始");
		}
		else if (dungeonMapManager.dungeonCurrentFloorNum == dungeonMapManager.dungeonMaxFloorNum)
		{
			PlayerNonSaveDataManager.selectTutorialName = "dungeonBoss";
			PlayerFlagDataManager.tutorialFlagDictionary["dungeonBoss"] = true;
			Debug.Log("ダンジョンボスのチュートリアル開始");
		}
		else if (PlayerFlagDataManager.tutorialFlagDictionary["dungeonMap"])
		{
			flag = true;
		}
		else
		{
			PlayerNonSaveDataManager.selectTutorialName = "dungeonMap";
			PlayerFlagDataManager.tutorialFlagDictionary["dungeonMap"] = true;
			Debug.Log("ダンジョンマップのチュートリアル開始");
		}
		if (flag)
		{
			Transition(noTutorialLink);
		}
		else
		{
			Invoke("InvokeMethod", 0.3f);
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
		SceneManager.LoadSceneAsync("tutorialUI", LoadSceneMode.Additive);
		Transition(stateLink);
	}
}
