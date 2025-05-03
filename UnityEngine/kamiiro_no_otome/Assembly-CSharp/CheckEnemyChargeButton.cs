using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckEnemyChargeButton : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private bool isEnemySetUp;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		isEnemySetUp = utageBattleSceneManager.isEnemyGroupSetUp.All((bool x) => x);
		if (isEnemySetUp)
		{
			if (scenarioBattleSkillManager.isEnemyChargeMax)
			{
				MasterAudio.PlaySound("SeEnemySkillChargeMax", 1f, null, 0f, null, null);
				scenarioBattleSkillManager.isEnemyChargeMax = false;
			}
			Debug.Log("チャージ確認完了");
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
