using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CloseSkillWindow : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleSkillManager.skillWindow.SetActive(value: false);
		scenarioBattleSkillManager.commandClickSummaryWindow.SetActive(value: false);
		scenarioBattleSkillManager.blackImageGoArray[0].SetActive(value: false);
		scenarioBattleSkillManager.blackImageGoArray[1].SetActive(value: false);
		scenarioBattleTurnManager.isUseSkillPlayer = false;
		scenarioBattleSkillManager.isUseSkill = false;
		scenarioBattleTurnManager.setFrameTypeName = "reset";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		utageBattleSceneManager.SetSelectFrameEnable(isEnable: true);
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
