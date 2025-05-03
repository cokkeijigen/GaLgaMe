using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckStartSupportAttack : StateBehaviour
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
		if (utageBattleSceneManager.supportAttackMemberId != int.MaxValue)
		{
			switch (utageBattleSceneManager.supportAttackMemberId)
			{
			case 1:
				utageBattleSceneManager.supportHealTurnFSM.SendTrigger("StartSupportHeal");
				break;
			case 4:
			{
				scenarioBattleTurnManager.isUseSkillPlayer = true;
				scenarioBattleTurnManager.isSupportSkill = true;
				scenarioBattleTurnManager.useSkillPartyMemberID = utageBattleSceneManager.supportAttackMemberId;
				scenarioBattleTurnManager.battleUseSkillID = 141;
				BattleSkillData useSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == 141);
				scenarioBattleTurnManager.useSkillData = useSkillData;
				utageBattleSceneManager.supportSkillTurnFSM.SendTrigger("StartSupportSkill");
				break;
			}
			case 5:
				utageBattleSceneManager.supportAttackTurnFSM.SendTrigger("StartSupportAttack");
				break;
			case 2:
			case 3:
				break;
			}
		}
		else
		{
			Transition(stateLink);
		}
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
