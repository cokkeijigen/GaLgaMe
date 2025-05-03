using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetOptionsSliderValue : StateBehaviour
{
	public enum Type
	{
		bgm,
		hBgm,
		se,
		ambience,
		textSpeed,
		autoSpeed,
		wheelPower
	}

	public Type type;

	private Slider slider;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		slider = GetComponent<Slider>();
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.bgm:
			PlayerOptionsDataManager.optionsBgmVolume = (int)slider.value;
			break;
		case Type.hBgm:
			PlayerOptionsDataManager.optionsHBgmVolume = (int)slider.value;
			break;
		case Type.se:
			PlayerOptionsDataManager.optionsSeVolume = (int)slider.value;
			break;
		case Type.ambience:
			PlayerOptionsDataManager.optionsAmbienceVolume = (int)slider.value;
			break;
		case Type.textSpeed:
			PlayerOptionsDataManager.optionsTextSpeed = (int)slider.value;
			break;
		case Type.autoSpeed:
			PlayerOptionsDataManager.optionsAutoTextSpeed = (int)slider.value;
			break;
		case Type.wheelPower:
			PlayerOptionsDataManager.optionsMouseWheelPower = (int)slider.value;
			break;
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
