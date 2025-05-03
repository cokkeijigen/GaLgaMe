using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckIsPlayerCumShot : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexBattleManager sexBattleManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public Type type;

	public float waitTime;

	public float waitTime2;

	public bool isVictoryCumShot;

	public StateLink noLimitLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		float num = waitTime / (float)sexBattleManager.battleSpeed;
		switch (type)
		{
		case Type.player:
			if (sexBattleManager.selectSkillID == 200)
			{
				Invoke("InvokeMethod", num);
			}
			else if (sexBattleFertilizationManager.isInSideCumShot && !PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				Invoke("InvokeMethod", num);
			}
			else
			{
				Invoke("InvokeMethod", num / 3f);
			}
			break;
		case Type.heroine:
			Invoke("InvokeMethod2", num);
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
		if (isVictoryCumShot)
		{
			sexBattleFertilizationManager.CalcVictoryCumShotProcess();
		}
		else
		{
			sexBattleFertilizationManager.CalcCumShotProcess();
		}
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		bool flag = false;
		if (isVictoryCumShot)
		{
			Transition(stateLink);
			return;
		}
		switch (type)
		{
		case Type.player:
			if (PlayerSexStatusDataManager.playerSexExtasyLimit[0] <= 0)
			{
				flag = true;
			}
			break;
		case Type.heroine:
			if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] <= 0)
			{
				flag = true;
			}
			break;
		}
		if (flag)
		{
			Transition(noLimitLink);
		}
		else
		{
			Transition(stateLink);
		}
	}
}
