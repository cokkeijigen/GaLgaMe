using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckScenarioBattleInitialize : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	private bool isCharacterSetUp;

	private bool isCharacterHpSetUp;

	private bool isEnemySetUp;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleUiManager.SetMaterialEffect("none");
		foreach (GameObject playerFrameGo in utageBattleSceneManager.playerFrameGoList)
		{
			playerFrameGo.GetComponent<ArborFSM>().SendTrigger("ResetCharacterFrame");
		}
		foreach (GameObject enemyButtonGo in utageBattleSceneManager.enemyButtonGoList)
		{
			enemyButtonGo.GetComponent<ArborFSM>().SendTrigger("ResetEnemyButton");
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		isCharacterSetUp = utageBattleSceneManager.isCharacterButtonSetUp.All((bool x) => x);
		isCharacterHpSetUp = utageBattleSceneManager.isCharacterHpGroupSetUp.All((bool x) => x);
		isEnemySetUp = utageBattleSceneManager.isEnemyGroupSetUp.All((bool y) => y);
		if (isCharacterSetUp && isCharacterHpSetUp && isEnemySetUp)
		{
			Transition(stateLink);
			Debug.Log("戦闘初期化完了");
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
