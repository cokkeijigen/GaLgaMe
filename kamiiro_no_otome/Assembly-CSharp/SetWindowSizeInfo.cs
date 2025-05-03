using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetWindowSizeInfo : StateBehaviour
{
	public Text widthText;

	public Text heightText;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		widthText.text = Screen.currentResolution.width.ToString();
		heightText.text = Screen.currentResolution.height.ToString();
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
