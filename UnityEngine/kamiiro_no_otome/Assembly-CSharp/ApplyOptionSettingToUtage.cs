using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class ApplyOptionSettingToUtage : StateBehaviour
{
	private GameObject advEngine;

	public AdvConfig advConfig;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		advEngine = GameObject.Find("AdvEngine");
		advConfig = advEngine.GetComponent<AdvConfig>();
		if (PlayerNonSaveDataManager.loadSceneName == "scenario")
		{
			advEngine.GetComponent<AdvEngine>().Param.SetParameterBoolean("isUtageToJumpFromGarelly", PlayerNonSaveDataManager.isUtageToJumpFromGarelly);
		}
		if (PlayerNonSaveDataManager.isUtageHmode)
		{
			advConfig.BgmVolume = PlayerOptionsDataManager.optionsHBgmVolume;
		}
		else
		{
			advConfig.BgmVolume = PlayerOptionsDataManager.optionsBgmVolume;
		}
		advConfig.SeVolume = PlayerOptionsDataManager.optionsSeVolume;
		advConfig.AmbienceVolume = PlayerOptionsDataManager.optionsAmbienceVolume;
		if (PlayerOptionsDataManager.isAllVoiceDisable)
		{
			advConfig.SetTaggedMasterVolume("ルーシー", 0f);
			advConfig.SetTaggedMasterVolume("リィナ", 0f);
			advConfig.SetTaggedMasterVolume("シア", 0f);
			advConfig.SetTaggedMasterVolume("レヴィ", 0f);
			advConfig.SetTaggedMasterVolume("シャーロ", 0f);
			advConfig.SetTaggedMasterVolume("ドライアド", 0f);
			advConfig.SetTaggedMasterVolume("ラムネ", 0f);
			advConfig.SetTaggedMasterVolume("ニンフ", 0f);
			advConfig.SetTaggedMasterVolume("ショタエデン", 0f);
			advConfig.SetTaggedMasterVolume("ナチリ", 0f);
			advConfig.SetTaggedMasterVolume("パミルトン", 0f);
			advConfig.SetTaggedMasterVolume("フルリラ", 0f);
			advConfig.SetTaggedMasterVolume("三姉妹", 0f);
			advConfig.SetTaggedMasterVolume("ミユリ", 0f);
			advConfig.SetTaggedMasterVolume("フィル", 0f);
			advConfig.SetTaggedMasterVolume("リーゼ", 0f);
			advConfig.SetTaggedMasterVolume("カミラ", 0f);
			advConfig.SetTaggedMasterVolume("ソフィア", 0f);
			advConfig.SetTaggedMasterVolume("アリア", 0f);
			advConfig.SetTaggedMasterVolume("ノーラ", 0f);
		}
		else
		{
			if (PlayerOptionsDataManager.isLucyVoiceTypeSexy)
			{
				advConfig.SetTaggedMasterVolume("ルーシー", PlayerOptionsDataManager.optionsVoice1Volume * 0.65f);
			}
			else
			{
				advConfig.SetTaggedMasterVolume("ルーシー", PlayerOptionsDataManager.optionsVoice1Volume * 1.3f);
			}
			advConfig.SetTaggedMasterVolume("リィナ", PlayerOptionsDataManager.optionsVoice2Volume * 0.65f);
			advConfig.SetTaggedMasterVolume("シア", PlayerOptionsDataManager.optionsVoice3Volume * 1.45f);
			advConfig.SetTaggedMasterVolume("レヴィ", PlayerOptionsDataManager.optionsVoice4Volume * 0.85f);
			advConfig.SetTaggedMasterVolume("シャーロ", PlayerOptionsDataManager.optionsVoice5Volume * 1.15f);
			advConfig.SetTaggedMasterVolume("ドライアド", PlayerOptionsDataManager.optionsSubVoice1Volume);
			advConfig.SetTaggedMasterVolume("ラムネ", PlayerOptionsDataManager.optionsSubVoice2Volume * 0.85f);
			advConfig.SetTaggedMasterVolume("ニンフ", PlayerOptionsDataManager.optionsSubVoice3Volume * 0.65f);
			advConfig.SetTaggedMasterVolume("ショタエデン", PlayerOptionsDataManager.optionsSubVoice4Volume * 0.85f);
			advConfig.SetTaggedMasterVolume("ナチリ", PlayerOptionsDataManager.optionsSubVoice5Volume * 1.15f);
			advConfig.SetTaggedMasterVolume("パミルトン", PlayerOptionsDataManager.optionsSubVoice6Volume * 0.85f);
			advConfig.SetTaggedMasterVolume("フルリラ", PlayerOptionsDataManager.optionsSubVoice7Volume);
			advConfig.SetTaggedMasterVolume("三姉妹", PlayerOptionsDataManager.optionsSubVoice8Volume);
			advConfig.SetTaggedMasterVolume("ミユリ", PlayerOptionsDataManager.optionsMobVoice1Volume);
			advConfig.SetTaggedMasterVolume("フィル", PlayerOptionsDataManager.optionsMobVoice2Volume);
			advConfig.SetTaggedMasterVolume("リーゼ", PlayerOptionsDataManager.optionsMobVoice3Volume * 1.15f);
			advConfig.SetTaggedMasterVolume("カミラ", PlayerOptionsDataManager.optionsMobVoice4Volume * 0.5f);
			advConfig.SetTaggedMasterVolume("ソフィア", PlayerOptionsDataManager.optionsMobVoice5Volume * 0.65f);
			advConfig.SetTaggedMasterVolume("ノア", PlayerOptionsDataManager.optionsMobVoice6Volume * 0.85f);
			advConfig.SetTaggedMasterVolume("アリア", PlayerOptionsDataManager.optionsMobVoice7Volume * 0.65f);
			advConfig.SetTaggedMasterVolume("ノーラ", PlayerOptionsDataManager.optionsMobVoice8Volume);
		}
		advConfig.MessageSpeed = PlayerOptionsDataManager.optionsTextSpeed;
		advConfig.AutoBrPageSpeed = PlayerOptionsDataManager.optionsAutoTextSpeed * 1.8f;
		Debug.Log("宴オート改ページ速度：" + advConfig.AutoBrPageSpeed);
		advConfig.MessageWindowTransparency = 0.35f;
		advConfig.IsMouseWheelSendMessage = PlayerOptionsDataManager.optionsMouseWheelSend;
		advEngine.transform.Find("UI/BackLog").GetComponent<AdvUguiBacklogManager>().isCloseScrollWheelDown = PlayerOptionsDataManager.optionsMouseWheelBacklog;
		advEngine.transform.Find("UI").GetComponent<AdvUguiManager>().DisableMouseWheelBackLog = !PlayerOptionsDataManager.optionsMouseWheelBacklog;
		if (PlayerOptionsDataManager.optionsVoiceStopTypeNext)
		{
			advConfig.VoiceStopType = VoiceStopType.OnNextVoice;
		}
		else
		{
			advConfig.VoiceStopType = VoiceStopType.OnClick;
		}
		Transition(stateLink);
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
