using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckLucyVoiceSelect : StateBehaviour
{
	private ScenarioBattleVoiceManager scenarioBattleVoiceManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleVoiceManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleVoiceManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-2")
		{
			Debug.Log("リザルトシナリオ名：" + PlayerNonSaveDataManager.resultScenarioName);
			if (PlayerOptionsDataManager.isAllVoiceDisable)
			{
				Debug.Log("全音声オフになっている");
				PlayerFlagDataManager.tutorialFlagDictionary["lucyVoiceSelect"] = true;
				Transition(stateLink);
			}
			else if (!PlayerFlagDataManager.tutorialFlagDictionary["lucyVoiceSelect"])
			{
				MasterAudio.PlaySound("SeDialogOpen", 1f, null, 0f, null, null);
				scenarioBattleVoiceManager.battleDialogCanvasGo.SetActive(value: true);
			}
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
}
