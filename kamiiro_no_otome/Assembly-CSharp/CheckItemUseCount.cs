using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckItemUseCount : StateBehaviour
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
		CanvasGroup component = utageBattleSceneManager.itemButton.GetComponent<CanvasGroup>();
		if (scenarioBattleTurnManager.battleUseItemCount < scenarioBattleTurnManager.battleEnableUseItemMaxNum)
		{
			component.interactable = true;
			component.alpha = 1f;
		}
		else
		{
			component.interactable = false;
			component.alpha = 0.5f;
		}
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
