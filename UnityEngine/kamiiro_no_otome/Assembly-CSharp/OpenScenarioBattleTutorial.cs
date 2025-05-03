using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class OpenScenarioBattleTutorial : StateBehaviour
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
		PlayerNonSaveDataManager.selectTutorialName = "scenarioBattle2";
		PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle2"] = true;
		Invoke("InvokeMethod", 0.3f);
		Debug.Log("コマンド戦闘２ターン目のチュートリアル開始");
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
