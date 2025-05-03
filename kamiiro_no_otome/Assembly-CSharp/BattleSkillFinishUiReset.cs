using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class BattleSkillFinishUiReset : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

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
		GameObject[] commandButtonGroup = utageBattleSceneManager.commandButtonGroup;
		for (int i = 0; i < commandButtonGroup.Length; i++)
		{
			commandButtonGroup[i].SetActive(value: true);
		}
		CanvasGroup component = utageBattleSceneManager.battleCanvas.GetComponent<CanvasGroup>();
		component.alpha = 1f;
		component.interactable = true;
		scenarioBattleTurnManager.battleUseSkillID = 0;
		utageBattleSceneManager.SetEnemyTargetGroupVisble(isVisible: true);
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
