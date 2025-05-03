using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class ApplyOptionsSetting : StateBehaviour
{
	public StateLink stateLink;

	public ParameterContainer parameterContainer;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		PlayerOptionsDataManager.optionsBgmVolume = parameterContainer.GetFloat("preBgmVolume") / 10f;
		PlayerOptionsDataManager.optionsHBgmVolume = parameterContainer.GetFloat("preHBgmVolume") / 10f;
		PlayerOptionsDataManager.optionsSeVolume = parameterContainer.GetFloat("preSeVolume") / 10f;
		PlayerOptionsDataManager.optionsAmbienceVolume = parameterContainer.GetFloat("preAmbienceVolume") / 10f;
		GameObject.Find("PlaylistController").GetComponent<PlaylistController>().PlaylistVolume = 1f;
		PlayerOptionsDataManager.optionsVoice1Volume = parameterContainer.GetFloat("preVoiceVolume1") / 10f;
		PlayerOptionsDataManager.optionsVoice2Volume = parameterContainer.GetFloat("preVoiceVolume2") / 10f;
		PlayerOptionsDataManager.optionsVoice3Volume = parameterContainer.GetFloat("preVoiceVolume3") / 10f;
		PlayerOptionsDataManager.optionsVoice4Volume = parameterContainer.GetFloat("preVoiceVolume4") / 10f;
		PlayerOptionsDataManager.optionsVoice5Volume = parameterContainer.GetFloat("preVoiceVolume5") / 10f;
		PlayerOptionsDataManager.optionsSubVoice1Volume = parameterContainer.GetFloat("preSubVoiceVolume1") / 10f;
		PlayerOptionsDataManager.optionsSubVoice2Volume = parameterContainer.GetFloat("preSubVoiceVolume2") / 10f;
		PlayerOptionsDataManager.optionsSubVoice3Volume = parameterContainer.GetFloat("preSubVoiceVolume3") / 10f;
		PlayerOptionsDataManager.optionsSubVoice4Volume = parameterContainer.GetFloat("preSubVoiceVolume4") / 10f;
		PlayerOptionsDataManager.optionsSubVoice5Volume = parameterContainer.GetFloat("preSubVoiceVolume5") / 10f;
		PlayerOptionsDataManager.optionsSubVoice6Volume = parameterContainer.GetFloat("preSubVoiceVolume6") / 10f;
		PlayerOptionsDataManager.optionsSubVoice7Volume = parameterContainer.GetFloat("preSubVoiceVolume7") / 10f;
		PlayerOptionsDataManager.optionsSubVoice8Volume = parameterContainer.GetFloat("preSubVoiceVolume8") / 10f;
		PlayerOptionsDataManager.optionsMobVoice1Volume = parameterContainer.GetFloat("preMobVoiceVolume1") / 10f;
		PlayerOptionsDataManager.optionsMobVoice2Volume = parameterContainer.GetFloat("preMobVoiceVolume2") / 10f;
		PlayerOptionsDataManager.optionsMobVoice3Volume = parameterContainer.GetFloat("preMobVoiceVolume3") / 10f;
		PlayerOptionsDataManager.optionsMobVoice4Volume = parameterContainer.GetFloat("preMobVoiceVolume4") / 10f;
		PlayerOptionsDataManager.optionsMobVoice5Volume = parameterContainer.GetFloat("preMobVoiceVolume5") / 10f;
		PlayerOptionsDataManager.optionsMobVoice6Volume = parameterContainer.GetFloat("preMobVoiceVolume6") / 10f;
		PlayerOptionsDataManager.optionsMobVoice7Volume = parameterContainer.GetFloat("preMobVoiceVolume7") / 10f;
		PlayerOptionsDataManager.optionsMobVoice8Volume = parameterContainer.GetFloat("preMobVoiceVolume8") / 10f;
		PlayerOptionsDataManager.isAllVoiceDisable = parameterContainer.GetBool("preAllVoiceDisable");
		PlayerOptionsDataManager.isLucyVoiceTypeSoft = parameterContainer.GetBool("preLucyVoiceTypeSoft");
		PlayerOptionsDataManager.isLucyVoiceTypeSexy = parameterContainer.GetBool("preLucyVoiceTypeSexy");
		PlayerOptionsDataManager.optionsTextSpeed = parameterContainer.GetFloat("preTextSpeed") / 10f;
		PlayerOptionsDataManager.optionsAutoTextSpeed = parameterContainer.GetFloat("preAutoTextSpeed") / 10f;
		PlayerOptionsDataManager.optionsMouseWheelSend = parameterContainer.GetBool("preMouseWheelSend");
		PlayerOptionsDataManager.defaultMouseWheelBacklog = parameterContainer.GetBool("preMouseWheelBacklog");
		PlayerOptionsDataManager.optionsMouseWheelPower = parameterContainer.GetFloat("preMouseWheelPower") / 10f;
		PlayerOptionsDataManager.optionsVoiceStopTypeNext = parameterContainer.GetBool("preVoiceStopTypeNext");
		PlayerOptionsDataManager.optionsVoiceStopTypeClick = parameterContainer.GetBool("preVoiceStopTypeClick");
		PlayerOptionsDataManager.optionsWindowSize = parameterContainer.GetInt("preWindowSize");
		PlayerOptionsDataManager.optionsFullScreenMode = parameterContainer.GetBool("preFullScreenMode");
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
