using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetBattleCommandVisible : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public bool setVisibleValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		utageBattleSceneManager.chargeAttackButton.SetActive(value: false);
		GameObject[] commandButtonGroup = utageBattleSceneManager.commandButtonGroup;
		for (int i = 0; i < commandButtonGroup.Length; i++)
		{
			commandButtonGroup[i].SetActive(setVisibleValue);
		}
		utageBattleSceneManager.SetEnemyTargetGroupVisble(setVisibleValue);
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
