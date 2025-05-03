using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetDisplayWindowMode : StateBehaviour
{
	public int windowSize;

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
		switch (windowSize)
		{
		case 0:
			Screen.SetResolution(1024, 576, fullscreen: false);
			break;
		case 1:
			Screen.SetResolution(1280, 720, fullscreen: false);
			break;
		case 2:
			Screen.SetResolution(1440, 810, fullscreen: false);
			break;
		case 3:
			Screen.SetResolution(1600, 900, fullscreen: false);
			break;
		case 4:
			Screen.SetResolution(1920, 1080, fullscreen: false);
			break;
		case 5:
			Screen.SetResolution(2240, 1260, fullscreen: false);
			break;
		case 6:
			Screen.SetResolution(2560, 1440, fullscreen: false);
			break;
		case 7:
			Screen.SetResolution(3840, 2160, fullscreen: false);
			break;
		}
		parameterContainer.SetInt("preWindowSize", windowSize);
		parameterContainer.SetBool("preFullScreenMode", value: false);
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
