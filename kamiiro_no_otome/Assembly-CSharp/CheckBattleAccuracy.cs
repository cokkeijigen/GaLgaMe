using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckBattleAccuracy : StateBehaviour
{
	public enum Type
	{
		player,
		enemy,
		support
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public float waitTime;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		int num = 0;
		int num2 = 0;
		switch (type)
		{
		case Type.player:
		{
			int supportAttackMemberId = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID;
			int memberNum2 = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberNum;
			int memberNum = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.playerTargetNum].memberNum;
			num = PlayerStatusDataManager.characterAccuracy[supportAttackMemberId];
			num2 = PlayerStatusDataManager.enemyEvasion[memberNum];
			if (PlayerBattleConditionManager.playerBuffCondition[memberNum2].Count > 0)
			{
				int battleBuffCondition2 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[memberNum2], "accuracy");
				float buffPower2 = GetBuffPower(battleBuffCondition2);
				num = Mathf.RoundToInt((float)num * buffPower2);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[memberNum].Count > 0)
			{
				int battleBuffCondition3 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "evasion");
				float buffPower3 = GetBuffPower(battleBuffCondition3);
				num2 = Mathf.RoundToInt((float)num2 * buffPower3);
				Debug.Log("敵の回避バフ：" + buffPower3);
			}
			break;
		}
		case Type.enemy:
		{
			int supportAttackMemberId = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.enemyTargetNum].memberID;
			int memberNum2 = scenarioBattleTurnManager.enemyTargetNum;
			int memberNum = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberNum;
			num = PlayerStatusDataManager.enemyAccuracy[memberNum];
			num2 = PlayerStatusDataManager.characterEvasion[supportAttackMemberId];
			if (PlayerBattleConditionManager.playerBuffCondition[memberNum2].Count > 0)
			{
				int battleBuffCondition4 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.playerBuffCondition[memberNum2], "evasion");
				float buffPower4 = GetBuffPower(battleBuffCondition4);
				num2 = Mathf.RoundToInt((float)num2 * buffPower4);
			}
			if (PlayerBattleConditionManager.enemyBuffCondition[memberNum].Count > 0)
			{
				int battleBuffCondition5 = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "accuracy");
				float buffPower5 = GetBuffPower(battleBuffCondition5);
				num = Mathf.RoundToInt((float)num * buffPower5);
				Debug.Log("敵の命中バフ：" + buffPower5);
			}
			break;
		}
		case Type.support:
		{
			int supportAttackMemberId = utageBattleSceneManager.supportAttackMemberId;
			int memberNum = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.playerTargetNum].memberNum;
			num = PlayerStatusDataManager.characterAccuracy[supportAttackMemberId];
			num2 = PlayerStatusDataManager.enemyEvasion[memberNum];
			if (PlayerBattleConditionManager.enemyBuffCondition[memberNum].Count > 0)
			{
				int battleBuffCondition = PlayerBattleConditionAccess.GetBattleBuffCondition(PlayerBattleConditionManager.enemyBuffCondition[memberNum], "evasion");
				float buffPower = GetBuffPower(battleBuffCondition);
				num2 = Mathf.RoundToInt((float)num2 * buffPower);
				Debug.Log("敵の回避バフ：" + buffPower);
			}
			break;
		}
		}
		int value = num - num2;
		value = Mathf.Clamp(value, 0, 100);
		int num3 = Random.Range(0, 100);
		Debug.Log("命中率；" + num + "／回避率：" + num2 + "／合計値：" + value + "／ランダム値：" + num3);
		if (value >= num3)
		{
			Transition(trueLink);
		}
		else
		{
			Invoke("InvokeMethod", waitTime);
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
		Transition(falseLink);
	}

	private float GetBuffPower(int num)
	{
		float result = 1f;
		if (num != 0)
		{
			result = (float)num / 100f + 1f;
		}
		return result;
	}
}
