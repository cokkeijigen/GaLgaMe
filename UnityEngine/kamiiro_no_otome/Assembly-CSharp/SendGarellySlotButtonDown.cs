using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendGarellySlotButtonDown : StateBehaviour
{
	private GarellyManager garellyManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		garellyManager = GameObject.Find("Garelly Manager").GetComponent<GarellyManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		string text = parameterContainer.GetString("sceneName");
		string text2 = text.Substring(0, 6);
		Debug.Log("ジャンプするシナリオ名の前半部分：" + text2);
		if (text2 == "H_Lucy" && PlayerOptionsDataManager.isLucyVoiceTypeSexy)
		{
			text += "_sexy";
		}
		garellyManager.PushSlotButton(text);
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
