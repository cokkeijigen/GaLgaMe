using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class OpenSexBattleTutorial : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.selectTutorialName = "sexBattle";
		PlayerFlagDataManager.tutorialFlagDictionary["sexBattle"] = true;
		Invoke("InvokeMethod", 0.3f);
		Debug.Log("身体検査のチュートリアル開始");
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
