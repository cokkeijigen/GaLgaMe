using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetBattleViewWithItemWindow : StateBehaviour
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
		ItemData useItemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == scenarioBattleTurnManager.battleUseItemID);
		scenarioBattleTurnManager.useItemData = useItemData;
		scenarioBattleSkillManager.blackImageGoArray[0].SetActive(value: true);
		scenarioBattleTurnManager.setFrameTypeName = "select";
		scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
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
