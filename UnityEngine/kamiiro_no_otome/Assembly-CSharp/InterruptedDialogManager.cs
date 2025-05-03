using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

public class InterruptedDialogManager : MonoBehaviour
{
	public GameObject interruptedCanvasGo;

	public CanvasGroup interruptedCanvasGroup;

	public Localize interruptedDialogTextLoc;

	public GameObject saveButtonGroupGo_adventure;

	public GameObject saveButtonGroupGo_exploration;

	public GameObject quitButtonGroupGo;

	public Localize continueButtonTextLoc;

	public Localize quitButtonTextLoc;

	public void OpenInterruptedDialog()
	{
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "interruptedSave_adventure":
			interruptedDialogTextLoc.Term = "alertInterruptedSave_adventure";
			saveButtonGroupGo_adventure.SetActive(value: true);
			saveButtonGroupGo_exploration.SetActive(value: false);
			quitButtonGroupGo.SetActive(value: false);
			break;
		case "interruptedSave_exploration":
			interruptedDialogTextLoc.Term = "alertInterruptedSave_exploration";
			saveButtonGroupGo_adventure.SetActive(value: false);
			saveButtonGroupGo_exploration.SetActive(value: true);
			quitButtonGroupGo.SetActive(value: false);
			break;
		case "saveAfter":
		{
			string utageInterruptedDialogType = PlayerNonSaveDataManager.utageInterruptedDialogType;
			if (!(utageInterruptedDialogType == "interruptedSave_adventure"))
			{
				if (utageInterruptedDialogType == "interruptedSave_exploration")
				{
					interruptedDialogTextLoc.Term = "alertInterruptedSaveAfter_exploration";
					continueButtonTextLoc.Term = "buttonContinueExploration";
					quitButtonTextLoc.Term = "buttonRestAndQuit";
				}
			}
			else
			{
				interruptedDialogTextLoc.Term = "alertInterruptedSaveAfter_adventure";
				continueButtonTextLoc.Term = "buttonContinueAdventure";
				quitButtonTextLoc.Term = "buttonRestAndQuit";
			}
			saveButtonGroupGo_adventure.SetActive(value: false);
			saveButtonGroupGo_exploration.SetActive(value: false);
			quitButtonGroupGo.SetActive(value: true);
			break;
		}
		}
		MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
		interruptedCanvasGo.SetActive(value: true);
		Debug.Log("中断セーブのダイアログを開く");
	}

	public void PushInterruptedDialogOkButton()
	{
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "interruptedSave_adventure":
		case "interruptedSave_exploration":
			PlayerNonSaveDataManager.currentScenarioLabelName = GameObject.Find("AdvEngine").GetComponent<AdvEngine>().Page.CurrentData.ScenarioLabelData.ScenarioLabel;
			interruptedCanvasGo.SetActive(value: false);
			PlayerNonSaveDataManager.isInterruptedSave = true;
			PlayerNonSaveDataManager.selectSystemTabName = "save";
			SceneManager.LoadSceneAsync("systemUI", LoadSceneMode.Additive);
			break;
		case "saveAfter":
			Application.Quit();
			break;
		}
	}

	public void PushInterruptedDialogCancelButton()
	{
		PlayerNonSaveDataManager.isInterruptedSave = false;
		switch (PlayerNonSaveDataManager.openDialogName)
		{
		case "interruptedSave_adventure":
		case "interruptedSave_exploration":
			interruptedCanvasGo.SetActive(value: false);
			GameObject.Find("AdvEngine").GetComponent<AdvEngine>().ResumeScenario();
			break;
		case "saveAfter":
			interruptedCanvasGo.SetActive(value: false);
			GameObject.Find("AdvEngine").GetComponent<AdvEngine>().ResumeScenario();
			break;
		}
	}
}
