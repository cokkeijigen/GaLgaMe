using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioSkillBuffText : StateBehaviour
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
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				list = PlayerBattleConditionAccess.GetBattleAliveMemberList(PlayerBattleConditionManager.playerIsDead);
				if (list.Count > 1)
				{
					string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
					utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = allTargetTerm;
				}
				else
				{
					int memberID = PlayerBattleConditionManager.playerIsDead[list[0]].memberID;
					utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + memberID;
				}
			}
			else
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
		}
		else if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
		}
		else
		{
			int memberID = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.enemyTargetNum];
			utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + memberID;
		}
		if (battleSkillData.skillType == BattleSkillData.SkillType.regeneration)
		{
			utageBattleSceneManager.battleTextArray3[4].GetComponent<Localize>().Term = "battleTextEffectAddRegeneration";
			Debug.Log("スキルタイプ：リジェネ");
		}
		else
		{
			utageBattleSceneManager.battleTextArray3[4].GetComponent<Localize>().Term = "battleTextEffect" + text;
			utageBattleSceneManager.battleTextArray3[5].GetComponent<Localize>().Term = "battleTextEffectUp";
			utageBattleSceneManager.battleTextArray3[5].SetActive(value: true);
			Debug.Log("スキルタイプ：バフ");
		}
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[4].SetActive(value: true);
		string effectType = battleSkillData.effectType;
		string text2 = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text2 + effectType.Substring(1);
		MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
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
