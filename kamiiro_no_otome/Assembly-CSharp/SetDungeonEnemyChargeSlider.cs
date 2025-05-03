using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonEnemyChargeSlider : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private int afterCharge;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = base.transform.parent.GetComponent<DungeonBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		if (parameterContainer.GetBool("isPlayerSkill"))
		{
			Transition(stateLink);
			return;
		}
		afterCharge = 0;
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		dungeonBattleManager.enemyChargeSlider.value = afterCharge;
		dungeonBattleManager.enemyChargeText.text = afterCharge.ToString();
		dungeonBattleManager.enemyCharge = afterCharge;
	}

	public override void OnStateUpdate()
	{
		dungeonBattleManager.enemyChargeText.text = Mathf.Floor(dungeonBattleManager.enemyChargeSlider.value).ToString();
	}

	public override void OnStateLateUpdate()
	{
	}
}
