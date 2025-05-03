using System.Collections.Generic;
using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioSkillDebuffText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private BattleSkillData battleSkillData;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		List<int> list = new List<int>();
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		string text = battleSkillData.buffType.ToString();
		text = text.Substring(0, 1).ToUpper() + text.Substring(1);
		if (scenarioBattleTurnManager.skillAttackHitDataSubList.Count > 0)
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				if (scenarioBattleTurnManager.isUseSkillPlayer)
				{
					list = PlayerBattleConditionAccess.GetBattleAliveMemberList(PlayerBattleConditionManager.enemyIsDead);
					if (scenarioBattleTurnManager.skillAttackHitDataSubList.Count > 1)
					{
						utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "battleTextEnemyMultiTarget";
					}
					else
					{
						int memberID = scenarioBattleTurnManager.skillAttackHitDataSubList[0].memberID;
						utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "enemy" + memberID;
					}
				}
				else
				{
					list = PlayerBattleConditionAccess.GetBattleAliveMemberList(PlayerBattleConditionManager.playerIsDead);
					if (scenarioBattleTurnManager.skillAttackHitDataSubList.Count > 1)
					{
						string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
						utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = allTargetTerm;
					}
					else
					{
						int memberID = scenarioBattleTurnManager.skillAttackHitDataSubList[0].memberID;
						utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "character" + memberID;
					}
				}
			}
			else if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int memberID = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
				utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "enemy" + memberID;
			}
			else
			{
				int memberID = PlayerStatusDataManager.playerPartyMember[scenarioBattleTurnManager.enemyTargetNum];
				utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "character" + memberID;
			}
			utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "battleTextEffect" + text;
			utageBattleSceneManager.battleTextArray4[2].GetComponent<Localize>().Term = "battleTextEffectDown";
			utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
			utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
			utageBattleSceneManager.battleTextArray4[2].SetActive(value: true);
		}
		else
		{
			if (scenarioBattleTurnManager.isAllTargetSkill)
			{
				if (scenarioBattleTurnManager.isUseSkillPlayer)
				{
					list = PlayerBattleConditionAccess.GetBattleAliveMemberList(PlayerBattleConditionManager.enemyIsDead);
					if (list.Count > 1)
					{
						utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "battleTextEnemyAllTarget";
					}
					else
					{
						int memberID = PlayerBattleConditionManager.enemyIsDead[list[0]].memberID;
						utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + memberID;
					}
				}
				else
				{
					list = PlayerBattleConditionAccess.GetBattleAliveMemberList(PlayerBattleConditionManager.playerIsDead);
					if (list.Count > 1)
					{
						string allTargetTerm2 = scenarioBattleSkillManager.GetAllTargetTerm();
						utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = allTargetTerm2;
					}
					else
					{
						int memberID = PlayerBattleConditionManager.playerIsDead[list[0]].memberID;
						utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + memberID;
					}
				}
			}
			else if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int memberID = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + memberID;
			}
			else
			{
				int memberID = PlayerStatusDataManager.playerPartyMember[scenarioBattleTurnManager.enemyTargetNum];
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + memberID;
			}
			utageBattleSceneManager.battleTextArray3[4].GetComponent<Localize>().Term = "battleTextEffect" + text;
			utageBattleSceneManager.battleTextArray3[5].GetComponent<Localize>().Term = "battleTextEffectDown";
			utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
			utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
			utageBattleSceneManager.battleTextArray3[4].SetActive(value: true);
			utageBattleSceneManager.battleTextArray3[5].SetActive(value: true);
		}
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
