using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetMouseWheelPower : StateBehaviour
{
	public ScrollRect scrollRect;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		scrollRect.scrollSensitivity = PlayerOptionsDataManager.optionsMouseWheelPower * 10f;
	}

	public override void OnStateLateUpdate()
	{
	}
}
