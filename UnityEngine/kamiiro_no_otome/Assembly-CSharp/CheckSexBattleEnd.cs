using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexBattleEnd : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexBattleManager sexBattleManager;

	public Type type;

	public float waitTime;

	public StateLink continueLink;

	public StateLink afterEcstasySkipLink;

	public StateLink battleEndLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.player:
			if (PlayerSexStatusDataManager.playerSexExtasyLimit[0] > 0)
			{
				Transition(continueLink);
				break;
			}
			sexBattleManager.isSexBattleDefeat = true;
			Invoke("InvokeMethod", waitTime);
			break;
		case Type.heroine:
			if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] > 0 && sexBattleManager.selectSkillID != 200)
			{
				Transition(continueLink);
				break;
			}
			if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] > 0 && sexBattleManager.selectSkillID == 200)
			{
				Transition(afterEcstasySkipLink);
				break;
			}
			sexBattleManager.isSexBattleDefeat = false;
			Invoke("InvokeMethod", waitTime);
			break;
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

	private void InvokeMethod()
	{
		Transition(battleEndLink);
	}
}
