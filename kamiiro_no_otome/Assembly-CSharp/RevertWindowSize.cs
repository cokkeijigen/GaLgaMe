using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RevertWindowSize : StateBehaviour
{
	public ParameterContainer optionParam;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		bool optionsFullScreenMode = PlayerOptionsDataManager.optionsFullScreenMode;
		int @int = optionParam.GetInt("preWindowSize");
		bool @bool = optionParam.GetBool("preFullScreenMode");
		if (@int != PlayerOptionsDataManager.optionsWindowSize && @bool != optionsFullScreenMode)
		{
			switch (PlayerOptionsDataManager.optionsWindowSize)
			{
			case 0:
				Screen.SetResolution(1024, 576, optionsFullScreenMode);
				break;
			case 1:
				Screen.SetResolution(1280, 720, optionsFullScreenMode);
				break;
			case 2:
				Screen.SetResolution(1440, 810, optionsFullScreenMode);
				break;
			case 3:
				Screen.SetResolution(1600, 900, optionsFullScreenMode);
				break;
			case 4:
				Screen.SetResolution(1920, 1080, optionsFullScreenMode);
				break;
			case 5:
				Screen.SetResolution(2240, 1260, optionsFullScreenMode);
				break;
			case 6:
				Screen.SetResolution(2560, 1440, optionsFullScreenMode);
				break;
			case 7:
				Screen.SetResolution(3840, 2160, optionsFullScreenMode);
				break;
			}
		}
		else
		{
			Debug.Log("画面サイズに変更なし");
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
