using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetOptionsInitialize : StateBehaviour
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
		PlayerOptionsDataManager.optionsBgmVolume = PlayerOptionsDataManager.defaultBgmVolume;
		PlayerOptionsDataManager.optionsHBgmVolume = PlayerOptionsDataManager.defaultHBgmVolume;
		PlayerOptionsDataManager.optionsSeVolume = PlayerOptionsDataManager.defaultSeVolume;
		PlayerOptionsDataManager.optionsAmbienceVolume = PlayerOptionsDataManager.defaultAmbienceVolume;
		PlayerOptionsDataManager.optionsVoice1Volume = PlayerOptionsDataManager.defaultVoice1Volume;
		PlayerOptionsDataManager.optionsVoice2Volume = PlayerOptionsDataManager.defaultVoice2Volume;
		PlayerOptionsDataManager.optionsVoice3Volume = PlayerOptionsDataManager.defaultVoice3Volume;
		PlayerOptionsDataManager.optionsVoice4Volume = PlayerOptionsDataManager.defaultVoice4Volume;
		PlayerOptionsDataManager.optionsVoice5Volume = PlayerOptionsDataManager.defaultVoice5Volume;
		PlayerOptionsDataManager.optionsSubVoice1Volume = PlayerOptionsDataManager.defaultSubVoice1Volume;
		PlayerOptionsDataManager.optionsSubVoice2Volume = PlayerOptionsDataManager.defaultSubVoice2Volume;
		PlayerOptionsDataManager.optionsSubVoice3Volume = PlayerOptionsDataManager.defaultSubVoice3Volume;
		PlayerOptionsDataManager.optionsSubVoice4Volume = PlayerOptionsDataManager.defaultSubVoice4Volume;
		PlayerOptionsDataManager.optionsSubVoice5Volume = PlayerOptionsDataManager.defaultSubVoice5Volume;
		PlayerOptionsDataManager.optionsSubVoice6Volume = PlayerOptionsDataManager.defaultSubVoice6Volume;
		PlayerOptionsDataManager.optionsSubVoice7Volume = PlayerOptionsDataManager.defaultSubVoice7Volume;
		PlayerOptionsDataManager.optionsSubVoice8Volume = PlayerOptionsDataManager.defaultSubVoice8Volume;
		PlayerOptionsDataManager.optionsMobVoice1Volume = PlayerOptionsDataManager.defaultMobVoice1Volume;
		PlayerOptionsDataManager.optionsMobVoice2Volume = PlayerOptionsDataManager.defaultMobVoice2Volume;
		PlayerOptionsDataManager.optionsMobVoice3Volume = PlayerOptionsDataManager.defaultMobVoice3Volume;
		PlayerOptionsDataManager.optionsMobVoice4Volume = PlayerOptionsDataManager.defaultMobVoice4Volume;
		PlayerOptionsDataManager.optionsMobVoice5Volume = PlayerOptionsDataManager.defaultMobVoice5Volume;
		PlayerOptionsDataManager.optionsMobVoice6Volume = PlayerOptionsDataManager.defaultMobVoice6Volume;
		PlayerOptionsDataManager.optionsMobVoice7Volume = PlayerOptionsDataManager.defaultMobVoice7Volume;
		PlayerOptionsDataManager.optionsMobVoice8Volume = PlayerOptionsDataManager.defaultMobVoice8Volume;
		PlayerOptionsDataManager.optionsTextSpeed = PlayerOptionsDataManager.defaultTextSpeed;
		PlayerOptionsDataManager.optionsAutoTextSpeed = PlayerOptionsDataManager.defaultAutoTextSpeed;
		PlayerOptionsDataManager.optionsMouseWheelSend = PlayerOptionsDataManager.defaultMouseWheelSend;
		PlayerOptionsDataManager.optionsMouseWheelBacklog = PlayerOptionsDataManager.defaultMouseWheelBacklog;
		PlayerOptionsDataManager.optionsMouseWheelPower = PlayerOptionsDataManager.defaultMouseWheelPower;
		PlayerOptionsDataManager.optionsVoiceStopTypeNext = PlayerOptionsDataManager.defaultVoiceStopTypeNext;
		PlayerOptionsDataManager.optionsVoiceStopTypeClick = PlayerOptionsDataManager.defaultVoiceStopTypeClick;
		PlayerOptionsDataManager.optionsFullScreenMode = PlayerOptionsDataManager.defaultFullScreenMode;
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
