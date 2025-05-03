using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioBattleSpeed : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

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
		switch (utageBattleSceneManager.battleSpeed)
		{
		case 1:
			utageBattleSceneManager.battleSpeed = 2;
			utageBattleSceneManager.speedTmpGO.text = "2";
			break;
		case 2:
			utageBattleSceneManager.battleSpeed = 4;
			utageBattleSceneManager.speedTmpGO.text = "4";
			break;
		case 4:
			utageBattleSceneManager.battleSpeed = 1;
			utageBattleSceneManager.speedTmpGO.text = "1";
			break;
		}
		PlayerDataManager.scenarioBattleSpeed = utageBattleSceneManager.battleSpeed;
		Debug.Log("戦闘速度：" + utageBattleSceneManager.battleSpeed);
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
