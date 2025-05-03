using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SendSelectSexSkillId : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		if (sexBattleManager.isSkillButtonStay)
		{
			Debug.Log("スキルボタンStay中");
			return;
		}
		int @int = parameterContainer.GetInt("skillID");
		sexBattleManager.selectSkillID = @int;
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
