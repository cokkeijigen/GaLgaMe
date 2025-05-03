using System.Collections.Generic;
using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioSkillBadStateText : StateBehaviour
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
		string text = battleSkillData.subType.ToString();
		text = text.Substring(0, 1).ToUpper() + text.Substring(1);
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				list = PlayerBattleConditionAccess.GetBattleAliveMemberList(PlayerBattleConditionManager.enemyIsDead);
				if (list.Count > 1)
				{
					utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "battleTextEnemyAllTarget";
				}
				else
				{
					int memberID = PlayerBattleConditionManager.enemyIsDead[list[0]].memberID;
					utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "enemy" + memberID;
				}
			}
			else
			{
				list = PlayerBattleConditionAccess.GetBattleAliveMemberList(PlayerBattleConditionManager.playerIsDead);
				if (list.Count > 1)
				{
					string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
					utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = allTargetTerm;
				}
				else
				{
					int memberID = PlayerBattleConditionManager.playerIsDead[list[0]].memberID;
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
		utageBattleSceneManager.battleTextArray4[1].GetComponent<Localize>().Term = "textConjunctionIs";
		switch (battleSkillData.subType)
		{
		case BattleSkillData.SubType.poison:
			utageBattleSceneManager.battleTextArray4[2].GetComponent<Localize>().Term = "battleTextEffectAddPoison";
			utageBattleSceneManager.battleTextArray4[2].SetActive(value: true);
			utageBattleSceneManager.battleTextArray4[3].SetActive(value: false);
			utageBattleSceneManager.battleTextArray4[4].SetActive(value: false);
			break;
		case BattleSkillData.SubType.paralyze:
			utageBattleSceneManager.battleTextArray4[2].GetComponent<Localize>().Term = "battleTextEffectAddParalyze";
			utageBattleSceneManager.battleTextArray4[2].SetActive(value: false);
			utageBattleSceneManager.battleTextArray4[3].SetActive(value: true);
			utageBattleSceneManager.battleTextArray4[4].SetActive(value: false);
			break;
		case BattleSkillData.SubType.stagger:
			utageBattleSceneManager.battleTextArray4[2].GetComponent<Localize>().Term = "battleTextEffectAddStagger";
			utageBattleSceneManager.battleTextArray4[2].SetActive(value: false);
			utageBattleSceneManager.battleTextArray4[3].SetActive(value: false);
			utageBattleSceneManager.battleTextArray4[4].SetActive(value: true);
			break;
		}
		utageBattleSceneManager.battleTextArray4[5].GetComponent<Localize>().Term = "battleTextEffectAddState";
		utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray4[1].SetActive(value: true);
		utageBattleSceneManager.battleTextArray4[5].SetActive(value: true);
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
