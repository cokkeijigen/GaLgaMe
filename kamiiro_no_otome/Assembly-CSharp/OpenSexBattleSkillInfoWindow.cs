using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class OpenSexBattleSkillInfoWindow : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	public bool isVisible;

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
		sexBattleManager.skillInfoWindowGo.SetActive(isVisible);
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
