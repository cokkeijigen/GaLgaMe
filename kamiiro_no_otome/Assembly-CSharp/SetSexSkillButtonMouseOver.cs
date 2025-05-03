using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillButtonMouseOver : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	public bool value;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleManager.isSkillButtonStay = value;
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
