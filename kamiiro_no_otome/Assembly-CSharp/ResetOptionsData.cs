using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ResetOptionsData : StateBehaviour
{
	public enum Type
	{
		bgmVolume = 0,
		hBgmVolume = 1,
		seVolume = 2,
		ambienceVolume = 3,
		voiceVolume = 11,
		allVoiceDisable = 12,
		lucyVoiceTypeSoft = 13,
		lucyVoiceTypeSexy = 14,
		textSpeed = 4,
		autoTextSpeed = 5,
		mouseWheelSend = 6,
		mouseWheelPower = 7,
		voiceStopTypeNext = 8,
		voiceStopTypeClick = 9,
		mouseWheelBacklog = 10
	}

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.bgmVolume:
			GetComponent<Slider>().value = PlayerOptionsDataManager.defaultBgmVolume * 10f;
			break;
		case Type.hBgmVolume:
			GetComponent<Slider>().value = PlayerOptionsDataManager.defaultHBgmVolume * 10f;
			break;
		case Type.seVolume:
			GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSeVolume * 10f;
			break;
		case Type.ambienceVolume:
			GetComponent<Slider>().value = PlayerOptionsDataManager.defaultAmbienceVolume * 10f;
			break;
		case Type.voiceVolume:
			ResetVoiceVolume();
			break;
		case Type.allVoiceDisable:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.defaultAllVoiceDisable;
			break;
		case Type.lucyVoiceTypeSoft:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.defaultLucyVoiceTypeSoft;
			break;
		case Type.lucyVoiceTypeSexy:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.defaultLucyVoiceTypeSexy;
			break;
		case Type.textSpeed:
			GetComponent<Slider>().value = PlayerOptionsDataManager.defaultTextSpeed * 10f;
			break;
		case Type.autoTextSpeed:
			GetComponent<Slider>().value = PlayerOptionsDataManager.defaultAutoTextSpeed * 10f;
			break;
		case Type.mouseWheelSend:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.defaultMouseWheelSend;
			break;
		case Type.mouseWheelPower:
			GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMouseWheelPower * 10f;
			break;
		case Type.voiceStopTypeNext:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.defaultVoiceStopTypeNext;
			break;
		case Type.voiceStopTypeClick:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.defaultVoiceStopTypeClick;
			break;
		case Type.mouseWheelBacklog:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.defaultMouseWheelBacklog;
			break;
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

	private void ResetVoiceVolume()
	{
		string @string = GetComponent<ParameterContainer>().GetString("voiceName");
		string text = @string.Substring(0, @string.Length - 1);
		int num = int.Parse(@string.Substring(@string.Length - 1));
		Debug.Log("リセット／グループ名：" + text + "／番号：" + num);
		switch (text)
		{
		case "mainVoice":
			switch (num)
			{
			case 1:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultVoice1Volume * 10f;
				break;
			case 2:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultVoice2Volume * 10f;
				break;
			case 3:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultVoice3Volume * 10f;
				break;
			case 4:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultVoice4Volume * 10f;
				break;
			case 5:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultVoice5Volume * 10f;
				break;
			}
			break;
		case "subVoice":
			switch (num)
			{
			case 1:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice1Volume * 10f;
				break;
			case 2:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice2Volume * 10f;
				break;
			case 3:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice3Volume * 10f;
				break;
			case 4:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice4Volume * 10f;
				break;
			case 5:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice5Volume * 10f;
				break;
			case 6:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice6Volume * 10f;
				break;
			case 7:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice7Volume * 10f;
				break;
			case 8:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultSubVoice8Volume * 10f;
				break;
			}
			break;
		case "mobVoice":
			switch (num)
			{
			case 1:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice1Volume * 10f;
				break;
			case 2:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice2Volume * 10f;
				break;
			case 3:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice3Volume * 10f;
				break;
			case 4:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice4Volume * 10f;
				break;
			case 5:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice5Volume * 10f;
				break;
			case 6:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice6Volume * 10f;
				break;
			case 7:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice7Volume * 10f;
				break;
			case 8:
				GetComponent<Slider>().value = PlayerOptionsDataManager.defaultMobVoice8Volume * 10f;
				break;
			}
			break;
		}
	}
}
