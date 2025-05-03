using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendSkillButtonRefresh : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private PlayMakerFSM skillButtonFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		GameObject gameObject = parameterContainer.GetGameObject("statusGroupParentGo");
		skillButtonFSM = gameObject.GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		skillButtonFSM.SendEvent("SkillButtonRefresh");
		Debug.Log("PMにイベントを送信／スキルボタンの更新");
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
