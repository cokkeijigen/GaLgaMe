using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetWindowSize : StateBehaviour
{
	public bool isFullScreen;

	public ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		parameterContainer.SetBool("preFullScreenMode", value: false);
		switch (PlayerOptionsDataManager.optionsWindowSize)
		{
		case 0:
			Screen.SetResolution(1024, 576, isFullScreen);
			break;
		case 1:
			Screen.SetResolution(1280, 720, isFullScreen);
			break;
		case 2:
			Screen.SetResolution(1440, 810, isFullScreen);
			break;
		case 3:
			Screen.SetResolution(1600, 900, isFullScreen);
			break;
		case 4:
			Screen.SetResolution(1920, 1080, isFullScreen);
			break;
		case 5:
			Screen.SetResolution(2240, 1260, isFullScreen);
			break;
		case 6:
			Screen.SetResolution(2560, 1440, isFullScreen);
			break;
		case 7:
			Screen.SetResolution(3840, 2160, isFullScreen);
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
