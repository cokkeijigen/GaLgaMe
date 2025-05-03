using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcSexRegenerationPower : StateBehaviour
{
	private SexBattleTurnManager sexBattleTurnManager;

	public StateLink noRegeneLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		int num = 100;
		PlayerSexStatusDataManager.MemberSexBuffCondition memberSexBuffCondition = PlayerSexStatusDataManager.playerSexBuffCondition[0].Find((PlayerSexStatusDataManager.MemberSexBuffCondition data) => data.type == "regeneration");
		if (memberSexBuffCondition != null)
		{
			if (PlayerSexStatusDataManager.playerSexBuffCondition[0].Count > 0)
			{
				int sexBattleBuffCondition = PlayerSexBattleConditionAccess.GetSexBattleBuffCondition(PlayerSexStatusDataManager.playerSexBuffCondition[0], "heal");
				num += sexBattleBuffCondition;
			}
			float num2 = (float)PlayerSexStatusDataManager.playerSexMaxHp[0] * ((float)memberSexBuffCondition.power / 100f);
			num2 *= (float)num / 100f;
			float num3 = Random.Range(0.9f, 1.1f);
			num2 *= num3;
			sexBattleTurnManager.sexBattleHealValue = Mathf.RoundToInt(num2);
			Debug.Log("リジェネである");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("リジェネではない");
			Transition(noRegeneLink);
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
