using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAttackFactorEffect : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private int poisonSuccessRate;

	private int paralyzeSuccessRate;

	private int staggerSuccessRate;

	public StateLink effectHitLink;

	public StateLink noEffectLink;

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
		int memberID = PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID;
		poisonSuccessRate = PlayerEquipDataManager.equipFactorPoison[memberID];
		paralyzeSuccessRate = PlayerEquipDataManager.equipFactorParalyze[memberID];
		staggerSuccessRate = PlayerEquipDataManager.equipFactorStagger[memberID];
		if (poisonSuccessRate == 0 && paralyzeSuccessRate == 0 && staggerSuccessRate == 0)
		{
			Debug.Log("ファクター付与効果なし");
			Transition(noEffectLink);
			return;
		}
		int num = PlayerStatusDataManager.enemyResist[scenarioBattleTurnManager.playerTargetNum];
		poisonSuccessRate -= num;
		poisonSuccessRate = Mathf.Clamp(poisonSuccessRate, 0, 100);
		paralyzeSuccessRate -= num;
		paralyzeSuccessRate = Mathf.Clamp(paralyzeSuccessRate, 0, 100);
		staggerSuccessRate -= num;
		staggerSuccessRate = Mathf.Clamp(staggerSuccessRate, 0, 100);
		int num2 = Random.Range(0, 100);
		Debug.Log($"ファクター命中率：{poisonSuccessRate}／命中ランダム：{num2}");
		if (poisonSuccessRate >= num2 && poisonSuccessRate > 0)
		{
			scenarioBattleTurnManager.factorEffectSuccessList[0] = true;
		}
		int num3 = Random.Range(0, 100);
		Debug.Log($"ファクター命中率：{paralyzeSuccessRate}／命中ランダム：{num3}");
		if (paralyzeSuccessRate >= num3 && paralyzeSuccessRate > 0)
		{
			scenarioBattleTurnManager.factorEffectSuccessList[1] = true;
		}
		int num4 = Random.Range(0, 100);
		Debug.Log($"ファクター命中率：{staggerSuccessRate}／命中ランダム：{num4}");
		if (staggerSuccessRate >= num4 && staggerSuccessRate > 0)
		{
			scenarioBattleTurnManager.factorEffectSuccessList[2] = true;
		}
		if (scenarioBattleTurnManager.factorEffectSuccessList.Any((bool data) => data))
		{
			Transition(effectHitLink);
			return;
		}
		utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "battleTextNoEffect";
		utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
		Invoke("InvokeMethod", 0.3f);
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
		Transition(noEffectLink);
	}
}
