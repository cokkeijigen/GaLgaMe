using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleSpeed : StateBehaviour
{
	private SexBattleManager sexBattleManager;

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
		switch (sexBattleManager.battleSpeed)
		{
		case 1:
			sexBattleManager.battleSpeed = 2;
			sexBattleManager.speedTmpGo.text = "2";
			break;
		case 2:
			sexBattleManager.battleSpeed = 4;
			sexBattleManager.speedTmpGo.text = "4";
			break;
		case 4:
			sexBattleManager.battleSpeed = 1;
			sexBattleManager.speedTmpGo.text = "1";
			break;
		}
		Debug.Log("戦闘速度：" + sexBattleManager.battleSpeed);
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
