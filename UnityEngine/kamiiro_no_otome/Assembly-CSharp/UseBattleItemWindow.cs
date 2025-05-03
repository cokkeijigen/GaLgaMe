using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class UseBattleItemWindow : StateBehaviour
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
		PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(scenarioBattleTurnManager.battleUseItemID, 1);
		scenarioBattleTurnManager.battleUseItemCount++;
		int num = scenarioBattleTurnManager.battleEnableUseItemMaxNum - scenarioBattleTurnManager.battleUseItemCount;
		scenarioBattleTurnManager.itemEnableUseCountText.text = num.ToString();
		utageBattleSceneManager.itemButton.GetComponent<ArborFSM>().SendTrigger("CheckItemUseCount");
		scenarioBattleSkillManager.itemWindow.SetActive(value: false);
		scenarioBattleSkillManager.commandClickSummaryWindow.SetActive(value: false);
		scenarioBattleSkillManager.blackImageGoArray[0].SetActive(value: false);
		scenarioBattleSkillManager.blackImageGoArray[1].SetActive(value: false);
		GameObject[] commandButtonGroup = utageBattleSceneManager.commandButtonGroup;
		for (int i = 0; i < commandButtonGroup.Length; i++)
		{
			commandButtonGroup[i].SetActive(value: false);
		}
		utageBattleSceneManager.battleCanvas.GetComponent<CanvasGroup>().interactable = false;
		utageBattleSceneManager.SetEnemyTargetGroupVisble(isVisible: false);
		scenarioBattleTurnManager.setFrameTypeName = "reset";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
		scenarioBattleUiManager.SetMaterialEffect("none");
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
