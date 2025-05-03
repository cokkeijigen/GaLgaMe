using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexSelfDamage : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	public StateLink stateLink;

	public StateLink noSelfDamageLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		if (sexBattleManager.heroineSexSkillData.actionType == SexSkillData.ActionType.piston)
		{
			Transition(stateLink);
		}
		else
		{
			Transition(noSelfDamageLink);
		}
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
