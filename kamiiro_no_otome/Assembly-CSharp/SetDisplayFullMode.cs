using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetDisplayFullMode : StateBehaviour
{
	public ParameterContainer parameterContainer;

	private int height;

	private int width;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		width = (int)PlayerOptionsDataManager.currentDisplayWidth;
		int num = Mathf.FloorToInt(PlayerOptionsDataManager.currentDisplayWidth / 16f);
		height = num * 9;
		Screen.SetResolution(width, height, fullscreen: true);
		parameterContainer.SetBool("preFullScreenMode", value: true);
		Debug.Log("フルスクリーンサイズ：" + width + "×" + height);
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
