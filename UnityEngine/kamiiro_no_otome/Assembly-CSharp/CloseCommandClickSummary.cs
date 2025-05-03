using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CloseCommandClickSummary : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleSkillManager.commandClickSummaryWindow.SetActive(value: false);
		scenarioBattleSkillManager.blackImageGoArray[0].SetActive(value: true);
		scenarioBattleSkillManager.blackImageGoArray[1].SetActive(value: false);
		scenarioBattleUiManager.SetMaterialEffect("none");
		scenarioBattleTurnManager.setFrameTypeName = "select";
		if (scenarioBattleSkillManager.isUseSkill)
		{
			scenarioBattleSkillManager.skillWindow.SetActive(value: true);
			scenarioBattleSkillManager.skillFSM.SendTrigger("RefreshSkillWindow");
		}
		else
		{
			scenarioBattleSkillManager.itemWindow.SetActive(value: true);
			scenarioBattleSkillManager.itemFSM.SendTrigger("RefreshItemWindow");
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
