using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CalcPlayerSoloChargeAttack : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		List<int> list = new List<int>();
		string text = "";
		int skillID = 0;
		ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
		if (scenarioBattleData.supportBattleCharacterID != int.MaxValue && scenarioBattleData.isSupportCharacterTakeDamage)
		{
			int[] memberArray = scenarioBattleData.battleCharacterID.ToArray();
			int i;
			for (i = 0; i < memberArray.Length; i++)
			{
				if (PlayerStatusDataManager.characterSp[memberArray[i]] >= 100 && memberArray.Contains(memberArray[i]) && !PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == memberArray[i]).isDead)
				{
					Debug.Log("チャージ押下／チャージ済みID：" + memberArray[i]);
					list.Add(memberArray[i]);
					break;
				}
			}
		}
		else
		{
			int j;
			for (j = 0; j < PlayerStatusDataManager.partyMemberCount; j++)
			{
				if (PlayerStatusDataManager.characterSp[j] >= 100 && PlayerStatusDataManager.playerPartyMember.Contains(j) && !PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == j).isDead)
				{
					Debug.Log("チャージ押下／チャージ済みID：" + j);
					list.Add(j);
					break;
				}
			}
		}
		scenarioBattleSkillManager.chargeSkillCharacterId = list;
		if (list.Count > 1)
		{
			for (int k = 0; k < list.Count; k++)
			{
				text += list[k];
			}
			Debug.Log("連続するインデックス：" + text);
		}
		else
		{
			text = list[0].ToString();
		}
		for (int l = list.Count; l < 5; l++)
		{
			text += 0;
		}
		Debug.Log("５桁のインデックス：" + text);
		skillID = int.Parse(text);
		scenarioBattleSkillManager.isUseSkill = true;
		scenarioBattleTurnManager.isUseSkillPlayer = true;
		scenarioBattleTurnManager.isAllTargetSkill = true;
		scenarioBattleSkillManager.isUseChargeSkill = true;
		BattleSkillData battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
		scenarioBattleTurnManager.useSkillData = battleSkillData;
		scenarioBattleTurnManager.battleUseSkillID = battleSkillData.skillID;
		scenarioBattleTurnManager.useSkillPartyMemberID = 0;
		Debug.Log("チャージスキル／ID：" + battleSkillData.skillID);
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "simpleCircleGlowBlue").effectPrefabGo;
		Transform transform = PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.chargeButtonPointRect.transform);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		PoolManager.Pools["SkillEffect"].Despawn(transform, 1f, utageBattleSceneManager.poolSkillManagerGO.transform);
		scenarioBattleSkillManager.chargeFSM.SendTrigger("StartChargeSkill");
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
