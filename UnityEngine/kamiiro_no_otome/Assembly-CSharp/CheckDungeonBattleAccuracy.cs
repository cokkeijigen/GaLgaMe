using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBattleAccuracy : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	public int paralyzeProbability;

	public StateLink trueLink;

	public StateLink falseLink;

	public StateLink chargeLink;

	public StateLink paralyzeLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager.isCriticalAttack = false;
		parameterContainer.SetInt("enemyAttackDamage", 0);
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		string text = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		int num6 = int.Parse(parameterContainer.GetStringList("AgilityQueueList")[0].Substring(1));
		switch (text)
		{
		case "p":
			num5 = PlayerStatusDataManager.playerPartyMember[num6];
			num = PlayerStatusDataManager.characterAccuracy[num5] - PlayerStatusDataManager.enemyAllEvasion;
			num2 = PlayerStatusDataManager.characterCritical[num5];
			if (PlayerBattleConditionManager.playerBuffCondition[0].Count > 0)
			{
				num3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[0], "critical");
			}
			break;
		case "e":
		{
			if (dungeonBattleManager.enemyCharge >= 100)
			{
				Transition(chargeLink);
			}
			num = PlayerStatusDataManager.enemyAccuracy[num6] - PlayerStatusDataManager.playerAllEvasion;
			num2 = PlayerStatusDataManager.enemyCritical[num6];
			for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
			{
				num4 += PlayerEquipDataManager.equipFactorCriticalResist[PlayerStatusDataManager.playerPartyMember[i]];
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[0].Count > 0)
			{
				num3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[0], "critical");
			}
			break;
		}
		case "c":
			Transition(chargeLink);
			break;
		}
		int num7 = Random.Range(0, 100);
		bool flag = false;
		if (!(text == "p"))
		{
			if (text == "e" && PlayerBattleConditionAccess.GetBattleBadState(PlayerBattleConditionManager.enemyBadState[0], "paralyze") > 0 && num7 <= paralyzeProbability)
			{
				flag = true;
			}
		}
		else if (PlayerBattleConditionAccess.GetBattleBadState(PlayerBattleConditionManager.playerBadState[0], "paralyze") > 0 && num7 <= paralyzeProbability)
		{
			flag = true;
		}
		if (flag)
		{
			Transition(paralyzeLink);
		}
		int num8 = Random.Range(0, 100);
		Debug.Log($"命中判定：{num}>={num8}");
		if (num >= num8)
		{
			int num9 = Random.Range(0, 100);
			num2 = Mathf.Clamp(num2 + num3 - num4, 0, 100);
			if (num2 >= num9)
			{
				dungeonBattleManager.isCriticalAttack = true;
			}
			else
			{
				dungeonBattleManager.isCriticalAttack = false;
			}
			Debug.Log($"クリティカル判定：{dungeonBattleManager.isCriticalAttack}／判定：{num2}/{num9}");
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
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
