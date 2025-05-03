using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetBattleCanvasInteractable : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public bool isInteractable;

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
		CanvasGroup component = utageBattleSceneManager.battleCanvas.GetComponent<CanvasGroup>();
		CanvasGroup component2 = utageBattleSceneManager.chargeAttackButton.GetComponent<CanvasGroup>();
		if (isInteractable)
		{
			if (PlayerNonSaveDataManager.isChargeAttackTutorial)
			{
				component.alpha = 1f;
				component.interactable = false;
				component2.interactable = true;
			}
			else
			{
				component.alpha = 1f;
				component.interactable = true;
				component2.interactable = true;
			}
		}
		else
		{
			component.interactable = false;
			component2.interactable = false;
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
