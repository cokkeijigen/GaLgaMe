using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class GetOptionsFloatValue : StateBehaviour
{
	public enum Type
	{
		bgmVolume = 0,
		hBgmVolume = 1,
		seVolume = 2,
		ambienceVolume = 3,
		voiceVolume = 7,
		textSpeed = 4,
		autoTextSpeed = 5,
		mouseWheelPower = 6
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
			GetComponent<Slider>().value = PlayerOptionsDataManager.optionsBgmVolume * 10f;
			break;
		case Type.hBgmVolume:
			GetComponent<Slider>().value = PlayerOptionsDataManager.optionsHBgmVolume * 10f;
			break;
		case Type.seVolume:
			GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSeVolume * 10f;
			break;
		case Type.ambienceVolume:
			GetComponent<Slider>().value = PlayerOptionsDataManager.optionsAmbienceVolume * 10f;
			break;
		case Type.voiceVolume:
			GetVoiceVolume();
			break;
		case Type.textSpeed:
			GetComponent<Slider>().value = PlayerOptionsDataManager.optionsTextSpeed * 10f;
			break;
		case Type.autoTextSpeed:
			GetComponent<Slider>().value = PlayerOptionsDataManager.optionsAutoTextSpeed * 10f;
			break;
		case Type.mouseWheelPower:
			GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMouseWheelPower * 10f;
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

	private void GetVoiceVolume()
	{
		string @string = GetComponent<ParameterContainer>().GetString("voiceName");
		string text = @string.Substring(0, @string.Length - 1);
		int num = int.Parse(@string.Substring(@string.Length - 1));
		Debug.Log("グループ名：" + text + "／番号：" + num);
		switch (text)
		{
		case "mainVoice":
			switch (num)
			{
			case 1:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsVoice1Volume * 10f;
				break;
			case 2:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsVoice2Volume * 10f;
				break;
			case 3:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsVoice3Volume * 10f;
				break;
			case 4:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsVoice4Volume * 10f;
				break;
			case 5:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsVoice5Volume * 10f;
				break;
			}
			break;
		case "subVoice":
			switch (num)
			{
			case 1:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice1Volume * 10f;
				break;
			case 2:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice2Volume * 10f;
				break;
			case 3:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice3Volume * 10f;
				break;
			case 4:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice4Volume * 10f;
				break;
			case 5:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice5Volume * 10f;
				break;
			case 6:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice6Volume * 10f;
				break;
			case 7:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice7Volume * 10f;
				break;
			case 8:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsSubVoice8Volume * 10f;
				break;
			}
			break;
		case "mobVoice":
			switch (num)
			{
			case 1:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice1Volume * 10f;
				break;
			case 2:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice2Volume * 10f;
				break;
			case 3:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice3Volume * 10f;
				break;
			case 4:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice4Volume * 10f;
				break;
			case 5:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice5Volume * 10f;
				break;
			case 6:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice6Volume * 10f;
				break;
			case 7:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice7Volume * 10f;
				break;
			case 8:
				GetComponent<Slider>().value = PlayerOptionsDataManager.optionsMobVoice8Volume * 10f;
				break;
			}
			break;
		}
	}
}
