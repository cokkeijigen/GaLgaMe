using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckCraftTutorial : StateBehaviour
{
	private CraftManager craftManager;

	private CraftUiManager craftUiManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftUiManager = GameObject.Find("Craft Dialog Manager").GetComponent<CraftUiManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isTutorialCrafted)
		{
			PlayerFlagDataManager.tutorialFlagDictionary["craft"] = true;
			craftUiManager.exitButtonTextLoc.Term = "buttonBack";
			craftManager.exitButtonCanvasGroup.alpha = 1f;
			craftManager.exitButtonCanvasGroup.interactable = true;
			Transition(stateLink);
		}
		else if (PlayerNonSaveDataManager.isTutorialOpened)
		{
			craftUiManager.exitButtonTextLoc.Term = "buttonBack";
			craftManager.exitButtonCanvasGroup.alpha = 0.5f;
			craftManager.exitButtonCanvasGroup.interactable = false;
			Transition(stateLink);
		}
		else
		{
			PlayerNonSaveDataManager.isTutorialOpened = true;
			PlayerNonSaveDataManager.selectTutorialName = "craft";
			craftUiManager.exitButtonTextLoc.Term = "buttonBack";
			craftManager.exitButtonCanvasGroup.alpha = 0.5f;
			craftManager.exitButtonCanvasGroup.interactable = false;
			Invoke("InvokeMethod", 0.3f);
			Debug.Log("クラフトのチュートリアル開始");
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
	}
}
