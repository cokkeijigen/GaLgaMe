using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioFactorBadStateText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public float waitTime;

	public StateLink stateLink;

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
		int num = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
		if (scenarioBattleTurnManager.factorEffectSuccessList[0])
		{
			utageBattleSceneManager.battleTextArray4[2].GetComponent<Localize>().Term = "battleTextEffectAddPoison";
			utageBattleSceneManager.battleTextArray4[2].SetActive(value: true);
		}
		if (scenarioBattleTurnManager.factorEffectSuccessList[1])
		{
			utageBattleSceneManager.battleTextArray4[3].GetComponent<Localize>().Term = "battleTextEffectAddParalyze";
			utageBattleSceneManager.battleTextArray4[3].SetActive(value: true);
		}
		if (scenarioBattleTurnManager.factorEffectSuccessList[2])
		{
			utageBattleSceneManager.battleTextArray4[4].GetComponent<Localize>().Term = "battleTextEffectAddStagger";
			utageBattleSceneManager.battleTextArray4[4].SetActive(value: true);
		}
		utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "enemy" + num;
		utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "textConjunctionIs";
		utageBattleSceneManager.battleTextArray4[5].GetComponent<Localize>().Term = "battleTextEffectAddState";
		utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
		utageBattleSceneManager.battleTextArray4[5].SetActive(value: true);
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
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
		Transition(stateLink);
	}
}
