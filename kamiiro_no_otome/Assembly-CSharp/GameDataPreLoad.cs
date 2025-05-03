using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GameDataPreLoad : StateBehaviour
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
		PlayerOptionsDataManager.optionsBgmVolume = ES3.Load("optionsBgmVolume", 0.5f);
		PlayerOptionsDataManager.optionsHBgmVolume = ES3.Load("optionsHBgmVolume", 0.3f);
		PlayerOptionsDataManager.optionsSeVolume = ES3.Load("optionsSeVolume", 0.5f);
		PlayerOptionsDataManager.optionsAmbienceVolume = ES3.Load("optionsAmbienceVolume", 0.5f);
		PlayerOptionsDataManager.optionsTextSpeed = ES3.Load("optionsTextSpeed", 0.5f);
		PlayerOptionsDataManager.optionsAutoTextSpeed = ES3.Load("optionsAutoTextSpeed", 0.5f);
		PlayerOptionsDataManager.optionsMouseWheelSend = ES3.Load("optionsMouseWheelSend", defaultValue: true);
		PlayerOptionsDataManager.optionsMouseWheelBacklog = ES3.Load("optionsMouseWheelBacklog", defaultValue: true);
		PlayerOptionsDataManager.optionsMouseWheelPower = ES3.Load("optionsMouseWheelPower", 0.3f);
		PlayerOptionsDataManager.optionsVoiceStopTypeNext = ES3.Load("optionsVoiceStopTypeNext", defaultValue: false);
		PlayerOptionsDataManager.optionsVoiceStopTypeClick = ES3.Load("optionsVoiceStopTypeClick", defaultValue: true);
		PlayerOptionsDataManager.optionsFullScreenMode = ES3.Load("optionsFullScreenMode", defaultValue: false);
		PlayerOptionsDataManager.optionsWindowSize = ES3.Load("optionsDisplaySize", 3);
		PlayerDataManager.lastSaveSlotNum = ES3.Load<int>("lastSaveSlotNum");
		PlayerDataManager.lastSaveSlotPageNum = ES3.Load<int>("lastSaveSlotPageNum");
		PlayerFlagDataManager.sceneGarellyFlagDictionary = ES3.Load<Dictionary<string, bool>>("sceneGarellyFlagDictionary");
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
