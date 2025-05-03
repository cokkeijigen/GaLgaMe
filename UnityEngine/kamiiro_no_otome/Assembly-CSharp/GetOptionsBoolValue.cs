using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class GetOptionsBoolValue : StateBehaviour
{
	public enum Type
	{
		sendMouse,
		stopNext,
		stopClick,
		backlogWheel,
		allVoiceDisable,
		lucyVoiceTypeSoft,
		lucyVoiceTypeSexy
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
		case Type.sendMouse:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.optionsMouseWheelSend;
			break;
		case Type.stopNext:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.optionsVoiceStopTypeNext;
			break;
		case Type.stopClick:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.optionsVoiceStopTypeClick;
			break;
		case Type.backlogWheel:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.optionsMouseWheelBacklog;
			break;
		case Type.allVoiceDisable:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.isAllVoiceDisable;
			break;
		case Type.lucyVoiceTypeSoft:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.isLucyVoiceTypeSoft;
			break;
		case Type.lucyVoiceTypeSexy:
			GetComponent<Toggle>().isOn = PlayerOptionsDataManager.isLucyVoiceTypeSexy;
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
}
