using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class GetWindowSize : StateBehaviour
{
	public StateLink stateLink;

	[SerializeField]
	private float screenWidth;

	[SerializeField]
	private float screenHeight;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		screenWidth = Screen.currentResolution.width;
		screenHeight = Screen.currentResolution.height;
		PlayerOptionsDataManager.currentDisplayWidth = screenWidth;
		PlayerOptionsDataManager.currentDisplayHeight = screenHeight;
		Debug.Log("モニタ解像度" + screenWidth + "×" + screenHeight);
		if (screenHeight < 720f)
		{
			PlayerOptionsDataManager.optionsWindowSize = 0;
		}
		else if (screenHeight < 810f)
		{
			PlayerOptionsDataManager.optionsWindowSize = 1;
		}
		else if (screenHeight < 900f)
		{
			PlayerOptionsDataManager.optionsWindowSize = 2;
		}
		else if (screenHeight < 1080f)
		{
			PlayerOptionsDataManager.optionsWindowSize = 3;
		}
		else if (screenHeight < 1260f)
		{
			PlayerOptionsDataManager.optionsWindowSize = 4;
		}
		else if (screenHeight < 1440f)
		{
			PlayerOptionsDataManager.optionsWindowSize = 5;
		}
		else if (screenHeight < 2160f)
		{
			PlayerOptionsDataManager.optionsWindowSize = 6;
		}
		else
		{
			PlayerOptionsDataManager.optionsWindowSize = 7;
		}
		Debug.Log("ウィンドウサイズ：" + PlayerOptionsDataManager.optionsWindowSize);
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
