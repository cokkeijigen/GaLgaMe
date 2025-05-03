using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckHeroineFollowTutorial : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		if (!PlayerFlagDataManager.tutorialFlagDictionary["heroineFollow"])
		{
			inDoorTalkManager.inDoorCanvasGo.GetComponent<CanvasGroup>().interactable = false;
			PlayerNonSaveDataManager.isInDoorExitLock = true;
			PlayerNonSaveDataManager.selectTutorialName = "heroineFollow";
			PlayerFlagDataManager.tutorialFlagDictionary["heroineFollow"] = true;
			Invoke("InvokeMethod", 0.3f);
			Debug.Log("ヒロイン同行のチュートリアル開始");
		}
		else
		{
			Transition(stateLink);
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
