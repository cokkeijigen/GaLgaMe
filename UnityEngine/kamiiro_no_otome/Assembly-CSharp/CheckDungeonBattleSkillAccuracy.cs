using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonBattleSkillAccuracy : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	private BattleSkillData battleSkillData;

	public StateLink trueLink;

	public StateLink falseLink;

	public StateLink positiveSkillLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		battleSkillData = dungeonBattleManager.battleSkillData;
		if (!parameterContainer.GetBool("isPositiveSkill"))
		{
			CalcAccuracy();
		}
		else
		{
			Transition(positiveSkillLink);
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

	private void CalcAccuracy()
	{
		float num = 0f;
		num = Random.Range(0, 100);
		if ((float)battleSkillData.skillAccuracy >= num)
		{
			Transition(trueLink);
		}
		else
		{
			Invoke("InvokeMethod", 0.3f);
		}
		Debug.Log("命中率：" + battleSkillData.skillAccuracy + " / スキル命中ランダム：" + num);
	}

	private void InvokeMethod()
	{
		Transition(falseLink);
	}
}
