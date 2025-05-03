using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckCarriageStoreTutorial : StateBehaviour
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
		PlayerNonSaveDataManager.selectTutorialName = "carriageStore";
		PlayerFlagDataManager.tutorialFlagDictionary["carriageStore"] = true;
		PlayerNonSaveDataManager.isCarriageStoreTutorial = true;
		Invoke("InvokeMethod", 0.3f);
		Debug.Log("商品販売のチュートリアル開始");
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
